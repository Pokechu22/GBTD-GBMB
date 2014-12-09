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

		private TileListEntry[] visibleEntries = new TileListEntry[0];

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

			foreach (var c in visibleEntries) {
				this.Controls.Remove(c);
			}
			visibleEntries = new TileListEntry[numberOfVisibleEntries];
			for (int i = 0; i < numberOfVisibleEntries; i++) {
				TileListEntry newEntry = new TileListEntry();
				newEntry.Location = new Point(1, 1 + (ENTRY_HEIGHT * i));
				newEntry.Tile = new Tile(); //TODO
				newEntry.Number = i;
				newEntry.Name = "Entry" + i;
				visibleEntries[i] = newEntry;
				//TODO colors.
				@this.Controls.Add(visibleEntries[i]); //Note: @this is not this control; it is the used background.  This is for clarity.
			}

			this.Resize += new EventHandler(TileList_Resize);
		}

		private void TileList_Load(object sender, EventArgs e) {
			this.OnResize(e);
		}
	}
}
