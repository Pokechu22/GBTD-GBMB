using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Tile
{
	public partial class PixelEditableTileRenderer : TileRenderer
	{
		public PixelEditableTileRenderer() {
			InitializeComponent();
		}

		private void PixelEditableTileRenderer_PixelClicked(object sender, PixelClickEventArgs e) {
			Tile[e.x, e.y] = GBColor.WHITE;
		}
	}
}
