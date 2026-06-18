using Godot;
using System;

public partial class ShapeStringHelper
{
	private ShapeStringHelper() {}
	
	public static (int, int) GetDimensions(string shape)
	{
		int maxHeight=0, maxWidth=0;

		int y=0;
		foreach (string line in shape.Split('\n'))
		{
			int x=0;
			foreach (char c in line)
			{
				if (c == 'O')
				{
					maxHeight = Math.Max(maxHeight, y);
					maxWidth = Math.Max(maxWidth, x);
				}
				
				++x;
			}

			++y;
		}

		return (maxWidth+1, maxHeight+1);
	}

	public static bool[,] ParseShape(string shapeString, int width, int height)
	{
		bool[,] shape = new bool[width, height];

		int y = 0;
		foreach(string line in shapeString.Split('\n'))
		{
			int x = 0;
			foreach (char c in line)
			{
				if (c == 'O') shape[x, y] = true;
				++x;
			}
			++y;
		}
		
		return shape;
	}
}
