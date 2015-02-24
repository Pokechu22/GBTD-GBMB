using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palettes;

namespace GB.Shared.Tiles
{
	public class TileRenderer : Control
	{
		protected override Size DefaultSize { get { return new Size(8, 8); } }

		#region Private members
		private TileData tileData = new TileData { GBC_Palette = 0, SGB_Palette = 0, paletteData = new PaletteData(), tile = new Tile(8, 8) };

		private byte clickedX = 0, clickedY = 0;
		private MouseButtons buttons = MouseButtons.None;

		private bool grid = false;
		private bool border = true;
		private bool nibbleMarkers = false;

		private Border3DSide borderSides = Border3DSide.All;

		private int pixelScale = 1;

		private bool selected;

		private ColorSet colorSet;
		#endregion

		#region Public properties
		public TileData TileData {
			get { return tileData; }
			set { tileData = value; OnResize(new EventArgs()); OnTileChange(new EventArgs()); OnPalatteChange(new EventArgs()); }
		}

		[Category("Data"), Description("The ColorSet used by this tile.")]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; OnPalatteChange(new EventArgs()); }
		}

		[Category("Data"), Description("The palette used by this tile.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Palette Palette {
			get {
				return tileData.GetPalette(ColorSet);
			}
			set {
				tileData.SetPalette(ColorSet, value);
				OnPalatteChange(new EventArgs());
			}
		}

		[Category("Data"), Description("The entire palette paletteData.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PaletteData PaletteData {
			get {
				return tileData.paletteData;
			}
			set {
				tileData.paletteData = value;
				OnPalatteChange(new EventArgs());
			}
		}

		[Category("Data"), Description("The ID of the palette used by this tileData.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PaletteID {
			get {
				return tileData.GetRow(ColorSet);
			}
			set {
				tileData.SetRow(ColorSet, (UInt16)value);
				OnPalatteChange(new EventArgs());
			}
		}
		
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tile Tile {
			get {
				return tileData.tile;
			}
			set {
				this.tileData.tile = value;
				OnResize(new EventArgs());
				OnTileChange(new EventArgs());
			}
		}

		[Category("Display"), Description("Controls drawing of a pixel grid.")]
		public bool Grid {
			get {
				return grid;
			}
			set {
				this.grid = value;
				this.Invalidate(true);
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
				this.Invalidate(true);
			}
		}

		[Category("Display"), Description("Controls drawing of nibble markers, which are blue dots every 4 pixels.")]
		public bool NibbleMarkers {
			get {
				return nibbleMarkers;
			}
			set {
				nibbleMarkers = value;
				this.Invalidate(true);
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
				this.Invalidate(true);
			}
		}

		/// <summary>
		/// The pixelScale of the TileRenderer.
		/// </summary>
		[Category("Dispaly"), Description("The size of a single pixel.")]
		public int PixelScale {
			get { return pixelScale; }
			set {
				if (pixelScale <= 0) {
					throw new ArgumentOutOfRangeException("Must be greater than 0", "value");
				}
				pixelScale = value;
				OnResize(new EventArgs());
			}
		}

		[Category("Dispaly"), Description("Whether or not to render as if selected.")]
		public bool Selected {
			get { return selected; }
			set { selected = value; this.Invalidate(); }
		}
		#endregion

		#region Events
		/// <summary>
		/// Fires when the currently-used tileData is changed.
		/// </summary>
		[Category("Property Changed"), Description("Fires when the currently-used tileData is changed.")]
		public event EventHandler TileChanged;

		/// <summary>
		/// Fires when the currently-used palatte is changed.
		/// </summary>
		[Category("Property Changed"), Description("Fires when the currently-used palatte is changed.")]
		public event EventHandler PalatteChanged;

		/// <summary>
		/// Fires when a pixel in the tileData is clicked.  
		/// Use this over OnClick.
		/// </summary>
		[Category("Action"), Description("Fires when a pixel in the tileData is clicked.  Use this over OnClick.")]
		public event PixelClickedEventHandler PixelClicked;

		protected virtual void OnPalatteChange(EventArgs e) {
			if (PalatteChanged != null) {
				PalatteChanged(this, e);
			}
			this.Invalidate(true);
		}

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
		}

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

			for (byte x = 0; x < this.tileData.Width; x++) {
				for (byte y = 0; y < this.tileData.Height; y++) {
					drawPixel(x, y, tileData.tile[x, y], e.Graphics);
				}
			}

			//Offset sizes.
			float w = this.Width - ((border && borderSides.HasFlag(Border3DSide.Right)) ? 1 : 0);
			float h = this.Height - ((border && borderSides.HasFlag(Border3DSide.Bottom)) ? 1 : 0);

			if (grid) {
				for (UInt16 x = 1; x < this.tileData.Width; x++) {
					e.Graphics.DrawLine(Pens.Black,
						x * (w / this.tileData.Width),
						0,
						x * (w / this.tileData.Width),
						this.Height);
				}
				for (UInt16 y = 1; y < this.tileData.Height; y++) {
					e.Graphics.DrawLine(Pens.Black,
						0,
						y * (h / this.tileData.Height),
						this.Width,
						y * (h / this.tileData.Height));
				}
			}
			if (border) {
				ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), 
					Color.Black, borderSides.HasFlag(Border3DSide.Left) ? 1 : 0, ButtonBorderStyle.Solid,
					Color.Black, borderSides.HasFlag(Border3DSide.Top) ? 1 : 0, ButtonBorderStyle.Solid,
					Color.Black, borderSides.HasFlag(Border3DSide.Right) ? 1 : 0, ButtonBorderStyle.Solid,
					Color.Black, borderSides.HasFlag(Border3DSide.Bottom) ? 1 : 0, ButtonBorderStyle.Solid);
			}
			if (nibbleMarkers) {
				//3 here is the size.
				for (int x = 0; x <= 2; x++) {
					for (int y = 0; y <= 2; y++) {
						int centX = 0, centY = 0;

						switch (x) {
						case 0: centX = 0; break;
						case 1: centX = Width / 2; break;
						case 2: centX = Width - 1; break;
						}
						switch (y) {
						case 0: centY = 0; break;
						case 1: centY = Height / 2; break;
						case 2: centY = Height - 1; break;
						}

						e.Graphics.FillRectangle(Brushes.Blue, centX - 1, centY, 3, 1);
						e.Graphics.FillRectangle(Brushes.Blue, centX, centY - 1, 1, 3);
					}
				}
			}

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
			if (x < 0 || x >= this.tileData.Width) {
				throw new InvalidOperationException("x must be between 0 and " + (this.tileData.Width - 1) + "; got " + x);
			}
			if (y < 0 || y > this.tileData.Height) {
				throw new InvalidOperationException("y must be between 0 and " + (this.tileData.Height - 1) + "; got " + y);
			}

			float x1 = (x * ((this.Width - (border ? 1 : 0)) / (float)this.tileData.Width));
			float y1 = (y * ((this.Height - (border ? 1 : 0)) / (float)this.tileData.Height));

			if (border) {
				x1++;
				y1++;
			}

			float w = this.Width - ((border && borderSides.HasFlag(Border3DSide.Right)) ? 1 : 0);
			float h = this.Height - ((border && borderSides.HasFlag(Border3DSide.Bottom)) ? 1 : 0);

			float width = (w / this.tileData.Width);
			float height = (h / this.tileData.Height);

			Color c;
			if (Enabled) {
				c = tileData.paletteData.GetColor(ColorSet, (UInt16)PaletteID, color);
				if (Selected) {
					c = c.FilterAsSelected();
				}
			} else {
				c = SystemColors.Control;
			}

			using (Brush brush = new SolidBrush(c)) {
				g.FillRectangle(brush, x1, y1, width, height);
			}
		}

		/// <summary>If already resizing, we don't want to start a second resize.</summary>
		private volatile bool resizing = false;
		/// <summary>
		/// Ensure that it is divisible evenly by 8.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			
			if (!resizing) {
				resizing = true;

				Size newSize = new Size();

				newSize.Width = this.pixelScale * this.tileData.Width;
				newSize.Height = this.pixelScale * this.tileData.Height;

				if (this.border) {
					if (this.borderSides.HasFlag(Border3DSide.Right)) {
						newSize.Width++;
					}
					if (this.borderSides.HasFlag(Border3DSide.Bottom)) {
						newSize.Height++;
					}
				}

				this.Size = newSize;
				resizing = false;
			}
		}

		protected internal byte getClickedPixelX(int clickedX) {
			int offset = ((border && borderSides.HasFlag(Border3DSide.Right)) ? 1 : 0);

			byte returned = (byte)(((clickedX - offset) * this.tileData.Width) / this.Width);

			if (returned < 0 || returned >= this.tileData.Width) {
				throw new InvalidOperationException("Mouse out of bounds.");
			}
			return returned;
		}

		protected internal byte getClickedPixelY(int clickedY) {
			int offset = ((border && borderSides.HasFlag(Border3DSide.Bottom)) ? 1 : 0);

			byte returned = (byte)(((clickedY - offset) * this.tileData.Height) / this.Height);

			if (returned < 0 || returned >= this.tileData.Height) {
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

