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
		private readonly GBRObjectTileData tileData;

		public ExportDialog(GBRObjectTileExport settings, GBRObjectTileData tileData) : this() {
			this.settings = settings;
			this.tileData = tileData;

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

		/// <summary>
		/// Focuses the error'd control and displays a messagebox with the given message.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="control"></param>
		private void MarkError(String message, Control control) {
			//Try to find the tabpage.
			Control c = control;
			while ((c != null) && !(c is TabPage)) {
				c = c.Parent;
			}

			if (c != null) {
				tabControl.SelectedTab = (TabPage)c;
			}

			// Display the mesage and focus the now-visible control
			MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			control.Focus();
		}

		/// <summary>
		/// Checks if the data is valid; if it isn't, a message is displayed.
		/// </summary>
		/// <returns>Whether the data is valid.</returns>
		private bool IsDataValid() {
			if (String.IsNullOrWhiteSpace(fileNameTextBox.Text)) {
				MarkError("'Filename' is is mandatory for this export type.", fileNameTextBox);
				return false;
			}
			if (String.IsNullOrWhiteSpace(fromTextBox.Text)) {
				MarkError("'From' is is mandatory for this export type.", fileNameTextBox);
				return false;
			}
			if (String.IsNullOrWhiteSpace(toTextBox.Text)) {
				MarkError("'To' is is mandatory for this export type.", fileNameTextBox);
				return false;
			}
			if (String.IsNullOrWhiteSpace(metatileConvertCheckBox.Text)) {
				MarkError("'Index offset' is is mandatory for this export type.", fileNameTextBox);
				return false;
			}

			if (fileTypeComboBox.SelectedIndex < 4) { //TAsm / GBDK C
				if (String.IsNullOrWhiteSpace(labelTextBox.Text)) {
					MarkError("'Label' is is mandatory for this export type.", fileNameTextBox);
					return false;
				}
				if (String.IsNullOrWhiteSpace(blockSizeTextBox.Text)) {
					MarkError("'Block size' is is mandatory for this export type.", fileNameTextBox);
					return false;
				}

				if (fileTypeComboBox.SelectedIndex < 2) { //RGBDS Asm/Obj
					if (String.IsNullOrWhiteSpace(sectionTextBox.Text)) {
						MarkError("'Section' is is mandatory for this export type.", fileNameTextBox);
						return false;
					}
					if (String.IsNullOrWhiteSpace(bankTextBox.Text)) {
						MarkError("'Bank' is is mandatory for this export type.", fileNameTextBox);
						return false;
					}
				}
			}

			if (fromTextBox.Value > toTextBox.Value) {
				MarkError("'To' should be higher or equal to 'From'.", toTextBox);
				return false;
			}
			if (toTextBox.Value > tileData.Count - 1) {
				MarkError("'To' can not exceed '" + (tileData.Count - 1) + "'.", toTextBox);
				return false;
			}
			if (splitDataCheckBox.Checked && blockSizeTextBox.Value < 1) {
				MarkError("'Block size' should be higher or equal to 1.", blockSizeTextBox);
				return false;
			}

			return true;
		}

		private void okButton_Click(object sender, EventArgs e) {
			if (!IsDataValid()) {
				return;
			}

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
