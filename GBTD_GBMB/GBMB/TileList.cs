using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GB.Shared.Tiles;
using GB.Shared.GBRFile;
using GB.Shared.GBMFile;
using System.Drawing.Imaging;
using GB.Shared.Palettes;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace GB.GBMB
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
		//Currently unused :/
		private UInt16 bookmark1, bookmark2, bookmark3;

		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; this.Invalidate(true); }
		}
		public PaletteData PaletteData {
			get { return paletteData; }
			set { paletteData = value; this.Invalidate(true); }
		}
		public GBRObjectTilePalette PaletteMapping {
			get { return paletteMapping; }
			set { paletteMapping = value; this.Invalidate(true); }
		}
		public GBRObjectTileData TileSet {
			get { return tileSet; }
			set {
				tileSet = value;

				scrollBar.Enabled = (tileSet != null);
				scrollBar.Maximum = (tileSet != null ? tileSet.Count : 16) - 1;

				this.Invalidate(true);
			}
		}
		public UInt16 SelectedTile {
			get { return selectedTile; }
			set { selectedTile = value; this.Invalidate(); }
		}
		public UInt16 Bookmark1 { get { return bookmark1; } set { bookmark1 = value; this.Invalidate(true); } }
		public UInt16 Bookmark2 { get { return bookmark2; } set { bookmark2 = value; this.Invalidate(true); } }
		public UInt16 Bookmark3 { get { return bookmark3; } set { bookmark3 = value; this.Invalidate(true); } }

		private const int NUMBER_WIDTH = 21;
		private const int NUMBER_HEIGHT = 16;
		private const int TILE_X = NUMBER_WIDTH + 1;
		private const int TILE_Y = 0;
		private const int TILE_WIDTH = 16;
		private const int TILE_HEIGHT = 16;
		private const int INFO_WIDTH = NUMBER_WIDTH + TILE_WIDTH + 1;
		private const int INFO_HEIGHT = 17;

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
			this.scrollBar.Dock = DockStyle.Right;
			this.scrollBar.Enabled = false;
			this.scrollBar.Minimum = 0;
			this.scrollBar.Maximum = 15;
			this.scrollBar.LargeChange = 1; //Otherwise maximum doesn't work.
			this.scrollBar.SmallChange = 1;

			this.scrollBar.ValueChanged += new EventHandler((o, a) => this.Invalidate(true));
			this.ResumeLayout(false);
			this.Controls.Add(scrollBar);

			this.Width = INFO_WIDTH  + scrollBar.Width + 1;
		}

		protected override void OnResize(EventArgs e) {
			this.SuspendLayout();
			this.Width = INFO_WIDTH + scrollBar.Width + 1;
			this.numberOfVisibleTiles = ((this.Height - 2) / INFO_HEIGHT);
			this.Height = (numberOfVisibleTiles * INFO_HEIGHT) + 2;
			this.ResumeLayout();
			base.OnResize(e);
		}

		protected override void OnPaint(PaintEventArgs e) {
			Graphics g = e.Graphics;

			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half; //Fixes lines in the middle issue.
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
			
			using (StringFormat format = new StringFormat())
			using (Brush fore = new SolidBrush(this.BackColor), back = new SolidBrush(ControlPaint.Dark(this.BackColor))) {
				format.FormatFlags = StringFormatFlags.NoClip;
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Center;

				int scrolledPos = scrollBar.Value - numberOfVisibleTiles + 1;
				if (scrolledPos < 0) { scrolledPos = 0; }

				for (UInt16 i = 0; i < numberOfVisibleTiles; i++) {
					int tileNum = i + scrolledPos;

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

						g.DrawImage(MakeTileBitmap(TileSet.tiles[tileNum],
								GetApropriatelyFilteredColor(i, GBColor.WHITE),
								GetApropriatelyFilteredColor(i, GBColor.LIGHT_GRAY),
								GetApropriatelyFilteredColor(i, GBColor.DARK_GRAY),
								GetApropriatelyFilteredColor(i, GBColor.BLACK)),
							TILE_X + 1, TILE_Y + y, TILE_WIDTH, TILE_HEIGHT);
					} else {
						g.FillRectangle(fore, TILE_X + 1, TILE_Y + y, TILE_WIDTH, TILE_HEIGHT);
					}
				}
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

			if (tileNumber == SelectedTile) {
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
	}
}
