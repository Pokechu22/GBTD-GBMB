using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using GB.Shared.Palettes;
using GB.Shared.GBMFile;

namespace GB.GBMB
{
	public class MapControl : Control
	{
		[Category("Map data"), Description("The width of the Map.")]
		public UInt32 MapWidth { get; set; }
		[Category("Map data"), Description("The height of the Map.")]
		public UInt32 MapHeight { get; set; }

		/// <summary>
		/// The zoom used.  TODO: Enum?
		/// </summary>
		[Category("Map display"), Description("The zoom to use for the map.")]
		public UInt16 Zoom { get; set; }

		[Category("Map data"), Description("The color set to use for the map.")]
		public ColorSet ColorSet { get; set; }
		[Category("Map data"), Description("The palette set to use for the map.")]
		public PaletteSet PaletteSet { get; set; }

		[Category("Map data"), Description("The map to use.")]
		public GBMObjectMapTileData Map { get; set; }
	}
}
