using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using GB.Shared.Palettes;
using System.Drawing;

namespace GB.Shared.Tiles
{
	public struct TileData
	{
		public PaletteData paletteData;
		public Tile tile;
		public UInt16 GBC_Palette;
		public UInt16 SGB_Palette;

		public UInt16 Width { get { return tile.Width; } }
		public UInt16 Height { get { return tile.Height; } }

		/// <summary>
		/// Gets the Palette used with the given ColorSet.
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		public Palette_ GetPalette(ColorSet set) {
			switch (set) {
			case ColorSet.GAMEBOY: return new Palette_();
			case ColorSet.GAMEBOY_POCKET: return new Palette_();
			case ColorSet.GAMEBOY_COLOR: return paletteData.GBCPaletteSet[GBC_Palette];
			case ColorSet.SUPER_GAMEBOY: return paletteData.GBCPaletteSet[SGB_Palette];
			case ColorSet.GAMEBOY_COLOR_FILTERED: return paletteData.GBCPaletteSet[GBC_Palette];
			default: throw new InvalidEnumArgumentException("set", (int)set, typeof(ColorSet));
			}
		}

		/// <summary>
		/// Sets the Palette used with the given ColorSet.
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		public void SetPalette(ColorSet set, Palette_ value) {
			switch (set) {
			case ColorSet.GAMEBOY: return;
			case ColorSet.GAMEBOY_POCKET: return;
			case ColorSet.GAMEBOY_COLOR: paletteData.GBCPaletteSet[GBC_Palette] = value; return;
			case ColorSet.SUPER_GAMEBOY: paletteData.GBCPaletteSet[SGB_Palette] = value; return;
			case ColorSet.GAMEBOY_COLOR_FILTERED: paletteData.GBCPaletteSet[GBC_Palette] = value; return;
			default: throw new InvalidEnumArgumentException("set", (int)set, typeof(ColorSet));
			}
		}

		/// <summary>
		/// Gets the current paletteid with the given ColorSet.  If there is no palette row for the specified ColorSet, 0 is returned.
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		public int GetRow(ColorSet set) {
			switch (set) {
			case ColorSet.GAMEBOY: return 0;
			case ColorSet.GAMEBOY_POCKET: return 0;
			case ColorSet.GAMEBOY_COLOR: return GBC_Palette;
			case ColorSet.SUPER_GAMEBOY: return SGB_Palette;
			case ColorSet.GAMEBOY_COLOR_FILTERED: return GBC_Palette;
			default: throw new InvalidEnumArgumentException("set", (int)set, typeof(ColorSet));
			}
		}

		/// <summary>
		/// Sets the current paletteid for the given ColorSet.  If there is no palette row for the specified ColorSet, nothing is done.
		/// </summary>
		/// <param name="set"></param>
		/// <returns></returns>
		public void SetRow(ColorSet set, UInt16 value) {
			switch (set) {
			case ColorSet.GAMEBOY: return;
			case ColorSet.GAMEBOY_POCKET: return;
			case ColorSet.GAMEBOY_COLOR: GBC_Palette = value; return;
			case ColorSet.SUPER_GAMEBOY: SGB_Palette = value; return;
			case ColorSet.GAMEBOY_COLOR_FILTERED: GBC_Palette = value; return;
			default: throw new InvalidEnumArgumentException("set", (int)set, typeof(ColorSet));
			}
		}
	}
}
