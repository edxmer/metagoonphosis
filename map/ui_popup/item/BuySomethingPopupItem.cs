using Godot;
using System;

public partial class BuySomethingPopupItem : BuySomethingPopup
{
	public string rewardItem="";
	public int rewardAmount=1;
	// Called when the node enters the scene tree for the first time.
	public override void _Process(double delta)
	{
		base._Process(delta);
		myText=rewardItem+" "+rewardAmount.ToString()+"\n";
	}
	public override void AddReward()
	{
		PlayerStats.Instance.PlayerInventory.AddItem(rewardItem,rewardAmount);
	}
}
