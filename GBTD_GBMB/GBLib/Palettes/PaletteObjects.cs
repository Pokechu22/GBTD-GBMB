using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.Tiles;

namespace GB.Shared.Palettes
{
	public interface IPaletteSetBehavior
	{
		IPaletteEntryBehavior EntryBehavior { get; }
		int Height { get; }
	}

	public class GBCPaletteSetBehavior : IPaletteSetBehavior
	{
		private GBCPaletteEntryBehavior behavior = new GBCPaletteEntryBehavior();

		public IPaletteEntryBehavior EntryBehavior { get { return behavior; } }
		public int Height { get { return 8; } }
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
		Color GetFilteredColor(PaletteEntry entry);
	}

	/// <summary>
	/// Modifies properites of the palette.
	/// </summary>
	public class GBCPaletteEntryBehavior : IPaletteEntryBehavior
	{
		public Color GetFilteredColor(PaletteEntry entry) {
			return entry.color;
		}
	}
	
	public struct PaletteSet
	{
		private /*readonly*/ Palette[] palettes;
		private /*readonly*/ IPaletteSetBehavior behaviour;

		public PaletteSet(Palette[] palettes, IPaletteSetBehavior behaviour) {
			this.palettes = palettes;
			this.behaviour = behaviour;
		}

		public Palette[] Rows {
			get {
				if (palettes == null) {
					palettes = new Palette[this.Behaviour.Height];
				}
				return palettes;
			}
			set {
				if (value.Length != this.behaviour.Height) {
					throw new ArgumentOutOfRangeException("value.Length", value.Length, "Length of new array MUST match that of the behaviour's height (" + this.Behaviour.Height + ")");
				}
				palettes = value;
			}
		}

		public IPaletteSetBehavior Behaviour {
			get {
				if (this.behaviour == null) {
					this.behaviour = new GBCPaletteSetBehavior();
				}
				return behaviour;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException();
				}
				behaviour = value; }
		}

		public int NumberOfRows {
			get { return this.Behaviour.Height; }
		}

		public Palette this[int row] {
			get { return Rows[row]; }
		}

