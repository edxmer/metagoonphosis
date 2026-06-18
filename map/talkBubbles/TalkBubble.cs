using Godot;
using System;
using System.Collections.Generic;




public partial class TalkBubble : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public bool amITalking {get;private set;}
	private string displayName="Gduy";
	private string displayText="";
	private List<TalkBubblePage> currentPages;
	private string currentTextPage="";
	private double waitBetweenLetters=0.05;
	private double nextLetterTimer=0;
	private Label myNameNode;
	private Label myTextNode;
	
	private void closeUp()
	{
		amITalking=false;
		Visible=false;
		displayName="";
		displayText="";
	}
	
	public void openUp()
	{
		
	}
	
	public void NewText(List<TalkBubblePage> newTexts)
	{
		openUp();
		amITalking=true;
		Visible=true;
		currentPages=newTexts;
		goToNextPage();
	}
	
	private void goToNextPage()
	{
		displayText="";
		if (currentPages.Count==0)
		{
			closeUp();
			return;
		}
		TalkBubblePage nextP=currentPages[0];
		currentPages.RemoveAt(0);
		displayName=nextP.myName;
		currentTextPage=nextP.myText;
		waitBetweenLetters=nextP.myTalkSpeed;
		nextLetterTimer=waitBetweenLetters;
	}
	
	private int currentNextLetterID()
	{
		var len=displayText.Length;
		if (len>=currentTextPage.Length)
		{
			return -1;
		}
		return len;
	}
	private void addLetter()
	{
		var next=currentNextLetterID();
		if (next!=-1)
		{
			var charc=currentTextPage.Substring(next,1);
			if (charc=="§")
			{
				currentTextPage=currentTextPage.Remove(next,1);
			}
			else
			{
				displayText+=charc;
			}
			
		}
	}
	
	private void doTextStep(double delta)
	{
		if (currentTextPage.Length>displayText.Length)
		{
			while(nextLetterTimer<0)
			{
				nextLetterTimer+=waitBetweenLetters;
				addLetter();
			}
			nextLetterTimer-=delta;
			if (Input.IsActionPressed("ui_accept"))
			{
				nextLetterTimer-=delta*4;
			}
		}
		else if (Input.IsActionJustPressed("ui_accept"))
		{
			goToNextPage();
		}
	}
	
	public override void _Ready()
	{
		amITalking=false;
		AddToGroup("talkBubble");
		currentPages=new ();
		Visible = false;
		myNameNode=GetNode<Label>("NameLabel");
		myTextNode=GetNode<Label>("SaidTextLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (amITalking)
		{
			doTextStep(delta);
			myNameNode.Text=displayName;
			myTextNode.Text=displayText;
		}
	}
}
