﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Palette
{
	public partial class GBPaletteSetSelector : UserControl
	{
		#region Private inner classes
		/// <summary>
		/// Internal class which represents an individual entry.
		/// </summary>
		private class PalatteEntry : Label
		{
			/// <summary>
			/// Width/height of the individual control.
			/// </summary>
			private const int WIDTH = 19, HEIGHT = 19;

			/// <summary>
			/// Initial offset for each control (at no aditional offset)
			/// </summary>
			private const int X_OFFSET = 16 + 20, Y_OFFSET = 19;

			/// <summary>
			/// The spacing between each control.
			/// </summary>
			private const int X_SPACING = WIDTH, Y_SPACING = HEIGHT + 9;

			public readonly int x, y;
			private GBPaletteSetSelector selector;

			public Color Color {
				get { return this.BackColor; }
				set { if (value == null) { throw new ArgumentNullException(); } this.BackColor = value; onColorChange(); }
			}

			public PalatteEntry(GBPaletteSetSelector selector, int x, int y) {
				this.x = x;
				this.y = y;

				this.selector = selector;

				this.Text = x.ToString();
				this.Name = "entry_x" + x + "_y" + y;

				this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

				this.Size = new System.Drawing.Size(WIDTH, HEIGHT);
				this.Location = new System.Drawing.Point(X_OFFSET + (x * X_SPACING), Y_OFFSET + (y * Y_SPACING));

				this.TabIndex = (y * 4) + x;

				this.Color = selector.defaultColorScheme[x];

				this.Paint += new PaintEventHandler(PalatteEntry_Paint);
				this.MouseDown += new MouseEventHandler(PalatteEntry_MouseDown);
			}

			/// <summary>
			/// Paints an individual entry.
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			internal void PalatteEntry_Paint(object sender, PaintEventArgs e) {
				if (sender is PalatteEntry) {
					e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

					PalatteEntry c = (PalatteEntry)sender;

					//Draw the main border.
					ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, c.Width, c.Height),
						Color.Black, ButtonBorderStyle.Solid);
					//If selected draw the inner border.
					if (selector.selectedX == c.x && selector.selectedY == c.y) {
						ControlPaint.DrawBorder(e.Graphics, new Rectangle(1, 1, c.Width - 2, c.Height - 2),
							SystemColors.Highlight, ButtonBorderStyle.Solid);
					}
				}
			}

			internal void PalatteEntry_MouseDown(object sender, MouseEventArgs e) {
				if (sender is PalatteEntry) {
					PalatteEntry entry = (PalatteEntry)sender;

					selector.SelectedX = entry.x;
					selector.SelectedY = entry.y;

					selector.Refresh();
				}
			}

			internal void onColorChange() {
				//Changes text color.
				if (((Color.R < 0x40) && (Color.G < 0x40)) ||
						((Color.G < 0x40) && (Color.B < 0x40)) ||
						((Color.R < 0x40) && (Color.B < 0x40))) {
					this.ForeColor = Color.White;
				} else {
					this.ForeColor = Color.Black;
				}
			}

			public override string ToString() {
				return base.ToString() + " @ x" + x + " y" + y;
			}
		}
		#endregion

		#region Constants
		/// <summary>
		/// Total number of columns.
		/// </summary>
		public const int COLUMNS_MAX = 4;
		/// <summary>
		/// Total number of rows.
		/// </summary>
		public const int ROWS_MAX = 8;
		#endregion

		#region Private fields

		private PalatteEntry[,] entries = new PalatteEntry[ROWS_MAX, COLUMNS_MAX];
		private Label[] labels = new Label[ROWS_MAX];
		
		#region Property clones
		/// <summary>
		/// The currently selected coordinates.  If -1 nothing is selected.
		/// </summary>
		private int selectedX = -1, selectedY = -1;

		private int rows = 8;

		/// <summary>
		/// The default grayscale GB color shceme.
		/// </summary>
		private Color[] defaultColorScheme = {
										  Color.FromArgb(255, 255, 255),
										  Color.FromArgb(192, 192, 192),
										  Color.FromArgb(144, 144, 144),
										  Color.FromArgb(0, 0, 0)
									  };

		//Use the GBC filter?
		private bool FGBCFilter = false;

		#endregion
		#endregion

		#region Public properties
		/// <summary>
		/// The x-location currently selected.  -1 if none selected.
		/// </summary>
		[Description("The x-location currently selected.  -1 if none selected."), Category("Data")]
		public int SelectedX {
			get { return selectedX; }
			set {
				if ((value >= COLUMNS_MAX) || (value < -1)) {
					throw new ArgumentOutOfRangeException("SelectedX", "Value must be between 0 and " + (COLUMNS_MAX - 1) +  " inclusive, or -1 to represent no selection.");
				}
				selectedX = value;
				onSelectionChange();
			}
		}

		/// <summary>
		/// The x-location currently selected.  -1 if none selected.
		/// </summary>
		[Description("The x-location currently selected.  -1 if none selected."), Category("Data")]
		public int SelectedY {
			get { return selectedY; }
			set {
				if ((value >= ROWS_MAX) || (value < -1)) {
					throw new ArgumentOutOfRangeException("SelectedY", "Value must be between 0 and " + (ROWS_MAX - 1) + " inclusive, or -1 to represent no selection.");
				}
				selectedY = value;
				onSelectionChange();
			}
		}

		/// <summary>
		/// The number of rows to include.
		/// </summary>
		[Description("The number of rows to include."), Category("Data")]
		public int Rows {
			get { return rows; }
			set {
				if ((value > ROWS_MAX) || (value < 1)) {
					throw new ArgumentOutOfRangeException("Rows", "Value must be between 1 and " + (ROWS_MAX) + " inclusive.");
				}
				rows = value;

				hideUnusedControls();
			}
		}

		/// <summary>
		/// The color of the currently selected cell.  If none are selected, it is illegal to try and access it; it is also illegal to set to null.
		/// </summary>
		[Description("The currently-selected color."), Category("Data"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SelectedColor {
			get {
				if (selectedX == -1 || selectedY == -1) {
					return Color.Black;
				}
				return entries[selectedX, selectedY].Color;
			}
			set {
				if (selectedX == -1 || selectedY == -1) {
					throw new InvalidOperationException("Cannot set the selected color when there is no selection!");
				}
				if (value == null) {
					throw new ArgumentNullException();
				}
				entries[selectedX, selectedY].Color = value;
			}
		}

		/// <summary>
		/// The colors of all entries.
		/// </summary>
		[Description("The colors of all entries."), Category("Data"), Browsable(false)]
		public Color[,] Colors {
			get {
				Color[,] temp = new Color[COLUMNS_MAX,rows];
				foreach (PalatteEntry e in entries) {
					if (e.y >= rows) {
						continue;
					}
					temp[e.x, e.y] = e.Color;
				}
				return temp;
			}
		}

		/// <summary>
		/// The color used for the first entry by default.
		/// </summary>
		[Description("The color used for the first entry by default."), Category("Defaults")]
		public Color WhiteColor {
			get { return defaultColorScheme[0]; }
			set { if (value == null) { throw new ArgumentNullException(); } defaultColorScheme[0] = value; }
		}

		/// <summary>
		/// The color used for the second entry by default.
		/// </summary>
		[Description("The color used for the second entry by default."), Category("Defaults")]
		public Color LightGrayColor {
			get { return defaultColorScheme[1]; }
			set { if (value == null) { throw new ArgumentNullException(); } defaultColorScheme[1] = value; }
		}

		/// <summary>
		/// The color used for the third entry by default.
		/// </summary>
		[Description("The color used for the third entry by default."), Category("Defaults")]
		public Color DarkGrayColor {
			get { return defaultColorScheme[2]; }
			set { if (value == null) { throw new ArgumentNullException(); } defaultColorScheme[2] = value; }
		}

		/// <summary>
		/// The color used for the fourth entry by default.
		/// </summary>
		[Description("The color used for the fourth entry by default."), Category("Defaults")]
		public Color BlackColor {
			get { return defaultColorScheme[3]; }
			set { if (value == null) { throw new ArgumentNullException(); } defaultColorScheme[3] = value; }
		}

		/// <summary>
		/// Use the GBC Filter?
		/// 
		/// Declared line 123:
		///      property GBCFilter : boolean read FGBCFilter write SetGBCFilter;
		/// </summary>
		[Description("Use the regular GBC filter, rather than the regular colors.  GBC colors are paler."), Category("Data")]
		public bool GBCFilter {
			get { return FGBCFilter; }
			set { FGBCFilter = value; OnUseGBCFilterChange(); }
		}
		#endregion

		#region Events
		/// <summary>
		/// Event handler for when the slection is changed.
		/// </summary>
		[Category("Action"), Description("Fires when the selection is changed")]
		public event EventHandler SelectionChanged;

		/// <summary>
		/// Event handler for when the use of the GBC filter is toggled.
		/// </summary>
		[Category("Property Changed"), Description("Fires when the use of the GBC filter is toggled.")]
		public event EventHandler UseGBCFilterChanged;

		protected void onSelectionChange() {
			if (SelectionChanged != null) {
				SelectionChanged(this, new EventArgs());
			}
		}

		protected void OnUseGBCFilterChange() {
			if (UseGBCFilterChanged != null) {
				UseGBCFilterChanged(this, new EventArgs());
			}
			this.Refresh();
		}
		#endregion

		public GBPaletteSetSelector() {
			InitializeComponent();
			addControls();
		}

		internal void addControls() {
			//Entries
			entries = new PalatteEntry[COLUMNS_MAX, ROWS_MAX];
			for (int y = 0; y < ROWS_MAX; y++) {
				for (int x = 0; x < COLUMNS_MAX; x++) {
					entries[x, y] = new PalatteEntry(this, x, y);
					this.Controls.Add(entries[x, y]);
				}
			}
			//Informational labels.
			for (int y = 0; y < ROWS_MAX; y++) {
				Label l = new Label();
				l.Name = "label_y" + y;
				l.Text = y.ToString();

				l.Location = new Point(16, 23 + (y * 28));

				l.SendToBack();

				Controls.Add(l);
				labels[y] = l;
			}
		}

		/// <summary>
		/// Sets whether unused controls are hidden.
		/// </summary>
		internal void hideUnusedControls() {
			for (int y = 0; y < ROWS_MAX; y++) {
				labels[y].Visible = (y < rows);
				for (int x = 0; x < COLUMNS_MAX; x++) {
					entries[x, y].Visible = (y < rows);
				}
			}
		}
	}
}
