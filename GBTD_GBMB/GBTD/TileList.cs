using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GB.Shared.GBRFile;
using GB.Shared.Palettes;
using GB.Shared.Tiles;

namespace GB.GBTD
{
	class TileList : Control
	{
		[DefaultValue(""), Description("This is not useful for this control")]
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text { get { return base.Text; } set { base.Text = value; } }

		private int numberOfVisibleTiles = 0;

		private VScrollBar scrollBar;

		private ColorSet colorSet;
		private PaletteData paletteData;

		private GBRObjectTilePalette paletteMapping;
		private GBRObjectTileData tileSet;

		private UInt16 selectedTile;
		private UInt16 bookmark1 = 0xFFFF, bookmark2 = 0xFFFF, bookmark3 = 0xFFFF;

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; this.Invalidate(true); }
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteData PaletteData {
			get { return paletteData; }
			set { paletteData = value; this.Invalidate(true); }
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBRObjectTilePalette PaletteMapping {
			get { return paletteMapping; }
			set { paletteMapping = value; this.Invalidate(true); }
		}
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBRObjectTileData TileSet {
			get { return tileSet; }
			set {
				if (this.tileSet != null) {
					tileSet.SizeChanged -= new EventHandler(tileSet_SizeChanged);
				}
				tileSet = value;
				if (tileSet != null) {
					tileSet.SizeChanged += new EventHandler(tileSet_SizeChanged);
				}

				scrollBar.Enabled = (tileSet != null);
				scrollBar.Maximum = (tileSet != null ? tileSet.Count : 16) - 1;

				this.OnResize(new EventArgs());

				this.Invalidate(true);
			}
		}

		public UInt16 SelectedTile {
			get { return selectedTile; }
			set { selectedTile = value; this.Invalidate(true); }
		}
		[DefaultValue(typeof(UInt16), "65535")]
		public UInt16 Bookmark1 { get { return bookmark1; } set { bookmark1 = value; this.Invalidate(true); } }
		[DefaultValue(typeof(UInt16), "65535")]
		public UInt16 Bookmark2 { get { return bookmark2; } set { bookmark2 = value; this.Invalidate(true); } }
		[DefaultValue(typeof(UInt16), "65535")]
		public UInt16 Bookmark3 { get { return bookmark3; } set { bookmark3 = value; this.Invalidate(true); } }

		private Image bookmark1Icon = new Bitmap(5, 7);
		private Image bookmark2Icon = new Bitmap(5, 7);
		private Image bookmark3Icon = new Bitmap(5, 7);

		[Category("Display"), Description("The icon to use for bookmark number 1.")]
		public Image Bookmark1Icon {
			get { return bookmark1Icon; }
			set {
				if (value == null) { value = new Bitmap(5, 7); }
				bookmark1Icon = value;
				this.Invalidate(true);
			}
		}
		[Category("Display"), Description("The icon to use for bookmark number 2.")]
		public Image Bookmark2Icon {
			get { return bookmark2Icon; }
			set {
				if (value == null) { value = new Bitmap(5, 7); }
				bookmark2Icon = value;
				this.Invalidate(true);
			}
		}
		[Category("Display"), Description("The icon to use for bookmark number 3.")]
		public Image Bookmark3Icon {
			get { return bookmark3Icon; }
			set {
				if (value == null) { value = new Bitmap(5, 7); }
				bookmark3Icon = value;
				this.Invalidate(true);
			}
		}

		private int NUMBER_WIDTH { get { return 21; } }
		private int NUMBER_HEIGHT { get { return TILE_HEIGHT; } }
		private int TILE_X { get { return NUMBER_WIDTH + 1; } }
		private int TILE_Y { get { return 0; } }
		private int TILE_WIDTH { get { return (tileSet != null ? tileSet.Width * 2 : 16); } }
		private int TILE_HEIGHT { get { return (tileSet != null ? tileSet.Height * 2 : 16); } }
		private int INFO_WIDTH { get { return NUMBER_WIDTH + TILE_WIDTH + 1; } }
		private int INFO_HEIGHT { get { return NUMBER_HEIGHT + 1; } }
		private int SCROLL_X { get { return INFO_WIDTH + 2; } }
		private int SCROLL_Y { get { return 1; } }

		public TileList() {
			SetStyle(ControlStyles.FixedWidth, true);

			this.DoubleBuffered = true;
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw,
				true);

			this.scrollBar = new VScrollBar();
			this.SuspendLayout();
			this.scrollBar.Name = "scrollBar";
			this.scrollBar.TabIndex = 0;
			this.scrollBar.Enabled = false;
			this.scrollBar.Minimum = 0;
			this.scrollBar.Maximum = 15;
			this.scrollBar.LargeChange = 1; //Otherwise maximum doesn't work.
			this.scrollBar.SmallChange = 1;
			this.scrollBar.Location = new Point(SCROLL_X, SCROLL_Y);
			this.scrollBar.Height = this.Height - 2;

			this.scrollBar.ValueChanged += new EventHandler((o, a) => this.Invalidate(true));
			this.ResumeLayout(false);
			this.Controls.Add(scrollBar);

			this.Width = INFO_WIDTH  + scrollBar.Width + 2;
		}

