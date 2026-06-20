using Godot;
using System;

public partial class InventoryUi : Node2D
{
	private Node2D itemspos;
	private int amount;
	private int PosOneY=16;
	private bool Open;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible=false;
		Open=false;
	}
	
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
