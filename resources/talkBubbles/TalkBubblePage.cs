using Godot;
using System;
[GlobalClass]
public partial class TalkBubblePage : Resource
{
	[Export (PropertyHint.MultilineText)]public string myText;
	[Export]public string myName;
	[Export]public double myTalkSpeed;
	public TalkBubblePage() { }
	public TalkBubblePage(string myText,string myName,double myTalkSpeed)
	{
		this.myText=myText;
		this.myName=myName;
		this.myTalkSpeed=myTalkSpeed;
	}
}
