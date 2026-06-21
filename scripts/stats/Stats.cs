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

            _intStatEntries.Clear();
            _intStats.Clear();

            foreach (var item in value)
            {
                if (item is IntStatEntry entry)
                {
                    _intStatEntries.Add(entry);
                    _intStats.Add(entry.Key, entry.Value);
                }
                else if (item is not null)
                {
                    GD.PushError($"Corrupt Stats data found! Expected IntStatEntry, got {item.GetType()}");
                }
            }
        }
    }
    
    [Export] private Godot.Collections.Array<BoolStatEntry> BoolStatEntries
    {
         get => _boolStatEntries;
        set
        {
            if (value == null) return;

            _boolStatEntries.Clear();
            _boolStats.Clear();

            foreach (var item in value)
            {
                if (item is BoolStatEntry entry)
                {
                    _boolStatEntries.Add(entry);
                    _boolStats.Add(entry.Key, entry.Value);
                }
                else if (item is not null)
                {
                    GD.PushError($"Corrupt Stats data found! Expected BoolStatEntry, got {item.GetType()}");
                }
            }
        }
    }

    private Godot.Collections.Array<IntStatEntry> _intStatEntries = [];
    private Godot.Collections.Array<BoolStatEntry> _boolStatEntries = [];

    private Dictionary<IntStatType, int> _intStats { get; set; } = [];
    private Dictionary<BoolStatType, bool> _boolStats { get; set; } = [];

    public int GetIntStat(IntStatType key)
    {
        if (_intStats.ContainsKey(key))
        {
            return _intStats[key];
        }
        else return 0;
    }

    public void SetIntStat(IntStatType key, int value)
    {
        if (value == 0 && !_intStats.ContainsKey(key)) return;
        _intStats[key] = value;
        ApplyStatRules();
    }

    public bool GetBoolStat(BoolStatType key)
    {
        if (_boolStats.ContainsKey(key))
        {
            return _boolStats[key];
        }
        else return false;
    }

    public void SetBoolStat(BoolStatType key, bool value)
    {
        if (value == false && !_boolStats.ContainsKey(key)) return;
        _boolStats[key] = value;
        ApplyStatRules();
    }

    public static Stats operator + (Stats left, Stats right)
    {
        Stats stats = new();

        foreach (var key in Enum.GetValues<IntStatType>())
        {
            stats.SetIntStat(key, left.GetIntStat(key) + right.GetIntStat(key));
        }

        foreach (var key in Enum.GetValues<BoolStatType>())
        {
            stats.SetBoolStat(key, left.GetBoolStat(key) || right.GetBoolStat(key));
        }

        return stats;
    }

    public override string ToString()
    {
        List<string> keyValueStringPairs = [];
        foreach (var key in Enum.GetValues<IntStatType>())
        {
            keyValueStringPairs.Add($"{key.ToString()}: {GetIntStat(key)}");
        }

        foreach (var key in Enum.GetValues<BoolStatType>())
        {
            keyValueStringPairs.Add($"{key.ToString()}: {GetBoolStat(key)}");
        }

        return string.Join("; ", keyValueStringPairs);
    }

    public string GetNonZeroStatsStringDictStyle()
    {
        List<string> keyValueStringPairs = [];
        foreach (var key in Enum.GetValues<IntStatType>())
        {
            if (GetIntStat(key) != 0) keyValueStringPairs.Add($"{key}:{GetIntStat(key)}");
        }

        foreach (var key in Enum.GetValues<BoolStatType>())
        {
            if (GetBoolStat(key)) keyValueStringPairs.Add($"{key}:{GetBoolStat(key)}");
        }

        return "{" + string.Join("; ", keyValueStringPairs) + "}";
    }

    public string GetNonZeroStatsStringNormalStyle()
    {
        List<string> keyValueStringPairs = [];
        foreach (var key in Enum.GetValues<IntStatType>())
        {
            if (GetIntStat(key) != 0) keyValueStringPairs.Add($"{key}: {GetIntStat(key)}");
        }

        foreach (var key in Enum.GetValues<BoolStatType>())
        {
            if (GetBoolStat(key)) keyValueStringPairs.Add($"{key}: {GetBoolStat(key)}");
        }

        return string.Join("\n", keyValueStringPairs);
    }

    private void ApplyStatRules()
    {
        if (GetIntStat(IntStatType.Bouyance) >= 10) SetBoolStat(BoolStatType.CanSwim, true);
    }
}
