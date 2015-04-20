using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBMFile;
using System.IO;

namespace GB.GBMB.Dialogs
{
	public partial class ExportOptionsDialog : Form
	{
		public GBMObjectMapExportSettings ExportSettings {
			get;
			private set;
		}

		public GBMObjectMapExportProperties ExportProperties {
			get;
			private set;
		}

		private ExportOptionsDialog() {
			InitializeComponent();
		}

		public ExportOptionsDialog(GBMObjectMapProperties properties, GBMObjectMapExportSettings exportSettings,
				GBMObjectMapExportProperties exportProperties) : this() {
			
			this.ExportSettings = exportSettings;
			this.ExportProperties = exportProperties;

			propEditControl.PropertyNames = properties.Properties;
			propEditControl.ExportProperties = exportProperties.Data;

			resultingPlanesControl.Properties = exportProperties.Data;

			typeDropDown.SelectedIndex = (int)exportSettings.FileType;
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
			resultingPlanesControl.Properties = propEditControl.ExportProperties;
		}

		private void splitDataCheckBox_CheckedChanged(object sender, EventArgs e) {
			labelBlockSize.Enabled = splitDataCheckBox.Checked;
			blockSizeTextBox.Enabled = splitDataCheckBox.Checked;
			changeBankForEachBlockCheckBox.Enabled = splitDataCheckBox.Checked;
		}

		protected override void OnClosing(CancelEventArgs e) {
			//TODO validate data.
			
			//Save data.  TODO exportsettings portion.
			if (DialogResult == DialogResult.OK) {
				this.ExportProperties.Data = propEditControl.ExportProperties;
			}
			base.OnClosing(e);
		}

		private void okButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void browseButton_Click(object sender, EventArgs e) {
			SaveFileDialog dialog = new SaveFileDialog();

			//TODO set up filter.
			dialog.InitialDirectory = Properties.Settings.Default.ExportPath;
			dialog.FileName = fileNameTextBox.Text;
			dialog.Filter = new String[] {
				"RGBDS Assembly file|*.z80|",
				"RGBDS Object file|*.obj|",
				"TASM Assembly file|*.z80|",
				"GBDK C file|*.c|",
				"All-purpose binary file|*.bin|",
				"ISAS Assembly file|*.s"
			}.Aggregate(new StringBuilder(), (b, s) => b.Append(s)).ToString();
			dialog.FilterIndex = typeDropDown.SelectedIndex + 1; //FilterIndex starts at 1 rather than 0.

			var result = dialog.ShowDialog();

			if (result != DialogResult.OK) { return; }

			fileNameTextBox.Text = dialog.FileName;
			Properties.Settings.Default.ExportPath = Path.GetDirectoryName(dialog.FileName);
			typeDropDown.SelectedIndex = dialog.FilterIndex - 1;
		}
	}
}
