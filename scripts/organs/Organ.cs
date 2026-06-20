using Godot;
using System;

[GlobalClass]
public partial class Organ : Resource
{
	[Export] public OrganType Type { get; set; } = OrganType.Other;
    [Export] public string OrganName { get; set; }
    [Export] public Texture2D Texture { get; set; }
    [Export] public Stats StatIncreases { get; set; } = new();

	[Export(PropertyHint.MultilineText)]
	public string OrganShapeString 
    { 
        get => _organShapeString; 
        set
        {
            _organShapeString = value;
            (Width, Height) = ShapeStringHelper.GetDimensions(value);
            Shape = ShapeStringHelper.ParseShape(value, Width, Height);
        }
    }

	public int Width, Height;
	public bool[,] Shape;

    private string _organShapeString;
}
