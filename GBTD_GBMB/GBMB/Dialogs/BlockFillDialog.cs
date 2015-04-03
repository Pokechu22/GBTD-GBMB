using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBRFile;
using GB.Shared.GBMFile;
using GB.Shared.Palettes;

namespace GB.GBMB.Dialogs
{
	public partial class BlockFillDialog : Form
	{
		public GBMObjectMapSettings Settings { get; private set; }
		public GBMObjectMapTileData Map { get; private set; }

		private BlockFillDialog() {
			InitializeComponent();
		}

		public BlockFillDialog(GBMObjectMapSettings settings, ColorSet colorSet, UInt16 selectedTile, int left, int top,
				GBRObjectTileData tileSet, GBRObjectTilePalette paletteMapping, PaletteData paletteData, GBMObjectMapTileData map) : this() {
			
			this.Settings = settings;
			this.Map = map;

			tileList.TileSet = tileSet;
			tileList.PaletteMapping = paletteMapping;
			tileList.PaletteData = paletteData;
			tileList.ColorSet = colorSet;
			tileList.SelectedTile = selectedTile;

			leftTextBox.Value = left;
			topTextBox.Value = top;

			widthTextBox.Value = (int)settings.BlockFillWidth;
			heightTextBox.Value = (int)settings.BlockFillHeight;
			paternComboBox.SelectedIndex = (int)settings.BlockFillPattern;
		}

		/// <summary>
		/// Validates that the given settings are acceptable.  If not, a message box is shown and false is returned.
		/// </summary>
		/// <returns>Whether the data is valid.</returns>
		public bool IsDataValid() {
			if (leftTextBox.Text == "") {
				MessageBox.Show("Left is mandatory.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				leftTextBox.Focus();
				leftTextBox.SelectAll();
				return false;
			}
			if (leftTextBox.Value >= Map.Master.Width) {
				MessageBox.Show("Left should be lower or equal to " + (Map.Master.Width - 1) + ".", 
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				leftTextBox.Focus();
				leftTextBox.SelectAll();
				return false;
			}
			if (topTextBox.Text == "") {
				MessageBox.Show("Top is mandatory.", 
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				topTextBox.Focus();
				topTextBox.SelectAll();
				return false;
			}
			if (topTextBox.Value >= Map.Master.Height) {
				MessageBox.Show("Top should be lower or equal to " + (Map.Master.Height - 1) + ".",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				topTextBox.Focus();
				topTextBox.SelectAll();
				return false;
			}
			if (widthTextBox.Text == "") {
				MessageBox.Show("Width is mandatory.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				widthTextBox.Focus();
				widthTextBox.SelectAll();
				return false;
			}
			if (widthTextBox.Value <= 0) {
				MessageBox.Show("Width should be higher than 0.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				widthTextBox.Focus();
				widthTextBox.SelectAll();
				return false;
			}
			if (heightTextBox.Text == "") {
				MessageBox.Show("Height is mandatory.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				heightTextBox.Focus();
				heightTextBox.SelectAll();
				return false;
			}
			if (heightTextBox.Value <= 0) {
				MessageBox.Show("Height should be higher than 0.",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				heightTextBox.Focus();
				heightTextBox.SelectAll();
				return false;
			}

			return true;
		}

		private void okButton_Click(object sender, EventArgs e) {
			if (!IsDataValid()) {
				return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();

			Settings.BlockFillHeight = (uint)heightTextBox.Value;
			Settings.BlockFillWidth = (uint)widthTextBox.Value;
			Settings.BlockFillPattern = (uint)paternComboBox.SelectedIndex;
			
			//TODO apply the effect.
		}
	}
}
