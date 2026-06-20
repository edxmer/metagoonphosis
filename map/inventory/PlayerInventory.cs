using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
public class PlayerInventory
{
	private Dictionary<string,int> inventory;
	public PlayerInventory()
	{
		inventory= new();
	}
	public string[] ListItems()
	{
		return inventory.Keys.ToArray();
	}
	public void AddItem(string itemName,int count)
	{
		if (inventory.ContainsKey(itemName))
		{
			inventory[itemName]+=count;
		}
		else
		{
			inventory[itemName] = count;
		}
	}
	public void AddItem(string itemName)
	{
		AddItem( itemName,1);
	}
	public bool RemoveItem(string itemName,int count)
	{
		if (HasItem( itemName,count))
		{
			AddItem(itemName, -count);
			if (GetCount(itemName)<=0)
			{
				inventory.Remove(itemName);
			}
			return true;
			
		}
		return false;
	}
	
	public int GetCount(string itemName)
	{
		if (inventory.ContainsKey(itemName))
		{
			return inventory[itemName];
		}
		return 0;
	}
	public bool HasItem(string itemName,int minCount)
	{
		return (GetCount(itemName)>=minCount);
	}
}
