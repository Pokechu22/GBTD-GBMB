﻿using System;
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

namespace GB.GBMB
{
	public partial class MapEdit : Form
	{
		private GBMFile gbmFile;
		private GBRFile gbrFile;
		private AUMemMappedFile mmf;
		private string tileFileName;

		private bool infoPanel;

		/// <summary>
		/// TODO: NYI.
		/// </summary>
		public bool ShowInfoPanel {
			get { return infoPanel; }
			set {
				infoPanelMenuItem.Checked = value;
				gbmFile.GetObjectOfType<GBMObjectMapSettings>().ShowInfoPanel = value;
				infoPanel = value;
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


		public MapEdit() {
			InitializeComponent();

			mapEditBorder_Resize(mapEditBorder, new EventArgs());
		}

		private void openMenuItem_Click(object sender, EventArgs e) {
			OpenFileDialog d = new OpenFileDialog();
			d.Filter = "GBM files|*.gbm|All files|*.*";

			var result = d.ShowDialog();
			if (result != DialogResult.OK) {
				return;
			}

			LoadFile(d.FileName);
			auMessenger.FileName = tileFileName;
			mmf = new AUMemMappedFile(tileFileName, auMessenger, gbrFile);

			mapControl.Enabled = true;
		}

		public void LoadFile(String mapPath) {
			Environment.CurrentDirectory = Path.GetDirectoryName(mapPath);
			using (var stream = File.OpenRead(mapPath)) {
				gbmFile = new GBMFile(stream);
			}
			
			GBMObjectMap map = gbmFile.GetObjectOfType<GBMObjectMap>();

			tileFileName = Path.GetFullPath(map.TileFile);

			using (var stream = File.OpenRead(tileFileName)) {
				gbrFile = new GBRFile(stream);
			}

			this.mapControl.Map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			this.mapControl.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
			var pals = gbrFile.GetObjectsOfType<GBRObjectPalettes>().First();
			this.mapControl.PaletteData = new Shared.Palettes.PaletteData(pals.SGBPalettes, pals.GBCPalettes);
			this.mapControl.DefaultPalette = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();

			var settings = gbmFile.GetObjectOfType<GBMObjectMapSettings>();
			this.AutoUpdate = settings.AutoUpdate;
			this.ShowGrid = settings.ShowGrid;
			this.ShowInfoPanel = settings.ShowInfoPanel;
			this.ShowPropertyColors = settings.ShowPropColors;
			this.ShowDoubleMarkers = settings.ShowDoubleMarkers;
		}

		/// <summary>
		/// Sets up the MainMenu after the form loads.
		/// We use MainMenu because it renders right, even though it is extremely old.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			this.Menu = mainMenu;
		}

		protected override void OnResize(EventArgs e) {
			mapEditBorder.Width = this.Width - 100;
			mapEditBorder.Height = this.Height - 100;
			base.OnResize(e);
		}

		private void mapEditBorder_Resize(object sender, EventArgs e) {
			//Keep mapControl within mapEditBorder.
			mapControl.SetBounds(mapEditBorder.Location.X + 1, mapEditBorder.Location.Y + 1, mapEditBorder.Width - 2, mapEditBorder.Height - 2);
		}

		private void auMessenger_OnTileChanged(object sender, TileChangedEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				gbrFile.GetObjectsOfType<GBRObjectTileData>().First().tiles[args.TileID] = mmf.Tiles[args.TileID];
				//TODO: Below is very ineficiant.
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().GBCPalettes[args.TileID] = mmf.PalMaps[args.TileID].GBC;
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().SGBPalettes[args.TileID] = mmf.PalMaps[args.TileID].SGB;

				//Alert it of the change (This is bad code, but I don't know how to fix yet)
				this.mapControl.TileSet = this.mapControl.TileSet;
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
			}));
		}

		private void toolList_AddRowClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			map.Resize(map.Master.Width, map.Master.Height + 1);

			mapControl.Map = map;
		}

		private void toolList_AddColumnClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			map.Resize(map.Master.Width + 1, map.Master.Height);

			mapControl.Map = map;
		}

		private void toolList_RemoveRowClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			map.Resize(map.Master.Width, map.Master.Height - 1);

			mapControl.Map = map;
		}

		private void toolList_RemoveColumnClicked(object sender, EventArgs e) {
			var map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			map.Resize(map.Master.Width - 1, map.Master.Height);

			mapControl.Map = map;
		}

		private void toolList_SelectedToolChanged(object sender, EventArgs e) {
			//TODO: Not yet implemented.
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
	}
}
