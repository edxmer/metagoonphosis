using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class EnemyData : Resource
{
    [Export] public string EnemyName { get; set; }
    
    [Export] public Texture2D EnemyTexture { get; set; }
    [Export] public Stats EnemyStats { get; set; }
    [Export] public Array<Ability> Abilities { get; set; }
}