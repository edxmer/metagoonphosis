using Godot;
using System;

public partial class Organ : Node
{
    // Top left position
    [Export]
    public int TopLeftPositionX { get; set; }
    [Export]
    public int TopLeftPositionY { get; set; }

    [Export]
    public int Width { get; set; }
    [Export]
    public int Height { get; set; }

    [Export(PropertyHint.MultilineText)]
    public string OrganShapeString { get; set; }

    public bool[,] Shape;

    public void ParseOrganShape()
    {
        Shape = new bool[Height, Width];

        int i = 0;
        foreach(string line in OrganShapeString.Split('\n'))
        {
            if (Height <= i) throw new Exception("Organ height does not match the height of the provided Organ string.");

            int j = 0;
            foreach (char c in line)
            {
                if (Width <= j) throw new Exception("Organ width does not match the width of the provided Organ string.");

                if (c == 'O') Shape[i, j] = true;
                
                ++j;
            }

            ++i;
        }
    }
}
