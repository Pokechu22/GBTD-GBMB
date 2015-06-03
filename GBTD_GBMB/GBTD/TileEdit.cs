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
using System.Runtime.InteropServices;

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
				MenuItem[] colorSetItems = new MenuItem[] {
					colorSetGameboyPocketMenuItem,
					colorSetGameboyMenuItem,
					colorSetGameboyColorMenuItem,
					colorSetFilteredGameboyColorMenuItem,
					colorSetSuperGameboyMenuItem
				};

				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().ColorSet = value;

				tileList.ColorSet = value;
				mainTileEdit.ColorSet = value;
				previewRenderer.ColorSet = value;
				colorSelector.ColorSet = value;

				foreach (MenuItem item in colorSetItems) {
					ColorSet itemSet = (ColorSet)item.Tag;
					item.Checked = (value == itemSet);
				}

				this.FileModified = true;
			}
		}

		public bool AutoUpdate {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().AutoUpdate; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().AutoUpdate = value;

				autoUpdateMenuItem.Checked = value;
				toolList.AutoUpdate = value;

				auMessenger.Enabled = value;

				this.FileModified = true;
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

				this.FileModified = true;
			}
		}

		public bool SimpleMode {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().SimpleMode; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().SimpleMode = value;
				var tileSet = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

				simpleModeMenuItem.Checked = value;
				previewRenderer.Simple = value;

				UpdateSize();

				this.FileModified = true;
			}
		}

		public UInt16 Bookmark1 {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark1; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark1 = value;
				tileList.Bookmark1 = value;

				gotoBookmark1MenuItem.Enabled = (value < gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles.Length);
				clearBookmark1MenuItem.Enabled = (value != 0xFFFF);

				this.FileModified = true;
			}
		}
		public UInt16 Bookmark2 {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark2; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark2 = value;
				tileList.Bookmark2 = value;

				gotoBookmark2MenuItem.Enabled = (value < gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles.Length);
				clearBookmark2MenuItem.Enabled = (value != 0xFFFF);

				this.FileModified = true;
			}
		}
		public UInt16 Bookmark3 {
			get { return gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark3; }
			set {
				gbrFile.GetOrCreateObjectOfType<GBRObjectTileSettings>().Bookmark3 = value;
				tileList.Bookmark3 = value;

				gotoBookmark3MenuItem.Enabled = (value < gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles.Length);
				clearBookmark3MenuItem.Enabled = (value != 0xFFFF);

				this.FileModified = true;
			}
		}

		public UInt16 SelectedTile {
			get { return tileList.SelectedTile; }
			set {
				tileList.SelectedTile = value;
				mainTileEdit.SelectedTile = value;
				previewRenderer.SelectedTile = value;
				colorSelector.SelectedTile = value;
			}
		}

		public GBColor LeftColor {
			get { return mainTileEdit.LeftColor; }
			set {
				mainTileEdit.LeftColor = value;
				colorSelector.LeftColor = value;

				this.FileModified = true;
			}
		}
		public GBColor RightColor {
			get { return mainTileEdit.RightColor; }
			set {
				mainTileEdit.RightColor = value;
				colorSelector.RightColor = value;

				this.FileModified = true;
			}
		}
		public GBColor MiddleColor {
			get { return mainTileEdit.MiddleColor; }
			set {
				mainTileEdit.MiddleColor = value;
				colorSelector.MiddleColor = value;

				this.FileModified = true;
			}
		}
		public GBColor X1Color {
			get { return mainTileEdit.X1Color; }
			set {
				mainTileEdit.X1Color = value;
				colorSelector.X1Color = value;

				this.FileModified = true;
			}
		}
		public GBColor X2Color {
			get { return mainTileEdit.X2Color; }
			set {
				mainTileEdit.X2Color = value;
				colorSelector.X2Color = value;

				this.FileModified = true;
			}
		}

		public TileEditorID TileEditor {
			get { return toolList.SelectedTool; }
			set {
				toolList.SelectedTool = value;

				switch (value) {
				case TileEditorID.NoEdit:
					mainTileEdit.TileEditor = new NoEditTileEditor();
					penMenuItem.Checked = false;
					floodFillMenuItem.Checked = false;
					break;
				case TileEditorID.PixelEdit:
					mainTileEdit.TileEditor = new PixelTileEditor();
					penMenuItem.Checked = true;
					floodFillMenuItem.Checked = false;
					break;
				case TileEditorID.FloodFill:
					mainTileEdit.TileEditor = new FloodFillTileEditor();
					penMenuItem.Checked = false;
					floodFillMenuItem.Checked = true;
					break;
				default:
					mainTileEdit.TileEditor = null;
					penMenuItem.Checked = false;
					floodFillMenuItem.Checked = false;
					break;
				}

				this.FileModified = true;
			}
		}

		/// <summary>
		/// Whether the currently loaded file has been modified.
		/// </summary>
		public bool FileModified { get; private set; }

		public TileEdit() {
			InitializeComponent();

			initClipboardChangeCheck();
		}

		protected override void OnLoad(EventArgs e) {
			//Loads the menu.
			//http://stackoverflow.com/a/28462365/3991344
			this.Menu = this.mainMenu;

			String[] environmentArgs = Environment.GetCommandLineArgs();

			foreach (string arg in environmentArgs) {
				if (arg.StartsWith("-")) {
					MessageBox.Show("This switch is not supported by this version.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

					Environment.Exit(1);
					return;
				}
			}

			if (environmentArgs.Length >= 2) {
				LoadTileFile(environmentArgs[1]);
			} else {
				LoadTileFile(new GBRFile());
			}

			base.OnLoad(e);
		}

		protected override void OnClosing(CancelEventArgs e) {
			if (this.FileModified) {
				var result = MessageBox.Show("The current tiles have been changed.  Save changes first ?", "Confirm",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (result == DialogResult.Cancel) {
					e.Cancel = true;
				} else if (result == DialogResult.Yes) {
					saveButton_OnClicked(this, e);
					e.Cancel = false;
				} else if (result == DialogResult.No) {
					e.Cancel = false;
				}
			}

			base.OnClosing(e);
		}

		private void LoadTileFile(String path) {
			this.filePath = path;

			using (Stream stream = File.OpenRead(path)) {
				LoadTileFile(new GBRFile(stream));
				RecentFileUtils.AddToRecentlyUsedFilesList(path, Properties.Settings.Default);
				Properties.Settings.Default.GBRPath = Path.GetDirectoryName(filePath);
				this.Text = "Gameboy Tile Designer - " + Path.GetFileName(path);
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
			tileSet.ColorMappingChanged += new EventHandler(tileSet_ColorMappingChanged);

			setDisplayedTileSize(tileSet.Width, tileSet.Height);

			tileList.TileSet = tileSet;
			tileList.PaletteData = paletteData;
			tileList.PaletteMapping = paletteMapping;

			mainTileEdit.TileSet = tileSet;
			mainTileEdit.Palettes = palettes;
			mainTileEdit.PaletteMapping = paletteMapping;

			previewRenderer.TileSet = tileSet;
			previewRenderer.Palettes = palettes;
			previewRenderer.PaletteMapping = paletteMapping;

			colorSelector.Palettes = palettes;
			colorSelector.PaletteMapping = paletteMapping;
			colorSelector.TileSet = tileSet;

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

			this.FileModified = false;
		}

		void tileSet_ColorMappingChanged(object sender, EventArgs e) {
			var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			mmf.GBPalettes.SetPalettes(tileData.Color0Mapping, tileData.Color1Mapping, tileData.Color2Mapping, tileData.Color3Mapping);

			this.FileModified = true;
		}

		void tileSet_SizeChanged(object sender, EventArgs e) {
			var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			setDisplayedTileSize(tileData.Width, tileData.Height);
			this.UpdateSize();

			this.FileModified = true;
		}

		void tileSet_CountChanged(object sender, EventArgs e) {
			//TODO

			this.FileModified = true;
		}

		// See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AddClipboardFormatListener(IntPtr hwnd);

		private void initClipboardChangeCheck() {
			AddClipboardFormatListener(Handle);
			OnClipboardUpdate();
		}

		private void OnClipboardUpdate() {
			pasteMenuItem.Enabled = Clipboard.ContainsImage();
			pasteButton.Enabled = Clipboard.ContainsImage();
		}

		protected override void WndProc(ref Message m) {
			const int WM_CLIPBOARDUPDATE = 0x031D;

			if (m.Msg == WM_CLIPBOARDUPDATE) {
				OnClipboardUpdate();
			}

			base.WndProc(ref m);
		}

		/// <summary>
		/// Called when anything that acts as an open button is clicked.
		/// </summary>
		private void openButton_OnClick(object sender, EventArgs e) {
			if (this.FileModified) {
				var saveResult = MessageBox.Show("The current tiles have been changed.  Save changes first ?", "Confirm",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				if (saveResult == DialogResult.Cancel) {
					return;
				} else if (saveResult == DialogResult.Yes) {
					saveButton_OnClicked(this, e);
				}
				//A result of No just continues exectuing to below.
			}

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "GBR files|*.gbr|All files|*.*";

			DialogResult result = dialog.ShowDialog();

			if (result != DialogResult.OK) {
				return;
			}

			LoadTileFile(dialog.FileName);
		}

		private void saveButton_OnClicked(object sender, EventArgs e) {
			if (filePath == null) {
				//If we didn't initially load the file, we save as instead.
				saveAsButton_OnClicked(sender, e);
			}

			gbrFile.GetOrCreateObjectOfType<GBRObjectProducerInfo>().UpdateWithCurrentApp();

			using (var stream = File.OpenWrite(filePath)) {
				gbrFile.SaveToStream(stream);
			}
		}

		private void saveAsButton_OnClicked(object sender, EventArgs e) {
			gbrFile.GetOrCreateObjectOfType<GBRObjectProducerInfo>().UpdateWithCurrentApp();

			DialogResult result;
			SaveFileDialog d = new SaveFileDialog();
			d.Filter = "GBR files|*.gbr|All files|*.*";

			d.InitialDirectory = Properties.Settings.Default.GBRPath;

			result = d.ShowDialog();
			if (result != DialogResult.OK) { return; }

			filePath = d.FileName;

			using (var stream = d.OpenFile()) {
				gbrFile.SaveToStream(stream);
			}

			Properties.Settings.Default.GBRPath = Path.GetDirectoryName(filePath);
			this.Text = "Gameboy Tile Designer - " + Path.GetFileName(filePath);
			Properties.Settings.Default.GBRPath = Path.GetDirectoryName(filePath);
		}

		private void exportButton_OnClicked(object sender, EventArgs e) {

		}

		private void exportToButton_OnClicked(object sender, EventArgs e) {
			ExportDialog dialog = new ExportDialog();

			var result = dialog.ShowDialog();

			if (result == DialogResult.OK) {
				//TODO: Actually export.
			}
		}

		private void importFromButton_OnClicked(object sender, EventArgs e) {
			ImportDialog dialog = new ImportDialog();

			var result = dialog.ShowDialog();

			if (result == DialogResult.OK) {
				//TODO: Actually import.
			}
		}

		private void auMessenger_OnColorPaletteChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				//TODO

				this.FileModified = true;
			}));
		}

		private void auMessenger_OnGBPaletteChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

				tileData.SetColorMapping(mmf.GBPalettes.GBColor0, mmf.GBPalettes.GBColor1, mmf.GBPalettes.GBColor2, mmf.GBPalettes.GBColor3);

				this.FileModified = true;
			}));
		}

		private void auMessenger_OnTileChanged(object sender, TileChangedEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				gbrFile.GetObjectOfType<GBRObjectTileData>().Tiles[args.TileID] = mmf.Tiles[args.TileID];
				
				gbrFile.GetObjectOfType<GBRObjectTilePalette>().GBCPalettes[args.TileID] = mmf.PalMaps[args.TileID].GBC;
				gbrFile.GetObjectOfType<GBRObjectTilePalette>().SGBPalettes[args.TileID] = mmf.PalMaps[args.TileID].SGB;

				this.tileList.Invalidate(true);
				this.previewRenderer.Invalidate(true);
				this.mainTileEdit.Invalidate(true);
				this.colorSelector.Invalidate(true);

				this.FileModified = true;
			}));
		}

		private void auMessenger_OnTileRefreshNeeded(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				var tileData = gbrFile.GetObjectOfType<GBRObjectTileData>();
				var paletteMapping = gbrFile.GetObjectOfType<GBRObjectTilePalette>();

				this.SelectedTile = 0;

				tileData.Tiles = mmf.Tiles.GetTilesArray();

				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					gbcPal[i] = mmf.PalMaps[i].GBC;
					sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				paletteMapping.GBCPalettes = gbcPal;
				paletteMapping.SGBPalettes = sgbPal;

				this.tileList.Invalidate(true);
				this.previewRenderer.Invalidate(true);
				this.mainTileEdit.Invalidate(true);
				this.colorSelector.Invalidate(true);

				this.FileModified = true;
			}));
		}

		private void auMessenger_OnTileSizeChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				var tileData = gbrFile.GetObjectOfType<GBRObjectTileData>();
				var paletteMapping = gbrFile.GetObjectOfType<GBRObjectTilePalette>();

				this.SelectedTile = 0;

				tileData.Width = (UInt16)mmf.TileWidth;
				tileData.Height = (UInt16)mmf.TileHeight;

				tileData.Tiles = mmf.Tiles.GetTilesArray();

				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					gbcPal[i] = mmf.PalMaps[i].GBC;
					sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				paletteMapping.GBCPalettes = gbcPal;
				paletteMapping.SGBPalettes = sgbPal;

				this.tileList.Invalidate(true);
				this.previewRenderer.Invalidate(true);
				this.mainTileEdit.Invalidate(true);
				this.colorSelector.Invalidate(true);

				this.setDisplayedTileSize(tileData.Width, tileData.Height);
				this.UpdateSize();

				this.FileModified = true;
			}));
		}

		private void auMessenger_OnTotalRefreshNeeded(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				var tileData = gbrFile.GetObjectOfType<GBRObjectTileData>();
				var paletteMapping = gbrFile.GetObjectOfType<GBRObjectTilePalette>();

				this.SelectedTile = 0;

				tileData.Width = (UInt16)mmf.TileWidth;
				tileData.Height = (UInt16)mmf.TileHeight;

				tileData.Tiles = mmf.Tiles.GetTilesArray();

				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					gbcPal[i] = mmf.PalMaps[i].GBC;
					sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				paletteMapping.GBCPalettes = gbcPal;
				paletteMapping.SGBPalettes = sgbPal;

				this.tileList.Invalidate(true);
				this.previewRenderer.Invalidate(true);
				this.mainTileEdit.Invalidate(true);
				this.colorSelector.Invalidate(true);

				tileData.SetColorMapping(mmf.GBPalettes.GBColor0, mmf.GBPalettes.GBColor1, mmf.GBPalettes.GBColor2, mmf.GBPalettes.GBColor3);

				this.setDisplayedTileSize(tileData.Width, tileData.Height);
				this.UpdateSize();

				//TODO load the palettedata from MMF

				this.FileModified = true;
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

			toolList.RotateEnabled = (tileset.Width == tileset.Height);

			if (Width == 8 && Height == 8) {
				if (SimpleMode) {
					this.ClientSize = new Size(324, 264);

					previewRenderer.Size = new Size(26, 193);
					previewRenderer.SmallLocation = new Point(0, 83);
					previewRenderer.LargeLocation = new Point(0, 0);
					previewRenderer.LargeCount = 0;
				} else {
					this.ClientSize = new Size(397, 264);

					previewRenderer.Size = new Size(98, 193);
					previewRenderer.SmallLocation = new Point(36, 35);
					previewRenderer.LargeLocation = new Point(0, 95);
					previewRenderer.LargeCount = 4;
				}

				mainTileEdit.Size = new Size(193, 193);

				colorSelector.Location = new Point(34, 234);

				tileList.Height = 222;
				tileList.Top = 38;
			} else if (Width == 8 && Height == 16) {
				if (SimpleMode) {
					this.ClientSize = new Size(344, 456);

					previewRenderer.Size = new Size(26, 385);
					previewRenderer.SmallLocation = new Point(0, 167);
					previewRenderer.LargeLocation = new Point(0, 0);
					previewRenderer.LargeCount = 0;
				} else {
					this.ClientSize = new Size(417, 456);

					previewRenderer.Size = new Size(98, 385);
					previewRenderer.SmallLocation = new Point(36, 72);
					previewRenderer.LargeLocation = new Point(0, 191);
					previewRenderer.LargeCount = 4;
				}

				mainTileEdit.Size = new Size(193, 385);

				colorSelector.Location = new Point(34, 426);

				tileList.Height = 397;
				tileList.Top = 46;
			} else if (Width == 16 && Height == 16) {
				if (SimpleMode) {
					this.ClientSize = new Size(410, 296);

					previewRenderer.Size = new Size(50, 225);
					previewRenderer.SmallLocation = new Point(0, 79);
					previewRenderer.LargeLocation = new Point(0, 0);
					previewRenderer.LargeCount = 0;
				} else {
					this.ClientSize = new Size(507, 296);

					previewRenderer.Size = new Size(146, 225);
					previewRenderer.SmallLocation = new Point(48, 12);
					previewRenderer.LargeLocation = new Point(0, 79);
					previewRenderer.LargeCount = 3;
				}

				mainTileEdit.Size = new Size(225, 225);

				colorSelector.Location = new Point(50, 266);

				tileList.Height = 232;
				tileList.Top = 49;
			} else if (Width == 32 && Height == 32) {
				if (SimpleMode) {
					this.ClientSize = new Size(506, 328);

					previewRenderer.Size = new Size(98, 294);
					previewRenderer.SmallLocation = new Point(0, 81);
					previewRenderer.LargeLocation = new Point(0, 0);
					previewRenderer.LargeCount = 0;
				} else {
					this.ClientSize = new Size(603, 336);

					previewRenderer.Size = new Size(194, 294);
					previewRenderer.SmallLocation = new Point(48, 0);
					previewRenderer.LargeLocation = new Point(0, 100);
					previewRenderer.LargeCount = 2;
				}

				mainTileEdit.Size = new Size(257, 257);

				colorSelector.Location = new Point(66, 298);

				tileList.Height = 261;
				tileList.Top = 50;
			}

			tileList.Left = this.ClientSize.Width - tileList.Width;

			tileEditBorder.Width = this.ClientSize.Width - tileList.Width - 3;
			tileEditBorder.Height = this.ClientSize.Height - 34;

			previewRenderer.Left = tileEditBorder.Right - previewRenderer.Width - 5;

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

			if (mmf != null) {
				mmf.TileCount = tileset.Count;
			}
		}

		private void tileList_SelectedTileChanged(object sender, EventArgs e) {
			this.SelectedTile = tileList.SelectedTile;
		}

		private void toolList_SelectedToolChanged(object sender, EventArgs e) {
			this.TileEditor = toolList.SelectedTool;
		}

		private void mainTileEdit_TileChanged(object sender, EventArgs e) {
			var tile = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>().Tiles[SelectedTile];

			//TODO: Event that automatically updates this in the gbrfile.  
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void colorSelector_PaletteChanged(object sender, EventArgs e) {
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);
			mainTileEdit.Invalidate(true);

			if (mmf != null) {
				var palMaps = gbrFile.GetOrCreateObjectOfType<GBRObjectTilePalette>();
				switch (ColorSet) {
				case ColorSet.GAMEBOY_COLOR:
				case ColorSet.GAMEBOY_COLOR_FILTERED:
					mmf.PalMaps.GBC[SelectedTile] = (byte)palMaps.GBCPalettes[SelectedTile];
					break;
				case ColorSet.SUPER_GAMEBOY:
					mmf.PalMaps.SGB[SelectedTile] = (byte)palMaps.SGBPalettes[SelectedTile];
					break;
				}
			}

			this.FileModified = true;
		}

		private void colorSelector_MouseColorChanged(object sender, EventArgs e) {
			this.LeftColor = colorSelector.LeftColor;
			this.RightColor = colorSelector.RightColor;
			this.MiddleColor = colorSelector.MiddleColor;
			this.X1Color = colorSelector.X1Color;
			this.X2Color = colorSelector.X2Color;

			this.FileModified = true;
		}

		private void colorSetMenuItem_Clicked(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;

			if (item != null) {
				this.ColorSet = (ColorSet)item.Tag;
			}

			this.FileModified = true;
		}

		private void toolList_ScrollUpClicked(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			var tile = TileTransform.ScrolledUp(tileset.Tiles[SelectedTile]);
			tileset.Tiles[SelectedTile] = tile;

			//TODO: Event that automatically updates this in the gbrfile.  
			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void toolList_ScrollLeftClicked(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			var tile = TileTransform.ScrolledLeft(tileset.Tiles[SelectedTile]);
			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void toolList_ScrollRightClicked(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			var tile = TileTransform.ScrolledRight(tileset.Tiles[SelectedTile]);
			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void toolList_ScrollDownClicked(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			var tile = TileTransform.ScrolledDown(tileset.Tiles[SelectedTile]);
			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void toolList_FlipVerticallyClicked(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			var tile = TileTransform.FlippedVertically(tileset.Tiles[SelectedTile]);
			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void toolList_FlipHorizontallyClicked(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			var tile = TileTransform.FlippedHoriziontally(tileset.Tiles[SelectedTile]);
			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void toolList_RotateClockwiseClicked(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			var tile = TileTransform.RotateClockwise(tileset.Tiles[SelectedTile]);
			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void cutButton_Click(object sender, EventArgs e) {
			var tileset = gbrFile.GetObjectOfType<GBRObjectTileData>();
			Tile tile = tileset.Tiles[SelectedTile];

			Clipboard.SetImage(tile.ToImage());

			//Clear the old tile.
			tileset.Tiles[SelectedTile] = new Tile(tile.Width, tile.Height);

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void copyButton_Click(object sender, EventArgs e) {
			var tileset = gbrFile.GetObjectOfType<GBRObjectTileData>();
			Tile tile = tileset.Tiles[SelectedTile];

			Clipboard.SetImage(tile.ToImage());
		}

		private void pasteButton_Click(object sender, EventArgs e) {
			if (!Clipboard.ContainsImage()) {
				throw new WarningException("Clipboard must contain an image to paste a tile!");
			}

			Color white = Color.FromArgb(255, 255, 255);
			Color lightGray = Color.FromArgb(192, 192, 192);
			Color darkGray = Color.FromArgb(128, 128, 128);
			Color black = Color.FromArgb(0, 0, 0);

			var tileset = gbrFile.GetObjectOfType<GBRObjectTileData>();

			Tile tile = tileset.Tiles[SelectedTile];

			using (Bitmap bitmap = new Bitmap(Clipboard.GetImage())) {
				for (int y = 0; y < bitmap.Height && y < tile.Height; y++) {
					for (int x = 0; x < bitmap.Width && x < tile.Width; x++) {
						Color pixel = bitmap.GetPixel(x, y);
						if (pixel == white) {
							tile[x, y] = GBColor.WHITE;
						} else if (pixel == lightGray) {
							tile[x, y] = GBColor.LIGHT_GRAY;
						} else if (pixel == darkGray) {
							tile[x, y] = GBColor.DARK_GRAY;
						} else if (pixel == black) {
							tile[x, y] = GBColor.BLACK;
						} else {
							//By default, use white.
							tile[x, y] = GBColor.WHITE;
						}
					}
				}
			}

			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void clearTilesMenuItem_Click(object sender, EventArgs e) {
			var tileset = gbrFile.GetObjectOfType<GBRObjectTileData>();

			for (int i = 0; i < tileset.Count; i++) {
				tileset.Tiles[i] = new Tile(tileset.Width, tileset.Height);
			}

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles.SetTilesArray(tileset.Tiles);
			}

			this.FileModified = true;
		}

		private void flipColorsMenuItem_Click(object sender, EventArgs e) {
			var tileset = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			Tile oldTile = tileset.Tiles[SelectedTile];
			Tile tile = new Tile(oldTile.Width, oldTile.Height);
			for (int y = 0; y < tile.Height; y++) {
				for (int x = 0; x < tile.Width; x++) {
					GBColor oldColor = oldTile[x, y];
					if (oldColor == LeftColor) {
						tile[x, y] = RightColor;
					} else if (oldColor == RightColor) {
						tile[x, y] = LeftColor;
					} else {
						tile[x, y] = oldColor;
					}
				}
			}

			tileset.Tiles[SelectedTile] = tile;

			mainTileEdit.Invalidate(true);
			tileList.Invalidate(true);
			previewRenderer.Invalidate(true);

			if (mmf != null) {
				mmf.Tiles[SelectedTile] = tile;
			}

			this.FileModified = true;
		}

		private void penMenuItem_Click(object sender, EventArgs e) {
			this.TileEditor = TileEditorID.PixelEdit;
		}

		private void floodFillMenuItem_Click(object sender, EventArgs e) {
			this.TileEditor = TileEditorID.FloodFill;
		}

		private void fileMenuItem_Popup(object sender, EventArgs e) {
			// Handle recently used list.
			if (RecentFileUtils.IsRecentlyUsedFilesListEmpty(Properties.Settings.Default) ){
				reopenMenuItem.Visible = false;
				reopenSeperatorMenuItem.Visible = false;
			} else {
				reopenMenuItem.Visible = true;
				reopenSeperatorMenuItem.Visible = true;

				RecentFileUtils.AddRecentlyUsedFilesListItems(Properties.Settings.Default, reopenMenuItem, 
					new EventHandler(reopenMenuItem_Click));
			}
		}

		private void reopenMenuItem_Click(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;
			if (item != null) {
				LoadTileFile(item.Text);
			}
		}

		private void aboutMenuItem_Click(object sender, EventArgs e) {
			new GBTDAboutBox().ShowDialog();
		}

		private void mainTileEdit_HasUndoChanged(object sender, EventArgs e) {
			undoMenuItem.Enabled = mainTileEdit.HasUndo;
		}

		private void undoMenuItem_Click(object sender, EventArgs e) {
			mainTileEdit.Undo();
		}
	}
}
