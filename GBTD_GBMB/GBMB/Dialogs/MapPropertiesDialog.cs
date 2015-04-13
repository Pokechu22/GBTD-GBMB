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
	public partial class MapPropertiesDialog : Form
	{
		public GBMObjectMap Map { get; private set; }

		private MapPropertiesDialog() {
			InitializeComponent();

			fileNameTextBox.AutoSize = false;
			fileNameTextBox.Size = new Size(169, 21);
		}

		public MapPropertiesDialog(GBMObjectMap map) : this() {
			this.Map = map;

			widthTextBox.Value = this.Map.Width;
			heightTextBox.Value = this.Map.Height;
			fileNameTextBox.Text = this.Map.TileFile;
		}

		private void okButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;
			this.Close();

			this.Map.Resize(widthTextBox.Value, heightTextBox.Value);
			this.Map.TileFile = fileNameTextBox.Text;
		}

		private void browseButton_Click(object sender, EventArgs e) {
			//Start with the current file name, if it is set.
			openFileDialog.FileName = fileNameTextBox.Text;
			var result = openFileDialog.ShowDialog();

			if (result != DialogResult.OK) { return; }

			fileNameTextBox.Text = openFileDialog.FileName;
		}
	}
}
