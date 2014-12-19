using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.Tiles
{
	/// <summary>
	/// A single tile.
	/// </summary>
	public struct Tile
	{
		internal GBColor[,] pixels;

		/// <summary>
		/// Pixels on the tile.  MUST BE 8 by 8 exactly.
		/// </summary>
		public GBColor[,] Pixels {
			get {
				//Initialize pixels if needed.
				if (pixels == null) {
					pixels = new GBColor[8, 8];
				}

				return pixels;
			}
			set {
				if (value.GetLength(0) != 8 || value.GetLength(1) != 8) {
					throw new ArgumentException("Value is not of the right size - Must be an array [8,8].");
				}
				pixels = value;
			}
		}

		/// <summary>
		/// Sets/gets the specific pixels of the image.
		/// </summary>
		/// <param name="x">x-coord of pixel, from 0 to 7</param>
		/// <param name="y">y-coord of pixel, from 0 to 7</param>
		/// <returns>Pixel at (x,y).</returns>
		public GBColor this[int x, int y] {
			get {
				//Initialize pixels if needed.
				if (pixels == null) {
					pixels = new GBColor[8, 8];
				}
				
				if (x < 0 || x > 7) {
					throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and 7");
				}
				if (y < 0 || y > 7) {
					throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and 7");
				}
				return Pixels[x, y];
			}
			set {
				if (x < 0 || x > 7) {
					throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and 7");
				}
				if (y < 0 || y > 7) {
					throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and 7");
				}

				this.pixels[x, y] = value;
			}
		}
	}
}
