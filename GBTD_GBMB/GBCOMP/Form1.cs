using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Tile;

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

			VRAMTileProvider t = new VRAMTileProvider(newBytes);

			this.renderData.Tile = t.getTiles().ElementAt(0);

			this.Refresh();
		}

		private void buttonPalette_Click(object sender, EventArgs e) {
			ChoosePalette p = new ChoosePalette();
			p.ShowDialog();

			this.renderData.BlackColor = p.BlackColor;
			this.renderData.DarkGrayColor = p.DarkGrayColor;
			this.renderData.LightGrayColor = p.LightGrayColor;
			this.renderData.WhiteColor = p.WhiteColor;

			this.Refresh();
		}
	}
}
