using Godot;
using System;
using System.Linq;

[GlobalClass]
public partial class CavityGridUI : Control
{
	// Visual size of a slot, in pixels
	[Export] public int SlotSizePx { get; set; } = 64;

	private Vector2 _dragPointerOffset;
	private Cavity _cavity;

	public override void _Ready()
	{
		Initialize(PlayerStats.Instance.Cavity);
	}
	
	private void Initialize(Cavity cavity)
	{
		_dragPointerOffset = new Vector2(SlotSizePx * 0.5f, SlotSizePx * 0.5f);
		_cavity = cavity;
		ReloadVisualItems();
		CustomMinimumSize = new Vector2(_cavity.Width * SlotSizePx, _cavity.Height * SlotSizePx);
		cavity.OrgansChanged += ReloadVisualItems;
	}

    public override void _Draw()
    {
		Color lineColor = new Color(1, 1, 1, 0.2f);

        for (int x=0; x<_cavity.Width; ++x) for (int y=0; y<_cavity.Height; ++y)
		if (_cavity.Shape[x, y])
		{
			Vector2 topLeftCorner = new Vector2(x*SlotSizePx, y*SlotSizePx);
			Vector2 topRightCorner = new Vector2((x+1)*SlotSizePx, y*SlotSizePx);
			Vector2 botLeftCorner = new Vector2(x*SlotSizePx, (y+1)*SlotSizePx);
			Vector2 botRightCorner = new Vector2((x+1)*SlotSizePx, (y+1)*SlotSizePx);

			DrawLine(topLeftCorner, topRightCorner, lineColor);
			DrawLine(topRightCorner, botRightCorner, lineColor);
			DrawLine(botRightCorner, botLeftCorner, lineColor);
			DrawLine(botLeftCorner, topLeftCorner, lineColor);
		}
    }

	public void ReloadVisualItems()
    {
		// Free all children of this node
		foreach (Node c in GetChildren())
		{
			c.QueueFree();
		}

		// Spawn in all items in chestcavity

		foreach (var (slot, origin) in _cavity.GetItems())
		{
			//($"Spawning visual item: {slot.Organ.OrganName}, {origin.ToString()}");
			SpawnVisualItem(slot, origin);
		}
    }

	public void SpawnVisualItem(CavitySlot slot, Vector2I origin)
	{
		var view = new CavityOrganViewUI();
		view.Initialize(slot, SlotSizePx);

		view.Position = new Vector2(origin.X * SlotSizePx, origin.Y * SlotSizePx);

		AddChild(view);
	}

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
		atPosition += _dragPointerOffset;

        var dragData = data.AsGodotDictionary();
		if (!dragData.ContainsKey("slot") || !dragData.ContainsKey("grab_offset")) return false;

		CavitySlot slot = (CavitySlot)dragData["slot"];
		Vector2 offset = (Vector2)dragData["grab_offset"];

		Vector2I targetGridPosition = CalculateTargetGridPosition(atPosition, offset);

		bool fitsInCavity = _cavity.CanChangeSlotPosition(slot, targetGridPosition);

		return fitsInCavity;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
		atPosition += _dragPointerOffset;

		var dragData = data.AsGodotDictionary();
		if (!dragData.ContainsKey("slot") || !dragData.ContainsKey("grab_offset")) return;

		CavitySlot slot = (CavitySlot)dragData["slot"];
		Vector2 offset = (Vector2)dragData["grab_offset"];

		Vector2I targetGridPosition = CalculateTargetGridPosition(atPosition, offset);

		_cavity.RemoveSlot(slot);
		bool success = _cavity.TryPlaceSlot(slot, targetGridPosition);
		if (!success) GD.PushWarning($"Unsuccessful slot placing at {targetGridPosition}.");
    }

	private Vector2I CalculateTargetGridPosition(Vector2 mousePosition, Vector2 offset)
	{
		Vector2 positionFromZero = mousePosition - offset;

		var flooredPosition = new Vector2I(
			Mathf.FloorToInt(positionFromZero.X / SlotSizePx),
			Mathf.FloorToInt(positionFromZero.Y / SlotSizePx)
		);

		return flooredPosition;
	}
}
