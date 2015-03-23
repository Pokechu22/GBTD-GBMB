using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBMB
{
	public partial class ToolList : UserControl
	{
		protected override Size DefaultSize { get { return new Size(26, 174); } }
		protected override Size DefaultMaximumSize { get { return new Size(26, 174); } }
		protected override Size DefaultMinimumSize { get { return new Size(26, 174); } }

		public ToolList() {
			InitializeComponent();

			SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, true);
		}
	}
}
