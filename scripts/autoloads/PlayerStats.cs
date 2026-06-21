using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStats : Node
{
	public static PlayerStats Instance { get; private set; }

	public Stats Stats { get; private set; } = new(); // By default every stat is zero, but the organs change the stats
	public Cavity Cavity { get; private set; }
	public UnusedOrgans UnusedOrgans { get; private set; } = new();
	public PlayerInventory PlayerInventory { get;set; } = new();

	public bool PlayerWonLastFight { get; set; }

	public bool IsSomethingOpenInMap { get; set; } = false;

	private PackedScene _fightManagerPrefab;

	public override void _Ready()
	{
		// Set singleton instance
		Instance = this;

		// Load chest cavity from resource file
		Cavity = GD.Load<Cavity>("res://resources/cavity/player_cavity.tres");

		_fightManagerPrefab = GD.Load<PackedScene>("res://scenes/fight/fight_manager.tscn");

		// Initializing player starting organs
		List<StartingOrganConfig> startingOrgans = [
				GD.Load<StartingOrganConfig>("res://resources/payer_starting_organs/starting_heart.tres"),
				GD.Load<StartingOrganConfig>("res://resources/payer_starting_organs/starting_lung.tres"),
		];
		foreach (var organConfig in startingOrgans)
		{
			bool success = Cavity.TryPlaceOrgan(organConfig.Organ, organConfig.Origin);
			if (!success) GD.PushWarning($"Could not place starting organ into cavity: {organConfig.Organ.OrganName}");
		}

		// Add event listener to Organs Changed event to update stats
		Cavity.OrgansChanged += UpdateStats;
		
		Cavity.EmitSignal(Cavity.SignalName.OrgansChanged);
	}

	public void UpdateStats()
	{
		Stats = new();

		foreach (var organ in Cavity.GetOrgans())
		{
			//GD.Print($"Stat increased of organ '{organ.OrganName}'{organ.StatIncreases}");
			Stats += organ.StatIncreases;
		}
	}

	public void GiveOrgan(Organ organ)
	{
		UnusedOrgans.Add(new OrganSlot(organ));
	}

	public void StartFight(EnemyData enemyData)
	{
		GetTree().Paused = true;

		FightManager fightManager = _fightManagerPrefab.Instantiate<FightManager>();

		GetTree().Root.AddChild(fightManager);

		fightManager.FightEnded += OnFightEnded;

		fightManager.InitalizeFight(enemyData);
	}

	private void OnFightEnded(bool result)
	{
		GetTree().Paused = false;
		PlayerWonLastFight = result;
	}
}
