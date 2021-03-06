﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using GB.Shared.GBRFile;

namespace GB.GBMB.Exporting
{
	/// <summary>
	/// Seperates data into planes for export.  TODO: This does not respect half-planes yet.
	/// </summary>
	public static class MapDataMaker
	{
		/// <summary>
		/// Gets the bytes for a specific tile.
		/// </summary>
		/// <returns></returns>
		public static byte[] GetBytesForTile(GBMFile gbmFile, GBRFile gbrFile, int x, int y, PlaneCount count) {
			var properties = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportProperties>();
			var mapData = gbmFile.GetOrCreateObjectOfType<GBMObjectMapTileData>();

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
					AddBits(ref temp, GetGBCPalette(gbrFile, mapData.Tiles[x, y]), position, property.Size);
					break;
				case 7: //SGB palette
					AddBits(ref temp, GetSGBPalette(gbrFile, mapData.Tiles[x, y]), position, property.Size);
					break;
				case 8: //GBC BG Attribute
					AddBits(ref temp, GetGBCBGAttribute(gbrFile, mapData.Tiles[x, y]), position, property.Size);
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

					var mapProperties = gbmFile.GetOrCreateObjectOfType<GBMObjectMapPropertyData>();
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
		/// Gets the tiles are continues bytes for the specified GBMFile.
		/// </summary>
		public static byte[][] GetTileContinuousData(GBMFile gbmFile, GBRFile gbrFile) {
			var settings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

			int planeCount = settings.PlaneCount.GetNumberOfPlanes();

			//Build the byte array data of data.
			Byte[] dataTemp = new Byte[settings.Master.Width * settings.Master.Height * planeCount];

			if (settings.MapLayout == MapLayout.Rows) {
				for (int y = 0; y < settings.Master.Height; y++) {
					for (int x = 0; x < settings.Master.Width; x++) {
						Byte[] tileData = MapDataMaker.GetBytesForTile(gbmFile, gbrFile, x, y, settings.PlaneCount);
						for (int i = 0; i < planeCount; i++) {
							dataTemp[(((y * settings.Master.Width) + x) * planeCount) + i] = tileData[i];
						}
					}
				}
			} else if (settings.MapLayout == MapLayout.Columns) {
				for (int x = 0; x < settings.Master.Width; x++) {
					for (int y = 0; y < settings.Master.Height; y++) {
						Byte[] tileData = MapDataMaker.GetBytesForTile(gbmFile, gbrFile, x, y, settings.PlaneCount);
						for (int i = 0; i < planeCount; i++) {
							dataTemp[(((x * settings.Master.Height) + y) * planeCount) + i] = tileData[i];
						}
					}
				}
			}

			Byte[][] data;

			if (settings.PlaneCount != PlaneCount.Half_Plane) {
				int numberOfSplitBlocks = (int)Math.Ceiling((double)(settings.Master.Width * settings.Master.Height) / settings.SplitSize);

				data = new Byte[numberOfSplitBlocks][];

				for (uint i = 0; i < numberOfSplitBlocks; i++) {
					uint length = (uint)(settings.SplitSize * planeCount);
					if (length > dataTemp.Length - (settings.SplitSize * planeCount * i)) {
						length = (uint)(dataTemp.Length - (settings.SplitSize * planeCount * i));
					}
					data[i] = new Byte[length];
					Array.Copy(dataTemp, settings.SplitSize * planeCount * i, data[i], 0, length);
				}
			} else { //Half-plane exporting.
				uint splitSize = (uint)Math.Ceiling(settings.SplitSize / 2.0);

				int numberOfSplitBlocks = (int)Math.Ceiling((double)(settings.Master.Width * settings.Master.Height) / (settings.SplitSize * 2));

				data = new Byte[numberOfSplitBlocks][];

				for (uint block = 0; block < numberOfSplitBlocks; block++) {

					uint length = splitSize;
					if (length > dataTemp.Length - (splitSize * block)) {
						length = (uint)(dataTemp.Length - (splitSize * block));
					}
					data[block] = new Byte[length];

					for (int i = 0; i < splitSize; i++) {
						data[block][i] = 0;

						if ((splitSize * block * 2) + (i * 2) < dataTemp.Length) {
							data[block][i] |= (byte)((dataTemp[(splitSize * block * 2) + (i * 2)] & 0x0f) << 4);
						}
						if ((splitSize * block * 2) + (i * 2) + 1 < dataTemp.Length) {	
							data[block][i] |= (byte)(dataTemp[(splitSize * block * 2) + (i * 2) + 1] & 0x0f);
						}
					}
				}
			}

			return data;
		}

		/// <summary>
		/// Gets the planes are continues bytes for the specified GBMFile.
		/// </summary>
		public static byte[,][] GetPlaneContinuousData(GBMFile gbmFile, GBRFile gbrFile) {
			var settings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

			int planeCount = settings.PlaneCount.GetNumberOfPlanes();

			Byte[][] planedDataTemp = new Byte[planeCount][];

			for (int i = 0; i < planeCount; i++) {
				planedDataTemp[i] = new byte[settings.Master.Width * settings.Master.Height];

				if (settings.MapLayout == MapLayout.Rows) {
					for (int y = 0; y < settings.Master.Height; y++) {
						for (int x = 0; x < settings.Master.Width; x++) {
							Byte[] tileData = MapDataMaker.GetBytesForTile(gbmFile, gbrFile, x, y, settings.PlaneCount);

							planedDataTemp[i][((y * settings.Master.Width) + x)] = tileData[i];
						}
					}
				} else if (settings.MapLayout == MapLayout.Columns) {
					for (int x = 0; x < settings.Master.Width; x++) {
						for (int y = 0; y < settings.Master.Height; y++) {
							Byte[] tileData = MapDataMaker.GetBytesForTile(gbmFile, gbrFile, x, y, settings.PlaneCount);

							planedDataTemp[i][((x * settings.Master.Height) + y)] = tileData[i];
						}
					}
				}
			}

			Byte[,][] planedData;

			if (settings.PlaneCount != PlaneCount.Half_Plane) {
				int numberOfSplitBlocks = (int)Math.Ceiling((double)(settings.Master.Width * settings.Master.Height) / settings.SplitSize);

				planedData = new Byte[planeCount, numberOfSplitBlocks][];

				for (int plane = 0; plane < planeCount; plane++) {
					for (uint block = 0; block < numberOfSplitBlocks; block++) {
						uint length = settings.SplitSize;
						if (length > planedDataTemp[plane].Length - (settings.SplitSize * block)) {
							length = (uint)(planedDataTemp[plane].Length - (settings.SplitSize * block));
						}
						planedData[plane, block] = new Byte[length];
						Array.Copy(planedDataTemp[plane], settings.SplitSize * block, planedData[plane, block], 0, length);
					}
				}
			} else { //Half-planes
				uint splitSize = (uint)Math.Ceiling(settings.SplitSize / 2.0);

				int numberOfSplitBlocks = (int)Math.Ceiling((double)(settings.Master.Width * settings.Master.Height) / (settings.SplitSize * 2));

				planedData = new Byte[1, numberOfSplitBlocks][];

				const uint plane = 0; //Current plane; will never change on .5 planes.

				for (uint block = 0; block < numberOfSplitBlocks; block++) {

					uint length = splitSize;
					if (length > planedDataTemp[plane].Length - (splitSize * block)) {
						length = (uint)(planedDataTemp[plane].Length - (splitSize * block));
					}
					planedData[plane, block] = new Byte[length];

					for (int i = 0; i < splitSize; i++) {
						planedData[plane, block][i] = 0;

						if ((splitSize * block * 2) + (i * 2) < planedDataTemp[plane].Length) {
							planedData[plane, block][i] |= (byte)((planedDataTemp[plane][(splitSize * block * 2) + (i * 2)] & 0x0f) << 4);
						}
						if ((splitSize * block * 2) + (i * 2) + 1 < planedDataTemp[plane].Length) {
							planedData[plane, block][i] |= (byte)(planedDataTemp[plane][(splitSize * block * 2) + (i * 2) + 1] & 0x0f);
						}
					}
				}
			}

			return planedData;
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
		private static UInt16 GetGBCPalette(GBRFile gbrFile, GBMObjectMapTileDataRecord tile) {
			if (tile.GBCPalette != null) {
				return tile.GBCPalette.Value;
			} else {
				return (UInt16)gbrFile.GetObjectOfType<GBRObjectTilePalette>().GBCPalettes[tile.TileNumber];
			}
		}

		/// <summary>
		/// Converts a palette to the apropriate value, using default if needed.
		/// </summary>
		private static UInt16 GetSGBPalette(GBRFile gbrFile, GBMObjectMapTileDataRecord tile) {
			if (tile.SGBPalette != null) {
				return tile.SGBPalette.Value;
			} else {
				return (UInt16)gbrFile.GetObjectOfType<GBRObjectTilePalette>().SGBPalettes[tile.TileNumber];
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
		private static Byte GetGBCBGAttribute(GBRFile gbrFile, GBMObjectMapTileDataRecord tile) {
			Byte result = 0;

			result |= (byte)(false ? 0x80 : 0x00); //TODO actual priority value, which doesn't exist yet.
			result |= (byte)(tile.FlippedVertically ? 0x40 : 0x00);
			result |= (byte)(tile.FlippedHorizontally ? 0x20 : 0x00);

			result |= (byte)(((tile.TileNumber & 0x100) != 0) ? 0x08 : 0x00); //If the tile number has bit 8 set, it's in the second bank.
			result |= (byte)(GetGBCPalette(gbrFile, tile) & 0x07);

			return result;
		}
	}
}
