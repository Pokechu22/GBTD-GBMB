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

		#region Graphics/rendering
		const int FIRST_CELL_X = 4, FIRST_CELL_Y = 17;
		const int CELL_WIDTH = 21, CELL_HEIGHT = 19;

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

			DrawHeaderCell(e.Graphics, 0, 0);

			for (int x = 0; x < 8; x++) { //TODO unhardcode this.
				DrawHeaderCell(e.Graphics, x + 1, 0, x.ToString());
			}
			for (int y = 0; y < 4; y++) { //TODO unhardcode this.
				DrawHeaderCell(e.Graphics, 0, y + 1, y.ToString());
			}
			for (int y = 0; y < 4; y++) {
				for (int x = 0; x < 8; x++) {
					if (x == 0 && y == 0) { //TODO better selection test -- why can it even be selected?
						DrawSelectedEntryCell(e.Graphics, x + 1, y + 1, "1");
					} else {
						DrawEntryCell(e.Graphics, x + 1, y + 1, "2");
					}
				}
			}

			DrawGlobalBorder(e.Graphics, 4, 8);

			base.OnPaint(e);
		}

		/// <summary>
		/// Draws one of the header cells, optionally with text.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="cellX"></param>
		/// <param name="cellY"></param>
		/// <param name="text"></param>
		private void DrawHeaderCell(Graphics g, int cellX, int cellY, String text = "") {
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			g.DrawString(text, Font, SystemBrushes.ControlText, new RectangleF(FIRST_CELL_X + (cellX * CELL_WIDTH),
				FIRST_CELL_Y + (cellY * CELL_HEIGHT), CELL_WIDTH, CELL_HEIGHT - 2), format);

			BorderPaint.DrawBorderFull(g, FIRST_CELL_X + (cellX * CELL_WIDTH), FIRST_CELL_Y + (cellY * CELL_HEIGHT), CELL_WIDTH, CELL_HEIGHT,
				Color.Black, Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>
		/// Draws one of the selecterd entry cells, optionally with text.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="cellX"></param>
		/// <param name="cellY"></param>
		/// <param name="text"></param>
		private void DrawSelectedEntryCell(Graphics g, int cellX, int cellY, String text = "") {
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			g.FillRectangle(SystemBrushes.Highlight, FIRST_CELL_X + (cellX * CELL_WIDTH), FIRST_CELL_Y + (cellY * CELL_HEIGHT), 
				CELL_WIDTH, CELL_HEIGHT);

			g.DrawString(text, Font, SystemBrushes.HighlightText, new RectangleF(FIRST_CELL_X + (cellX * CELL_WIDTH), 
				FIRST_CELL_Y + (cellY * CELL_HEIGHT), CELL_WIDTH, CELL_HEIGHT - 2), format);

			BorderPaint.DrawBorderFull(g, FIRST_CELL_X + (cellX * CELL_WIDTH), FIRST_CELL_Y + (cellY * CELL_HEIGHT), CELL_WIDTH, CELL_HEIGHT,
				SystemColors.ControlLight, Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>
		/// Draws one of the entry cells, optionally with text.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="cellX"></param>
		/// <param name="cellY"></param>
		/// <param name="text"></param>
		private void DrawEntryCell(Graphics g, int cellX, int cellY, String text = "") {
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			g.FillRectangle(SystemBrushes.ControlLightLight, FIRST_CELL_X + (cellX * CELL_WIDTH), FIRST_CELL_Y + (cellY * CELL_HEIGHT),
				CELL_WIDTH, CELL_HEIGHT);

			g.DrawString(text, Font, SystemBrushes.ControlText, new RectangleF(FIRST_CELL_X + (cellX * CELL_WIDTH),
				FIRST_CELL_Y + (cellY * CELL_HEIGHT), CELL_WIDTH, CELL_HEIGHT - 2), format);

			BorderPaint.DrawBorderFull(g, FIRST_CELL_X + (cellX * CELL_WIDTH), FIRST_CELL_Y + (cellY * CELL_HEIGHT), CELL_WIDTH, CELL_HEIGHT,
				SystemColors.ControlLight, Border3DSide.Right | Border3DSide.Bottom);
		}

		/// <summary>
		/// Draws the border all the away around the cell area.
		/// Call this method AFTER all of the other drawings.
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rows"></param>
		/// <param name="cols"></param>
		private void DrawGlobalBorder(Graphics g, int rows, int cols) {
			BorderPaint.DrawBorderFull(g, FIRST_CELL_X - 1, FIRST_CELL_Y - 1, (CELL_WIDTH * (cols + 1)) + 1, (CELL_HEIGHT * (rows + 1)) + 1,
				Color.Black, Border3DSide.Left | Border3DSide.Right | Border3DSide.Top | Border3DSide.Bottom);
		}
		#endregion
	}
}
