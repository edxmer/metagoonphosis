using Godot;
using System;

public partial class Interactable : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private AnimatedSprite2D animation;
	private Area2D playerLookHitbox;
	public override void _Ready()
	{
		animation=GetNode<AnimatedSprite2D>("Sprite");
		var player = GetTree().GetFirstNodeInGroup("player");
		if (player != null)
		{
			playerLookHitbox = player.GetNode<Area2D>("LookingAtHitbox");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			var overlapping=GetOverlappingBodies();
			if (overlapping.Contains(playerLookHitbox))
			{
				Vector2 pos=Position;
				pos.X+=5;
				Position=pos;
			}
			
		}
	}
}
