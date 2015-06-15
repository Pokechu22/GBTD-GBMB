using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBRFile;
using System.IO;
using System.Windows.Forms;
using GB.Shared.Tiles;

namespace GB.GBTD.Exporting
{
	/// <summary>
	/// Base class for any text-based file outputs.
	/// </summary>
	public abstract class TileExportTextBase : ITileExporter
	{
		protected TileExportTextBase() { }

		/// <summary>
		/// The stream that is being written to.
		/// </summary>
		protected StreamWriter Stream { get; private set; }

		private bool header;
		/// <summary>
		/// Whether it is the header file that is currently being written.
		/// </summary>
		protected bool Header {
			get {
				if (Stream == null) { throw new InvalidOperationException("Not currently exporting!  Stream is null."); }
				return header;
			}
			private set { header = value; }
		}

		/// <summary>
		/// Text that should be put at the begining of the header block.  EG for C, "<c>/*</c>".
		/// </summary>
		protected abstract string HeaderBegin { get; }
		/// <summary>
		/// Text that should be put at the start of each line of the header block.  EG for ASM, "<c>; </c>".
		/// </summary>
		protected abstract string HeaderLine { get; }
		/// <summary>
		/// Text that should be put at the end of the header block.  EG for C, "<c>*/</c>".
		/// </summary>
		protected abstract string HeaderEnd { get; }

		/// <summary>
		/// The text that starts a block.  EG for C, "<c>{</c>".
		/// </summary>
		protected abstract string BlockBegin { get; }
		/// <summary>
		/// The text that ends a block.  EG for C, "<c>};</c>".
		/// </summary>
		protected abstract string BlockEnd { get; }

		/// <summary>
		/// Whether or not to inlcude the "Section" and "Bank" text on the header.  True for RGBDS only.
		/// </summary>
		protected abstract bool IncludeSectionAndBank { get; }

		public virtual bool SupportsExportMain { get { return true; } }
		public virtual bool SupportsExportInclude { get { return true; } }

		public void ExportMain(GBRFile gbrFile, Stream stream, String fileName) {
			if (!SupportsExportMain) {
				throw new InvalidOperationException("This tile exporter does not support exporting main files!");
			}

			Header = false;
			
			using (this.Stream = new StreamWriter(stream)) {
				var exportSettings = gbrFile.GetOrCreateObjectOfType<GBRObjectTileExport>();
				var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

				WriteHeader(exportSettings, tileData, fileName);
				Stream.WriteLine();
				WriteSection(exportSettings, 0);
				WriteTileData(gbrFile);

				Stream.WriteLine();
				WriteLineComment("End of " + Path.GetFileName(fileName).ToUpperInvariant());
			}

			this.Stream = null;
		}

		public void ExportInclude(GBRFile gbrFile, Stream stream, String fileName) {
			if (!SupportsExportInclude) {
				throw new InvalidOperationException("This tile exporter does not support exporting include files!");
			}

			Header = true;

			using (this.Stream = new StreamWriter(stream)) {
				var exportSettings = gbrFile.GetOrCreateObjectOfType<GBRObjectTileExport>();
				var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

				WriteHeader(exportSettings, tileData, fileName);
				Stream.WriteLine();
				WriteSection(exportSettings, 0);
				WriteMapDataIncludes(gbrFile);

				Stream.WriteLine();
				WriteLineComment("End of " + Path.GetFileName(fileName).ToUpperInvariant());
			}

			this.Stream = null;
		}

