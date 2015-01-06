using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.Tiles
{
	/// <summary>
	/// Applies various transformations to tiles.
	/// </summary>
	public static class TileTransform
	{
		/// <summary>
		/// Scrolls a tileData leftwards.
		/// </summary>
		/// <param name="tileData"></param>
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
		/// Scrolls a tileData rightwards.
		/// </summary>
		/// <param name="tileData"></param>
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
		/// Scrolls a tileData downwards.
		/// </summary>
		/// <param name="tileData"></param>
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
		/// Scrolls a tileData upwards.
		/// </summary>
		/// <param name="tileData"></param>
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
		/// Flips a tileData vertically.
		/// </summary>
		/// <param name="tileData"></param>
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
		/// Flips a tileData horizontally.
		/// </summary>
		/// <param name="tileData"></param>
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

		/// <summary>
		/// Rotates a tileData clockwise.
		/// </summary>
		/// <param name="tileData"></param>
		/// <returns></returns>
		public static Tile RotateClockwise(Tile tile) {
			Tile returned = new Tile();

			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[y, 7 - x];//tileData.pixels[y + 4, x];
				}
			}

			return returned;
		}

		/// <summary>
		/// Rotates a tileData counterclockwise.
		/// 
		/// This is not used in the regular GBTD.
		/// </summary>
		/// <param name="tileData"></param>
		/// <returns></returns>
		public static Tile RotateCounterclockwise(Tile tile) {
			Tile returned = new Tile();

			returned.pixels = new GBColor[8, 8];

			for (uint x = 0; x < 8; x++) {
				for (uint y = 0; y < 8; y++) {
					returned.pixels[x, y] = tile.pixels[7 - y, x];
				}
			}

			return returned;
		}
	}
}
