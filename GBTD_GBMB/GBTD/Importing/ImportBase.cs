using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GB.Shared.GBRFile;
using GB.Shared.Tiles;

namespace GB.GBTD.Importing
{
	/// <summary>
	/// Provides a basis for the tile import types.
	/// </summary>
	public abstract class ImportBase
	{
		public void Import(Stream stream, GBRObjectTileData tileset, GBRObjectTileImport importSettings) {
			UInt16 oldWidth = tileset.Width;
			UInt16 oldHeight = tileset.Height;

			if (oldWidth != 8 || oldHeight != 8) {
				tileset.ResizeTiles(8, 8);
			}

			Tile[] readTiles = ReadTiles(stream, importSettings);

			for (int tile = 0; tile < importSettings.TileCount; tile++) {
				tileset.Tiles[tile + importSettings.FirstProgramTile] = readTiles[tile];
			}

			if (oldWidth != 8 || oldHeight != 8) {
				tileset.ResizeTiles(oldWidth, oldHeight);
			}
		}

		protected abstract Tile[] ReadTiles(Stream stream, GBRObjectTileImport importSettings);

		/// <summary>
		/// Reads an 8x8 tile in the "Byte per pixel" format.
		/// </summary>
		protected Tile ReadTileByteFormat(Stream stream) {
			GBColor[,] pixels = new GBColor[8, 8];

			for (int y = 0; y < 8; y++) {
				for (int x = 0; x < 8; x++) {
					pixels[x, y] = (GBColor)(stream.ReadByte() % 4);
				}
			}

			return new Tile(pixels);
		}

		/// <summary>
		/// Reads an 8x8 tile in the "2 bits per pixel" format.
		/// </summary>
		protected Tile ReadTile2BitFormat(Stream stream) {
			GBColor[,] pixels = new GBColor[8, 8];

			for (int y = 0; y < 8; y++) {
				byte[] bytes = new byte[2];

				if (stream.Read(bytes, 0, 2) != 2) {
					throw new EndOfStreamException();
				}

				pixels[0, y] = ConvertToGBColorSequental((bytes[0] & 0xC0) >> 6);
				pixels[1, y] = ConvertToGBColorSequental((bytes[0] & 0x30) >> 4);
				pixels[2, y] = ConvertToGBColorSequental((bytes[0] & 0x0C) >> 2);
				pixels[3, y] = ConvertToGBColorSequental((bytes[0] & 0x03) >> 0);
				pixels[4, y] = ConvertToGBColorSequental((bytes[1] & 0xC0) >> 6);
				pixels[5, y] = ConvertToGBColorSequental((bytes[1] & 0x30) >> 4);
				pixels[6, y] = ConvertToGBColorSequental((bytes[1] & 0x0C) >> 2);
				pixels[7, y] = ConvertToGBColorSequental((bytes[1] & 0x03) >> 0);
			}

			return new Tile(pixels);
		}

		/// <summary>
		/// Reads an 8x8 tile in the "Gameboy VRAM" format.
		/// </summary>
		protected Tile ReadTileVRAMFormat(Stream stream) {
			GBColor[,] pixels = new GBColor[8, 8];

			for (int y = 0; y < 8; y++) {
				byte[] bytes = new byte[2];

				if (stream.Read(bytes, 0, 2) != 2) {
					throw new EndOfStreamException();
				}

				pixels[0, y] = ConvertToGBColorVRAM(bytes[0] & 0x80, bytes[1] & 0x80);
				pixels[1, y] = ConvertToGBColorVRAM(bytes[0] & 0x40, bytes[1] & 0x40);
				pixels[2, y] = ConvertToGBColorVRAM(bytes[0] & 0x20, bytes[1] & 0x20);
				pixels[3, y] = ConvertToGBColorVRAM(bytes[0] & 0x10, bytes[1] & 0x10);
				pixels[4, y] = ConvertToGBColorVRAM(bytes[0] & 0x08, bytes[1] & 0x08);
				pixels[5, y] = ConvertToGBColorVRAM(bytes[0] & 0x04, bytes[1] & 0x04);
				pixels[6, y] = ConvertToGBColorVRAM(bytes[0] & 0x02, bytes[1] & 0x02);
				pixels[7, y] = ConvertToGBColorVRAM(bytes[0] & 0x01, bytes[1] & 0x01);
			}

			return new Tile(pixels);
		}

		/// <summary>
		/// Converts the given value to a GBColor, in the sequential format.
		/// 
		/// (00: White, 01: Light gray, 10: Dark gray, 11: black)
		/// </summary>
		protected GBColor ConvertToGBColorSequental(int b) {
			switch (b) {
			case 0: return GBColor.WHITE;
			case 1: return GBColor.LIGHT_GRAY;
			case 2: return GBColor.DARK_GRAY;
			case 3: return GBColor.BLACK;
			default: return (GBColor)b;
			}
		}

		/// <summary>
		/// Converts the given value to a GBColor, in VRAM format.
		/// 
		/// (00: White, 01: Dark gray, 10: Light gray, 11: black)
		/// </summary>
		protected GBColor ConvertToGBColorVRAM(int b1, int b2) {
			if (b1 != 0) {
				if (b2 != 0) {
					return GBColor.BLACK;
				} else {
					return GBColor.LIGHT_GRAY;
				}
			} else {
				if (b2 != 0) {
					return GBColor.DARK_GRAY;
				} else {
					return GBColor.WHITE;
				}
			}
		}
	}
}
