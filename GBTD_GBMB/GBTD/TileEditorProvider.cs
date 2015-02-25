using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GB.Shared.Tiles;

namespace GB.GBTD
{
	/// <summary>
	/// Contains IDs of different TileEditors.
	/// </summary>
	public enum TileEditorID
	{
		/// <summary>
		/// Refers to <see cref="NoEditTileEditor"/>.
		/// </summary>
		NoEdit = 0,
		/// <summary>
		/// Refers to <see cref="PixelTileEditor"/>.
		/// </summary>
		PixelEdit = 1,
		/// <summary>
		/// Refers to <see cref="FloodFillTileEditor"/>.
		/// </summary>
		FloodFill = 2
	}

	/// <summary>
	/// Gets a TileEditor from an ID or name.
	/// </summary>
	public static class TileEditorProvider
	{
		/// <summary>
		/// Gets the relevant editor for the <see cref="TileEditorID"/>.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static TileEditor GetEditorForID(TileEditorID id) {
			switch (id) {
			case TileEditorID.NoEdit: return new NoEditTileEditor();
			case TileEditorID.PixelEdit: return new PixelTileEditor();
			case TileEditorID.FloodFill: return new FloodFillTileEditor();
			}
			throw new InvalidEnumArgumentException("id", (int)id, typeof(TileEditorID));
		}
	}
}
