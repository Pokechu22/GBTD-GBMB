using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Tile;

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
						return 15;
					}
				}

				protected override int WIDTH {
					get {
						return 20;
					}
				}

				protected override int HEIGHT {
					get {
						return 20;
					}
				}

				private int index = 0;
				public int Index {
					get { return index; }
					set { index = value; this.Text = value.ToString(); this.Refresh(); }
				}

				public GBTDPaletteChooserMouseEntryPaletteEntry(MouseButtons buttons, GBTDPaletteChooser chooser)
					: base(0, 0) {
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
			private MouseButtons buttons;

			private Palette_ item = default(Palette_);
			public Palette_ Item {
				get { return item; }
				set { item = value; OnValueChange(); }
			}

			private int index = 0;
			public int Index {
				get { return entry.Index; }
				set { index = value; OnValueChange(); }
			}

			public GBColor MatchingGBColor {
				get {
					switch (index) {
					case 0: return GBColor.WHITE;
					case 1: return GBColor.LIGHT_GRAY;
					case 2: return GBColor.DARK_GRAY;
					case 3: return GBColor.BLACK;
					default: throw new InvalidEnumArgumentException();
					}
				}
				set {
					switch (value) {
					case GBColor.WHITE: index = 0; return;
					case GBColor.LIGHT_GRAY: index = 1; return;
					case GBColor.DARK_GRAY: index = 2; return;
					case GBColor.BLACK: index = 3; return;
					default: throw new InvalidEnumArgumentException();
					}
				}
			}

			private void OnValueChange() {
				entry.Color = item[index];
				entry.Index = index;
				this.Refresh();
			}

			public GBTDPaletteChooserMouseEntry(MouseButtons buttons, GBTDPaletteChooser chooser)
				: base() {
				String identifier;

				this.buttons = buttons;

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

				this.Item = chooser.Set[0];

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

		[Category("Display"), Description("Use the GBC filter?")]
		public bool UseGBCFilter {
			get { return useGBCFilter; }
			set { useGBCFilter = value; this.Refresh(); }
		}

		private PaletteSet_ set = new PaletteSet_();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Data"), Description("The used set.")]
		public PaletteSet_ Set {
			get { set = this.gbPaletteChooser1.Set; return this.set; }
			set { this.set = value; this.gbPaletteChooser1.Set = set; this.Refresh(); }
		}

		[Category("Data"), Description("The row currently used.")]
		public int SelectedRow {
			get { return this.gbPaletteChooser1.SelectedRowIndex; }
			set { this.gbPaletteChooser1.SelectedRowIndex = value; }
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

		[Category("Value"), Description("The color used by the left mouse button.")]
		public GBColor LeftMouseColor {
			get { return mouseButtonL.MatchingGBColor; }
			set { mouseButtonL.MatchingGBColor = value; }
		}
		[Category("Value"), Description("The color used by the right mouse button.")]
		public GBColor RightMouseColor {
			get { return mouseButtonR.MatchingGBColor; }
			set { mouseButtonR.MatchingGBColor = value; }
		}
		[Category("Value"), Description("The color used by the middle mouse button.")]
		public GBColor MiddleMouseColor {
			get { return mouseButtonM.MatchingGBColor; }
			set { mouseButtonM.MatchingGBColor = value; }
		}
		[Category("Value"), Description("The color used by the X1 mouse button.")]
		public GBColor X1MouseColor {
			get { return mouseButtonX1.MatchingGBColor; }
			set { mouseButtonX1.MatchingGBColor = value; }
		}
		[Category("Value"), Description("The color used by the X2 mouse button.")]
		public GBColor X2MouseColor {
			get { return mouseButtonX2.MatchingGBColor; }
			set { mouseButtonX2.MatchingGBColor = value; }
		}

		protected void OnButtonsChange(MouseButtons change) {
			//Values for individual controls
			//Initial value of each coord
			const int INITIAL_X = 2, INITIAL_Y = 2;
			//Change applied to each coord
			const int X_OFFSET = 37, Y_OFFSET = 0;
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

			this.MaximumSize = new Size(115 + x, 26);
			this.MinimumSize = new Size(115 + x, 26);
			this.Size = new Size(115 + x, 26);

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

		private void gbPaletteChooser1_SelectedPaletteChanged(object sender, GBPaletteChooser.SelectedPaletteChangeEventArgs e) {
			this.mouseButtonL.Item = e.newItem;
			this.mouseButtonR.Item = e.newItem;
			this.mouseButtonM.Item = e.newItem;
			this.mouseButtonX1.Item = e.newItem;
			this.mouseButtonX2.Item = e.newItem;

			if (SelectedPaletteChanged != null) {
				SelectedPaletteChanged(this, new EventArgs());
			}
		}

		private void gbPaletteChooser1_PaletteEntryClicked(object sender, GBPaletteChooser.PaletteEntryClickEventArgs e) {
			if (e.button.HasFlag(System.Windows.Forms.MouseButtons.Left)) {
				this.mouseButtonL.Index = e.clickedEntry;
			}
			if (e.button.HasFlag(System.Windows.Forms.MouseButtons.Right)) {
				this.mouseButtonR.Index = e.clickedEntry;
			}
			if (e.button.HasFlag(System.Windows.Forms.MouseButtons.Middle)) {
				this.mouseButtonM.Index = e.clickedEntry;
			}
			if (e.button.HasFlag(System.Windows.Forms.MouseButtons.XButton1)) {
				this.mouseButtonX1.Index = e.clickedEntry;
			}
			if (e.button.HasFlag(System.Windows.Forms.MouseButtons.XButton2)) {
				this.mouseButtonX2.Index = e.clickedEntry;
			}

			if (MouseButtonColorChanged != null) {
				MouseButtonColorChanged(this, new EventArgs());
			}
		}

		/// <summary>
		/// Triggers when the selected palette is changed.
		/// </summary>
		[Category("Data"), Description("Triggers when the selected palette is changed.")]
		public event EventHandler SelectedPaletteChanged;

		/// <summary>
		/// Triggers when one of the mouse button effects is changed, even ones that aren't visible.
		/// By ones that aren't visible, I mean XButton1, ect.
		/// </summary>
		[Category("Data"), Description("Triggers when one of the mouse button effects is changed, even ones that aren't visible.")]
		public event EventHandler MouseButtonColorChanged;
	}

	//public class GBTDGBCPaletteChooser : GBTDPaletteChooser<GBCPaletteSet, GBCPalette, GBCPaletteEntry> { }
}
