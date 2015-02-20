using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Palettes;
using GB.Shared.Tiles;
using GB.Shared;
using GB.GBTD.Dialogs;
using GB.Shared.GBRFile;

namespace GB.GBTD
{
	public partial class TileEdit : Form
	{
		public Tile[] Tiles {
			get {
				return Array.ConvertAll(tileList1.Tiles, item => item.tile);
			}
			/*paletteData {
				tileList1.Tiles = (TileData[]) value.Clone();
			}*/
			//TODO
		}

		private int selectedTileNumber = 0;

		/// <summary>
		/// The index of the tileData currently selected.
		/// </summary>
		[Category("Data"), Description("The index of the tileData currently selected.")]
		public int SelectedTileNumber {
			get {
				return selectedTileNumber;
			}
			set {
				updateTile(value);
				value = selectedTileNumber;
			}
		}

		private ColorSet colorSet = ColorSet.GAMEBOY;
		public ColorSet ColorSet {
			get { return colorSet; }
			set {
				//TODO actually use this value.
				colorSet = value;

				MenuItem[] items = new MenuItem[] {
					colorSetGameboyMenuItem,
					colorSetGameboyPocketMenuItem,
					colorSetSuperGameboyMenuItem,
					colorSetGameboyColorMenuItem,
					colorSetFilteredGameboyColorMenuItem
				};
				foreach (MenuItem item in items) {
					if (item.Tag is ColorSet) {
						item.Checked = ((ColorSet)item.Tag == colorSet);
					}
				}
			}
		}

		public TileEdit() {
			InitializeComponent();

			initClipboardChangeCheck();

			previewRenderer1.TileData = mainTileEdit.TileData;
		}

		protected override void OnLoad(EventArgs e) {
			//Loads the menu.
			//http://stackoverflow.com/a/28462365/3991344
			this.Menu = this.mainMenu;

			MoveControlsFromTileSize();

			base.OnLoad(e);
		}

		private volatile bool updatingFromTileList = false;

		/// <summary>
		/// Called when the selected tileData is changed.
		/// </summary>
		protected void updateTile(int tile) {
			if (updatingFromTileList) { return; }

			updatingFromTileList = true;
			this.mainTileEdit.TileData = tileList1.TileDatas.Tiles[tile];
			this.paletteChooser.SelectedRow = tileList1.TileDatas.Tiles[tile].GetRow(ColorSet);
			updatingFromTileList = false;
		}

		/// <summary>
		/// Moves all of the controls based off of their size.
		/// </summary>
		protected void MoveControlsFromTileSize() {
			this.SuspendLayout();

			this.previewRenderer1.Location = new Point(33 + mainTileEdit.Width + 9, 39);
			this.paletteChooser.Location = new Point(34, mainTileEdit.Location.Y + mainTileEdit.Height + 3);

			this.tileEditBorder.Size = new Size(33 + mainTileEdit.Width + 9 + previewRenderer1.Width + 5, 32 + mainTileEdit.Height + 4);
			this.ClientSize = new Size(tileEditBorder.Width + 3 + tileList1.Width, tileEditBorder.Top + tileEditBorder.Height + 1);

			this.tileList1.Location = new Point(tileEditBorder.Location.X + tileEditBorder.Width + 3, tileEditBorder.Location.Y + 4);

			this.ResumeLayout();

			this.Refresh();
		}

		private void tileEditBorder_Paint(object sender, PaintEventArgs e) {
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, tileEditBorder.Width, tileEditBorder.Height, Border3DStyle.SunkenOuter);
		}

		private void toolList1_SelectedToolChanged(object sender, EventArgs e) {
			ToolList list = sender as ToolList;
			if (list != null) {
				mainTileEdit.EditorTypeID = list.SelectedTool;
				switch (list.SelectedTool) {
				case TileEditorID.NoEdit:
					penMenuItem.Checked = false;
					floodFillMenuItem.Checked = false;
					break;
				case TileEditorID.PixelEdit:
					penMenuItem.Checked = true;
					floodFillMenuItem.Checked = false;
					break;
				case TileEditorID.FloodFill:
					penMenuItem.Checked = false;
					floodFillMenuItem.Checked = true;
					break;
				}
			}
		}

