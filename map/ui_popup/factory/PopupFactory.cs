using Godot;
using System;

public static class PopupFactory
{
		private static PackedScene popupItemScene = GD.Load<PackedScene>("res://map/ui_popup/item/buy_something_popup_item.tscn");
	private static PackedScene popupOrganScene = GD.Load<PackedScene>("res://map/ui_popup/organ/buy_something_popup_organ.tscn");
	// Called when the node enters the scene tree for the first time.
	public static BuySomethingPopup CreatePopupItem(Node2D parent,int cost,string rewardItem,int rewardAmount)
	{
		var item =  popupItemScene.Instantiate<BuySomethingPopupItem>();
		parent.AddChild(item);
		item.rewardItem=rewardItem;
		item.rewardAmount=rewardAmount;
		item.neededMoney=cost;
		item.Position=parent.Position;
		return item;
	}
	public static BuySomethingPopup CreatePopupItem(Node2D parent,int cost,string rewardItem)
	{
		return CreatePopupItem(parent,cost,rewardItem,1);
	}
	public static BuySomethingPopup CreatePopupOrgan(Node2D parent,int cost,Organ organ,string name)
	{
		var item = popupOrganScene.Instantiate<BuySomethingPopupOrgan>();
		parent.AddChild(item);
		item.neededMoney=cost;
		item.Position=parent.Position;
		item.givenOrgan=organ;
		item.myText=name+"\n";
		return item;
	}
}
