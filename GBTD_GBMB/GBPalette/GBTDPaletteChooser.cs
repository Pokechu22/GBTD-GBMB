using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Palette
{
	public partial class GBTDPaletteChooser : UserControl
	{
		protected override Size DefaultMaximumSize {
			get {
				return new Size(191, 26);
			}
		}

		protected override Size DefaultMinimumSize {
			get {
				return new Size(191, 26);
			}
		}

		protected override Size DefaultSize {
			get {
				return new Size(191, 26);
			}
		}

		public GBTDPaletteChooser() {
			InitializeComponent();
		}

		private void GBTDPaletteChooser_Paint(object sender, PaintEventArgs e) {
			//Paints a border.
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.Width, this.Height, Border3DStyle.RaisedInner);
		}

		private void paintInnerBorder(object sender, PaintEventArgs e) {
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, ((Control)sender).Width, ((Control)sender).Height, Border3DStyle.SunkenOuter);
		}
	}
}
