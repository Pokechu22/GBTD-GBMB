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

namespace GB.GBMB
{
	public partial class MapEdit : Form
	{
		private GBMFile gbmFile;
		private GBRFile gbrFile;
		private AUMemMappedFile mmf;
		private string tileFileName;

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
			mmf = new AUMemMappedFile(tileFileName, auMessenger);

			mapControl1.Enabled = true;
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

			this.mapControl1.Map = gbmFile.GetObjectOfType<GBMObjectMapTileData>();
			this.mapControl1.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
			var pals = gbrFile.GetObjectsOfType<GBRObjectPalettes>().First();
			this.mapControl1.PaletteData = new Shared.Palettes.PaletteData(pals.SGBPalettes, pals.GBCPalettes);
			this.mapControl1.DefaultPalette = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
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

		private void mapEditBorder_Resize(object sender, EventArgs e) {
			//Keep mapControl within mapEditBorder.
			mapControl1.SetBounds(mapEditBorder.Location.X + 1, mapEditBorder.Location.Y + 1, mapEditBorder.Width - 2, mapEditBorder.Height - 2);
		}

		private void auMessenger_OnTileChanged(object sender, TileChangedEventArgs args) {
			Invoke(new MethodInvoker(delegate
			{
				gbrFile.GetObjectsOfType<GBRObjectTileData>().First().tiles[args.TileID] = mmf.Tiles[args.TileID];
				//TODO: Below is very ineficiant.
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().GBCPalettes[args.TileID] = mmf.PalMaps[args.TileID].GBC;
				gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First().SGBPalettes[args.TileID] = mmf.PalMaps[args.TileID].SGB;

				//Alert it of the change (This is bad code, but I don't know how to fix yet)
				this.mapControl1.TileSet = this.mapControl1.TileSet;
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

				this.mapControl1.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
				this.mapControl1.DefaultPalette = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
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
				this.mapControl1.TileSet = gbrFile.GetObjectsOfType<GBRObjectTileData>().First();
				this.mapControl1.DefaultPalette = gbrFile.GetObjectsOfType<GBRObjectTilePalette>().First();
			}));
		}
	}
}
