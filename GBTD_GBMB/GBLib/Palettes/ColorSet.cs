using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
}
