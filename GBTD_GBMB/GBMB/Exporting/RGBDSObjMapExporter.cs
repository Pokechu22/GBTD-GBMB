using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using GB.Shared.GBRFile;
using System.IO;

namespace GB.GBMB.Exporting
{
	/// <summary>
	/// Exports to a pre-compiled RBDS object file.  
	/// 
	/// <para>For reference, this is how that export works (as far as I can tell):</para>
	/// 
	/// <para>In general, everything is little endian, strings are ASCII, and strings are null-terminated.</para>
	/// <para>The header.  "RGB1" followed by 2 four-byte numbers: the amount of objects, and the number of sections.</para>
	/// <para>Labels for each of the objects.  See <see cref="RGBDSLabel"/> for the format.</para>
	/// </summary>
	public class RGBDSObjMapExporter : IMapExporter
	{
		/// <summary>
		/// Writes stuff in the RGBDS format.  Vaugly messy, but simplifies other code.
		/// </summary>
		private class RGBDSFormatWriter
		{
			private Stream stream;

			public RGBDSFormatWriter(Stream stream) {
				this.stream = stream;
			}

			/// <summary>
			/// Writes a null-terminated string to the stream.
			/// </summary>
			public void WriteNullTerminatedString(string value) {
				const byte NULL_TERMINATOR = 0x00;

				WriteNonNullTerminatedString(value);

				WriteUInt32(NULL_TERMINATOR);
			}

			/// <summary>
			/// Writes a non-null-terminated string to the stream.
			/// </summary>
			public void WriteNonNullTerminatedString(string value) {
				byte[] bytes = Encoding.ASCII.GetBytes(value);

				WriteBytes(bytes);
			}

			/// <summary>
			/// Writes a byte to the stream.
			/// </summary>
			public void WriteByte(byte b) {
				stream.WriteByte(b);
			}

			public void WriteBytes(byte[] bytes) {
				stream.Write(bytes, 0, bytes.Length);
			}

			public override void WriteUInt32(UInt32 value) {
				WriteByte((byte)((value >> 0) & 0xFF));
				WriteByte((byte)((value >> 8) & 0xFF));
				WriteByte((byte)((value >> 16) & 0xFF));
				WriteByte((byte)((value >> 24) & 0xFF));
			}
		}

		/// <summary>
		/// A label put in the labels section that describes some data.
		/// </summary>
		private struct RGBDSLabel
		{
			/// <summary>
			/// The name of the label.
			/// </summary>
			public String Name { get; set; }
			/// <summary>
			/// The mode?  No clue what this does, but 2 is export.
			/// </summary>
			public byte Mode { get; set; }
			/// <summary>
			/// The section, which is probably something with banks or whatnot.  Always 0 here.
			/// </summary>
			public UInt32 Section { get; set; }
			/// <summary>
			/// The location in the actual data.
			/// </summary>
			public UInt32 Location { get; set; }

			/// <summary>
			/// Writes the label to the given writer.
			/// 
			/// <para>The format is: A null-terminated string for the <see cref="Name"/>, 
			/// then the <see cref="Mode"/>, then the <see cref="Section"/>, then the <see cref="Location"/>.</para>
			/// </summary>
			public void WriteToStream(RGBDSFormatWriter writer) {
				writer.WriteNullTerminatedString(Name);
				writer.WriteByte(Mode);
				writer.WriteUInt32(Section);
				writer.WriteUInt32(Location);
			}
		}

		public bool SupportsExportMain {
			get { return true; }
		}

		public bool SupportsExportInclude {
			get { return true; }
		}

		public void ExportMain(GBMFile gbmFile, GBRFile gbrFile, Stream stream, string fileName) {
			const UInt32 NUMBER_OF_SECTIONS = 1;

			UInt32 numberOfBlocks = 0; //TODO

			RGBDSFormatWriter writer = new RGBDSFormatWriter(stream);

			//Header.  This is fairly simple.  
			writer.WriteNonNullTerminatedString("RGB1");
			writer.WriteUInt32(numberOfBlocks);
			writer.WriteUInt32(NUMBER_OF_SECTIONS);
		}

		/// <summary>
		/// Writes a properly formated RGBDS header.
		/// 
		/// <para>
		/// The header format is a magic string ("RGB1"),
		/// then the number of blocks (4 bit, little endian unsigned int),
		/// then the number of sections (4 bit, little endian unsigned int).
		/// </para>
		/// </summary>
		protected void WriteHeader(RGBDSFormatWriter writer, UInt32 numberOfBlocks, UInt32 numberOfSections) {
			const string MAGIC_HEADER = "RGB1";

			writer.WriteNonNullTerminatedString(MAGIC_HEADER);
			writer.WriteUInt32(numberOfBlocks);
			writer.WriteUInt32(numberOfSections);
		}

		/// <summary>
		/// Writes each of the labels.  The format of the label is described in <see cref="RGBDSLabel.WriteToStream"/>
		/// </summary>
		protected void WriteLabels(RGBDSFormatWriter writer, RGBDSLabel[] labels) {
			for (int i = 0; i < labels.Length; i++) {
				labels[i].WriteToStream(writer);
			}
		}

		// Object include for RGBDS is actually that of RGBDS asm.  So we create a new RGBDS ASM exporter and do it there.
		// It's a bit silly, but it would be weirder to copy that code here.  And Object main isn't text-based, so having this
		// partially implement that would be a bad idea.
		public void ExportInclude(GBMFile gbmFile, GBRFile gbrFile, Stream stream, string fileName) {
			var asmExporter = new RGBDSAssemblyMapExporter();
			asmExporter.ExportInclude(gbmFile, gbrFile, stream, fileName);
		}
	}
}
