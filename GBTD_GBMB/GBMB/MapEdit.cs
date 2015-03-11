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

namespace GB.GBMB
{
	public partial class MapEdit : Form
	{
		private GBMFile gbmFile;
		private GBRFile gbrFile;

		public MapEdit() {
			InitializeComponent();

			mapEditBorder_Resize(mapEditBorder, new EventArgs());
		}

		private void button1_Click(object sender, EventArgs e) {
			OpenFileDialog d = new OpenFileDialog();
			d.Filter = "GBM files|*.gbm|All files|*.*";

			var result = d.ShowDialog();
			if (result != DialogResult.OK) {
				return;
			}

			Environment.CurrentDirectory = Path.GetDirectoryName(d.FileName);
			using (var stream = d.OpenFile()) {
				gbmFile = new GBMFile(stream);
			}

			GBMObjectMap map = gbmFile.GetObjectOfType<GBMObjectMap>();

			using (var stream = File.OpenRead(map.TileFile)) {
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
	}
}
