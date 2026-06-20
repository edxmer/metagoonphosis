using Godot;
using System;
using System.Linq;

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
			scores[itemName] = count;
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
		return (getCount(itemName)>=minCount);
	}
}
