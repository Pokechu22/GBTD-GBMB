using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Tile
{
	public partial class TileList : UserControl
	{
		/// <summary>
		/// Width of the control.
		/// </summary>
		private const int WIDTH = 56;

		private const int ENTRY_HEIGHT = 17;

		/// <summary>
		/// The number of entries visible.
		/// </summary>
		private int numberOfVisibleEntries = 0;

		private int numberOfEntries = 0;
		/// <summary>
		/// The total number of tiles in total.
		/// </summary>
		[Category("Data"), Description("The number of tiles in total.")]
		public int NumberOfEntries {
			get { return numberOfEntries; }
			set { numberOfEntries = value; OnNumberOfEntriesChanged(); }
		}

		public TileList() {
			InitializeComponent();
		}

		private void background_Paint(object sender, PaintEventArgs e) {
			Control c = sender as Control;
			if (c != null) {
				ControlPaint.DrawBorder3D(e.Graphics, 0, 0, c.Width, c.Height, Border3DStyle.SunkenOuter, Border3DSide.Bottom | Border3DSide.Right);
				ControlPaint.DrawBorder3D(e.Graphics, 0, 0, c.Width, c.Height, Border3DStyle.SunkenOuter, Border3DSide.Top | Border3DSide.Left);
			}
		}

		private void TileList_Resize(object sender, EventArgs e) {
			this.Resize -= new EventHandler(TileList_Resize);
			
			this.Width = WIDTH;

			int tempHeight = this.Height - 2;
			//Round to the proper size, and setup numberOfVisibleEntries.
			numberOfVisibleEntries = tempHeight / ENTRY_HEIGHT;
			this.Height = (numberOfVisibleEntries * ENTRY_HEIGHT) + 2;

			entriesPanel.Controls.Clear();

			for (int i = 0; i < numberOfVisibleEntries; i++) {
				TileListEntry newEntry = new TileListEntry();
				newEntry.Location = new Point(1, 1 + (ENTRY_HEIGHT * i));
				newEntry.Tile = new Tile(); //TODO
				newEntry.Number = i;
				newEntry.Name = "Entry" + i;
				if (vScrollBar1.Value + i >= numberOfEntries) {
					newEntry.Enabled = false;
				} else {
					newEntry.Enabled = true;
				}
				//TODO colors.
				entriesPanel.Controls.Add(newEntry);
			}

			OnNumberOfEntriesChanged();

			this.Resize += new EventHandler(TileList_Resize);
		}

		private void OnNumberOfEntriesChanged() {
			int max = numberOfEntries - numberOfVisibleEntries;

			if (max < 0) { max = 0; }
			vScrollBar1.Maximum = max;
			vScrollBar1.Minimum = 0;
			vScrollBar1.Value = 0;
		}

		private void TileList_Load(object sender, EventArgs e) {
			this.OnResize(e);
		}

		private void vScrollBar1_ValueChanged(object sender, EventArgs e) {
			for (int i = 0; i < numberOfVisibleEntries; i++) {
				TileListEntry entry = entriesPanel.Controls.Find("Entry" + i, false)[0] as TileListEntry;
				entry.Number = vScrollBar1.Value + i;
				//TODO grab the tile.
				if (vScrollBar1.Value + i >= numberOfEntries) {
					entry.Enabled = false;
				} else {
					entry.Enabled = true;
				}
				entry.Refresh();
			}
		}
	}
}
