using Godot;
using System;

public static class PopupFactory
{
		private static PackedScene popupItemScene = GD.Load<PackedScene>("res://map/ui_popup/item/buy_something_popup_item.tscn");
	private static PackedScene popupOrganScene = GD.Load<PackedScene>("res://map/ui_popup/organ/buy_something_popup_organ.tscn");
	private static PackedScene popupFightScene = GD.Load<PackedScene>("res://map/ui_popup/fight/buy_something_popup_fight.tscn");
	// Called when the node enters the scene tree for the first time.
	public static BuySomethingPopup CreatePopupItem(Node2D parent,int cost,string rewardItem,int rewardAmount)
	{
		var item =  popupItemScene.Instantiate<BuySomethingPopupItem>();
		parent.AddChild(item);
		item.rewardItem=rewardItem;
		item.rewardAmount=rewardAmount;
		item.neededMoney=cost;
		item.Position=new Vector2(parent.Position.X,parent.Position.Y+35);
		item.setMyText();
		return item;
	}
	public static BuySomethingPopup CreatePopupFight(Node2D parent,EnemyData enemyData,string text,int rewardMoney,Organ? rewardOrgan)
	{
			
		var item =  popupFightScene.Instantiate<BuySomethingPopupFight>();
		parent.AddChild(item);
		item.enemyData=enemyData;
		item.rewardOrgan=rewardOrgan;
		item.parent=parent;
		item.rewardMoney=rewardMoney;
		item.neededMoney=0;
		item.Position=new Vector2(parent.Position.X,parent.Position.Y+35);
		item.setMyText();
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
		item.Position=new Vector2(parent.Position.X,parent.Position.Y+35);
		item.givenOrgan=organ;
		item.myText=name+"\n";
		item.setMyText();
		return item;
	}
}