		private void gbtdgbcPaletteChooser1_MouseButtonColorChanged(object sender, EventArgs e) {
			GBTDPaletteChooser chooser = sender as GBTDPaletteChooser;
			if (chooser != null) {
				mainTileEdit.LeftMouseColor = chooser.LeftMouseColor;
				mainTileEdit.RightMouseColor = chooser.RightMouseColor;
				mainTileEdit.MiddleMouseColor = chooser.MiddleMouseColor;
				mainTileEdit.XButton1MouseColor = chooser.X1MouseColor;
				mainTileEdit.XButton2MouseColor = chooser.X2MouseColor;
			}
		}

		private void mainTileEdit_TileChanged(object sender, EventArgs e) {
			TileData data = new TileData();
			data.tile = mainTileEdit.Tile;
			data.paletteData = this.paletteChooser.PaletteData;
			data.SetRow(ColorSet, (UInt16)this.paletteChooser.SelectedRow);

			if (!updatingFromTileList) {
				this.tileList1[tileList1.SelectedEntry] = data;
			}

			this.previewRenderer1.TileData = data;

			mainTileEdit.Refresh();
		}

		private void mainTileEdit_PalatteChanged(object sender, EventArgs e) {
			TileData data = new TileData();
			data.tile = mainTileEdit.Tile;
			data.paletteData = this.paletteChooser.PaletteData;
			data.SetRow(ColorSet, (UInt16)this.paletteChooser.SelectedRow);

			if (!updatingFromTileList) {
				this.tileList1[tileList1.SelectedEntry] = data;
			}

			this.previewRenderer1.TileData = data;

			mainTileEdit.Refresh();
		}

		private void palettesMenuItem_Click(object sender, EventArgs e) {
			/*ChoosePalette d = new ChoosePalette(null, this.ColorSet);
			d.ShowDialog();
			if (d.DialogResult == DialogResult.OK) {
				paletteChooser.PaletteData = d.Palette;
				tileList1.PaletteSet = d.Set;
			}*/
		}

		private void paletteChooser_SelectedPaletteChanged(object sender, EventArgs e) {
			if (updatingFromTileList) { return; }

			mainTileEdit.PaletteData = paletteChooser.PaletteData;
			mainTileEdit.PaletteID = paletteChooser.SelectedRow;
		}

		private void scrollLeftClicked(object sender, EventArgs e) {
			mainTileEdit.Tile = TileTransform.ScrolledLeft(mainTileEdit.Tile);
		}

		private void scrollDownClicked(object sender, EventArgs e) {
			mainTileEdit.Tile = TileTransform.ScrolledDown(mainTileEdit.Tile);
		}

		private void scrollRightClicked(object sender, EventArgs e) {
			mainTileEdit.Tile = TileTransform.ScrolledRight(mainTileEdit.Tile);
		}

		private void scrollUpClicked(object sender, EventArgs e) {
			mainTileEdit.Tile = TileTransform.ScrolledUp(mainTileEdit.Tile);
		}

		private void flipHorizontallyClicked(object sender, EventArgs e) {
			mainTileEdit.Tile = TileTransform.FlippedHoriziontally(mainTileEdit.Tile);
		}

		private void flipVerticallyClicked(object sender, EventArgs e) {
			mainTileEdit.Tile = TileTransform.FlippedVertically(mainTileEdit.Tile);
		}

		private void rotateClockwiseClicked(object sender, EventArgs e) {
			mainTileEdit.Tile = TileTransform.RotateClockwise(mainTileEdit.Tile);
		}

		private void tileList1_SelectedEntryChanged(object sender, EventArgs e) {
			updateTile(tileList1.SelectedEntry);
		}

		private void cutButtonClicked(object sender, EventArgs e) {
			Clipboard.SetImage(mainTileEdit.Tile.ToImage());
			mainTileEdit.Tile = new Tile(8, 8); //Empty.
		}

		private void copyButtonClicked(object sender, EventArgs e) {
			Clipboard.SetImage(mainTileEdit.Tile.ToImage());
		}

		private void pasteButtonClicked(object sender, EventArgs e) {
			if (!Clipboard.ContainsImage()) {
				return;
			}
			mainTileEdit.Tile = Tile.FromImage(Clipboard.GetImage(), 8, 8);
		}

