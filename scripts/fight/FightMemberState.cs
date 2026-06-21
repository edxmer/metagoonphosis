using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class FightMemberState : Resource
{
    public Stats Stats { get; private set; }
    public List<Ability> Abilities { get; private set; }
    public Marker2D AbilityOrigin { get; private set; }

    public int Health { get; set; }
    public int MaxHealth { get; set; }

    public FightMemberState() {}
    
    public FightMemberState(Stats stats, List<Ability> abilities, Marker2D abilityOrigin)
    {
        Stats = stats;
        Abilities = abilities;
        AbilityOrigin = abilityOrigin;

        MaxHealth = Stats.GetIntStat(IntStatType.Hp);
        Health = MaxHealth;
    }

    public bool DidLose()
    {
        return Health <= 0;
    }

    public List<Ability> GetAvailableAbilities() => Abilities.Where(x => x.Cooldown == 0).ToList();

    public void TickCooldowns()
    {
        foreach (var ability in Abilities)
		{
			ability.TickCooldown();
		}
    }

    public void TakeDamage(int damage)
    {
        Health -= Math.Max(0, damage - Stats.GetIntStat(IntStatType.Defense));
    }

    public void Heal(int amount)
    {
        Health = Math.Min(Health + amount, MaxHealth);
    }
}