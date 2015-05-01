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
	/// <para>The header.  "RGB1" followed by a 4-byte number: the amount of objects.  Little endian.</para>
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

			public void WriteToStream(RGBDSFormatWriter stream) {
				stream.WriteNullTerminatedString(Name);
				stream.WriteByte(Mode);
				stream.WriteUInt32(Section);
				stream.WriteUInt32(Location);
			}
		}

		public bool SupportsExportMain {
			get { return true; }
		}

		public bool SupportsExportInclude {
			get { return true; }
		}

		public void ExportMain(GBMFile gbmFile, GBRFile gbrFile, Stream stream, string fileName) {
			RGBDSFormatWriter writer = new RGBDSFormatWriter(stream);

			
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
