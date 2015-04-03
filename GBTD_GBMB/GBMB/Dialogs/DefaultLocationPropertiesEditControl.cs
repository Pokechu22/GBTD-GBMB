using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBMB.Dialogs
{
	public partial class DefaultLocationPropertiesEditControl : UserControl
	{
		protected override Size DefaultSize { get { return new Size(201, 169); } }

		public DefaultLocationPropertiesEditControl() {
			InitializeComponent();
		}
	}
}
