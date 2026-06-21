using Godot;
using System;

public partial class StatsListLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerStats.Instance.Cavity.OrgansChanged += UpdateStats;
		UpdateStats();
	}

	public void UpdateStats()
	{
		Stats stats = PlayerStats.Instance.Stats;

		Text = stats.GetNonZeroStatsStringNormalStyle();
	}
}
