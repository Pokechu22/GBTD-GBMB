using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.Tiles
{
	/// <summary>
	/// Defines something that is capable of editing tiles.
	/// </summary>
	public abstract class TileEditor
	{
		/// <summary>
		/// Applies the edit to the tileData.
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

	public class FloodFillTileEditor : TileEditor
	{
		public override Tile EditTile(Tile input, byte x, byte y, GBColor color) {
			GBColor clickedColor = input[x, y];

			// Prevent a potentially-infinite loop which would ultimately result in no changes.
			if (clickedColor == color) {
				return input;
			}

			return chainColoration(input, x, y, color, clickedColor);
		}

		protected Tile chainColoration(Tile tile, int x, int y, GBColor color, GBColor toReplace) {
			tile[x, y] = color;
			if (x > 0 && tile[x - 1, y] == toReplace) {
				tile = chainColoration(tile, x - 1, y, color, toReplace);
			}
			if (x < 7 && tile[x + 1, y] == toReplace) {
				tile = chainColoration(tile, x + 1, y, color, toReplace);
			}
			if (y > 0 && tile[x, y - 1] == toReplace) {
				tile = chainColoration(tile, x, y - 1, color, toReplace);
			}
			if (y < 7 && tile[x, y + 1] == toReplace) {
				tile = chainColoration(tile, x, y + 1, color, toReplace);
			}
			return tile;
		}

		public override TileEditorID GetID() {
			return TileEditorID.FloodFill;
		}
	}
}
