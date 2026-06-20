using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Stats : Resource
{
    public Dictionary<IntStatTypes, int> IntStats { get; set; }
    public Dictionary<BoolStatTypes, bool> BoolStats { get; set; }

    [Export] private Godot.Collections.Array<IntStatEntry> IntStatEntries
    {
        get => _intStatEntries;
        set
        {
            _intStatEntries = value;

            IntStats = new();

            foreach (var entry in _intStatEntries)
            {
                IntStats.Add(entry.Key, entry.Value);
            }
        }
    }
    
    [Export] private Godot.Collections.Array<BoolStatEntry> BoolStatEntries
    {
         get => _boolStatEntries;
        set
        {
            _boolStatEntries = value;

            BoolStats = new();

            foreach (var entry in _boolStatEntries)
            {
                BoolStats.Add(entry.Key, entry.Value);
            }
        }
    }

    private Godot.Collections.Array<IntStatEntry> _intStatEntries;
    private Godot.Collections.Array<BoolStatEntry> _boolStatEntries;

    public static Stats GetDefaultStats()
    {
        Stats stats = new();
        
        stats.IntStats = [];
        foreach (var type in Enum.GetValues<IntStatTypes>())
        {
            stats.IntStats.Add(type, 0);
        }

        stats.BoolStats = [];
        foreach (var type in Enum.GetValues<BoolStatTypes>())
        {
            stats.BoolStats.Add(type, false);
        }

        return stats;
    }
}
