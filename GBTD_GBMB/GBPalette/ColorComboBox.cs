using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using GB.Shared.Tile;

namespace GB.Shared.Palette
{
	/// <summary>
	/// Based off of this:
	/// http://www.csharphelp.com/2006/08/combobox-with-images/
	/// </summary>
	public class ColorComboBox : ComboBox
	{
		protected override void OnPaint(PaintEventArgs e) {
			try {
				ColorItem item = (ColorItem)SelectedItem;
				e.Graphics.DrawImage(item.DrawToBitmap(), 0, 0);
			} catch { }
		}

		protected override void OnDrawItem(DrawItemEventArgs ea) {
			ea.DrawBackground();
			ea.DrawFocusRectangle();

			ColorItem item;
			Rectangle bounds = ea.Bounds;

			try {
				if (ea.Index == -1) {
					item = (ColorItem)SelectedItem;

					ea.Graphics.DrawImage(item.DrawToBitmap(), ea.Bounds.X, ea.Bounds.Y);
				} else {
					item = (ColorItem)Items[ea.Index];

					ea.Graphics.DrawImage(item.DrawToBitmap(), ea.Bounds.X, ea.Bounds.Y);
				}
			} catch {
				if (ea.Index != -1) {
					ea.Graphics.DrawString(Items[ea.Index].ToString(), ea.Font, new
				   SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
				} else {
					ea.Graphics.DrawString(Text, ea.Font, new
				   SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
				}
			}

			base.OnDrawItem(ea);
		}

		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// ColorComboBox
			// 
			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.DropDownHeight = 152;
			this.IntegralHeight = false;
			this.ItemHeight = 19;
			this.MaximumSize = new System.Drawing.Size(76, 0);
			this.MinimumSize = new System.Drawing.Size(76, 0);
			this.Size = new System.Drawing.Size(76, 25);
			this.ResumeLayout(false);
		}
	}

	public class ColorItem
	{
		internal class ComboBoxPaletteEntry : PaletteEntry
		{
			public ComboBoxPaletteEntry(int x, int y) : base(x, y) { }

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

		public Color White {
			get {
				return colors[(int)GBColor.WHITE];
			}
			set {
				colors[(int)GBColor.WHITE] = value;
			}
		}

		public Color LightGray {
			get {
				return colors[(int)GBColor.LIGHT_GRAY];
			}
			set {
				colors[(int)GBColor.LIGHT_GRAY] = value;
			}
		}

		public Color DarkGray {
			get {
				return colors[(int)GBColor.DARK_GRAY];
			}
			set {
				colors[(int)GBColor.DARK_GRAY] = value;
			}
		}

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
	}
}
