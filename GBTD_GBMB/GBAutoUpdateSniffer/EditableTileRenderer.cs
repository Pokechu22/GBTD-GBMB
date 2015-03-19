using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Controls;
using GB.Shared.Tiles;

namespace GBAutoUpdateSniffer
{
	/// <summary>
	/// A renderer that changes the color when clicked.  Very basic, unlike that of GBTD.
	/// </summary>
	public class EditableTileRenderer : TileRenderer
	{
		public EditableTileRenderer() {
			//Required for it to update when not focused.  http://stackoverflow.com/a/2326001/3991344
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.ResizeRedraw,
				true);
		}

		protected override void OnPixelClicked(PixelClickEventArgs e) {
			//Ignore out of range clicks.
			if (e.x < 0 || e.x >= Tile.Width || e.y < 0 || e.y >= Tile.Height) { return; }

			Tile t = this.Tile;
			GBColor oldColor = t[e.x, e.y];

			GBColor newColor;


			if (e.mouseButton == System.Windows.Forms.MouseButtons.Left) {
				switch (oldColor) {
				case GBColor.BLACK: newColor = GBColor.DARK_GRAY; break;
				case GBColor.DARK_GRAY: newColor = GBColor.LIGHT_GRAY; break;
				case GBColor.LIGHT_GRAY: newColor = GBColor.WHITE; break;
				case GBColor.WHITE: newColor = GBColor.BLACK; break;
				default: return; //This *SHOULD NEVER* happen.
				}
			} else if (e.mouseButton == System.Windows.Forms.MouseButtons.Right) {
				switch (oldColor) {
				case GBColor.BLACK: newColor = GBColor.WHITE; break;
				case GBColor.DARK_GRAY: newColor = GBColor.BLACK; break;
				case GBColor.LIGHT_GRAY: newColor = GBColor.DARK_GRAY; break;
				case GBColor.WHITE: newColor = GBColor.LIGHT_GRAY; break;
				default: return; //This *SHOULD NEVER* happen.
				}
			} else {
				return;
			}

			t[e.x, e.y] = newColor;

			this.Tile = t;

			base.OnPixelClicked(e);
		}
	}
}
