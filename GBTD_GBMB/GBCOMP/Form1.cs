﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Tile;
using GB.Shared.Palette;

namespace GBRenderer
{
	public partial class Form1 : Form
	{
		/// <summary>
		/// All of the bytes in the used file.
		/// </summary>
		private byte[] fullBytes = {
								   0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
							   };

		public Form1() {
			InitializeComponent();
		}

		private void buttonOpen_Click(object sender, EventArgs e) {
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				//Fun with goto.  Retry the code if an error occurs.
			retry: 
				using (System.IO.Stream data = openFileDialog.OpenFile()) {
					if (data.Length < 16) {
						if (MessageBox.Show("File is too short, must be at least 16 bytes",
							"Error: File too short",
							MessageBoxButtons.RetryCancel,
							MessageBoxIcon.Error) == DialogResult.Retry) {
							goto retry;
						} else {
							return;
						}
					}
					fullBytes = new byte[data.Length];
					data.Read(fullBytes, 0, (int)data.Length);
				}

				
				this.offsetUpDown.Value = 0;
				this.offsetUpDown.Maximum = fullBytes.Length - 17;

				this.offsetUpDown_ValueChanged(offsetUpDown, new EventArgs());
			}
		}

		private void offsetUpDown_ValueChanged(object sender, EventArgs e) {
			int index = (int)offsetUpDown.Value;
			if (index + 16 >= fullBytes.Length) {
				throw new InvalidOperationException("OffsetUpDown went out of bounds!  (" + index + " + 16 is more than file length " + fullBytes.Length + ")");
			}

			byte[] newBytes = new byte[16];

			//Not the best way to use a tileprovider, but it works.
			for (int i = 0; i < newBytes.Length; i++) {
				newBytes[i] = fullBytes[i + index];
			}

			VRAMTileParser parser = new VRAMTileParser(newBytes);

			this.tileRenderer.Tile = parser.ElementAt(0);

			this.Refresh();
		}

		private void buttonPalette_Click(object sender, EventArgs e) {
			GBCChoosePalette p = new GBCChoosePalette(this.gbtdPaletteChooser1.Set);

			DialogResult result = p.ShowDialog();
			if (result != DialogResult.OK) {
				return;
			}

			this.tileRenderer.BlackColor = p.Set.Rows[0][GBColor.BLACK];
			this.tileRenderer.DarkGrayColor = p.Set.Rows[0][GBColor.DARK_GRAY];
			this.tileRenderer.LightGrayColor = p.Set.Rows[0][GBColor.LIGHT_GRAY];
			this.tileRenderer.WhiteColor = p.Set.Rows[0][GBColor.WHITE];

			this.gbtdPaletteChooser1.Set = p.Set;

			this.Refresh();
		}

		private void Form1_Load(object sender, EventArgs e) {

		}
	}
}
