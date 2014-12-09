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
	partial class TileListEntry : UserControl
	{
		//The three default sizings.
		protected override Size DefaultMaximumSize { get { return new Size(38, 17); } }
		protected override Size DefaultMinimumSize { get { return new Size(38, 17); } }
		protected override Size DefaultSize { get { return new Size(38, 17); } }
		protected override Padding DefaultMargin { get { return new Padding(0, 0, 0, 0); } }

		private int number = 0;
		[Category("Data"), Description("The tile's number.")]
		public int Number {
			get { return number; }
			set { number = value; SetupApearence(); }
		}

		private Tile tile = new Tile();
		[Category("Data"), Description("The tile used.")]
		[ReadOnly(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Tile Tile {
			get { return tile; }
			set { tile = value; SetupApearence(); }
		}

		private Color black = Color.Black, darkGray = Color.DarkGray, lightGray = Color.LightGray, white = Color.White;
		[Category("Data"), Description("The color used for \"Black\" areas.")]
		public Color Black {
			get { return this.black; }
			set { this.black = value; SetupApearence(); }
		}
		[Category("Data"), Description("The color used for \"Dark Gray\" areas.")]
		public Color DarkGray {
			get { return this.darkGray; }
			set { this.darkGray = value; SetupApearence(); }
		}
		[Category("Data"), Description("The color used for \"Light Gray\" areas.")]
		public Color LightGray {
			get { return this.lightGray; }
			set { this.lightGray = value; SetupApearence(); }
		}
		[Category("Data"), Description("The color used for \"White\" areas.")]
		public Color White {
			get { return this.white; }
			set { this.white = value; SetupApearence(); }
		}

		public void SetColors(Color black, Color darkGray, Color lightGray, Color white) {
			this.white = white;
			this.darkGray = darkGray;
			this.lightGray = lightGray;
			this.black = black;
			SetupApearence();
		}

		public TileListEntry() {
			InitializeComponent();
		}

		private void textLabel_Paint(object sender, PaintEventArgs e) {
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textLabel.Width, textLabel.Height, Border3DStyle.RaisedInner, Border3DSide.Left);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textLabel.Width, textLabel.Height, Border3DStyle.RaisedInner, Border3DSide.Bottom);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textLabel.Width, textLabel.Height, Border3DStyle.RaisedInner, Border3DSide.Right);
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, textLabel.Width, textLabel.Height, Border3DStyle.RaisedInner, Border3DSide.Top);
			
		}

		protected virtual void SetupApearence() {
			if (Enabled) {
				textLabel.Text = number.ToString();
				tileRenderer1.Tile = this.tile;
				tileRenderer1.SetColors(black, darkGray, lightGray, white);
			} else {
				textLabel.Text = "";
				tileRenderer1.Tile = this.tile;
				tileRenderer1.SetColors(SystemColors.Control, SystemColors.Control, SystemColors.Control, SystemColors.Control);
			}
			this.Refresh();
		}

		protected override void OnEnabledChanged(EventArgs e) {
			base.OnEnabledChanged(e);
			SetupApearence();
		}
	}
}
