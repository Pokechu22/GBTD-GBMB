using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Palette
{
	public partial class GBTDPaletteChooser : UserControl
	{
		/// <summary>
		/// Mouse entry used to contain one of the colors for a mouse button.
		/// </summary>
		private class GBTDPaletteChooserMouseEntry : Panel
		{
			/// <summary>
			/// And a palette entry for the inside of that.
			/// </summary>
			private class GBTDPaletteChooserMouseEntryPaletteEntry : PaletteEntry
			{
				private GBTDPaletteChooser chooser;

				protected override int Y_OFFSET {
					get {
						return 1;
					}
				}

				protected override int X_OFFSET {
					get {
						return 16;
					}
				}

				protected override int WIDTH {
					get {
						return 19;
					}
				}

				protected override int HEIGHT {
					get {
						return 19;
					}
				}

				public GBTDPaletteChooserMouseEntryPaletteEntry(MouseButtons buttons, GBTDPaletteChooser chooser) : base(0, 0) {
					this.chooser = chooser;
				}

				protected override void SetSelected() {
					//N/A
				}

				protected override bool IsSelected() {
					return false; //N/A
				}

				protected override bool UseGBCFilter {
					get {
						return chooser.UseGBCFilter;
					}
					set {
						chooser.UseGBCFilter = value;
						this.Refresh();
					}
				}

				protected override Color GetDefaultColor() {
					return Color.Black;
				}
			}

			protected override Size DefaultMinimumSize {
				get {
					return new Size(36, 22);
				}
			}

			protected override Size DefaultMaximumSize {
				get {
					return new Size(36, 22);
				}
			}

			protected override Size DefaultSize {
				get {
					return new Size(36, 22);
				}
			}

			private GBTDPaletteChooserMouseEntryPaletteEntry entry;

			public GBTDPaletteChooserMouseEntry(MouseButtons buttons, GBTDPaletteChooser chooser) : base() {
				String identifier;

				switch (buttons) {
				case System.Windows.Forms.MouseButtons.Left: identifier = "L"; break;
				case System.Windows.Forms.MouseButtons.Right: identifier = "R"; break;
				case System.Windows.Forms.MouseButtons.Middle: identifier = "M"; break;
				case System.Windows.Forms.MouseButtons.XButton1: identifier = "X1"; break;
				case System.Windows.Forms.MouseButtons.XButton2: identifier = "X2"; break;
				default: throw new InvalidOperationException("Illegal MouseButtons provided: Must only be one; got " + buttons + " (" + ((int)buttons) + ")");
				}

				this.Name = "mouseDisplay_" + identifier;

				this.SuspendLayout();

				this.Paint += new PaintEventHandler(paintBorder);

				Label label = new Label();
				label.Name = "label_" + this.Name;
				label.Text = identifier;
				label.Location = new Point(2, 5);
				label.BackColor = Color.FromArgb(0, 0, 0, 0);

				this.entry = new GBTDPaletteChooserMouseEntryPaletteEntry(buttons, chooser);

				this.Controls.Add(label);
				this.Controls.Add(entry);

				label.SendToBack();

				this.ResumeLayout();
			}

			private void paintBorder(object sender, PaintEventArgs e) {
				ControlPaint.DrawBorder3D(e.Graphics, 0, 0, ((Control)sender).Width, ((Control)sender).Height, Border3DStyle.SunkenOuter);
			}
		}

		private bool useGBCFilter = false;

		public bool UseGBCFilter {
			get { return useGBCFilter; }
			set { useGBCFilter = value; this.Refresh(); }
		}
		protected override Size DefaultMaximumSize {
			get {
				return new Size(191, 26);
			}
		}

		protected override Size DefaultMinimumSize {
			get {
				return new Size(191, 26);
			}
		}

		protected override Size DefaultSize {
			get {
				return new Size(191, 26);
			}
		}

		private MouseButtons displayedButtons = System.Windows.Forms.MouseButtons.Left | System.Windows.Forms.MouseButtons.Right;

		private GBTDPaletteChooserMouseEntry mouseButtonL, mouseButtonR, mouseButtonM, mouseButtonX1, mouseButtonX2;

		[Category("Display"), Description("Controls which buttons are displayed.")]
		public MouseButtons DisplayedButtons {
			get { return displayedButtons; }
			set { displayedButtons = value; OnButtonsChange(value); }
		}

		protected void OnButtonsChange(MouseButtons change) {
			//Values for individual controls
			//Initial value of each coord
			const int INITIAL_X = 2, INITIAL_Y = 2;
			//Change applied to each coord
			const int X_OFFSET = 38, Y_OFFSET = 0;
			//Current coords
			int x = INITIAL_X, y = INITIAL_Y;

			if (change.HasFlag(System.Windows.Forms.MouseButtons.Left)) {
				mouseButtonL.Visible = mouseButtonL.Enabled = true;
				mouseButtonL.Location = new Point(x, y);

				x += X_OFFSET; y += Y_OFFSET;
			} else {
				mouseButtonL.Visible = mouseButtonL.Enabled = false;
			}
			if (change.HasFlag(System.Windows.Forms.MouseButtons.Right)) {
				mouseButtonR.Visible = mouseButtonR.Enabled = true;
				mouseButtonR.Location = new Point(x, y);

				x += X_OFFSET; y += Y_OFFSET;
			} else {
				mouseButtonR.Visible = mouseButtonR.Enabled = false;
			}
			if (change.HasFlag(System.Windows.Forms.MouseButtons.Middle)) {
				mouseButtonM.Visible = mouseButtonM.Enabled = true;
				mouseButtonM.Location = new Point(x, y);

				x += X_OFFSET; y += Y_OFFSET;
			} else {
				mouseButtonM.Visible = mouseButtonM.Enabled = false;
			}
			if (change.HasFlag(System.Windows.Forms.MouseButtons.XButton1)) {
				mouseButtonX1.Visible = mouseButtonX1.Enabled = true;
				mouseButtonX1.Location = new Point(x, y);

				x += X_OFFSET; y += Y_OFFSET;
			} else {
				mouseButtonX1.Visible = mouseButtonX1.Enabled = false;
			}
			if (change.HasFlag(System.Windows.Forms.MouseButtons.XButton2)) {
				mouseButtonX2.Visible = mouseButtonX2.Enabled = true;
				mouseButtonX2.Location = new Point(x, y);

				x += X_OFFSET; y += Y_OFFSET;
			} else {
				mouseButtonX2.Visible = mouseButtonX2.Enabled = false;
			}

			this.MaximumSize = new Size(113 + x, 26);
			this.MinimumSize = new Size(113 + x, 26);
			this.Size = new Size(113 + x, 26);

			this.Refresh();
		}

		public GBTDPaletteChooser() {
			InitializeComponent();

			this.SuspendLayout();

			mouseButtonL = new GBTDPaletteChooserMouseEntry(MouseButtons.Left, this);
			mouseButtonR = new GBTDPaletteChooserMouseEntry(MouseButtons.Right, this);
			mouseButtonM = new GBTDPaletteChooserMouseEntry(MouseButtons.Middle, this);
			mouseButtonX1 = new GBTDPaletteChooserMouseEntry(MouseButtons.XButton1, this);
			mouseButtonX2 = new GBTDPaletteChooserMouseEntry(MouseButtons.XButton2, this);

			mouseButtonL.Visible = mouseButtonL.Enabled = false;
			mouseButtonR.Visible = mouseButtonR.Enabled = false;
			mouseButtonM.Visible = mouseButtonM.Enabled = false;
			mouseButtonX1.Visible = mouseButtonX1.Enabled = false;
			mouseButtonX2.Visible = mouseButtonX2.Enabled = false;

			this.Controls.Add(mouseButtonL);
			this.Controls.Add(mouseButtonR);
			this.Controls.Add(mouseButtonM);
			this.Controls.Add(mouseButtonX1);
			this.Controls.Add(mouseButtonX2);
			this.ResumeLayout();

			this.OnButtonsChange(this.displayedButtons);
		}

		private void GBTDPaletteChooser_Paint(object sender, PaintEventArgs e) {
			//Paints a border.
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.Width, this.Height, Border3DStyle.RaisedInner);
		}
	}
}
