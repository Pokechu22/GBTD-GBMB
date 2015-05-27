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
using System.IO;
using GB.Shared.AutoUpdate;

namespace GB.GBTD
{
	public partial class TileEdit : Form
	{
		private string filePath;
		private GBRFile gbrFile;

		private AUMemMappedFile mmf;

		public ColorSet ColorSet {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ColorSet; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ColorSet = value;

				tileList.ColorSet = value;
				mainTileEdit.ColorSet = value;
			}
		}

		public bool AutoUpdate {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().AutoUpdate; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().AutoUpdate = value;

				autoUpdateMenuItem.Checked = value;
				toolList.AutoUpdate = value;

				auMessenger.Enabled = value;
			}
		}

		public bool ShowNibbleMarkers {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ShowNibbleMarkers; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ShowNibbleMarkers = value;

				nibbleMarkersMenuItem.Checked = value;
				mainTileEdit.DrawNibbleMarkers = value;
			}
		}

		public bool ShowGrid {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ShowGrid; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ShowGrid = value;

				gridMenuItem.Checked = value;
				mainTileEdit.DrawGrid = value;
			}
		}

		public bool SimpleMode {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().SimpleMode; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().SimpleMode = value;
				var tileSet = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

				simpleModeMenuItem.Checked = value;

				UpdateSize();
			}
		}

		public UInt16 Bookmark1 {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark1; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark1 = value;
				tileList.Bookmark1 = value;

				gotoBookmark1MenuItem.Enabled = (value < gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles.Length);
				clearBookmark1MenuItem.Enabled = (value != 0xFFFF);
			}
		}
		public UInt16 Bookmark2 {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark2; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark2 = value;
				tileList.Bookmark2 = value;

				gotoBookmark2MenuItem.Enabled = (value < gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles.Length);
				clearBookmark2MenuItem.Enabled = (value != 0xFFFF);
			}
		}
		public UInt16 Bookmark3 {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark3; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark3 = value;
				tileList.Bookmark3 = value;

				gotoBookmark3MenuItem.Enabled = (value < gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles.Length);
				clearBookmark3MenuItem.Enabled = (value != 0xFFFF);
			}
		}

		public UInt16 SelectedTile {
			get { return tileList.SelectedTile; }
			set {
				tileList.SelectedTile = value;
				mainTileEdit.SelectedTile = value;
			}
		}

		public GBColor LeftColor {
			get { return mainTileEdit.LeftColor; }
			set {
				mainTileEdit.LeftColor = value;
			}
		}
		public GBColor RightColor {
			get { return mainTileEdit.RightColor; }
			set {
				mainTileEdit.RightColor = value;
			}
		}
		public GBColor MiddleColor {
			get { return mainTileEdit.MiddleColor; }
			set {
				mainTileEdit.MiddleColor = value;
			}
		}
		public GBColor X1Color {
			get { return mainTileEdit.X1Color; }
			set {
				mainTileEdit.X1Color = value;
			}
		}
		public GBColor X2Color {
			get { return mainTileEdit.X2Color; }
			set {
				mainTileEdit.X2Color = value;
			}
		}

		public TileEditorID TileEditor {
			get { return toolList.SelectedTool; }
			set {
				toolList.SelectedTool = value;

				switch (value) {
				case TileEditorID.NoEdit:
					mainTileEdit.TileEditor = new NoEditTileEditor();
					break;
				case TileEditorID.PixelEdit:
					mainTileEdit.TileEditor = new PixelTileEditor();
					break;
				case TileEditorID.FloodFill:
					mainTileEdit.TileEditor = new FloodFillTileEditor();
					break;
				default:
					mainTileEdit.TileEditor = null;
					break;
				}
			}
		}

		public TileEdit() {
			InitializeComponent();

			initClipboardChangeCheck();
		}

		protected override void OnLoad(EventArgs e) {
			//Loads the menu.
			//http://stackoverflow.com/a/28462365/3991344
			this.Menu = this.mainMenu;

			LoadTileFile(new GBRFile());

			base.OnLoad(e);
		}

		private void LoadTileFile(String path) {
			this.filePath = path;

			using (Stream stream = File.OpenRead(path)) {
				LoadTileFile(new GBRFile(stream));
			}
		}

		private void LoadTileFile(GBRFile file) {
			this.gbrFile = file;

			var tileSet = file.GetOrCreateObjectOfType<GBRObjectTileData>();
			var palettes = file.GetOrCreateObjectOfType<GBRObjectPalettes>();
			var paletteMapping = file.GetOrCreateObjectOfType<GBRObjectTilePalette>();
			var settings = file.GetOrCreateObjectOfType<GBRObjectTileSettings>();
			
			PaletteData paletteData = new PaletteData(palettes.SGBPalettes, palettes.GBCPalettes);

			tileSet.SizeChanged += new EventHandler(tileSet_SizeChanged);
			tileSet.CountChanged += new EventHandler(tileSet_CountChanged);

			setDisplayedTileSize(tileSet.Width, tileSet.Height);

			tileList.TileSet = tileSet;
			tileList.PaletteData = paletteData;
			tileList.PaletteMapping = paletteMapping;

			mainTileEdit.TileSet = tileSet;
			mainTileEdit.Palettes = palettes;
			mainTileEdit.PaletteMapping = paletteMapping;

			this.AutoUpdate = settings.AutoUpdate;
			this.ColorSet = settings.ColorSet;
			this.ShowNibbleMarkers = settings.ShowNibbleMarkers;
			this.ShowGrid = settings.ShowGrid;
			this.SimpleMode = settings.SimpleMode;
			this.Bookmark1 = settings.Bookmark1;
			this.Bookmark2 = settings.Bookmark2;
			this.Bookmark3 = settings.Bookmark3;

			this.SelectedTile = 0;

			this.LeftColor = settings.LeftColor;
			this.RightColor = settings.RightColor;
			this.MiddleColor = settings.MiddleMouseColor;
			this.X1Color = settings.X1MouseColor;
			this.X2Color = settings.X2MouseColor;

			this.TileEditor = TileEditorID.PixelEdit;

			this.UpdateSize();

			if (!String.IsNullOrEmpty(filePath)) {
				auMessenger.FileName = filePath;
				if (mmf != null) {
					mmf.Dispose(); //Remove old MMF.
				}
				mmf = new AUMemMappedFile(filePath, auMessenger, gbrFile);
			}
		}

		void tileSet_SizeChanged(object sender, EventArgs e) {
			var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			setDisplayedTileSize(tileData.Width, tileData.Height);
			this.UpdateSize();
		}

		void tileSet_CountChanged(object sender, EventArgs e) {
			//TODO
		}

		private void initClipboardChangeCheck() {
			NativeMethods.AddClipboardFormatListener(Handle);
			OnClipboardUpdate();
		}

		private void OnClipboardUpdate() {
			//TODO
		}

		protected override void WndProc(ref Message m) {
			if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE) {
				OnClipboardUpdate();
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// Called when anything that acts as an open button is clicked.
		/// </summary>
		private void openButton_OnClick(object sender, EventArgs e) {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "GBR files|*.gbr|All files|*.*";

			DialogResult result = dialog.ShowDialog();

			if (result != DialogResult.OK) {
				return;
			}

			LoadTileFile(dialog.FileName);
		}

		private void saveButton_OnClicked(object sender, EventArgs e) {

		}

		private void saveAsButton_OnClicked(object sender, EventArgs e) {

		}

		private void exportButton_OnClicked(object sender, EventArgs e) {

		}

		private void exportToButton_OnClicked(object sender, EventArgs e) {

		}

		private void importFromButton_OnClicked(object sender, EventArgs e) {

		}

		private void auMessenger_OnColorPaletteChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				//TODO
			}));
		}

		private void auMessenger_OnGBPaletteChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				//TODO
			}));
		}

		private void auMessenger_OnTileChanged(object sender, TileChangedEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				gbrFile.GetObjectOfType<GBRObjectTileData>().Tiles[args.TileID] = mmf.Tiles[args.TileID];
				
				gbrFile.GetObjectOfType<GBRObjectTilePalette>().GBCPalettes[args.TileID] = mmf.PalMaps[args.TileID].GBC;
				gbrFile.GetObjectOfType<GBRObjectTilePalette>().SGBPalettes[args.TileID] = mmf.PalMaps[args.TileID].SGB;

				//Alert it of the change (This is bad code, but I don't know how to fix yet)
				this.tileList.TileSet = gbrFile.GetObjectOfType<GBRObjectTileData>();
			}));
		}

		private void auMessenger_OnTileRefreshNeeded(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				var tileData = gbrFile.GetObjectOfType<GBRObjectTileData>();
				var defaultPalette = gbrFile.GetObjectOfType<GBRObjectTilePalette>();

				tileData.Tiles = mmf.Tiles.GetTilesArray();

				//Inefficiant, but it works.
				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					gbcPal[i] = mmf.PalMaps[i].GBC;
					sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				defaultPalette.GBCPalettes = gbcPal;
				defaultPalette.SGBPalettes = sgbPal;

				this.tileList.TileSet = tileData;
				this.tileList.PaletteMapping = defaultPalette;
			}));
		}

		private void auMessenger_OnTileSizeChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				var tileData = gbrFile.GetObjectOfType<GBRObjectTileData>();
				var defaultPalette = gbrFile.GetObjectOfType<GBRObjectTilePalette>();

				tileData.Width = (UInt16)mmf.TileWidth;
				tileData.Height = (UInt16)mmf.TileHeight;

				tileData.Tiles = mmf.Tiles.GetTilesArray();

				//Inefficiant, but it works.
				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					gbcPal[i] = mmf.PalMaps[i].GBC;
					sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				defaultPalette.GBCPalettes = gbcPal;
				defaultPalette.SGBPalettes = sgbPal;

				this.tileList.TileSet = tileData;
				this.tileList.PaletteMapping = defaultPalette;
			}));
		}

		private void auMessenger_OnTotalRefreshNeeded(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				var tileData = gbrFile.GetObjectOfType<GBRObjectTileData>();
				var defaultPalette = gbrFile.GetObjectOfType<GBRObjectTilePalette>();

				tileData.Width = (UInt16)mmf.TileWidth;
				tileData.Height = (UInt16)mmf.TileHeight;

				tileData.Tiles = mmf.Tiles.GetTilesArray();

				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					gbcPal[i] = mmf.PalMaps[i].GBC;
					sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				defaultPalette.GBCPalettes = gbcPal;
				defaultPalette.SGBPalettes = sgbPal;

				//Alert it of the change (This is bad code, but I don't know how to fix yet)
				this.tileList.TileSet = tileData;
				this.tileList.PaletteMapping = defaultPalette;
				//TODO load the palettedata from MMF
			}));
		}

		private void toolList_AutoUpdateChanged(object sender, EventArgs e) {
			this.AutoUpdate = toolList.AutoUpdate;
		}

		private void autoUpdateMenuItem_Click(object sender, EventArgs e) {
			this.AutoUpdate = !this.AutoUpdate;
		}

		private void simpleModeMenuItem_Click(object sender, EventArgs e) {
			this.SimpleMode = !this.SimpleMode;
		}

		private void gridMenuItem_Click(object sender, EventArgs e) {
			this.ShowGrid = !this.ShowGrid;
		}

		private void nibbleMarkersMenuItem_Click(object sender, EventArgs e) {
			this.ShowNibbleMarkers = !this.ShowNibbleMarkers;
		}

		private void setBookmarkMenuItem_Click(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;

			if (item != null) {
				switch (Convert.ToInt32(item.Tag)) {
				case 1: this.Bookmark1 = this.SelectedTile; break;
				case 2: this.Bookmark2 = this.SelectedTile; break;
				case 3: this.Bookmark3 = this.SelectedTile; break;
				}
			}
		}

		private void gotoBookmarkMenuItem_Click(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;

			if (item != null) {
				UInt16 bookmark = 0xFFFF;

				switch (Convert.ToInt32(item.Tag)) {
				case 1: bookmark = this.Bookmark1; break;
				case 2: bookmark = this.Bookmark2; break;
				case 3: bookmark = this.Bookmark3; break;
				}

				if (bookmark < gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles.Length) {
					this.SelectedTile = bookmark;
				}
			}
		}

		private void clearBookmarkMenuItem_Click(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;

			if (item != null) {
				switch (Convert.ToInt32(item.Tag)) {
				case 1: this.Bookmark1 = 0xFFFF; break;
				case 2: this.Bookmark2 = 0xFFFF; break;
				case 3: this.Bookmark3 = 0xFFFF; break;
				}
			}
		}

		private void setDisplayedTileSize(UInt16 Width, UInt16 Height) {
			if (Width == 8 && Height == 8) {
				size8x8MenuItem.Checked = true;
				size8x16MenuItem.Checked = false;
				size16x16MenuItem.Checked = false;
				size32x32MenuItem.Checked = false;
			} else if (Width == 8 && Height == 16) {
				size8x8MenuItem.Checked = false;
				size8x16MenuItem.Checked = true;
				size16x16MenuItem.Checked = false;
				size32x32MenuItem.Checked = false;
			} else if (Width == 16 && Height == 16) {
				size8x8MenuItem.Checked = false;
				size8x16MenuItem.Checked = false;
				size16x16MenuItem.Checked = true;
				size32x32MenuItem.Checked = false;
			} else if (Width == 32 && Height == 32) {
				size8x8MenuItem.Checked = false;
				size8x16MenuItem.Checked = false;
				size16x16MenuItem.Checked = false;
				size32x32MenuItem.Checked = true;
			} else {
				size8x8MenuItem.Checked = false;
				size8x16MenuItem.Checked = false;
				size16x16MenuItem.Checked = false;
				size32x32MenuItem.Checked = false;
			}
		}

		public void SetTileSize(UInt16 Width, UInt16 Height) {
			setDisplayedTileSize(Width, Height);
			this.UpdateSize();

			var tileSet = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			tileSet.ResizeTiles(Width, Height);
			if (mmf != null) {
				mmf.SetTileSize(Width, Height);
			}

			this.tileList.TileSet = tileSet;
		}

		protected void UpdateSize() {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();
			UInt16 Width = tileset.Width;
			UInt16 Height = tileset.Height;

			this.SuspendLayout();

			if (Width == 8 && Height == 8) {
				if (SimpleMode) {
					this.ClientSize = new Size(324, 264);
				} else {
					this.ClientSize = new Size(397, 264);
				}

				tileList.Height = 222;
				tileList.Top = 38;
			} else if (Width == 8 && Height == 16) {
				if (SimpleMode) {
					this.ClientSize = new Size(344, 456);
				} else {
					this.ClientSize = new Size(417, 456);
				}

				tileList.Height = 397;
				tileList.Top = 46;
			} else if (Width == 16 && Height == 16) {
				if (SimpleMode) {
					this.ClientSize = new Size(410, 296);
				} else {
					this.ClientSize = new Size(507, 296);
				}

				tileList.Height = 232;
				tileList.Top = 49;
			} else if (Width == 32 && Height == 32) {
				if (SimpleMode) {
					this.ClientSize = new Size(506, 328);
				} else {
					this.ClientSize = new Size(603, 328);
				}

				tileList.Height = 261;
				tileList.Top = 50;
			}

			tileList.Left = this.ClientSize.Width - tileList.Width;

			tileEditBorder.Width = this.ClientSize.Width - tileList.Width - 3;
			tileEditBorder.Height = this.ClientSize.Height - 34;

			this.ResumeLayout(true);
		}

		private void size8x8MenuItem_Click(object sender, EventArgs e) {
			SetTileSize(8, 8);
		}

		private void size8x16MenuItem_Click(object sender, EventArgs e) {
			SetTileSize(8, 16);
		}

		private void size16x16MenuItem_Click(object sender, EventArgs e) {
			SetTileSize(16, 16);
		}

		private void size32x32MenuItem_Click(object sender, EventArgs e) {
			SetTileSize(32, 32);
		}

		private void tileCountMenuItem_Click(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			TileCountDialog dialog = new TileCountDialog(tileset);

			var result = dialog.ShowDialog();

			if (result != DialogResult.OK) {
				return;
			}
			
			tileList.TileSet = tileset;
		}

		private void tileList_SelectedTileChanged(object sender, EventArgs e) {
			this.SelectedTile = tileList.SelectedTile;
		}

		private void toolList_SelectedToolChanged(object sender, EventArgs e) {
			this.TileEditor = toolList.SelectedTool;
		}
	}
}
