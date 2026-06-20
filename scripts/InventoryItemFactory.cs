using Godot;
using System;

public static class InventoryItemFactory
{
	// Called when the node enters the scene tree for the first time.
	
	public static InventoryItem CreateItem(Node2D parent,string name,int amount)
	{
		var item = new InventoryItem();
		parent.AddChild(item);
		item.SetText(name+" x"+amount.ToString());
		return item;
	}
}
