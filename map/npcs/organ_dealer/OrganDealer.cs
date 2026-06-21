using Godot;
using System;

public partial class OrganDealer : NpcTalkableThrowaway
{
	private bool amICaught=false;
	protected override void AfterTalkedEvent()
	{
		if (amICaught)
		{
			return;
		}
		amICaught=true;
		currentIdleAnimation="disappear";
		currentTalkAnimation="disappear";
		animation.Play(currentTalkAnimation);
		Visible=false;
		PlayerStats.Instance.PlayerInventory.AddItem("Fly");
	}
	public override void ActionOnInteract()
	{
		if (amICaught)
		{
			return;
		}
		TalkBubblePage[] current_text={new TalkBubblePage(
			"Wait please don't catch me\n§§§§§§§§Wait an hour i was learning karate online§§§§§§§§§\nI want to fight back"
			,"Weak Little Fly",0.05,"res://assets/sounds/talksounds/sound_talk_chirp.wav"),
			new TalkBubblePage(
			"I'm a quick learner...§§§§Just wait a little\n§§§§I already know how to count up to five\nin Chinese\n§§§§§§§§1, 2, 3, 4, 5§§§§§§§§§§"
			,"Weak Little Fly",0.06,"res://assets/sounds/talksounds/sound_talk_chirp.wav"),
			new TalkBubblePage(
			"* A Fly was added to your Inventory"
			,"",0.08,"res://assets/sounds/talksounds/sound_talk_sign.wav"),
			new TalkBubblePage(
			"Wow okay...§§§§§§§ I was getting too strong anyway"
			,"Weak Little Fly",0.06,"res://assets/sounds/talksounds/sound_talk_chirp.wav"),
			
			};
		myCurrentLines=new TalkBubbleArray(current_text);

		SaySomething();
	}
}
