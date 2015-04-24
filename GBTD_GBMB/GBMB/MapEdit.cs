using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBRFile;
using GB.Shared.GBMFile;
using System.IO;
using GB.Shared.AutoUpdate;
using GB.Shared.Palettes;
using System.Runtime.InteropServices;
using GB.Shared.Controls;
using GB.GBMB.Dialogs;

namespace GB.GBMB
{
	public partial class MapEdit : Form
	{
		private GBMFile gbmFile;
		private GBRFile gbrFile;
		private AUMemMappedFile mmf;
		private string mapFileName;
		private string tileFileName;

		[Description("Whether or not the info panel is shown.")]
		public bool ShowInfoPanel {
			get { return infoPanelBorder.Visible; }
			set {
				infoPanelMenuItem.Checked = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().ShowInfoPanel = value;

				this.SuspendLayout();
				infoPanelBorder.Visible = value;
				infoPanelBorder.Enabled = value;
				infoPanelHorizontalFlipCheckBox.Visible = value;
				infoPanelHorizontalFlipCheckBox.Enabled = value;
				infoPanelLocationLabel.Visible = value;
				infoPanelLocationLabel.Enabled = value;
				infoPanelPaletteComboBox.Visible = value;
				infoPanelPaletteComboBox.Enabled = value;
				infoPanelPalLabel.Visible = value;
				infoPanelPalLabel.Enabled = value;
				infoPanelVerticalFlipCheckBox.Visible = value;
				infoPanelVerticalFlipCheckBox.Enabled = value;

				this.OnResize(new EventArgs());

				this.ResumeLayout(true);
			}
		}
		[Description("Whether or not a grid is displayed.")]
		public bool ShowGrid {
			get { return mapControl.ShowGrid; }
			set {
				gridMenuItem.Checked = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().ShowGrid = value;
				mapControl.ShowGrid = value;
			}
		}
		[Description("Whether or not double markers are displayed.")]
		public bool ShowDoubleMarkers {
			get { return mapControl.ShowDoubleMarkers; }
			set {
				doubleMarkersMenuItem.Checked = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().ShowDoubleMarkers = value;
				mapControl.ShowDoubleMarkers = value;
			}
		}
		[Description("Whether or not properties are colorized.")]
		public bool ShowPropertyColors {
			get { return mapControl.ShowPropertyColors; }
			set {
				propertyColorsMenuItem.Checked = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().ShowPropColors = value;
				mapControl.ShowPropertyColors = value;
			}
		}
		[Description("Whether or not AutoUpdate is enabled.")]
		public bool AutoUpdate {
			get { return toolList.AutoUpdate; }
			set {
				autoUpdateMenuItem.Checked = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().AutoUpdate = value;
				toolList.AutoUpdate = value;
			}
		}

		[Description("The map's zoom level.")]
		public ZoomLevel ZoomLevel {
			get { return mapControl.ZoomLevel; }
			set {
				MenuItem[] ZoomControls = new MenuItem[] {
					zoom25PercentMenuItem,
					zoom50PercentMenuItem,
					zoom100PercentMenuItem,
					zoom150PercentMenuItem,
					zoom200PercentMenuItem
				};

				foreach (MenuItem item in ZoomControls) {
					if (item.Tag is ZoomLevel) {
						item.Checked = (((ZoomLevel)item.Tag) == value);
					} else {
						//TODO: This is an error state.
						item.Checked = false;
					}
				}

				switch (value) {
				case ZoomLevel._25: zoomComboBox.SelectedIndex = 0; break;
				case ZoomLevel._50: zoomComboBox.SelectedIndex = 1; break;
				case ZoomLevel._100: zoomComboBox.SelectedIndex = 2; break;
				case ZoomLevel._150: zoomComboBox.SelectedIndex = 3; break;
				case ZoomLevel._200: zoomComboBox.SelectedIndex = 4; break;
				default: zoomComboBox.SelectedIndex = -1; break; //Error state, again.
				}

				mapControl.ZoomLevel = value;
			}
		}

		[Description("The color set to use.")]
		public ColorSet ColorSet {
			get { return mapControl.ColorSet; }
			set {
				MenuItem[] ColorSetControls = new MenuItem[] {
					colorSetGameboyPocketMenuItem,
					colorSetGameboyMenuItem,
					colorSetGameboyColorMenuItem,
					colorSetSuperGameboyMenuItem,
					colorSetFilteredGameboyColorMenuItem
				};

				foreach (MenuItem item in ColorSetControls) {
					if (item.Tag is ColorSet) {
						item.Checked = (((ColorSet)item.Tag) == value);
					} else {
						//TODO: This is an error state.
						item.Checked = false;
					}
				}

				mapControl.ColorSet = value;
				tileList.ColorSet = value;

				infoPanelVerticalFlipCheckBox.Visible = infoPanelVerticalFlipCheckBox.Enabled = value.SupportsTileFlipping();
				infoPanelHorizontalFlipCheckBox.Visible = infoPanelHorizontalFlipCheckBox.Enabled = value.SupportsTileFlipping();

				infoPanelPaletteComboBox.Visible = infoPanelPaletteComboBox.Enabled = value.SupportsPaletteCustomization();
				infoPanelPalLabel.Visible = infoPanelPalLabel.Enabled = value.SupportsPaletteCustomization();

				infoPanelPaletteComboBox.Items.Clear();
				infoPanelPaletteComboBox.MaxDropDownItems = value.GetNumberOfRows() + 1;
				infoPanelPaletteComboBox.Items.Add("Defualt");
				for (int i = 0; i < value.GetNumberOfRows(); i++) {
					infoPanelPaletteComboBox.Items.Add(i.ToString());
				}

				if (value.SupportsPaletteCustomization()) {
					var palTemp = mapControl.SelectionPalette;
					if (palTemp.HasValue) {
						if (palTemp >= 0) {
							infoPanelPaletteComboBox.SelectedIndex = palTemp.Value + 1;
						} else {
							infoPanelPaletteComboBox.SelectedIndex = -1;
						}
					} else {
						infoPanelPaletteComboBox.SelectedIndex = 0;
					}
				}

				infoPanelPaletteComboBox.Location =
					new Point(infoPanelBorder.Right - (value.SupportsTileFlipping() ? 246 : 96), infoPanelBorder.Top + 1);
				infoPanelPalLabel.Location = 
					new Point(infoPanelBorder.Right - (value.SupportsTileFlipping() ? 270 : 120), infoPanelBorder.Top + 3);
			}
		}

