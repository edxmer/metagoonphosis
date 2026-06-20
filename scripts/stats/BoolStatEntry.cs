using System;
using Godot;

public partial class BoolStatEntry : Resource
{
    [Export] public BoolStatTypes Key { get; set; }
    [Export] public bool Value { get; set; }
}