using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GB.Shared.Palettes
{
	/// <summary>
	/// Options that are valid for <see cref="GBRObjectTileSettings.ColorSet"/>.
	/// </summary>
	public enum ColorSet : byte
	{
		GAMEBOY_POCKET = 0,
		GAMEBOY = 1,
		GAMEBOY_COLOR = 2,
		SUPER_GAMEBOY = 3,
		GAMEBOY_COLOR_FILTERED = 4
	}

	public static class ColorSetExtensions
	{
		/// <summary>
		/// Is this color set filtered by GBC color filtration?
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static bool IsFiltered(this ColorSet @this) {
			switch (@this) {
			case ColorSet.GAMEBOY_POCKET: return false;
			case ColorSet.GAMEBOY: return false;
			case ColorSet.GAMEBOY_COLOR: return false;
			case ColorSet.SUPER_GAMEBOY: return false;
			case ColorSet.GAMEBOY_COLOR_FILTERED: return true;
			default: throw new InvalidEnumArgumentException("@this", (int)@this, typeof(ColorSet));
			}
		}
		
		/// <summary>
		/// Gets the display name of this colorset.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static String GetDisplayName(this ColorSet @this) {
			switch (@this) {
			case ColorSet.GAMEBOY_POCKET: return "Gameboy Pocket";
			case ColorSet.GAMEBOY: return "Gameboy";
			case ColorSet.GAMEBOY_COLOR: return "Gameboy Color";
			case ColorSet.SUPER_GAMEBOY: return "Super Gameboy";
			case ColorSet.GAMEBOY_COLOR_FILTERED: return "Gameboy Color";
			default: throw new InvalidEnumArgumentException("@this", (int)@this, typeof(ColorSet));
			}
		}

		/// <summary>
		/// Whether or not this ColorSet has a Palette.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static bool SupportsPaletteCustomization(this ColorSet @this) {
			switch (@this) {
			case ColorSet.GAMEBOY_POCKET: return false;
			case ColorSet.GAMEBOY: return false;
			case ColorSet.GAMEBOY_COLOR: return true;
			case ColorSet.SUPER_GAMEBOY: return true;
			case ColorSet.GAMEBOY_COLOR_FILTERED: return true;
			default: throw new InvalidEnumArgumentException("@this", (int)@this, typeof(ColorSet));
			}
		}

		/// <summary>
		/// Whether or not this ColorSet supports tile flipping.
		/// </summary>
		/// <param name="this"></param>
		/// <returns></returns>
		public static bool SupportsTileFlipping(this ColorSet @this) {
			switch (@this) {
			case ColorSet.GAMEBOY_POCKET: return false;
			case ColorSet.GAMEBOY: return false;
			case ColorSet.GAMEBOY_COLOR: return true;
			case ColorSet.SUPER_GAMEBOY: return false;
			case ColorSet.GAMEBOY_COLOR_FILTERED: return true;
			default: throw new InvalidEnumArgumentException("@this", (int)@this, typeof(ColorSet));
			}
		}
	}
}
