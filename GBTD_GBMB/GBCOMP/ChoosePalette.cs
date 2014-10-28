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
		private GBColorSetSelector selectedControl = null;

		public ChoosePalette() {
			InitializeComponent();
			
		}

		private void colorPicker1_OnChange(object sender, EventArgs e) {
			MessageBox.Show("" + selectedControl + selectedControl.GetType().ToString());
			if (selectedControl != null) {
				selectedControl.CurrentColor = colorPicker1.MainViewColor;
			}
		}

		private void panelMouseDown(object sender, EventArgs e) {
			if (sender is GBColorSetSelector) {
				GBColorSetSelector c = (GBColorSetSelector)sender;
				selectedControl = c;
				colorPicker1.FirstColor = selectedControl.CurrentColor;
			}
			this.Refresh();
		}
	}
}
