using Robust.Shared.Configuration;

namespace Content.Shared.NF14.CCVar;

[CVarDefs]
public sealed class NF14CVars
{
    /// <summary>
    /// Whether or not respawning is enabled.
    /// </summary>
    public static readonly CVarDef<bool> RespawnEnabled =
        CVarDef.Create("nf14.respawn.enabled", true, CVar.SERVER | CVar.REPLICATED);

    /// <summary>
    /// Respawn time, how long the player has to wait in seconds after death.
    /// </summary>
    public static readonly CVarDef<float> RespawnTime =
        CVarDef.Create("nf14.respawn.time", 300.0f, CVar.SERVER | CVar.REPLICATED);

    /*
     * End of round stats
     */
    #region EndOfRoundStats
    #region BloodLost
    /// <summary>
    ///     The amount of blood lost required to trigger the BloodLost end of round stat.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the BloodLost end of round stat.
    /// </remarks>
    public static readonly CVarDef<float> BloodLostThreshold =
        CVarDef.Create("eorstats.bloodlost_threshold", 300f, CVar.SERVERONLY);
    #endregion BloodLost

    #region CuffedTime
    /// <summary>
    ///     The amount of time required to trigger the CuffedTime end of round stat, in minutes.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the CuffedTime end of round stat.
    /// </remarks>
    public static readonly CVarDef<int> CuffedTimeThreshold =
        CVarDef.Create("eorstats.cuffedtime_threshold", 5, CVar.SERVERONLY);
    #endregion CuffedTime

    #region EmitSound
    /// <summary>
    ///     The amount of sounds required to trigger the EmitSound end of round stat.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the EmitSound end of round stat.
    /// </remarks>
    public static readonly CVarDef<int> EmitSoundThreshold =
        CVarDef.Create("eorstats.emitsound_threshold", 10, CVar.SERVERONLY);
    #endregion EmitSound

    #region InstrumentPlayed
    /// <summary>
    ///     The amount of instruments required to trigger the InstrumentPlayed end of round stat, in minutes.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the InstrumentPlayed end of round stat.
    /// </remarks>
    public static readonly CVarDef<int> InstrumentPlayedThreshold =
        CVarDef.Create("eorstats.instrumentplayed_threshold", 5, CVar.SERVERONLY);
    #endregion InstrumentPlayed

    #region MopUsed
    /// <summary>
    ///     The amount of liquid mopped required to trigger the MopUsed end of round stat.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the MopUsed end of round stat.
    /// </remarks>
    public static readonly CVarDef<int> MopUsedThreshold =
        CVarDef.Create("eorstats.mopused_threshold", 100, CVar.SERVERONLY);

    /// <summary>
    ///     Should a stat be displayed specifically when no mopping was done?
    /// </summary>
    public static readonly CVarDef<bool> MopUsedDisplayNone =
        CVarDef.Create("eorstats.mopused_displaynone", true, CVar.SERVERONLY);

    /// <summary>
    ///     The amount of top moppers to show in the end of round stats.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the top moppers.
    /// </remarks>
    public static readonly CVarDef<int> MopUsedTopMopperCount =
        CVarDef.Create("eorstats.mopused_topmoppercount", 3, CVar.SERVERONLY);
    #endregion MopUsed

    #region ShotsFired
    /// <summary>
    ///     The amount of shots fired required to trigger the ShotsFired end of round stat.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the ShotsFired end of round stat.
    /// </remarks>
    public static readonly CVarDef<int> ShotsFiredThreshold =
        CVarDef.Create("eorstats.shotsfired_threshold", 20, CVar.SERVERONLY);

    /// <summary>
    ///     Should a stat be displayed specifically when no shots were fired?
    /// </summary>
    public static readonly CVarDef<bool> ShotsFiredDisplayNone =
        CVarDef.Create("eorstats.shotsfired_displaynone", true, CVar.SERVERONLY);
    #endregion ShotsFired

    #region SlippedCount
    /// <summary>
    ///     The amount of times slipped required to trigger the SlippedCount end of round stat.
    /// </summary>
    /// <remarks>
    ///     Setting this to 0 will disable the SlippedCount end of round stat.
    /// </remarks>
    public static readonly CVarDef<int> SlippedCountThreshold =
        CVarDef.Create("eorstats.slippedcount_threshold", 5, CVar.SERVERONLY);

    /// <summary>
    ///     Should a stat be displayed specifically when nobody was done?
    /// </summary>
    public static readonly CVarDef<bool> SlippedCountDisplayNone =
        CVarDef.Create("eorstats.slippedcount_displaynone", true, CVar.SERVERONLY);

    /// <summary>
    ///     Should the top slipper be displayed in the end of round stats?
    /// </summary>
    public static readonly CVarDef<bool> SlippedCountTopSlipper =
        CVarDef.Create("eorstats.slippedcount_topslipper", true, CVar.SERVERONLY);
    #endregion SlippedCount
    #endregion EndOfRoundStats

    /// <summary>
    /// Whether or not returning from cryosleep is enabled.
    /// </summary>
    public static readonly CVarDef<bool> CryoReturnEnabled =
        CVarDef.Create("nf14.uncryo.enabled", true, CVar.SERVER | CVar.REPLICATED);

    /// <summary>
    /// The time in seconds after which a cryosleeping body is considered expired and can be deleted from the storage map.
    /// </summary>
    public static readonly CVarDef<float> CryoExpirationTime =
        CVarDef.Create("nf14.uncryo.maxtime", 60 * 60f, CVar.SERVER | CVar.REPLICATED);
}
