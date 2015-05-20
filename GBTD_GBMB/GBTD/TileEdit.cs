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

			PaletteData paletteData = new PaletteData(palettes.SGBPalettes, palettes.GBCPalettes);

			tileList.TileSet = file.GetOrCreateObjectOfType<GBRObjectTileData>();
			tileList.PaletteData = paletteData;
			tileList.PaletteMapping = paletteMapping;

			if (!String.IsNullOrEmpty(filePath)) {
				auMessenger.FileName = filePath;
				if (mmf != null) {
					mmf.Dispose(); //Remove old MMF.
				}
				mmf = new AUMemMappedFile(filePath, auMessenger, gbrFile);

				auMessenger.Enabled = true; //TODO: Proper control over this (ToolList).
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
	}
}
