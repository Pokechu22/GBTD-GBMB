using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Tiles;
using GB.Shared.Palettes;
using GB.Shared.Controls;

namespace GB.GBTD
{
	public partial class PreviewRenderer : UserControl
	{
		private TileData tileData;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TileData TileData {
			get { return tileData; }
			set { tileData = value; OnTileDataChanged(); }
		}

		private ColorSet colorSet;
		[Category("Display"), Description("The color set used.")]
		public ColorSet ColorSet {
			get { return colorSet; }
			set {
				colorSet = value;
				foreach (TileRenderer t in this.renderers) {
					t.ColorSet = value;
				}
				miniPreviewRenderer.ColorSet = value;
			}
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

		private TileRenderer[,] renderers;

		public PreviewRenderer() {
			InitializeComponent();

			this.SetStyle(ControlStyles.FixedWidth, true);
			this.SetStyle(ControlStyles.FixedHeight, true);

			this.renderers = new TileRenderer[4, 4] {
				{groupedTileRenderer1, groupedTileRenderer2, groupedTileRenderer3, groupedTileRenderer4},
				{groupedTileRenderer5, groupedTileRenderer6, groupedTileRenderer7, groupedTileRenderer8},
				{groupedTileRenderer9, groupedTileRenderer10, groupedTileRenderer11, groupedTileRenderer12},
				{groupedTileRenderer13, groupedTileRenderer14, groupedTileRenderer15, groupedTileRenderer16},
			};
		}

		private void OnTileDataChanged() {
			int oldRendererWidth = miniPreviewRenderer.Width;
			int oldRendererHeight = miniPreviewRenderer.Height;

			miniPreviewRenderer.TileData = this.tileData;
			foreach (TileRenderer t in this.renderers) { t.TileData = this.tileData; }

			if (miniPreviewRenderer.Width != oldRendererWidth || miniPreviewRenderer.Height != oldRendererHeight) {
				panel1.Width = miniPreviewRenderer.Width * 4 + 2;
				panel1.Height = miniPreviewRenderer.Height * 4 + 2;

				panel2.Width = miniPreviewRenderer.Width + 2;
				panel2.Height = miniPreviewRenderer.Height + 2;

				for (int x = 0; x < 4; x++) {
					for (int y = 0; y < 4; y++) {
						TileRenderer r = renderers[x, y];
						r.Location = new Point(x * r.Width, y * r.Height);
					}
				}

				this.OnResize(new EventArgs());
			}
		}

		protected override void OnResize(EventArgs e) {
			int spaceForP2Centering;

			this.Height = (24 * (tileData.Height != 0 ? tileData.Height : 8));

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
