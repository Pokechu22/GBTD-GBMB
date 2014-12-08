﻿using System;
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

		protected internal byte clickedX = 0, clickedY = 0;
		protected internal MouseButtons buttons = MouseButtons.None;

		protected bool grid = false;
		protected bool border = true;

		protected Border3DSide borderSides = Border3DSide.All;
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

		[Category("Display"), Description("Controls drawing of a pixel grid.")]
		public bool Grid {
			get {
				return grid;
			}
			set {
				this.grid = value;
				this.Refresh();
			}
		}

		[Category("Display"), Description("Controls drawing of a border around the control.  Also effects the size.")]
		public bool Border {
			get {
				return border;
			}
			set {
				border = value;
				this.OnResize(new EventArgs());
				this.Refresh();
			}
		}

		[Category("Display"), Description("Controls drawing of a border around the control.  Also effects the size.")]
		public Border3DSide BorderSides {
			get {
				return borderSides;
			}
			set {
				borderSides = value;
				this.OnResize(new EventArgs());
				this.Refresh();
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

		/// <summary>
		/// Fires when a pixel in the tile is clicked.  
		/// Use this over OnClick.
		/// </summary>
		[Category("Action"), Description("Fires when a pixel in the tile is clicked.  Use this over OnClick.")]
		public event PixelClickedEvent PixelClicked;

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

		/// <summary>
		/// Sets all colors on this at once.
		/// Use this over setting each value individually, as otherwise you will end up redrawing several times, resulting in lag.
		/// </summary>
		/// <param name="black"></param>
		/// <param name="darkgray"></param>
		/// <param name="lightgray"></param>
		/// <param name="white"></param>
		public void SetColors(Color black, Color darkgray, Color lightgray, Color white) {
			this.blackColor = black;
			this.darkGrayColor = darkgray;
			this.lightGrayColor = lightgray;
			this.whiteColor = white;

			OnPalatteChange();
		}

		private void TileRenderer_Paint(object sender, PaintEventArgs e) {
			for (byte x = 0; x < 8; x++) {
				for (byte y = 0; y < 8; y++) {
					drawPixel(x, y, tile[x, y], e.Graphics);
				}
			}

			//Offset sizes.
			int w = this.Width - ((border && borderSides.HasFlag(Border3DSide.Right)) ? 1 : 0);
			int h = this.Height - ((border && borderSides.HasFlag(Border3DSide.Bottom)) ? 1 : 0);

			if (grid) {
				for (byte i = 1; i < 8; i++) {
					e.Graphics.DrawLine(Pens.Black,
						i * (w / 8.0f),
						0,
						i * (w / 8.0f),
						this.Height);
					e.Graphics.DrawLine(Pens.Black,
						0,
						i * (h / 8.0f),
						this.Width,
						i * (h / 8.0f));
				}
			}
			if (border) {
				ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), 
					Color.Black, borderSides.HasFlag(Border3DSide.Left) ? 1 : 0, ButtonBorderStyle.Solid,
					Color.Black, borderSides.HasFlag(Border3DSide.Top) ? 1 : 0, ButtonBorderStyle.Solid,
					Color.Black, borderSides.HasFlag(Border3DSide.Right) ? 1 : 0, ButtonBorderStyle.Solid,
					Color.Black, borderSides.HasFlag(Border3DSide.Bottom) ? 1 : 0, ButtonBorderStyle.Solid);
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

			float x1 = (x * ((this.Width - (border ? 1 : 0)) / 8.0f));
			float y1 = (y * ((this.Height - (border ? 1 : 0)) / 8.0f));

			if (border) {
				x1++;
				y1++;
			}

			int w = this.Width - ((border && borderSides.HasFlag(Border3DSide.Right)) ? 1 : 0);
			int h = this.Height - ((border && borderSides.HasFlag(Border3DSide.Bottom)) ? 1 : 0);

			float width = (w / 8.0f);
			float height = (h / 8.0f);

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

			if (this.border) {
				if (this.borderSides.HasFlag(Border3DSide.Right)) {
					this.Width++;
				}
				if (this.borderSides.HasFlag(Border3DSide.Bottom)) {
					this.Height++;
				}
			}
			this.Resize += new EventHandler(TileRenderer_Resize);
		}

		protected internal byte getClickedPixelX(int clickedX) {
			int offset = ((border && borderSides.HasFlag(Border3DSide.Right)) ? 1 : 0);

			byte returned = (byte)(((clickedX - offset) * 8) / this.Width);

			if (returned < 0 || returned >= 8) {
				throw new InvalidOperationException("Mouse out of bounds.");
			}
			return returned;
		}

		protected internal byte getClickedPixelY(int clickedY) {
			int offset = ((border && borderSides.HasFlag(Border3DSide.Bottom)) ? 1 : 0);

			byte returned = (byte)(((clickedY - offset) * 8) / this.Height);

			if (returned < 0 || returned >= 8) {
				throw new InvalidOperationException("Mouse out of bounds.");
			}
			return returned;
		}

		/// <summary>
		/// Called when something about the mouse is changed: Mouse buttons or position.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileRenderer_MouseChanged(object sender, MouseEventArgs e) {
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
					if (PixelClicked != null) {
						PixelClicked(this, new PixelClickEventArgs(x, y, buttons));
					}

					this.Refresh();
				}
			}
		}
	}

	/// <summary>
	/// EventArgs for anything dealing with a tile's pixel.
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

	public delegate void PixelClickedEvent(object sender, PixelClickEventArgs e);
}

