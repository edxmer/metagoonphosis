using System;
using Godot;

public partial class IntStatEntry : Resource
{
    [Export] public IntStatTypes Key { get; set; }
    [Export] public int Value { get; set; }
}