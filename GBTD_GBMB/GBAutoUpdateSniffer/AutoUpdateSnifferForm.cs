using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.AutoUpdate;
using GB.Shared.Tiles;

namespace GBAutoUpdateSniffer
{
	public partial class AutoUpdateSnifferForm : Form
	{
		public AutoUpdateSnifferForm() {
			InitializeComponent();

			//While the "Enabled" property claims to be useless for TabPage, it *does* disable the inner controls, which is useful.
			//It doesn't appear in intelisense, but it does work.
			tabPageMemoryMappedFile.Enabled = false;
			tabPageMessages.Enabled = false;
		}

		private AUMemMappedFile mmf;

		private void openButton_Click(object sender, EventArgs e) {
			var result = openFileDialog.ShowDialog();
			if (result != DialogResult.OK) {
				return;
			}

			fileNameLabel.Text = openFileDialog.FileName;
			auListener.FileName = openFileDialog.FileName;

			listBoxMessages.Items.Clear();

			labelMessageHex.Text = auListener.AutoUpdateMessageID.ToString("X4");
			labelMessageName.Text = auListener.AutoUpdateMessageName;

			this.mmf = new AUMemMappedFile(openFileDialog.FileName, this.auListener);

			//While the "Enabled" property claims to be useless for TabPage, it *does* disable the inner controls, which is useful.
			//It doesn't appear in intelisense, but it does work.
			tabPageMemoryMappedFile.Enabled = true;
			tabPageMessages.Enabled = true;

			mmfFileNameTextBox.Text = mmf.MMFName;
			labelMMFName.Text = mmf.MMFName;

			mmfTileRenderer.Tile = mmf.Tiles[(UInt16)mmfTileNumberTextBox.Value];
			mmfGBCPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].GBC;
			mmfSGBPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].SGB;

