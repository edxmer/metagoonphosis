using System;
using Godot;

[GlobalClass]
public partial class BoolStatEntry : Resource
{
    [Export] public BoolStatType Key { get; set; }
    [Export] public bool Value { get; set; }
}