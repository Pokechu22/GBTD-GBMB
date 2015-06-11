using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GB.Shared.Tiles;
using System.ComponentModel;

namespace GB.Shared.Palettes
{
	public class PaletteData
	{
		/// <summary>
		/// Creates a new PaletteData with the specified appearence.
		/// </summary>
		/// <param name="SGBPaletteSet"></param>
		/// <param name="GBCPaletteSet"></param>
		public PaletteData(PaletteSet SGBPaletteSet, PaletteSet GBCPaletteSet) {
			if (SGBPaletteSet == null) { throw new ArgumentNullException("SGBPaletteSet"); }
			if (GBCPaletteSet == null) { throw new ArgumentNullException("GBCPaletteSet"); }

			this.SGBPaletteSet = SGBPaletteSet;
			this.GBCPaletteSet = GBCPaletteSet;
		}

		/// <summary>
		/// Creates a paletteset wtih
		/// </summary>
		public PaletteData() {
			this.SGBPaletteSet = new PaletteSet(4);
			this.GBCPaletteSet = new PaletteSet(8);
		}

		public readonly PaletteSet SGBPaletteSet;
		public readonly PaletteSet GBCPaletteSet;

		/// <summary>
		/// Gets the color that would be used in the specified paletteData and applies filtration as needed.
		/// </summary>
		/// <param name="paletteData">This should be an enum, unfrotunately.</param>
		/// <param name="row"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		public Color GetColor(ColorSet set, UInt16 row, GBColor color) {
			switch (set) {
			case ColorSet.GAMEBOY_POCKET:
				return color.GetPocketColor();
			case ColorSet.GAMEBOY:
				return color.GetNormalColor();
			case ColorSet.GAMEBOY_COLOR:
				return this.GBCPaletteSet[row][color];
			case ColorSet.SUPER_GAMEBOY:
				return this.SGBPaletteSet[row][color];
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				return this.GBCPaletteSet[row][color].FilterWithGBC();
			default:
				throw new InvalidEnumArgumentException("paletteData", (int)set, typeof(ColorSet));
			}
		}

