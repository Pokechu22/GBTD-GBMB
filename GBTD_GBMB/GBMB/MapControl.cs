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
using System.Drawing;
using GB.Shared.Tiles;

namespace GB.GBMB
{
	public class MapControl : Control
	{
		/// <summary>
		/// The zoom used.  TODO: Enum?
		/// </summary>
		[Category("Map display"), Description("The zoom to use for the map.")]
		[DefaultValue(2f)]
		public float Zoom { get; set; }

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
			DoubleBuffered = true;

			Zoom = 2f;
			PaletteData = new PaletteData();
		}

		protected void OnMapChanged() {
			this.Invalidate(true);
		}

		protected override void OnPaint(PaintEventArgs e) {
			if (map != null && tileset != null) {
				for (int y = 0; y < map.Master.Height; y++) {
					for (int x = 0; x < map.Master.Width; x++) {
						DrawTile(e.Graphics, map.Tiles[x, y], x, y);
					}
				}
			}

			base.OnPaint(e);
		}

		private void DrawTile(Graphics g, GBMObjectMapTileDataRecord tile, int tileX, int tileY) {
			Tile t = tileset.tiles[tile.TileNumber];

			for (int y = 0; y < t.Height; y++) {
				for (int x = 0; x < t.Width; x++) {
					DrawPixel((tileX * t.Width) + x, (tileY * t.Height) + y, PaletteData.GetColor(this.ColorSet, 0, t[x, y]), g);
				}
			}
		}

		/// <summary>
		/// Draws a pixel.
		/// </summary>
		/// <param name="x">The x-position of the pixel in all pixels.</param>
		/// <param name="y">The y-position of the pixel in all pixels.</param>
		private void DrawPixel(int x, int y, Color color, Graphics g) {
			using (Brush brush = new SolidBrush(color)) {
				g.FillRectangle(brush, x * Zoom, y * Zoom, Zoom, Zoom);
			}
		}
	}
}
