using Godot;
using System;

public partial class Interactable : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animation;
	private Area2D playerLookHitbox;
	
	private bool amISeen=false;
	public override void _Ready()
	{
		base._Ready();
		AreaEntered += OnAreaEntered;
		AreaExited += OnAreaExited;
		animation=GetNode<AnimatedSprite2D>("Sprite");
		var player = GetTree().GetFirstNodeInGroup("palayer");
		if (player != null)
		{
			playerLookHitbox = player.GetNode<Area2D>("LookingAtHitbox");
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
		if (amISeen &&Input.IsActionJustPressed("ui_accept"))
		{
			
			ActionOnInteract();
			
		}
	}
}
