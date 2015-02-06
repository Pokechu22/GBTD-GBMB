using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GB.Shared.Tiles
{
	/// <summary>
	/// A single tile.
	/// </summary>
	public struct Tile
	{
		public readonly UInt16 Width;
		public readonly UInt16 Height;

		private GBColor[,] pixels;

		/// <summary>
		/// Creates a tile with the specified Width and Height.
		/// </summary>
		/// <param name="Width"></param>
		/// <param name="Height"></param>
		public Tile(UInt16 Width, UInt16 Height) {
			this.Width = Width;
			this.Height = Height;
			this.pixels = new GBColor[Width, Height];
		}

		/// <summary>
		/// Creates a tile with the specified pixels.
		/// </summary>
		/// <param name="pixels"></param>
		public Tile(GBColor[,] pixels) {
			this.pixels = (GBColor[,])pixels.Clone();
			this.Width = (UInt16)this.pixels.GetLength(0);
			this.Height = (UInt16)this.pixels.GetLength(1);
		}

		/// <summary>
		/// Creates a tile with the specified pixels of the specified width and height.
		/// </summary>
		/// <param name="pixels"></param>
		/// <param name="Width"></param>
		/// <param name="Height">The pixels to use, aranged left to right and then top to bottom.</param>
		public Tile(GBColor[] pixels, UInt16 Width, UInt16 Height) {
			this.Width = Width;
			this.Height = Height;
			this.pixels = new GBColor[Width, Height];

			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					this.pixels[x, y] = pixels[(y * Height) + x];
				}
			}
		}

		/// <summary>
		/// Sets the pixels.
		/// Pixels are cloned beforehand.
		/// </summary>
		/// <param name="pixels"></param>
		public void SetPixels(GBColor[,] pixels) {
			if (pixels.GetLength(0) != this.Width || pixels.GetLength(1) != this.Height) {
				throw new ArgumentException("Array is not of valid size; should be GBColor[" + this.Width + ", " + this.Height + "], but is actually a GBColor[" + pixels.GetLength(0) + ", " + pixels.GetLength(1) + "].", "pixels");
			}
			this.pixels = (GBColor[,])pixels.Clone();
		}

		/// <summary>
		/// Gets the pixels.
		/// This is seperate copy; modifications will not pass up.
		/// </summary>
		/// <returns></returns>
		public GBColor[,] GetPixels() {
			return (GBColor[,])pixels.Clone();
		}

		public static Tile FromImage(Image image, UInt16 Width, UInt16 Height) {
			if (image == null) {
				throw new ArgumentNullException();
			}
			
			Color BLACK = Color.FromArgb(0, 0, 0);
			Color DARK_GRAY = Color.FromArgb(128, 128, 128);
			Color LIGHT_GRAY = Color.FromArgb(192, 192, 192);
			Color WHITE = Color.FromArgb(255,255,255);

			Tile returned = new Tile(Width, Height);

			using (Bitmap bitmap = new Bitmap(image)) {
				for (int x = 0; x < returned.Width; x++) {
					for (int y = 0; y < returned.Height; y++) {
						if (x > bitmap.Width || y > bitmap.Height) {
							continue;
						}
						Color pixel = bitmap.GetPixel(x, y);

						if (pixel == BLACK) {
							returned[x, y] = GBColor.BLACK;
						} else if (pixel == DARK_GRAY) {
							returned[x, y] = GBColor.DARK_GRAY;
						} else if (pixel == LIGHT_GRAY) {
							returned[x, y] = GBColor.LIGHT_GRAY;
						} else if (pixel == WHITE) {
							returned[x, y] = GBColor.WHITE;
						} else {
							returned[x, y] = GBColor.WHITE; //TODO is this the best action?
						}
					}
				}
			}

			return returned;
		}

		public Bitmap ToImage() {
			Color BLACK = Color.FromArgb(0, 0, 0);
			Color DARK_GRAY = Color.FromArgb(128, 128, 128);
			Color LIGHT_GRAY = Color.FromArgb(192, 192, 192);
			Color WHITE = Color.FromArgb(255,255,255);

			Bitmap returned = new Bitmap(this.Width, this.Height);
			for (int x = 0; x < this.Width; x++) {
				for (int y = 0; y < this.Height; y++) {
					switch (this[x, y]) {
					case GBColor.BLACK: returned.SetPixel(x, y, BLACK); break;
					case GBColor.DARK_GRAY: returned.SetPixel(x, y, DARK_GRAY); break;
					case GBColor.LIGHT_GRAY: returned.SetPixel(x, y, LIGHT_GRAY); break;
					case GBColor.WHITE: returned.SetPixel(x, y, WHITE); break;
					default: returned.SetPixel(x, y, WHITE); break; //TODO: Again, best action?
					}
				}
			}

			return returned;
		}

		/// <summary>
		/// Sets/gets the specific pixels of the image.
		/// <para>Note: This preforms a clone of the pixels, which is expensive.  If you need to make many modifications, look at 
		/// <see cref="GetPixels"/> and <see cref="SetPixels"/>.</para>
		/// </summary>
		/// <param name="x">x-coord of pixel, from 0 to 7</param>
		/// <param name="y">y-coord of pixel, from 0 to 7</param>
		/// <returns>Pixel at (x,y).</returns>
		public GBColor this[int x, int y] {
			get {
				if (x < 0 || x >= Width) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and " + Width); }
				if (y < 0 || y >= Height) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and " + Height); }

				return pixels[x, y];
			}
			set {
				if (x < 0 || x > Width) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and " + Width); }
				if (y < 0 || y > Height) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and " + Height); }

				this.pixels = (GBColor[,])this.pixels.Clone();
				this.pixels[x, y] = value;
			}
		}

		/// <summary>
		/// Sets/gets the specific pixels of the image.
		/// <para>Note: This preforms a clone of the pixels, which is expensive.  If you need to make many modifications, look at 
		/// <see cref="GetPixels"/> and <see cref="SetPixels"/>.</para>
		/// </summary>
		/// <param name="x">x-coord of pixel, from 0 to 7</param>
		/// <param name="y">y-coord of pixel, from 0 to 7</param>
		/// <returns>Pixel at (x,y).</returns>
		public GBColor this[uint x, uint y] {
			get {
				if (x < 0 || x >= Width) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and " + Width); }
				if (y < 0 || y >= Height) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and " + Height); }

				return pixels[(int)x, (int)y];
			}
			set {
				if (x < 0 || x >= Width) { throw new ArgumentOutOfRangeException("x", x, "Pixel x coordinate must be between 0 and " + Width); }
				if (y < 0 || y >= Height) { throw new ArgumentOutOfRangeException("y", y, "Pixel y coordinate must be between 0 and " + Height); }

				this.pixels = (GBColor[,])this.pixels.Clone();
				this.pixels[(int)x, (int)y] = value;
			}
		}
	}
}