		private Tool tool = Tool.PEN;
		/// <summary>
		/// The selected tool.  TODO: Put this in the map or do something with it.
		/// </summary>
		[Description("The currently selected tool.")]
		public Tool SelectedTool {
			get { return tool; }
			set {
				MenuItem[] ToolMenuItems = new MenuItem[] {
					penMenuItem,
					floodFillMenuItem,
					dropperMenuItem
				};

				foreach (MenuItem item in ToolMenuItems) {
					if (item.Tag is Tool) {
						item.Checked = (((Tool)item.Tag) == value);
					} else {
						item.Checked = false;
					}
				}

				tool = value;
			}
		}

		[DefaultValue(0)]
		[Description("The currently selected tile, which will be used on right click.")]
		public UInt16 SelectedTile {
			get { return tileList.SelectedTile; }
			set { tileList.SelectedTile = value; }
		}

		public UInt16 Bookmark1 {
			get { return tileList.Bookmark1; }
			set {
				tileList.Bookmark1 = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().Bookmark1 = value;

				gotoBookmark1MenuItem.Enabled = (value != 0xFFFF);
			}
		}
		public UInt16 Bookmark2 {
			get { return tileList.Bookmark2; }
			set {
				tileList.Bookmark2 = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().Bookmark2 = value;

				gotoBookmark2MenuItem.Enabled = (value != 0xFFFF);
			}
		}
		public UInt16 Bookmark3 {
			get { return tileList.Bookmark3; }
			set {
				tileList.Bookmark3 = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().Bookmark3 = value;

				gotoBookmark3MenuItem.Enabled = (value != 0xFFFF);
			}
		}

		public MapEdit() {
			InitializeComponent();

			this.OnResize(new EventArgs());

			zoomComboBox.SelectedIndex = 4;
		}

		private void onOpenButtonClicked(object sender, EventArgs e) {
			OpenFileDialog d = new OpenFileDialog();
			d.Filter = "GBM files|*.gbm|All files|*.*";

			d.InitialDirectory = Properties.Settings.Default.GBMPath;

			var result = d.ShowDialog();
			if (result != DialogResult.OK) {
				return;
			}

			LoadMapFile(d.FileName);

			mapControl.Enabled = true;

			Properties.Settings.Default.GBMPath = Path.GetDirectoryName(d.FileName);
		}

