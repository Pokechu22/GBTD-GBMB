using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Palettes
{
	/// <summary>
	/// Control that edits a Palettes Set.
	/// </summary>
	public partial class GBPaletteSetSelector : UserControl
	{
		protected override Size DefaultSize { get { return new Size(96, 216); } }
		protected override Size DefaultMaximumSize { get { return new Size(96, 216); } }
		protected override Size DefaultMinimumSize { get { return new Size(96, 216); } }

		protected override Padding DefaultMargin { get { return new Padding(16, 19, 3, 3);} }

		#region Private inner classes
		private sealed class PaletteSetEntry : PaletteEntryRenderer
		{
			internal GBPaletteSetSelector selector;

			protected override int X_OFFSET { get { return 20; } }
			protected override int Y_OFFSET { get { return 0; } }

			public PaletteSetEntry(GBPaletteSetSelector selector, int x, int y)
				: base(x, y) {
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

		#region Private fields

		private PaletteSetEntry[,] entries = null;
		private Label[] labels = null;
		
		#region Property clones
		/// <summary>
		/// The currently selected coordinates.  If -1 nothing is selected.
		/// </summary>
		private int selectedX = -1, selectedY = -1;

		//Use the GBC filter?
		private bool FGBCFilter = false;

		/// <summary>
		/// The default grayscale GB color shceme.
		/// </summary>
		private Color[] defaultColorScheme = {
										  Color.FromArgb(255, 255, 255),
										  Color.FromArgb(192, 192, 192),
										  Color.FromArgb(144, 144, 144),
										  Color.FromArgb(0, 0, 0)
									  };

		private PaletteSet set = new PaletteSet(new Palette[8], new GBCPaletteSetBehavior());
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
				if ((value >= 4) || (value < -1)) {
					throw new ArgumentOutOfRangeException("SelectedX", "Value must be between 0 and " + (4 - 1) +  " inclusive, or -1 to represent no selection.");
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
				if ((value >= set.NumberOfRows) || (value < -1)) {
					throw new ArgumentOutOfRangeException("SelectedY", "Value must be between 0 and " + (set.NumberOfRows - 1) + " inclusive, or -1 to represent no selection.");
				}
				selectedY = value;
				onSelectionChange();
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
		/// The palette set used for this.
		/// </summary>
		[Description("The palette set used for this."), Category("Data")]
		public PaletteSet Set {
			get { freshenToSet(); return set; }
			set { set = value; freshenFromSet(); }
		}

		/// <summary>
		/// The color used for the first entry by default.
		/// </summary>
		[Description("The color used for the first entry by default."), Category("Defaults")]
		public Color DefaultWhiteColor {
			get { return defaultColorScheme[0]; }
			set { if (value == null) { throw new ArgumentNullException(); } defaultColorScheme[0] = value; }
		}

		/// <summary>
		/// The color used for the second entry by default.
		/// </summary>
		[Description("The color used for the second entry by default."), Category("Defaults")]
		public Color DefaultLightGrayColor {
			get { return defaultColorScheme[1]; }
			set { if (value == null) { throw new ArgumentNullException(); } defaultColorScheme[1] = value; }
		}

		/// <summary>
		/// The color used for the third entry by default.
		/// </summary>
		[Description("The color used for the third entry by default."), Category("Defaults")]
		public Color DefaultDarkGrayColor {
			get { return defaultColorScheme[2]; }
			set { if (value == null) { throw new ArgumentNullException(); } defaultColorScheme[2] = value; }
		}

		/// <summary>
		/// The color used for the fourth entry by default.
		/// </summary>
		[Description("The color used for the fourth entry by default."), Category("Defaults")]
		public Color DefaultBlackColor {
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
			this.Invalidate(true);
		}

		protected void OnUseGBCFilterChange() {
			if (UseGBCFilterChanged != null) {
				UseGBCFilterChanged(this, new EventArgs());
			}
			this.Invalidate(true);
		}
		#endregion

		public GBPaletteSetSelector() {
			InitializeComponent();
			addControls();
		}

		private void addControls() {
			//Entries
			entries = new PaletteSetEntry[4, set.NumberOfRows];
			for (int y = 0; y < set.NumberOfRows; y++) {
				for (int x = 0; x < 4; x++) {
					entries[x, y] = new PaletteSetEntry(this, x, y);
					this.Controls.Add(entries[x, y]);
				}
			}

			//Informational labels.
			labels = new Label[set.NumberOfRows];
			for (int y = 0; y < set.NumberOfRows; y++) {
				Label l = new Label();
				l.Name = "label_y" + y;
				l.Text = y.ToString();

				l.Location = new Point(0, 4 + (y * 28));

				l.SendToBack();

				Controls.Add(l);
				labels[y] = l;
			}
		}

		/// <summary>
		/// Updates the PaletteSet used with the current data.
		/// </summary>
		protected virtual void freshenToSet() {
			foreach (PaletteSetEntry e in this.entries) {
				set.Rows[e.y][e.x] = new PaletteEntry(e.x, e.y, e.Color, set.Rows[e.y][e.x].behavior);
			}
		}

		/// <summary>
		/// Updates the controls here with those from the PaletteSet.
		/// </summary>
		protected virtual void freshenFromSet() {
			for (int row = 0; row < set.NumberOfRows; row++) {
				for (int x = 0; x < 4; x++) {
					entries[x, row].Color = set[row][x];
				}
			}


		}
	}
}
