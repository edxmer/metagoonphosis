using Godot;
using System;

[GlobalClass]
public partial class Ability : Resource
{
    [Export] public string AbilityName { get; set; }
    [Export] public string AbilityDescription { get; set; }

    [Export] public int DefaultCooldown { get; set; } = 1;

    [Export] public double AnimationLength { get; set; } = 1.0;

    public int Cooldown { get; private set; } = 0;


    public virtual void CastAbility(FightMemberState from, FightMemberState to)
    {
        Cooldown = DefaultCooldown;
    }

    public void ResetCooldown()
    {
        Cooldown = 0;
    }

    public void TickCooldown()
    {
        Cooldown = Math.Max(0, Cooldown - 1);
    }

    public override string ToString()
    {
        return $"Cast Ability: {AbilityName}\n* {AbilityDescription} *\nCooldown: {DefaultCooldown}";
    }
}