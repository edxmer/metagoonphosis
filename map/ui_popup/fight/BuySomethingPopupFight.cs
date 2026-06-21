using Godot;
using System;
using System.Collections.Generic;
public partial class BuySomethingPopupFight: BuySomethingPopup
{
	public bool FightHappening=false;
	public TalkBubble myTalker;
	public int rewardMoney=0;
	public EnemyData? enemyData;
	public Organ? rewardOrgan;
	public Node2D? parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myTalker=GetTree().GetFirstNodeInGroup("talkBubble") as TalkBubble;
		enemyData=null;
		rewardOrgan=null;
		parent=null;
	}
	public void setMyText()
	{
		myLabel.Text="Attack "+myText;
	}
	protected override void Kill()
	{
		if (!FightHappening)
		{
		QueueFree();
		}
	}
	public void SayRewardMessage()
	{
		myTalker.NewText(new List<TalkBubblePage> {new TalkBubblePage("* You won!\nYou got "+rewardMoney.ToString()+" Dollars\nAnd maybe organs","",0.4)});
	}
	public void SayDeathMessage()
	{
		myTalker.NewText(new List<TalkBubblePage> {new TalkBubblePage("* You got beaten up.","",0.4)});
	}
	public override void AddReward()
	{
		if (enemyData==null)
		{
			Kill();
			return;
		}
		FightHappening=true;
		startFight();
	}
	public async void startFight()
	{
		var signalArgs = await PlayerStats.Instance.StartFight(enemyData);
		bool result = (bool)signalArgs[0];
		FightHappening=false;
		if (result)
		{
			SayRewardMessage();
			parent.Hide();
		}
		else
		{
			SayDeathMessage();
		}
		Kill();
	}
}
