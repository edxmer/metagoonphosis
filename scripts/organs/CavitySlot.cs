using Godot;
using System;

[GlobalClass]
public partial class CavitySlot : Resource
{
    [Export] public Organ Organ { get; set; }

    public CavitySlot() {}

    public CavitySlot(Organ organ)
    {
        Organ = organ;
    }
}
