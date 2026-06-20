using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStats : Node
{
	public static PlayerStats Instance { get; private set; }
	public Stats Stats { get; set; }
	public ChestCavity ChestCavity { get; set; }
	public PlayerInventory PlayerInventory { get;set; }
	public bool IsSomethingOpenInMap { get; set; }

	public override void _Ready()
	{
		// Set singleton instance
		Instance = this;
		IsSomethingOpenInMap=false;
		// By default every stat is zero, but the organs change the stats
		Stats = new();
		PlayerInventory=new();
		// Load chest cavity from resource file
		ChestCavity = GD.Load<ChestCavity>("res://resources/cavity/player_chest_cavity.tres");

		// Initializing player starting organs
		List<StartingOrganConfig> startingOrgans = [
				GD.Load<StartingOrganConfig>("res://resources/payer_starting_organs/starting_heart.tres"),
		];
		foreach (var organConfig in startingOrgans)
		{
			bool success = ChestCavity.TryPlaceOrgan(organConfig.Organ, organConfig.Origin);
			if (!success) GD.PushWarning($"Could not place starting organ into cavity: {organConfig.Organ.OrganName}");
		}

		// Add event listener to Organs Changed event to update stats
		ChestCavity.OrgansChanged += UpdateStats;

		// Update Stats
		UpdateStats();
	}

	public void UpdateStats()
	{
		Stats = new();

		foreach (var organ in ChestCavity.GetOrgans())
		{
			GD.Print($"Stat increased of organ '{organ.OrganName}'{organ.StatIncreases}");
			Stats += organ.StatIncreases;
		}
	}


}
