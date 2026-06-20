using Godot;
using System;

public partial class NpcTest1 : NpcTalkableThrowaway
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public override void ActionOnInteract()
	{
		TalkBubblePage[] current_text={new TalkBubblePage("What do you want?§§§§\nDon't you see im busy§§§§§§§§§\nI'm too busy thinking.","Lajos",0.2)};
		myCurrentLines=new TalkBubbleArray(current_text);
		SaySomething();
	}
}