		private void initClipboardChangeCheck() {
			NativeMethods.AddClipboardFormatListener(Handle);
			OnClipboardUpdate();
		}

		private void OnClipboardUpdate() {
			this.pasteMenuItem.Enabled = Clipboard.ContainsImage();
			this.pasteButton.Enabled = Clipboard.ContainsImage();
		}

		protected override void WndProc(ref Message m) {
			if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE) {
				OnClipboardUpdate();
			}

			base.WndProc(ref m);
		}

		private void exportToMenuItem_Click(object sender, EventArgs e) {
			ExportDialog dialog = new ExportDialog();
			dialog.ShowDialog();
			//TODO
		}

		private void importFromMenuItem_Click(object sender, EventArgs e) {
			ImportDialog dialog = new ImportDialog();
			dialog.ShowDialog();
			//TODO
		}

		private void tileCountMenuItem_Click(object sender, EventArgs e) {
			TileCountDialog dialog = new TileCountDialog();
			dialog.ShowDialog();
			//TODO
		}

		private void size8x8MenuItem_Click(object sender, EventArgs e) {
			//TODO do this better...  This is temporary.
			mainTileEdit.Tile = new Tile(8, 8);

			size8x8MenuItem.Checked = true;
			size8x16MenuItem.Checked = false;
			size16x16MenuItem.Checked = false;
			size32x32MenuItem.Checked = false;

			this.MoveControlsFromTileSize();
		}

		private void size8x16MenuItem_Click(object sender, EventArgs e) {
			mainTileEdit.Tile = new Tile(8, 16);

			size8x8MenuItem.Checked = false;
			size8x16MenuItem.Checked = true;
			size16x16MenuItem.Checked = false;
			size32x32MenuItem.Checked = false;

			this.MoveControlsFromTileSize();
		}

		private void size16x16MenuItem_Click(object sender, EventArgs e) {
			mainTileEdit.Tile = new Tile(16, 16);

			size8x8MenuItem.Checked = false;
			size8x16MenuItem.Checked = false;
			size16x16MenuItem.Checked = true;
			size32x32MenuItem.Checked = false;

			this.MoveControlsFromTileSize();
		}

		private void size32x32MenuItem_Click(object sender, EventArgs e) {
			mainTileEdit.Tile = new Tile(32, 32);

			size8x8MenuItem.Checked = false;
			size8x16MenuItem.Checked = false;
			size16x16MenuItem.Checked = false;
			size32x32MenuItem.Checked = true;

			this.MoveControlsFromTileSize();
		}

		private void simpleModeMenuItem_Click(object sender, EventArgs e) {
			simpleModeMenuItem.Checked ^= true;
			previewRenderer1.Simple = simpleModeMenuItem.Checked;

			this.MoveControlsFromTileSize();
		}

		private void nibbleMarkersMenuItem_Click(object sender, EventArgs e) {
			nibbleMarkersMenuItem.Checked ^= true;
			mainTileEdit.NibbleMarkers = nibbleMarkersMenuItem.Checked;
		}

		private void gridMenuItem_Click(object sender, EventArgs e) {
			gridMenuItem.Checked ^= true;
			mainTileEdit.Grid = gridMenuItem.Checked;
		}

		//These two probably aren't the most logical yet.
		private void autoUpdateMenuItem_Click(object sender, EventArgs e) {
			toolList.AutoUpdate ^= true;
		}

		private void toolList_AutoUpdateChanged(object sender, EventArgs e) {
			autoUpdateMenuItem.Checked = toolList.AutoUpdate;
		}

		private void penMenuItem_Click(object sender, EventArgs e) {
			toolList.SelectedTool = TileEditorID.PixelEdit;
		}

		private void floodFillMenuItem_Click(object sender, EventArgs e) {
			toolList.SelectedTool = TileEditorID.FloodFill;
		}

		/// <summary>
		/// Called when one of the menu items for changing the color paletteData is clicked.
		/// </summary>
		private void colorSetMenuItems_Click(object sender, EventArgs e) {
			if (sender is MenuItem) {
				MenuItem i = sender as MenuItem;
				if (i.Tag is ColorSet) {
					this.ColorSet = (ColorSet)i.Tag;
				}
			}
		}
	}
}
