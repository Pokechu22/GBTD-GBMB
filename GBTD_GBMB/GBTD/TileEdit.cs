using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Palette;

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

		public TileEdit() {
			InitializeComponent();
		}

		private void tileEditBorder_Paint(object sender, PaintEventArgs e) {
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, tileEditBorder.Width, tileEditBorder.Height, Border3DStyle.SunkenOuter);
		}

		private void toolList1_SelectedToolChanged(object sender, EventArgs e) {
			ToolList list = sender as ToolList;
			if (list != null) {
				pixelEditableTileRenderer1.EditorTypeID = list.SelectedTool;
			}
		}

		private void gbtdgbcPaletteChooser1_MouseButtonColorChanged(object sender, EventArgs e) {
			GBTDGBCPaletteChooser chooser = sender as GBTDGBCPaletteChooser;
			if (chooser != null) {
				pixelEditableTileRenderer1.LeftMouseColor = chooser.LeftMouseColor;
				pixelEditableTileRenderer1.RightMouseColor = chooser.RightMouseColor;
				pixelEditableTileRenderer1.MiddleMouseColor = chooser.MiddleMouseColor;
				pixelEditableTileRenderer1.XButton1MouseColor = chooser.X1MouseColor;
				pixelEditableTileRenderer1.XButton2MouseColor = chooser.X2MouseColor;
			}
		}
	}
}
