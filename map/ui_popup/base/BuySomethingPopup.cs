using Godot;
using System;

public partial class BuySomethingPopup : Node2D
{
	
	public int neededMoney=0;
	protected Label myLabel;
	public bool setText=false;
	protected TextureButton myButtonDen;
	protected TextureButton myButtonAcc;
	protected bool itsOver=false;
	private AudioStreamPlayer2D mySoundPlayer;
	public string myText="";
	
	public virtual void setMyText()
	{
		myLabel.Text=myText+" "+neededMoney.ToString()+"$";
	}
	public override void _Ready()
	{
		itsOver=false;
		myButtonAcc = GetNode<TextureButton>("Accept");
		myButtonAcc.Pressed += OnButtonPressedAccept;
		myButtonDen = GetNode<TextureButton>("Deny");
		myLabel = GetNode<Label>("Whattosay");
		myButtonDen.Pressed += OnButtonPressedDeny;
		mySoundPlayer=GetNode<AudioStreamPlayer2D>("Sounds");
		AddToGroup("canTalkNPC");
	}
	public virtual void RemoveMoney()
	{
		PlayerStats.Instance.PlayerInventory.RemoveItem("Dollar",neededMoney);
	}
	public virtual void AddReward()
	{
		PlayerStats.Instance.PlayerInventory.AddItem("Dollar",neededMoney);
	}
	public bool HasEnoughMoney()
	{
		return PlayerStats.Instance.PlayerInventory.GetCount("Dollar")>=neededMoney;
	}
	public void PlayDenySound()
	{
		mySoundPlayer.Stream = GD.Load<AudioStream>("res://assets/sounds/talksounds/sound_buy_refuse.mp3");
		mySoundPlayer.Play();
	}
	public void PlayAcceptSound()
	{
		mySoundPlayer.Stream = GD.Load<AudioStream>("res://assets/sounds/talksounds/sound_buy.wav");
		mySoundPlayer.Play();
	}
	protected virtual void Kill()
	{
		QueueFree();
	}
	
	public void StopTalking()
	{
		Kill();
	}
	protected virtual void OnButtonPressedDeny()
	{
		Kill();
	}
	protected virtual void OnButtonPressedAccept()
	{
		if (itsOver)
		{
			Kill();
		}
		if (HasEnoughMoney())
		{
			RemoveMoney();
			AddReward();
			PlayAcceptSound();
		}
		else
		{
			
			PlayDenySound();
		}
		itsOver=true;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (itsOver && (!mySoundPlayer.Playing))
		{
			Kill();
		}
		if (!PlayerStats.Instance.IsSomethingOpenInMap)
		{
			Kill();
		}
			setMyText();
		
	}
}
