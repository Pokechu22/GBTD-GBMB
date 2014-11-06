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
	public partial class GBPaletteSetSelector : UserControl
	{
		#region Private inner classes
		private sealed class PaletteSetEntry : PaletteEntry
		{
			internal GBPaletteSetSelector selector;

			public PaletteSetEntry(GBPaletteSetSelector selector, int x, int y) : base(x, y) {
				this.selector = selector;
			}

			protected override void SetSelected() {
				selector.SelectedX = this.x;
				selector.SelectedY = this.y;
			}

			protected override bool IsSelected() {
				return (selector.SelectedX == this.x && selector.SelectedY == this.y);
			}

			protected override bool UseGBCFilter {
				get {
					return selector.GBCFilter;
				}
				set {
					selector.GBCFilter = value;
				}
			}

			protected override Color GetDefaultColor() {
				return selector.defaultColorScheme[this.x];
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

		private PaletteSetEntry[,] entries = new PaletteSetEntry[ROWS_MAX, COLUMNS_MAX];
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
				foreach (PaletteSetEntry e in entries) {
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
			this.Refresh();
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
			entries = new PaletteSetEntry[COLUMNS_MAX, ROWS_MAX];
			for (int y = 0; y < ROWS_MAX; y++) {
				for (int x = 0; x < COLUMNS_MAX; x++) {
					entries[x, y] = new PaletteSetEntry(this, x, y);
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
