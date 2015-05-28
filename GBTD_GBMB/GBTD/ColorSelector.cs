using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GB.Shared.Tiles;
using System.ComponentModel;
using GB.Shared.GBRFile;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using GB.Shared.Palettes;

namespace GB.GBTD
{
	public class ColorSelector : Control
	{
		protected override Size DefaultMaximumSize { get { return new Size(191, 26); } }
		protected override Size DefaultMinimumSize { get { return new Size(191, 26); } }
		protected override Size DefaultSize { get { return new Size(191, 26); } }

		private UInt16 selectedTile;
		private ColorSet colorSet;
		private GBRObjectPalettes palettes;
		private GBRObjectTilePalette paletteMapping;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public UInt16 SelectedTile {
			get { return selectedTile; }
			set {
				selectedTile = value;
				this.Invalidate(true);
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; this.Invalidate(true); }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBRObjectPalettes Palettes {
			get { return palettes; }
			set {
				palettes = value;
				this.Invalidate(true);
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBRObjectTilePalette PaletteMapping {
			get { return paletteMapping; }
			set {
				paletteMapping = value;
				this.Invalidate(true);
			}
		}

		private GBColor leftColor, rightColor, middleColor, x1Color, x2Color;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor LeftColor {
			get { return leftColor; }
			set {
				if (value != leftColor) {
					leftColor = value;
					if (OnColorChanged != null) { OnColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor RightColor {
			get { return rightColor; }
			set {
				if (value != rightColor) {
					rightColor = value;
					if (OnColorChanged != null) { OnColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor MiddleColor {
			get { return middleColor; }
			set {
				if (value != middleColor) {
					middleColor = value;
					if (OnColorChanged != null) { OnColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor X1Color {
			get { return x1Color; }
			set {
				if (value != x1Color) {
					x1Color = value;
					if (OnColorChanged != null) { OnColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor X2Color {
			get { return x2Color; }
			set {
				if (value != x2Color) {
					x2Color = value;
					if (OnColorChanged != null) { OnColorChanged(this, new EventArgs()); }
				}
			}
		}

		public ColorSelector() {
			DoubleBuffered = true;

			SetStyle(ControlStyles.ResizeRedraw, true);
		}

		public event EventHandler OnColorChanged;

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			if (palettes == null) {
				e.Graphics.DrawString("palettes is null!", Font, Brushes.Red, new Point(0, 0));
			} else if (paletteMapping == null) {
				e.Graphics.DrawString("paletteMapping is null!", Font, Brushes.Red, new Point(0, 0));
			} else {
				BorderPaint.DrawBorderFull(e.Graphics, 0, 0, Width, Height, SystemColors.ControlLightLight, Border3DSide.Left | Border3DSide.Top);
				BorderPaint.DrawBorderFull(e.Graphics, 0, 0, Width, Height, SystemColors.ControlDark, Border3DSide.Right | Border3DSide.Bottom);

				DrawMouseButtonDisplay(e, "L", leftColor, 2, 2);
				DrawMouseButtonDisplay(e, "R", rightColor, 39, 2);
			}

			base.OnPaint(e);
		}

		private void DrawMouseButtonDisplay(PaintEventArgs e, String text, GBColor color, int x, int y) {
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;

			BorderPaint.DrawBorderFull(e.Graphics, x, y, 36, 22, SystemColors.ControlDark, Border3DSide.Left | Border3DSide.Top);
			BorderPaint.DrawBorderFull(e.Graphics, x, y, 36, 22, SystemColors.ControlLightLight, Border3DSide.Right | Border3DSide.Bottom);

			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			e.Graphics.DrawString(text, Font, Brushes.Black, new Rectangle(x + 3, y + 3, 11, 18), format);

			int colorNum;
			switch (color) {
			case GBColor.WHITE: colorNum = 0; break;
			case GBColor.LIGHT_GRAY: colorNum = 1; break;
			case GBColor.DARK_GRAY: colorNum = 2; break;
			case GBColor.BLACK: colorNum = 3; break;
			default: colorNum = (int)color; break;
			}

			using (SolidBrush brush = new SolidBrush(GetColor(colorSet, selectedTile, color))) {
				e.Graphics.FillRectangle(brush, x + 15, y + 1, 19, 19);
			}
			e.Graphics.DrawRectangle(Pens.Black, x + 15, y + 1, 19, 19);

			e.Graphics.DrawString(colorNum.ToString(), Font, Brushes.Black, new Rectangle(x + 19, y + 3, 11, 18), format);
		}

		protected override void OnResize(EventArgs e) {
			this.Size = new Size(191, 26);
			
			base.OnResize(e);
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
