﻿using Content.Shared.Actions;
using Robust.Shared.Audio;
using Content.Server.Magic.Events;

namespace Content.Server._Pirate.Mage.Events;

public sealed partial class MageKnockSpellEvent: InstantActionEvent, ISpeakSpell
{
    /// <summary>
    /// The range this spell opens doors in
    /// 4f is the default
    /// </summary>
    [DataField("range")]
    public float Range = 4f;

    [DataField("knockSound")]
    public SoundSpecifier KnockSound = new SoundPathSpecifier("/Audio/Magic/knock.ogg");

    /// <summary>
    /// Volume control for the spell.
    /// </summary>
    [DataField("knockVolume")]
    public float KnockVolume = 5f;

    [DataField("speech")]
    public string? Speech { get; set;}

    /// <summary>
    /// How much mana should be drained.
    /// </summary>
    [DataField("manaCost")]
    public float ManaCost = 10f;

}
