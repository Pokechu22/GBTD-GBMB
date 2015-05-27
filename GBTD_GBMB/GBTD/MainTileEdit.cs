using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GB.Shared.GBRFile;
using System.ComponentModel;
using GB.Shared.Palettes;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using GB.Shared.Tiles;
using System.Drawing.Drawing2D;

namespace GB.GBTD
{
	/// <summary>
	/// Draws the main tile-edit control.
	/// </summary>
	public class MainTileEdit : Control
	{
		private UInt16 selectedTile;
		private bool drawGrid;
		private bool drawNibbleMarkers;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public UInt16 SelectedTile {
			get { return selectedTile; }
			set {
				selectedTile = value;
				this.Invalidate(true);
			}
		}

		public bool DrawGrid {
			get { return drawGrid; }
			set {
				drawGrid = value;
				this.Invalidate(true);
			}
		}

		public bool DrawNibbleMarkers {
			get { return drawNibbleMarkers; }
			set {
				drawNibbleMarkers = value;
				this.Invalidate(true);
			}
		}

		private GBRObjectTileData tileset;
		private GBRObjectTilePalette paletteMapping;
		private GBRObjectPalettes palettes;
		private ColorSet colorSet;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBRObjectTileData TileSet {
			get { return tileset; }
			set { tileset = value; this.Invalidate(true); }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBRObjectTilePalette PaletteMapping {
			get { return paletteMapping; }
			set { paletteMapping = value; this.Invalidate(true); }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBRObjectPalettes Palettes {
			get { return palettes; }
			set { palettes = value; this.Invalidate(true); }
		}
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; this.Invalidate(true); }
		}

		public MainTileEdit() {
			DoubleBuffered = true;

			SetStyle(ControlStyles.ResizeRedraw, true);
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

			if (tileset == null) {
				e.Graphics.DrawString("tileset is null!", Font, Brushes.Red, new Point(0, 0));
			} else if (palettes == null) {
				e.Graphics.DrawString("palettes is null!", Font, Brushes.Red, new Point(0, 0));
			} else if (paletteMapping == null) {
				e.Graphics.DrawString("paletteMapping is null!", Font, Brushes.Red, new Point(0, 0));
			} else {
				using (Bitmap tileBitmap = MakeTileBitmap(tileset.Tiles[this.selectedTile],
						GetColor(ColorSet, selectedTile, GBColor.WHITE),
						GetColor(ColorSet, selectedTile, GBColor.LIGHT_GRAY),
						GetColor(ColorSet, selectedTile, GBColor.DARK_GRAY),
						GetColor(ColorSet, selectedTile, GBColor.BLACK))) {
					e.Graphics.DrawImage(tileBitmap, new Rectangle(0, 0, Width - 1, Height - 1));
				}
			}

			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;

			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, Width, Height), Color.Black, ButtonBorderStyle.Solid);

			if (drawGrid) {
				//Offset sizes.
				float w = this.Width - 1;
				float h = this.Height - 1;

				Tile tile = tileset.Tiles[selectedTile];

				for (UInt16 x = 1; x < tile.Width; x++) {
					e.Graphics.DrawLine(Pens.Black,
						x * (w / tile.Width),
						0,
						x * (w / tile.Width),
						this.Height);
				}
				for (UInt16 y = 1; y < tile.Height; y++) {
					e.Graphics.DrawLine(Pens.Black,
						0,
						y * (h / tile.Height),
						this.Width,
						y * (h / tile.Height));
				}
			}
			if (drawNibbleMarkers) {
				e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
				float w = this.Width - 1;
				float h = this.Height - 1;

				Tile tile = tileset.Tiles[selectedTile];

				for (UInt16 x = 0; x <= tile.Width; x += 4) {
					for (UInt16 y = 0; y <= tile.Height; y += 4) {
						int nx = (int)(x * (w / tile.Width));
						int ny = (int)(y * (h / tile.Height));

						e.Graphics.DrawLine(Pens.Blue, nx - 1, ny, nx + 1, ny);
						e.Graphics.DrawLine(Pens.Blue, nx, ny - 1, nx, ny + 1);
					}
				}
			}

			base.OnPaint(e);
		}

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
					int usedARGB = 0x00FF00;

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
		//// Gets the proper color for the given ColorSet.
		/// </summary>
		/// <param name="set"></param>
		/// <param name="Palette"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		private Color GetColor(ColorSet set, UInt16 tile, GBColor color) {
			switch (set) {
			case ColorSet.GAMEBOY_COLOR:
				return palettes.GBCPalettes[paletteMapping.GBCPalettes[tile]][color];
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				return palettes.GBCPalettes[paletteMapping.GBCPalettes[tile]][color].FilterWithGBC();
			case ColorSet.SUPER_GAMEBOY:
				return palettes.SGBPalettes[paletteMapping.SGBPalettes[tile]][color];
			case ColorSet.GAMEBOY:
				return color.GetNormalColor();
			case ColorSet.GAMEBOY_POCKET:
				return color.GetPocketColor();
			default: throw new InvalidEnumArgumentException("set", (int)set, typeof(ColorSet));
			}
		}
	}
}
