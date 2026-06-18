using Godot;
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
}
