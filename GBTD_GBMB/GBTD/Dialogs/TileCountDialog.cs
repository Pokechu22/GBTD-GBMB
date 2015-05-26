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
	public partial class TileCountDialog : Form
	{
		const int MAX_8_BY_8_TILES = 768;

		private readonly GBRObjectTileData tileSet;
		private readonly int tilesPerTile;
		private readonly int maxTiles;
		
		public TileCountDialog(GBRObjectTileData tileSet) {
			InitializeComponent();

			this.tileSet = tileSet;
			this.tileCountTextBox.Value = tileSet.Count;
			this.tilesPerTile = ((tileSet.Width / 8) * (tileSet.Height / 8));
			maxTiles = MAX_8_BY_8_TILES / tilesPerTile;

			this.labelTileCount.Text = String.Format("&Tile count ({0} maximum):", maxTiles);
		}

		private void okButton_Click(object sender, EventArgs e) {
			if (tileCountTextBox.Value % (16 / tilesPerTile) != 0) {
				MessageBox.Show("Tile count must be evenly divisible by " + (16 / tilesPerTile) + "!", "Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (tileCountTextBox.Value > maxTiles) {
				MessageBox.Show("Tile count can not exceed " + maxTiles + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (tileCountTextBox.Value < 1) {
				MessageBox.Show("Tile count should be at least 1.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			tileSet.Count = (UInt16)tileCountTextBox.Value;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
