using Godot;
using System;

public partial class CreatureFly : NpcTalkableThrowaway
{
	protected virtual void AfterTalkedEvent()
	{
		
	}
	public override void ActionOnInteract()
	{
		SaySomething();
	}
}
