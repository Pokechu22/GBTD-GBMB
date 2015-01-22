﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.Tiles
{
	/// <summary>
	/// A single tileData.
	/// </summary>
	public struct Tile
	{
		#region Bad code
		/// <summary>
		/// Stupid code to avoid an array.
		/// Using an array makes modifications to duplicated tiles change through all, which is not what I want.
		/// </summary>
		private struct PixelGroup8x8
		{
			private struct PixelGroup4x4
			{
				private struct PixelGroup2x2
				{
					/// <summary>
					/// Groups of each set of pixels.
					/// </summary>
					private GBColor _0_0, _0_1, _1_0, _1_1;

					public GBColor this[int x, int y] {
						get {
							const int BIT = 0x01;

							if ((x & BIT) == 0) {
								if ((y & BIT) == 0) { return _0_0; } else { return _0_1; }
							} else {
								if ((y & BIT) == 0) { return _1_0; } else { return _1_1; }
							}
						}
						set {
							const int BIT = 0x01;

							if ((x & BIT) == 0) {
								if ((y & BIT) == 0) { _0_0 = value; } else { _0_1 = value; }
							} else {
								if ((y & BIT) == 0) { _1_0 = value; } else { _1_1 = value; }
							}
						}
					}
				}

				/// <summary>
				/// Groups of each set of pixels: _x_y
				/// </summary>
				private PixelGroup2x2 _0_0, _0_1, _1_0, _1_1;

				public GBColor this[int x, int y] {
					get {
						const int BIT = 0x02;

						if ((x & BIT) == 0) {
							if ((y & BIT) == 0) { return _0_0[x, y]; } else { return _0_1[x, y]; }
						} else {
							if ((y & BIT) == 0) { return _1_0[x, y]; } else { return _1_1[x, y]; }
						}
					}
					set {
						const int BIT = 0x02;

						if ((x & BIT) == 0) {
							if ((y & BIT) == 0) { _0_0[x, y] = value; } else { _0_1[x, y] = value; }
						} else {
							if ((y & BIT) == 0) { _1_0[x, y] = value; } else { _1_1[x, y] = value; }
						}
					}
				}
			}

			/// <summary>
			/// Groups of each set of pixels: _x_y
			/// </summary>
			private PixelGroup4x4 _0_0, _0_1, _1_0, _1_1;

			public static implicit operator PixelGroup8x8(GBColor[,] pixels) {
				if (pixels.GetLength(0) != 8 || pixels.GetLength(1) != 8) {
					throw new ArgumentOutOfRangeException("pixels", pixels, "must be an [8,8] array");
				}

				PixelGroup8x8 returned = new PixelGroup8x8();

				for (int x = 0; x < 8; x++) {
					for (int y = 0; y < 8; y++) {
						returned[x, y] = pixels[x, y];
					}
				}

				return returned;
			}

			public static implicit operator GBColor[,](PixelGroup8x8 pixels) {
				GBColor[,] returned = new GBColor[8, 8];

				for (int x = 0; x < 8; x++) {
					for (int y = 0; y < 8; y++) {
						returned[x, y] = pixels[x, y];
					}
				}

				return returned;
			}

			public GBColor this[int x, int y] {
				get {
					const int BIT = 0x04;

					if ((x & BIT) == 0) {
						if ((y & BIT) == 0) { return _0_0[x, y]; } else { return _0_1[x, y]; }
					} else {
						if ((y & BIT) == 0) { return _1_0[x, y]; } else { return _1_1[x, y]; }
					}
				}
				set {
					const int BIT = 0x04;

					if ((x & BIT) == 0) {
						if ((y & BIT) == 0) { _0_0[x, y] = value; } else { _0_1[x, y] = value; }
					} else {
						if ((y & BIT) == 0) { _1_0[x, y] = value; } else { _1_1[x, y] = value; }
					}
				}
			}
		}
		#endregion

		private PixelGroup8x8 pixels;

		/// <summary>
		/// Pixels on the tileData.  MUST BE 8 by 8 exactly.
		/// </summary>
		[Obsolete("Can't be modified directly, and thus discards changes.  Use indexer instead.")]
		public GBColor[,] Pixels {
			get {
				return pixels;
			}
			set {
				if (value == null) { throw new ArgumentNullException("value"); }
				if (value.GetLength(0) != 8 || value.GetLength(1) != 8) {
					throw new ArgumentOutOfRangeException("value", value, "must be an [8,8] array");
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
				if (x < 0 || x > 7) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and 7"); }
				if (y < 0 || y > 7) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and 7"); }

				return pixels[x, y];
			}
			set {
				if (x < 0 || x > 7) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and 7"); }
				if (y < 0 || y > 7) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and 7"); }
				
				this.pixels[x, y] = value;
			}
		}

		/// <summary>
		/// Sets/gets the specific pixels of the image.
		/// </summary>
		/// <param name="x">x-coord of pixel, from 0 to 7</param>
		/// <param name="y">y-coord of pixel, from 0 to 7</param>
		/// <returns>Pixel at (x,y).</returns>
		public GBColor this[uint x, uint y] {
			get {
				if (x < 0 || x > 7) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and 7"); }
				if (y < 0 || y > 7) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and 7"); }

				return pixels[(int)x, (int)y];
			}
			set {
				if (x < 0 || x > 7) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and 7"); }
				if (y < 0 || y > 7) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and 7"); }

				this.pixels[(int)x, (int)y] = value;
			}
		}
	}
}
