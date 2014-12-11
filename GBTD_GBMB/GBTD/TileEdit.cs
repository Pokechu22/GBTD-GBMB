using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Palette;
using GB.Shared.Tile;

namespace GB.GBTD
{
	public partial class TileEdit : Form
	{
		private class HoverChangingToolStripButton : ToolStripButton
		{
			/// <summary>
			/// Whether or not currently being hovered over.
			/// </summary>
			private bool hovered = false;

			/// <summary>
			/// Image used when hovered over.
			/// </summary>
			private Image hoveredImage;
			/// <summary>
			/// Image used elsewhere.
			/// </summary>
			private Image nonhoveredImage;

			/// <summary>
			/// Image used when hovered over.
			/// </summary>
			[Category("Appearance"), Description("The image used when hovered over.")]
			public Image HoveredImage {
				get { return hoveredImage; }
				set { if (value == null) { /*throw new ArgumentNullException();*/value = Image; } hoveredImage = value; updateImage(); }
			}
			/// <summary>
			/// Image used when not hovered over.
			/// </summary>
			[Category("Appearance"), Description("The image used when not hovered over.")]
			public Image NonHoveredImage {
				get { return nonhoveredImage; }
				set { if (value == null) { /*throw new ArgumentNullException();*/value = Image; } nonhoveredImage = value; updateImage(); }
			}

			[EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public override Image Image {
				get { return base.Image; }
				set { base.Image = value; }
			}

			protected override void OnMouseEnter(EventArgs e) {
				base.OnMouseEnter(e);
				hovered = true;
				updateImage();
			}
			protected override void OnMouseLeave(EventArgs e) {
				base.OnMouseLeave(e);
				hovered = false;
				updateImage();
			}

			protected void updateImage() {
				this.Image = (hovered ? hoveredImage : nonhoveredImage);
			}
		}

		/// <summary>
		/// Contains all of the tilerenderers used to preview, so that I don't have to refer to all of them seperately.
		/// </summary>
		private List<TileRenderer> previewRenderers = new List<TileRenderer>();

		public TileEdit() {
			InitializeComponent();

			//Set up the preview renderers.
			previewRenderers.Add(miniPreviewRenderer);

			previewRenderers.Add(groupedTileRenderer1);
			previewRenderers.Add(groupedTileRenderer2);
			previewRenderers.Add(groupedTileRenderer3);
			previewRenderers.Add(groupedTileRenderer4);
			previewRenderers.Add(groupedTileRenderer5);
			previewRenderers.Add(groupedTileRenderer6);
			previewRenderers.Add(groupedTileRenderer7);
			previewRenderers.Add(groupedTileRenderer8);
			previewRenderers.Add(groupedTileRenderer9);
			previewRenderers.Add(groupedTileRenderer10);
			previewRenderers.Add(groupedTileRenderer11);
			previewRenderers.Add(groupedTileRenderer12);
			previewRenderers.Add(groupedTileRenderer13);
			previewRenderers.Add(groupedTileRenderer14);
			previewRenderers.Add(groupedTileRenderer15);
			previewRenderers.Add(groupedTileRenderer16);
		}

		private void tileEditBorder_Paint(object sender, PaintEventArgs e) {
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, tileEditBorder.Width, tileEditBorder.Height, Border3DStyle.SunkenOuter);
		}

		private void toolList1_SelectedToolChanged(object sender, EventArgs e) {
			ToolList list = sender as ToolList;
			if (list != null) {
				mainTileEdit.EditorTypeID = list.SelectedTool;
			}
		}

		private void gbtdgbcPaletteChooser1_MouseButtonColorChanged(object sender, EventArgs e) {
			GBTDGBCPaletteChooser chooser = sender as GBTDGBCPaletteChooser;
			if (chooser != null) {
				mainTileEdit.LeftMouseColor = chooser.LeftMouseColor;
				mainTileEdit.RightMouseColor = chooser.RightMouseColor;
				mainTileEdit.MiddleMouseColor = chooser.MiddleMouseColor;
				mainTileEdit.XButton1MouseColor = chooser.X1MouseColor;
				mainTileEdit.XButton2MouseColor = chooser.X2MouseColor;
			}
		}

		private void mainTileEdit_TileChanged(object sender, EventArgs e) {
			foreach (var v in previewRenderers) {
				v.Tile = mainTileEdit.Tile;
			}
			this.tileList1[0] = mainTileEdit.Tile;
		}

		private void mainTileEdit_PalatteChanged(object sender, EventArgs e) {
			foreach (var v in previewRenderers) {
				v.BlackColor = mainTileEdit.BlackColor;
				v.WhiteColor = mainTileEdit.WhiteColor;
				v.LightGrayColor = mainTileEdit.LightGrayColor;
				v.DarkGrayColor = mainTileEdit.DarkGrayColor;
			}
		}

		private void autoUpdatedToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			toolList.AutoUpdate = autoUpdatedToolStripMenuItem.Checked;
		}

		private void gridToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			mainTileEdit.Grid = gridToolStripMenuItem.Checked;
		}

		private void palettesToolStripMenuItem_Click(object sender, EventArgs e) {
			GBCChoosePalette d = new GBCChoosePalette(paletteChooser.Set.Clone() as GBCPaletteSet);
			d.ShowDialog();
			if (d.DialogResult == DialogResult.OK) {
				paletteChooser.Set = d.Set;
			}
		}

		private void paletteChooser_SelectedPaletteChanged(object sender, EventArgs e) {
			mainTileEdit.SetColors(paletteChooser.Set[paletteChooser.SelectedRow].EntryBlack,
				paletteChooser.Set[paletteChooser.SelectedRow].EntryWhite,
				paletteChooser.Set[paletteChooser.SelectedRow].EntryLightGray,
				paletteChooser.Set[paletteChooser.SelectedRow].EntryDarkGray);
		}
	}
}
