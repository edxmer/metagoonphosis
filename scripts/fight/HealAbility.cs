using Godot;
using System;

public partial class HealAbility : Ability
{
    [Export] public int BaseHealFactor = 0;

    public override void CastAbility(FightMemberState from, FightMemberState to)
    {
        base.CastAbility(from, to);

        from.Heal(BaseHealFactor + from.Stats.GetIntStat(IntStatType.Catness));
    }

    public override string ToString()
    {
        // This will only matter for the player, so we can use the player stats without worry

        string healAmount = $"{BaseHealFactor} + {PlayerStats.Instance.Stats.GetIntStat(IntStatType.Catness)}";

        return $"{AbilityName}\n\n* {AbilityDescription} *\n\nCooldown: {DefaultCooldown} | Heal amount: {healAmount}";
    }
}