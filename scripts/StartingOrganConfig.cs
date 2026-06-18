using Godot;
using System;
using System.Numerics;

[GlobalClass]
public partial class StartingOrganConfig : Resource
{
    [Export] public Organ Organ;
    [Export] public Vector2I Origin;
}
