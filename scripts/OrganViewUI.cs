using Godot;
using Godot.NativeInterop;
using System;

public partial class OrganViewUI : TextureRect
{
	private CavitySlot _slot;
	private int _slotSize;

	public void Initialize(CavitySlot slot, int slotSize)
	{
		_slot = slot;
		_slotSize = slotSize;

		Organ organ = _slot.Organ;

		Texture = organ.Texture;
		ExpandMode = ExpandModeEnum.IgnoreSize;

		CustomMinimumSize = new Vector2(organ.Width * _slotSize, organ.Height * _slotSize);
	}

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var preview = new TextureRect
		{
			Texture = this.Texture,
			ExpandMode = ExpandModeEnum.IgnoreSize,
			CustomMinimumSize = this.CustomMinimumSize,
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
}
