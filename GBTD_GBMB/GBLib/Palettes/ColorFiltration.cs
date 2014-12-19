﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GB.Shared.Palettes
{
	/// <summary>
	/// Filters colors.
	/// </summary>
	public static class ColorFiltration
	{
		/// <summary>
		/// Filters the color.
		/// 
		/// Based off of lines 78-110 of ColorControlor.pas.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color TranslateToGBCColor(Color color) {
			byte[] intensity = new byte[0x20] {
				0x00,0x10,0x20,0x30,0x40,0x50,0x5e,0x6c,0x7a,0x88,0x94,0xa0,0xae,0xb7,0xbf,0xc6,
				0xce,0xd3,0xd9,0xdf,0xe3,0xe7,0xeb,0xef,0xf3,0xf6,0xf9,0xfb,0xfd,0xfe,0xff,0xff
			};

			byte[,] influence = new byte[3, 3] { { 16, 4, 4 }, { 8, 16, 8 }, { 0, 8, 16 } };

			byte[,] m = new byte[3, 3];
			byte i, j;
			byte[] rgb = new byte[3];
			Color c;

			rgb[0] = (byte)(color.R >> 3);
			rgb[1] = (byte)(color.G >> 3);
			rgb[2] = (byte)(color.B >> 3);

			for (i = 0; i < 3; i++) {
				for (j = 0; j < 3; j++) {
					m[i, j] = (byte)((intensity[rgb[i]] * influence[i, j]) >> 5);
				}
			}
			for (i = 0; i < 3; i++) {
				if (m[0, i] > m[1, i]) { j = m[0, i]; m[0, i] = m[1, i]; m[1, i] = j; }
				if (m[1, i] > m[2, i]) { j = m[1, i]; m[1, i] = m[2, i]; m[2, i] = j; };
				if (m[0, i] > m[1, i]) { j = m[0, i]; m[0, i] = m[1, i]; m[1, i] = j; };
				rgb[i] = (byte)((((m[0, i] + m[1, i] * 2 + m[2, i] * 4) * 5) >> 4) + 32);
			}

			c = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
			return c;
		}

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

		#region Extension methods used with Palette
		/// <summary>
		/// Applies the GBC filter to a Palette.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Palette FilterWithGBC(this Palette @this) {
			@this.entry0.color = TranslateToGBCColor(@this.entry0.color);
			@this.entry1.color = TranslateToGBCColor(@this.entry1.color);
			@this.entry2.color = TranslateToGBCColor(@this.entry2.color);
			@this.entry3.color = TranslateToGBCColor(@this.entry3.color);
			return @this;
		}
		/// <summary>
		/// Applies the selection filter to a Palette.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Palette FilterAsSelected(this Palette @this) {
			@this.entry0.color = GetSelectedColor(@this.entry0.color);
			@this.entry1.color = GetSelectedColor(@this.entry1.color);
			@this.entry2.color = GetSelectedColor(@this.entry2.color);
			@this.entry3.color = GetSelectedColor(@this.entry3.color);
			return @this;
		}
		#endregion
	}
}