using Godot;
using System;

public static class InventoryItemFactory
{
	// Called when the node enters the scene tree for the first time.
	
	public static InventoryItem CreateItem(PackedScene InventoryScene,string name,int amount)
	{
		var item = InventoryScene.Instantiate<InventoryItem>();
		item.SetText(name+" x"+amount.ToString());
		return item;
	}
}
