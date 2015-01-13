using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using GB.Shared.Palettes;

namespace GB.Shared.Tiles
{
	public struct TileData
	{
		public PaletteSet set;
		public int paletteID;
		public Tile tile;

		/// <summary>
		/// The palette used for this tileData.
		/// </summary>
		public Palette Palette {
			get { return set.Rows[paletteID]; }
		}
	}
}
