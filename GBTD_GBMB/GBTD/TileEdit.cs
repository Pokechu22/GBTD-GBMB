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

namespace GB.GBTD
{
	public partial class TileEdit : Form
	{
		private class HoverChangingToolStripButton : ToolStripButton
		{
			/// <summary>
			/// Whether or not currently being hovered over.
			/// </summary>
			private bool hovered = false;

			/// <summary>
			/// Image used when hovered over.
			/// </summary>
			private Image hoveredImage;
			/// <summary>
			/// Image used elsewhere.
			/// </summary>
			private Image nonhoveredImage;

			/// <summary>
			/// Image used when hovered over.
			/// </summary>
			[Category("Appearance"), Description("The image used when hovered over.")]
			public Image HoveredImage {
				get { return hoveredImage; }
				set { if (value == null) { /*throw new ArgumentNullException();*/value = Image; } hoveredImage = value; updateImage(); }
			}
			/// <summary>
			/// Image used when not hovered over.
			/// </summary>
			[Category("Appearance"), Description("The image used when not hovered over.")]
			public Image NonHoveredImage {
				get { return nonhoveredImage; }
				set { if (value == null) { /*throw new ArgumentNullException();*/value = Image; } nonhoveredImage = value; updateImage(); }
			}

			[EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public override Image Image {
				get { return base.Image; }
				set { base.Image = value; }
			}

			protected override void OnMouseEnter(EventArgs e) {
				base.OnMouseEnter(e);
				hovered = true;
				updateImage();
			}
			protected override void OnMouseLeave(EventArgs e) {
				base.OnMouseLeave(e);
				hovered = false;
				updateImage();
			}

			protected void updateImage() {
				this.Image = (hovered ? hoveredImage : nonhoveredImage);
			}
		}

		public Tile[] Tiles {
			get {
				return Array.ConvertAll(tileList1.Tiles, item => item.tile);
			}
			/*set {
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

		public TileEdit() {
			InitializeComponent();

			initClipboardChangeCheck();

			previewRenderer1.TileData = mainTileEdit.TileData;
		}

		private volatile bool updatingFromTileList = false;

		/// <summary>
		/// Called when the selected tileData is changed.
		/// </summary>
		protected void updateTile(int tile) {
			if (updatingFromTileList) { return; }

			updatingFromTileList = true;
			this.mainTileEdit.TileData = tileList1.TileDatas.Tiles[tile];
			this.paletteChooser.SelectedRow = tileList1.TileDatas.Tiles[tile].paletteID;
			updatingFromTileList = false;
		}

		private void tileEditBorder_Paint(object sender, PaintEventArgs e) {
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, tileEditBorder.Width, tileEditBorder.Height, Border3DStyle.SunkenOuter);
		}

		private void toolList1_SelectedToolChanged(object sender, EventArgs e) {
			ToolList list = sender as ToolList;
			if (list != null) {
				mainTileEdit.EditorTypeID = list.SelectedTool;
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
			data.set = this.paletteChooser.Set;
			data.paletteID = this.paletteChooser.SelectedRow;

			if (!updatingFromTileList) {
				this.tileList1[tileList1.SelectedEntry] = data;
			}

			this.previewRenderer1.TileData = data;

			mainTileEdit.Refresh();
		}

		private void mainTileEdit_PalatteChanged(object sender, EventArgs e) {
			TileData data = new TileData();
			data.tile = mainTileEdit.Tile;
			data.set = this.paletteChooser.Set;
			data.paletteID = this.paletteChooser.SelectedRow;

			if (!updatingFromTileList) {
				this.tileList1[tileList1.SelectedEntry] = data;
			}

			this.previewRenderer1.TileData = data;

			mainTileEdit.Refresh();
		}

		private void autoUpdatedToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			toolList.AutoUpdate = autoUpdatedToolStripMenuItem.Checked;
		}

		private void gridToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			mainTileEdit.Grid = gridToolStripMenuItem.Checked;
		}

		private void palettesToolStripMenuItem_Click(object sender, EventArgs e) {
			ChoosePalette d = new ChoosePalette(paletteChooser.Set/*.Clone() as Palette_Set*/);
			d.ShowDialog();
			if (d.DialogResult == DialogResult.OK) {
				paletteChooser.Set = d.Set;
				tileList1.PaletteSet = d.Set;
			}
		}

		private void paletteChooser_SelectedPaletteChanged(object sender, EventArgs e) {
			if (updatingFromTileList) { return; }

			mainTileEdit.PaletteSet = paletteChooser.Set;
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
		}

		private void OnClipboardUpdate() {
			this.pasteTileToolStripMenuItem.Enabled = Clipboard.ContainsImage();
			this.pasteToolStripButton.Enabled = Clipboard.ContainsImage();
		}

		protected override void WndProc(ref Message m) {
			if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE) {
				OnClipboardUpdate();
			}

			base.WndProc(ref m);
		}

		private void exportToToolStripMenuItem_Click(object sender, EventArgs e) {
			ExportDialog dialog = new ExportDialog();
			dialog.ShowDialog();
			//TODO
		}

		private void improtFromToolStripMenuItem_Click(object sender, EventArgs e) {
			ImportDialog dialog = new ImportDialog();
			dialog.ShowDialog();
			//TODO
		}

		private void tileCountToolStripMenuItem_Click(object sender, EventArgs e) {
			TileCountDialog dialog = new TileCountDialog();
			dialog.ShowDialog();
			//TODO
		}

		private void simpleToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			previewRenderer1.Simple = simpleToolStripMenuItem.Checked;
		}

		private void nibbleMarkersToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			mainTileEdit.NibbleMarkers = nibbleMarkersToolStripMenuItem.Checked;
		}

		private void size8x8ToolStripMenuItem_Click(object sender, EventArgs e) {
			//TODO do this better...  This is temporary.
			mainTileEdit.Tile = new Tile(8, 8);
			
			size8x8ToolStripMenuItem.Checked = true;
			size8x16ToolStripMenuItem.Checked = false;
			size16x16ToolStripMenuItem.Checked = false;
			size32x32ToolStripMenuItem.Checked = false;
		}

		private void size8x16ToolStripMenuItem_Click(object sender, EventArgs e) {
			mainTileEdit.Tile = new Tile(8, 16);

			size8x8ToolStripMenuItem.Checked = false;
			size8x16ToolStripMenuItem.Checked = true;
			size16x16ToolStripMenuItem.Checked = false;
			size32x32ToolStripMenuItem.Checked = false;
		}

		private void size16x16ToolStripMenuItem_Click(object sender, EventArgs e) {
			mainTileEdit.Tile = new Tile(16, 16);

			size8x8ToolStripMenuItem.Checked = false;
			size8x16ToolStripMenuItem.Checked = false;
			size16x16ToolStripMenuItem.Checked = true;
			size32x32ToolStripMenuItem.Checked = false;
		}

		private void size32x32ToolStripMenuItem_Click(object sender, EventArgs e) {
			mainTileEdit.Tile = new Tile(32, 32);

			size8x8ToolStripMenuItem.Checked = false;
			size8x16ToolStripMenuItem.Checked = false;
			size16x16ToolStripMenuItem.Checked = false;
			size32x32ToolStripMenuItem.Checked = true;
		}
	}
}
