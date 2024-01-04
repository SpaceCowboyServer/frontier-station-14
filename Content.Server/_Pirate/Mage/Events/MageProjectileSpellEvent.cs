using Content.Shared.Actions;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;
using Content.Server.Magic.Events;

namespace Content.Server._Pirate.Mage.Events;

public sealed partial class MageProjectileSpellEvent : WorldTargetActionEvent, ISpeakSpell
{
    /// <summary>
    /// What entity should be spawned.
    /// </summary>
    [DataField("prototype", required: true, customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string Prototype = default!;

    /// <summary>
    /// Gets the targeted spawn positions; may lead to multiple entities being spawned.
    /// </summary>
    [DataField("posData")] public MagicSpawnData Pos = new TargetCasterPos();

    [DataField("speech")]
    public string? Speech { get; set;}

    /// <summary>
    /// How much mana should be drained.
    /// </summary>
    [DataField("manaCost")]
    public float ManaCost = 30f;

}
