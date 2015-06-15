using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.Tiles;
using GB.Shared.GBRFile;

namespace GB.GBTD.Exporting
{
	/// <summary>
	/// Converts tile data into useful info.
	/// </summary>
	public static class TileDataMaker
	{
		/// <summary>
		/// Converts the tile to its bytes format (no compression is currently attempted).
		/// </summary>
		/// <param name="tile"></param>
		/// <param name="exportSettings"></param>
		/// <returns></returns>
		public static byte[] GetTileBytes(Tile tile, GBRObjectTileExport exportSettings) {
			int subTilesX = tile.Width / 8;
			byte[] data;

			switch (exportSettings.Format) {
			case ExportFormat.GameBoy4Color:
				data = new byte[2 * tile.Height];

				for (int y = 0; y < tile.Height; y++) {
					for (int x = 0; x < tile.Width; x++) {
						int xTile = x / 8;

						data[(((y * subTilesX) + xTile) * 2) + 0] <<= 1;
						data[(((y * subTilesX) + xTile) * 2) + 1] <<= 1;

						data[((y * subTilesX) + xTile) * 2 + 0] |= (byte)((tile[x, y] == GBColor.LIGHT_GRAY || tile[x, y] == GBColor.BLACK) ? 1 : 0);
						data[((y * subTilesX) + xTile) * 2 + 1] |= (byte)((tile[x, y] == GBColor.DARK_GRAY || tile[x, y] == GBColor.BLACK) ? 1 : 0);
					}
				}

				return data;
			case ExportFormat.GameBoy2Color:
				data = new byte[tile.Height];

				for (int y = 0; y < tile.Height; y++) {
					for (int x = 0; x < tile.Width; x++) {
						int xTile = x / 8;

						data[(y * subTilesX) + xTile] <<= 1;
						data[(y * subTilesX) + xTile] |= (byte)(tile[x, y] == GBColor.BLACK ? 1 : 0);
					}
				}

				return data;
			case ExportFormat.BytePerColor:
				data = new byte[tile.Width * tile.Height];

				for (int y = 0; y < tile.Height; y++) {
					for (int x = 0; x < tile.Width; x++) {
						byte b;

						switch (tile[x, y]) {
						case GBColor.WHITE: b = 0; break;
						case GBColor.DARK_GRAY: b = 1; break;
						case GBColor.LIGHT_GRAY: b = 2; break;
						case GBColor.BLACK: b = 3; break;
						default: b = (byte)tile[x, y]; break;
						}

						data[x + (y * tile.Width)] = b;
					}
				}

				return data;
			case ExportFormat.ConsecutiveFourColor:
				data = new byte[2 * tile.Width];

				for (int y = 0; y < tile.Height; y++) {
					for (int x = 0; x < tile.Width; x++) {
						int xTile = x / 4;

						data[((y * subTilesX * 2) + xTile)] <<= 2;

						byte b = 0;
						switch (tile[x, y]) {
						case GBColor.WHITE: b = 0; break;
						case GBColor.LIGHT_GRAY: b = 1; break;
						case GBColor.DARK_GRAY: b = 2; break;
						case GBColor.BLACK: b = 3; break;
						}

						data[((y * subTilesX * 2) + xTile)] |= b;
					}
				}

				return data;
			default:
				throw new Exception("ExportSetting's format " + exportSettings.Format + "(" + (int)exportSettings.Format + ") is invalid!");
			}
		}

		/// <summary>
		/// Converts a color to the GBC 16-bit version.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static UInt16 RGB(int r, int g, int b) {
			return (UInt16)((r >> 3) | ((g >> 3) << 5) | ((b >> 3) << 10));
		}
	}
}
