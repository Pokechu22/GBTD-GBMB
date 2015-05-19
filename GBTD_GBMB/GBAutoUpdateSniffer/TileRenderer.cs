using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palettes;
using GB.Shared.Tiles;

namespace GBAutoUpdateSniffer
{
	public class TileRenderer : Control
	{
		protected override Size DefaultSize { get { return new Size(8, 8); } }

		#region Private members
		private Tile tile = new Tile(8, 8);

		private byte clickedX = 0, clickedY = 0;
		private MouseButtons buttons;
		#endregion

		#region Public properties
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tile Tile {
			get {
				return tile;
			}
			set {
				this.tile = value;
				OnTileChange(new EventArgs());
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Fires when the currently-used tileData is changed.
		/// </summary>
		[Category("Property Changed"), Description("Fires when the currently-used tileData is changed.")]
		public event EventHandler TileChanged;

		/// <summary>
		/// Fires when a pixel in the tileData is clicked.  
		/// Use this over OnClick.
		/// </summary>
		[Category("Action"), Description("Fires when a pixel in the tileData is clicked.  Use this over OnClick.")]
		public event PixelClickedEventHandler PixelClicked;

		protected virtual void OnTileChange(EventArgs e) {
			if (TileChanged != null) {
				TileChanged(this, e);
			}
			this.Invalidate(true);
		}

		protected virtual void OnPixelClicked(PixelClickEventArgs e) {
			if (PixelClicked != null) {
				PixelClicked(this, e);
			}
			this.Invalidate(true);
		}
		#endregion

		public TileRenderer() {
			SetStyle(ControlStyles.FixedHeight, true);
			SetStyle(ControlStyles.FixedWidth, true);

			this.DoubleBuffered = true;

			//Required for it to update when not focused.  http://stackoverflow.com/a/2326001/3991344
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw,
				true);
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

			for (byte x = 0; x < this.Tile.Width; x++) {
				for (byte y = 0; y < this.Tile.Height; y++) {
					drawPixel(x, y, Tile[x, y], e.Graphics);
				}
			}

			//Offset sizes.
			float w = this.Width - 1;
			float h = this.Height - 1;

			for (UInt16 x = 1; x < this.Tile.Width; x++) {
				e.Graphics.DrawLine(Pens.Black,
					x * (w / this.Tile.Width),
					0,
					x * (w / this.Tile.Width),
					this.Height);
			}
			for (UInt16 y = 1; y < this.Tile.Height; y++) {
				e.Graphics.DrawLine(Pens.Black,
					0,
					y * (h / this.Tile.Height),
					this.Width,
					y * (h / this.Tile.Height));
			}
			ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), Color.Black, ButtonBorderStyle.Solid);
			
			base.OnPaint(e);
		}

		/// <summary>
		/// Draws a pixel.
		/// </summary>
		/// <param name="x">x; between 0 and 7</param>
		/// <param name="y">y; between 0 and 7</param>
		/// <param name="color">color; between 0 and 3</param>
		/// <param name="g">Graphics.</param>
		private void drawPixel(byte x, byte y, GBColor color, Graphics g) {
			if (x < 0 || x >= this.Tile.Width) {
				throw new ArgumentOutOfRangeException("x", x, "Must be between 0 and " + (this.Tile.Width - 1) + ".");
			}
			if (y < 0 || y >= this.Tile.Height) {
				throw new ArgumentOutOfRangeException("y", y, "Must be between 0 and " + (this.Tile.Height - 1) + ".");
			}

			float x1 = (x * ((this.Width - 1) / (float)this.Tile.Width)) + 1;
			float y1 = (y * ((this.Height - 1) / (float)this.Tile.Height)) + 1;

			float width = ((this.Width - 1) / this.Tile.Width);
			float height = ((this.Height - 1) / this.Tile.Height);

			Color c;
			if (Enabled) {
				switch (color) {
				case GBColor.BLACK: c = Color.Black; break;
				case GBColor.DARK_GRAY: c = Color.DarkGray; break;
				case GBColor.LIGHT_GRAY: c = Color.LightGray; break;
				case GBColor.WHITE: c = Color.White; break;
				default: c = Color.Lime; break;
				}
			} else {
				c = SystemColors.Control;
			}

			using (Brush brush = new SolidBrush(c)) {
				g.FillRectangle(brush, x1, y1, width, height);
			}
		}

		protected internal byte getClickedPixelX(int clickedX) {
			byte returned = (byte)(((clickedX - 1) * this.Tile.Width) / this.Width);

			if (returned < 0 || returned >= this.Tile.Width) {
				throw new InvalidOperationException("Mouse out of bounds.");
			}
			return returned;
		}

		protected internal byte getClickedPixelY(int clickedY) {
			byte returned = (byte)(((clickedY - 1) * this.Tile.Height) / this.Height);

			if (returned < 0 || returned >= this.Tile.Height) {
				throw new InvalidOperationException("Mouse out of bounds.");
			}
			return returned;
		}

		protected override void OnMouseMove(MouseEventArgs e) {
			base.OnMouseMove(e);
			OnMouseDoSomething(e);
		}
		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			OnMouseDoSomething(e);
		}
		protected override void OnMouseUp(MouseEventArgs e) {
			base.OnMouseUp(e);
			OnMouseDoSomething(e);
		}

		/// <summary>
		/// Called when something about the mouse is changed: Mouse buttons or position.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMouseDoSomething(MouseEventArgs e) {
			byte x = 0, y = 0;
			MouseButtons buttons;
			try {
				x = getClickedPixelX(e.X);
				y = getClickedPixelY(e.Y);
				buttons = e.Button;
			} catch (InvalidOperationException) {
				return;
			}

			if (x != this.clickedX || y != this.clickedY || buttons != this.buttons) {
				this.clickedX = x;
				this.clickedY = y;
				this.buttons = buttons;

				if (e.Button != MouseButtons.None) {
					OnPixelClicked(new PixelClickEventArgs(x, y, buttons));
				}
			}
		}
	}

	/// <summary>
	/// EventArgs for anything dealing with a tileData's pixel.
	/// </summary>
	public class PixelEventArgs : EventArgs
	{
		public readonly byte x;
		public readonly byte y;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public PixelEventArgs(byte x, byte y) {
			this.x = x;
			this.y = y;
		}
	}

	/// <summary>
	/// EventArgs for when a pixel is clicked on.
	/// </summary>
	public class PixelClickEventArgs : PixelEventArgs
	{
		public readonly MouseButtons mouseButton;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="button"></param>
		public PixelClickEventArgs(byte x, byte y, MouseButtons mouseButton)
			: base(x, y) {
			this.mouseButton = mouseButton;
		}
	}

	public delegate void PixelClickedEventHandler(object sender, PixelClickEventArgs e);
}

