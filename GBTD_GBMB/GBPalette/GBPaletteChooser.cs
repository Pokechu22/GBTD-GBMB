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
	public partial class GBPaletteChooser : UserControl
	{
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

		[Category("Data")]
		public ColorItem[] Colors {
			get { return colors; }
			set { colors = value; }
		}

		public GBPaletteChooser() {
			InitializeComponent();

			entry0 = new PaletteChooserEntry(0, 0, comboBox1);
			entry1 = new PaletteChooserEntry(1, 0, comboBox1);
			entry2 = new PaletteChooserEntry(2, 0, comboBox1);
			entry3 = new PaletteChooserEntry(3, 0, comboBox1);

			this.SuspendLayout();
			this.Controls.Add(entry0);
			this.Controls.Add(entry1);
			this.Controls.Add(entry2);
			this.Controls.Add(entry3);

			this.comboBox1.SendToBack();
			this.ResumeLayout();
		}

		private void comboBox1_DrawItem(object sender, DrawItemEventArgs e) {
			if (e.Index >= 0 && e.Index < colors.Length) {
				e.Graphics.DrawImageUnscaled(colors[e.Index].DrawToBitmap(), e.Bounds);
			} else {
				e.Graphics.DrawImageUnscaled(colors[0].DrawToBitmap(), e.Bounds);
				((ComboBox)(sender)).SelectedIndex = 0;
			}
		}

		private void comboBox1_MeasureItem(object sender, MeasureItemEventArgs e) {
			e.ItemHeight = 19;
			e.ItemWidth = 19 * 5;
		}
	}

	public class ColorItem
	{
		internal class ComboBoxPaletteEntry : PaletteEntry
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

	internal class PaletteChooserEntry : PaletteEntry
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

		public PaletteChooserEntry(int x, int y, Control toOverlay) : base(x, y) {
			this.toOverlay = toOverlay;
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
}
