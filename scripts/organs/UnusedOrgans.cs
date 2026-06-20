using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class UnusedOrgans : Resource
{
    [Signal]
    public delegate void UnusedOrgansChangedEventHandler();

    private readonly HashSet<OrganSlot> _slots = [];

    public void Add(OrganSlot slot)
    {
        _slots.Add(slot);
        EmitSignal(SignalName.UnusedOrgansChanged);
    }

    public bool Contains(OrganSlot slot)
    {
        return _slots.Contains(slot);
    }

    public void Remove(OrganSlot slot)
    {
        _slots.Remove(slot);
        EmitSignal(SignalName.UnusedOrgansChanged);
    }

    public List<OrganSlot> GetSlots() => _slots.ToList();
}