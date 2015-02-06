using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Tiles;

namespace GB.GBTD
{
	public partial class PreviewRenderer : UserControl
	{
		private TileData tileData;

		public TileData TileData {
			get { return tileData; }
			set { tileData = value; OnTileDataChanged(); }
		}

		private bool simple = false;

		public bool Simple {
			get { return simple; }
			set {
				simple = value;

				panel1.Visible = panel1.Enabled = !value;
				
				OnResize(new EventArgs());
			}
		}

		public PreviewRenderer() {
			InitializeComponent();

			this.SetStyle(ControlStyles.FixedWidth, true);
		}

		private void OnTileDataChanged() {
			int oldRendererWidth = miniPreviewRenderer.Width;
			int oldRendererHeight = miniPreviewRenderer.Height;

			//The following line is massive but it changes all of the datas at once.
			miniPreviewRenderer.TileData = groupedTileRenderer1.TileData = groupedTileRenderer2.TileData = groupedTileRenderer3.TileData = groupedTileRenderer4.TileData = groupedTileRenderer5.TileData = groupedTileRenderer6.TileData = groupedTileRenderer7.TileData = groupedTileRenderer8.TileData = groupedTileRenderer9.TileData = groupedTileRenderer10.TileData = groupedTileRenderer11.TileData = groupedTileRenderer12.TileData = groupedTileRenderer13.TileData = groupedTileRenderer14.TileData = groupedTileRenderer15.TileData = groupedTileRenderer16.TileData = this.tileData;

			if (miniPreviewRenderer.Width != oldRendererWidth || miniPreviewRenderer.Height != oldRendererHeight) {
				panel1.Width = miniPreviewRenderer.Width * 4 + 2;
				panel1.Height = miniPreviewRenderer.Height * 4 + 2;
				this.OnResize(new EventArgs());
			}
		}

		protected override void OnResize(EventArgs e) {
			int spaceForP2Centering;

			if (panel1.Visible) {
				this.Width = panel1.Width;
				
				spaceForP2Centering = this.Height - panel1.Height;
			} else {
				this.Width = panel2.Width;

				spaceForP2Centering = this.Height;
			}

			panel1.Location = new Point(0, this.Height - panel1.Height);
			panel2.Location = new Point((this.Width / 2) - (panel2.Width / 2), (spaceForP2Centering / 2) - (panel2.Width / 2));

			base.OnResize(e);
		}
	}
}
