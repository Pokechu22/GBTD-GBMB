using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBRenderer
{
	public partial class ChoosePalette : Form
	{
		private Control selectedControl = null;

		public ChoosePalette() {
			InitializeComponent();
			
		}

		private void colorPicker1_OnChange(object sender, EventArgs e) {
			if (selectedControl != null) {
				selectedControl.BackColor = colorPicker1.MainViewColor;

				//Brightness.
				if (((selectedControl.BackColor.R < 0x40) && (selectedControl.BackColor.G < 0x40)) ||
						((selectedControl.BackColor.G < 0x40) && (selectedControl.BackColor.B < 0x40)) ||
						((selectedControl.BackColor.R < 0x40) && (selectedControl.BackColor.B < 0x40))) {
					selectedControl.ForeColor = Color.White;
				} else {
					selectedControl.ForeColor = Color.Black;
				}
			}
		}

		private void panelPaint(object sender, PaintEventArgs e) {
			if (sender is Label) {
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

				Label c = (Label)sender;

				//Draw the main border.
				ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, c.Width, c.Height),
					Color.Black, ButtonBorderStyle.Solid);
				//If selected draw the inner border.
				if (Object.ReferenceEquals(selectedControl, c)) {
					ControlPaint.DrawBorder(e.Graphics, new Rectangle(1, 1, c.Width - 2, c.Height - 2),
						SystemColors.Highlight, ButtonBorderStyle.Solid);
				}
			}
		}

		private void panelMouseDown(object sender, MouseEventArgs e) {
			if (sender is Control) {
				Control c = (Control)sender;
				selectedControl = c;
				colorPicker1.FirstColor = selectedControl.BackColor;
			}
			this.Refresh();
		}
	}
}
