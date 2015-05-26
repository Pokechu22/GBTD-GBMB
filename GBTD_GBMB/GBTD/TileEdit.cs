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
			}
		}

		public bool ShowGrid {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ShowGrid; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ShowGrid = value;

				gridMenuItem.Checked = value;
			}
		}

		public bool SimpleMode {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().SimpleMode; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().SimpleMode = value;

				simpleModeMenuItem.Checked = value;
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
			set { tileList.SelectedTile = value; }
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

			var tileData = file.GetOrCreateObjectOfType<GBRObjectTileData>();
			var palettes = file.GetOrCreateObjectOfType<GBRObjectPalettes>();
			var paletteMapping = file.GetOrCreateObjectOfType<GBRObjectTilePalette>();
			var settings = file.GetOrCreateObjectOfType<GBRObjectTileSettings>();

			PaletteData paletteData = new PaletteData(palettes.SGBPalettes, palettes.GBCPalettes);

			tileList.TileSet = file.GetOrCreateObjectOfType<GBRObjectTileData>();
			tileList.PaletteData = paletteData;
			tileList.PaletteMapping = paletteMapping;

			this.AutoUpdate = settings.AutoUpdate;
			this.ColorSet = settings.ColorSet;
			this.ShowNibbleMarkers = settings.ShowNibbleMarkers;
			this.ShowGrid = settings.ShowGrid;
			this.SimpleMode = settings.SimpleMode;
			this.Bookmark1 = settings.Bookmark1;
			this.Bookmark2 = settings.Bookmark2;
			this.Bookmark3 = settings.Bookmark3;

			if (!String.IsNullOrEmpty(filePath)) {
				auMessenger.FileName = filePath;
				if (mmf != null) {
					mmf.Dispose(); //Remove old MMF.
				}
				mmf = new AUMemMappedFile(filePath, auMessenger, gbrFile);
			}
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
	}
}
