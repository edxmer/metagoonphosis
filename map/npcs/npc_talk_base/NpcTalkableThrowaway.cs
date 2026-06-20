using Godot;
using System;
using System.Collections.Generic;
public partial class NpcTalkableThrowaway : Interactable
{
	// Called when the node enters the scene tree for the first time.
	public TalkBubble myTalker;
	public string currentIdleAnimation="idle";
	public string currentTalkAnimation="talk";
	public bool isSpeaking=false;
	[Export] public TalkBubbleArray myCurrentLines;
	public bool SaySomething()
	{
		if (myTalker.amITalking || isSpeaking)
		{
			return false;
		}
		else
		{
			animation.Play(currentTalkAnimation);
			isSpeaking=true;
			myTalker.NewText(new List<TalkBubblePage>(myCurrentLines.myText));
			return true;
		}
	}
	public virtual void StopTalking()
	{
		if (isSpeaking)
		{
			isSpeaking=false;
			animation.Play(currentIdleAnimation);
		}
	}
	
	public override void _Ready()
	{
		base._Ready();
		AddToGroup("canTalkNPC");
		myTalker=GetTree().GetFirstNodeInGroup("talkBubble") as TalkBubble;
	}
	public override void ActionOnInteract()
	{
		SaySomething();
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
}
