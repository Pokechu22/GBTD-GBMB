using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Tile;

namespace GBRenderer
{
	public partial class RenderData : UserControl
	{
		internal Color whiteColor = Color.White;
		internal Color lightGrayColor = Color.LightGray;
		internal Color darkGrayColor = Color.Gray;
		internal Color blackColor = Color.Black;

		internal Tile tile = new Tile();

		/// <summary>
		/// The whitemost color.
		/// </summary>
		public Color WhiteColor {
			get {
				return whiteColor;
			}
			set {
				whiteColor = value;
			}
		}

		/// <summary>
		/// The light-gray color.
		/// </summary>
		public Color LightGrayColor {
			get {
				return lightGrayColor;
			}
			set {
				lightGrayColor = value;
			}
		}

		/// <summary>
		/// The dark-gray color.
		/// </summary>
		public Color DarkGrayColor {
			get {
				return darkGrayColor;
			}
			set {
				darkGrayColor = value;
			}
		}

		/// <summary>
		/// The black color.
		/// </summary>
		public Color BlackColor {
			get {
				return blackColor;
			}
			set {
				blackColor = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tile Tile {
			get {
				return tile;
			}
			set {
				this.tile = value;
				this.Refresh();
			}
		}

		public RenderData() {
			InitializeComponent();
		}

		private void RenderData_Load(object sender, EventArgs e) {

		}

		private void RenderData_Paint(object sender, PaintEventArgs e) {
			for (byte x = 0; x < 8; x++) {
				for (byte y = 0; y < 8; y++) {
					drawPixel(x, y, tile[x, y], e.Graphics);
				}
			}
		}

		/// <summary>
		/// Draws a pixel.
		/// </summary>
		/// <param name="x">x; between 0 and 7</param>
		/// <param name="y">y; between 0 and 7</param>
		/// <param name="color">color; between 0 and 3</param>
		/// <param name="g">Graphics.</param>
		private void drawPixel(byte x, byte y, GBColor color, Graphics g) {
			if (x < 0 || x > 7) {
				throw new InvalidOperationException("x must be between 0 and 7; got " + x);
			}
			if (y < 0 || y > 7) {
				throw new InvalidOperationException("y must be between 0 and 7; got " + y);
			}

			float x1 = (x * (this.Width / 8.0f));
			float y1 = (y * (this.Height / 8.0f));
			float width = (this.Width / 8.0f);
			float height = (this.Height / 8.0f);

			Color c = Color.Black;
			switch (color) {
			case GBColor.WHITE: c = whiteColor; break;
			case GBColor.DARK_GRAY: c = darkGrayColor; break;
			case GBColor.LIGHT_GRAY: c = lightGrayColor; break;
			case GBColor.BLACK: c = blackColor; break;
			}

			using (Brush brush = new SolidBrush(c)) {
				g.FillRectangle(brush, x1, y1, width, height);
			}
		}
	}
}
