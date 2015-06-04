using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.Tiles;
using GB.Shared.GBRFile;
using System.IO;

namespace GB.GBTD.Importing
{
	/// <summary>
	/// Imports from a GBE file.  I do not know what a GBE file is.
	/// </summary>
	public class GBEImport : ImportBase
	{
		protected override Tile[] ReadTiles(Stream stream, GBRObjectTileImport importSettings) {
			//TODO: Load the GB palette data (see IMPORT.PAS in GBTD, 312-323).

			stream.Position = 768 + (importSettings.FirstImportFileTile * 8 * 8);

			Tile[] returned = new Tile[importSettings.TileCount];

			for (int i = 0; i < importSettings.TileCount; i++) {
				returned[i + importSettings.FirstProgramTile] = ReadTileByteFormat(stream);
			}

			return returned;
		}
	}
}
