using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBRenderer
{
	/// <summary>
	/// ROUGHLY Based off of GBCOMP-Src\GBColorSetSelector.pas.
	/// </summary>
	public partial class GBColorSetSelector : UserControl
	{
		#region Private fields
		#region Property clones
		private int entryCount = 4;

		private int entryWidth = 19;
		private int entryHeight = 19;

		private int selectedIndex = 0;
		#endregion

		/// <summary>
		/// The entries currently used.
		/// </summary>
		private Label[] entries;

		/// <summary>
		/// The control currently selected.
		/// </summary>
		private Control selectedControl;
		#endregion

		#region Public properties
		/// <summary>
		/// The number of entries.
		/// </summary>
		[Description("The number of entries to use."), Category("Layout")]
		public int EntryCount {
			get { return entryCount; }
			set {
				if (value < 1) { throw new ArgumentOutOfRangeException(); } else {
					entryCount = value;

					selectedIndex = 0;

					this.Width = entryWidth * entryCount;
					this.Height = entryHeight;

					this.MinimumSize = this.Size;
					this.MaximumSize = this.Size;
				}
			}
		}

		/// <summary>
		/// The width of a single entry.
		/// </summary>
		[Description("The width of a single entry."), Category("Layout")]
		public int EntryWidth {
			get { return entryWidth; }
			set {
				if (value < 1) { throw new ArgumentOutOfRangeException(); } else {
					entryWidth = value;
					this.Width = entryWidth * entryCount;

					this.MinimumSize = this.Size;
					this.MaximumSize = this.Size;
				}
			}
		}

		/// <summary>
		/// The height of a single entry.
		/// </summary>
		[Description("The height of a single entry."), Category("Layout")]
		public int EntryHeight {
			get { return entryHeight; }
			set {
				if (value < 1) { throw new ArgumentOutOfRangeException(); } else {
					entryHeight = value;
					this.Height = entryHeight;

					this.MinimumSize = this.Size;
					this.MaximumSize = this.Size;
				}
			}
		}

		/// <summary>
		/// The currently-selected index.
		/// </summary>
		[Description("The currently selected index."), Category("Data")]
		public int SelectedIndex {
			get { return selectedIndex; }
			set { selectedIndex = value; }
		}

		/// <summary>
		/// The colors on this control.
		/// </summary>
		[Description("The colors currently in use."), Category("Data")]
		public Color[] Colors {
			get {
				Color[] temp = new Color[entries.Length];
				for (int i = 0; i < entries.Length; i++) {
					temp[i] = entries[i].BackColor;
				}
				return temp;
			}
		}

		/// <summary>
		/// The color of the currently-selected entry, or gray if not set.
		/// </summary>
		[Description("The color of the currently-selected entry."), Category("Data")]
		public Color CurrentColor {
			get { return selectedControl != null ? selectedControl.BackColor : Color.Gray; }
			set { if (selectedControl != null) { selectedControl.BackColor = value; OnCurrentColorChanged(); } }
		}
		#endregion

		#region Public Events
		/// <summary>
		/// Event handler for when this is changed.
		/// </summary>
		[Category("Action"), Description("Fires when the currently selected color is changed")]
		public event EventHandler CurrentColorChanged;

		protected virtual void OnCurrentColorChanged() {
			// Preform color updatings.
			if (selectedControl != null) {
				selectedControl.BackColor = CurrentColor;

				//Brightness.
				if (((selectedControl.BackColor.R < 0x40) && (selectedControl.BackColor.G < 0x40)) ||
						((selectedControl.BackColor.G < 0x40) && (selectedControl.BackColor.B < 0x40)) ||
						((selectedControl.BackColor.R < 0x40) && (selectedControl.BackColor.B < 0x40))) {
					selectedControl.ForeColor = Color.White;
				} else {
					selectedControl.ForeColor = Color.Black;
				}

				selectedControl.Refresh();
			}

			// Actually raise the event
			if (CurrentColorChanged != null) {
				CurrentColorChanged(this, new EventArgs());
			}
		}
		#endregion

		public GBColorSetSelector() {
			InitializeComponent();
		}

		private void GBColorSetSelector_Load(object sender, EventArgs e) {
			addAllEntries();
		}

		private void addAllEntries() {
			this.Controls.Clear();

			entries = new Label[entryCount];

			for (int i = 0; i < entryCount; i++) {
				Label l = new Label();
				l.Location = new System.Drawing.Point(entryWidth * i, 0);
				l.Name = i.ToString();
				l.Size = new System.Drawing.Size(entryWidth, entryHeight);
				l.TabIndex = i;
				l.Text = i.ToString();
				l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
				l.Paint += new System.Windows.Forms.PaintEventHandler(this.entryPaint);
				l.MouseDown += new System.Windows.Forms.MouseEventHandler(this.entryMouseDown);

				entries[i] = l;
				this.Controls.Add(l);
			}
		}

		/// <summary>
		/// Paints an individual entry.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal void entryPaint(object sender, PaintEventArgs e) {
			if (sender is Label) {
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

				Label c = (Label)sender;

				//Draw the main border.
				ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, c.Width, c.Height),
					Color.Black, ButtonBorderStyle.Solid);
				//If selected draw the inner border.
				if (Object.ReferenceEquals(selectedControl, c)) {
					ControlPaint.DrawBorder(e.Graphics, new Rectangle(1, 1, c.Width - 2, c.Height - 2),
						SystemColors.Highlight, ButtonBorderStyle.Solid);
				}
			}
		}

		internal void entryMouseDown(object sender, MouseEventArgs e) {
			if (sender is Control) {
				Control c = (Control)sender;
				selectedControl = c;
				for (int i = 0; i < entries.Length; i++) {
					if (Object.ReferenceEquals(selectedControl, entries[i])) {
						selectedIndex = i;
						break;
					}
				}
				OnCurrentColorChanged();
				//TODO: Update with an event.
			}
			this.Refresh();
		}
	}
}
