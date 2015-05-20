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

namespace GB.GBTD
{
	public partial class TileEdit : Form
	{
		private string filePath;
		private GBRFile gbrFile;

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
	}
}
