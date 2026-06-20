using Godot;
using System;

public partial class InventoryItem : Label
{
	private static LabelSettings settings=GD.Load<LabelSettings>("res://assets/fonts/fontSettings/inventoryitem.tres");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LabelSettings =settings;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SetText(string text)
	{
		Text=text;
	}
}
