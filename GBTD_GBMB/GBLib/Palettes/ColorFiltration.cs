﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GB.Shared.Tiles;
using System.ComponentModel;

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
		public static Color FilterWithGBC(this Color color) {
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
		/// Applies a red color filter to the specified color.
		/// 
		/// <para>Based off of lines 124-142 of ColorConverter.PAS.</para>
		/// </summary>
		/// 
		/// <param name="c">The color to filter.</param>
		/// <returns>The filtered color.</returns>
		public static Color FilterAsRed(this Color c) {
			int r, b, g;

			r = (c.R / 2) * 3;
			g = (c.G / 3) * 2;
			b = (c.B / 3) * 2;

			//Ensure in-bounds (may be unneeded, but was in origional)
			if (r > 0xff) { r = 0xff; }
			if (g > 0xff) { g = 0xff; }
			if (b > 0xff) { b = 0xff; }

			return Color.FromArgb(r, g, b);
		}

		/// <summary>
		/// Applies a green color filter to the specified color.
		/// 
		/// <para>Based off of lines 145-163 of ColorConverter.PAS.</para>
		/// </summary>
		/// 
		/// <param name="c">The color to filter.</param>
		/// <returns>The filtered color.</returns>
		public static Color FilterAsGreen(this Color c) {
			int r, b, g;

			r = (c.R / 3) * 2;
			g = (c.G / 2) * 3;
			b = (c.B / 3) * 2;

			//Ensure in-bounds (may be unneeded, but was in origional)
			if (r > 0xff) { r = 0xff; }
			if (g > 0xff) { g = 0xff; }
			if (b > 0xff) { b = 0xff; }

			return Color.FromArgb(r, g, b);
		}

		/// <summary>
		/// Converts the color to its selected varient.
		/// 
		/// <para>Based on 166-186 of ColorConverter.PAS.</para>
		/// </summary>
		/// 
		/// <param name="c">The color to filter.</param>
		/// <returns></returns>
		public static Color FilterAsSelected(this Color c) {
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

		/// <summary>
		/// Gets the color that GBTD uses when in Pocket GB color mode - true grayscale.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Color GetPocketColor(this GBColor @this) {
			switch (@this) {
			case GBColor.BLACK: return Color.FromArgb(0, 0, 0);
			case GBColor.DARK_GRAY: return Color.FromArgb(128, 128, 128);
			case GBColor.LIGHT_GRAY: return Color.FromArgb(192, 192, 192);
			case GBColor.WHITE: return Color.FromArgb(255, 255, 255);
			default: throw new InvalidEnumArgumentException("@this", (int)@this, typeof(GBColor));
			}
		}

		/// <summary>
		/// Gets the color that GBTD uses when in Pocket GB color mode - in "greenscale".
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Color GetNormalColor(this GBColor @this) {
			switch (@this) {
			case GBColor.BLACK: return Color.FromArgb(7, 57, 46);
			case GBColor.DARK_GRAY: return Color.FromArgb(32, 117, 49);
			case GBColor.LIGHT_GRAY: return Color.FromArgb(57, 185, 66);
			case GBColor.WHITE: return Color.FromArgb(224, 239, 41);
			default: throw new InvalidEnumArgumentException("@this", (int)@this, typeof(GBColor));
			}
		}

		#region Extension methods used with Palette
		/// <summary>
		/// Applies the GBC filter to a Palette.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Palette FilterWithGBC(this Palette @this) {
			Palette pal = new Palette();
			pal.Color0 = FilterWithGBC(@this.Color0);
			pal.Color1 = FilterWithGBC(@this.Color1);
			pal.Color2 = FilterWithGBC(@this.Color2);
			pal.Color3 = FilterWithGBC(@this.Color3);
			return pal;
		}
		/// <summary>
		/// Applies the red filter to a Palette.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Palette FilterAsRed(this Palette @this) {
			Palette pal = new Palette();
			pal.Color0 = FilterAsRed(@this.Color0);
			pal.Color1 = FilterAsRed(@this.Color1);
			pal.Color2 = FilterAsRed(@this.Color2);
			pal.Color3 = FilterAsRed(@this.Color3);
			return pal;
		}
		/// <summary>
		/// Applies the green filter to a Palette.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Palette FilterAsGreen(this Palette @this) {
			Palette pal = new Palette();
			pal.Color0 = FilterAsGreen(@this.Color0);
			pal.Color1 = FilterAsGreen(@this.Color1);
			pal.Color2 = FilterAsGreen(@this.Color2);
			pal.Color3 = FilterAsGreen(@this.Color3);
			return pal;
		}
		/// <summary>
		/// Applies the selection filter to a Palette.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static Palette FilterAsSelected(this Palette @this) {
			Palette pal = new Palette();
			pal.Color0 = FilterAsSelected(@this.Color0);
			pal.Color1 = FilterAsSelected(@this.Color1);
			pal.Color2 = FilterAsSelected(@this.Color2);
			pal.Color3 = FilterAsSelected(@this.Color3);
			return pal;
		}
		#endregion
	}
}
