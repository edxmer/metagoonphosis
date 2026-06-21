using Godot;
using System;
[GlobalClass]
public partial class TalkBubblePage : Resource
{
	[Export (PropertyHint.MultilineText)]public string myText;
	[Export]public string myName;
	[Export]public double myTalkSpeed;
	[Export (PropertyHint.File, "*.wav,*.ogg,*.mp3")]public string audioPath;
	public TalkBubblePage() { }
	public TalkBubblePage(string myText,string myName,double myTalkSpeed,string audioPath)
	{
		this.myText=myText;
		this.myName=myName;
		this.myTalkSpeed=myTalkSpeed;
		this.audioPath=audioPath;
	}
	public TalkBubblePage(string myText,string myName,double myTalkSpeed)
	{
		this.myText=myText;
		this.myName=myName;
		this.myTalkSpeed=myTalkSpeed;
		this.audioPath="res://assets/sounds/talksounds/sound_talk_sign.wav";
	}
}
