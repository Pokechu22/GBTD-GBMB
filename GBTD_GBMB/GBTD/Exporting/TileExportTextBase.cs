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
		/// The text that begins the line of the footer.  EG for C, "<c>/*</c>".
		/// A newline is not, by default, appended after this.
		/// </summary>
		protected abstract string FooterBegin { get; }
		/// <summary>
		/// The text that ends the line of the footer.  EG for C, "<c>*/</c>".
		/// A newline is appended after this by default.
		/// </summary>
		protected abstract string FooterEnd { get; }

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
				WriteFooter(fileName);
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
				WriteFooter(fileName);
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
				(settings.StoreTilesInArray ? "All tiles as one unit." : "Each tile separate."));
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
		/// Writes the entire tileset's data.
		/// </summary>
		protected void WriteTileData(GBRFile gbrFile) {
			var settings = gbrFile.GetOrCreateObjectOfType<GBRObjectTileExport>();
			var tileData = gbrFile.GetOrCreateObjectOfType<GBRObjectTileData>();

			//TODO: Properly respect tile settings.  For now, just assume byte per pixel and split into arrays for each tile.
			byte[] pixelBuffer = new byte[tileData.Width * tileData.Height];

			for (int tile = settings.FromTile; tile <= settings.ToTile; tile++) {
				for (int y = 0; y < tileData.Height; y++) {
					for (int x = 0; x < tileData.Width; x++) {
						byte b;

						switch (tileData.Tiles[tile][x, y]) {
						case GBColor.WHITE: b = 0; break;
						case GBColor.LIGHT_GRAY: b = 1; break;
						case GBColor.DARK_GRAY: b = 2; break;
						case GBColor.BLACK: b = 3; break;
						default: b = (byte)tileData.Tiles[tile][x, y]; break;
						}

						pixelBuffer[(y * tileData.Width) + x] = b;
					}
				}

				//TODO: WritePlaneLabel.
				WriteData(pixelBuffer);
			}
		}

		/// <summary>
		/// Writes the includes that are used for the tileset.
		/// </summary>
		protected void WriteMapDataIncludes(GBRFile gbrFile) {
			var settings = gbrFile.GetOrCreateObjectOfType<GBRObjectTileExport>();

			//TODO
		}

		/// <summary>
		/// Writes a section of data.
		/// </summary>
		/// <param name="bytes"></param>
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

		/// <summary>
		/// Writes the footer.
		/// </summary>
		protected virtual void WriteFooter(String fileName) {
			Stream.WriteLine("{0} End of {1} {2}", FooterBegin, Path.GetFileName(fileName).ToUpperInvariant(), FooterEnd);
		}
	}
}
