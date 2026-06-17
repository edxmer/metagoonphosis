using Godot;
using System;

public enum OrganType
{
	HEART,
	LUNG,
	OTHER
}

[GlobalClass]
public partial class Organ : Node
{
	[Export]
	public OrganType Type { get; set; } = OrganType.OTHER;

	// Top left position
	[Export]
	public int TopLeftPositionX { get; set; }
	[Export]
	public int TopLeftPositionY { get; set; }

	[Export(PropertyHint.MultilineText)]
	public string OrganShapeString { get; set; }

	public bool[,] Shape;

	public int Width, Height;

	public override void _Ready()
	{
		(Width, Height) = ShapeStringHelper.GetDimensions(OrganShapeString);
		Shape = ShapeStringHelper.ParseShape(OrganShapeString, Width, Height);
	}
}
