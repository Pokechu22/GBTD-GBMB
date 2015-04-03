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
using GB.Shared.Palettes;

namespace GB.GBMB.Dialogs
{
	public partial class DefaultLocationPropertiesDialog : Form
	{
		public GBMObjectMapProperties Properties { get; private set; }
		public GBMObjectDefaultTilePropertyValues DefaultProperties { get; private set; }

		private DefaultLocationPropertiesDialog() {
			InitializeComponent();
		}

		public DefaultLocationPropertiesDialog(GBRObjectTileData tileSet, ColorSet colorSet, UInt16 selectedTile, 
				GBRObjectTilePalette paletteMapping, PaletteData paletteData, GBMObjectMapProperties properties, 
				GBMObjectDefaultTilePropertyValues defaultProperties) : this() {

			this.Properties = properties;
			this.DefaultProperties = defaultProperties;

			tileList.TileSet = tileSet;
			tileList.PaletteMapping = paletteMapping;
			tileList.PaletteData = paletteData;
			tileList.ColorSet = colorSet;
			tileList.SelectedTile = selectedTile;
		}
	}
}
