using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palettes;

namespace GB.Shared.Tiles
{
	partial class TileListEntryControl : UserControl
	{
		//The three default sizings.
		protected override Size DefaultMaximumSize { get { return new Size(38, 17); } }
		protected override Size DefaultMinimumSize { get { return new Size(38, 17); } }
		protected override Size DefaultSize { get { return new Size(38, 17); } }
		protected override Padding DefaultMargin { get { return new Padding(0, 0, 0, 0); } }

		private int number = 0;

		private ColorSet colorSet;

		[Category("Data"), Description("The tileData's number.")]
		public int Number {
			get { return number; }
			set { number = value; SetupApearence(); }
		}

		private TileData tileData = new TileData();
		[Category("Data"), Description("The tileData used.")]
		[ReadOnly(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TileData TileData {
			get { return tileData; }
			set { tileData = value; SetupApearence(); }
		}

		[Category("Data"), Description("The ColorSet used.")]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; }
		}

		private bool selected;
		[Category("Data"), Description("Whether or not this has been selected.")]
		public bool Selected {
			get { return selected; }
			set { selected = value; SetupApearence(); }
		}

		public TileListEntryControl() {
			InitializeComponent();
		}

		

		protected virtual void SetupApearence() {
			if (Enabled) {
				if (Selected) {
					tileRenderer1.Tile = this.tileData.tile;
					tileRenderer1.Palette = this.tileData.GetPalette(ColorSet).FilterAsSelected();
				} else {
					tileRenderer1.Tile = this.tileData.tile;
					tileRenderer1.Palette = this.tileData.GetPalette(ColorSet);
				}
			} else {
				tileRenderer1.Tile = this.tileData.tile;
				tileRenderer1.Palette = new Palette_(); //TODO
			}
			this.Invalidate(true);
		}

		protected override void OnEnabledChanged(EventArgs e) {
			base.OnEnabledChanged(e);
			SetupApearence();
		}

		private void textDisplay_Paint(object sender, PaintEventArgs e) {
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textDisplay.Width, textDisplay.Height, Border3DStyle.RaisedInner, Border3DSide.Left);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textDisplay.Width, textDisplay.Height, Border3DStyle.RaisedInner, Border3DSide.Bottom);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textDisplay.Width, textDisplay.Height, Border3DStyle.RaisedInner, Border3DSide.Right);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textDisplay.Width, textDisplay.Height, Border3DStyle.RaisedInner, Border3DSide.Top);

			StringFormat format = new StringFormat(StringFormatFlags.NoClip);
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;

			String usedText = "";
			if (this.Enabled) {
				usedText = this.number.ToString();
			}
			e.Graphics.DrawString(usedText, new Font(DefaultFont.FontFamily, 7.5f), Brushes.Black, 
				new RectangleF(0, -1, textDisplay.Width, textDisplay.Height), format);
		}

		/// <summary>
		/// Propper on-click handling in an ugly way.
		/// Calls <code>this.InvokeOnClick</code> whenever a subcontrol is clicked.
		/// 
		/// TODO: This is not a good way of doing this, but I don't know the right way.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void subcontrol_Click(object sender, EventArgs e) {
			this.InvokeOnClick(this, e);
		}
	}
}
