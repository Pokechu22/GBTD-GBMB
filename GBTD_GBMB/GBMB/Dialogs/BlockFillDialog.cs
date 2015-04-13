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
		public GBMObjectMapPropertyData Properties { get; private set; }
		public GBMObjectDefaultTilePropertyValues DefaultProperties { get; private set; }

		private BlockFillDialog() {
			InitializeComponent();
		}

		public BlockFillDialog(GBMObjectMapSettings settings, ColorSet colorSet, UInt16 selectedTile, uint left, uint top,
				GBRObjectTileData tileSet, GBRObjectTilePalette paletteMapping, PaletteData paletteData, GBMObjectMapTileData map,
				GBMObjectMapPropertyData properties, GBMObjectDefaultTilePropertyValues defaultProperties) : this() {
			
			this.Settings = settings;
			this.Map = map;
			this.Properties = properties;
			this.DefaultProperties = defaultProperties;

			tileList.TileSet = tileSet;
			tileList.PaletteMapping = paletteMapping;
			tileList.PaletteData = paletteData;
			tileList.ColorSet = colorSet;
			tileList.SelectedTile = selectedTile;
			
			leftTextBox.Value = left;
			topTextBox.Value = top;

			widthTextBox.Value = settings.BlockFillWidth;
			heightTextBox.Value = settings.BlockFillHeight;
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
			Settings.BlockFillPattern = (BlockFillMode)paternComboBox.SelectedIndex;

			ApplyEffectToMap(Settings.BlockFillPattern, tileList.SelectedTile, leftTextBox.Value, topTextBox.Value, 
				widthTextBox.Value, heightTextBox.Value);
		}

		private void ApplyEffectToMap(BlockFillMode mode, UInt16 tile, uint minX, uint minY, uint width, uint height) {
			UInt16 currentTile = tile;

			uint maxX = minX + width - 1;
			uint maxY = minY + height - 1;

			uint mapWidth = Map.Master.Width;
			uint mapHeight = Map.Master.Height;
			uint propCount = Properties.Master.PropCount;

			switch (mode) {
			case BlockFillMode.SELECTED_TILE:
				for (uint x = minX; x <= maxX; x++) {
					for (uint y = minY; y <= maxY; y++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
					}
				}
				break;
			case BlockFillMode.LEFT_TO_RIGHT: 
				for (uint x = minX; x <= maxX; x++) {
					for (uint y = minY; y <= maxY; y++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
					}
					IncrementTile(ref currentTile);
				}
				break;
			case BlockFillMode.LEFT_TO_RIGHT_TOP_TO_BOTTOM:
				for (uint y = minY; y <= maxY; y++) {
					for (uint x = minX; x <= maxX; x++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
						IncrementTile(ref currentTile);
					}
				}
				break;
			case BlockFillMode.TOP_TO_BOTTOM:
				for (uint y = minY; y <= maxY; y++) {
					for (uint x = minX; x <= maxX; x++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
					}
					IncrementTile(ref currentTile);
				}
				break;
			case BlockFillMode.TOP_TO_BOTTOM_LEFT_TO_RIGHT: 
				for (uint x = minX; x <= maxX; x++) {
					for (uint y = minY; y <= maxY; y++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
						IncrementTile(ref currentTile);
					}
				}
				break;
			case BlockFillMode.RIGHT_TO_LEFT:
				for (uint x = maxX; x <= minX; x++) {
					for (uint y = maxY; y <= minY; y++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
					}
					IncrementTile(ref currentTile);
				}
				break;
			case BlockFillMode.RIGHT_TO_LEFT_TOP_TO_BOTTOM: 
				for (uint y = maxY; y <= minY; y++) {
					for (uint x = maxX; x <= minX; x++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
						IncrementTile(ref currentTile);
					}
				}
				break;
			case BlockFillMode.BOTTOM_TO_TOP:
				for (uint y = maxY; y <= minY; y++) {
					for (uint x = maxX; x <= minX; x++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
					}
					IncrementTile(ref currentTile);
				}
				break;
			case BlockFillMode.BOTTOM_TO_TOP_RIGHT_TO_LEFT:
				for (uint x = maxX; x <= minX; x++) {
					for (uint y = maxY; y <= minY; y++) {
						if (x < mapWidth && y < mapHeight) {
							Map.Tiles[x, y] = new GBMObjectMapTileDataRecord(currentTile);
							for (int i = 0; i < propCount; i++) {
								Properties.Data[x, y, i] = DefaultProperties.Data[currentTile, i];
							}
						}
						IncrementTile(ref currentTile);
					}
				}
				break;
			default: throw new InvalidEnumArgumentException("mode", (int)mode, typeof(BlockFillMode));
			}
		}

		/// <summary>
		/// Increments the tile, wrapping around if beyond the maximum.
		/// </summary>
		private void IncrementTile(ref UInt16 currentTile) {
			currentTile++;
			if (currentTile >= tileList.TileSet.Count) {
				currentTile = 0;
			}
		}
	}
}
