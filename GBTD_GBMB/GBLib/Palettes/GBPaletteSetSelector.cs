using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Tiles;

namespace GB.Shared.Palettes
{
	/// <summary>
	/// Control that edits a Palettes ActiveSet.
	/// </summary>
	public partial class GBPaletteSetSelector : Control
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
					return selector.selectedSet.IsFiltered();
				}
				set {
					//Do nothing.
				}
			}

			protected override Color GetDefaultColor() {
				return ((GBColor)this.x).GetPocketColor();
			}
		}
		#endregion

		#region Private fields

		private PaletteSetEntry[,] entries = null;
		private Label[] labels = null;

		/// <summary>
		/// The set that is actively in use.
		/// This can only be used internally because changes aren't imideately reflected.
		/// </summary>
		private PaletteSet_ ActiveSet {
			get { return PaletteData.GetPaletteSet(this.selectedSet); }
		}

		#region Property clones
		/// <summary>
		/// The currently selected coordinates.  If -1 nothing is selected.
		/// </summary>
		private int selectedX = -1, selectedY = -1;

		private PaletteData paletteData = new PaletteData();
		private ColorSet selectedSet;
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
				OnSelectionChange();
			}
		}

		/// <summary>
		/// The x-location currently selected.  -1 if none selected.
		/// </summary>
		[Description("The x-location currently selected.  -1 if none selected."), Category("Data")]
		public int SelectedY {
			get { return selectedY; }
			set {
				if ((value >= ActiveSet.Size) || (value < -1)) {
					throw new ArgumentOutOfRangeException("SelectedY", "Value must be between 0 and " + (ActiveSet.Size - 1) + " inclusive, or -1 to represent no selection.");
				}
				selectedY = value;
				OnSelectionChange();
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
		/// The palette data used for this.
		/// </summary>
		[Description("The palette data used for this."), Category("Data")]
		public PaletteData PaletteData {
			get { freshenToSet(); return paletteData; }
			set { paletteData = value; freshenFromSet(); }
		}
		#endregion

		#region Events
		/// <summary>
		/// Event handler for when the slection is changed.
		/// </summary>
		[Category("Action"), Description("Fires when the selection is changed")]
		public event EventHandler SelectionChanged;

		protected void OnSelectionChange() {
			if (SelectionChanged != null) {
				SelectionChanged(this, new EventArgs());
			}
			this.Invalidate(true);
		}
		#endregion

		public GBPaletteSetSelector() {
			addControls();
		}
		
		private void addControls() {
			//Entries
			entries = new PaletteSetEntry[4, ActiveSet.Size];
			for (int y = 0; y < ActiveSet.Size; y++) {
				for (int x = 0; x < 4; x++) {
					entries[x, y] = new PaletteSetEntry(this, x, y);
					this.Controls.Add(entries[x, y]);
				}
			}

			//Informational labels.
			labels = new Label[ActiveSet.Size];
			for (int y = 0; y < ActiveSet.Size; y++) {
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
				this.ActiveSet[e.y][e.x] = e.Color;
			}
		}

		/// <summary>
		/// Updates the controls here with those from the PaletteSet.
		/// </summary>
		protected virtual void freshenFromSet() {
			for (int row = 0; row < ActiveSet.Size; row++) {
				for (int x = 0; x < 4; x++) {
					entries[x, row].Color = this.ActiveSet[row][x];
				}
			}


		}
	}
}
