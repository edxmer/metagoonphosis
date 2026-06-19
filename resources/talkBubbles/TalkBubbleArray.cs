using Godot;
using System;

[GlobalClass]
public partial class TalkBubbleArray : Resource
{
	[Export] public TalkBubblePage[] myText;
	public TalkBubbleArray(){}
	public TalkBubbleArray(TalkBubblePage[] text)
	{
		myText=text;
	}
}