		/// <summary>
		/// The default Palette set to use.
		/// </summary>
		public static PaletteSet DefaultPaletteSet {
			get {
				IPaletteSetBehavior behavior = new GBCPaletteSetBehavior();
				Palette[] palettes = new Palette[behavior.Height];
				for (int i = 0; i < palettes.Length; i++) {
					palettes[i] = Palette.DefaultPalette;
				}
				return new PaletteSet(palettes, behavior);
			}
		}
	}

	public struct Palette
	{
		public /*readonly*/ PaletteEntry entry0;
		public /*readonly*/ PaletteEntry entry1;
		public /*readonly*/ PaletteEntry entry2;
		public /*readonly*/ PaletteEntry entry3;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteEntry EntryWhite {
			get { return entry0; }
			set { entry0 = value; }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteEntry EntryLightGray {
			get { return entry1; }
			set { entry1 = value; }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteEntry EntryDarkGray {
			get { return entry2; }
			set { entry2 = value; }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteEntry EntryBlack {
			get { return entry3; }
			set { entry3 = value; }
		}

		public PaletteEntry this[int entryNum] {
			get {
				switch (entryNum) {
				case 0: return entry0;
				case 1: return entry1;
				case 2: return entry2;
				case 3: return entry3;
				default: throw new ArgumentOutOfRangeException("entryNum", entryNum, "Must be between 0 and 3 (inclusive)");
				}
			}
			set {
				switch (entryNum) {
				case 0: entry0 = value; return;
				case 1: entry1 = value; return;
				case 2: entry2 = value; return;
				case 3: entry3 = value; return;
				default: throw new ArgumentOutOfRangeException("entryNum", entryNum, "Must be between 0 and 3 (inclusive)");
				}
			}
		}
		public PaletteEntry this[GBColor color] {
			get {
				switch (color) {
				case GBColor.BLACK: return EntryBlack;
				case GBColor.DARK_GRAY: return EntryDarkGray;
				case GBColor.LIGHT_GRAY: return EntryLightGray;
				case GBColor.WHITE: return EntryWhite;
				default: throw new InvalidEnumArgumentException("color", (int)color, typeof(GBColor));
				}
			}
			set {
				switch (color) {
				case GBColor.BLACK: EntryBlack = value; return;
				case GBColor.DARK_GRAY: EntryDarkGray = value; return;
				case GBColor.LIGHT_GRAY: EntryLightGray = value; return;
				case GBColor.WHITE: EntryWhite = value; return;
				default: throw new InvalidEnumArgumentException("color", (int)color, typeof(GBColor));
				}
			}
		}

		public Palette(PaletteEntry entry0, PaletteEntry entry1, PaletteEntry entry2, PaletteEntry entry3) {
			this.entry0 = entry0;
			this.entry1 = entry1;
			this.entry2 = entry2;
			this.entry3 = entry3;
		}

		/// <summary>
		/// Default-used palette.
		/// </summary>
		public static Palette DefaultPalette {
			get {
				return new Palette(
					new PaletteEntry(0, 0, Color.White, new GBCPaletteEntryBehavior()),
					new PaletteEntry(1, 0, Color.LightGray, new GBCPaletteEntryBehavior()),
					new PaletteEntry(2, 0, Color.DarkGray, new GBCPaletteEntryBehavior()),
					new PaletteEntry(3, 0, Color.Black, new GBCPaletteEntryBehavior()));
			}
		}

		/// <summary>
		/// Disabled-appearence palette.
		/// </summary>
		public static Palette DisabledPalette {
			get {
				return new Palette(
					new PaletteEntry(0, 0, SystemColors.Control, new GBCPaletteEntryBehavior()),
					new PaletteEntry(1, 0, SystemColors.Control, new GBCPaletteEntryBehavior()),
					new PaletteEntry(2, 0, SystemColors.Control, new GBCPaletteEntryBehavior()),
					new PaletteEntry(3, 0, SystemColors.Control, new GBCPaletteEntryBehavior()));
			}
		}
	}

	public struct PaletteEntry
	{
		public readonly int y;
		public readonly int x;
		public /*readonly*/ Color color;

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

		public PaletteEntry(int x, int y, Color color, IPaletteEntryBehavior behavior) {
			this.x = x;
			this.y = y;
			this.color = color;
			this.behavior = behavior;
		}

		public static implicit operator Color(PaletteEntry @this) {
			return @this.DisplayColor;
		}
	}

	/// <summary>
	/// Contains the extension methods needed.
	/// </summary>
	public static class PaletteExtensions
	{
		public static PaletteSet SetEntryColor(this PaletteSet @this, int x, int y, Color color) {
			if (x < 0 || x >= 4) {
				throw new ArgumentOutOfRangeException("x", x, "Must be in range of 0 ≤ x < 4 (the width)");
			}
			if (y < 0 || y >= @this.Behaviour.Height) {
				throw new ArgumentOutOfRangeException("y", y, "Must be in range of 0 ≤ y < " + @this.Behaviour.Height + " (the height)");
			}

			Palette[] palettes = @this.Rows.SetEntryColor(x, y, color);

			return new PaletteSet(palettes, @this.Behaviour);
		}

		public static Palette[] SetEntryColor(this Palette[] @this, int x, int y, Color color) {
			if (x < 0 || x >= 4) {
				throw new ArgumentOutOfRangeException("x", x, "Must be in range of 0 ≤ x < 4 (the width)");
			}
			if (y < 0 || y >= @this.Length) {
				throw new ArgumentOutOfRangeException("y", y, "Must be in range of 0 ≤ y < " + @this.Length + " (the height)");
			}

			Palette[] palettes = (Palette[])@this.Clone();
			palettes[y] = @this[y].SetEntryColor(x, color);

			return palettes;
		}

		public static Palette SetEntryColor(this Palette @this, int x, Color color) {
			switch (x) {
			case 0: return new Palette(@this.entry0.SetColor(color), @this.entry1, @this.entry2, @this.entry3);
			case 1: return new Palette(@this.entry0, @this.entry1.SetColor(color), @this.entry2, @this.entry3);
			case 2: return new Palette(@this.entry0, @this.entry1, @this.entry2.SetColor(color), @this.entry3);
			case 3: return new Palette(@this.entry0, @this.entry1, @this.entry2, @this.entry3.SetColor(color));
			}
			throw new ArgumentOutOfRangeException("x", x, "Must be in range of 0 ≤ x < 4 (the width)");
		}

		public static PaletteEntry SetColor(this PaletteEntry @this, Color color) {
			return new PaletteEntry(@this.x, @this.y, color, @this.behavior);
		}

		private static readonly char[] EntrySplitChars = { '\t', '\r', '\n' };
		private static readonly char[] RowSplitChars = { '\r', '\n' };
		private static readonly char[] SetSplitChars = { '\r', '\n' };

		/// <summary>
		/// Serializes a palette set to a string.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static string PaletteSetToString(this PaletteSet @this) {
			StringBuilder ret = new StringBuilder();

			foreach (Palette e in @this.Rows) {
				ret.Append(e.PaletteToString());
			}

			return ret.ToString();
		}

		/// <summary>
		/// Deserializes a palette set from a string.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value"></param>
		/// <param name="startingIndex">The first line of the string to use.  Most of the time will be left at 0.</param>
		/// <param name="thisStartingIndex">The first row of the set to modify.</param>
		/// <returns>True if sucessful, false if could not be parsed.</returns>
		/// <exception cref="ArgumentException">when format is invalid.</exception>
		public static bool TryStringToPaletteSet(this PaletteSet @this, string value, int startingIndex = 0, int thisStartingIndex = 0) {
			if (thisStartingIndex < 0 || startingIndex < 0) {
				return false;
			}

			String[] split = value.Split(SetSplitChars, StringSplitOptions.RemoveEmptyEntries);

			Palette[] rows = (Palette[])@this.Rows.Clone();
			for (int i = 0; i < @this.NumberOfRows; i++) {
				for (int j = 0; j < 4; j++) {
					if (i >= @this.NumberOfRows - thisStartingIndex) {
						break;
					}
					if ((i * 4) + j + startingIndex >= split.Length) {
						break;
					}
					if (!rows[i + thisStartingIndex][j].TryStringToEntry(split[(i * 4) + j + startingIndex])) {
						return false;
					}
				}
			}
			@this.Rows = rows;
			return true;
		}

		/// <summary>
		/// Deserializes a palette set from a string.  Throws an excpetion if invalid input is used.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value"></param>
		/// <param name="startingIndex">The first line of the string to use.  Most of the time will be left at 0.</param>
		/// <param name="thisStartingIndex">The first row of the set to modify.</param>
		/// <exception cref="ArgumentException">when format is invalid.</exception>
		public static void StringToPaletteSet(this PaletteSet @this, string value, int startingIndex = 0, int thisStartingIndex = 0) {
			if (thisStartingIndex < 0) { throw new ArgumentOutOfRangeException("thisStartingIndex"); }
			if (startingIndex < 0) { throw new ArgumentOutOfRangeException("startingIndex"); }

			String[] split = value.Split(SetSplitChars, StringSplitOptions.RemoveEmptyEntries);

			Palette[] rows = (Palette[])@this.Rows.Clone();
			for (int i = 0; i < @this.NumberOfRows; i++) {
				for (int j = 0; j < 4; j++) {
					if (i >= @this.NumberOfRows - thisStartingIndex) {
						break;
					}
					if ((i * 4) + j + startingIndex >= split.Length) {
						break;
					}
					
					//Call; allow any thown exceptions to continue up.
					rows[i + thisStartingIndex][j].StringToEntry(split[(i * 4) + j + startingIndex]);
				}
			}
			@this.Rows = rows;
		}

		/// <summary>
		/// Serializes a palette row to a string.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static string PaletteToString(this Palette @this) {
			return String.Format("{0}\r\n{1}\r\n{2}\r\n{3}\r\n", @this.entry0.EntryToString(), @this.entry1.EntryToString(), @this.entry2.EntryToString(), @this.entry3.EntryToString());
		}

		/// <summary>
		/// Deserializes a palette row from a string.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value"></param>
		/// <param name="startingIndex">The position to start at in the string, by default 0.</param>
		/// <returns></returns>
		public static bool TryStringToPalette(this Palette @this, string value, int startingIndex = 0) {
			string[] split = value.Split(RowSplitChars, StringSplitOptions.RemoveEmptyEntries);
			if (startingIndex + 4 > split.Length) { return false; }


			PaletteEntry[] entries = new PaletteEntry[4];
			for (int i = 0; i < 4; i++) {
				entries[i] = @this[i];
				if (!entries[i].TryStringToEntry(split[startingIndex + i])) {
					return false;
				}
			}
			for (int i = 0; i < 4; i++) {
				@this[i] = entries[i];
			}

			return true;
		}

		/// <summary>
		/// Deserializes a palette row from a string.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value"></param>
		/// <param name="startingIndex">The position to start at in the string, by default 0.</param>
		/// <exception cref="ArgumentException">When the value cannot be parsed.</exception>
		public static void StringToPalette(this Palette @this, string value, int startingIndex = 0) {
			string[] split = value.Split(RowSplitChars, StringSplitOptions.RemoveEmptyEntries);
			if (startingIndex + 4 > split.Length) { throw new ArgumentException("Not enough room for the remaining entries!", "value"); }

			PaletteEntry[] entries = new PaletteEntry[4];
			for (int i = 0; i < 4; i++) {
				entries[i] = @this[i];

				try {
					entries[i].StringToEntry(split[startingIndex + i]);
				} catch (ArgumentException) {
					throw;
					//Probably could leave this alone, but it looks cleaner.
				}
			}
			for (int i = 0; i < 4; i++) {
				@this[i] = entries[i];
			}
		}

		/// <summary>
		/// Serializes an entry to a string.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static string EntryToString(this PaletteEntry @this) {
			return String.Format("{0}\t{1}\t{2}", @this.color.R >> 3, @this.color.G >> 3, @this.color.B >> 3);
		}

		/// <summary>
		/// Deserializes an entry from a string.  Throws an exception if invalid format.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value"></param>
		/// <exception cref="ArgumentException">When given an invalid fromat.</exception>
		public static void StringToEntry(this PaletteEntry @this, string value) {
			string[] split = value.Split(EntrySplitChars, StringSplitOptions.RemoveEmptyEntries);

			if (split.Length != 3) { throw new ArgumentException("Too many tabs in input string, expected 2, got " + value, "value"); }

			int r, g, b;

			if (!Int32.TryParse(split[0], out r)) { throw new ArgumentException("Could not parse r value: expected int, got " + split[0], "value"); }
			if (!Int32.TryParse(split[1], out g)) { throw new ArgumentException("Could not parse g value: expected int, got " + split[1], "value"); }
			if (!Int32.TryParse(split[2], out b)) { throw new ArgumentException("Could not parse b value: expected int, got " + split[2], "value"); }

			r <<= 3;
			g <<= 3;
			b <<= 3;
			
			if (r < 0 || r > 255) { throw new ArgumentException("R value out of bounds: less than 0 or more than 32: got " + (r >> 3), "value"); }
			if (g < 0 || g > 255) { throw new ArgumentException("G value out of bounds: less than 0 or more than 32: got " + (g >> 3), "value"); }
			if (r < 0 || r > 255) { throw new ArgumentException("B value out of bounds: less than 0 or more than 32: got " + (b >> 3), "value"); }

			@this.color = Color.FromArgb(r, g, b);
		}

		/// <summary>
		/// Deserializes an entry from a string.
		/// </summary>
		/// <param name="this"></param>
		/// <param name="value"></param>
		/// <returns>True if valid format, false otherwise.  If false is returned, no changes are made.</returns>
		public static bool TryStringToEntry(this PaletteEntry @this, string value) {
			string[] split = value.Split(EntrySplitChars, StringSplitOptions.RemoveEmptyEntries);

			if (split.Length != 3) { return false; }

			int r, g, b;

			if (!Int32.TryParse(split[0], out r)) { return false; }
			if (!Int32.TryParse(split[1], out g)) { return false; }
			if (!Int32.TryParse(split[2], out b)) { return false; }

			r <<= 3;
			g <<= 3;
			b <<= 3;

			if (r < 0 || r > 255) { return false; }
			if (g < 0 || g > 255) { return false; }
			if (r < 0 || r > 255) { return false; }

			@this.color = Color.FromArgb(r, g, b);
			return true;
		}
	}
}
