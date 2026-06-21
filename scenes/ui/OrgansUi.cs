using Godot;
using System;

public partial class OrgansUi : Control
{
	private bool open=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
		open=false;
	}
	private void OnOpened()
	{
		open=true;
		Show();
		PlayerStats.Instance.IsSomethingOpenInMap=true;
	}
	private void OnClosed()
	{
		open=false;
		PlayerStats.Instance.IsSomethingOpenInMap=false;
		Hide();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_open_cavity"))
		{
			if (!open && !PlayerStats.Instance.IsSomethingOpenInMap)
			{
				OnOpened();
			}
			else if (open)
			{
				OnClosed();
			}
		}
	}
}
