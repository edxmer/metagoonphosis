using Godot;
using System;

[GlobalClass]
/// <summary>
/// A node used to create attack types that the creature can use.
/// </summary>
public partial class Attack : Node
{
    [Export]
    public string AttackName { get; set; } = "Attack Name";
    [Export]
    public int BaseAttackDamage { get; set; } = 0;
    [Export]
    public int TotalCooldown { get; set; } = 0;

    //[Export]
    //public PackedScene AttackEffect { get; set; }

    public int RealAttackDamage { get; set; } = 0;
    public int CurrentCooldown { get; set; } = 0;

    public override void _Ready()
    {
        // TODO
        // Check if proper child nodes exist, throw error otherwise.
    }

    public void Apply(Creature attacker, Creature attackee)
    {
        // attackee sounds so stupid
        attackee.TakeDamage(attacker.GetBaseAttackDamage());

        // TODO: attack effect   
    }
}
