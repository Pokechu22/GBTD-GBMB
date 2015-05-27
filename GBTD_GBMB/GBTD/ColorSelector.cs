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

namespace GB.GBTD
{
	public class ColorSelector : Control
	{
		protected override Size DefaultMaximumSize { get { return new Size(191, 26); } }
		protected override Size DefaultMinimumSize { get { return new Size(191, 26); } }
		protected override Size DefaultSize { get { return new Size(191, 26); } }

		private GBRObjectPalettes palettes;

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

		public event EventHandler OnColorChanged;

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			BorderPaint.DrawBorderFull(e.Graphics, 0, 0, Width, Height, SystemColors.ControlLightLight, Border3DSide.Left | Border3DSide.Top);
			BorderPaint.DrawBorderFull(e.Graphics, 0, 0, Width, Height, SystemColors.ControlDark, Border3DSide.Right | Border3DSide.Bottom);

			DrawMouseButtonDisplay(e, "L", leftColor, 2, 2);
			DrawMouseButtonDisplay(e, "R", rightColor, 39, 2);

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

			e.Graphics.FillRectangle(Brushes.White, x + 15, y + 1, 19, 19); //TODO: Get and use bkg color here.
			e.Graphics.DrawRectangle(Pens.Black, x + 15, y + 1, 19, 19);

			e.Graphics.DrawString("0", Font, Brushes.Black, new Rectangle(x + 19, y + 3, 11, 18), format); //TODO: Use real number.
		}

		protected override void OnResize(EventArgs e) {
			this.Size = new Size(191, 26);
			
			base.OnResize(e);
		}
	}
}
