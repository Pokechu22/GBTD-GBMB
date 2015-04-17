using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GB.GBMB
{
	internal static class BorderPaint
	{
		/// <summary>
		/// Draws a border.  The bottom and right sides have 1 pixel "Clipped", for the other border side.
		/// Use this method if you are making a full border.
		/// </summary>
		public static void DrawBorderClipped(Graphics g, int x, int y, int width, int height, Color color, Border3DSide sides) {
			DrawBorderClipped(g, new Rectangle(x, y, width, height), color, sides);
		}

		/// <summary>
		/// Draws a border.  The bottom and right sides have 1 pixel "Clipped", for the other border side.
		/// Use this method if you are making a full border.
		/// </summary>
		public static void DrawBorderClipped(Graphics g, Rectangle rect, Color color, Border3DSide sides) {
			using (Pen p = new Pen(color)) {
				if (sides.HasFlag(Border3DSide.Top)) {
					g.DrawLine(p, rect.Left, rect.Top, rect.Right - 1, rect.Top);
				}
				if (sides.HasFlag(Border3DSide.Left)) {
					g.DrawLine(p, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
				}
				if (sides.HasFlag(Border3DSide.Bottom)) {
					g.DrawLine(p, rect.Left + 1, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
				}
				if (sides.HasFlag(Border3DSide.Right)) {
					g.DrawLine(p, rect.Right - 1, rect.Top + 1, rect.Right - 1, rect.Bottom - 1);
				}
			}
		}

		/// <summary>
		/// Draws a border all the way around.
		/// </summary>
		public static void DrawBorderFull(Graphics g, int x, int y, int width, int height, Color color, Border3DSide sides) {
			DrawBorderFull(g, new Rectangle(x, y, width, height), color, sides);
		}

		/// <summary>
		/// Draws a border all the way around.
		/// </summary>
		public static void DrawBorderFull(Graphics g, Rectangle rect, Color color, Border3DSide sides) {
			using (Pen p = new Pen(color)) {
				if (sides.HasFlag(Border3DSide.Top)) {
					g.DrawLine(p, rect.Left, rect.Top, rect.Right - 1, rect.Top);
				}
				if (sides.HasFlag(Border3DSide.Left)) {
					g.DrawLine(p, rect.Left, rect.Top, rect.Left, rect.Bottom - 1);
				}
				if (sides.HasFlag(Border3DSide.Bottom)) {
					g.DrawLine(p, rect.Left, rect.Bottom - 1, rect.Right - 1, rect.Bottom - 1);
				}
				if (sides.HasFlag(Border3DSide.Right)) {
					g.DrawLine(p, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom - 1);
				}
			}
		}
	}
}
