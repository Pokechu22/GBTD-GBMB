﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palettes;
using GB.Shared.Tiles;

namespace GB.Shared.Controls
{
	partial class TileListEntryControl : UserControl
	{
		protected override Padding DefaultMargin { get { return new Padding(0, 0, 0, 0); } }

		private int number = 0;

		private ColorSet colorSet;

		[Category("Data"), Description("The tileData's number.")]
		public int Number {
			get { return number; }
			set { number = value; this.Invalidate(true); }
		}

		private TileData tileData = new TileData();
		[Category("Data"), Description("The tileData used.")]
		[ReadOnly(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TileData TileData {
			get { return tileData; }
			set {
				tileData = value; tileRenderer1.TileData = value; OnTileChanged(); this.Invalidate(true);
			}
		}

		[Category("Data"), Description("The ColorSet used.")]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; tileRenderer1.ColorSet = value; this.Invalidate(true); }
		}

		private bool selected;
		[Category("Data"), Description("Whether or not this has been selected.")]
		public bool Selected {
			get { return selected; }
			set { selected = value; tileRenderer1.Selected = value; this.Invalidate(true); }
		}

		public void OnTileChanged() {
			this.textDisplay.Height = tileRenderer1.Height;
			this.background.Size = this.Size = new Size(tileRenderer1.Bounds.Right, tileRenderer1.Bounds.Bottom + 1);
		}
		
		public TileListEntryControl() {
			InitializeComponent();
		}

		protected override void OnEnabledChanged(EventArgs e) {
			base.OnEnabledChanged(e);
			this.Invalidate(true);
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