using Content.Server._Park.Species.Shadowkin.Components;
using Content.Shared.Inventory.Events;

namespace Content.Server._Park.Species.Shadowkin.Systems;

public sealed partial class ShadowkinSightSystem : EntitySystem
{
    [Dependency] private readonly ShadowkinDarkSwapSystem _darkSwap = default!;


    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ShadowkinSightComponent, GotEquippedEvent>(OnEquipped);
        SubscribeLocalEvent<ShadowkinSightComponent, GotUnequippedEvent>(OnUnEquipped);
    }


    private void OnEquipped(EntityUid uid, ShadowkinSightComponent component, GotEquippedEvent args)
    {
        _darkSwap.SetVisibility(args.Equipee, true, false, false);
    }

    private void OnUnEquipped(EntityUid uid, ShadowkinSightComponent component, GotUnequippedEvent args)
    {
        _darkSwap.SetVisibility(args.Equipee, false, false, false);
    }
}
