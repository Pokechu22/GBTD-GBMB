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
	public abstract partial class GBTDPaletteChooser<TSet, TRow, TEntry> : UserControl
		where TSet : PaletteSetBase<TRow, TEntry>, new()
		where TRow : PaletteBase<TEntry>
		where TEntry: PaletteEntryBase
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
				private GBTDPaletteChooser<TSet, TRow, TEntry> chooser;

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

				public GBTDPaletteChooserMouseEntryPaletteEntry(MouseButtons buttons, GBTDPaletteChooser<TSet, TRow, TEntry> chooser)
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

			private TRow item = default(TRow);
			public TRow Item {
				get { return item; }
				set { item = value; OnValueChange(); }
			}

			private int index = 0;
			public int Index {
				get { return entry.Index; }
				set { index = value; OnValueChange(); }
			}

			private void OnValueChange() {
				entry.Color = item[index];
				entry.Index = index;
				this.Refresh();
			}

			public GBTDPaletteChooserMouseEntry(MouseButtons buttons, GBTDPaletteChooser<TSet, TRow, TEntry> chooser)
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

		private TSet set = new TSet();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Data"), Description("The used set.")]
		public TSet Set {
			get { set = this.gbPaletteChooser1.Set; return this.set; }
			set { if (value == null) { throw new ArgumentNullException(); } this.set = value; this.gbPaletteChooser1.Set = set; this.Refresh(); }
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

		protected GBTDPaletteChooser() {
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

		private void gbPaletteChooser1_SelectedPaletteChanged(object sender, GBPaletteChooser<TSet, TRow, TEntry>.SelectedPaletteChangeEventArgs e) {
			this.mouseButtonL.Item = e.newItem;
			this.mouseButtonR.Item = e.newItem;
			this.mouseButtonM.Item = e.newItem;
			this.mouseButtonX1.Item = e.newItem;
			this.mouseButtonX2.Item = e.newItem;
		}

		private void gbPaletteChooser1_PaletteEntryClicked(object sender, GBPaletteChooser<TSet, TRow, TEntry>.PaletteEntryClickEventArgs e) {
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
		}
	}

	public class GBTDGBCPaletteChooser : GBTDPaletteChooser<GBCPaletteSet, GBCPalette, GBCPaletteEntry> { }
}
