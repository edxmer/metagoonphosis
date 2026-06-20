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
		itemspos=GetNode<Node2D>("ItemsContained");
	}
	
	private void OnOpened()
	{
		
		Visible=true;
		KillItemsCur();
		PlayerStats.Instance.IsSomethingOpenInMap=true;
		ListChildren(PlayerStats.Instance.PlayerInventory);
	}
	private void OnClosed()
	{
		KillItemsCur();
		PlayerStats.Instance.IsSomethingOpenInMap=false;
		Visible=false;
	}
	private void ListChildren(PlayerInventory inv)
	{
		string[] items=inv.ListItems();
		int count=0;
		foreach (string item in items)
		{
			
			var one=InventoryItemFactory.CreateItem(itemspos,item,inv.GetCount(item));
			one.Position=new Vector2(itemspos.Position.X,itemspos.Position.Y+count*PosOneY);
			count++;
			
		}
		amount=count;
	}
	private void KillItemsCur()
	{
		foreach (Node child in itemspos.GetChildren())
		{
			if (child is InventoryItem item)
			{
				item.Free();
			}
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionJustPressed("ui_open_inventory") )
		{
			if (Visible)
			{
				OnClosed();
			}
			else if (!PlayerStats.Instance.IsSomethingOpenInMap)
			{
				OnOpened();
			}
		}
	}
}
