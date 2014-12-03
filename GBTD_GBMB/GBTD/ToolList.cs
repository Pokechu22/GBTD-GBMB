using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBTD
{
	public partial class ToolList : UserControl
	{
		//Sizes.
		protected override Size DefaultSize { get { return new Size(27, 217); } }
		protected override Size DefaultMaximumSize { get { return new Size(27, 217); } }
		protected override Size DefaultMinimumSize { get { return new Size(27, 217); } }
		//Margin.
		protected override Padding DefaultMargin { get { return new Padding(4); } }

		public ToolList() {
			InitializeComponent();
		}

		private void paintBorder(object sender, PaintEventArgs e) {
			Control control = sender as Control;
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, control.Width, control.Height, Border3DStyle.RaisedInner);
		}
	}
}
