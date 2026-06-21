using Godot;
using System;

public partial class Interactable : Area2D
{
	// Called when the node enters the scene tree for the first time.
	protected AnimatedSprite2D animation;
	private Area2D playerLookHitbox;
	protected Player playerScene;
	private bool amISeen=false;
	public override void _Ready()
	{
		base._Ready();
		AddToGroup("interactable");
		AddToGroup("NPC");
		AreaEntered += OnAreaEntered;
		AreaExited += OnAreaExited;
		animation=GetNode<AnimatedSprite2D>("Sprite");
		playerScene = GetTree().GetFirstNodeInGroup("PlayerGoon") as Player;
		if (playerScene != null)
		{
			playerLookHitbox = playerScene.GetNode<Area2D>("LookingAtHitbox");
		}
	}
	private void OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("playerLookHitbox"))
		{

			amISeen=true;
		}
	}
	private void OnAreaExited(Area2D area)
	{
		if (area.IsInGroup("playerLookHitbox"))
		{
			amISeen=false;
		}
	}
	public virtual void ActionOnInteract()
	{
		var pos=Position;
		pos.X+=5;
		Position=pos;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (amISeen &&Input.IsActionJustPressed("ui_accept") && !PlayerStats.Instance.IsSomethingOpenInMap)
		{
			
			ActionOnInteract();
			
		}
	}
}
