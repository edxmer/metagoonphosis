using Godot;
using System;

[GlobalClass]
public partial class OrganSlot : Resource
{
    [Export] public Organ Organ { get; set; }

    public OrganSlot() {}

    public OrganSlot(Organ organ)
    {
        Organ = organ;
    }
}
