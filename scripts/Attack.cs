using Godot;
using System;


/// <summary>
/// A node used to create attack types that the creature can use.
/// </summary>
[GlobalClass]
public partial class Attack : Resource
{
    [Export]
    public string AttackName { get; set; } = "Attack Name";
    [Export]
    public int BaseAttackDamage { get; set; } = 0;
    [Export]
    public int TotalCooldown { get; set; } = 0;

    //[Export]
    //public AttackEffect Effect { get; set; }

    public int RealAttackDamage { get; set; } = 0;
    public int CurrentCooldown { get; set; } = 0;

    public void Apply(Creature attacker, Creature attackee)
    {
        // attackee sounds so stupid
        
        // TODO: add these methods to Creature
        //attackee.TakeDamage(attacker.GetBaseAttackDamage());

        // TODO: attack effect   
    }
}
