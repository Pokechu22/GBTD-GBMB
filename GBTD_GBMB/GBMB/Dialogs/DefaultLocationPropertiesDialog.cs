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
	public partial class DefaultLocationPropertiesDialog : Form
	{
		private DefaultLocationPropertiesDialog() {
			InitializeComponent();
		}

		public DefaultLocationPropertiesDialog(GBRObjectTileData tileSet, ColorSet colorSet, UInt16 selectedTile, 
				GBRObjectTilePalette paletteMapping, PaletteData paletteData, GBMObjectMapProperties properties, 
				GBMObjectDefaultTilePropertyValues defaultProperties) : this() {

			editControl.Properties = properties;
			editControl.DefaultProperties = defaultProperties;
			
			tileList.TileSet = tileSet;
			tileList.PaletteMapping = paletteMapping;
			tileList.PaletteData = paletteData;
			tileList.ColorSet = colorSet;
			tileList.SelectedTile = selectedTile;
		}

		private void tileList_SelectedTileChanged(object sender, EventArgs e) {
			tileGroupBox.Text = "Tile " + tileList.SelectedTile;
			if (tileList.SelectedTile == editControl.SelectedTile) {
				return;
			}

			if (!editControl.IsValid()) {
				tileList.SelectedTile = editControl.SelectedTile; //Cancel the change.
			} else {
				editControl.SelectedTile = tileList.SelectedTile;
			}
		}

		protected override void OnClosing(CancelEventArgs e) {
			if (!editControl.IsValid()) {
				e.Cancel = true;
			} else {
				editControl.SaveCurrent();
			}
			base.OnClosing(e);
		}

		private void okButton_Click(object sender, EventArgs e) {
			if (editControl.IsValid()) {
				editControl.SaveCurrent();
				this.Close();
			}
		}
	}
}
