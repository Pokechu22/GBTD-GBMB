using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.Tile
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
			Tile returned = new Tile();
			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[unchecked(x + 1) % 8, y];
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
			Tile returned = new Tile();
			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[unchecked(x - 1) % 8, y];
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
			Tile returned = new Tile();
			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[x, unchecked(y - 1) % 8];
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
			Tile returned = new Tile();
			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[x, unchecked(y + 1) % 8];
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
			Tile returned = new Tile();

			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[7 - x, y];
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
			Tile returned = new Tile();

			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[x, 7 - y];
				}
			}

			return returned;
		}
	}
}
