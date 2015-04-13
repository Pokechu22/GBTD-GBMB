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
	public partial class ImportDialog : Form
	{
		private GBRObjectTileImport settings;

		public GBRObjectTileImport Settings {
			get { return settings; }
			set {
				settings = value;
				LoadFromSettings();
			}
		}

		public ImportDialog(GBRObjectTileImport settings) : this() {
			this.Settings = settings;
		}

		public ImportDialog() {
			InitializeComponent();
			InitializeComboboxes();
		}

		private void InitializeComboboxes() {
			typeComboBox.SelectedIndex = 0;
			formatComboBox.SelectedIndex = 0;
		}

		protected void LoadFromSettings() {
			this.fileNameTextBox.Text = settings.FileName;
			this.typeComboBox.SelectedIndex = (int)settings.FileType;
			this.firstGBTDTileTextBox.Value = settings.FirstProgramTile;
			this.tileCountTextBox.Value = settings.TileCount;
			this.firstImportFileTileTextBox.Value = settings.FirstImportFileTile;
			this.firstByteToUseTextBox.Value = settings.FirstByte;
			this.formatComboBox.SelectedIndex = (int)settings.BinaryFileFormat;
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

		private void okButton_Click(object sender, EventArgs e) {
			SaveToSettings();
		}
	}
}
