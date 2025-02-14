using System.Linq;
using Content.Server.GameTicking;
using Content.Server.Mind;
using Content.Shared.FixedPoint;
using Content.Shared.GameTicking;
using Content.Shared.NF14.CCVar;
using Robust.Shared.Configuration;

namespace Content.Server._Park.EndOfRoundStats.MopUsed;

public sealed class MopUsedStatSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _config = default!;
    [Dependency] private readonly MindSystem _mind = default!;


    Dictionary<MopperData, FixedPoint2> userMopStats = new();
    int timesMopped = 0;

    private struct MopperData
    {
        public String Name;
        public String? Username;
    }


    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MopUsedStatEvent>(OnMopUsed);

        SubscribeLocalEvent<RoundEndTextAppendEvent>(OnRoundEnd);
        SubscribeLocalEvent<RoundRestartCleanupEvent>(OnRoundRestart);
    }


    private void OnMopUsed(MopUsedStatEvent args)
    {
        timesMopped++;

        var name = MetaData(args.Mopper).EntityName;
        // var username = _mind.TryGetMind(args.Mopper, out var mind) && mind.Session != null ? mind.Session.Name : null;

        var mopperData = new MopperData
        {
            Name = name//,
            // Username = username
        };

        if (userMopStats.ContainsKey(mopperData))
        {
            userMopStats[mopperData] += args.AmountMopped;
            return;
        }

        userMopStats.Add(mopperData, args.AmountMopped);
    }

    private void OnRoundEnd(RoundEndTextAppendEvent ev)
    {
        var line = String.Empty;

        if (userMopStats.Count == 0 && _config.GetCVar<bool>(NF14CVars.MopUsedDisplayNone))
        {
            line += "\n[color=red]" + Loc.GetString("eorstats-mop-noamountmopped") + "[/color]";
        }
        else if (userMopStats.Count == 0)
        {
            return;
        }
        else
        {
            var sortedMoppers = userMopStats.OrderByDescending(m => m.Value);

            int totalAmountMopped = sortedMoppers.Sum(m => (int) m.Value);

            String impressColor;

            if (totalAmountMopped < _config.GetCVar<int>(NF14CVars.MopUsedThreshold))
                return;

            switch (totalAmountMopped)
            {
                case var x when x > 2600:
                    impressColor = "royalBlue";
                    break;
                case var x when x > 1400:
                    impressColor = "gold";
                    break;
                case var x when x > 600:
                    impressColor = "slateBlue";
                    break;
                default:
                    impressColor = "fireBrick";
                    break;
            }

            line += "\n" + Loc.GetString("eorstats-mop-amountmopped", ("amountMopped", totalAmountMopped), ("timesMopped", timesMopped), ("impressColor", impressColor));

            if (_config.GetCVar<int>(NF14CVars.MopUsedTopMopperCount) > 0)
                line += "\n" + Loc.GetString("eorstats-mop-topmopper-header");

            var currentPlace = 1;
            foreach (var mopper in sortedMoppers)
            {
                if (currentPlace > _config.GetCVar<int>(NF14CVars.MopUsedTopMopperCount))
                    break;

                line += GenerateTopMopper(mopper.Key, mopper.Value, currentPlace);

                currentPlace++;
            }
        }

        ev.AddLine(line);
    }

    private String GenerateTopMopper(MopperData data, FixedPoint2 amountMopped, int place)
    {
        var line = String.Empty;

        if (amountMopped > 0)
        {
            String impressColor;

            switch (place)
            {
                case 1:
                    impressColor = "gold";
                    break;
                case 2:
                    impressColor = "slateBlue";
                    break;
                default:
                    impressColor = "fireBrick";
                    break;
            }

            // if (data.Username != null)
            //     line += "\n" + Loc.GetString
            //     (
            //         "eorstats-mop-topmopper-hasusername",
            //         ("name", data.Name),
            //         ("username", data.Username),
            //         ("amountMopped", (int) amountMopped),
            //         ("impressColor", impressColor),
            //         ("place", place)
            //     );
            // else
                line += "\n" + Loc.GetString
                (
                    "eorstats-mop-topmopper-hasnousername",
                    ("name", data.Name),
                    ("amountMopped", (int) amountMopped),
                    ("impressColor", impressColor),
                    ("place", place)
                );
        }

        return line;
    }

    private void OnRoundRestart(RoundRestartCleanupEvent ev)
    {
        userMopStats.Clear();
        timesMopped = 0;
    }
}
