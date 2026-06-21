using Godot;
using System;
using System.Security.Cryptography;

[GlobalClass]
public partial class AttackAbility : Ability
{
    [Export] public int BaseAttackDamage = 0;
    [Export] public Texture2D AttackTexture { get; set; }

    public override void CastAbility(FightMemberState from, FightMemberState to)
    {
        base.CastAbility(from, to);

        // Cast ability effect
        AttackSprite2D effect = new(from.AbilityOrigin.GlobalPosition, to.AbilityOrigin.GlobalPosition, AnimationLength, AttackTexture);

        PlayerStats.Instance.GetTree().CurrentScene.AddChild(effect);

        to.TakeDamage(BaseAttackDamage + from.Stats.GetIntStat(IntStatType.AttackDamage));
    }

    public override string ToString()
    {
        // This will only matter for the player, so we can use the player stats without worry

        string attackDamage = $"{BaseAttackDamage} + {PlayerStats.Instance.Stats.GetIntStat(IntStatType.AttackDamage)}";

        return $"{AbilityName}\n\n* {AbilityDescription} *\n\nCooldown: {DefaultCooldown} | Attack Damage: {attackDamage}";
    }
}