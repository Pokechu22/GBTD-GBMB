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
	public partial class ImportDialog : Form
	{
		public ImportDialog() {
			InitializeComponent();
			InitializeComboboxes();
		}

		private void InitializeComboboxes() {
			typeComboBox.SelectedIndex = 0;
			formatComboBox.SelectedIndex = 0;
		}
	}
}
