using Godot;
using System;

public partial class InventoryItem : Node2D
{
	private Label myItemText;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myItemText=GetNode<Label>("InventoryItemText");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SetText(string text)
	{
		myItemText.Text=text;
	}
}
