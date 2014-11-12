using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GB.Shared.Palette
{
	/// <summary>
	/// Internal class which represents an individual entry.
	/// </summary>
	internal abstract class PaletteEntry : Label
	{
		protected override Padding DefaultMargin {
			get {
				return new Padding(0);
			}
		}

		protected override Padding DefaultPadding {
			get {
				return new Padding(0);
			}
		}

		/// <summary>
		/// Width of the individual control.
		/// </summary>
		protected virtual int WIDTH {
			get { return 19; }
		}
		/// <summary>
		/// Height of the individual control.
		/// </summary>
		protected virtual int HEIGHT {
			get { return 19; }
		}

		/// <summary>
		/// Initial offset for each control (at no aditional offset)
		/// </summary>
		protected virtual int X_OFFSET {
			get { return 16 + 20; }
		}
		/// <summary>
		/// Initial offset for each control (at no aditional offset)
		/// </summary>
		protected virtual int Y_OFFSET {
			get { return 19; }
		}

		/// <summary>
		/// The spacing between each control.
		/// </summary>
		protected virtual int X_SPACING {
			get { return WIDTH; }
		}
		/// <summary>
		/// The spacing between each control.
		/// </summary>
		protected virtual int Y_SPACING {
			get { return HEIGHT + 9; }
		}

		public readonly int x, y;

		private Color color;

		public Color Color {
			get { return this.color; }
			set {
				if (value == null) {
					throw new ArgumentNullException();
				}
				this.color = value;
				this.BackColor = (UseGBCFilter ? GBCFiltration.TranslateToGBCColor(value) : value);
				OnColorChange();
			}
		}

		protected PaletteEntry(int x, int y) {
			this.x = x;
			this.y = y;

			this.Text = x.ToString();
			this.Name = "entry_x" + x + "_y" + y;

			this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

			this.TabIndex = (y * 4) + x;

			this.Paint += new PaintEventHandler(PalatteEntry_Paint);
			this.MouseDown += new MouseEventHandler(PalatteEntry_MouseDown);
		}

		protected override void InitLayout() {
			base.InitLayout();
			this.Color = GetDefaultColor();

			this.Size = new System.Drawing.Size(WIDTH, HEIGHT);
			this.Location = new System.Drawing.Point(X_OFFSET + (x * X_SPACING), Y_OFFSET + (y * Y_SPACING));
		}

		/// <summary>
		/// Paints an individual entry.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal void PalatteEntry_Paint(object sender, PaintEventArgs e) {
			if (sender is PaletteEntry) {
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

				PaletteEntry c = (PaletteEntry)sender;
				c.BackColor = (UseGBCFilter ? GBCFiltration.TranslateToGBCColor(c.color) : c.color);
				
				//Draw the main border.
				ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, c.Width, c.Height),
					Color.Black, ButtonBorderStyle.Solid);
				//If selected draw the inner border.
				if (IsSelected()) {
					ControlPaint.DrawBorder(e.Graphics, new Rectangle(1, 1, c.Width - 2, c.Height - 2),
						SystemColors.Highlight, ButtonBorderStyle.Solid);
				}
			}
		}

		internal void PalatteEntry_MouseDown(object sender, MouseEventArgs e) {
			if (sender is PaletteEntry) {
				PaletteEntry entry = (PaletteEntry)sender;

				entry.SetSelected();
			}
		}

		public override string ToString() {
			return base.ToString() + " @ x" + x + " y" + y;
		}

		/// <summary>
		/// Called when the color is changed.  
		/// Default implementation handles the coloration of text.
		/// </summary>
		protected virtual void OnColorChange() {
			//Changes text color.
			if (((Color.R < 0x40) && (Color.G < 0x40)) ||
					((Color.G < 0x40) && (Color.B < 0x40)) ||
					((Color.R < 0x40) && (Color.B < 0x40))) {
				this.ForeColor = Color.White;
			} else {
				this.ForeColor = Color.Black;
			}
		}

		/// <summary>
		/// Called when the selection is changed; should modify any super behaviours.
		/// </summary>
		protected abstract void SetSelected();

		/// <summary>
		/// Checks whether or not currently selected.
		/// </summary>
		/// <returns></returns>
		protected abstract bool IsSelected();

		/// <summary>
		/// Checks whether or not the GBC filter is used.
		/// </summary>
		protected abstract bool UseGBCFilter {
			get;
			set;
		}

		/// <summary>
		/// Gets the default color used here.
		/// </summary>
		/// <returns></returns>
		protected abstract Color GetDefaultColor();
	}

}