		protected override void OnResize(EventArgs e) {
			this.SuspendLayout();

			this.Width = INFO_WIDTH + scrollBar.Width + 2;
			this.numberOfVisibleTiles = ((this.Height - 1) / INFO_HEIGHT);
			this.Height = (numberOfVisibleTiles * INFO_HEIGHT) + 1;
			this.scrollBar.Location = new Point(SCROLL_X, SCROLL_Y);
			this.scrollBar.Height = this.Height - 2;
			
			this.ResumeLayout();
			base.OnResize(e);
		}

		#region Graphics stuff
		protected override void OnPaint(PaintEventArgs e) {
			try {
				Graphics g = e.Graphics;

				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; //Fixes lines in the middle issue.
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

				using (StringFormat format = new StringFormat())
				using (Brush fore = new SolidBrush(this.BackColor), back = new SolidBrush(ControlPaint.DarkDark(this.BackColor))) {
					format.FormatFlags = StringFormatFlags.NoClip;
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Center;

					int scrolledPos = scrollBar.Value - numberOfVisibleTiles + 1;
					if (scrolledPos < 0) { scrolledPos = 0; }

					for (UInt16 i = 0; i < numberOfVisibleTiles; i++) {
						UInt16 tileNum = (UInt16)(i + scrolledPos);

						int y = 1 + (i * INFO_HEIGHT);

						//Fill background.
						g.FillRectangle(back, 1, y, INFO_WIDTH, INFO_HEIGHT);

						ControlPaint.DrawBorder3D(g, 1, y, NUMBER_WIDTH, NUMBER_HEIGHT, Border3DStyle.RaisedInner, Border3DSide.Left);
						ControlPaint.DrawBorder3D(g, 1, y, NUMBER_WIDTH, NUMBER_HEIGHT, Border3DStyle.RaisedInner, Border3DSide.Bottom);
						ControlPaint.DrawBorder3D(g, 1, y, NUMBER_WIDTH, NUMBER_HEIGHT, Border3DStyle.RaisedInner, Border3DSide.Right);
						ControlPaint.DrawBorder3D(g, 1, y, NUMBER_WIDTH, NUMBER_HEIGHT, Border3DStyle.RaisedInner, Border3DSide.Top);
						//The inside part.
						g.FillRectangle(fore, 2, 1 + y, NUMBER_WIDTH - 2, NUMBER_HEIGHT - 2);

						if (this.Enabled && tileSet != null && tileNum < tileSet.Count) {
							g.DrawString(tileNum.ToString(), new Font(DefaultFont.FontFamily, 7.5f), Brushes.Black,
								new RectangleF(1, -1 + y, NUMBER_WIDTH, NUMBER_HEIGHT), format);

							//Tile bookmark number.
							if (tileNum == bookmark1) {
								g.DrawImageUnscaledAndClipped(bookmark1Icon, new Rectangle(2, y + 1, 5, 7));
							} else if (tileNum == bookmark2) {
								g.DrawImageUnscaledAndClipped(bookmark2Icon, new Rectangle(2, y + 1, 5, 7));
							} else if (tileNum == bookmark3) {
								g.DrawImageUnscaledAndClipped(bookmark3Icon, new Rectangle(2, y + 1, 5, 7));
							}

							//The tile itself.
							g.DrawImage(MakeTileBitmap(TileSet.Tiles[tileNum],
									GetApropriatelyFilteredColor(tileNum, GBColor.WHITE),
									GetApropriatelyFilteredColor(tileNum, GBColor.LIGHT_GRAY),
									GetApropriatelyFilteredColor(tileNum, GBColor.DARK_GRAY),
									GetApropriatelyFilteredColor(tileNum, GBColor.BLACK)),
								TILE_X + 1, TILE_Y + y, TILE_WIDTH, TILE_HEIGHT);
						} else {
							g.FillRectangle(fore, TILE_X + 1, TILE_Y + y, TILE_WIDTH, TILE_HEIGHT);
						}
					}
				}

				ControlPaint.DrawBorder3D(g, new Rectangle(Point.Empty, this.Size), Border3DStyle.SunkenOuter, Border3DSide.Bottom);
				ControlPaint.DrawBorder3D(g, new Rectangle(Point.Empty, this.Size), Border3DStyle.SunkenOuter, Border3DSide.Top | Border3DSide.Left);
				ControlPaint.DrawBorder3D(g, new Rectangle(SCROLL_X - 1, SCROLL_Y - 1, scrollBar.Width + 1, this.Height), Border3DStyle.SunkenOuter, Border3DSide.Top | Border3DSide.Left);
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
			base.OnPaint(e);
		}

		/// <summary>
		/// Fast creation of a bitmap for tiles, using marshaling and stuff.
		/// </summary>
		/// <param name="tile"></param>
		/// <param name="white"></param>
		/// <param name="lightGray"></param>
		/// <param name="darkGray"></param>
		/// <param name="black"></param>
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

			bool canFlip = ColorSet.SupportsTileFlipping();

			for (int y = 0; y < height; y++) {
				IntPtr outputScan = (IntPtr)((long)outputData.Scan0 + (y * outputData.Stride));
				for (int x = 0; x < width; x++) {
					int usedARGB = 0;

					switch (tile[x, y]) {
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
		private Color GetApropriatelyFilteredColor(UInt16 tileNumber, GBColor color) {
			Color returned = GetColor(ColorSet, tileNumber, color);

			if (this.Enabled && tileSet != null && tileNumber == SelectedTile) {
				returned = returned.FilterAsSelected();
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
		private Color GetColor(ColorSet set, UInt16 tileNumber, GBColor color) {
			switch (set) {
			case ColorSet.GAMEBOY_COLOR:
				if (paletteMapping != null) {
					return PaletteData.GBCPaletteSet[paletteMapping.GBCPalettes[tileNumber]][color];
				} else {
					return PaletteData.GBCPaletteSet[0][color];
				}
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				if (paletteMapping != null) {
					return PaletteData.GBCPaletteSet[paletteMapping.GBCPalettes[tileNumber]][color].FilterWithGBC();
				} else {
					return PaletteData.GBCPaletteSet[0][color].FilterWithGBC();
				}
			case ColorSet.SUPER_GAMEBOY:
				if (paletteMapping != null) {
					return PaletteData.SGBPaletteSet[paletteMapping.SGBPalettes[tileNumber]][color];
				} else {
					return PaletteData.SGBPaletteSet[0][color];
				}
			case ColorSet.GAMEBOY:
				return color.GetNormalColor();
			case ColorSet.GAMEBOY_POCKET:
				return color.GetPocketColor();
			default: throw new InvalidEnumArgumentException("set", (int)set, typeof(ColorSet));
			}
		}
		#endregion

		[Category("Action"), Description("Fires when the selected tile is changed")]
		public event EventHandler SelectedTileChanged;

		protected virtual void OnSelectedTileChanged() {
			if (SelectedTileChanged != null) {
				SelectedTileChanged(this, new EventArgs());
			}
		}

		protected override void OnMouseClick(MouseEventArgs e) {
			int tileClicked = (e.Y - 2) / INFO_HEIGHT; //The clicked tile number, which may not be the actual tile (scrolling).
			if (tileClicked >= 0 && tileClicked < numberOfVisibleTiles) {
				int scrolledPos = scrollBar.Value - numberOfVisibleTiles + 1;
				if (scrolledPos < 0) { scrolledPos = 0; }

				this.selectedTile = (UInt16)(tileClicked + scrolledPos);

				this.Invalidate(true);
				OnSelectedTileChanged();
			}

			base.OnMouseClick(e);
		}

		void tileSet_SizeChanged(object sender, EventArgs e) {
			this.OnResize(e);
		}
	}
}
