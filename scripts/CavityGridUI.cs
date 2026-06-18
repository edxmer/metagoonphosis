using Godot;
using System;

[GlobalClass]
public partial class CavityGridUI : Control
{
	// Visual size of a slot, in pixels
	[Export] public int SlotSizePx { get; set; } = 64;

	private ChestCavity _chestCavity;
	private Vector2 PositionOffset = Vector2.Zero; // Offset to centre the draw functions


	public void Initialize(ChestCavity cavity)
	{
		_chestCavity = cavity;
		PositionOffset = new Vector2(-cavity.Width * SlotSizePx * 0.5f, -cavity.Height * SlotSizePx * 0.5f);
		ReloadVisualItems();
	}

    public override void _Draw()
    {
		Color lineColor = new Color(1, 1, 1, 0.2f);

        for (int x=0; x<_chestCavity.Width; ++x) for (int y=0; y<_chestCavity.Height; ++y)
		if (_chestCavity.Shape[x, y])
		{
			Vector2 topLeftCorner = new Vector2(x*SlotSizePx, y*SlotSizePx) + PositionOffset;
			Vector2 topRightCorner = new Vector2((x+1)*SlotSizePx, y*SlotSizePx) + PositionOffset;
			Vector2 botLeftCorner = new Vector2(x*SlotSizePx, (y+1)*SlotSizePx) + PositionOffset;
			Vector2 botRightCorner = new Vector2((x+1)*SlotSizePx, (y+1)*SlotSizePx) + PositionOffset;

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

		foreach (var (slot, origin) in _chestCavity.GetItems())
		{
			GD.Print($"Spawning visual item: {slot.Organ.OrganName}, {origin.ToString()}");
			SpawnVisualItem(slot, origin);
		}
    }

	public void SpawnVisualItem(CavitySlot slot, Vector2I origin)
	{
		var view = new OrganViewUI();
		view.Initialize(slot, SlotSizePx);

		view.Position = new Vector2(origin.X * SlotSizePx, origin.Y * SlotSizePx) + PositionOffset;

		AddChild(view);
	}
}
