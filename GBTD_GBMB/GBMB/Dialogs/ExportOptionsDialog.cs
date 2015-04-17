using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBMFile;

namespace GB.GBMB.Dialogs
{
	public partial class ExportOptionsDialog : Form
	{
		private ExportOptionsDialog() {
			InitializeComponent();
		}

		public ExportOptionsDialog(GBMObjectMapProperties properties, GBMObjectMapExportProperties exportProperties) : this() {
			propEditControl.PropertyNames = properties.Properties;
			propEditControl.ExportProperties = exportProperties.Data;

			resultingExportPlanesControl1.Properties = exportProperties.Data;
		}

		private void addRowButton_Click(object sender, EventArgs e) {
			propEditControl.AddRow();

			addRowButton.Enabled = (propEditControl.ExportProperties.Length < 32);
			removeRowButton.Enabled = (propEditControl.ExportProperties.Length > 0);
		}

		private void removeRowButton_Click(object sender, EventArgs e) {
			propEditControl.RemoveRow();

			addRowButton.Enabled = (propEditControl.ExportProperties.Length < 32);
			removeRowButton.Enabled = (propEditControl.ExportProperties.Length > 0);
		}

		private void propEditControl_SizeOrCountChanged(object sender, EventArgs e) {
			resultingExportPlanesControl1.Properties = propEditControl.ExportProperties;
		}
	}
}
