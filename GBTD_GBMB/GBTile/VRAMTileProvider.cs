using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.Tile
{
	/// <summary>
	/// Reads a tile as it appears in Gameboy VRAM: 
	/// For each row of pixels, 2 bytes.  
	/// </summary>
	public class VRAMTileProvider : TileProvider
	{
		private List<Tile> tiles = new List<Tile>();

		public VRAMTileProvider(Stream source) {
			Byte[] tempBytes = new Byte[16];

			int read;
			int index = 0;
			
			//Read the next byte to the value, and as long as it isn't -1, loop.
			while ((read = source.ReadByte()) != -1) {
				tempBytes[index] = (Byte)read;
				index++;
				if (index == 16) {
					tiles.Add(readTile(tempBytes));
					index = 0;
				}
			}

			if (index != 0) {
				throw new ArgumentException("Data length must be divisible evenly by 16 to parse!", "source");
			}
		}

		public VRAMTileProvider(Byte[] source) {
			if (source.Length % 16 != 0) {
				throw new ArgumentException("Data length must be divisible evenly by 16 to parse!", "source");
			}

			Byte[] tempBytes = new Byte[16];
			int index = 0;

			foreach (Byte b in source) {
				tempBytes[index] = b;
				index++;
				if (index == 16) {
					tiles.Add(readTile(tempBytes));
					index = 0;
				}
			}
		}

		public List<Tile> getTiles() {
			return new List<Tile>(this.tiles);
		}

		public int getNumberOfTiles() {
			return tiles.Count;
		}

		protected internal Tile readTile(Byte[] data) {
			if (data.Length != 16) {
				throw new ArgumentException("Data length must be exactly 16!", "data");
			}

			Tile t = new Tile();

			for (byte y = 0; y <= 7; y++) {
				for (byte x = 0; x <= 7; x++) {
					GBColor color = 0;
					color |= ((data[2 * y] & (1 << x)) != 0 ? (GBColor)1 : (GBColor)0);
					color |= ((data[(2 * y) + 1] & (1 << x)) != 0 ? (GBColor)2 : (GBColor)0);

					t[x, y] = color;
				}
			}

			return t;
		}
	}
}
