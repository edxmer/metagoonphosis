using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class Stats : Resource
{
    [Export] private Godot.Collections.Array<IntStatEntry> IntStatEntries
    {
        get => _intStatEntries;
        set
        {
            if (value == null) return;

            _intStatEntries = value;

            _intStats = [];

            foreach (IntStatEntry entry in _intStatEntries)
            {
                if (entry is not null) _intStats.Add(entry.Key, entry.Value);
            }
        }
    }
    
    [Export] private Godot.Collections.Array<BoolStatEntry> BoolStatEntries
    {
         get => _boolStatEntries;
        set
        {
            if (value == null) return;

            _boolStatEntries = value;

            _boolStats = [];

            foreach (BoolStatEntry entry in _boolStatEntries)
            {
                if (entry is not null) _boolStats.Add(entry.Key, entry.Value);
            }
        }
    }

    private Godot.Collections.Array<IntStatEntry> _intStatEntries;
    private Godot.Collections.Array<BoolStatEntry> _boolStatEntries;

    private Dictionary<IntStatTypes, int> _intStats { get; set; } = [];
    private Dictionary<BoolStatTypes, bool> _boolStats { get; set; } = [];

    public int GetIntStat(IntStatTypes key)
    {
        if (_intStats.ContainsKey(key))
        {
            return _intStats[key];
        }
        else return 0;
    }

    public void SetIntStat(IntStatTypes key, int value)
    {
        if (value == 0 && !_intStats.ContainsKey(key)) return;
        _intStats[key] = value;
    }

    public bool GetBoolStat(BoolStatTypes key)
    {
        if (_boolStats.ContainsKey(key))
        {
            return _boolStats[key];
        }
        else return false;
    }

    public void SetBoolStat(BoolStatTypes key, bool value)
    {
        if (value == false && !_boolStats.ContainsKey(key)) return;
        _boolStats[key] = value;
    }

    public static Stats operator + (Stats left, Stats right)
    {
        Stats stats = new();

        foreach (var key in Enum.GetValues<IntStatTypes>())
        {
            stats.SetIntStat(key, left.GetIntStat(key) + right.GetIntStat(key));
        }

        foreach (var key in Enum.GetValues<BoolStatTypes>())
        {
            stats.SetBoolStat(key, left.GetBoolStat(key) || right.GetBoolStat(key));
        }

        return stats;
    }

    public override string ToString()
    {
        List<string> keyValueStringPairs = [];
        foreach (var key in Enum.GetValues<IntStatTypes>())
        {
            keyValueStringPairs.Add($"{key.ToString()}: {GetIntStat(key)}");
        }

        foreach (var key in Enum.GetValues<BoolStatTypes>())
        {
            keyValueStringPairs.Add($"{key.ToString()}: {GetBoolStat(key)}");
        }

        return string.Join("; ", keyValueStringPairs);
    }
}
