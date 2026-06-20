using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class PlayerCavityManager : Node
{
    [Export] public Array<StartingOrganConfig> StartingOrgans { get; set; }
    [Export] public ChestCavity Cavity { get; set; }
    [Export] public CavityGridUI CavityGridUI { get; set; }

    public override void _Ready()
    {
        foreach (var organConfig in StartingOrgans)
        {
            bool success = Cavity.TryPlaceOrgan(organConfig.Organ, organConfig.Origin);
            if (!success) GD.PushWarning($"Could not place starting organ into cavity: {organConfig.Organ.OrganName}");
        }

        CavityGridUI.Initialize(Cavity);
    }
}
