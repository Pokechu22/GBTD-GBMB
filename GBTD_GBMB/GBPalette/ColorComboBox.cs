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
	internal class ColorComboBox : ComboBox
	{
		private ImageList imageList;
		public ImageList ImageList {
			get { return imageList; }
			set { imageList = value; }
		}

		public ColorComboBox() {
			DrawMode = DrawMode.OwnerDrawFixed;
		}

		protected override void OnDrawItem(DrawItemEventArgs ea) {
			ea.DrawBackground();
			ea.DrawFocusRectangle();

			ComboBoxExItem item;
			Size imageSize = imageList.ImageSize;
			Rectangle bounds = ea.Bounds;

			try {
				item = (ComboBoxExItem)Items[ea.Index];

				if (item.ImageIndex != -1) {
					imageList.Draw(ea.Graphics, bounds.Left, bounds.Top,
				   item.ImageIndex);
					ea.Graphics.DrawString(item.Text, ea.Font, new
				   SolidBrush(ea.ForeColor), bounds.Left + imageSize.Width, bounds.Top);
				} else {
					ea.Graphics.DrawString(item.Text, ea.Font, new
				   SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
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
	}

	class ColorItem
	{
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

	class ComboBoxExItem
	{
		private string _text;
		public string Text {
			get { return _text; }
			set { _text = value; }
		}

		private int _imageIndex;
		public int ImageIndex {
			get { return _imageIndex; }
			set { _imageIndex = value; }
		}

		public ComboBoxExItem()
			: this("") {
		}

		public ComboBoxExItem(string text)
			: this(text, -1) {
		}

		public ComboBoxExItem(string text, int imageIndex) {
			_text = text;
			_imageIndex = imageIndex;
		}

		public override string ToString() {
			return _text;
		}
	}
}

