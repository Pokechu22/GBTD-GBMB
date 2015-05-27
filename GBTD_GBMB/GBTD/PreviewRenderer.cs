using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Tiles;
using System.Drawing;
using System.ComponentModel;
using GB.Shared.Palettes;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using GB.Shared.GBRFile;
using System.Drawing.Drawing2D;

namespace GB.GBTD
{
	public class PreviewRenderer : Control
	{
		private UInt16 selectedTile;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public UInt16 SelectedTile {
			get { return selectedTile; }
			set {
				selectedTile = value;
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
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; this.Invalidate(true); }
		}

		private Point smallLocation;
		private Point largeLocation;

		private bool simple;
		private int largeCount;

		/// <summary>
		/// Location of the small renderer.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public Point SmallLocation {
			get { return smallLocation; }
			set { smallLocation = value; this.Invalidate(true); }
		}
		/// <summary>
		/// Location of the large renderer.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public Point LargeLocation {
			get { return largeLocation; }
			set { largeLocation = value; this.Invalidate(true); }
		}
		/// <summary>
		/// Whether to hide the large renderer.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public bool Simple {
			get { return simple; }
			set { simple = value; this.Invalidate(true); }
		}
		/// <summary>
		/// The number of tiles to display in the large renderer.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public int LargeCount {
			get { return largeCount; }
			set { largeCount = value; this.Invalidate(true); }
		}

		public PreviewRenderer() {
			DoubleBuffered = true;

			SetStyle(ControlStyles.ResizeRedraw, true);
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

			if (tileset == null) {
				e.Graphics.DrawString("tileset is null!", Font, Brushes.Red, new Point(0, 0));
			} else if (palettes == null) {
				e.Graphics.DrawString("palettes is null!", Font, Brushes.Red, new Point(0, 0));
			} else if (paletteMapping == null) {
				e.Graphics.DrawString("paletteMapping is null!", Font, Brushes.Red, new Point(0, 0));
			} else {
				using (Bitmap tileBitmap = MakeTileBitmap(tileset.Tiles[selectedTile], 
						GetColor(colorSet, selectedTile, GBColor.WHITE),
						GetColor(colorSet, selectedTile, GBColor.LIGHT_GRAY),
						GetColor(colorSet, selectedTile, GBColor.DARK_GRAY), 
						GetColor(colorSet, selectedTile, GBColor.BLACK))) {

					e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
					e.Graphics.DrawRectangle(Pens.Black, new Rectangle(smallLocation, new Size((tileset.Width * 3) + 1, (tileset.Height * 3) + 1)));

					e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
					
					e.Graphics.DrawImage(tileBitmap, 
						new Rectangle(smallLocation.X + 1, smallLocation.Y + 1, (tileset.Width * 3), (tileset.Height * 3)));

					if (!simple) {
						e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
						e.Graphics.DrawRectangle(Pens.Black,
							new Rectangle(largeLocation, new Size((tileset.Width * 3 * largeCount) + 1, (tileset.Height * 3 * largeCount) + 1)));

						e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

						for (int x = 0; x < largeCount; x++) {
							for (int y = 0; y < largeCount; y++) {
								Rectangle tileRect = new Rectangle(
									largeLocation.X + 1 + (x * tileset.Width * 3),
									largeLocation.Y + 1 + (y * tileset.Height * 3),
									(tileset.Width * 3), (tileset.Height * 3));

								e.Graphics.DrawImage(tileBitmap, tileRect);
							}
						}
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
		/// Gets the proper color for the given ColorSet.
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
