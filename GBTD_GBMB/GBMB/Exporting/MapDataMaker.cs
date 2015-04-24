using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;

namespace GB.GBMB.Exporting
{
	public static class MapDataMaker
	{
		/// <summary>
		/// Gets the bytes for a specific tile.
		/// </summary>
		/// <param name="gbmFile"></param>
		/// <returns></returns>
		public static byte[] GetBytesForTile(GBMFile gbmFile, int x, int y, PlaneCount count) {
			var properties = gbmFile.GetObjectOfType<GBMObjectMapExportProperties>();
			var mapData = gbmFile.GetObjectOfType<GBMObjectMapTileData>();

			ulong temp = 0;
			uint position = 0;

			foreach (var property in properties.Data) {
				switch (property.Property) {
				case 0: break; //Do nothing.
				case 1: //Tile number
				case 2: //Tile number: Low 8
				case 3: //Tile number: High 9
					AddBits(ref temp, (uint)(mapData.Tiles[x, y].TileNumber + properties.Master.TileOffset), position, property.Size);
					break;
				case 4: //Vertical flip
					AddBits(ref temp, GetNumericValue(mapData.Tiles[x, y].FlippedVertically), position, property.Size);
					break;
				case 5: //Horizontal flip
					AddBits(ref temp, GetNumericValue(mapData.Tiles[x, y].FlippedHorizontally), position, property.Size);
					break;
				case 6: //GBC palette.
					AddBits(ref temp, GetGBCPalette(gbmFile, mapData.Tiles[x, y]), position, property.Size);
					break;
				case 7: //SGB palette
					AddBits(ref temp, GetSGBPalette(gbmFile, mapData.Tiles[x, y]), position, property.Size);
					break;
				case 8: //GBC BG Attribute
					AddBits(ref temp, GetGBCBGAttribute(gbmFile, mapData.Tiles[x, y]), position, property.Size);
					break;
				case 9: //0 filler
					AddBits(ref temp, GetNumericValue(false), position, property.Size);
					break;
				case 10: //1 filler
					AddBits(ref temp, GetNumericValue(true), position, property.Size);
					break;
				default:
					int propNum = property.Property - 11;
					if (propNum < 0 || propNum >= 32) {
						throw new IndexOutOfRangeException("Could not parse the property value -- property is beyond the maximum property count (0 to 31) and is not a specific other property.  ExportProperty is " + property.Property + ", and tried to get " + propNum + " as the property.");
					}

					var mapProperties = gbmFile.GetObjectOfType<GBMObjectMapPropertyData>();
					if (propNum >= mapProperties.Data.GetLength(2)) {
						throw new IndexOutOfRangeException("Could not parse the property value -- property is beyond the total current property count and is not a specific other property.  ExportProperty is " + property.Property + ", and tried to get " + propNum + " as the property.  Length of the 3rd dimension of GBMObjectMapPropertyData's data array is " + mapProperties.Data.GetLength(2) + ", must be less than that.");
					}

					AddBits(ref temp, mapProperties.Data[x, y, propNum], position, property.Size);
					break;
				}

				position += property.Size;
			}

			byte[] result = new byte[count.GetNumberOfPlanes()];

			for (int i = 0; i < result.Length; i++) {
				result[i] = (byte)(temp & 0xFF);
				temp >>= 8;
			}

			return result;
		}

		/// <summary>
		/// Adds the specified numbers to the end result.
		/// </summary>
		private static void AddBits(ref ulong value, uint number, uint firstBit, uint size) {
			number &= CreateBitMask(size);

			value |= (number << (int)firstBit);
		}

		/// <summary>
		/// Creates a bitmask of the specified length.
		/// </summary>
		private static uint CreateBitMask(uint size) {
			uint result = 0;
			for (int i = 0; i < size; i++) {
				result <<= 1;
				result |= 1;
			}

			return result;
		}

		/// <summary>
		/// Creates a numeric value for the bool -- specificially either fully set or fully unset.
		/// </summary>
		private static uint GetNumericValue(bool b) {
			return b ? 0xFFFFFFFFu : 0x00000000u;
		}

		/// <summary>
		/// Converts a palette to the apropriate value, using default if needed.
		/// </summary>
		private static UInt16 GetGBCPalette(GBMFile gbmFile, GBMObjectMapTileDataRecord tile) {
			if (tile.GBCPalette != null) {
				return tile.GBCPalette.Value;
			} else {
				return 0; //TODO get defaults from GBRFile.
			}
		}

		/// <summary>
		/// Converts a palette to the apropriate value, using default if needed.
		/// </summary>
		private static UInt16 GetSGBPalette(GBMFile gbmFile, GBMObjectMapTileDataRecord tile) {
			if (tile.SGBPalette != null) {
				return tile.SGBPalette.Value;
			} else {
				return 0; //TODO get defaults from GBRFile.
			}
		}

		/// <summary>
		/// Gets the GBC Background Attribute for the given tile.
		/// 
		/// <para>From the GBDK docs: </para>
		/// 
		/// <para>Bit 7 - Priority flag. When this is set, it puts the tile above the sprites
    	/// with colour 0 being transparent. 0: below sprites, 1: above sprites</para>
		/// <para>Bit 6 - Vertical flip. Dictates which way up the tile is drawn vertically.
    	/// 0: normal, 1: upside down.</para>
		/// <para>Bit 5 - Horizontal flip. Dictates which way up the tile is drawn
    	/// horizontally. 0: normal, 1:back to front.</para>
		/// <para>Bit 4 - Not used.</para>
		/// <para>Bit 3 - Character Bank specification. Dictates from which bank of
    	/// Background Tile Patterns the tile is taken. 0: Bank 0, 1: Bank 1</para>
		/// <para>Bit 2 - See bit 0.</para>
		/// <para>Bit 1 - See bit 0.</para>
		/// <para>Bit 0 - Bits 0-2 indicate which of the 7 BKG colour palettes the tile is
		/// assigned.</para>
		/// </summary>
		private static Byte GetGBCBGAttribute(GBMFile gbmFile, GBMObjectMapTileDataRecord tile) {
			Byte result = 0;

			result |= (byte)(false ? 0x80 : 0x00); //TODO actual priority value, which doesn't exist yet.
			result |= (byte)(tile.FlippedVertically ? 0x40 : 0x00);
			result |= (byte)(tile.FlippedHorizontally ? 0x20 : 0x00);

			result |= (byte)(((tile.TileNumber & 0x100) != 0) ? 0x08 : 0x00); //If the tile number has bit 8 set, it's in the second bank.
			result |= (byte)(GetGBCPalette(gbmFile, tile) & 0x07);

			return result;
		}
	}
}
