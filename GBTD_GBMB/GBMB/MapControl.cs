using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using GB.Shared.Palettes;
using GB.Shared.GBMFile;
using GB.Shared.Controls;
using GB.Shared.GBRFile;

namespace GB.GBMB
{
	public class MapControl : Control
	{
		/// <summary>
		/// The zoom used.  TODO: Enum?
		/// </summary>
		[Category("Map display"), Description("The zoom to use for the map.")]
		[DefaultValue(2)]
		public UInt16 Zoom { get; set; }

		[Category("Map data"), Description("The color set to use for the map.")]
		public ColorSet ColorSet { get; set; }
		[Category("Map data"), Description("The palette set to use for the map.")]
		public PaletteData PaletteData { get; set; }

		private GBRObjectTileData tileset;
		private GBMObjectMapTileData map;

		[Category("Map data"), Description("The map to use.")]
		public GBMObjectMapTileData Map {
			get { return map; }
			set { map = value; OnMapChanged(); }
		}
		[Category("Map data"), Description("The tileset to use.")]
		public GBRObjectTileData TileSet {
			get { return tileset; }
			set { tileset = value; OnMapChanged(); }
		}

		public MapControl() {
			Zoom = 4;
			PaletteData = new PaletteData();
		}

		/// <summary>
		/// Call when the entirety of the map has been changed.
		/// </summary>
		protected void OnMapChanged() {
			this.Controls.Clear();

			if (map == null || tileset == null) {
				return;
			}

			this.SuspendLayout();

			int yPos = 0;
			for (int y = 0; y < map.Master.Height; y++) {
				int yDelta = 0;
				int xPos = 0;

				for (int x = 0; x < map.Master.Width; x++) {
					TileRenderer renderer = new TileRenderer();
					renderer.PaletteData = new PaletteData();

					renderer.Tile = tileset.tiles[map.Tiles[x, y].TileNumber];
					renderer.Name = "x" + x + "y" + y;
					renderer.PixelScale = Zoom;

					renderer.Border = false;

					renderer.Location = new System.Drawing.Point(xPos, yPos);

					this.Controls.Add(renderer);

					xPos += renderer.Width;
					yDelta = renderer.Height;
				}

				yPos += yDelta;
			}

			this.ResumeLayout();
		}
	}
}
