using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 32.0f;
	private AnimatedSprite2D animation;
	public bool faceForward;
	public override void _Ready()
	{
		faceForward=true;
		animation = GetNode<AnimatedSprite2D>("Animation");
		animation.FlipH=false;
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity=direction*Speed;
			if (velocity.Y>0)
			{
				faceForward=true;
			}
			else if (velocity.Y<0)
			{
				faceForward=false;
			}
			if (velocity.X<0)
			{
				animation.FlipH=true;
			}
			else if (velocity.X>0)
			{
				animation.FlipH=false;
			}
			if (faceForward)
			{
				animation.Play("walk_forward");
			}
			else
			{
				animation.Play("walk_backwards");
			}
		}
		else
		{
			velocity= Vector2.Zero;
			if (faceForward)
			{
				animation.Play("idle_forward");
			}
			else
			{
				animation.Play("idle_backwards");
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
