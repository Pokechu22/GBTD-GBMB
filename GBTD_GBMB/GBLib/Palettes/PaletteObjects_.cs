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
		public PaletteData() {
			this.SGBPaletteSet = new PaletteSet_(4);
			this.GBCPaletteSet = new PaletteSet_(8);
		}

		public readonly PaletteSet_ SGBPaletteSet;
		public readonly PaletteSet_ GBCPaletteSet;

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
				return this.GBCPaletteSet[row][(int)color];
			case ColorSet.SUPER_GAMEBOY:
				return this.SGBPaletteSet[row][(int)color];
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				return this.GBCPaletteSet[row][(int)color].FilterWithGBC();
			default:
				throw new InvalidEnumArgumentException("paletteData", (int)set, typeof(ColorSet));
			}
		}

		/// <summary>
		/// Gets the PaletteSet relevant to the specified color.
		/// </summary>
		/// <param name="paletteData"></param>
		/// <returns>The paletteData, or null if the paletteData isn't correct for this use.</returns>
		public PaletteSet_ GetPaletteSet(ColorSet set) {
			switch (set) {
			case ColorSet.GAMEBOY_POCKET:
				return new PaletteSet_(1);
			case ColorSet.GAMEBOY:
				return new PaletteSet_(1);
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

	public class PaletteSet_
	{
		/// <summary>
		/// Creates a paletteset of the specified size.
		/// </summary>
		/// <param name="size"></param>
		public PaletteSet_(UInt16 size) {
			this.palettes = new Palette_[size];
			for (int i = 0; i < size; i++) {
				palettes[i] = new Palette_();
			}
		}

		private Palette_[] palettes;

		public UInt16 Size { get { return (UInt16)palettes.Length; } }

		public Palette_ this[int index] {
			get {
				return palettes[index];
			}
			set {
				palettes[index] = value;
			}
		}
	}

	public class Palette_
	{
		public Palette_() {
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
	}
}
