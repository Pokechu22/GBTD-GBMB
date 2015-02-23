using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.Palettes;

namespace GB.Shared.Tiles
{
	public class TileDataList
	{
		private TileData[] tiles;

		public UInt16 Width { get; private set; }
		public UInt16 Height { get; private set; }

		/// <summary>
		/// The palette paletteData shared between all tile datas.
		/// </summary>
		private PaletteData sharedPalette = new PaletteData();

		/// <summary>
		/// The number of tiles represented.  Can be paletteData.
		/// If paletteData, resises the tile list using <see cref="Array.Resize"/>.
		/// </summary>
		public int Length {
			get { return tiles.Length; }
			set { Array.Resize(ref tiles, value); }
		}

		public TileData[] Tiles {
			get { return tiles; }
			set { if (value == null) { throw new ArgumentNullException(); } tiles = value; }
		}

		public PaletteData Palette {
			get { return sharedPalette; }
			set {
				sharedPalette = value;
				for (int i = 0; i < tiles.Length; i++) {
					tiles[i].paletteData = sharedPalette;
				}
			}
		}

		/// <summary>
		/// Creates a TileDataList with empty default values.
		/// </summary>
		/// <param name="length"></param>
		public TileDataList(int length, UInt16 Width, UInt16 Height) {
			this.Width = Width;
			this.Height = Height;

			tiles = new TileData[length];
			for (int i = 0; i < length; i++) {
				tiles[i] = new TileData { GBC_Palette = 0, SGB_Palette = 0, paletteData = new PaletteData(), tile = new Tile(Width, Height) };
			}
		}
	}
}