		private void onSaveButtonClicked(object sender, EventArgs e) {
			var result = MessageBox.Show("WARNING!  Saving is buggy.  Continue?  You may lose extra data from newer versions of the app.",
				"Saving is dangerous", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

			if (result != DialogResult.Yes) { return; }

			UpdateProducerInfo();

			using (var stream = File.Open(mapFileName, FileMode.Create, FileAccess.ReadWrite)) {
				gbmFile.SaveToStream(stream);
			}

			MessageBox.Show("File saved successfully.", "File saved successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void saveAsMenuItem_Click(object sender, EventArgs e) {
			var result = MessageBox.Show("WARNING!  Saving is buggy.  Continue?  You may lose extra data from newer versions of the app.",
				"Saving is dangerous", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

			if (result != DialogResult.Yes) { return; }

			UpdateProducerInfo();

			SaveFileDialog d = new SaveFileDialog();
			d.Filter = "GBM files|*.gbm|All files|*.*";

			d.InitialDirectory = Properties.Settings.Default.GBMPath;

			result = d.ShowDialog();
			if (result != DialogResult.OK) { return; }

			using (var stream = d.OpenFile()) {
				gbmFile.SaveToStream(stream);
			}

			MessageBox.Show("File saved successfully.", "File saved successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

			Properties.Settings.Default.GBMPath = Path.GetDirectoryName(d.FileName);
		}

		/// <summary>
		/// Unloads the current <see cref="gbmFile"/> if present.
		/// </summary>
		public void UnloadCurrentFile() {
			if (gbmFile != null) {
				var map = gbmFile.GetObjectOfType<GBMObjectMap>();
				if (map != null) {
					map.PropCountChanged -= new EventHandler(map_PropCountChanged);
					map.TileFileChanged -= new EventHandler(map_TileFileChanged);
				}
			}
		}

		/// <summary>
		/// Loads a GBMFile from the specified path.
		/// </summary>
		/// <param name="mapPath"></param>
		public void LoadMapFile(String mapPath) {
			UnloadCurrentFile();

			mapFileName = mapPath;

			Environment.CurrentDirectory = Path.GetDirectoryName(mapPath);
			using (var stream = File.OpenRead(mapPath)) {
				gbmFile = new GBMFile(stream);
			}
			
			GBMObjectMap map = gbmFile.GetObjectOfType<GBMObjectMap>();

			tileFileName = Path.GetFullPath(map.TileFile);

			LoadTileFile(tileFileName);

			map.PropCountChanged += new EventHandler(map_PropCountChanged);
			map.TileFileChanged += new EventHandler(map_TileFileChanged);
			this.infoPanelPropBorder1.Visible = this.infoPanelPropBorder1.Enabled = (map.PropCount != 0);
			this.infoPanelPropBorder2.Visible = this.infoPanelPropBorder2.Enabled = (map.PropCount != 0);

			this.mapControl.Map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			
			this.mapControl.Properties = gbmFile.GetObjectOfType<GBMObjectMapPropertyData>();
			this.mapControl.PropertyColors = gbmFile.GetObjectOfType<GBMObjectMapPropertyColors>();

			var settings = gbmFile.GetObjectOfType<GBMObjectMapSettings>();
			this.AutoUpdate = settings.AutoUpdate;
			this.ShowGrid = settings.ShowGrid;
			this.ShowInfoPanel = settings.ShowInfoPanel;
			this.ShowPropertyColors = settings.ShowPropColors;
			this.ShowDoubleMarkers = settings.ShowDoubleMarkers;
			
			this.ColorSet = (ColorSet)settings.ColorSet;

			this.Bookmark1 = settings.Bookmark1;
			this.Bookmark2 = settings.Bookmark2;
			this.Bookmark3 = settings.Bookmark3;

			this.Size = new Size((int)settings.FormWidth, (int)settings.FormHeight);
			this.WindowState = (settings.FormMaximized ? FormWindowState.Maximized : FormWindowState.Normal);

			this.map_PropCountChanged(map, new EventArgs());

			AddToReopenList(mapPath);
		}

		public void LoadTileFile(string tilePath) {
			using (var stream = File.OpenRead(tilePath)) {
				gbrFile = new GBRFile(stream);
			}

			this.mapControl.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
			this.tileList.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();

			var pals = gbrFile.GetObjectsOfType<GBRObjectPalettes>().First();
			this.mapControl.DefaultPalette = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
			this.tileList.PaletteMapping = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
			PaletteData paletteData = new Shared.Palettes.PaletteData(pals.SGBPalettes, pals.GBCPalettes);
			this.tileList.PaletteData = paletteData;
			this.mapControl.PaletteData = paletteData;

			auMessenger.FileName = tilePath;
			if (mmf != null) {
				mmf.Dispose(); //Remove old MMF.
			}
			mmf = new AUMemMappedFile(tilePath, auMessenger, gbrFile);
		}

		/// <summary>
		/// Sets up the MainMenu after the form loads.
		/// We use MainMenu because it renders right, even though it is extremely old.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.Menu = mainMenu;

			AddClipboardFormatListener(this.Handle);
			pasteButton.Enabled = pasteMenuItem.Enabled = Clipboard.ContainsText();

			LoadReopenList();
		}

		protected override void OnResize(EventArgs e) {
			this.SuspendLayout();

			bool showProperties = infoPanelPropBorder1.Visible;
			bool showFlips = infoPanelVerticalFlipCheckBox.Visible || infoPanelHorizontalFlipCheckBox.Visible;

			mainTileEditBorder.Height = this.ClientSize.Height - 34;
			mainTileEditBorder.Width = this.ClientSize.Width - 59;
			mapEditBorder.Width = mainTileEditBorder.Width - 37;
			mapEditBorder.Height = mainTileEditBorder.Height - (ShowInfoPanel ? (showProperties ? 58 : 33) : 8);

			mapControl.SetBounds(mapEditBorder.Location.X + 1, mapEditBorder.Location.Y + 1, mapEditBorder.Width - 2, mapEditBorder.Height - 2);

			tileList.Left = mainTileEditBorder.Right + 3;
			tileList.Height = mainTileEditBorder.Height - 20;

			infoPanelBorder.Top = mapEditBorder.Bottom + 4;
			infoPanelBorder.Width = mapEditBorder.Width - 1;
			infoPanelBorder.Height = (showProperties ? 46 : 21);

			infoPanelPropBorder1.Top = infoPanelBorder.Top + 21;
			infoPanelPropBorder1.Width = infoPanelBorder.Width - 4;
			infoPanelPropBorder2.Top = infoPanelBorder.Top + 20;
			infoPanelPropBorder2.Width = infoPanelBorder.Width - 4;

			infoPanelLocationLabel.Location = new Point(infoPanelBorder.Left + 4, infoPanelBorder.Top + 3);
			infoPanelHorizontalFlipCheckBox.Location = new Point(infoPanelBorder.Right - 80, infoPanelBorder.Top + 3);
			infoPanelVerticalFlipCheckBox.Location = new Point(infoPanelBorder.Right - 150, infoPanelBorder.Top + 3);
			infoPanelPaletteComboBox.Location = new Point(infoPanelBorder.Right - (showFlips ? 246 : 96), infoPanelBorder.Top + 1);
			infoPanelPalLabel.Location = new Point(infoPanelBorder.Right - (showFlips ? 270 : 120), infoPanelBorder.Top + 3);

			this.ResumeLayout(true);

			base.OnResize(e);
		}

		private void auMessenger_OnTileChanged(object sender, TileChangedEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				gbrFile.GetObjectsOfType<GBRObjectTileData>().First().tiles[args.TileID] = mmf.Tiles[args.TileID];
				//TODO: Below is very ineficiant.
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().GBCPalettes[args.TileID] = mmf.PalMaps[args.TileID].GBC;
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().SGBPalettes[args.TileID] = mmf.PalMaps[args.TileID].SGB;

				//Alert it of the change (This is bad code, but I don't know how to fix yet)
				this.mapControl.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
				this.tileList.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
			}));
		}

