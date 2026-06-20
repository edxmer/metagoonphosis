using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 48.0f;
	private static int HitboxDistance=16;
	private AllSpritesPlace animationAll;
	private Area2D lookingAt;
	public bool faceForward;
	public Vector2 facingDir;
	public override void _Ready()
	{
		AddToGroup("PlayerGoon");
		faceForward=true;
		animationAll = GetNode<AllSpritesPlace>("AllSpritesPlace");
		lookingAt = GetNode<Area2D>("LookingAtHitbox");
		animationAll.FlipH(false);
		facingDir=Vector2.Down;
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			facingDir=direction;
			velocity=direction*Speed;
			if (velocity.Y>=0)
			{
				faceForward=true;
			}
			else
			{
				faceForward=false;
			}
			if (velocity.X<0)
			{
				animationAll.FlipH(true);
				animationAll.Position = new Vector2(-5, 0);
			}
			else if (velocity.X>0)
			{
				animationAll.FlipH(false);
				animationAll.Position = new Vector2(0, 0);
			}
			if (faceForward)
			{
				animationAll.changeAnim("walk_forward");
			}
			else
			{
				animationAll.changeAnim("walk_backwards");
			}
			lookingAt.Position=facingDir.Normalized()*HitboxDistance;
		}
		else
		{
			velocity= Vector2.Zero;
			if (faceForward)
			{
				animationAll.changeAnim("idle_forward");
			}
			else
			{
				animationAll.changeAnim("idle_backwards");
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
