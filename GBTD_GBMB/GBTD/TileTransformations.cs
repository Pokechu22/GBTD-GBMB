using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GB.Shared.Tiles;

namespace GB.GBTD
{
	/// <summary>
	/// Applies various transformations to tiles.
	/// </summary>
	public static class TileTransform
	{
		/// <summary>
		/// Scrolls a tile leftwards.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile ScrolledLeft(Tile tile) {
			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[unchecked(x + 1) % tile.Width, y];
				}
			}

			return returned;
		}

		/// <summary>
		/// Scrolls a tile rightwards.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile ScrolledRight(Tile tile) {
			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[unchecked(x - 1) % tile.Width, y];
				}
			}

			return returned;
		}

		/// <summary>
		/// Scrolls a tile downwards.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile ScrolledDown(Tile tile) {
			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[x, unchecked(y - 1) % tile.Height];
				}
			}

			return returned;
		}

		/// <summary>
		/// Scrolls a tile upwards.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile ScrolledUp(Tile tile) {
			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[x, unchecked(y + 1) % tile.Height];
				}
			}

			return returned;
		}

		/// <summary>
		/// Flips a tile vertically.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile FlippedVertically(Tile tile) {
			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[tile.Width - x - 1, y];
				}
			}

			return returned;
		}

		/// <summary>
		/// Flips a tile horizontally.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile FlippedHoriziontally(Tile tile) {
			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[x, tile.Height - y - 1];
				}
			}

			return returned;
		}

		/// <summary>
		/// Rotates a tile clockwise.
		/// Note: the tile *must* be of the same width and height.
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile RotateClockwise(Tile tile) {
			if (tile.Width != tile.Height) {
				throw new ArgumentException("Tile width must equal tile height to rotate!  Got " + tile.Width + ", " + tile.Height + ".", "tile");
			}

			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[y, tile.Width - x - 1];//tileData.pixels[y + 4, x];
				}
			}

			return returned;
		}

		/// <summary>
		/// Rotates a tileData counterclockwise.
		/// 
		/// This is not used in the regular GBTD.
		/// 
		/// <para>Note: the tile *must* be of the same width and height.</para>
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public static Tile RotateCounterclockwise(Tile tile) {
			if (tile.Width != tile.Height) {
				throw new ArgumentException("Tile width must equal tile height to rotate!  Got " + tile.Width + ", " + tile.Height + ".", "tile");
			}

			Tile returned = new Tile(tile.Width, tile.Height);

			for (uint x = 0; x < tile.Width; x++) {
				for (uint y = 0; y < tile.Height; y++) {
					returned[x, y] = tile[tile.Height - y - 1, x];
				}
			}

			return returned;
		}
	}
}
