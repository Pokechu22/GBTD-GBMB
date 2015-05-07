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
	/// TODO: multi-section support.  Doesn't seem impossible, but it would take thinking.
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
		protected class RGBDSFormatWriter
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

				WriteByte(NULL_TERMINATOR);
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

			public void WriteBytes(params byte[] bytes) {
				stream.Write(bytes, 0, bytes.Length);
			}

			public void WriteUInt32(UInt32 value) {
				WriteByte((byte)((value >> 0) & 0xFF));
				WriteByte((byte)((value >> 8) & 0xFF));
				WriteByte((byte)((value >> 16) & 0xFF));
				WriteByte((byte)((value >> 24) & 0xFF));
			}
		}

		/// <summary>
		/// A label put in the labels section that describes some data.
		/// </summary>
		protected struct RGBDSLabel
		{
			private const byte DEFAULT_MODE = 2;
			private const UInt32 DEFAULT_SECTION = 0;

			/// <summary>
			/// Creates a RGBDSLabel with the given info.  Mode and Section are set at their normal values.
			/// </summary>
			public RGBDSLabel(String name, UInt32 location) : this() {
				Name = name;
				Mode = DEFAULT_MODE;
				Section = DEFAULT_SECTION;
				Location = location;
			}

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
			const UInt32 NUMBER_OF_SECTIONS = 1; //TODO -- this currently can't be changed... but in the future, mabye?

			var settings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

			RGBDSFormatWriter writer = new RGBDSFormatWriter(stream);

			byte[] data;
			RGBDSLabel[] labels;

			CreateData(gbmFile, gbrFile, out data, out labels);

			WriteHeader(writer, (UInt32)labels.Length, NUMBER_OF_SECTIONS);

			WriteLabels(writer, labels);
			WriteSection(writer, (UInt16)data.Length, settings.Bank);

			writer.WriteBytes(data);

			//EOF data, supposedly.
			writer.WriteBytes(0, 0, 0, 0);
		}

		/// <summary>
		/// Creates the map data and a list of indexes for each of the peices of data.
		/// </summary>
		/// <param name="gbmFile">The GBM File.</param>
		/// <param name="gbrFile">The GBR File.</param>
		/// <param name="bytes">Out -- contains all of the map bytes.</param>
		/// <param name="labels">Out -- contains the indecies and names of each section.</param>
		protected void CreateData(GBMFile gbmFile, GBRFile gbrFile, out Byte[] bytes, out RGBDSLabel[] labels) {
			using (MemoryStream stream = new MemoryStream()) {
				List<RGBDSLabel> labelsList = new List<RGBDSLabel>();

				var settings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

				int planeCount = settings.PlaneCount.GetNumberOfPlanes();

				if (settings.PlaneOrder == PlaneOrder.Tiles_Are_Continues) {
					Byte[][] data = MapDataMaker.GetTileContinuousData(gbmFile, gbrFile);

					for (int block = 0; block < data.Length; block++) {
						String name;
						if (settings.Split) {
							name = settings.LabelName.Trim() + "BLK" + block;
						} else {
							name = settings.LabelName.Trim();
						}

						labelsList.Add(new RGBDSLabel(name, (UInt32)stream.Position));

						stream.Write(data[block], 0, data[block].Length);
					}
				} else { //Planes are continues.
					Byte[,][] planedData = MapDataMaker.GetPlaneContinuousData(gbmFile, gbrFile);

					int blockCount = planedData.GetLength(1);

					for (int block = 0; block < blockCount; block++) {
						for (int plane = 0; plane < planeCount; plane++) {
							String name;

							if (settings.Split) {
								name = settings.LabelName.Trim() + "BLK" + block;
							} else {
								name = settings.LabelName.Trim();
							}

							if (plane == 0) {
								labelsList.Add(new RGBDSLabel(name, (UInt32)stream.Position));
								labelsList.Add(new RGBDSLabel(name + "PLN" + plane, (UInt32)stream.Position));
							} else {
								labelsList.Add(new RGBDSLabel(name + "PLN" + plane, (UInt32)stream.Position));
							}

							stream.Write(planedData[plane, block], 0, planedData[plane, block].Length);
						}
					}
				}

				bytes = stream.ToArray();
				labels = labelsList.ToArray();
			}
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

		/// <summary>
		/// Writes the header data to the specified writer.
		/// 
		/// <para>Currently this data is hardcoded.  That's fine, since it'll always be the same.</para>
		/// <para>Layout: A uint32, the size; then a byte that indicates the section type (decided by bank), 
		/// then the org (no idea what that is, but it's always 0xFFFFFFFF), and then the bank.  The bank is
		/// either the one from the settings, or if the one in the settings is 0, 0xFFFFFFFF.</para>
		/// </summary>
		protected void WriteSection(RGBDSFormatWriter writer, UInt32 size, UInt32 bank) {
			//The two sections.  Home is for when bank is 0.
			const byte SECTION_HOME = 0x03;
			//And code is for nonzero banks.
			const byte SECTION_CODE = 0x02;

			//The ORG value.  No clue what this is, but it's always 0xFFFFFFFF.
			const UInt32 ORG = 0xFFFFFFFF;

			//The bank used when in SECTION_HOME (when bank is 0).
			const UInt32 BANK_FOR_HOME = 0xFFFFFFFF;

			writer.WriteUInt32(size);
			writer.WriteByte(bank == 0 ? SECTION_HOME : SECTION_CODE);
			writer.WriteUInt32(ORG);
			writer.WriteUInt32(bank != 0 ? bank : BANK_FOR_HOME);
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
