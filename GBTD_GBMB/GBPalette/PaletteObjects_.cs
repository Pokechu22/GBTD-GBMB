using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.Tile;

namespace GB.Shared.Palette
{
	public interface IPaletteSetBehavior
	{
		IPaletteEntryBehavior EntryBehavior { get; }
		int Width { get; }
		int Height { get; }
	}

	/// <summary>
	/// Modifies properites of the palette.
	/// </summary>
	public interface IPaletteEntryBehavior
	{
		/// <summary>
		/// Gets the color used with filters applied.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		Color GetFilteredColor(PaletteEntry_ entry);
	}
	
	public struct PaletteSet_
	{
		public readonly Palette_[] palettes;
		public readonly IPaletteSetBehavior behaviour;
	}

	public struct Palette_
	{
		public readonly PaletteEntry_ entry0;
		public readonly PaletteEntry_ entry1;
		public readonly PaletteEntry_ entry2;
		public readonly PaletteEntry_ entry3;

		public PaletteEntry_ this[int entryNum] {
			get {
				switch (entryNum) {
				case 0: return entry0;
				case 1: return entry1;
				case 2: return entry2;
				case 3: return entry3;
				default: throw new ArgumentOutOfRangeException("entryNum", entryNum, "Must be between 0 and 3 (inclusive)");
				}
			}
		}
	}

	public struct PaletteEntry_
	{
		public readonly int y;
		public readonly int x;
		public readonly Color color;

		public readonly IPaletteEntryBehavior transform;

		public Color DisplayColor {
			get {
				if (transform != null) {
					return transform.GetFilteredColor(this);
				} else {
					return this.color;
				}
			}
		}
	}
}
