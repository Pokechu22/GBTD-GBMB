using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBTD.Dialogs
{
	public partial class ColorPicker : UserControl
	{
		protected override Size DefaultSize { get { return new Size(53, 230); } }
		protected override Size DefaultMinimumSize { get { return new Size(53, 230); } }
		protected override Size DefaultMaximumSize { get { return new Size(53, 230); } }

		[Description("The image that is used to get colors for the mouse.")]
		public Bitmap PixelImage { get; set; }
		[Description("The image that is displayed.")]
		public Image DisplayImage {
			get { return pictureBox.Image; }
			set { pictureBox.Image = value; }
		}

		private Color selectedColor;
		[Description("The currently-selected color (upper box)")]
		public Color SelectedColor {
			get { return selectedColor; }
			set {
				selectedColor = value;
				actualColorBox.BackColor = value;
				updateHoveredColor(value);

				if (SelectedColorChanged != null) {
					SelectedColorChanged(this, new EventArgs());
				}
			}
		}

		[Description("Fires when the selected color has changed.")]
		public event EventHandler SelectedColorChanged;

		public ColorPicker() {
			InitializeComponent();

			SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, true);
		}

		private bool updatingColor = false;

		/// <summary>
		/// Updates the hovered color preview and RGB text boxes.
		/// </summary>
		/// <param name="mouseX"></param>
		/// <param name="mouseY"></param>
		/// <returns>The currently-hovered color.</returns>
		private Color updateHoveredColor(Color color) {
			int r = color.R / 8;
			int g = color.G / 8;
			int b = color.B / 8;

			updatingColor = true;

			redTextBox.Text = r.ToString();
			greenTextBox.Text = g.ToString();
			blueTextBox.Text = b.ToString();

			updatingColor = false;

			//The "actual" color, IE the one on the Gameboy / elsewhere in the app.
			Color realColor = Color.FromArgb(r * 8, g * 8, b * 8);

			this.hoveredColorBox.BackColor = realColor;

			return realColor;
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e) {
			if (e.X >= 0 && e.Y >= 0 && e.X < PixelImage.Width && e.Y < PixelImage.Height) {
				updateHoveredColor(PixelImage.GetPixel(e.X, e.Y));
			} else {
				updateHoveredColor(Color.Black);
			}
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e) {
			Color color;
			
			if (e.X >= 0 && e.Y >= 0 && e.X < PixelImage.Width && e.Y < PixelImage.Height) {
				color = updateHoveredColor(PixelImage.GetPixel(e.X, e.Y));
			} else {
				color = updateHoveredColor(Color.Black);
			}

			this.actualColorBox.BackColor = color;
			this.selectedColor = color;
			
			if (SelectedColorChanged != null) {
				SelectedColorChanged(this, new EventArgs());
			}
		}

		private void actualColorBox_MouseEnter(object sender, EventArgs e) {
			updateHoveredColor(selectedColor);
		}

		private void colorTextBox_TextChanged(object sender, EventArgs e) {
			if (updatingColor) {
				return;
			}

			int r = (int)(redTextBox.Value * 8);
			int g = (int)(greenTextBox.Value * 8);
			int b = (int)(blueTextBox.Value * 8);

			Color color = Color.FromArgb(r, g, b);

			updateHoveredColor(color);
			this.actualColorBox.BackColor = color;
			this.selectedColor = color;

			if (SelectedColorChanged != null) {
				SelectedColorChanged(this, new EventArgs());
			}
		}
	}
}
