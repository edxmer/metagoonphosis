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

	[Export] public Control ButtonsContainer { get; set; }
	[Export] public Control ChooseAbilityButtonsParent { get; set; }
	[Export] public PackedScene ChooseAbilityButton { get; set; }

	private FightMemberState _playerState;
	private FightMemberState _enemyState;

	public override void _Ready()
	{
		InitalizeFight(EnemyData); //TODO: Temporary, Would be called from an outside script
	}

	public void InitalizeFight(EnemyData enemyData)
	{

		ButtonsContainer.Hide();

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

	public async Task UpdateUI()
	{
		PlayerHealthBar.Value = _playerState.Health;
		EnemyHealthBar.Value = _enemyState.Health;

		await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
	}

	private async void PlayerTurn()
	{
		_playerState.TickCooldowns();

		List<Ability> availableAbilities = _playerState.GetAvailableAbilities();

		ShowChooseAbilityUI(availableAbilities);

		var signalArgs = await ToSignal(this, SignalName.PlayerAbilitySelected);
		Ability chosenAbility = (Ability)signalArgs[0];
		
		FreeChooseAbilityUI();

		chosenAbility.CastAbility(_playerState, _enemyState);

		await ToSignal(GetTree().CreateTimer(chosenAbility.AnimationLength), SceneTreeTimer.SignalName.Timeout);

		await UpdateUI();

		if (_enemyState.DidLose()) EndFight(true);
		else EnemyTurn();
	}

    private async void EnemyTurn()
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

		await UpdateUI();

		if (_playerState.DidLose()) EndFight(false);
		else PlayerTurn();
	}

	private void EndFight(bool playerWon)
	{
		throw new NotImplementedException();
	}

	private void ShowChooseAbilityUI(List<Ability> abilities)
	{
		ButtonsContainer.Show();

		foreach (Ability ability in abilities)
		{
			ChooseAbilityButton button = ChooseAbilityButton.Instantiate<ChooseAbilityButton>();

			button.Initialize(ability, this);

			ChooseAbilityButtonsParent.AddChild(button);
		}
	}

	private void FreeChooseAbilityUI()
    {	
        foreach(Node node in ChooseAbilityButtonsParent.GetChildren())
		{
			if (node is ChooseAbilityButton) node.QueueFree();
		}

		ButtonsContainer.Hide();
    }

	private async void FightStartAnimation()
	{
		
	}
}
