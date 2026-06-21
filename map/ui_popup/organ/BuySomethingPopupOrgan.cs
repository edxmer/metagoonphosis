using Godot;
using System;

public partial class BuySomethingPopupOrgan : BuySomethingPopup
{
	public Organ? givenOrgan;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		givenOrgan=null;
	}
	public override void AddReward()
	{
		if (givenOrgan==null)
		{
			Kill();
			return;
		}
		PlayerStats.Instance.GiveOrgan(givenOrgan);
	}
}