		private void auMessenger_OnGBPaletteChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				//Ignored.
			}));
		}

		private void auMessenger_OnColorPaletteChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				//Ignored.
			}));
		}

		private void auMessenger_OnTileRefreshNeeded(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				gbrFile.GetObjectsOfType<GBRObjectTileData>().First().tiles = mmf.Tiles.GetTilesArray();

				//Inefficiant, but it works.
				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					 gbcPal[i] = mmf.PalMaps[i].GBC;
					 sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().GBCPalettes = gbcPal;
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().SGBPalettes = sgbPal;

				this.mapControl.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
				this.mapControl.DefaultPalette = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
				this.tileList.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
				this.tileList.PaletteMapping = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
			}));
		}

		private void auMessenger_OnTileSizeChanged(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				//Hahaha.  You thought you could get away with changing the size?
				//This app doesn't yet supportsizes other than 8.  So we force it.
				if (mmf.TileWidth != 8) {
					mmf.TileWidth = 8;
				}
				if (mmf.TileHeight != 8) {
					mmf.TileHeight = 8;
				}

				//Ignore this as well!
			}));
		}

		private void auMessenger_OnTotalRefreshNeeded(object sender, MessageEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				gbrFile.GetObjectsOfType<GBRObjectTileData>().First().tiles = mmf.Tiles.GetTilesArray();

				UInt32[] gbcPal = new UInt32[mmf.TileCount];
				UInt32[] sgbPal = new UInt32[mmf.TileCount];

				for (UInt16 i = 0; i < mmf.TileCount; i++) {
					gbcPal[i] = mmf.PalMaps[i].GBC;
					sgbPal[i] = mmf.PalMaps[i].SGB;
				}

				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().GBCPalettes = gbcPal;
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().SGBPalettes = sgbPal;

				//Alert it of the change (This is bad code, but I don't know how to fix yet)
				this.mapControl.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
				this.mapControl.DefaultPalette = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
				this.tileList.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
				this.tileList.PaletteMapping = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
				//TODO load the palettedata from MMF (for both map and tilelist)
			}));
		}

		private void addRowButtonClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMap>();

			if (map.Height >= 1023) { return; }

			map.Resize(map.Width, map.Height + 1);

			mapControl.Map = mapControl.Map; //TODO this is not logical -- use an event?
		}

		private void addColumnButtonClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMap>();

			if (map.Width >= 1023) { return; }

			map.Resize(map.Width + 1, map.Height);

			mapControl.Map = mapControl.Map; //TODO this is not logical -- use an event?
		}

		private void removeRowButtonClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMap>();

			if (map.Height <= 1) { return; }

			map.Resize(map.Width, map.Height - 1);

			mapControl.Map = mapControl.Map; //TODO this is not logical -- use an event?
		}

		private void removeColumnButtonClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMap>();

			if (map.Width <= 1) { return; }

			map.Resize(map.Width - 1, map.Height);

			mapControl.Map = mapControl.Map; //TODO this is not logical -- use an event?
		}

		private void toolList_AutoUpdateChanged(object sender, EventArgs e) {
			autoUpdateMenuItem.Checked = toolList.AutoUpdate;
			auMessenger.Enabled = toolList.AutoUpdate;
		}

		private void infoPanelMenuItem_Click(object sender, EventArgs e) {
			this.ShowInfoPanel ^= true;
		}

		private void gridMenuItem_Click(object sender, EventArgs e) {
			this.ShowGrid ^= true;
		}

		private void doubleMarkersMenuItem_Click(object sender, EventArgs e) {
			this.ShowDoubleMarkers ^= true;
		}

		private void propertyColorsMenuItem_Click(object sender, EventArgs e) {
			this.ShowPropertyColors ^= true;
		}

		private void autoUpdateMenuItem_Click(object sender, EventArgs e) {
			this.AutoUpdate ^= true;
		}

		private void zoomComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			switch (zoomComboBox.SelectedIndex) {
			case 0: ZoomLevel = ZoomLevel._25; return;
			case 1: ZoomLevel = ZoomLevel._50; return;
			case 2: ZoomLevel = ZoomLevel._100; return;
			case 3: ZoomLevel = ZoomLevel._150; return;
			case 4: ZoomLevel = ZoomLevel._200; return;
			default: zoomComboBox.SelectedIndex = 0; return;
			}
		}

		private void onZoomMenuItemClicked(object sender, EventArgs e) {
			MenuItem menuItem = sender as MenuItem;

			if (menuItem != null) {
				if (!(menuItem.Tag is ZoomLevel)) { return; }

				this.ZoomLevel = (ZoomLevel)menuItem.Tag;
			}
		}

		private void onColorSetMenuItemClicked(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;

			if (item != null) {
				if (item.Tag is ColorSet) {
					this.ColorSet = (ColorSet)item.Tag;
				}
			}
		}

		private void onToolMenuItemClicked(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;

			if (item != null) {
				if (item.Tag is Tool) {
					toolList.SelectedTool = (Tool)item.Tag;
				}
			}
		}

		private void clearMapMenuItem_Click(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			var properties = gbmFile.GetObjectOfType<GBMObjectMapPropertyData>();
			var defaultProperties = gbmFile.GetObjectOfType<GBMObjectDefaultTilePropertyValues>();

			for (int y = 0; y < map.Master.Height; y++) {
				for (int x = 0; x < map.Master.Width; x++) {
					map.Tiles[x, y] = new GBMObjectMapTileDataRecord();

					for (int i = 0; i < properties.Master.PropCount; i++) {
						properties.Data[x, y, i] = defaultProperties.Data[0, i];
					}
				}
			}

			mapControl.Map = map;
			mapControl.Properties = properties;
		}

		private void tileList_SelectedTileChanged(object sender, EventArgs e) {
			this.SelectedTile = tileList.SelectedTile;
		}

		private void onSetBookmarkClicked(object sender, EventArgs e) {
			switch ((int)(sender as MenuItem).Tag) {
			case 1: Bookmark1 = tileList.SelectedTile; break;
			case 2: Bookmark2 = tileList.SelectedTile; break;
			case 3: Bookmark3 = tileList.SelectedTile; break;
			default: throw new Exception("Tag for sending control was not a valid bookmark - got " + (sender as Control).Tag + ", expected number between 1 and 3 (inclusive).  Sender: " + sender);
			}
		}

		private void onGotoBookmarkClicked(object sender, EventArgs e) {
			switch ((int)(sender as MenuItem).Tag) {
			case 1: tileList.SelectedTile = Bookmark1; break;
			case 2: tileList.SelectedTile = Bookmark2; break;
			case 3: tileList.SelectedTile = Bookmark3; break;
			default: throw new Exception("Tag for sending control was not a valid bookmark - got " + (sender as Control).Tag + ", expected number between 1 and 3 (inclusive).  Sender: " + sender);
			}
		}

		private void onClearBookmarkClicked(object sender, EventArgs e) {
			const UInt16 CLEARED_BOOKMARK = 0xFFFF;

			switch ((int)(sender as MenuItem).Tag) {
			case 1: Bookmark1 = CLEARED_BOOKMARK; break;
			case 2: Bookmark2 = CLEARED_BOOKMARK; break;
			case 3: Bookmark3 = CLEARED_BOOKMARK; break;
			default: throw new Exception("Tag for sending control was not a valid bookmark - got " + (sender as Control).Tag + ", expected number between 1 and 3 (inclusive).  Sender: " + sender);
			}
		}

		private void mapControl_SelectionChanged(object sender, EventArgs e) {
			var properties = gbmFile.GetObjectOfType<GBMObjectMapProperties>();

			infoPanelVerticalFlipCheckBox.CheckState = mapControl.SelectionVerticalFlip;
			infoPanelHorizontalFlipCheckBox.CheckState = mapControl.SelectionHorizontalFlip;

			if (ColorSet.SupportsPaletteCustomization()) {
				var palTemp = mapControl.SelectionPalette;
				if (palTemp.HasValue) {
					if (palTemp >= 0) {
						infoPanelPaletteComboBox.SelectedIndex = palTemp.Value + 1;
					} else {
						infoPanelPaletteComboBox.SelectedIndex = -1;
					}
				} else {
					infoPanelPaletteComboBox.SelectedIndex = 0;
				}
			}
			for (int i = 0; i < properties.Master.PropCount; i++) {
				var propTemp = mapControl.GetSelectionPropertyData(i);
				if (propTemp.HasValue) {
					infoPanelPropTextBoxes[i].Text = propTemp.Value.ToString();
				} else {
					infoPanelPropTextBoxes[i].Text = "";
				}
			}

			int lowerSelectionX = (mapControl.SelectionX1 < mapControl.SelectionX2 ? mapControl.SelectionX1 : mapControl.SelectionX2);
			int upperSelectionX = (mapControl.SelectionX1 < mapControl.SelectionX2 ? mapControl.SelectionX2 : mapControl.SelectionX1);
			int lowerSelectionY = (mapControl.SelectionY1 < mapControl.SelectionY2 ? mapControl.SelectionY1 : mapControl.SelectionY2);
			int upperSelectionY = (mapControl.SelectionY1 < mapControl.SelectionY2 ? mapControl.SelectionY2 : mapControl.SelectionY1);

			//If the selection is only 1 tile, use the location and the tile.  Otherwise, use the range of the selection.
			if ((lowerSelectionX == upperSelectionX) && (lowerSelectionY == upperSelectionY)) {
				infoPanelLocationLabel.Text = String.Format("Location:  [{0},{1}]: {2}",
					lowerSelectionX, lowerSelectionY, mapControl.Map.Tiles[lowerSelectionX, lowerSelectionY].TileNumber);
			} else {
				infoPanelLocationLabel.Text = String.Format("Location:  [{0},{1}] - [{2},{3}]",
					lowerSelectionX, lowerSelectionY, upperSelectionX, upperSelectionY);
			}
		}

		private void infoPanelVerticalFlipCheckBox_Click(object sender, EventArgs e) {
			mapControl.SelectionVerticalFlip = infoPanelVerticalFlipCheckBox.CheckState;
		}

		private void infoPanelHorizontalFlipCheckBox_Click(object sender, EventArgs e) {
			mapControl.SelectionHorizontalFlip = infoPanelHorizontalFlipCheckBox.CheckState;
		}

		private void infoPanelPaletteComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			var palTemp = infoPanelPaletteComboBox.SelectedIndex;
			if (palTemp == -1) {
				return; //Do nothing
			} else if (palTemp == 0) {
				mapControl.SelectionPalette = null;
			} else {
				mapControl.SelectionPalette = infoPanelPaletteComboBox.SelectedIndex - 1;
			}
		}

		private void infoPanelPaletteComboBox_DrawItem(object sender, DrawItemEventArgs e) {
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			
			e.DrawBackground();
			e.DrawFocusRectangle();
			
			if (e.Index == -1) {
				return;
			} else if (e.Index == 0) {
				e.Graphics.DrawString("Default", e.Font, SystemBrushes.ControlText, e.Bounds);
			} else {
				Palette palette = mapControl.PaletteData.GetPaletteSet(this.ColorSet)[e.Index - 1];

				float width = e.Bounds.Width / 5f;

				RectangleF rect = new RectangleF(e.Bounds.X, e.Bounds.Y, width, e.Bounds.Height);

				for (int i = 0; i < 4; i++) {
					rect.X = e.Bounds.X + (width * i);
					using (SolidBrush brush = new SolidBrush(palette[i])) {
						e.Graphics.FillRectangle(brush, rect);
						e.Graphics.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
					}
				}
			}
		}

		private void mapControl_TileClicked(object sender, TileClickedEventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			var properties = gbmFile.GetObjectOfType<GBMObjectMapPropertyData>();
			var defaultProperties = gbmFile.GetObjectOfType<GBMObjectDefaultTilePropertyValues>();

			switch (this.SelectedTool) {
			case Tool.NONE: break; //Do nothing.
			case Tool.PEN:
				map.Tiles[e.tileX, e.tileY].TileNumber = this.SelectedTile;
				map.Tiles[e.tileX, e.tileY].FlippedHorizontally = false;
				map.Tiles[e.tileX, e.tileY].FlippedVertically = false;
				map.Tiles[e.tileX, e.tileY].GBCPalette = null;
				map.Tiles[e.tileX, e.tileY].SGBPalette = null;

				for (int i = 0; i < properties.Master.PropCount; i++) {
					properties.Data[e.tileX, e.tileY, i] = defaultProperties.Data[this.SelectedTile, i];
				}
				break;
			case Tool.FLOOD:
				ChainFloodFill(map, properties, defaultProperties, e.tileX, e.tileY, this.SelectedTile, map.Tiles[e.tileX, e.tileY].TileNumber);
				break;
			case Tool.DROPPER: 
				this.SelectedTile = map.Tiles[e.tileX, e.tileY].TileNumber;
				break;
			}

			e.mapControl.Map = map;
		}

		/// <summary>
		/// Recursively applies the flood fill effect.
		/// </summary>
		/// <param name="map">The map to edit.</param>
		/// <param name="x">The x to start the replacement at.</param>
		/// <param name="y">The y to start the replacement at.</param>
		/// <param name="set">The value to set each replaced tile to.</param>
		/// <param name="search">The value to replace.</param>
		/// <returns>The passed map parameter (both are modified)</returns>
		private GBMObjectMapTileData ChainFloodFill(GBMObjectMapTileData map, GBMObjectMapPropertyData properties,  
				GBMObjectDefaultTilePropertyValues defaultProperties, int x, int y, UInt16 set, UInt16 search) {

			if (search == set) {
				return map; //Deny a potentially infinite loop.
			}

			map.Tiles[x, y].TileNumber = set;
			map.Tiles[x, y].FlippedHorizontally = false;
			map.Tiles[x, y].FlippedVertically = false;
			map.Tiles[x, y].GBCPalette = null;
			map.Tiles[x, y].SGBPalette = null;

			for (int i = 0; i < properties.Master.PropCount; i++) {
				properties.Data[x, y, i] = defaultProperties.Data[set, i];
			}

			if (x > 0 && map.Tiles[x - 1, y].TileNumber == search) {
				ChainFloodFill(map, properties, defaultProperties, x - 1, y, set, search);
			}
			if (x < map.Master.Width - 1 && map.Tiles[x + 1, y].TileNumber == search) {
				ChainFloodFill(map, properties, defaultProperties, x + 1, y, set, search);
			}
			if (y > 0 && map.Tiles[x, y - 1].TileNumber == search) {
				ChainFloodFill(map, properties, defaultProperties, x, y - 1, set, search);
			}
			if (y < map.Master.Height - 1 && map.Tiles[x, y + 1].TileNumber == search) {
				ChainFloodFill(map, properties, defaultProperties, x, y + 1, set, search);
			}

			return map;
		}

		private void toolList_SelectedToolChanged(object sender, EventArgs e) {
			this.SelectedTool = toolList.SelectedTool;
		}

		private void onCutButtonClicked(object sender, EventArgs e) {
			Clipboard.SetText(mapControl.GetSelectedTiles().ToCopyPasteString());
			mapControl.FillSelectedTiles();
		}

		private void onCopyButtonClicked(object sender, EventArgs e) {
			Clipboard.SetText(mapControl.GetSelectedTiles().ToCopyPasteString());
		}

		private void onPasteButtonClicked(object sender, EventArgs e) {
			mapControl.PasteAtSelection(MapCopyPaste.FromCopyPasteString(Clipboard.GetText()));
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AddClipboardFormatListener(IntPtr hwnd);

		private const int WM_CLIPBOARDUPDATE = 0x031D;

		protected override void WndProc(ref Message m) {
			if (m.Msg == WM_CLIPBOARDUPDATE) {
				//Clipboard update occured.
				pasteButton.Enabled = pasteMenuItem.Enabled = Clipboard.ContainsText();
			}

			base.WndProc(ref m);
		}

		private void UpdateProducerInfo() {
			var ProducerInfo = gbmFile.GetObjectOfType<GBMObjectProducerInfo>();

			var splitVersion = ProductVersion.Split('.');

			ProducerInfo.Name = "GBMB in C# v" + ProductVersion + " by Pokechu22";
			ProducerInfo.Version = splitVersion[0] + "." + splitVersion[1];
			ProducerInfo.Info = "By Pokechu22; a remake of Harry Mulder's GBMB.  See http://github.com/pokechu22/GBTD_GBMB.";
		}

		private CleanLabel[] infoPanelPropLabels = new CleanLabel[0];
		private TextBox[] infoPanelPropTextBoxes = new TextBox[0];

		void map_PropCountChanged(object sender, EventArgs e) {
			GBMObjectMap map = (sender as GBMObjectMap);
			var properties = gbmFile.GetObjectOfType<GBMObjectMapProperties>();
			if (map != null) {
				this.infoPanelPropBorder1.Visible = this.infoPanelPropBorder1.Enabled = (map.PropCount != 0);
				this.infoPanelPropBorder2.Visible = this.infoPanelPropBorder2.Enabled = (map.PropCount != 0);
				
				//Dispose all of the curent labels and text boxes.
				foreach (var control in infoPanelPropLabels) { control.Dispose(); }
				foreach (var control in infoPanelPropTextBoxes) { control.Dispose(); }

				uint newPropCount = map.PropCount;

				infoPanelPropLabels = new CleanLabel[newPropCount];
				infoPanelPropTextBoxes = new TextBox[newPropCount];

				int currentX = 6;
				int textBoxY = 2;
				int labelY = 4;
				
				for (int i = 0; i < map.PropCount; i++) {
					GBMObjectMapPropertiesRecord prop = properties.Properties[i];
					
					CleanLabel label = new CleanLabel();
					label.Text = prop.Name;
					label.Name = "infoPanelPropLabel" + i;
					label.Location = new Point(currentX, labelY);
					label.Tag = i;

					currentX += label.Width;
					currentX += 6;

					TextBox textBox = new TextBox();
					textBox.Text = "";
					textBox.Width = 38;
					textBox.Height = 19;
					textBox.Location = new Point(currentX, textBoxY);
					
					textBox.Tag = i;
					textBox.KeyPress += new KeyPressEventHandler((s, a) => a.Handled = !(Char.IsNumber(a.KeyChar) || a.KeyChar == '\x08'));
					textBox.TextChanged += new EventHandler(infoPanelPropertyTextBox_TextChanged);
					
					currentX += textBox.Width;
					currentX += 3;

					infoPanelPropBorder1.Controls.Add(label);
					infoPanelPropBorder1.Controls.Add(textBox);

					infoPanelPropLabels[i] = label;
					infoPanelPropTextBoxes[i] = textBox;
				}
			}

			this.OnResize(new EventArgs());
		}

		void map_TileFileChanged(object sender, EventArgs e) {
			LoadTileFile((sender as GBMObjectMap).TileFile);
		}

		void infoPanelPropertyTextBox_TextChanged(object sender, EventArgs e) {
			var textBox = sender as TextBox;

			if (gbmFile == null) {return;}
			var map = gbmFile.GetObjectOfType<GBMObjectMap>();
			var properties = gbmFile.GetObjectOfType<GBMObjectMapProperties>();
			if (map == null||properties == null) { return; }

			if (textBox != null) {
				if (!(textBox.Tag is int)) {
					throw new Exception("Text box " + textBox + "'s tag is not a number -- got " + textBox.Tag + ".");
				}
				int prop = (int)textBox.Tag;
				if (prop >= map.PropCount) {
					throw new Exception("Text box's tag is beyond the maximum number of properties -- got " + prop + ", max is " + map.PropCount);
				}
				var property = properties.Properties[prop];

				if (!String.IsNullOrWhiteSpace(textBox.Text)) {
					UInt16 value;

					if (UInt16.TryParse(textBox.Text, out value)) {
						if (value >= property.MaxValue) {
							value = (UInt16)property.MaxValue;
							textBox.Text = property.MaxValue.ToString();
						}

						mapControl.SetSelectionPropertyData(prop, value);
					} else {
						textBox.Text = "";
						return;
					}
				}
			}
		}

		private void mapPropertiesMenuItem_Click(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMap>();

			MapPropertiesDialog dialog = new MapPropertiesDialog(map);
			dialog.ShowDialog();

			mapControl.Map = mapControl.Map;
		}

		private void blockFillMenuItem_Click(object sender, EventArgs e) {
			var tileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
			var paletteMapping = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
			var settings = gbmFile.GetObjectOfType<GBMObjectMapSettings>();
			var map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			var properties = gbmFile.GetObjectOfType<GBMObjectMapPropertyData>();
			var defaultProperties = gbmFile.GetObjectOfType<GBMObjectDefaultTilePropertyValues>();

			uint lowerSelectionX = (uint)(mapControl.SelectionX1 < mapControl.SelectionX2 ? mapControl.SelectionX1 : mapControl.SelectionX2);
			uint lowerSelectionY = (uint)(mapControl.SelectionY1 < mapControl.SelectionY2 ? mapControl.SelectionY1 : mapControl.SelectionY2);

			BlockFillDialog dialog = new BlockFillDialog(settings, ColorSet, SelectedTile, lowerSelectionX, lowerSelectionY, 
				tileSet, paletteMapping, tileList.PaletteData, map, properties, defaultProperties);
			dialog.ShowDialog();
			
			mapControl.Map = mapControl.Map;
		}

		private void locationPropertiesMenuItem_Click(object sender, EventArgs e) {
			var properties = gbmFile.GetObjectOfType<GBMObjectMapProperties>();
			var propColors = gbmFile.GetObjectOfType<GBMObjectMapPropertyColors>();

			LocationPropertiesDialog dialog = new LocationPropertiesDialog(properties, propColors);

			dialog.ShowDialog();

			mapControl.Properties = mapControl.Properties;
			mapControl.PropertyColors = mapControl.PropertyColors;
		}

		private void defaultLocationPropertiesMenuItem_Click(object sender, EventArgs e) {
			var paletteMapping = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
			var tileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
			var properties = gbmFile.GetObjectOfType<GBMObjectMapProperties>();
			var defaultProperties = gbmFile.GetObjectOfType<GBMObjectDefaultTilePropertyValues>();

			DefaultLocationPropertiesDialog dialog = new DefaultLocationPropertiesDialog(tileSet, ColorSet, SelectedTile, 
				paletteMapping, tileList.PaletteData, properties, defaultProperties);

			dialog.ShowDialog();
		}

		private void LoadReopenList() {
			if (Properties.Settings.Default.RecentlyUsedFiles == null) {
				Properties.Settings.Default.RecentlyUsedFiles = new System.Collections.Specialized.StringCollection();
			}

			reopenMenuItem.MenuItems.Clear();
			if (Properties.Settings.Default.RecentlyUsedFiles.Count > 0) {
				reopenMenuItem.Enabled = reopenMenuItem.Visible = true;
				reopenSeperatorMenuItem.Enabled = reopenSeperatorMenuItem.Visible = true;

				foreach (String fileName in Properties.Settings.Default.RecentlyUsedFiles) {
					reopenMenuItem.MenuItems.Add(new MenuItem(fileName, new EventHandler(anyReopenMenuItem_Click)));
				}
			} else {
				reopenMenuItem.Enabled = reopenMenuItem.Visible = false;
				reopenSeperatorMenuItem.Enabled = reopenSeperatorMenuItem.Visible = false;
			}
		}

		private void AddToReopenList(String gbmFile) {
			//TODO cap the length.

			//If the item is already present, remove it (so that we don't get a duplicate item).  Then, add it to the start.
			Properties.Settings.Default.RecentlyUsedFiles.Remove(gbmFile.ToLowerInvariant());
			Properties.Settings.Default.RecentlyUsedFiles.Insert(0, gbmFile.ToLowerInvariant());

			LoadReopenList();
		}

		void anyReopenMenuItem_Click(object sender, EventArgs e) {
			MenuItem item = sender as MenuItem;
			if (item != null) {
				LoadMapFile(item.Text);
			}
		}

		private void copyAsBitmapMenuItem_Click(object sender, EventArgs e) {
			using (Bitmap bitmap = mapControl.GetMapImage()) {
				Clipboard.SetImage(bitmap);
			}
		}

		private void onExportButtonClicked(object sender, EventArgs e) {
			MessageBox.Show("Exporting is not yet implemented!");
		}

		private void exportToMenuItem_Click(object sender, EventArgs e) {
			var properties = gbmFile.GetObjectOfType<GBMObjectMapProperties>();
			var exportSettings = gbmFile.GetObjectOfType<GBMObjectMapExportSettings>();
			var exportProperties = gbmFile.GetObjectOfType<GBMObjectMapExportProperties>();

			ExportOptionsDialog d = new ExportOptionsDialog(properties, exportSettings, exportProperties);

			d.ShowDialog();

			var ex = new Exporting.GBDKCMapExporter();
			using (MemoryStream stream = new MemoryStream()) {
				ex.Export(gbmFile, stream, "EXPORT.C");

				Clipboard.SetText(Encoding.Default.GetString(stream.ToArray()));
			}
		}
	}
}
