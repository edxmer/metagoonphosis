using Godot;
using System;

public partial class TurtleWife : NpcTalkableThrowaway
{
	private int talkedCounter;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		talkedCounter=0;
	}
	protected override void AfterTalkedEvent()
	{
		talkedCounter++;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public override void ActionOnInteract()
	{
		TalkBubblePage[] current_text;
		if (talkedCounter==0)
		
		{
			current_text=[
				new TalkBubblePage(
					"OOOOH HELLO\nHI HELLO LITTLE GOON\n§§§§§§§I'm the §§§§TURTLE's §§§§§§WIFE\n§§huhuhu",
					"Sexy Turtle Woman",0.04,"res://assets/sounds/talksounds/sound_talk_woman_2.wav"),
				new TalkBubblePage(
					"You might think the §§§EVIL §§§§FLY KING keeps me hostage\nAnd that's why §§§MY HUSBAND§§§§ wants to exterminate\n§§§§ALL THE FLIES\n§§§§§§§§§BUT THAT'S ACTUALLY VERY WRONG §§§huuhuhu",
					"Sexy Turtle Woman",0.04,"res://assets/sounds/talksounds/sound_talk_woman_2.wav"),
				new TalkBubblePage(
					"I am actually here in the §§§§§\nSWAMP KINGDOM OF THE EVIL FLIES\nBecause I'm §§§§§§§CHEATING ON MY §§§HUSBAND\nWith the §§§§§§KING OF ALL FLIES§§§ huhuhu",
					"Sexy Turtle Woman",0.04,"res://assets/sounds/talksounds/sound_talk_woman_2.wav"),
				new TalkBubblePage(
					"Because HE, §§§§§the §§§§KING OF FLIES\nhas a §§§§SEXY LITTLE CROWN§§§§§§\nhuhuhu",
					"Sexy Turtle Woman",0.04,"res://assets/sounds/talksounds/sound_talk_woman_2.wav")
			];
		}
		else
		{
			current_text=[
				new TalkBubblePage(
					"YUMMY SEXY FLY KING huhuhu",
					"Sexy Turtle Woman",0.04,"res://assets/sounds/talksounds/sound_talk_woman_2.wav")
			];
		}
		
		
		myCurrentLines=new TalkBubbleArray(current_text);
		SaySomething();
	}
}
