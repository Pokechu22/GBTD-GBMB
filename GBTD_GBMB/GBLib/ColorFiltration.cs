using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GB.Shared.Tile
{
	/// <summary>
	/// Filters colors. TODO: Merge with GBCFiltration.  (Caused circular dependency)
	/// </summary>
	public static class ColorFiltration
	{
		/// <summary>
		/// Converts the color to its selected varient.
		/// 
		/// Based on 166-186 of ColorConverter.PAS.
		/// TODO: Move nicer place.
		/// </summary>
		/// <returns></returns>
		public static Color GetSelectedColor(Color c) {
			int r, b, g;

			r = (c.R / 4) * 2;
			g = (c.G / 4) * 2;
			b = (c.B / 2) * 4;

			//Ensure in-bounds (may be unneeded, but was in origional)
			if (r > 0xff) { r = 0xff; }
			if (g > 0xff) { g = 0xff; }
			if (b > 0xff) { b = 0xff; }

			return Color.FromArgb(r, g, b);
		}
	}
}
