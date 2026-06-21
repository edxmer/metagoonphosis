using Godot;
using System;

public partial class CreatureFlyThree : NpcTalkableThrowaway
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
			"HEY!§§§§§ DON'T TOUCH ME§§§§§§\nI'm under 18§§§§§§§§§\nI mean§§§§ all flies are, i guess we only live for a §§week"
			,"Weak Little Fly",0.06,"res://assets/sounds/talksounds/sound_talk_chirp.wav"),
			new TalkBubblePage(
			"But §§I feel like what you're doing is §§§still§§\npretty problematic\n§§§§§§I haven't even finished watching Love Island\n§§§§§§§I haven't even started it:(("
			,"Weak Little Fly",0.05,"res://assets/sounds/talksounds/sound_talk_chirp.wav"),
			new TalkBubblePage(
			"How will I §§ever know what kind of §§§§whacky adventures\n§§§§Tommy Fury is up to\n:((((("
			,"Weak Little Fly",0.07,"res://assets/sounds/talksounds/sound_talk_chirp.wav"),
			new TalkBubblePage(
			"* A Fly was added to your Inventory"
			,"",0.08,"res://assets/sounds/talksounds/sound_talk_sign.wav")
			
			};
		myCurrentLines=new TalkBubbleArray(current_text);

		SaySomething();
	}
}
