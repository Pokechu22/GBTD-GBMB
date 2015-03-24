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
		private float zoom;
		private bool showGrid;
		private bool showDoubleMarkers;
		private bool showPropertyColors;

		/// <summary>
		/// The scale factor used for zooming.
		/// </summary>
		[Category("Map display"), Description("The scale factor used for the map.")]
		[DefaultValue(2f)]
		public float Zoom {
			get { return zoom; }
			set { zoom = value; this.Invalidate(); }
		}

		[Category("Map display"), Description("Whether or not a per-tile grid is shown.")]
		public bool ShowGrid {
			get { return showGrid; }
			set { showGrid = value; this.Invalidate(true); }
		}
		[Category("Map display"), Description("Whether or not dots are displayed every 2 tiles.")]
		public bool ShowDoubleMarkers {
			get { return showDoubleMarkers; }
			set { showDoubleMarkers = value; this.Invalidate(true); }
		}
		[Category("Map display"), Description("Whether or not properties are colorized.")]
		public bool ShowPropertyColors {
			get { return showPropertyColors; }
			set { showPropertyColors = value; this.Invalidate(true); }
		}
		
		[Category("Map data"), Description("The color set to use for the map.")]
		public ColorSet ColorSet { get; set; }
		[Category("Map data"), Description("The palette set to use for the map.")]
		public PaletteData PaletteData { get; set; }

		private GBRObjectTileData tileset;
		private GBMObjectMapTileData map;
		private GBRObjectTilePalette defaultPal;

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
		[Category("Map data"), Description("The default palette to use.")]
		public GBRObjectTilePalette DefaultPalette {
			get { return defaultPal; }
			set { defaultPal = value; OnMapChanged(); }
		}

		private int selectionX1, selectionY1, selectionX2, selectionY2;

		[Category("Map data"), Description("The first x cooridinate of the selection.")]
		[DefaultValue(0)]
		public int SelectionX1 {
			get { return selectionX1; }
			set { selectionX1 = value; OnSelectionChanged(); }
		}
		[Category("Map data"), Description("The first y cooridinate of the selection.")]
		[DefaultValue(0)]
		public int SelectionY1 {
			get { return selectionY1; }
			set { selectionY1 = value; OnSelectionChanged(); }
		}
		[Category("Map data"), Description("The second x cooridinate of the selection.")]
		[DefaultValue(0)]
		public int SelectionX2 {
			get { return selectionX2; }
			set { selectionX2 = value; OnSelectionChanged(); }
		}
		[Category("Map data"), Description("The second y cooridinate of the selection.")]
		[DefaultValue(0)]
		public int SelectionY2 {
			get { return selectionY2; }
			set { selectionY2 = value; OnSelectionChanged(); }
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
		private const int INITIAL_BOX_X = 0, INITIAL_BOX_Y = 0;
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

			SetStyle(ControlStyles.ResizeRedraw, true);

			Zoom = 4f;
			PaletteData = new PaletteData();

			ColorSet = Shared.Palettes.ColorSet.GAMEBOY_COLOR;
		}

		protected void OnMapChanged() {
			this.Invalidate(true);
		}

		protected void OnSelectionChanged() {
			this.Invalidate(true);
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			selectionX1 = selectionX2 = MouseToTileX(e.X);
			selectionY1 = selectionY2 = MouseToTileY(e.Y);

			OnSelectionChanged();

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Left)) {
				selectionX2 = MouseToTileX(e.X);
				selectionY2 = MouseToTileY(e.Y);

				OnSelectionChanged();
			}

			base.OnMouseMove(e);
		}

		/// <summary>
		/// Converts a mouse click position to a tile position.
		/// </summary>
		private int MouseToTileX(int mouseX) {
			int value = (mouseX - AFTER_BOX_X) / (int)(TILE_WIDTH * Zoom);

			if (value < 0) { return 0; }
			if (value >= map.Master.Width) { return (int)(map.Master.Width - 1); }
			return value;
		}

		/// <summary>
		/// Converts a mouse click position to a tile position.
		/// </summary>
		private int MouseToTileY(int mouseY) {
			int value = (mouseY - AFTER_BOX_Y) / (int)(TILE_HEIGHT * Zoom);

			if (value < 0) { return 0; }
			if (value >= map.Master.Height) { return (int)(map.Master.Height - 1); }
			return value;
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
						DrawTile(e, map.Tiles[x, y], x, y);
					}
				}

				if (showGrid) {
					DrawGrid(e);
				}
				if (showDoubleMarkers) {
					DrawDoubleMarkers(e); //TODO
				}
			}

			DrawNumberLabels(e);

			base.OnPaint(e);
		}

		private void DrawNumberLabels(PaintEventArgs e) {
			StringFormat centered = new StringFormat();
			centered.Alignment = StringAlignment.Center;
			centered.LineAlignment = StringAlignment.Center;
			
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

				if (map == null || tileset == null || RowNumber >= map.Master.Width) {
					//Reached end of numbering; create one large box for the remainder.
					OuterBorderRect.Width = this.Width - OuterBorderRect.X;
					InnerBorderRect.Width = this.Width - OuterBorderRect.X - 2;
					TextRect.Width = this.Width - OuterBorderRect.X - 4;

					ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top);
					ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Right | Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Left | Border3DSide.Top);
					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Right | Border3DSide.Bottom);

					e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);

					break;
				}

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
					YPos < this.Height;
					YPos += (int)(TILE_WIDTH * Zoom), ColNumber++) {

				OuterBorderRect.Y = YPos;
				InnerBorderRect.Y = YPos + 1;
				TextRect.Y = YPos + 2;

				if (map == null || tileset == null || ColNumber >= map.Master.Height) {
					//Reached end of numbering; create one large box for the remainder.
					OuterBorderRect.Height = this.Height - OuterBorderRect.Y;
					InnerBorderRect.Height = this.Height - OuterBorderRect.Y - 2;
					TextRect.Height = this.Height - OuterBorderRect.Y - 4;

					ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top);
					ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Right | Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Left | Border3DSide.Top);
					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Right | Border3DSide.Bottom);

					e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);

					break;
				}

				ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(e.Graphics, OuterBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Right | Border3DSide.Bottom);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedOuter, Border3DSide.Left | Border3DSide.Top);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Right | Border3DSide.Bottom);

				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);
				e.Graphics.DrawString(ColNumber.ToString(), this.Font, SystemBrushes.ControlText, TextRect, centered);
			}
		}

		private void DrawGrid(PaintEventArgs e) {
			int endOfTilesX = (int)(TILE_WIDTH * zoom * map.Master.Width) + AFTER_BOX_X;
			int endOfTilesY = (int)(TILE_HEIGHT * zoom * map.Master.Height) + AFTER_BOX_Y;

			for (int XPos = AFTER_BOX_X; XPos <= endOfTilesX; XPos += (int)(TILE_WIDTH * zoom)) {
				e.Graphics.DrawLine(Pens.Black, XPos, AFTER_BOX_Y, XPos, endOfTilesY);
			}
			for (int YPos = AFTER_BOX_Y; YPos <= endOfTilesY; YPos += (int)(TILE_HEIGHT * zoom)) {
				e.Graphics.DrawLine(Pens.Black, AFTER_BOX_X, YPos, endOfTilesX, YPos);
			}
		}

		private void DrawDoubleMarkers(PaintEventArgs e) {
			if (map == null || tileset == null) {
				return;
			}

			//TODO make this work off of scrolled position.
			for (int tileY = 2; tileY < map.Master.Height; tileY += 2) {
				for (int tileX = 2; tileX < map.Master.Width; tileX += 2) {
					int centX = (int)(tileX * TILE_WIDTH * Zoom) + AFTER_BOX_X - 1;
					int centY = (int)(tileY * TILE_HEIGHT * Zoom) + AFTER_BOX_Y - 1;

					e.Graphics.FillRectangle(Brushes.Red, centX - 1, centY, 3, 1);
					e.Graphics.FillRectangle(Brushes.Red, centX, centY - 1, 1, 3);
				}
			}
		}

		private void DrawTile(PaintEventArgs e, GBMObjectMapTileDataRecord record, int tileX, int tileY) {
			Tile t = tileset.tiles[record.TileNumber];
			RectangleF rect = new RectangleF(
				(tileX * t.Width * Zoom) + AFTER_BOX_X,
				(tileY * t.Height * Zoom) + AFTER_BOX_Y,
				t.Width * Zoom,
				t.Height * Zoom);

			if (!rect.IntersectsWith(e.ClipRectangle)) {
				return;
			}

			using (Bitmap bmp = MakeTileBitmap(record, t,
					GetApropriatelyFilteredColor(record, GBColor.WHITE, tileX, tileY),
					GetApropriatelyFilteredColor(record, GBColor.LIGHT_GRAY, tileX, tileY),
					GetApropriatelyFilteredColor(record, GBColor.DARK_GRAY, tileX, tileY),
					GetApropriatelyFilteredColor(record, GBColor.BLACK, tileX, tileY))) {
				e.Graphics.DrawImage(bmp, rect);
			}
		}

		/// <summary>
		/// Fast creation of a bitmap for tiles, using marshaling and stuff.
		/// </summary>
		/// <param name="record"></param>
		/// <param name="tile"></param>
		/// <param name="white"></param>
		/// <param name="lightGray"></param>
		/// <param name="darkGray"></param>
		/// <param name="black"></param>
		/// <returns></returns>
		private Bitmap MakeTileBitmap(GBMObjectMapTileDataRecord record, Tile tile, Color white, Color lightGray, Color darkGray, Color black) {
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

			bool canFlip = ColorSet.SupportsTileFlipping();

			for (int y = 0; y < height; y++) {
				IntPtr outputScan = (IntPtr)((long)outputData.Scan0 + (y * outputData.Stride));
				for (int x = 0; x < width; x++) {
					int usedARGB = 0;

					switch (tile[(record.FlippedHorizontally && canFlip) ? 7 - x : x, (record.FlippedVertically && canFlip) ? 7 - y : y]) {
					case GBColor.WHITE: usedARGB = whiteARGB; break;
					case GBColor.LIGHT_GRAY: usedARGB = lightGrayARGB; break;
					case GBColor.DARK_GRAY: usedARGB = darkGrayARGB; break;
					case GBColor.BLACK: usedARGB = blackARGB; break;
					}

					Marshal.WriteInt32(outputScan, x * 4, usedARGB);
				}
			}
			output.UnlockBits(outputData);
			
			return output;
		}

		/// <summary>
		/// Gets the color that has been filered for the specified location (e.g. with selection)
		/// </summary>
		/// <param name="record">The tile at that location</param>
		/// <param name="color">The color to get</param>
		/// <param name="tileX">The x-position of the tile</param>
		/// <param name="tileY">The y-position of the tile</param>
		/// <returns></returns>
		private Color GetApropriatelyFilteredColor(GBMObjectMapTileDataRecord record, GBColor color, int tileX, int tileY) {
			Color returned = GetColor(ColorSet, record, color);

			if (selectionX1 < selectionX2 ? (selectionX1 <= tileX && tileX <= selectionX2) : (selectionX2 <= tileX && tileX <= selectionX1)) {
				if (selectionY1 < selectionY2 ? (selectionY1 <= tileY && tileY <= selectionY2) : (selectionY2 <= tileY && tileY <= selectionY1)) {
					returned = returned.FilterAsSelected();
				}
			}

			return returned;
		}

		/// <summary>
		/// Gets the color used under the specified conditions (thouhg it isn't yet filtered)
		/// </summary>
		/// <param name="set"></param>
		/// <param name="record"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		private Color GetColor(ColorSet set, GBMObjectMapTileDataRecord record, GBColor color) {
			switch (set) {
			case ColorSet.GAMEBOY_COLOR:
				if (record.GBCPalette.HasValue) {
					return PaletteData.GBCPaletteSet[record.GBCPalette.Value][color];
				} else {
					if (defaultPal != null) {
						return PaletteData.GBCPaletteSet[defaultPal.GBCPalettes[record.TileNumber]][color];
					} else {
						return PaletteData.GBCPaletteSet[0][color];
					}
				}
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				if (record.GBCPalette.HasValue) {
					return PaletteData.GBCPaletteSet[record.GBCPalette.Value][color].FilterWithGBC();
				} else {
					if (defaultPal != null) {
						return PaletteData.GBCPaletteSet[defaultPal.GBCPalettes[record.TileNumber]][color].FilterWithGBC();
					} else {
						return PaletteData.GBCPaletteSet[0][color].FilterWithGBC();
					}
				}
			case ColorSet.SUPER_GAMEBOY:
				if (record.SGBPalette.HasValue) {
					return PaletteData.SGBPaletteSet[record.SGBPalette.Value][color];
				} else {
					if (defaultPal != null) {
						return PaletteData.SGBPaletteSet[defaultPal.SGBPalettes[record.TileNumber]][color];
					} else {
						return PaletteData.SGBPaletteSet[0][color];
					}
				}
			case ColorSet.GAMEBOY:
				return color.GetNormalColor();
			case ColorSet.GAMEBOY_POCKET:
				return color.GetPocketColor();
			default: throw new InvalidEnumArgumentException("set", (int)set, typeof(ColorSet));
			}
		}
	}
}
