using System;
using Godot;

[GlobalClass]
public partial class IntStatEntry : Resource
{
    [Export] public IntStatTypes Key { get; set; }
    [Export] public int Value { get; set; }
}