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
	internal partial class GBPaletteChooser : UserControl
	{
		/// <summary>
		/// Event for when the selected palette is changed.
		/// </summary>
		internal delegate void SelectedPaletteChangeEventHandler(object sender, SelectedPaletteChangeEventArgs e);

		internal delegate void PaletteEntryClickEventHandler(object sender, PaletteEntryClickEventArgs e);

		/// <summary>
		/// Event for when the selected palette is changed.
		/// </summary>
		internal class SelectedPaletteChangeEventArgs : EventArgs
		{
			private readonly GBPaletteChooser sender;

			public readonly int newIndex;
			public readonly Palette_ newItem;

			public SelectedPaletteChangeEventArgs(GBPaletteChooser sender, int newIndex) {
				this.sender = sender;
				this.newIndex = newIndex;
				this.newItem = sender.set[newIndex];
			}
		}

		internal class PaletteEntryClickEventArgs : EventArgs
		{
			private readonly GBPaletteChooser sender;

			public readonly int paletteIndex;
			public readonly Palette_ palette;

			public readonly int clickedEntry;
			public readonly Color clickedEntryColor;

			public readonly MouseButtons button;

			public PaletteEntryClickEventArgs(GBPaletteChooser sender, int paletteIndex, int clickedEntry, MouseButtons button) {
				this.sender = sender;

				this.paletteIndex = paletteIndex;
				this.palette = sender.set[paletteIndex];

				this.clickedEntry = clickedEntry;
				this.clickedEntryColor = palette[clickedEntry];

				this.button = button;
			}
		}

		protected class PaletteChooserEntry : PaletteEntry
		{
			protected override int X_OFFSET {
				get {
					return toOverlay.Location.X + 1;
				}
			}

			protected override int Y_OFFSET {
				get {
					return toOverlay.Location.Y + 1;
				}
			}

			protected override System.Windows.Forms.MouseButtons SelectionButtons {
				get {
					return MouseButtons.Left;
				}
			}

			/// <summary>
			/// Control to put these over.
			/// </summary>
			private Control toOverlay;
			private GBPaletteChooser chooser;

			public PaletteChooserEntry(int x, int y, Control toOverlay, GBPaletteChooser chooser)
				: base(x, y) {
				this.toOverlay = toOverlay;
				this.chooser = chooser;
			}

			protected override void SetSelected() {
				if (chooser.SelectOnLeftClick) {
					chooser.HighlightedEntryIndex = this.x;
				}
			}

			protected override bool IsSelected() {
				return this.x == chooser.HighlightedEntryIndex;
			}

			protected override bool UseGBCFilter {
				get {
					return chooser.UseGBCFilter;
				}
				set {
					chooser.UseGBCFilter = value;
				}
			}

			protected override Color GetDefaultColor() {
				return Color.Black;
			}

			protected override void OnMouseDown(MouseEventArgs e) {
				base.OnMouseDown(e);

				chooser.OnPaletteEntryClicked(this, e.Button);
			}
		}

		private class ComboBoxPaletteEntry : PaletteEntry
		{
			public ComboBoxPaletteEntry(int x, int y)
				: base(x, y) {
				this.InitLayout();
			}

			protected override void SetSelected() {
				//Do nothing
			}

			protected override bool IsSelected() {
				return false;
			}

			protected override bool UseGBCFilter {
				get {
					return false;
				}
				set {
					throw new NotImplementedException();
				}
			}

			protected override Color GetDefaultColor() {
				return Color.Black;
			}
		}

		private bool useGBCFilter;
		/// <summary>
		/// Controls whether or not to render with the GBC Filter.
		/// </summary>
		[Category("Behavior"), Description("Controls whether or not to render with the GBC Filter.")]
		public bool UseGBCFilter {
			get { return useGBCFilter; }
			set { useGBCFilter = value; this.Refresh(); }
		}

		private int highlightedEntryIndex = -1;
		/// <summary>
		/// The index of the entry that is currently highlighted (surrounded with a blue outline)
		/// </summary>
		[Category("Data"), Description("The currently-selected index.")]
		public int HighlightedEntryIndex {
			get { return highlightedEntryIndex; }
			set { highlightedEntryIndex = value; this.Refresh(); }
		}

		/// <summary>
		/// The index of the row currently selected.
		/// </summary>
		public int SelectedRowIndex {
			get { return dropDown.SelectedIndex; }
			set {
				if (value < 0 || value >= set.NumberOfRows) {
					throw new ArgumentOutOfRangeException("value", "Must be between 0 and " + (set.NumberOfRows-1) + "; got " + value + ".");
				}
				dropDown.SelectedIndex = value;
			}
		}

		/// <summary>
		/// The row currently selected.
		/// </summary>
		public Palette_ SelectedRow {
			get {
				if (SelectedRowIndex < 0 || SelectedRowIndex >= set.NumberOfRows) {
					SelectedRowIndex = 0;
				}
				return set[SelectedRowIndex];
			}
			set {
				if (SelectedRowIndex < 0 || SelectedRowIndex >= set.NumberOfRows) {
					SelectedRowIndex = 0;
				}
				int i = 0;
				i = i; //To create a warning.  TODO.
				//set[SelectedRowIndex] = value;
				reloadFromSet();
			}
		}

		private bool selectOnLeftClick = true;
		/// <summary>
		/// Controls whether or not to select a value on left click.
		/// </summary>
		[Category("Behavior"), Description("Controls whether or not to select a value on left click.")]
		public bool SelectOnLeftClick {
			get { return selectOnLeftClick; }
			set { selectOnLeftClick = value; }
		}

		protected override Size DefaultMaximumSize {
			get {
				return new Size(111, 22);
			}
		}

		protected override Size DefaultMinimumSize {
			get {
				return new Size(111, 22);
			}
		}

		protected override Size DefaultSize {
			get {
				return new Size(111, 22);
			}
		}

		private PaletteChooserEntry entry0, entry1, entry2, entry3;

		private PaletteSet_ set = new PaletteSet_();

		public PaletteSet_ Set {
			get { return set; }
			set { set = value; reloadFromSet(); OnSelectedPaletteChanged(); }
		}

		#region Events
		public event SelectedPaletteChangeEventHandler SelectedPaletteChanged;

		protected void OnSelectedPaletteChanged() {
			if (SelectedPaletteChanged != null) {
				SelectedPaletteChanged(this, new SelectedPaletteChangeEventArgs(this, dropDown.SelectedIndex));
			}
			this.Refresh();
		}

		public event PaletteEntryClickEventHandler PaletteEntryClicked;

		protected void OnPaletteEntryClicked(PaletteChooserEntry entry, MouseButtons buttons) {
			if (PaletteEntryClicked != null) {
				PaletteEntryClicked(this, new PaletteEntryClickEventArgs(this, this.dropDown.SelectedIndex, entry.x, buttons));
			}
			this.Refresh();
		}
		#endregion

		public GBPaletteChooser() {
			InitializeComponent();

			entry0 = new PaletteChooserEntry(0, 0, dropDown, this);
			entry1 = new PaletteChooserEntry(1, 0, dropDown, this);
			entry2 = new PaletteChooserEntry(2, 0, dropDown, this);
			entry3 = new PaletteChooserEntry(3, 0, dropDown, this);

			this.SuspendLayout();
			this.Controls.Add(entry0);
			this.Controls.Add(entry1);
			this.Controls.Add(entry2);
			this.Controls.Add(entry3);

			this.dropDown.SendToBack();
			this.ResumeLayout();

			vScrollBar.Maximum = dropDown.Items.Count - 1;
		}

		private void dropDown_DrawItem(object sender, DrawItemEventArgs e) {
			if (e.Index >= 0 && e.Index < set.NumberOfRows) {
				e.Graphics.DrawImageUnscaled(DrawRowToBitmap(set[e.Index]), e.Bounds);
			} else {
				e.Graphics.DrawImageUnscaled(DrawRowToBitmap(set[0]), e.Bounds);
				((ComboBox)(sender)).SelectedIndex = 0;
			}
		}

		private void dropDown_MeasureItem(object sender, MeasureItemEventArgs e) {
			e.ItemHeight = 19;
			e.ItemWidth = 19 * 5;
		}

		private void vScrollBar_ValueChanged(object sender, EventArgs e) {
			dropDown.SelectedIndex = vScrollBar.Value;
		}

		private void dropDown_SelectedIndexChanged(object sender, EventArgs e) {
			vScrollBar.Value = dropDown.SelectedIndex;
			//Update the other icons.
			ComboBox box = (ComboBox)sender;
			Palette_ item = set[Convert.ToInt32((String)box.Text)];
			entry0.Color = item[0];
			entry1.Color = item[1];
			entry2.Color = item[2];
			entry3.Color = item[3];

			OnSelectedPaletteChanged();
		}

		private void dropDown_SelectionChangeCommitted(object sender, EventArgs e) {
			vScrollBar.Value = dropDown.SelectedIndex;
			//Update the other icons.
			ComboBox box = (ComboBox)sender;
			Palette_ item = set[Convert.ToInt32((String)box.Text)];
			entry0.Color = item[0];
			entry1.Color = item[1];
			entry2.Color = item[2];
			entry3.Color = item[3];

			OnSelectedPaletteChanged();
		}

		/// <summary>
		/// Draws Palette_ to a bitmap.
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		protected virtual Image DrawRowToBitmap(Palette_ row) {
			Bitmap returned = null;

			for (int i = 0; i < 4; i++) {
				ComboBoxPaletteEntry e = new ComboBoxPaletteEntry(i, 0);
				if (returned == null) {
					returned = new Bitmap(e.Width * 4, e.Height);
				}

				e.Color = row[i];

				e.DrawToBitmap(returned, new Rectangle(e.Width * i, 0, e.Width, e.Height));
			}

			return returned;
		}

		/// <summary>
		/// Reloads the contents of this from the PaletteSet_.
		/// </summary>
		protected virtual void reloadFromSet() {
			if (this.dropDown.SelectedIndex < 0 || this.dropDown.SelectedIndex >= set.NumberOfRows) {
				this.dropDown.SelectedIndex = 0;
			}
			this.entry0.Color = Set[this.dropDown.SelectedIndex][0];
			this.entry1.Color = Set[this.dropDown.SelectedIndex][1];
			this.entry2.Color = Set[this.dropDown.SelectedIndex][2];
			this.entry3.Color = Set[this.dropDown.SelectedIndex][3];

			this.Refresh();
		}
	}
}
