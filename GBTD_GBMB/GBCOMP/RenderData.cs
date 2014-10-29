using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBRenderer
{
	public partial class RenderData : UserControl
	{
		internal Color whiteColor = Color.White;
		internal Color lightGrayColor = Color.LightGray;
		internal Color darkGrayColor = Color.Gray;
		internal Color blackColor = Color.Black;

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

		internal byte[] data = {
								 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
							 };

		public RenderData() {
			InitializeComponent();
		}

		private void RenderData_Load(object sender, EventArgs e) {

		}

		private void RenderData_Paint(object sender, PaintEventArgs e) {
			byte color;

			for (byte y = 0; y <= 7; y++) {
				for (byte x = 0; x <= 7; x++) {
					color = 0;
					color |= ((data[2 * y] & (1 << x)) != 0 ? (byte)1 : (byte)0);
					color |= ((data[(2 * y) + 1] & (1 << x)) != 0 ? (byte)2 : (byte)0);

					drawPixel(x, y, color, e.Graphics);
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
		private void drawPixel(byte x, byte y, byte color, Graphics g) {
			if (x < 0 || x > 7) {
				throw new InvalidOperationException("x must be between 0 and 7; got " + x);
			}
			if (y < 0 || y > 7) {
				throw new InvalidOperationException("y must be between 0 and 7; got " + y);
			}
			if (color < 0 || color > 3) {
				throw new InvalidOperationException("color must be between 0 and 3; got " + color);
			}

			float x1 = (x * (this.Width / 8.0f));
			float y1 = (y * (this.Height / 8.0f));
			float width = (this.Width / 8.0f);
			float height = (this.Height / 8.0f);

			Color c = Color.Black;
			switch (color) {
			case 0: c = whiteColor; break;
			case 1: c = darkGrayColor; break;
			case 2: c = lightGrayColor; break;
			case 3: c = blackColor; break;
			}

			using (Brush brush = new SolidBrush(c)) {
				g.FillRectangle(brush, x1, y1, width, height);
			}
		}

		public byte[] getData() {
			return this.data;
		}

		public void setData(byte[] data) {
			if (data.Length != 16) {
				throw new InvalidOperationException("Data must be 16 bytes long; it is " + data.Length + ".");
			}

			this.data = data;
			this.Refresh();
		}
	}
}
