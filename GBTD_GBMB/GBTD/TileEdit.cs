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
		private string filePath;
		private GBRFile GBRFile;

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
				paletteChooser.ColorSet = value;
				mainTileEdit.ColorSet = value;
				tileList1.ColorSet = value;
				previewRenderer1.ColorSet = value;

				palettesMenuItem.Enabled = value.SupportsPaletteCustomization();

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

			this.ColorSet = ColorSet.GAMEBOY_POCKET;
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
			if (!updatingFromTileList) {
				this.tileList1[tileList1.SelectedEntry] = mainTileEdit.TileData;
			}

			this.previewRenderer1.TileData = mainTileEdit.TileData;

			mainTileEdit.Refresh();
		}

		private void mainTileEdit_PalatteChanged(object sender, EventArgs e) {
			if (!updatingFromTileList) {
				this.tileList1[tileList1.SelectedEntry] = mainTileEdit.TileData;
			}

			this.previewRenderer1.TileData = mainTileEdit.TileData;

			mainTileEdit.Refresh();
		}

		private void palettesMenuItem_Click(object sender, EventArgs e) {
			ChoosePalette d = new ChoosePalette(paletteChooser.PaletteData, this.ColorSet);
			d.ShowDialog();
			if (d.DialogResult == DialogResult.OK) {
				paletteChooser.PaletteData = d.Palette;
				tileList1.PaletteSet = d.Palette;
			}
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
			GBRObjectTileExport settings;
			{
				//TODO: If GBRFile is null...
				var matches = this.GBRFile.GetObjectsOfType<GBRObjectTileExport>();
				if (matches.Count != 1) {
					MessageBox.Show("Invalid number of GBRObjectTileExport: " + matches.Count,
						"Failed to read GBR file", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				settings = matches[0];
			}

			ExportDialog dialog = new ExportDialog(settings);
			dialog.ShowDialog();
			//TODO
		}

		private void importFromMenuItem_Click(object sender, EventArgs e) {
			GBRObjectTileImport settings;
			{
				//TODO: If GBRFile is null...
				var matches = this.GBRFile.GetObjectsOfType<GBRObjectTileImport>();
				if (matches.Count != 1) {
					MessageBox.Show("Invalid number of GBRObjectTileImport: " + matches.Count,
						"Failed to read GBR file", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				settings = matches[0];
			}
			ImportDialog dialog = new ImportDialog(settings);
			dialog.ShowDialog();
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
		/// Called when one of the menu items for changing the color set is clicked.
		/// </summary>
		private void colorSetMenuItems_Click(object sender, EventArgs e) {
			if (sender is MenuItem) {
				MenuItem i = sender as MenuItem;
				if (i.Tag is ColorSet) {
					this.ColorSet = (ColorSet)i.Tag;
				}
			}
		}

		private void openButton_Click(object sender, EventArgs e) {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.DefaultExt = "gbr";
			dialog.Filter = "GBR Files|*.gbr|All files|*.*";
			dialog.SupportMultiDottedExtensions = true;
			DialogResult result = dialog.ShowDialog();
			if (result != System.Windows.Forms.DialogResult.OK) {
				return;
			}
			filePath = dialog.FileName;
			using (System.IO.Stream s = dialog.OpenFile()) {
				GBRFile = new GBRFile(s);
			}
			//TODO: Move load file logic?  Mabye also move open file logic?
			GBRObjectTileSettings TileSettings;
			{
				var temp = GBRFile.GetObjectsOfType<GBRObjectTileSettings>();
				if (temp.Count != 1) {
					MessageBox.Show("Invalid number of GBRObjectTileSettings: " + temp.Count, "Failed to read GBR file", 
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				TileSettings = temp[0];
			}

			this.paletteChooser.LeftMouseColor = TileSettings.LeftColor;
			this.paletteChooser.RightMouseColor = TileSettings.RightColor;
			this.paletteChooser.MiddleMouseColor = TileSettings.MiddleMouseColor;
			this.paletteChooser.X1MouseColor = TileSettings.X1MouseColor;
			this.paletteChooser.X2MouseColor = TileSettings.X2MouseColor;

			this.toolList.AutoUpdate = TileSettings.AutoUpdate;
			this.simpleModeMenuItem.Checked = this.previewRenderer1.Simple = TileSettings.SimpleMode;
			this.mainTileEdit.NibbleMarkers = TileSettings.ShowNibbleMarkers;
			this.mainTileEdit.Grid = TileSettings.ShowGrid;

			this.ColorSet = TileSettings.ColorSet;
			//TODO: TileSettings.ReferedID

			//TODO
			//TileSettings.Bookmark1;
			//TileSettings.Bookmark2;
			//TileSettings.Bookmark3;
			//TODO
			//TileSettings.SplitHeight;
			//TileSettings.SplitWidth;
			//TileSettings.SplitOrder;
			this.ColorSet = TileSettings.ColorSet;

			//Load the tiles.
			GBRObjectTileData tiles;
			{
				var temp = GBRFile.GetObjectsOfType<GBRObjectTileData>();
				if (temp.Count != 1) {
					MessageBox.Show("Invalid number of GBRObjectTileData: " + temp.Count, "Failed to read GBR file",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				tiles = temp[0];
			}

			tileList1.TileDatas.Length = tiles.Count;
			TileData[] tileDatas = (TileData[])tileList1.TileDatas.Tiles.Clone();
			for (int i = 0; i < tiles.Count; i++) {
				tileDatas[i].tile = tiles.tiles[i];
			}
			tileList1.Tiles = tileDatas;
			
			//Load the palettes.
			GBRObjectPalettes palettes;
			{
				var temp = GBRFile.GetObjectsOfType<GBRObjectPalettes>();
				if (temp.Count != 1) {
					MessageBox.Show("Invalid number of GBRObjectPalettes: " + temp.Count, "Failed to read GBR file",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				palettes = temp[0];
			}
			
			this.paletteChooser.PaletteData = this.tileList1.PaletteSet = new PaletteData(palettes.SGBPalettes, palettes.GBCPalettes);
		}

		private void saveButton_Click(object sender, EventArgs e) {
			//TODO
		}

		private void saveAsButton_Click(object sender, EventArgs e) {
			//TODO
		}
	}
}
