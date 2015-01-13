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
		public PaletteSet set;
		public int paletteID;
		public Tile tile;

		/// <summary>
		/// The palette used for this tileData.
		/// </summary>
		public Palette Palette {
			get { return set.Rows[paletteID]; }
			set { set.Rows[paletteID] = value; }
		}

		/// <summary>
		/// The first palette entry. 
		/// </summary>
		public PaletteEntry Entry0 {
			get { return set.Rows[paletteID].entry0; }
			set { this.set = set.SetEntryColor(0, paletteID, value.color); }
		}

		/// <summary>
		/// The first palette entry. 
		/// </summary>
		public PaletteEntry Entry1 {
			get { return set.Rows[paletteID].entry1; }
			set { this.set = set.SetEntryColor(1, paletteID, value.color); }
		}

		/// <summary>
		/// The first palette entry. 
		/// </summary>
		public PaletteEntry Entry2 {
			get { return set.Rows[paletteID].entry2; }
			set { this.set = set.SetEntryColor(2, paletteID, value.color); }
		}

		/// <summary>
		/// The first palette entry. 
		/// </summary>
		public PaletteEntry Entry3 {
			get { return set.Rows[paletteID].entry3; }
			set { this.set = set.SetEntryColor(3, paletteID, value.color); }
		}

		public void setEntryColor(int entryNum, Color color) {
			setEntryColor(entryNum, paletteID, color);
		}

		public void setEntryColor(int entryNum, int paletteNum, Color color) {
			this.set = set.SetEntryColor(entryNum, paletteNum, color);
		}
	}
}
