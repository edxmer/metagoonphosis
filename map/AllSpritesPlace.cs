using Godot;
using System;
using System.Collections.Generic;
public partial class AllSpritesPlace : Node2D
{
	// Called when the node enters the scene tree for the first time.

	private List<AnimatedSprite2D> mySprites;
	
	public override void _Ready()
	{
		mySprites = new List<AnimatedSprite2D>();
		foreach (Node child in GetChildren())
		{
			if (child is AnimatedSprite2D sprite)
			{
				mySprites.Add(sprite);
			}
		}
	}
	public void changeAnim(string changeTo)
	{
		foreach (AnimatedSprite2D spr in mySprites)
		{
			spr.Play(changeTo);
		}
	}
	public void FlipH(bool to)
	{
		foreach (AnimatedSprite2D spr in mySprites)
		{
			spr.FlipH=to;
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
