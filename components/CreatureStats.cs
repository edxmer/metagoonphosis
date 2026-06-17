using Godot;
using System;

[GlobalClass]
public partial class CreatureStats : Node
{
	// Base stats
	public int Hp { get; set; }
	public int Coolness { get; set; }
	public int IQ { get; set; }
	public int Power { get; set; }
	public int AttackDamage { get; set; }
	public int Defens { get; set; }
	public int Luminescence { get; set; }
	public bool HasHair { get; set; }
	public bool CanSwim { get; set; }
	public bool HasCatEars { get; set; }
	public int Catness { get; set; }
	public int Bouyance { get; set; }

	
}
