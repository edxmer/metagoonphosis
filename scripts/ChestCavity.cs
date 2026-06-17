using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class ChestCavity : Resource
{
	/// <summary>
	/// O for available position,
	/// X (or anything else) for not available position.
	/// 
	/// If line is cut off early, it will
	/// consider the rest of it not available.
	/// </summary>
	[Export(PropertyHint.MultilineText)]
	public string CavityShapeString
	{
		get => _cavityShapeString; 
		set
		{
			_cavityShapeString = value;
			(_width, _height) = ShapeStringHelper.GetDimensions(value);
			_shape = ShapeStringHelper.ParseShape(value, _width, _height);
		}
	}

	public List<Organ> Organs;

	private int _width, _height;
	private bool[,] _shape;
	private bool[,] _takenPositions;
	private string _cavityShapeString;

	public void LoadOrgans()
	{
		_takenPositions = new bool[_width, _height];
		// TODO: finish this
	}

	public bool DoesFit(Organ organ, int posI, int posJ)
	{
		for (int i = 0; i<organ.Height; ++i)
		{
			for (int j = 0; j<organ.Width; ++j)
			{
				if 
				(
					organ.Shape[i, j] && 
					!(
						(i+posI < _height) &&
						(j+posJ < _width)  &&
						!_shape[i+posI, j+posJ]
					)
				) return false;
			}
		}

		return true;
	}

	public void AddOrgan(Organ organ, int posI, int posJ)
	{
		
	}

	
}
