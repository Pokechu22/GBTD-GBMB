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
			propEditControl.Properties = properties.Properties;
			propEditControl.ExportProperties = exportProperties.Data;
		}
	}
}
