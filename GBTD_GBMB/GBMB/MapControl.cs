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

		/// <summary>
		/// Tile sizes.  THis currently isn't dynamic and the app just won't be happy if something else is given.
		/// </summary>
		private const int TILE_WIDTH = 8, TILE_HEIGHT = 8;

		/// <summary>
		/// The sizes of the labels.  The other part is based off of the zoom.
		/// </summary>
		private const int BOX_HEIGHT = 17, BOX_WIDTH = 27;
		/// <summary>
		/// Innitial positions of the labels.
		/// </summary>
		private const int INITIAL_BOX_X = 1, INITIAL_BOX_Y = 1;
		/// <summary>
		/// The position after the box.
		/// </summary>
		private const int AFTER_BOX_X = INITIAL_BOX_X + BOX_WIDTH;
		/// <summary>
		/// The position after the box.
		/// </summary>
		private const int AFTER_BOX_Y = INITIAL_BOX_Y + BOX_HEIGHT;

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
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

			if (map == null || tileset == null) {
				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, e.ClipRectangle);

				e.Graphics.DrawString("There is no tileset selected or something.  Go fix it.", this.Font, SystemBrushes.ControlText, 40, 40);
			} else {
				for (int y = 0; y < map.Master.Height; y++) {
					for (int x = 0; x < map.Master.Width; x++) {
						DrawTile(e.Graphics, map.Tiles[x, y], x, y);
					}
				}
			}

			StringFormat centered = new StringFormat();
			centered.Alignment = StringAlignment.Center;
			centered.LineAlignment = StringAlignment.Center;

			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(Point.Empty, this.Size), Border3DStyle.SunkenOuter, 
				Border3DSide.Top | Border3DSide.Left);

			Rectangle OuterBorderRect = new Rectangle(INITIAL_BOX_X, INITIAL_BOX_Y, BOX_WIDTH, BOX_HEIGHT);
			Rectangle InnerBorderRect = new Rectangle(INITIAL_BOX_X + 1, INITIAL_BOX_Y + 1, BOX_WIDTH - 2, BOX_HEIGHT - 2);
			Rectangle TextRect = new Rectangle(INITIAL_BOX_X + 2, INITIAL_BOX_Y + 2, BOX_WIDTH - 4, BOX_HEIGHT - 4);

			//The topleft empty label.
			ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top);
			ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Right | Border3DSide.Bottom);
			ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Left | Border3DSide.Top);
			ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Right | Border3DSide.Bottom);

			e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);

			//All of the labels on the top.
			OuterBorderRect = new Rectangle(AFTER_BOX_X, INITIAL_BOX_Y, (int)(TILE_WIDTH * Zoom), BOX_HEIGHT);
			InnerBorderRect = new Rectangle(AFTER_BOX_X + 1, INITIAL_BOX_Y + 1, (int)(TILE_WIDTH * Zoom) - 2, BOX_HEIGHT - 2);
			TextRect = new Rectangle(AFTER_BOX_X + 2, INITIAL_BOX_Y + 2, (int)(TILE_WIDTH * Zoom) - 4, BOX_HEIGHT - 4);

			for (int XPos = AFTER_BOX_X, RowNumber = 0;
					XPos < this.Width;
					XPos += (int)(TILE_WIDTH * Zoom), RowNumber++) {

				OuterBorderRect.X = XPos;
				InnerBorderRect.X = XPos + 1;
				TextRect.X = XPos + 2;

				ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Right | Border3DSide.Bottom);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Right | Border3DSide.Bottom);

				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);
				e.Graphics.DrawString(RowNumber.ToString(), this.Font, SystemBrushes.ControlText, TextRect, centered);
			}

			//All of the labels on the side.
			OuterBorderRect = new Rectangle(INITIAL_BOX_X, AFTER_BOX_Y, BOX_WIDTH, (int)(TILE_HEIGHT * Zoom));
			InnerBorderRect = new Rectangle(INITIAL_BOX_X + 1, AFTER_BOX_Y + 1, BOX_WIDTH - 2, (int)(TILE_HEIGHT * Zoom) - 2);
			TextRect = new Rectangle(INITIAL_BOX_X + 2, AFTER_BOX_Y + 2, BOX_WIDTH - 4, (int)(TILE_HEIGHT * Zoom) - 4);

			for (int YPos = AFTER_BOX_Y, ColNumber = 0;
					YPos < this.Width;
					YPos += (int)(TILE_WIDTH * Zoom), ColNumber++) {

				OuterBorderRect.Y = YPos;
				InnerBorderRect.Y = YPos + 1;
				TextRect.Y = YPos + 2;

				ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Right | Border3DSide.Bottom);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Right | Border3DSide.Bottom);

				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);
				e.Graphics.DrawString(ColNumber.ToString(), this.Font, SystemBrushes.ControlText, TextRect, centered);
			}

			base.OnPaint(e);
		}

		private void DrawTile(Graphics g, GBMObjectMapTileDataRecord tile, int tileX, int tileY) {
			Tile t = tileset.tiles[tile.TileNumber];
			RectangleF rect = new RectangleF(
				(tileX * t.Width * Zoom) + AFTER_BOX_X,
				(tileY * t.Height * Zoom) + AFTER_BOX_Y,
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
