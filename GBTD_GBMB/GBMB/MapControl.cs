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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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

			Zoom = 4f;
			PaletteData = new PaletteData();
		}

		protected void OnMapChanged() {
			this.Invalidate(true);
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; //Fixes lines in the middle issue.

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
			RectangleF rect = new RectangleF(
				tileX * t.Width * Zoom,
				tileY * t.Height * Zoom,
				t.Width * Zoom,
				t.Height * Zoom);

			using (Bitmap bmp = MakeTileBitmap(t, Color.White, Color.LightGray, Color.DarkGray, Color.Black)) {
				g.DrawImage(bmp, rect);
			}
		}

		/// <summary>
		/// Fast creation of a bitmap for tiles, using marshaling and stuff.
		/// </summary>
		/// <param name="tile"></param>
		/// <param name="white"></param>
		/// <param name="lightGray"></param>
		/// <param name="darkGray"></param>
		/// <param name="black"></param>
		/// <returns></returns>
		private Bitmap MakeTileBitmap(Tile tile, Color white, Color lightGray, Color darkGray, Color black) {
			Bitmap output = new Bitmap(tile.Width, tile.Height);
			int width = tile.Width;
			int height = tile.Height;

			int whiteARGB = white.ToArgb();
			int lightGrayARGB = lightGray.ToArgb();
			int darkGrayARGB = darkGray.ToArgb();
			int blackARGB = black.ToArgb();

			BitmapData outputData = output.LockBits(new Rectangle(0, 0, width, height),
													ImageLockMode.WriteOnly,
													PixelFormat.Format32bppArgb);

			for (int y = 0; y < height; y++) {
				IntPtr outputScan = (IntPtr)((long)outputData.Scan0 + (y * outputData.Stride));
				for (int x = 0; x < width; x++) {
					switch (tile[x, y]) {
					case GBColor.WHITE: Marshal.WriteInt32(outputScan, x * 4, whiteARGB); break;
					case GBColor.DARK_GRAY: Marshal.WriteInt32(outputScan, x * 4, lightGrayARGB); break;
					case GBColor.LIGHT_GRAY: Marshal.WriteInt32(outputScan, x * 4, darkGrayARGB); break;
					case GBColor.BLACK: Marshal.WriteInt32(outputScan, x * 4, blackARGB); break;
					}
				}
			}
			output.UnlockBits(outputData);

			return output;
		}
	}
}
