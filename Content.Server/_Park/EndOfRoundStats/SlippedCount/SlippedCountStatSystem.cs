using System.Linq;
using Content.Server.GameTicking;
using Content.Server.Mind;
using Content.Shared.Mind.Components;
using Content.Shared.GameTicking;
using Content.Shared.NF14.CCVar;
using Content.Shared.Slippery;
using Content.Shared.StepTrigger.Components;
using Robust.Shared.Configuration;
using Robust.Shared.Timing;

namespace Content.Server._Park.EndOfRoundStats.SlippedCount;

public sealed class SlippedCountStatSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _config = default!;
    [Dependency] private readonly MindSystem _mind = default!;

    Dictionary<PlayerData, int> userSlipStats = new();

    private struct PlayerData
    {
        public String Name;
        public String? Username;
    }


    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StepTriggerComponent, SlipEvent>(OnSlip);

        SubscribeLocalEvent<RoundEndTextAppendEvent>(OnRoundEnd);
        SubscribeLocalEvent<RoundRestartCleanupEvent>(OnRoundRestart);
    }


    private void OnSlip(EntityUid uid, StepTriggerComponent component, ref SlipEvent args)
    {
        string? username = null;

        var entity = args.Slipped;

        if (EntityManager.TryGetComponent<MindContainerComponent>(entity, out var mindComp) &&
            mindComp.Mind != null &&
            _mind.TryGetSession(mindComp.Mind, out var session))
        {
            username = session.Name;
        }

        var playerData = new PlayerData
        {
            Name = MetaData(entity).EntityName,
            Username = username
        };

        if (userSlipStats.ContainsKey(playerData))
        {
            userSlipStats[playerData]++;
            return;
        }

        userSlipStats.Add(playerData, 1);
    }

    private void OnRoundEnd(RoundEndTextAppendEvent ev)
    {
        var sortedSlippers = userSlipStats.OrderByDescending(m => m.Value);

        int totalTimesSlipped = sortedSlippers.Sum(m => (int) m.Value);

        var line = "[color=springGreen]";

        if (totalTimesSlipped < _config.GetCVar(NF14CVars.SlippedCountThreshold))
        {
            if (totalTimesSlipped == 0 && _config.GetCVar(NF14CVars.SlippedCountDisplayNone))
            {
                line += Loc.GetString("eorstats-slippedcount-none");
            }
            else
                return;
        }
        else
        {
            line += Loc.GetString("eorstats-slippedcount-totalslips", ("timesSlipped", totalTimesSlipped));

            line += GenerateTopSlipper(sortedSlippers.First().Key, sortedSlippers.First().Value);
        }

        ev.AddLine("\n" + line + "[/color]");
    }

    private String GenerateTopSlipper(PlayerData data, int amountSlipped)
    {
        var line = String.Empty;

        if (_config.GetCVar(NF14CVars.SlippedCountTopSlipper) == false)
            return line;

        if (data.Username != null)
            line += Loc.GetString
            (
                "eorstats-slippedcount-topslipper-hasusername",
                ("username", data.Username),
                ("name", data.Name),
                ("slipcount", amountSlipped)
            );
        else
            line += Loc.GetString
            (
                "eorstats-slippedcount-topslipper-hasnousername",
                ("name", data.Name),
                ("slipcount", amountSlipped)
            );

        return "\n" + line;
    }

    private void OnRoundRestart(RoundRestartCleanupEvent ev)
    {
        userSlipStats.Clear();
    }
}
