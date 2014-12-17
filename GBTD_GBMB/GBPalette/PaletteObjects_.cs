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

		public int NumberOfRows { get { return behaviour.Height; } }

		public Palette_ this[int row] {
			get { return palettes[row]; }
		}

		public PaletteSet_(Palette_[] palettes, IPaletteSetBehavior behaviour) {
			this.palettes = palettes;
			this.behaviour = behaviour;
		}
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

		public Palette_(PaletteEntry_ entry0, PaletteEntry_ entry1, PaletteEntry_ entry2, PaletteEntry_ entry3) {
			this.entry0 = entry0;
			this.entry1 = entry1;
			this.entry2 = entry2;
			this.entry3 = entry3;
		}
	}

	public struct PaletteEntry_
	{
		public readonly int y;
		public readonly int x;
		public readonly Color color;

		public readonly IPaletteEntryBehavior behavior;

		public Color DisplayColor {
			get {
				if (behavior != null) {
					return behavior.GetFilteredColor(this);
				} else {
					return this.color;
				}
			}
		}

		public PaletteEntry_(int x, int y, Color color, IPaletteEntryBehavior behavior) {
			this.x = x;
			this.y = y;
			this.color = color;
			this.behavior = behavior;
		}

		public static implicit operator Color(PaletteEntry_ @this) {
			return @this.color;
		}
	}

	/// <summary>
	/// Contains the extension methods needed.
	/// </summary>
	public static class PaletteExtensions
	{
		public static PaletteSet_ SePaletteEntry_Color(this PaletteSet_ @this, int x, int y, Color color) {
			if (x < 0 || x >= 4) {
				throw new ArgumentOutOfRangeException("x", x, "Must be in range of 0 ≤ x < 4 (the width)");
			}
			if (y < 0 || y >= @this.behaviour.Height) {
				throw new ArgumentOutOfRangeException("y", y, "Must be in range of 0 ≤ y < " + @this.behaviour.Height + " (the height)");
			}

			Palette_[] palettes = @this.palettes.SePaletteEntry_Color(x, y, color);

			return new PaletteSet_(palettes, @this.behaviour);
		}

		public static Palette_[] SePaletteEntry_Color(this Palette_[] @this, int x, int y, Color color) {
			if (x < 0 || x >= 4) {
				throw new ArgumentOutOfRangeException("x", x, "Must be in range of 0 ≤ x < 4 (the width)");
			}
			if (y < 0 || y >= @this.Length) {
				throw new ArgumentOutOfRangeException("y", y, "Must be in range of 0 ≤ y < " + @this.Length + " (the height)");
			}

			Palette_[] palettes = (Palette_[])@this.Clone();
			palettes[y] = @this[y].SePaletteEntry_Color(x, color);

			return palettes;
		}

		public static Palette_ SePaletteEntry_Color(this Palette_ @this, int x, Color color) {
			switch (x) {
			case 0: return new Palette_(@this.entry0.SetColor(color), @this.entry1, @this.entry2, @this.entry3);
			case 1: return new Palette_(@this.entry0, @this.entry1.SetColor(color), @this.entry2, @this.entry3);
			case 2: return new Palette_(@this.entry0, @this.entry1, @this.entry2.SetColor(color), @this.entry3);
			case 3: return new Palette_(@this.entry0, @this.entry1, @this.entry2, @this.entry3.SetColor(color));
			}
			throw new ArgumentOutOfRangeException("x", x, "Must be in range of 0 ≤ x < 4 (the width)");
		}

		public static PaletteEntry_ SetColor(this PaletteEntry_ @this, Color color) {
			return new PaletteEntry_(@this.x, @this.y, color, @this.behavior);
		}
	}
}
