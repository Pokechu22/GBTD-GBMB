using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;

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
	}
}
