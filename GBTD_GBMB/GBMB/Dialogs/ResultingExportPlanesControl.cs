using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

namespace GB.GBMB.Dialogs
{
	internal class ResultingExportPlanesControl : Control
	{
		protected override Size DefaultSize { get { return new Size(197, 116); } }

		protected override void OnPaint(PaintEventArgs e) {
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Near;

			BorderPaint.DrawBorderFull(e.Graphics, new Rectangle(0, 0, Width, Height), SystemColors.ControlLightLight,
				Border3DSide.Left | Border3DSide.Top);
			BorderPaint.DrawBorderFull(e.Graphics, new Rectangle(0, 0, Width, Height), SystemColors.ControlDark,
				Border3DSide.Right | Border3DSide.Bottom);

			e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			using (Brush brush = new SolidBrush(this.ForeColor)) {
				e.Graphics.DrawString(Text, Font, brush, new RectangleF(1, 1, Width - 4, Height - 2), format);
			}

			base.OnPaint(e);
		}
	}
}
