using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Tile
{
	public partial class TileRenderer : UserControl
	{
		#region Internal members
		protected internal Color whiteColor = Color.White;
		protected internal Color lightGrayColor = Color.LightGray;
		protected internal Color darkGrayColor = Color.Gray;
		protected internal Color blackColor = Color.Black;

		protected internal Tile tile = new Tile();
		#endregion

		#region Public properties
		/// <summary>
		/// The whitemost color.
		/// </summary>
		public Color WhiteColor {
			get {
				return whiteColor;
			}
			set {
				whiteColor = value;
				OnPalatteChange();
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
				OnPalatteChange();
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
				OnPalatteChange();
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
				OnPalatteChange();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tile Tile {
			get {
				return tile;
			}
			set {
				this.tile = value;
				OnTileChange();
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Fires when the currently-used tile is changed.
		/// </summary>
		[Category("Property Changed"), Description("Fires when the currently-used tile is changed.")]
		public event EventHandler TileChanged;

		/// <summary>
		/// Fires when the currently-used palatte is changed.
		/// </summary>
		[Category("Property Changed"), Description("Fires when the currently-used palatte is changed.")]
		public event EventHandler PalatteChanged;

		protected void OnPalatteChange() {
			if (PalatteChanged != null) {
				PalatteChanged(this, new EventArgs());
			}
			this.Refresh();
		}

		protected void OnTileChange() {
			if (TileChanged != null) {
				TileChanged(this, new EventArgs());
			}
			this.Refresh();
		}
		#endregion

		public TileRenderer() {
			InitializeComponent();
		}

		private void TileRenderer_Paint(object sender, PaintEventArgs e) {
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

		/// <summary>
		/// Ensure that it is divisible evenly by 8.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileRenderer_Resize(object sender, EventArgs e) {
			this.Resize -= new EventHandler(TileRenderer_Resize);
			this.Width &= ~0x07;
			this.Height &= ~0x07;
			this.Resize += new EventHandler(TileRenderer_Resize);
		}
	}
}

