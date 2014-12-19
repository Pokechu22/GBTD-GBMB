using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Tiles
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

		private Tile[] tiles = new Tile[0];

		/// <summary>
		/// The tiles used on this.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		[Category("Data"), Description("The tiles used.")]
		[Browsable(false), ReadOnly(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tile this[int tile] {
			get {
				if (tile < 0 || tile >= numberOfEntries) {
					throw new ArgumentOutOfRangeException("tile", tile, "Tiles out of range - must be between 0 and " + numberOfEntries + ".");
				}
				return tiles[tile];
			}
			set {
				/*if (value == null) {
					throw new ArgumentNullException();
				}*/
				tiles[tile] = value;
				onTileChanged(tile);
			}
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
				newEntry.Tile = new Tile();
				newEntry.Number = i;
				newEntry.Name = "Entry" + i;
				if (vScrollBar1.Value + i >= numberOfEntries) {
					newEntry.Tile = new Tile();
					newEntry.Enabled = false;
				} else {
					newEntry.Tile = tiles[vScrollBar1.Value + i];
					newEntry.Enabled = true;
				}
				//TODO colors.
				entriesPanel.Controls.Add(newEntry);
			}

			OnNumberOfEntriesChanged();

			this.Resize += new EventHandler(TileList_Resize);
		}

		private void OnNumberOfEntriesChanged() {
			vScrollBar1.Maximum = numberOfEntries;
			vScrollBar1.Minimum = 0;
			vScrollBar1.Value = 0;

			Array.Resize(ref tiles, numberOfEntries);
		}

		private void TileList_Load(object sender, EventArgs e) {
			this.OnResize(e);
		}

		private void vScrollBar1_ValueChanged(object sender, EventArgs e) {
			int scrolledIndex = vScrollBar1.Value - numberOfVisibleEntries;
			if (scrolledIndex < 0) {
				scrolledIndex = 0;
			}

			for (int i = 0; i < numberOfVisibleEntries; i++) {
				TileListEntry entry = entriesPanel.Controls.Find("Entry" + i, false)[0] as TileListEntry;
				entry.Number = scrolledIndex + i;
				//TODO grab the tile.
				if (scrolledIndex + i >= numberOfEntries) {
					entry.Tile = new Tile();
					entry.Enabled = false;
				} else {
					entry.Tile = tiles[vScrollBar1.Value + i];
					entry.Enabled = true;
				}
				entry.Refresh();
			}
		}

		/// <summary>
		/// Processes a change to a tile and redraws it.
		/// </summary>
		/// <param name="tile"></param>
		private void onTileChanged(int tile) {
			//Check if tile is on screen; stop if it isn't visible since it doesn't need to be redrawn.
			int visibleIndex = tile - vScrollBar1.Value;
			if (visibleIndex < 0 || visibleIndex >= numberOfVisibleEntries) {
				return;
			}

			//Update the entry.
			TileListEntry entry = entriesPanel.Controls.Find("Entry" + visibleIndex, false)[0] as TileListEntry;
			entry.Tile = tiles[tile];
		}
	}
}