			mmfIDTextBox.Text = mmf.ID;
		}

		private void listBoxMessages_SelectedIndexChanged(object sender, EventArgs e) {
			if (listBoxMessages.SelectedIndex == -1) {
				textBoxMessageInfo.Text = "";
			} else {
				textBoxMessageInfo.Text = listBoxMessages.SelectedItem.ToString();
			}
		}

		private void auListener_OnColorPaletteChanged(object sender, MessageEventArgs args) {
			try {
				updating = true;

				Invoke(new MethodInvoker(delegate
				{
					listBoxMessages.Items.Add(new AUEventInfo(args, AUEventType.Color_set_change));
				}));
			} catch (Exception ex) {
				MessageBox.Show("Exception occured while updating from MemoryMappedFile change:\n" + new AUEventInfo(args, AUEventType.Color_set_change) + "\n\n" + ex.ToString(), "MemoryMappedFile updating error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			} finally {
				updating = false;
			}
		}

		private void auListener_OnGBPaletteChanged(object sender, MessageEventArgs args) {
			try {
				updating = true;

				Invoke(new MethodInvoker(delegate
				{
					listBoxMessages.Items.Add(new AUEventInfo(args, AUEventType.Tile_palette));

					mmfColor0MappingTextBox.Value = mmf.GBPalettes.Color0;
					mmfColor1MappingTextBox.Value = mmf.GBPalettes.Color1;
					mmfColor2MappingTextBox.Value = mmf.GBPalettes.Color2;
					mmfColor3MappingTextBox.Value = mmf.GBPalettes.Color3;
				}));
			} catch (Exception ex) {
				MessageBox.Show("Exception occured while updating from MemoryMappedFile change:\n" + new AUEventInfo(args, AUEventType.Color_set_change) + "\n\n" + ex.ToString(), "MemoryMappedFile updating error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			} finally {
				updating = false;
			}
		}

		private void auListener_OnTileChanged(object sender, TileChangedEventArgs args) {
			try {
				updating = true;

				Invoke(new MethodInvoker(delegate
				{
					listBoxMessages.Items.Add(new AUEventInfo(args, AUEventType.Single_tile));

					//Update only if the right tile.
					if (args.TileID == mmfTileNumberTextBox.Value) {
						mmfTileRenderer.Tile = mmf.Tiles[(UInt16)mmfTileNumberTextBox.Value];
						mmfGBCPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].GBC;
						mmfSGBPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].SGB;
					}
				}));
			} catch (Exception ex) {
				MessageBox.Show("Exception occured while updating from MemoryMappedFile change:\n" + new AUEventInfo(args, AUEventType.Color_set_change) + "\n\n" + ex.ToString(), "MemoryMappedFile updating error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			} finally {
				updating = false;
			}
		}

		private void auListener_OnTileRefreshNeeded(object sender, MessageEventArgs args) {
			try {
				updating = true;

				Invoke(new MethodInvoker(delegate
				{
					listBoxMessages.Items.Add(new AUEventInfo(args, AUEventType.Tile_refresh));

					mmfTileRenderer.Tile = mmf.Tiles[(UInt16)mmfTileNumberTextBox.Value];
					mmfGBCPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].GBC;
					mmfSGBPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].SGB;
				}));
			} catch (Exception ex) {
				MessageBox.Show("Exception occured while updating from MemoryMappedFile change:\n" + new AUEventInfo(args, AUEventType.Color_set_change) + "\n\n" + ex.ToString(), "MemoryMappedFile updating error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			} finally {
				updating = false;
			}
		}

		private void auListener_OnTileSizeChanged(object sender, MessageEventArgs args) {
			try {
				updating = true;

				Invoke(new MethodInvoker(delegate
				{
					listBoxMessages.Items.Add(new AUEventInfo(args, AUEventType.Tile_size));
				}));
			} catch (Exception ex) {
				MessageBox.Show("Exception occured while updating from MemoryMappedFile change:\n" + new AUEventInfo(args, AUEventType.Color_set_change) + "\n\n" + ex.ToString(), "MemoryMappedFile updating error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			} finally {
				updating = false;
			}
		}

		private void auListener_OnTotalRefreshNeeded(object sender, MessageEventArgs args) {
			try {
				updating = true;

				Invoke(new MethodInvoker(delegate
				{
					listBoxMessages.Items.Add(new AUEventInfo(args, AUEventType.Total_refresh));

					mmfTileRenderer.Tile = mmf.Tiles[(UInt16)mmfTileNumberTextBox.Value];
					mmfGBCPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].GBC;
					mmfSGBPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].SGB;

					mmfColor0MappingTextBox.Value = mmf.GBPalettes.Color0;
					mmfColor1MappingTextBox.Value = mmf.GBPalettes.Color1;
					mmfColor2MappingTextBox.Value = mmf.GBPalettes.Color2;
					mmfColor3MappingTextBox.Value = mmf.GBPalettes.Color3;
				}));
			} catch (Exception ex) {
				MessageBox.Show("Exception occured while updating from MemoryMappedFile change:\n" + new AUEventInfo(args, AUEventType.Color_set_change) + "\n\n" + ex.ToString(), "MemoryMappedFile updating error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw;
			} finally {
				updating = false;
			}
		}

		private void mmfTileNumberTextBox_ValueChanged(object sender, EventArgs e) {
			mmfTileRenderer.Tile = mmf.Tiles[(UInt16)mmfTileNumberTextBox.Value];
			mmfGBCPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].GBC;
			mmfSGBPaletteTextBox.Value = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value].SGB;
		}

		private void mmfTileRenderer_PixelClicked(object sender, GB.Shared.Controls.PixelClickEventArgs e) {
			//Ignore out of range clicks.
			if (e.x < 0 || e.x >= mmfTileRenderer.Tile.Width || e.y < 0 || e.y >= mmfTileRenderer.Tile.Height) { return; }

			Tile t = mmfTileRenderer.Tile;
			GBColor oldColor = t[e.x, e.y];

			GBColor newColor;


			if (e.mouseButton == System.Windows.Forms.MouseButtons.Left) {
				switch (oldColor) {
				case GBColor.BLACK: newColor = GBColor.DARK_GRAY; break;
				case GBColor.DARK_GRAY: newColor = GBColor.LIGHT_GRAY; break;
				case GBColor.LIGHT_GRAY: newColor = GBColor.WHITE; break;
				case GBColor.WHITE: newColor = GBColor.BLACK; break;
				default: return; //This *SHOULD NEVER* happen.
				}
			} else if (e.mouseButton == System.Windows.Forms.MouseButtons.Right) {
				switch (oldColor) {
				case GBColor.BLACK: newColor = GBColor.WHITE; break;
				case GBColor.DARK_GRAY: newColor = GBColor.BLACK; break;
				case GBColor.LIGHT_GRAY: newColor = GBColor.DARK_GRAY; break;
				case GBColor.WHITE: newColor = GBColor.LIGHT_GRAY; break;
				default: return; //This *SHOULD NEVER* happen.
				}
			} else {
				return;
			}

			t[e.x, e.y] = newColor;

			if (!updating) {
				mmfTileRenderer.Tile = mmf.Tiles[(UInt16)mmfTileNumberTextBox.Value] = t;
			}
		}

		/// <summary>
		/// When updating, we don't want to set any values again.
		/// </summary>
		private volatile bool updating = false;

		private void mmfColorMappingTextBox_ValueChanged(object sender, EventArgs e) {
			try {
				NumericUpDown upDown = sender as NumericUpDown;
				if (upDown != null) {
					if (!(upDown.Tag is int)) {
						throw new InvalidOperationException(string.Format("Sender's tag is not an int - sender is {0}, of type {1}, with a tag of type {2} and value {3}", upDown, upDown.GetType(), upDown.Tag, upDown.Tag.GetType()));
					}
					int color = (int)upDown.Tag;

					switch ((byte)upDown.Value) {
					case 0: upDown.BackColor = Color.White; upDown.ForeColor = Color.Black; break;
					case 1: upDown.BackColor = Color.LightGray; upDown.ForeColor = Color.Black; break;
					case 2: upDown.BackColor = Color.Gray; upDown.ForeColor = Color.White; break;
					case 3: upDown.BackColor = Color.Black; upDown.ForeColor = Color.White; break;
					default: upDown.BackColor = Color.Lime; upDown.ForeColor = Color.Black; break;
					}

					if (!updating) {
						mmf.GBPalettes[(byte)color] = (byte)upDown.Value;
					}
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
				throw;
			}
		}

		private void mmfGBCPaletteTextBox_ValueChanged(object sender, EventArgs e) {
			if (!updating) {
				//TODO remove the need to do this stuff with temp by adding moar indexers.
				var temp = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value];
				temp.GBC = (byte)mmfGBCPaletteTextBox.Value;
				mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value] = temp;
			}
		}

		private void mmfSGBPaletteTextBox_ValueChanged(object sender, EventArgs e) {
			if (!updating) {
				var temp = mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value];
				temp.SGB = (byte)mmfSGBPaletteTextBox.Value;
				mmf.PalMaps[(UInt16)mmfTileNumberTextBox.Value] = temp;
			}
		}
	}
}
