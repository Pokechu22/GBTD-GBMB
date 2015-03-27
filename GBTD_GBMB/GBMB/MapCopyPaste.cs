using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.ComponentModel;

namespace GB.GBMB
{
	/// <summary>
	/// Provides copy-paste functionality for maps.
	/// 
	/// <para>COPY-PASTE FORMAT:</para>
	/// 
	/// <para>
	/// The copy paste format is relatively simple.  It takes the map data into several properties -- those
	/// of GBMObjectMapTileDataRecord plus any user-defined ones -- into several "sets".  Each set is composed of tab-seperated columns
	/// and newline-seperated rows; each entry contains one number.
	/// </para>
	/// <para>
	/// The order: 
	/// * TileNumber
	/// * FlippedVertically
	/// * FlippedHorizontally
	/// * GBCPal
	/// * SGBPal
	/// * User properties.
	/// </para>
	/// </summary>
	public static class MapCopyPaste
	{
		const string SET_DELIMETER = "\r\n\r\n";
		const string ROW_DELIMETER = "\r\n";
		const string ENTRY_DELIMETER = "\t";
		const string TRUE = "1";
		const string FALSE = "0";
		//The values to use for SGB and CGB when the palette is default.
		const byte DEFAULT_SGB = 4;
		const byte DEFAULT_GBC = 8;

		public static string ToCopyPasteString(this GBMObjectMapTileDataRecord[,] tiles) {
			StringBuilder returned = new StringBuilder();

			WriteSet(returned, tiles, tile => tile.TileNumber.ToString());
			WriteSet(returned, tiles, tile => tile.FlippedVertically ? TRUE : FALSE);
			WriteSet(returned, tiles, tile => tile.FlippedHorizontally ? TRUE : FALSE);
			WriteSet(returned, tiles, tile => (tile.GBCPalette ?? DEFAULT_GBC).ToString());
			WriteSet(returned, tiles, tile => (tile.SGBPalette ?? DEFAULT_SGB).ToString());

			return returned.ToString();
		}

		public static GBMObjectMapTileDataRecord[,] FromCopyPasteString(String s) {
			GBMObjectMapTileDataRecord[,] returned;

			String[] sets = s.Split(SET_DELIMETER);

			//Measure the size of rows and columns.
			int numberOfSets, numberOfRows, numberOfCols;
			{
				numberOfSets = sets.Length;
				if (numberOfSets < 5) {
					throw new WarningException("Copied data did not contain enough sets -- expected at least 5, found " + numberOfSets + ".\n" +
						"(Is this even tile data or is it random garbage?)");
				}

				var rows = sets[0].Split(ROW_DELIMETER);
				numberOfRows = rows.Length;
				if (numberOfRows == 0) {
					throw new WarningException("Copied data doesn't contain any rows!\n" +
						"(Is this even tile data or is it random garbage?)");
				}
					
				var cols = rows[0].Split(ENTRY_DELIMETER);
				numberOfCols = cols.Length;
				if (numberOfCols == 0) {
					throw new WarningException("Copied data doesn't contain any columns!\n" +
						"(Is this even tile data or is it random garbage?)");
				}
			}

			returned = new GBMObjectMapTileDataRecord[numberOfCols, numberOfRows];

			ReadSet(sets, 0, returned, (rec, val) => { rec.TileNumber = UInt16.Parse(val); return rec; });
			ReadSet(sets, 1, returned, (rec, val) => { rec.FlippedVertically = (val == TRUE); return rec; });
			ReadSet(sets, 2, returned, (rec, val) => { rec.FlippedHorizontally = (val == TRUE); return rec; });
			ReadSet(sets, 3, returned, (rec, val) => {
				byte? parsed = byte.Parse(val);
				rec.GBCPalette = (parsed != DEFAULT_GBC ? parsed : null);
				return rec;
			});
			ReadSet(sets, 4, returned, (rec, val) => {
				byte? parsed = byte.Parse(val);
				rec.SGBPalette = (parsed != DEFAULT_SGB ? parsed : null);
				return rec;
			});
			
			return returned;
		}

		/// <summary>
		/// Simplifies spliting of strings, because an array must be created otherwise.
		/// </summary>
		private static String[] Split(this String s, String delimeter) {
			return s.Split(new String[] { delimeter }, StringSplitOptions.None);
		}

		/// <summary>
		/// Writes a set to the builder.
		/// </summary>
		/// <param name="builder">The builder to write</param>
		/// <param name="tiles">The tiles to use.</param>
		/// <param name="prop">A function that takes a record and gets the right property from it.</param>
		private static void WriteSet(StringBuilder builder, GBMObjectMapTileDataRecord[,] tiles, Func<GBMObjectMapTileDataRecord, String> prop) {
			int tilesWidth = tiles.GetLength(0);
			int tilesHeight = tiles.GetLength(1);

			for (int y = 0; y < tilesHeight; y++) {
				for (int x = 0; x < tilesWidth; x++) {
					builder.Append(prop(tiles[x, y]));

					if (x != tilesWidth - 1) {
						builder.Append(ENTRY_DELIMETER);
					}
				}

				if (y == tilesHeight - 1) { //If on the last row, append the set delimeter; otherwise, do the row delimeter.
					builder.Append(SET_DELIMETER);
				} else {
					builder.Append(ROW_DELIMETER);
				}
			}
		}

		/// <summary>
		/// Reads a set.
		/// </summary>
		/// <param name="set">The data to read.</param>
		/// <param name="data">The data to write into.</param>
		/// <param name="parser">Parses and stores the data from the string into the record.  (Return the passed record with changes)</param>
		private static void ReadSet(String[] sets, int setNum, GBMObjectMapTileDataRecord[,] data,
				Func<GBMObjectMapTileDataRecord, String, GBMObjectMapTileDataRecord> parser) {

			int rowSize = data.GetLength(1);
			int colSize = data.GetLength(0);

			String[] rows = sets[setNum].Split(ROW_DELIMETER);

			if (rows.Length != rowSize) {
				throw new WarningException("Set #" + setNum + " did not have the expected number of rows - expected " + rowSize +
					", got " + rows.Length + ".\n(Is this even tile data or just random garbage?)");
			}
			
			for (int y = 0; y < rowSize; y++) {
				String[] entries = rows[y].Split(ENTRY_DELIMETER);

				if (entries.Length != colSize) {
					throw new WarningException("Set #" + setNum + "'s row " + y + " did not have the expected number of entires - expected " 
						+ colSize + ", got " + entries.Length + ".\n(Is this even tile data or just random garbage?)");
				}

				for (int x = 0; x < colSize; x++) {
					try {
						data[x, y] = parser(data[x, y], entries[x]);
					} catch (Exception e) {
						throw new WarningException("Set #" + setNum + "'s row " + y + "'s entry " + x + " threw an exception while parsing.\n" +
							"(Is this even tile data or just random garbage?)", e);
					}
				}
			}
		}
	}
}
