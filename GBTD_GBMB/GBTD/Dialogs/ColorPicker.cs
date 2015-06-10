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

		public ColorPicker() {
			InitializeComponent();

			SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, true);
		}
	}
}
