using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Palettes;

namespace GB.Shared.Tiles
{
	public partial class TileListControl : UserControl
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

		private TileDataList tileDatas = new TileDataList(16, 8, 8);

		/// <summary>
		/// All of the tiles and their palettes.
		/// </summary>
		[Category("Data"), Description("All of the tiles and their palettes.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TileDataList TileDatas {
			get { return tileDatas; }
			set { if (value == null) { throw new ArgumentNullException("value"); } tileDatas = value; OnNumberOfEntriesChanged(); }
		}

		/// <summary>
		/// The total number of tiles in total.
		/// </summary>
		[Category("Data"), Description("The number of tiles in total.")]
		public int NumberOfEntries {
			get { return tileDatas.Length; }
			set { tileDatas.Length = value; OnNumberOfEntriesChanged(); }
		}

		public PaletteSet PaletteSet {
			get { return tileDatas.Palette; }
			set { tileDatas.Palette = value; onTilesChanged(); }
		}

		private int selectedEntry = 0;
		public int SelectedEntry {
			get { return selectedEntry; }
			set { selectedEntry = value; OnSelectedEntryChanged(); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TileData[] Tiles {
			get {
				return tileDatas.Tiles;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				tileDatas.Tiles = value;
				onTilesChanged();
			}
		}

		/// <summary>
		/// The tiles used on this.
		/// </summary>
		/// <param name="tileData"></param>
		/// <returns></returns>
		[Category("Data"), Description("The tiles used.")]
		[Browsable(false), ReadOnly(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TileData this[int tile] {
			get {
				if (tile < 0 || tile >= tileDatas.Length) {
					throw new ArgumentOutOfRangeException("tileData", tile, "Tiles out of range - must be between 0 and " + tileDatas.Length + ".");
				}
				return tileDatas.Tiles[tile];
			}
			set {
				tileDatas.Tiles[tile] = value;
				onTileChanged(tile);
			}
		}

		/// <summary>
		/// Event for when the selected entry has been changed.
		/// </summary>
		[Category("Property Changed"), Description("Raised when the selected entry has been changed.")]
		public event EventHandler SelectedEntryChanged;

		public TileListControl() {
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
			this.SuspendLayout();
			
			this.Width = WIDTH;

			int tempHeight = this.Height - 2;
			//Round to the proper size, and setup numberOfVisibleEntries.
			numberOfVisibleEntries = tempHeight / ENTRY_HEIGHT;
			this.Height = (numberOfVisibleEntries * ENTRY_HEIGHT) + 2;

			entriesPanel.Controls.Clear();

			for (int i = 0; i < numberOfVisibleEntries; i++) {
				TileListEntryControl newEntry = new TileListEntryControl();
				newEntry.Location = new Point(1, 1 + (ENTRY_HEIGHT * i));
				newEntry.TileData = new TileData();
				newEntry.Number = i;
				newEntry.Name = "Entry" + i;
				if (vScrollBar1.Value + i >= tileDatas.Length) {
					newEntry.TileData = new TileData();
					newEntry.Enabled = false;
				} else {
					newEntry.TileData = tileDatas.Tiles[vScrollBar1.Value + i];
					newEntry.Enabled = true;
				}
				newEntry.Selected = (newEntry.Number == selectedEntry); //ActiveSet selected if selected.

				newEntry.Click += new EventHandler(this.OnEntryClicked);
				//TODO colors.
				entriesPanel.Controls.Add(newEntry);
			}

			OnNumberOfEntriesChanged();

			this.Resize += new EventHandler(TileList_Resize);
			this.ResumeLayout();
		}

		private void OnNumberOfEntriesChanged() {
			vScrollBar1.Maximum = tileDatas.Length;
			vScrollBar1.Minimum = 0;
			vScrollBar1.Value = 0;
		}

		private void TileList_Load(object sender, EventArgs e) {
			this.OnResize(e);
		}

		private int scrolledIndex = 0;

		private void vScrollBar1_ValueChanged(object sender, EventArgs e) {
			this.SuspendLayout();

			int oldScrolledIndex = scrolledIndex;
			scrolledIndex = vScrollBar1.Value - numberOfVisibleEntries;
			if (scrolledIndex < 0) {
				scrolledIndex = 0;
			}
			if (scrolledIndex == oldScrolledIndex) {
				return;
			}

			for (int i = 0; i < numberOfVisibleEntries; i++) {
				TileListEntryControl entry = entriesPanel.Controls.Find("Entry" + i, false)[0] as TileListEntryControl;
				entry.Number = scrolledIndex + i;
				
				if (scrolledIndex + i >= tileDatas.Length) {
					entry.TileData = new TileData();
					entry.Enabled = false;
				} else {
					entry.TileData = tileDatas.Tiles[scrolledIndex + i];
					entry.Enabled = true;
				}
				entry.Selected = (entry.Number == selectedEntry); //ActiveSet selected if selected.
			}
			this.ResumeLayout();
		}

		/// <summary>
		/// Processes a change to a tileData and redraws it.
		/// </summary>
		/// <param name="tileData"></param>
		private void onTileChanged(int tile) {
			//Check if tileData is on screen; stop if it isn't visible since it doesn't need to be redrawn.
			int visibleIndex = tile - scrolledIndex;
			if (visibleIndex < 0 || visibleIndex >= numberOfVisibleEntries) {
				return;
			}

			//Update the entry.
			TileListEntryControl entry = entriesPanel.Controls.Find("Entry" + visibleIndex, false)[0] as TileListEntryControl;
			entry.TileData = tileDatas.Tiles[tile];
		}

		/// <summary>
		/// Processes a change to all tiles.
		/// </summary>
		/// <param name="tileData"></param>
		private void onTilesChanged() {
			scrolledIndex = vScrollBar1.Value - numberOfVisibleEntries;
			if (scrolledIndex < 0) {
				scrolledIndex = 0;
			}

			for (int i = 0; i < numberOfVisibleEntries; i++) {
				TileListEntryControl entry = entriesPanel.Controls.Find("Entry" + i, false)[0] as TileListEntryControl;

				if (scrolledIndex + i >= tileDatas.Length) {
					entry.TileData = new TileData();
					entry.Enabled = false;
				} else {
					entry.TileData = tileDatas.Tiles[scrolledIndex + i];
					entry.Enabled = true;
				}
				entry.Selected = (entry.Number == selectedEntry); //ActiveSet selected if selected.
			}
		}

		/// <summary>
		/// Called when an entry is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OnEntryClicked(object sender, EventArgs args) {
			TileListEntryControl entry = sender as TileListEntryControl;
			SelectedEntry = entry.Number;
		}

		/// <summary>
		/// Called when the selected entry is changed.
		/// </summary>
		private void OnSelectedEntryChanged() {
			foreach (TileListEntryControl e in entriesPanel.Controls) {
				e.Selected = (e.Number == selectedEntry); //ActiveSet selected if selected.
			}

			if (SelectedEntryChanged != null) {
				SelectedEntryChanged(this, new EventArgs());
			}
		}
	}
}
