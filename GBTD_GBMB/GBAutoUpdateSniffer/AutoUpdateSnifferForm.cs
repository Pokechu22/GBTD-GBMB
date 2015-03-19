﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.AutoUpdate;

namespace GBAutoUpdateSniffer
{
	public partial class AutoUpdateSnifferForm : Form
	{
		public AutoUpdateSnifferForm() {
			InitializeComponent();
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

			byte[,] bytes = mmf.Tiles[0];
			bytes[0, 0] = 3;
			mmf.Tiles[0] = bytes;
		}

		private void listBoxMessages_SelectedIndexChanged(object sender, EventArgs e) {
			if (listBoxMessages.SelectedIndex == -1) {
				textBoxMessageInfo.Text = "";
			} else {
				textBoxMessageInfo.Text = listBoxMessages.SelectedItem.ToString();
			}
		}

		/// <summary>
		/// Adds a messege to the list of messages.
		/// </summary>
		private void addMessageToList(AUEventInfo info) {
			try {
				//Because this might come from a seperate thread, invoke the change on the right thread.
				//http://stackoverflow.com/a/142069/3991344
				listBoxMessages.Invoke(new MethodInvoker(delegate { listBoxMessages.Items.Add(info); }));
			} catch (Exception e) {
				MessageBox.Show(e.ToString(), "An exception occured while adding an item to the list.", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void auListener_OnColorPaletteChanged(object sender, MessageEventArgs args) {
			addMessageToList(new AUEventInfo(args, AUEventType.Color_set_change));
		}

		private void auListener_OnGBPaletteChanged(object sender, MessageEventArgs args) {
			addMessageToList(new AUEventInfo(args, AUEventType.Tile_palette));
		}

		private void auListener_OnTileChanged(object sender, TileChangedEventArgs args) {
			addMessageToList(new AUEventInfo(args, AUEventType.Single_tile));
		}

		private void auListener_OnTileRefreshNeeded(object sender, MessageEventArgs args) {
			addMessageToList(new AUEventInfo(args, AUEventType.Tile_refresh));
		}

		private void auListener_OnTileSizeChanged(object sender, MessageEventArgs args) {
			addMessageToList(new AUEventInfo(args, AUEventType.Tile_size));
		}

		private void auListener_OnTotalRefreshNeeded(object sender, MessageEventArgs args) {
			addMessageToList(new AUEventInfo(args, AUEventType.Total_refresh));
		}
	}
}