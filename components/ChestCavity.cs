using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class ChestCavity : Node2D
{
	/// <summary>
	/// O for available position,
	/// X (or anything else) for not available position.
	/// 
	/// If line is cut off early, it will
	/// consider the rest of it not available.
	/// </summary>
	[Export(PropertyHint.MultilineText)]
	public string CavityShapeString { get; set; }

	private int _width, _height;
	private bool[,] _shape;
	private bool[,] _takenPositions;

	public override void _Ready()
	{
		// Parsing shape from shape string
		(_width, _height) = ShapeStringHelper.GetDimensions(CavityShapeString);
		_shape = ShapeStringHelper.ParseShape(CavityShapeString, _width, _height);

		LoadOrgans();
	}

	public List<Organ> GetOrgans() => GetChildren().OfType<Organ>().ToList();

	public void LoadOrgans()
	{
		_takenPositions = new bool[_width, _height];
		// TODO: finish this
	}

	public bool DoesFit(Organ organ)
	{
		int iOffset = organ.TopLeftPositionY;
		int jOffset = organ.TopLeftPositionX;

		for (int i = 0; i<organ.Height; ++i)
		{
			for (int j = 0; j<organ.Width; ++j)
			{
				if (
					organ.Shape[i, j] 
					&& !((i+iOffset < _height) 
						&& (j+jOffset < _width)
						&& !_shape[i+iOffset, j+jOffset]
						)
					) return false;
			}
		}

		return true;
	}

	public void AddOrgan(Organ organ)
	{
		// TODO: is this really needed? Maybe we will use a different system
		if (!DoesFit(organ)) throw new Exception("Organ does not fit!");
	}

	
}
