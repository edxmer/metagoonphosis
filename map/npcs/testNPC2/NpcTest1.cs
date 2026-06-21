using Godot;
using System;

public partial class NpcTest1 : NpcTalkableThrowaway
{
	private bool AddedStatue;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		AddedStatue=false;
	}
	protected override void AfterTalkedEvent()
	{
		Position=new Vector2(Position.X,Position.Y+10);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public override void ActionOnInteract()
	{
		PopupFactory.CreatePopupItem(this,0,"szex",1);
		TalkBubblePage[] current_text={new TalkBubblePage("What do you want?§§§§\nDon't you see im busy§§§§§§§§§\nI'm too busy thinking.","Lajos",0.2)};
		myCurrentLines=new TalkBubbleArray(current_text);
		SaySomething();
		if (!AddedStatue)
		{
			AddedStatue=true;
			PlayerStats.Instance.PlayerInventory.AddItem("Tomato man Statue");
			PlayerStats.Instance.PlayerInventory.AddItem("Tomato womanStatue");
		}
	}
}
