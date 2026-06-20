using System;
using Godot;

[GlobalClass]
public partial class UnusedOrganListUI : ScrollContainer
{
    // Visual size of a slot, in pixels
	[Export] public int SlotSizePx { get; set; } = 64;

	private Cavity _cavity;
    private UnusedOrgans _unusedOrgans;

    public override void _Ready()
    {
        _cavity = PlayerStats.Instance.Cavity;
        _unusedOrgans = PlayerStats.Instance.UnusedOrgans;

        _unusedOrgans.UnusedOrgansChanged += ReloadVisualItems;

		ReloadVisualItems();
    }

    public void ReloadVisualItems()
    {
        var container = GetNode<VBoxContainer>("VBoxContainer");
        
		// Free all children of this node
		foreach (Node c in container.GetChildren())
		{
			c.QueueFree();
		}

		// Spawn in all visual items from _unusedOrgans
		foreach (var slot in _unusedOrgans.GetSlots())
		{
			SpawnVisualItem(slot, container);
		}
    }

    private void SpawnVisualItem(OrganSlot slot, Node parent)
    {
        GD.Print("Spawning slot: ", slot.Organ.OrganName);

        var view = new OrganViewUI();
		view.Initialize(slot, SlotSizePx);

		parent.AddChild(view);
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        var dragData = data.AsGodotDictionary();
		return dragData.ContainsKey("slot") && dragData.ContainsKey("grab_offset");
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
		var dragData = data.AsGodotDictionary();
		if (!dragData.ContainsKey("slot") || !dragData.ContainsKey("grab_offset")) return;

		OrganSlot slot = (OrganSlot)dragData["slot"];

        GD.Print("Slot organ name: ", slot.Organ.OrganName);

		if (_cavity.Contains(slot)) // Changing position from Cavity
		{
			_cavity.RemoveSlot(slot);
		}
		else // Moving from UnusedOrgan
		{
			_unusedOrgans.Remove(slot);
		}

        _unusedOrgans.Add(slot);
    }
}