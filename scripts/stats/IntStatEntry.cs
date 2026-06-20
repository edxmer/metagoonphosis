using System;
using Godot;

[GlobalClass]
public partial class IntStatEntry : Resource
{
    [Export] public IntStatType Key { get; set; }
    [Export] public int Value { get; set; }
}