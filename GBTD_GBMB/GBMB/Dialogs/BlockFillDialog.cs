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

		private BlockFillDialog() {
			InitializeComponent();
		}

		public BlockFillDialog(GBMObjectMapSettings settings, ColorSet colorSet, UInt16 selectedTile, int left, int top,
				GBRObjectTileData tileSet, GBRObjectTilePalette paletteMapping, PaletteData paletteData) : this() {
			
			this.Settings = settings;

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

		private void okButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.OK;
			this.Close();

			Settings.BlockFillHeight = (uint)heightTextBox.Value;
			Settings.BlockFillWidth = (uint)widthTextBox.Value;
			Settings.BlockFillPattern = (uint)paternComboBox.SelectedIndex;
		}
	}
}
