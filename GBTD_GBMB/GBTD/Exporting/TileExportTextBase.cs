using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBRFile;
using System.IO;

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
		/// Whether or not to inlcude the "Increment bank" text on the header.  For C, false; for ASM, true.
		/// </summary>
		protected abstract bool IncludeIncBank { get; }

		public virtual bool SupportsExportMain { get { return true; } }
		public virtual bool SupportsExportInclude { get { return true; } }

		public void ExportMain(GBRFile gbrFile, Stream stream, String fileName) {
			if (!SupportsExportMain) {
				throw new InvalidOperationException("This tile exporter does not support exporting main files!");
			}

			Header = false;
			
			using (this.Stream = new StreamWriter(stream)) {
				var exportSettings = gbrFile.GetOrCreateObjectOfType<GBRObjectTileExport>();

				WriteHeader(exportSettings, fileName);
				Stream.WriteLine();
				WriteSizeDefines(exportSettings);
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

				WriteHeader(exportSettings, fileName);
				Stream.WriteLine();
				WriteSizeDefines(exportSettings);
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
		protected void WriteHeader(GBRObjectTileExport settings, String fileName) {
			if (!String.IsNullOrEmpty(HeaderBegin)) {
				Stream.WriteLine(HeaderBegin);
			}
			Stream.WriteLine(HeaderLine + " " + Path.GetFileName(fileName).ToUpperInvariant());
			Stream.WriteLine(HeaderLine);
			//TODO
			if (!String.IsNullOrEmpty(HeaderEnd)) {
				Stream.WriteLine(HeaderEnd);
			}
		}

		/// <summary>
		/// Writes the defines that state the width, height, and bank.
		/// </summary>
		protected abstract void WriteSizeDefines(GBRObjectTileExport settings);

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

			//TODO
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
			const int DATA_PER_LINE = 10;

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
