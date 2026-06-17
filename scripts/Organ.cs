using Godot;
using System;

public enum OrganType
{
	HEART,
	LUNG,
	OTHER
}

[GlobalClass]
public partial class Organ : Resource
{
	[Export]
	public OrganType Type { get; set; } = OrganType.OTHER;

    [Export]
    public Texture2D Icon { get; set; }

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
