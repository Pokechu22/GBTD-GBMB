using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBTD.Dialogs
{
	public partial class ExportDialog : Form
	{
		public ExportDialog() {
			InitializeComponent();
			InitializeDropdowns();
		}

		/// <summary>
		/// Initializes all of the dropdowns by seting their SelectedIndex's to 0.
		/// </summary>
		private void InitializeDropdowns() {
			fileTypeComboBox.SelectedIndex = 0;
			formatComboBox.SelectedIndex = 0;
			counterComboBox.SelectedIndex = 0;
			palettesCGBComboBox.SelectedIndex = 0;
			palettesSGBComboBox.SelectedIndex = 0;
		}
	}
}
