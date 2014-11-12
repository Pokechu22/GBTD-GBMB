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
		private class PaletteChooserEntry : PaletteEntry
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
				chooser.FirstMouseSelectedIndex = this.x;
			}

			protected override bool IsSelected() {
				return this.x == chooser.FirstMouseSelectedIndex;
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

			protected override void OnClick(EventArgs e) {
				base.OnClick(e);

			}
		}

		private bool useGBCFilter;
		public bool UseGBCFilter {
			get { return useGBCFilter; }
			set { useGBCFilter = value; this.Refresh(); }
		}
		private int firstMouseSelectedIndex = 0;
		public int FirstMouseSelectedIndex {
			get { return firstMouseSelectedIndex; }
			set {
				if (value < 0 || value > 3) {
					throw new ArgumentOutOfRangeException("value", value, "Must be between 0 and 3 (inclusive)");
				}
				OnPaletteEntryClicked(value);
			}
		}
		protected override Size DefaultMaximumSize {
			get {
				return new Size(108, 21);
			}
		}

		protected override Size DefaultMinimumSize {
			get {
				return new Size(108, 21);
			}
		}

		protected override Size DefaultSize {
			get {
				return new Size(108, 21);
			}
		}

		private PaletteChooserEntry entry0, entry1, entry2, entry3;
		
		private ColorItem[] colors = new ColorItem[8] {
			new ColorItem(),
			new ColorItem(),
			new ColorItem(),
			new ColorItem(),
			new ColorItem(),
			new ColorItem(),
			new ColorItem(),
			new ColorItem()
		};

		#region Events
		public event SelectedPaletteEventHandler SelectedPaletteChanged;

		protected void OnSelectionChanged() {
			if (SelectedPaletteChanged != null) {
				SelectedPaletteChanged(this, new SelectedPaletteEventArgs(dropDown.SelectedIndex, colors[dropDown.SelectedIndex]));
			}
			this.Refresh();
		}

		public event PaletteEntryClickEventHandler PaletteEntryClicked;

		protected void OnPaletteEntryClicked(int clickedIndex) {
			if (PaletteEntryClicked != null) {
				PaletteEntryClickEventArgs args = new PaletteEntryClickEventArgs(clickedIndex, this.firstMouseSelectedIndex);
				PaletteEntryClicked(this, args);
				if (args.shouldChange) {
					this.firstMouseSelectedIndex = clickedIndex;
				}
			} else {
				this.firstMouseSelectedIndex = clickedIndex;
			}
			this.Refresh();
		}
		#endregion

		[Category("Data"), ReadOnly(true), Browsable(true)]
		public ColorItem[] Colors {
			get { return colors; }
			set { colors = value; }
		}

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
			if (e.Index >= 0 && e.Index < colors.Length) {
				e.Graphics.DrawImageUnscaled(colors[e.Index].DrawToBitmap(), e.Bounds);
			} else {
				e.Graphics.DrawImageUnscaled(colors[0].DrawToBitmap(), e.Bounds);
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
			ColorItem item = colors[Convert.ToInt32((String)box.Text)];
			entry0.Color = item.White;
			entry1.Color = item.LightGray;
			entry2.Color = item.DarkGray;
			entry3.Color = item.Black;
		}

		private void dropDown_electionChangeCommitted(object sender, EventArgs e) {
			vScrollBar.Value = dropDown.SelectedIndex;
			//Update the other icons.
			ComboBox box = (ComboBox)sender;
			ColorItem item = colors[Convert.ToInt32((String)box.Text)];
			entry0.Color = item.White;
			entry1.Color = item.LightGray;
			entry2.Color = item.DarkGray;
			entry3.Color = item.Black;
		}
	}

	internal class ColorItem
	{
		private class ComboBoxPaletteEntry : PaletteEntry
		{
			public ComboBoxPaletteEntry(int x, int y) : base(x, y) {
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

		internal Image DrawToBitmap() {
			Bitmap returned = null;

			for (int i = 0; i < colors.Length; i++) {
				ComboBoxPaletteEntry e = new ComboBoxPaletteEntry(i, 0);
				if (returned == null) {
					returned = new Bitmap(e.Width * 4, e.Height);
				}

				e.Color = this[i];

				e.DrawToBitmap(returned, new Rectangle(e.Width * i, 0, e.Width, e.Height));
			}

			return returned;
		}

		private Color[] colors = new Color[4];

		[Category("Data")]
		public Color White {
			get {
				return colors[(int)GBColor.WHITE];
			}
			set {
				colors[(int)GBColor.WHITE] = value;
			}
		}

		[Category("Data")]
		public Color LightGray {
			get {
				return colors[(int)GBColor.LIGHT_GRAY];
			}
			set {
				colors[(int)GBColor.LIGHT_GRAY] = value;
			}
		}

		[Category("Data")]
		public Color DarkGray {
			get {
				return colors[(int)GBColor.DARK_GRAY];
			}
			set {
				colors[(int)GBColor.DARK_GRAY] = value;
			}
		}

		[Category("Data")]
		public Color Black {
			get {
				return colors[(int)GBColor.BLACK];
			}
			set {
				colors[(int)GBColor.BLACK] = value;
			}
		}

		public Color this[int index] {
			get {
				return colors[index];
			}
			set {
				colors[index] = value;
			}
		}

		public Color this[GBColor color] {
			get {
				return colors[(int)color];
			}
			set {
				colors[(int)color] = value;
			}
		}

		public ColorItem() {
			this.Black = Color.Black;
			this.DarkGray = Color.DarkGray;
			this.LightGray = Color.LightGray;
			this.White = Color.White;
		}
	}

	internal delegate void SelectedPaletteEventHandler(object sender, SelectedPaletteEventArgs args);

	internal class SelectedPaletteEventArgs : EventArgs
	{
		public readonly int index;
		public readonly ColorItem item;

		public SelectedPaletteEventArgs(int index, ColorItem item) {
			this.index = index;
			this.item = item;
		}
	}

	internal delegate void PaletteEntryClickEventHandler(object sender, PaletteEntryClickEventArgs e);

	/// <summary>
	/// Event for when an entry is clicked.
	/// </summary>
	internal class PaletteEntryClickEventArgs : EventArgs
	{
		public readonly int clickedIndex;
		public readonly int oldIndex;

		public bool shouldChange = true;

		public PaletteEntryClickEventArgs(int clickedIndex, int oldIndex) {
			this.clickedIndex = clickedIndex;
			this.oldIndex = oldIndex;
		}
	}
}
