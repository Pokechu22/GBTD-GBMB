using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBRFile;
using System.IO;

namespace GB.GBTD.Dialogs
{
	public partial class ImportDialog : Form
	{
		private readonly GBRObjectTileImport settings;
		private readonly GBRObjectTileData tileset;

		public ImportDialog(GBRObjectTileImport settings, GBRObjectTileData tileset) {
			InitializeComponent();
			InitializeComboboxes();

			this.settings = settings;
			this.tileset = tileset;

			this.fileNameTextBox.Text = settings.FileName;
			this.typeComboBox.SelectedIndex = (int)settings.FileType;
			this.firstGBTDTileTextBox.Value = settings.FirstProgramTile;
			this.tileCountTextBox.Value = settings.TileCount;
			this.firstImportFileTileTextBox.Value = settings.FirstImportFileTile;
			this.firstByteToUseTextBox.Value = settings.FirstByte;
			this.formatComboBox.SelectedIndex = (int)settings.BinaryFileFormat;
		}

		private void InitializeComboboxes() {
			typeComboBox.SelectedIndex = 0;
			formatComboBox.SelectedIndex = 0;
		}

		protected void SaveToSettings() {
			settings.FileName = this.fileNameTextBox.Text;
			settings.FileType = (ImportFileType)this.typeComboBox.SelectedIndex;
			settings.FirstProgramTile = (UInt16)this.firstGBTDTileTextBox.Value;
			settings.TileCount = (UInt16)this.tileCountTextBox.Value;
			settings.FirstImportFileTile = (UInt16)this.firstImportFileTileTextBox.Value;
			settings.FirstByte = this.firstByteToUseTextBox.Value;
			settings.BinaryFileFormat = (ImportBinaryFileFormat)this.formatComboBox.SelectedIndex;
		}

		private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			bool isGBE = (typeComboBox.SelectedIndex == 0);

			labelFirstImportFileTile.Enabled = isGBE;
			firstImportFileTileTextBox.Enabled = isGBE;

			labelFirstByteToUse.Enabled = !isGBE;
			firstByteToUseTextBox.Enabled = !isGBE;
			labelFormat.Enabled = !isGBE;
			formatComboBox.Enabled = !isGBE;
		}

		private void ShowError(Control control, String message) {
			MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

			control.Focus();
		}

		/// <summary>
		/// Checks whether the currently entered data is valid.  If not, a messagebox is dispalyed.
		/// </summary>
		/// <returns>Whether the data is valid.</returns>
		private bool IsDataValid() {
			if (String.IsNullOrEmpty(fileNameTextBox.Text)) {
				ShowError(fileNameTextBox, "'Filename' is mandatory for this import type.");
				return false;
			}
			if (String.IsNullOrEmpty(firstGBTDTileTextBox.Text)) {
				ShowError(firstGBTDTileTextBox, "'First tile in GBTD' is mandatory for this import type.");
				return false;
			}
			if (typeComboBox.SelectedIndex == 0 && String.IsNullOrEmpty(firstImportFileTileTextBox.Text)) {
				ShowError(firstImportFileTileTextBox, "'First tile from import file' is mandatory for this import type.");
				return false;
			}
			if (typeComboBox.SelectedIndex == 1 && String.IsNullOrEmpty(firstByteToUseTextBox.Text)) {
				ShowError(firstByteToUseTextBox, "'First byte to use' is mandatory for this import type.");
				return false;
			}

			if (tileCountTextBox.Value < 1) {
				ShowError(tileCountTextBox, "'Tile count' should be higher or equal to 1.");
				return false;
			}
			if (tileCountTextBox.Value + firstGBTDTileTextBox.Value > tileset.Count) {
				ShowError(firstGBTDTileTextBox, "'First tile in GBTD' combined with 'Tile count' can not exceed " + tileset.Count + ".");
				return false;
			}
			if (!File.Exists(fileNameTextBox.Text)) {
				ShowError(fileNameTextBox, "File '" + fileNameTextBox.Text + "' does not exist.");
				return false;
			}

			return true;
		}

		private void okButton_Click(object sender, EventArgs e) {
			if (!IsDataValid()) {
				return;
			}

			SaveToSettings();

			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
