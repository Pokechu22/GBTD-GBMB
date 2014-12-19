using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.Tiles
{
	/// <summary>
	/// Reads or writes a tile as it appears in Gameboy VRAM: 
	/// For each row of pixels, 2 bytes.  
	/// </summary>
	public class VRAMTileParser : TileParser
	{
		public VRAMTileParser() : base() {
			
		}

		public VRAMTileParser(Stream source) : base() {
			ReadFromStream(source);
		}

		public VRAMTileParser(Byte[] source) : base() {
			ReadFromBytes(source);
		}

		protected internal static Byte[] writeTile(Tile tile) {
			Byte[] data = new Byte[16];
			for (byte y = 0; y <= 7; y++) {
				for (byte x = 0; x <= 7; x++) {
					GBColor color = tile[x,y];
					data[2 * y] |= (Byte)((((int)color & 0x01) != 0 ? 1 : 0) << x);
					data[(2 * y) + 1] |= (Byte)((((int)color & 0x02) != 0 ? 1 : 0) << x);
				}
			}

			return data;
		}

		protected internal static Tile readTile(Byte[] data) {
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

		public override void WriteToStream(Stream stream) {
			foreach (Tile tile in Tiles) {
				stream.Write(writeTile(tile), 0, 16);
			}
		}

		public override void ReadFromStream(Stream stream) {
			Byte[] tempBytes = new Byte[16];

			if ((stream.Length % 16) != 0) {
				throw new ArgumentException("Data length must be divisible evenly by 16 to parse!", "source");
			}

			//Read the next byte to the value, and as long as it isn't -1, loop.
			while (stream.Read(tempBytes, 0, 16) != 0) {
				Tiles.Add(readTile(tempBytes));
			}
		}
	}
}
