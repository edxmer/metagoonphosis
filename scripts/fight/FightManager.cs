using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[GlobalClass]
public partial class FightManager : Node
{
	[Signal]
	public delegate void PlayerAbilitySelectedEventHandler(Ability ability);

	// TODO: Temporary, later this should be set by calling the InitiateFight method
	[Export] public EnemyData EnemyData { get; set; }

	[Export] public Sprite2D EnemySprite { get; set; }
	[Export] public Label EnemyLabel { get; set; }

	[Export] public ProgressBar PlayerHealthBar { get; set; }
	[Export] public ProgressBar EnemyHealthBar { get; set; }

	[Export] public Marker2D PlayerAttackOrigin { get; set; }
	[Export] public Marker2D EnemyAttackOrigin { get; set; }

	private FightMemberState _playerState;
	private FightMemberState _enemyState;

	public override void _Ready()
	{
		InitiateFight(EnemyData); //TODO: Temporary, Would be called from an outside script
	}

	public void InitiateFight(EnemyData enemyData)
	{
		_playerState = new(
			PlayerStats.Instance.Stats, 
			PlayerStats.Instance.Cavity.GetAbilities().Select(x => (Ability)x.Duplicate()).ToList(), 
			PlayerAttackOrigin
			);

		_enemyState = new(
			enemyData.EnemyStats, 
			enemyData.Abilities.ToList().Select(x => (Ability)x.Duplicate()).ToList(),
			EnemyAttackOrigin
			);

		EnemyLabel.Text = enemyData.EnemyName;

		PlayerHealthBar.MaxValue = _playerState.Health; // By default health is max
		EnemyHealthBar.MaxValue = _enemyState.Health;

		EnemySprite.Texture = enemyData.EnemyTexture;

		PlayerTurn();
	}

	public async void PlayerTurn()
	{
		_playerState.TickCooldowns();

		List<Ability> availableAbilities = _playerState.GetAvailableAbilities();

		Node abilityUI = ShowChooseAbilityUI(availableAbilities);

		var signalArgs = await ToSignal(this, SignalName.PlayerAbilitySelected);
		Ability chosenAbility = (Ability)signalArgs[0];
		
		abilityUI.QueueFree();

		chosenAbility.CastAbility(_playerState, _enemyState);

		await ToSignal(GetTree().CreateTimer(chosenAbility.AnimationLength), SceneTreeTimer.SignalName.Timeout);

		UpdateUI();

		if (_enemyState.DidLose()) EndFight(true);
		else EnemyTurn();
	}

	public async void EnemyTurn()
	{
		_enemyState.TickCooldowns();

		List<Ability> availableAbilities = _enemyState.GetAvailableAbilities();

		if (availableAbilities.Count > 0)
		{
			Random rand = new();
			Ability chosenAbility = availableAbilities[rand.Next(availableAbilities.Count)];

			chosenAbility.CastAbility(_enemyState, _playerState);

			await ToSignal(GetTree().CreateTimer(chosenAbility.AnimationLength), SceneTreeTimer.SignalName.Timeout);
		}
		else
		{
			GD.Print("Unable to choose ability for enemy.");
			await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		}

		if (_playerState.DidLose()) EndFight(false);
		else EnemyTurn();
	}

	public void UpdateUI()
	{
		PlayerHealthBar.Value = _playerState.Health;
		EnemyHealthBar.Value = _enemyState.Health;
	}

	public void EndFight(bool playerWon)
	{
		
	}

	public Node ShowChooseAbilityUI(List<Ability> abilities)
	{
		throw new NotImplementedException();
	}
}
