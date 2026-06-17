using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class ShapeStringHelper
{
    private ShapeStringHelper() {}
    
    public static (int, int) GetDimensions(string shape)
    {
        int maxI=0, maxJ=0;

        int i=0;
        foreach (string line in shape.Split('\n'))
        {
            int j=0;
            foreach (char c in line)
            {
                if (c == 'O')
                {
                    maxI = Math.Max(maxI, i);
                    maxJ = Math.Max(maxJ, j);
                }
            }   
        }

        return (maxI+1, maxJ+1);
    }

    public static bool[,] ParseShape(string shapeString, int width, int height)
    {
        bool[,] shape = new bool[height, width];

        int i = 0;
        foreach(string line in shapeString.Split('\n'))
        {
            int j = 0;
            foreach (char c in line)
            {
                if (c == 'O') shape[i, j] = true;
                ++j;
            }
            ++i;
        }
        
        return shape;
    }
}
