using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Tile
{
	public partial class PixelEditableTileRenderer : TileRenderer
	{
		#region Private fields
		private GBColor leftMouseColor = GBColor.BLACK;
		private GBColor rightMouseColor = GBColor.WHITE;
		private GBColor middleMouseColor = GBColor.DARK_GRAY;
		private GBColor xButton1MouseColor = GBColor.WHITE;
		private GBColor xButton2MouseColor = GBColor.WHITE;
		#endregion

		#region Public Properties

		/// <summary>
		/// The color that is used for the left mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the left mouse button.")]
		public GBColor LeftMouseColor {
			get { return leftMouseColor; }
			set { leftMouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the right mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the right mouse button.")]
		public GBColor RightMouseColor {
			get { return rightMouseColor; }
			set { rightMouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the middle mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the middle mouse button.")]
		public GBColor MiddleMouseColor {
			get { return middleMouseColor; }
			set { middleMouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the xButton1 mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the xButton1 mouse button.")]
		public GBColor XButton1MouseColor {
			get { return xButton1MouseColor; }
			set { xButton1MouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the xButton2 mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the xButton2 mouse button.")]
		public GBColor XButton2MouseColor {
			get { return xButton2MouseColor; }
			set { xButton2MouseColor = value; }
		}
		#endregion


		public PixelEditableTileRenderer() {
			InitializeComponent();
		}

		private void PixelEditableTileRenderer_PixelClicked(object sender, PixelClickEventArgs e) {
			GBColor color;
			//Try to set the color to the dictionary value; if it fails use black.
			if (e.mouseButton.HasFlag(MouseButtons.XButton2)) {
				color = xButton2MouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.XButton1)) {
				color = xButton1MouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.Middle)) {
				color = middleMouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.Right)) {
				color = rightMouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.Left)) {
				color = leftMouseColor;
			} else {
				color = GBColor.WHITE;
			}

			Tile[e.x, e.y] = color;
		}
	}
}
