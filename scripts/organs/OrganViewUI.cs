using Godot;
using Godot.NativeInterop;
using System;

public partial class OrganViewUI : TextureRect
{
	private OrganSlot _slot;
	private int _slotSizePx;

	public void Initialize(OrganSlot slot, int slotSize)
	{
		_slot = slot;
		_slotSizePx = slotSize;

		Organ organ = _slot.Organ;

		Texture = organ.Texture;
		
		TooltipText = organ.OrganName;
		
		ExpandMode = ExpandModeEnum.IgnoreSize;

		CustomMinimumSize = new Vector2(organ.Width * _slotSizePx, organ.Height * _slotSizePx);
	}

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var preview = new TextureRect
		{
			Texture = Texture,
			ExpandMode = ExpandModeEnum.IgnoreSize,
			CustomMinimumSize = CustomMinimumSize,
			Modulate = new Color(1, 1, 1, 0.5f),
		};

		Control previewContainer = new();
		previewContainer.AddChild(preview);
		preview.Position = -atPosition;

		SetDragPreview(previewContainer);

		Modulate = new Color(1, 1, 1, 0.1f);

		var dragData = new Godot.Collections.Dictionary
		{
			{ "slot", _slot},
			{ "grab_offset", atPosition },
		};

		return dragData;
    }

	    public override void _Notification(int what)
    {
        base._Notification(what);

		if (what == NotificationDragEnd)
		{
			if (!GetViewport().GuiIsDragSuccessful())
			{
				Modulate = new Color(1, 1, 1, 1.0f);
			}
		}
    }

    public override bool _HasPoint(Vector2 point)
    {
        int localGridX = Mathf.FloorToInt(point.X / _slotSizePx);
        int localGridY = Mathf.FloorToInt(point.Y / _slotSizePx);

		Organ organ = _slot.Organ;
		
		if (0 <= localGridX && localGridX < organ.Width && 0 <= localGridY &&  localGridY < organ.Height)
			return organ.Shape[localGridX, localGridY];
		
		return false;
    }
}
