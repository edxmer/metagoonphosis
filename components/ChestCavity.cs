using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class ChestCavity : Node
{
    [Export]
    public int Width { get; set; }

    [Export]
    public int Height { get; set; }

    /// <summary>
    /// O for available position,
    /// X (or anything else) for not available position.
    /// 
    /// If line is cut off early, it will
    /// consider the rest of it not available.
    /// </summary>
    [Export(PropertyHint.MultilineText)]
    public string CavityShapeString { get; set; }

    private bool[,] _shape;
    private bool[,] _takenPositions;

    public override void _Ready()
    {
        ParseCavityShape();
        LoadOrgans();
    }

    public void ParseCavityShape()
    {
        _shape = new bool[Height, Width];

        int i = 0;
        foreach(string line in CavityShapeString.Split('\n'))
        {
            if (Height <= i) throw new Exception("Cavity height does not match the height of the provided cavity string.");

            int j = 0;
            foreach (char c in line)
            {
                if (Width <= j) throw new Exception("Cavity width does not match the width of the provided cavity string.");

                if (c == 'O') _shape[i, j] = true;
                
                ++j;
            }

            ++i;
        }
    }

    public List<Organ> GetOrgans() => GetChildren().OfType<Organ>().ToList();

    public void LoadOrgans()
    {
        _takenPositions = new bool[Width, Height];
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
                    && !((i+iOffset < Height) 
                        && (j+jOffset < Width) 
                        && !_shape[i+iOffset, j+jOffset]
                        )
                    ) return false;
            }
        }

        return true;
    }

    public void AddOrgan(Organ organ)
    {
        if (!DoesFit(organ)) throw new Exception("Organ does not fit!");
    }

    
}
