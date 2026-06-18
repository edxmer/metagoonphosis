using Godot;
using System;
using System.Collections.Generic;
public partial class NpcTalkableThrowaway : Interactable
{
	// Called when the node enters the scene tree for the first time.
	public TalkBubble myTalker;
	[Export] TalkBubbleArray myCurrentLines;
	public bool SaySomething()
	{
		if (myTalker.amITalking)
		{
			return false;
		}
		else
		{
			myTalker.NewText(new List<TalkBubblePage>(myCurrentLines.myText));
			return true;
		}
	}
	public override void _Ready()
	{
		base._Ready();
		myTalker=GetTree().GetFirstNodeInGroup("talkBubble") as TalkBubble;
	}
	public override void ActionOnInteract()
	{
		SaySomething();
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
}
