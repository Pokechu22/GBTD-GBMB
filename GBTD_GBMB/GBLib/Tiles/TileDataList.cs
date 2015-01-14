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

		/// <summary>
		/// The palette set shared between all tile datas.
		/// </summary>
		private PaletteSet sharedPallete = PaletteSet.DefaultPaletteSet;

		/// <summary>
		/// The number of tiles represented.  Can be set.
		/// If set, resises the tile list using <see cref="Array.Resize"/>.
		/// </summary>
		public int Length {
			get { return tiles.Length; }
			set { Array.Resize(ref tiles, value); }
		}

		public TileData[] Tiles {
			get { return tiles; }
			set { if (value == null) { throw new ArgumentNullException(); } tiles = value; }
		}

		public PaletteSet Palette {
			get { return sharedPallete; }
			set {
				sharedPallete = value;
				for (int i = 0; i < tiles.Length; i++) {
					tiles[i].set = sharedPallete;
				}
			}
		}

		/// <summary>
		/// Creates a TileDataList with empty default values.
		/// </summary>
		/// <param name="length"></param>
		public TileDataList(int length) {
			tiles = new TileData[length];
			for (int i = 0; i < length; i++) {
				tiles[i] = new TileData { paletteID = 0, set = PaletteSet.DefaultPaletteSet, tile = new Tile() };
			}
		}
	}
}
