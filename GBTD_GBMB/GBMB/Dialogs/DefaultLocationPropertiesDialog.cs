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

			defaultLocationPropertiesEditControl1.Properties = properties;
			defaultLocationPropertiesEditControl1.DefaultProperties = defaultProperties;
			
			tileList.TileSet = tileSet;
			tileList.PaletteMapping = paletteMapping;
			tileList.PaletteData = paletteData;
			tileList.ColorSet = colorSet;
			tileList.SelectedTile = selectedTile;
		}

		private void tileList_SelectedTileChanged(object sender, EventArgs e) {
			if (tileList.SelectedTile == defaultLocationPropertiesEditControl1.SelectedTile) {
				return;
			}

			if (!defaultLocationPropertiesEditControl1.IsValid()) {
				tileList.SelectedTile = defaultLocationPropertiesEditControl1.SelectedTile; //Cancel the change.
			} else {
				defaultLocationPropertiesEditControl1.SelectedTile = tileList.SelectedTile;
			}
		}

		protected override void OnClosing(CancelEventArgs e) {
			if (!defaultLocationPropertiesEditControl1.IsValid()) {
				e.Cancel = true;
			} else {
				defaultLocationPropertiesEditControl1.SaveCurrent();
			}
			base.OnClosing(e);
		}

		private void okButton_Click(object sender, EventArgs e) {
			if (defaultLocationPropertiesEditControl1.IsValid()) {
				defaultLocationPropertiesEditControl1.SaveCurrent();
				this.Close();
			}
		}
	}
}
