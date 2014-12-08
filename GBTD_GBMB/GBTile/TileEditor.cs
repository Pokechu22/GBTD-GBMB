using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.Tile
{
	/// <summary>
	/// Defines something that is capable of editing tiles.
	/// </summary>
	public abstract class TileEditor
	{
		/// <summary>
		/// Applies the edit to the tile.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		public abstract Tile EditTile(Tile input, byte x, byte y, GBColor color);

		/// <summary>
		/// Gets the ID used by this editor.
		/// </summary>
		/// <returns></returns>
		public abstract TileEditorID GetID();

		public override string ToString() {
			return this.GetType().Name;
		}
	}

	/// <summary>
	/// A TileEditor that does nothing whatsoever to suplied tiles.
	/// </summary>
	public class NoEditTileEditor : TileEditor
	{
		public override Tile EditTile(Tile input, byte x, byte y, GBColor color) {
			return input;
		}

		public override TileEditorID GetID() {
			return TileEditorID.NoEdit;
		}
	}

	/// <summary>
	/// A TileEditor which replaces the clicked pixel with a specific color.
	/// </summary>
	public class PixelTileEditor : TileEditor
	{
		public override Tile EditTile(Tile input, byte x, byte y, GBColor color) {
			input[x, y] = color;
			return input;
		}

		public override TileEditorID GetID() {
			return TileEditorID.PixelEdit;
		}
	}
}
