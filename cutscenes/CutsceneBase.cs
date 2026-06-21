using Godot;
using System;
using System.Collections.Generic;
public partial class CutsceneBase : Node2D
{
	protected AnimatedSprite2D animation;
	private TalkBubble talkBubble;
	private bool done;
	private int index=0;
	private List<string> CutsceneAnim;
	private bool started=false;
	private List<List<TalkBubblePage>> CutsceneTalk;
	// Called when the node enters the scene tree for the first time.
	
	public void PlayCurrentIndex()
	{
		animation.Play(CutsceneAnim[index]);
		talkBubble.NewText(CutsceneTalk[index]);
	}
	public virtual void OnEnd()
	{
		GetTree().ChangeSceneToFile("res://main/main_scene.tscn");
	}
	public override void _Ready()
	{
		index=0;
		done=false;
		animation=GetNode<AnimatedSprite2D>("CutsceneAnim");
		talkBubble=GetNode<TalkBubble>("TalkBubble");
		AddToGroup("canTalkNPC");
		CutsceneAnim=new();
		CutsceneTalk=new();
		FillUpLists();
	}
	public virtual void FillUpLists()
	{
		CutsceneAnim.Add("first");
		CutsceneTalk.Add(
			new List<TalkBubblePage> 
			{
				new TalkBubblePage("* This is Goostave.§§§§§\nGoostave is a little Goon.§§§§§§§\nWhat a beautiful creature§§§§§§§§\nPress [Enter] to continue."
				,"",0.05f),
				new TalkBubblePage("* Goostave has a little secret in his\nlittle heart§§§§§§§§§\nHe's madly in love with §§§§§§Lois Griffin.\n§§§§§§§She's the prettiest girl he knows about"
				,"",0.05f)
			}
		);
		CutsceneAnim.Add("second");
		CutsceneTalk.Add(
			new List<TalkBubblePage> 
			{
				new TalkBubblePage("Maaan...§§§§§ She's sooooo §§§§§DREAMY\n§§§§§§§§I love you §§§§Lois Griffin....\n§§§§§§§I hope you feel the same"
				,"Goostave",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("Today... it's finally the day...§§§§\nThe day I confess my BULGING§§§§ LOVE for her"
				,"Goostave",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
			}
		);
		CutsceneAnim.Add("third");
		CutsceneTalk.Add(
			new List<TalkBubblePage> 
			{
				new TalkBubblePage("* Goostave combed his antennas and dusted his belly.\n§§§§§And he stepped out his door"
				,"",0.05f),
			}
		);
		CutsceneAnim.Add("fourth");
		CutsceneTalk.Add(
			new List<TalkBubblePage> 
			{
				new TalkBubblePage("* At Lois Griffin's house *"
				,"",0.05f),
				new TalkBubblePage("umm.. hehe... §§§\nHIIII LOIS GRIFFIN§§§§§§\nWhat a lovely §§day§§, we have"
				,"Goostave",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("I hoped I could find you here§§§§...\n§§§§§I wanted to §§§§§§§§§§§§§§§tell you something"
				,"Goostave",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("Ummmm§§§§....§§§§§ So okay so§§§§§ ... I'll just say it\n§§§§§§§§§I-§§§§I§§§§§§§§§ LOVE YOU SO MUCH LOIS GRIFFIN\nI THINK ABOUT YOU ALL THE TIME I'M OBSESSED WITH YOU"
				,"Goostave",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("Please oooohh please go on a date with me"
				,"Goostave",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
			}
		);
		CutsceneAnim.Add("fifth");
		CutsceneTalk.Add(
			new List<TalkBubblePage> 
			{
				new TalkBubblePage("§§§§§§§§§§ummm.....§§§§§ I...§§§§\nI don't even know where to start...."
				,"Lois Griffin",0.06f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("It's not just that I don't find you attractive\n§§§§§§THE FACT §§§§§§..THE AUDACITY you have to THINK\nI would say yes to that.....§§§§ \nEEeeeewww"
				,"Lois Griffin",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("You are soooo §§BALD, §§§§§§§FAT,§§§§§§ WEAK§§§§§§, AND UNCOOL\nYou look like you are breathing manually\n§§and like§§§§ you could just die any second\n§§§§§§§§§AND YOU HAVE THAT WEIRD FIXATION ON TRAINS LIKE EW"
				,"Lois Griffin",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("AND YOU HAVE NO NIPPLES§§§§, like what..\n......just go away..§§§§and stay there..\n§§§§§I have to take a shower after looking at you. §§§§§§Ew."
				,"Lois Griffin",0.04f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),

			}
		);
		CutsceneAnim.Add("sixth");
		CutsceneTalk.Add(
			new List<TalkBubblePage> 
			{
				new TalkBubblePage("MAN§§§§§§... Life's SO UNFAIR§§§§§§\nWhat a STUPID and §§§§DUMB idea.....\n§§§§Like she will ever want to be with me"
				,"Goostave",0.06f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("I should be more like you, §§§§Toothpaste Man....§§§§§§\nOooh those STRONG BONES\n§§§§§LUSH HAIR\n§§§§§§§§§OOHHH HOW I YEARN FOR THOSE NIPPLES"
				,"Goostave",0.06f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("Ewww dude what are you even talking about"
				,"Toothpaste Man",0.06f,"res://assets/sounds/talksounds/sound_talk_man.wav"),
				new TalkBubblePage("I mean like in a manly way\n§§§§§Like how much i want those nipples\nLike on my body"
				,"Goostave",0.03f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("Nevermind I'll just go away\n§§§§Stupid Toothpaste man§§§ :("
				,"Goostave",0.06f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
			}
		);
		CutsceneAnim.Add("seventh");
		CutsceneTalk.Add(
			new List<TalkBubblePage> 
			{
				new TalkBubblePage("* On his sad little walk home,§§§§§§\nGoostave found a Cool Bone on the ground"
				,"",0.05f),
				new TalkBubblePage("Oh WOW!§§§§ What a shiny shiny Bone\n§§§§§How I wish this was in my BODY"
				,"Goostave",0.05f,"res://assets/sounds/talksounds/sound_talk_goon.wav"),
				new TalkBubblePage("* And in that moment, §§§§§Goostave had an idea"
				,"",0.05f)
			}
		);
	}
	public void StopTalking()
	{
		index++;
		if (index<CutsceneTalk.Count)
		{
			PlayCurrentIndex();
		}
		else
		{
			OnEnd();
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!started)
		{
			PlayCurrentIndex();
		}
		started=true;
	}
}
