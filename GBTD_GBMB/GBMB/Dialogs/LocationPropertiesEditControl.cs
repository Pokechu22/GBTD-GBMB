using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using GB.Shared.GBMFile;

namespace GB.GBMB.Dialogs
{
	internal class LocationPropertiesEditControl : Control
	{
		protected override Size DefaultSize { get { return new Size(241, 174); } }

		private GBMObjectMapPropertiesRecord[] properties;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBMObjectMapPropertiesRecord[] Properties {
			get { return properties; }
			set { properties = value; this.Invalidate(); }
		}

		public LocationPropertiesEditControl() {
			SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
		}

		protected override void OnPaint(PaintEventArgs e) {
			const int LEFT_X = 2, TOP_Y = 2; //TopLeft coords (due to border).
			const int BOX_HEIGHT = 19;
			const int NUMBER_X = LEFT_X + 0, NUMBER_WIDTH = 21;
			const int NAME_X = LEFT_X + 21, NAME_WIDTH = 121;
			const int MAX_X = LEFT_X + 142, MAX_WIDTH = 50;
			const int BITS_X = LEFT_X + 192, BITS_WIDTH = 45;

			base.OnPaint(e);

			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(Point.Empty, this.Size), Border3DStyle.Sunken, 
				Border3DSide.Bottom | Border3DSide.Right | Border3DSide.Top | Border3DSide.Left);

			DrawTextRect(e.Graphics, "", NUMBER_X, TOP_Y, NUMBER_WIDTH, BOX_HEIGHT);
			DrawTextRect(e.Graphics, "Name", NAME_X, TOP_Y, NAME_WIDTH, BOX_HEIGHT);
			DrawTextRect(e.Graphics, "Max", MAX_X, TOP_Y, MAX_WIDTH, BOX_HEIGHT);
			DrawTextRect(e.Graphics, "Bits", BITS_X, TOP_Y, BITS_WIDTH, BOX_HEIGHT);
		}

		private void DrawTextRect(Graphics g, String text, int x, int y, int width, int height) {
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			Rectangle OuterBorderRect = new Rectangle(x, y, width, height);
			Rectangle InnerBorderRect = new Rectangle(x, y, width - 1, height - 1);
			Rectangle TextRect = new Rectangle(x + 0, y + 1, width - 2, height - 3);

			g.FillRectangle(SystemBrushes.Control, OuterBorderRect);

			BorderPaint.DrawBorderFull(g, OuterBorderRect, Color.Black, Border3DSide.Bottom | Border3DSide.Right);
			BorderPaint.DrawBorderClipped(g, InnerBorderRect, Color.White, Border3DSide.Left | Border3DSide.Top);
			BorderPaint.DrawBorderClipped(g, InnerBorderRect, SystemColors.ControlDark, Border3DSide.Right | Border3DSide.Bottom);

			g.DrawString(text, this.Font, SystemBrushes.ControlText, TextRect, format);
		}

		private void DrawEditRect(Graphics g, int x, int y, int width, int height) {
			Color color = Color.FromArgb(192, 192, 192);

			BorderPaint.DrawBorderFull(g, new Rectangle(x, y, width, height), color, Border3DSide.Right | Border3DSide.Bottom);
		}
	}
}
