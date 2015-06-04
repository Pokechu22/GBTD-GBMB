using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.Tiles;
using GB.Shared.GBRFile;
using System.IO;

namespace GB.GBTD.Importing
{
	public class BinaryImport : ImportBase
	{
		protected override Tile[] ReadTiles(Stream stream, GBRObjectTileImport importSettings) {
			stream.Position = importSettings.FirstByte;

			Tile[] returned = new Tile[importSettings.TileCount];

			try {
				for (int i = 0; i < returned.Length; i++) {
					switch (importSettings.BinaryFileFormat) {
					case ImportBinaryFileFormat.BytePerPixel:
						returned[i + importSettings.FirstProgramTile] = ReadTileByteFormat(stream);
						break;
					case ImportBinaryFileFormat.TwoBitsPerPixel:
						returned[i + importSettings.FirstProgramTile] = ReadTile2BitFormat(stream);
						break;
					case ImportBinaryFileFormat.GameboyVRAM:
						returned[i + importSettings.FirstProgramTile] = ReadTileVRAMFormat(stream);
						break;
					}
				}
			} catch (EndOfStreamException) {
				//Ignore, as it is expected to encounter this.
			}

			return returned;
		}
	}
}
