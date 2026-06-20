using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

#pragma warning disable CS0649

[GlobalClass]
public partial class Cavity : Resource
{
	[Signal]
	public delegate void OrgansChangedEventHandler();

	/// <summary>
	/// O for available position,
	/// X (or anything else) for not available position.
	/// If line is cut off early, it will
	/// consider the rest of it not available.
	/// </summary>
	[Export(PropertyHint.MultilineText)]
	public string CavityShapeString
	{
		get => _cavityShapeString; 
		set
		{
			_cavityShapeString = value;
			if (string.IsNullOrEmpty(_cavityShapeString)) return;
			(Width, Height) = ShapeStringHelper.GetDimensions(value);
			//GD.Print($"Cavity width: {Width}, height: {Height}");
			Shape = ShapeStringHelper.ParseShape(value, Width, Height);
			_slots = new OrganSlot[Width, Height];
		}
	}

	public int Width, Height;
	public bool[,] Shape;

	private string _cavityShapeString;

	private OrganSlot[,] _slots;
	private Dictionary<OrganSlot, Vector2I> _slotOrigins = new();

	public bool CanChangeSlotPosition(OrganSlot slot, Vector2I origin)
	{
		Organ organ = slot.Organ;

		if (origin.X < 0 || origin.Y < 0 || Width < origin.X + organ.Width || Height < origin.Y + organ.Height)
		{
			return false;
		}

		for (int x=0; x<organ.Width; ++x) for (int y=0; y<organ.Height; ++y) 
		{
			if (organ.Shape[x, y])
			{
				if (!(_slots[origin.X + x, origin.Y + y] == slot) && 
				   	(!Shape[origin.X + x, origin.Y + y] || _slots[origin.X + x, origin.Y + y] != null)) return false;
			}
		}

		return true;
	}

	public bool CanPlaceOrgan(Organ organ, Vector2I origin)
	{
		if (origin.X < 0 || origin.Y < 0 || Width < origin.X + organ.Width || Height < origin.Y + organ.Height)
		{
			return false;
		}

		for (int x=0; x<organ.Width; ++x) for (int y=0; y<organ.Height; ++y) 
		{
			if (organ.Shape[x, y])
			{
				if (!Shape[origin.X + x, origin.Y + y] || _slots[origin.X + x, origin.Y + y] != null ) return false;
			}
		}

		return true;
	}

	public bool TryPlaceOrgan(Organ organ, Vector2I origin)
	{
		if (!CanPlaceOrgan(organ, origin)) return false;
		OrganSlot slot = new OrganSlot(organ);
		return TryPlaceSlot(slot, origin);
	}

	public bool TryPlaceSlot(OrganSlot slot, Vector2I origin)
	{
		Organ organ = slot.Organ;
		_slotOrigins.Add(slot, origin);

		for (int x=0; x<organ.Width; ++x) for (int y=0; y<organ.Height; ++y) 
		{
			if (organ.Shape[x, y])
			{
				_slots[origin.X + x, origin.Y + y] = slot;
			}
		}

		EmitSignal(SignalName.OrgansChanged);
		
		return true;
	}


	public void RemoveSlot(OrganSlot slot)
	{
		Vector2I origin = _slotOrigins[slot];
		Organ organ = slot.Organ;

		_slotOrigins.Remove(slot);

		for (int x=0; x<organ.Width; ++x) for (int y=0; y<organ.Height; ++y) 
		{
			if (organ.Shape[x, y])
			{
				_slots[origin.X + x, origin.Y + y] = null;
			}
		}
		
		EmitSignal(SignalName.OrgansChanged);
	}

	public List<Organ> GetOrgans() => _slotOrigins.Keys.Select(x => x.Organ).ToList();
	public List<KeyValuePair<OrganSlot, Vector2I>> GetItems() => _slotOrigins.ToList();
}

#pragma warning restore CS0649