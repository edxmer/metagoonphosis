using Godot;
using System;
using System.Collections.Generic;




public partial class TalkBubble : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public bool amITalking {get;private set;}
	private const int playSoundAfter=3;
	private string displayName="Gduy";
	private string audioPath="mute";
	private string displayText="";
	private List<TalkBubblePage> currentPages;
	private string currentTextPage="";
	private double waitBetweenLetters=0.05;
	private double nextLetterTimer=0;
	private Label myNameNode;
	private Label myTextNode;
	private AudioStreamPlayer2D mySoundPlayer;
	public void closeUp()
	{
		Visible=false;
		if (PlayerStats.Instance!=null)
		{
			PlayerStats.Instance.IsSomethingOpenInMap=false;
		}
		
		displayName="";
		displayText="";
		audioPath="mute";
		GetTree().CallGroup("canTalkNPC", "StopTalking");
	}
	
	public void openUp()
	{
		if (PlayerStats.Instance==null)
		{
			return;
		}
		 PlayerStats.Instance.IsSomethingOpenInMap=true;
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
		audioPath=nextP.audioPath;
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
				if (displayText.Length%3==0)
				{
					PlaySound(audioPath);
				}
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
				nextLetterTimer-=delta*9;
			}
			if (Input.IsActionPressed("ui_close_dialog"))
			{
				closeUp();
			}
		}
		else if (Input.IsActionJustPressed("ui_accept"))
		{
			goToNextPage();
		}
	}
	
	public override void _Ready()
	{
		
		AddToGroup("talkBubble");
		currentPages=new ();
		Visible = false;
		myNameNode=GetNode<Label>("NameLabel");
		myTextNode=GetNode<Label>("SaidTextLabel");
		mySoundPlayer=GetNode<AudioStreamPlayer2D>("SoundPlayer");
	}
	
	public void PlaySound(string soundPath)
	{
		if (soundPath==null || soundPath=="mute")
		{
			return;
		}
		mySoundPlayer.Stream = GD.Load<AudioStream>(soundPath);
		mySoundPlayer.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Visible)
		{
			amITalking=false;
		}
		if (amITalking)
		{
			doTextStep(delta);
			myNameNode.Text=displayName;
			myTextNode.Text=displayText;
		}
		
	}
}
