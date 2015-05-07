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
		[Description("This property is not meaningful for this control.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text { get { return base.Text; } set { base.Text = value; } }

		private float zoom;
		private bool showGrid;
		private bool showDoubleMarkers;
		private bool showPropertyColors;

		/// <summary>
		/// The scale factor used for zooming.
		/// </summary>
		[Category("Map display"), Description("The scale factor used for the map.")]
		public ZoomLevel ZoomLevel {
			get { return (ZoomLevel)(zoom * 2); }
			set { zoom = ((int)value / 2f); this.Invalidate(); }
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

		private ColorSet colorSet;
		private PaletteData paletteData;

		[Category("Map data"), Description("The color set to use for the map.")]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; this.Invalidate(); }
		}
		[Category("Map data"), Description("The palette set to use for the map.")]
		public PaletteData PaletteData {
			get { return paletteData; }
			set { paletteData = value; this.Invalidate(); }
		}

		private GBRObjectTileData tileset;
		private GBMObjectMapTileData map;
		private GBRObjectTilePalette defaultPal;
		private GBMObjectMapPropertyData properties;
		private GBMObjectMapPropertyColors propertyColors;
		
		[Category("Map data"), Description("The map to use.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBMObjectMapTileData Map {
			get { return map; }
			set { map = value; OnMapChanged(); }
		}
		[Category("Map data"), Description("The tileset to use.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBRObjectTileData TileSet {
			get { return tileset; }
			set { tileset = value; OnMapChanged(); }
		}
		[Category("Map data"), Description("The default palette to use.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBRObjectTilePalette DefaultPalette {
			get { return defaultPal; }
			set { defaultPal = value; OnMapChanged(); }
		}
		[Category("Map data"), Description("The actual data for each of the properties.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBMObjectMapPropertyData Properties {
			get { return properties; }
			set { properties = value; OnMapChanged(); }
		}
		[Category("Map data"), Description("The color data applied to each of the properties.")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBMObjectMapPropertyColors PropertyColors {
			get { return propertyColors; }
			set { propertyColors = value; OnMapChanged(); }
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
		/// Whether or not the entire set of tiles in the selection are vertically flipped or not.
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckState SelectionVerticalFlip {
			get {
				int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
				int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
				int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
				int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

				//Whether or not we have found cases where it is or is not checked.
				bool foundYes = false, foundNo = false;

				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
						if (map.Tiles[x, y].FlippedVertically) {
							foundYes = true;
						} else {
							foundNo = true;
						}
					}
				}

				if (foundYes && foundNo) {
					return CheckState.Indeterminate;
				}
				if (foundYes) {
					return CheckState.Checked;
				}
				if (foundNo) {
					return CheckState.Unchecked;
				}
				throw new Exception("There is no selection?  No tiles were found...");
			}
			set {
				//Ignore values being set to indeterminite.
				if (value == CheckState.Indeterminate) { return; }

				bool check = (value == CheckState.Checked);

				int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
				int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
				int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
				int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
						map.Tiles[x, y].FlippedVertically = check;
					}
				}

				OnMapChanged();
			}
		}

		/// <summary>
		/// Whether or not the entire set of tiles in the selection are horizontally flipped or not.
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckState SelectionHorizontalFlip {
			get {
				int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
				int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
				int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
				int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

				//Whether or not we have found cases where it is or is not checked.
				bool foundYes = false, foundNo = false;

				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
						if (map.Tiles[x, y].FlippedHorizontally) {
							foundYes = true;
						} else {
							foundNo = true;
						}
					}
				}

				if (foundYes && foundNo) {
					return CheckState.Indeterminate;
				}
				if (foundYes) {
					return CheckState.Checked;
				}
				if (foundNo) {
					return CheckState.Unchecked;
				}
				throw new Exception("There is no selection?  No tiles were found...");
			}
			set {
				//Ignore values being set to indeterminite.
				if (value == CheckState.Indeterminate) { return; }

				bool check = (value == CheckState.Checked);

				int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
				int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
				int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
				int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
						map.Tiles[x, y].FlippedHorizontally = check;
					}
				}

				OnMapChanged();
			}
		}

		/// <summary>
		/// The palette that the selection uses.
		/// <para>A value of <c>null</c> means that the palette is default.
		/// A value of <c>-1</c> means that there are multiple palettes in the seelction.
		/// Any other value is the actual palette.</para>
		/// </summary>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int? SelectionPalette {
			get {
				if (this.colorSet != ColorSet.GAMEBOY_COLOR
					&& this.colorSet != ColorSet.GAMEBOY_COLOR_FILTERED
					&& this.colorSet != ColorSet.SUPER_GAMEBOY) {
					throw new InvalidOperationException("Can only get the selected palette when using a GBC or SGB color set!");
				}
				
				int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
				int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
				int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
				int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

				byte? firstFoundValue; //TODO this is not the best name.
				if (colorSet == ColorSet.SUPER_GAMEBOY) {
					firstFoundValue = map.Tiles[lowerSelectionX, lowerSelectionY].SGBPalette;
				} else {
					firstFoundValue = map.Tiles[lowerSelectionX, lowerSelectionY].GBCPalette;
				}
				bool foundOthers = false;

				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
						if (colorSet == ColorSet.SUPER_GAMEBOY) {
							if (map.Tiles[x, y].SGBPalette != firstFoundValue) {
								foundOthers = true;
							}
						} else {
							if (map.Tiles[x, y].GBCPalette != firstFoundValue) {
								foundOthers = true;
							}
						}
					}
				}

				if (foundOthers) {
					return -1;
				} else {
					return firstFoundValue;
				}
			}
			set {
				if (value == -1) { return; } //For -1, we will just ignore it.  But otherwise...
				byte? val = (byte?)value; //If this cannot be casted, too bad.  It shouldn't be set like that.

				if (this.colorSet != ColorSet.GAMEBOY_COLOR
					&& this.colorSet != ColorSet.GAMEBOY_COLOR_FILTERED
					&& this.colorSet != ColorSet.SUPER_GAMEBOY) {
					throw new InvalidOperationException("Can only get the selected palette when using a GBC or SGB color set!");
				}

				int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
				int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
				int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
				int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
						if (colorSet == ColorSet.SUPER_GAMEBOY) {
							map.Tiles[x, y].SGBPalette = val;
						} else {
							map.Tiles[x, y].GBCPalette = val;
						}
					}
				}

				OnMapChanged();
			}
		}

		public void FillSelectedTiles(UInt16 tileNumber = 0, bool flippedHorizontally = false, bool flippedVertically = false, 
				byte? gbcPal = null, byte? sgbPal = null) {
			int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
			int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
			int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
			int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

			for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					map.Tiles[x, y].TileNumber = tileNumber;
					map.Tiles[x, y].FlippedHorizontally = flippedHorizontally;
					map.Tiles[x, y].FlippedVertically = flippedVertically;
					map.Tiles[x, y].GBCPalette = gbcPal;
					map.Tiles[x, y].SGBPalette = sgbPal;
				}
			}

			OnMapChanged();
		}

		public GBMObjectMapTileDataRecord[,] GetSelectedTiles() {
			int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
			int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
			int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
			int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

			int selectionWidth = upperSelectionX - lowerSelectionX + 1;
			int selectionHeight = upperSelectionY - lowerSelectionY + 1;

			GBMObjectMapTileDataRecord[,] returned = new GBMObjectMapTileDataRecord[selectionWidth, selectionHeight];

			for (int y = 0; y < selectionHeight; y++) {
				for (int x = 0; x < selectionWidth; x++) {
					returned[x, y] = map.Tiles[lowerSelectionX + x, lowerSelectionY + y];
				}
			}

			return returned;
		}

		public void PasteAtSelection(GBMObjectMapTileDataRecord[,] data) {
			int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
			int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);

			for (int y = 0; y < data.GetLength(1); y++) {
				for (int x = 0; x < data.GetLength(0); x++) {
					if (x + lowerSelectionX >= map.Master.Width || y + lowerSelectionY >= map.Master.Height) { continue; }

					map.Tiles[x + lowerSelectionX, y + lowerSelectionY] = data[x, y];
				}
			}

			OnMapChanged();
		}

		/// <summary>
		/// Gets the value of the specified proeprty within the selection.
		/// </summary>
		/// <returns>The value, or <c>null</c> if there are multiple values.</returns>
		public UInt16? GetSelectionPropertyData(int property) {
			int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
			int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
			int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
			int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

			UInt16? returned = properties.Data[lowerSelectionX, lowerSelectionY, property];

			for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					if (properties.Data[x, y, property] != returned) {
						returned = null;
					}
				}
			}

			return returned;
		}

		/// <summary>
		/// Sets the selection's property value.
		/// </summary>
		public void SetSelectionPropertyData(int property, UInt16 value) {
			int lowerSelectionX = (selectionX1 < selectionX2 ? selectionX1 : selectionX2);
			int upperSelectionX = (selectionX1 < selectionX2 ? selectionX2 : selectionX1);
			int lowerSelectionY = (selectionY1 < selectionY2 ? selectionY1 : selectionY2);
			int upperSelectionY = (selectionY1 < selectionY2 ? selectionY2 : selectionY1);

			for (int y = lowerSelectionY; y <= upperSelectionY; y++) {
				for (int x = lowerSelectionX; x <= upperSelectionX; x++) {
					properties.Data[x, y, property] = value;
				}
			}

			OnMapChanged();
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

			zoom = 4f;
			PaletteData = new PaletteData();

			ColorSet = Shared.Palettes.ColorSet.GAMEBOY_COLOR;
		}

		[Description("Fires when the map's tiles have changed.")]
		public event EventHandler MapChanged;
		[Description("Fires when the selection has changed.")]
		public event EventHandler SelectionChanged;

		protected void OnMapChanged() {
			this.Invalidate(true);

			if (MapChanged != null) {
				MapChanged(this, new EventArgs());
			}
		}

		protected void OnSelectionChanged() {
			this.Invalidate(true);

			if (SelectionChanged != null) {
				SelectionChanged(this, new EventArgs());
			}
		}

		[Description("Fires when a tile is clicked -- use this to apply tools!")]
		public event EventHandler<TileClickedEventArgs> TileClicked;
		protected void OnTileClicked(int tileX, int tileY) {
			if (TileClicked != null) {
				TileClicked(this, new TileClickedEventArgs(tileX, tileY, this));
			}
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			if (map != null && tileset != null) {
				if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Left)) {
					int newSelectionX1, newSelectionX2, newSelectionY1, newSelectionY2;

					newSelectionX1 = newSelectionX2 = MouseToTileX(e.X);
					newSelectionY1 = newSelectionY2 = MouseToTileY(e.Y);

					if (newSelectionX1 != selectionX1 || newSelectionX2 != selectionX2 ||
						newSelectionY1 != selectionY1 || newSelectionY2 != selectionY2) {

						selectionX1 = newSelectionX1;
						selectionX2 = newSelectionX2;
						selectionY1 = newSelectionY1;
						selectionY2 = newSelectionY2;

						OnSelectionChanged();
					}
				}
				if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Right)) {
					OnTileClicked(MouseToTileX(e.X), MouseToTileY(e.Y));

					OnMapChanged();
				}
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			if (map != null && tileset != null) {
				if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Left)) {
					int newSelectionX2, newSelectionY2;

					newSelectionX2 = MouseToTileX(e.X);
					newSelectionY2 = MouseToTileY(e.Y);

					if (newSelectionX2 != selectionX2 || newSelectionY2 != selectionY2) {

						selectionX2 = newSelectionX2;
						selectionY2 = newSelectionY2;

						OnSelectionChanged();
					}
				}
				if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Right)) {
					OnTileClicked(MouseToTileX(e.X), MouseToTileY(e.Y));

					OnMapChanged();
				}
			}

			base.OnMouseMove(e);
		}

		/// <summary>
		/// Converts a mouse click position to a tile position.
		/// </summary>
		private int MouseToTileX(int mouseX) {
			int value = (mouseX - AFTER_BOX_X) / (int)(TILE_WIDTH * zoom);

			if (value < 0) { return 0; }
			if (value >= map.Master.Width) { return (int)(map.Master.Width - 1); }
			return value;
		}

		/// <summary>
		/// Converts a mouse click position to a tile position.
		/// </summary>
		private int MouseToTileY(int mouseY) {
			int value = (mouseY - AFTER_BOX_Y) / (int)(TILE_HEIGHT * zoom);

			if (value < 0) { return 0; }
			if (value >= map.Master.Height) { return (int)(map.Master.Height - 1); }
			return value;
		}

		/// <summary>
		/// Creates an image representation of the map.
		/// </summary>
		/// <returns></returns>
		public Bitmap GetMapImage() {
			if (map == null || tileset == null) {
				throw new WarningException("Map or tileset not set; cannot create map bitmap!");
			}
			
			Bitmap bitmap = new Bitmap((int)(map.Master.Width * tileset.Width), (int)(map.Master.Height * tileset.Height));
			using (Graphics g = Graphics.FromImage(bitmap)) {
				for (int y = 0; y < map.Master.Height; y++) {
					for (int x = 0; x < map.Master.Width; x++) {
						GBMObjectMapTileDataRecord record = map.Tiles[x, y];
						Tile t = tileset.tiles[record.TileNumber];

						Bitmap bmp = MakeTileBitmap(record, t,
							GetColor(colorSet, record, GBColor.WHITE),
							GetColor(colorSet, record, GBColor.LIGHT_GRAY),
							GetColor(colorSet, record, GBColor.DARK_GRAY),
							GetColor(colorSet, record, GBColor.BLACK));

						g.DrawImageUnscaled(bmp, x * tileset.Width, y * tileset.Width);
					}
				}
			}

			return bitmap;
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; //Fixes lines in the middle issue.
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

			if (map == null || tileset == null) {
				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, e.ClipRectangle);

				using (StringFormat format = new StringFormat(StringFormatFlags.NoWrap)) {
					//Temporarilly disable offsetting, since it messes up the letter l.
					e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;

					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Near;
					
					//Intentionally multi-line string with a starting newline.
					e.Graphics.DrawString(@"
No tileset selected.
Goto File, Map properties to select a tileset.", this.Font, SystemBrushes.ControlText, 
						new RectangleF(AFTER_BOX_X, AFTER_BOX_Y, this.Width - AFTER_BOX_X, this.Height - AFTER_BOX_Y), format);

					//And re-enable it.
					e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
				}
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

			//Again, text doesn't like the half offset.
			e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Default;
			DrawNumberLabels(e);
			e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

			base.OnPaint(e);
		}

		private void DrawNumberLabels(PaintEventArgs e) {
			StringFormat centered = new StringFormat();
			centered.Alignment = StringAlignment.Center;
			centered.LineAlignment = StringAlignment.Center;
			
			Rectangle OuterBorderRect = new Rectangle(INITIAL_BOX_X, INITIAL_BOX_Y, BOX_WIDTH, BOX_HEIGHT);
			Rectangle InnerBorderRect = new Rectangle(INITIAL_BOX_X, INITIAL_BOX_Y, BOX_WIDTH - 1, BOX_HEIGHT - 1);
			Rectangle TextRect = new Rectangle(INITIAL_BOX_X + 1, INITIAL_BOX_Y + 1, BOX_WIDTH - 3, BOX_HEIGHT - 3);

			//The topleft empty label.
			e.Graphics.FillRectangle(SystemBrushes.ControlDarkDark, OuterBorderRect);

			ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Right);
			ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Top | Border3DSide.Bottom);

			e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);

			//All of the labels on the top.
			OuterBorderRect = new Rectangle(AFTER_BOX_X, INITIAL_BOX_Y, (int)(TILE_WIDTH * zoom), BOX_HEIGHT);
			InnerBorderRect = new Rectangle(AFTER_BOX_X, INITIAL_BOX_Y, (int)(TILE_WIDTH * zoom) - 1, BOX_HEIGHT - 1);
			TextRect = new Rectangle(AFTER_BOX_X + 1, INITIAL_BOX_Y + 1, (int)(TILE_WIDTH * zoom) - 3, BOX_HEIGHT - 3);

			for (int XPos = AFTER_BOX_X, RowNumber = 0;
					XPos < this.Width;
					XPos += (int)(TILE_WIDTH * zoom), RowNumber++) {

				OuterBorderRect.X = XPos;
				InnerBorderRect.X = XPos;
				TextRect.X = XPos + 1;

				if (map == null || tileset == null || RowNumber >= map.Master.Width) {
					//Reached end of numbering; create one large box for the remainder.
					OuterBorderRect.Width = this.Width - OuterBorderRect.X;
					InnerBorderRect.Width = this.Width - OuterBorderRect.X - 1;
					TextRect.Width = this.Width - OuterBorderRect.X - 3;

					e.Graphics.FillRectangle(SystemBrushes.ControlDarkDark, OuterBorderRect);

					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Right);
					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Top | Border3DSide.Bottom);

					e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);

					break;
				}

				e.Graphics.FillRectangle(SystemBrushes.ControlDarkDark, OuterBorderRect);

				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Right);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Top | Border3DSide.Bottom);

				e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);
				e.Graphics.DrawString(RowNumber.ToString(), this.Font, SystemBrushes.ControlText, TextRect, centered);
			}

			//All of the labels on the side.
			OuterBorderRect = new Rectangle(INITIAL_BOX_X, AFTER_BOX_Y, BOX_WIDTH, (int)(TILE_HEIGHT * zoom));
			InnerBorderRect = new Rectangle(INITIAL_BOX_X, AFTER_BOX_Y, BOX_WIDTH - 1, (int)(TILE_HEIGHT * zoom) - 1);
			TextRect = new Rectangle(INITIAL_BOX_X + 1, AFTER_BOX_Y + 1, BOX_WIDTH - 3, (int)(TILE_HEIGHT * zoom) - 3);

			for (int YPos = AFTER_BOX_Y, ColNumber = 0;
					YPos < this.Height;
					YPos += (int)(TILE_WIDTH * zoom), ColNumber++) {

				OuterBorderRect.Y = YPos;
				InnerBorderRect.Y = YPos;
				TextRect.Y = YPos + 1;

				if (map == null || tileset == null || ColNumber >= map.Master.Height) {
					//Reached end of numbering; create one large box for the remainder.
					OuterBorderRect.Height = this.Height - OuterBorderRect.Y;
					InnerBorderRect.Height = this.Height - OuterBorderRect.Y - 1;
					TextRect.Height = this.Height - OuterBorderRect.Y - 3;

					e.Graphics.FillRectangle(SystemBrushes.ControlDarkDark, OuterBorderRect);

					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Right);
					ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Top | Border3DSide.Bottom);

					e.Graphics.FillRectangle(SystemBrushes.ButtonFace, TextRect);

					break;
				}

				e.Graphics.FillRectangle(SystemBrushes.ControlDarkDark, OuterBorderRect);

				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Left | Border3DSide.Right);
				ControlPaint.DrawBorder3D(e.Graphics, InnerBorderRect, Border3DStyle.RaisedInner, Border3DSide.Top | Border3DSide.Bottom);

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
					int centX = (int)(tileX * TILE_WIDTH * zoom) + AFTER_BOX_X - 1;
					int centY = (int)(tileY * TILE_HEIGHT * zoom) + AFTER_BOX_Y - 1;

					e.Graphics.FillRectangle(Brushes.Red, centX - 1, centY, 3, 1);
					e.Graphics.FillRectangle(Brushes.Red, centX, centY - 1, 1, 3);
				}
			}
		}

		private void DrawTile(PaintEventArgs e, GBMObjectMapTileDataRecord record, int tileX, int tileY) {
			Tile t = tileset.tiles[record.TileNumber];
			RectangleF rect = new RectangleF(
				(tileX * t.Width * zoom) + AFTER_BOX_X,
				(tileY * t.Height * zoom) + AFTER_BOX_Y,
				t.Width * zoom,
				t.Height * zoom);

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

			if (showPropertyColors && properties != null && propertyColors != null) {
				//TODO properly handle amount of property colors other than 2.
				if (propertyColors.Master.PropColorCount == 2 && properties.Master.PropCount != 0) {
					var red = propertyColors.Data[0];
					var green = propertyColors.Data[1];
					//Red = 0, Green = 1
					UInt16 redProp = properties.Data[tileX, tileY, red.Property];
					UInt16 greenProp = properties.Data[tileX, tileY, green.Property];

					if (red.Operator.IsTrue(redProp, red.Value)) {
						returned = returned.FilterAsRed();
					}
					if (green.Operator.IsTrue(greenProp, green.Value)) {
						returned = returned.FilterAsGreen();
					}
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

		private void InitializeComponent() {
			this.SuspendLayout();
			this.ResumeLayout(false);

		}
	}

	/// <summary>
	/// Event args used when a tile is clicked.
	/// </summary>
	public class TileClickedEventArgs : EventArgs
	{
		/// <summary>
		/// The x-cooridinate of the clicked tile.
		/// </summary>
		public readonly int tileX;
		/// <summary>
		/// The y-cooridinate of the clicked tile.
		/// </summary>
		public readonly int tileY;
		/// <summary>
		/// The mapcontrol that generated the event.
		/// </summary>
		public readonly MapControl mapControl;

		public TileClickedEventArgs(int tileX, int tileY, MapControl sender) {
			this.tileX = tileX;
			this.tileY = tileY;
			this.mapControl = sender;
		}
	}
}
