using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBRFile;

namespace GB.GBTD.Dialogs
{
	public partial class ExportDialog : Form
	{
		private readonly GBRObjectTileExport settings;

		public ExportDialog(GBRObjectTileExport settings) : this() {
			this.settings = settings;

			this.tabControl.SelectedIndex = settings.SelectedTab;

			this.fileNameTextBox.Text = settings.FileName;
			this.fileTypeComboBox.SelectedIndex = (int)settings.FileType;
			this.labelTextBox.Text = settings.LabelName;
			this.sectionTextBox.Text = settings.SectionName;
			this.bankTextBox.Value = settings.Bank;
			this.fromTextBox.Value = settings.FromTile;
			this.toTextBox.Value = settings.ToTile;
			this.formatComboBox.SelectedIndex = (int)settings.Format;
			this.counterComboBox.SelectedIndex = (int)settings.CounterType;
			this.singleUnitCheckBox.Checked = !settings.StoreTilesInArray;
			this.gbCompressCheckBox.Checked = (settings.UseCompression == ExportCompressionMode.GBCompress);

			this.includePaletteCheckBox.Checked = settings.IncludeColors;
			this.palettesCGBComboBox.SelectedIndex = (int)settings.GBCPalMode;
			this.palettesSGBComboBox.SelectedIndex = (int)settings.SGBPalMode;
			this.metatileConvertCheckBox.Checked = settings.MakeMetaTiles;
			this.indexOffsetTextBox.Value = settings.MetaTileOffset;
			this.indexCounterComboBox.SelectedIndex = (int)settings.MetaCounterFormat;
			this.splitDataCheckBox.Checked = settings.Split;
			this.blockSizeTextBox.Value = settings.BlockSize;
		}

		private ExportDialog() {
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
			indexCounterComboBox.SelectedIndex = 0;
		}

		protected void SaveToSettings() {
			settings.SelectedTab = (byte)this.tabControl.SelectedIndex;

			settings.FileName = this.fileNameTextBox.Text;
			settings.FileType = (GBRExportFileType)this.fileTypeComboBox.SelectedIndex;
			settings.LabelName = this.labelTextBox.Text;
			settings.SectionName= this.sectionTextBox.Text;
			settings.Bank = (byte)this.bankTextBox.Value;
			settings.FromTile = (UInt16)this.fromTextBox.Value;
			settings.ToTile = (UInt16)this.toTextBox.Value;
			settings.Format = (ExportFormat)this.formatComboBox.SelectedIndex;
			settings.CounterType= (ExportCounterType)this.counterComboBox.SelectedIndex;
			settings.StoreTilesInArray = !this.singleUnitCheckBox.Checked;
			settings.UseCompression = this.gbCompressCheckBox.Checked ? ExportCompressionMode.GBCompress : ExportCompressionMode.None;

			settings.IncludeColors = this.includePaletteCheckBox.Checked;
			settings.GBCPalMode = (ExportPaletteMode)this.palettesCGBComboBox.SelectedIndex;
			settings.SGBPalMode= (ExportPaletteMode)this.palettesSGBComboBox.SelectedIndex;
			settings.MakeMetaTiles = this.metatileConvertCheckBox.Checked;
			settings.MetaTileOffset = this.indexOffsetTextBox.Value;
			settings.MetaCounterFormat= (ExportCounterType)this.indexCounterComboBox.SelectedIndex;
			settings.Split = this.splitDataCheckBox.Checked;
			settings.BlockSize = this.blockSizeTextBox.Value;
		}

		private void okButton_Click(object sender, EventArgs e) {
			//TODO: Validate.

			this.SaveToSettings();

			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;

			this.Close();
		}
	}
}
