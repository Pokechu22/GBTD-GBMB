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
	}
}
