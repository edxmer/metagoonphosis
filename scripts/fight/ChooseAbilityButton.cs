using Godot;
using System;

public partial class ChooseAbilityButton : Button
{
	private Ability _ability { get; set; }
	public FightManager _fightManager { get; set; }

	public void Initialize(Ability ability, FightManager fightManager)
	{
		_ability = ability;
		_fightManager = fightManager;

		Text = _ability.ToString();
	}

    public override void _Ready()
    {
        Pressed += OnPress;
    }

	public void OnPress()
	{
		_fightManager.EmitSignal(FightManager.SignalName.PlayerAbilitySelected, _ability);
	}
}