		/// <summary>
		/// Gets the PaletteSet relevant to the specified color.
		/// </summary>
		/// <param name="paletteData"></param>
		/// <returns>The paletteData, or null if the paletteData isn't correct for this use.</returns>
		public PaletteSet GetPaletteSet(ColorSet set) {
			switch (set) {
			case ColorSet.GAMEBOY_POCKET:
				return new PaletteSet(1);
			case ColorSet.GAMEBOY:
				return new PaletteSet(1);
			case ColorSet.GAMEBOY_COLOR:
				return GBCPaletteSet;
			case ColorSet.SUPER_GAMEBOY:
				return SGBPaletteSet;
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				return GBCPaletteSet;
			default:
				throw new InvalidEnumArgumentException("paletteData", (int)set, typeof(ColorSet));
			}
		}
	}

	/// <summary>
	/// Represents a series of Palettes.
	/// </summary>
	public class PaletteSet : ICloneable
	{
		/// <summary>
		/// Creates a paletteset with the specified palettes.
		/// </summary>
		/// <param name="palettes">The palettes to use.</param>
		public PaletteSet(params Palette[] palettes) {
			if (palettes == null) { throw new ArgumentNullException("palettes"); }

			this.palettes = new Palette[palettes.Length];
			for (int i = 0; i < palettes.Length; i++) {
				this.palettes[i] = (Palette)palettes[i].Clone();
			}
		}

		/// <summary>
		/// Creates a PaletteSet of the specified size.
		/// </summary>
		/// <param name="size"></param>
		public PaletteSet(UInt16 size) {
			this.palettes = new Palette[size];
			for (int i = 0; i < size; i++) {
				palettes[i] = new Palette();
			}
		}

		private Palette[] palettes;

		public UInt16 Size { get { return (UInt16)palettes.Length; } }

		public Palette this[int index] {
			get {
				return palettes[index];
			}
			set {
				palettes[index] = value;
			}
		}

		public Palette this[uint index] {
			get {
				return palettes[index];
			}
			set {
				palettes[index] = value;
			}
		}

		public object Clone() {
			return new PaletteSet(this.palettes);
		}
	}

	/// <summary>
	/// Represents a single 4-color palette.
	/// </summary>
	public class Palette : ICloneable
	{
		/// <summary>
		/// Creates a palette with the specified colors.
		/// </summary>
		public Palette(Color Color0, Color Color1, Color Color2, Color Color3) {
			this.Color0 = Color0;
			this.Color1 = Color1;
			this.Color2 = Color2;
			this.Color3 = Color3;
		}

		/// <summary>
		/// Creates a palette with the specified colors.
		/// </summary>
		/// <param name="Colors">An array, of length 4, containin each color.</param>
		public Palette(Color[] Colors) {
			if (Colors == null) { throw new ArgumentNullException("Colors"); }
			if (Colors.Length != 4) { throw new ArgumentException("Colors MUST have a length of 4 (got " + Colors.Length + ")", "Colors"); }

			this.Color0 = Colors[0];
			this.Color1 = Colors[1];
			this.Color2 = Colors[2];
			this.Color3 = Colors[3];
		}

		/// <summary>
		/// Created a palette with the default colors.
		/// </summary>
		public Palette() {
			this.Color0 = Color.White;
			this.Color1 = Color.LightGray;
			this.Color2 = Color.Gray;
			this.Color3 = Color.Black;
		}

		public Color Color0 { get; set; }
		public Color Color1 { get; set; }
		public Color Color2 { get; set; }
		public Color Color3 { get; set; }

		public Color this[int index] {
			get {
				switch (index) {
				case 0: return Color0;
				case 1: return Color1;
				case 2: return Color2;
				case 3: return Color3;
				default: throw new ArgumentOutOfRangeException("index", "Index must be between 0 and 3 (inclusive)!");
				}
			}
			set {
				switch (index) {
				case 0: Color0 = value; break;
				case 1: Color1 = value; break;
				case 2: Color2 = value; break;
				case 3: Color3 = value; break;
				default: throw new ArgumentOutOfRangeException("index", "Index must be between 0 and 3 (inclusive)!");
				}
			}
		}

		public Color this[uint index] {
			get {
				switch (index) {
				case 0: return Color0;
				case 1: return Color1;
				case 2: return Color2;
				case 3: return Color3;
				default: throw new ArgumentOutOfRangeException("index", "Index must be between 0 and 3 (inclusive)!");
				}
			}
			set {
				switch (index) {
				case 0: Color0 = value; break;
				case 1: Color1 = value; break;
				case 2: Color2 = value; break;
				case 3: Color3 = value; break;
				default: throw new ArgumentOutOfRangeException("index", "Index must be between 0 and 3 (inclusive)!");
				}
			}
		}

		public Color this[GBColor color] {
			get {
				switch (color) {
				case GBColor.WHITE: return Color0;
				case GBColor.LIGHT_GRAY: return Color1;
				case GBColor.DARK_GRAY: return Color2;
				case GBColor.BLACK: return Color3;
				default: throw new InvalidEnumArgumentException("color", (int)color, typeof(GBColor));
				}
			}
			set {
				switch (color) {
				case GBColor.WHITE: Color0 = value; break;
				case GBColor.LIGHT_GRAY: Color1 = value; break;
				case GBColor.DARK_GRAY: Color2 = value; break;
				case GBColor.BLACK: Color3 = value; break;
				default: throw new InvalidEnumArgumentException("color", (int)color, typeof(GBColor));
				}
			}
		}

		public object Clone() {
			return new Palette(this.Color0, this.Color1, this.Color2, this.Color3);
		}
	}
}