		/// <summary>
		/// Writes the header at the top that includes version information and such.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="fileName"></param>
		protected void WriteHeader(GBRObjectTileExport settings, GBRObjectTileData tileData, String fileName) {
			if (!String.IsNullOrEmpty(HeaderBegin)) {
				Stream.WriteLine(HeaderBegin);
			}
			Stream.WriteLine(HeaderLine + " " + Path.GetFileName(fileName).ToUpperInvariant());
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + " " + (Header ? "Include File." : "Tile Source File."));
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + " Info:");
			if (IncludeSectionAndBank) {
				Stream.WriteLine(HeaderLine + "  Section              : " + settings.SectionName);
				Stream.WriteLine(HeaderLine + "  Bank                 : " + settings.Bank);
			}
			Stream.WriteLine(HeaderLine + "  Form                 : " + 
				(settings.CreateArrayForEachTile ? "All tiles as one unit." : "Each tile separate."));
			Stream.WriteLine(HeaderLine + "  Format               : " + settings.Format.GetDisplayString() + ".");
			Stream.WriteLine(HeaderLine + "  Compression          : " + settings.CompressionType.GetDisplayString() + ".");
			Stream.WriteLine(HeaderLine + "  Counter              : " + settings.CounterType.GetDisplayString() + ".");
			Stream.WriteLine(HeaderLine + "  Tile size            : " + tileData.Width + " x " + tileData.Height);
			Stream.WriteLine(HeaderLine + "  Tiles                : " + settings.FromTile + " to " + settings.ToTile);
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + "  Palette colors       : " + (settings.IncludeColors ? "Included." : "None."));
			Stream.WriteLine(HeaderLine + "  SGB Palette          : " + settings.SGBPalMode.GetDisplayString() + ".");
			Stream.WriteLine(HeaderLine + "  CGB Palette          : " + settings.GBCPalMode.GetDisplayString() + ".");
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + "  Convert to metatiles : " + (settings.MakeMetaTiles ? "Yes." : "No."));
			if (settings.MakeMetaTiles) {
				Stream.WriteLine(HeaderLine + "  Index offset       : " + (settings.MetaTileOffset));
				Stream.WriteLine(HeaderLine + "  Index counter      : " + settings.MetaCounterFormat.GetDisplayString() + ".");
			}
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + " This file was generated by GBTD in C# v " + Application.ProductVersion);
			if (!String.IsNullOrEmpty(HeaderEnd)) {
				Stream.WriteLine(HeaderEnd);
			}
		}

		/// <summary>
		/// Writes a section label.  Only relevant for ASM; by default this method does nothing.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="bankOffset"></param>
		protected virtual void WriteSection(GBRObjectTileExport settings, int bankOffset) {
			//Do nothing by default.
		}

		/// <summary>
		/// Writes a label used for a tile.
		/// </summary>
		/// <param name="settings">General settings.</param>
		/// <param name="tileNum">
		/// The current tile number, or null if tileNum doesn't need to be included.
		/// This will increment regardless of the block value.
		/// </param>
		/// <param name="block">The current block, or null if the block doesn't need to be included.</param>
		protected virtual void WriteTileLabel(GBRObjectTileExport settings, int? tileNum = null, int? block = null) {
			String label;

			if (tileNum != null) {
				if (block != null) {
					label = String.Format("{0}BLK{1}TLE{2}", settings.LabelName, block, tileNum);
				} else {
					label = String.Format("{0}TLE{1}", settings.LabelName, tileNum);
				}
			} else {
				WriteLineComment("Start of tile array.");

				if (block != null) {
					label = String.Format("{0}BLK{1}", settings.LabelName, block);
				} else {
					label = String.Format("{0}", settings.LabelName);
				}
			}

			WriteLabel(label);
		}

		/// <summary>
		/// Writes a label that starts an array.
		/// </summary>
		protected abstract void WriteLabel(String label);

		/// <summary>
		/// Writes a single-line comment containing the given text.
		/// </summary>
		/// <param name="contents"></param>
		protected abstract void WriteLineComment(String contents);
		
		/// <summary>
		/// Writes the entire tileset's data.
		/// </summary>
		protected void WriteTileData(GBRFile gbrFile) {
			var settings = gbrFile.GetOrCreateObjectOfType<GBRObjectTileExport>();
			var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();


			if (settings.Split) {
				int blockCounter = 0;
				int block = 0;

				if (settings.CreateArrayForEachTile) {
					for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
						byte[] pixels = TileDataMaker.GetTileBytes(tileData.Tiles[tile], settings);

						WriteTileLabel(settings, tile, block);
						WriteData(pixels);

						blockCounter++;
						if (blockCounter == settings.BlockSize) {
							blockCounter = 0;

							block++;
						}
					}
				} else {
					using (MemoryStream stream = new MemoryStream()) {
						for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
							byte[] pixels = TileDataMaker.GetTileBytes(tileData.Tiles[tile], settings);
							stream.Write(pixels, 0, pixels.Length);

							blockCounter++;
							if (blockCounter == settings.BlockSize) {
								blockCounter = 0;

								WriteTileLabel(settings, block: block);
								WriteData(stream.ToArray());

								block++;
								//Clear the stream.
								stream.SetLength(0);
							}
						}

						if (stream.Length != 0) {
							WriteTileLabel(settings, block: block);
							WriteData(stream.ToArray());
						}
					}
				}
			} else {
				if (settings.CreateArrayForEachTile) {
					for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
						byte[] pixels = TileDataMaker.GetTileBytes(tileData.Tiles[tile], settings);

						WriteTileLabel(settings, tile);
						WriteData(pixels);
					}
				} else {
					byte[] data;

					using (MemoryStream stream = new MemoryStream()) {
						for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
							byte[] pixels = TileDataMaker.GetTileBytes(tileData.Tiles[tile], settings);
							stream.Write(pixels, 0, pixels.Length);
						}

						data = stream.ToArray();
					}

					WriteTileLabel(settings);
					WriteData(data);
				}
			}
		}

		/// <summary>
		/// Writes the includes that are used for the tileset.
		/// </summary>
		protected void WriteMapDataIncludes(GBRFile gbrFile) {
			var settings = gbrFile.GetOrCreateObjectOfType<GBRObjectTileExport>();

			if (settings.Split) {
				int blockCounter = 0;
				int block = 0;

				if (settings.CreateArrayForEachTile) {
					for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
						WriteTileLabel(settings, tile, block);

						blockCounter++;
						if (blockCounter == settings.BlockSize) {
							blockCounter = 0;

							block++;
						}
					}
				} else {
					for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
						blockCounter++;
						if (blockCounter == settings.BlockSize) {
							blockCounter = 0;

							WriteTileLabel(settings, block: block);

							block++;
						}
					}

					if (blockCounter != 0) {
						WriteTileLabel(settings, block: block);
					}
				}
			} else {
				if (settings.CreateArrayForEachTile) {
					for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
						WriteTileLabel(settings, tile);
					}
				} else {
					WriteTileLabel(settings);
				}
			}
		}

		/// <summary>
		/// Writes a section of data.
		/// </summary>
		/// <param name="bytes">The bytes for the tile itself.</param>
		protected virtual void WriteData(Byte[] bytes) {
			//The number of values to write each line.
			const int DATA_PER_LINE = 8;
			
			Stream.Write(BlockBegin);
			int position = 0;
			while (position < bytes.Length) {
				WriteDataLine(bytes, ref position, DATA_PER_LINE);
			}
			Stream.Write(BlockEnd);

			Stream.WriteLine();
		}

		/// <summary>
		/// Writes a line of data.
		/// </summary>
		/// <param name="bytes">The data to write.</param>
		/// <param name="position">The position in the array to start at.  Will be incremented.</param>
		/// <param name="count">The number of bytes to write.</param>
		protected abstract void WriteDataLine(Byte[] bytes, ref int position, int count);
	}
}
