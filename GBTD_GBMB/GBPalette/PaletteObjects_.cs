using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.Tile;

namespace GB.Shared.Palette
{
	/// <summary>
	/// Modifies properites of the palette.
	/// </summary>
	public interface IPaletteTransform
	{
		/// <summary>
		/// Gets the color used with filters applied.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		Color GetFilteredColor(PaletteEntry_ entry);
	}

	public sealed class NoChangePaletteTransform : IPaletteTransform
	{
		public Color GetFilteredColor(PaletteEntry_ entry) {
			return entry.color;
		}
	}
	
	public struct PaletteSet_
	{
		public Palette_[] palettes;
	}

	public struct Palette_
	{
		public readonly PaletteEntry_ entry0;
		public readonly PaletteEntry_ entry1;
		public readonly PaletteEntry_ entry2;
		public readonly PaletteEntry_ entry3;
	}

	public struct PaletteEntry_
	{
		public readonly int? y;
		public readonly int x;
		public readonly Color color;

		public readonly IPaletteTransform transform;

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
