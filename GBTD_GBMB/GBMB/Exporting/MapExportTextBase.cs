﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.IO;
using GB.Shared.GBRFile;

namespace GB.GBMB.Exporting
{
	/// <summary>
	/// Base class for export fromats that are text-based.
	/// </summary>
	public abstract class MapExportTextBase : IMapExporter
	{
		protected MapExportTextBase() { }

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

		public void ExportMain(GBMFile gbmFile, GBRFile gbrFile, Stream stream, String fileName) {
			if (!SupportsExportMain) {
				throw new InvalidOperationException("This map exporter does not support exporting main files!");
			}

			Header = false;
			
			using (this.Stream = new StreamWriter(stream)) {
				var mapExportSettings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

				WriteHeader(mapExportSettings, fileName);
				Stream.WriteLine();
				WriteSizeDefines(mapExportSettings);
				WriteSection(mapExportSettings, 0);
				WriteMapData(gbmFile, gbrFile);
				WriteFooter(fileName);
			}

			this.Stream = null;
		}

		public void ExportInclude(GBMFile gbmFile, GBRFile gbrFile, Stream stream, String fileName) {
			if (!SupportsExportInclude) {
				throw new InvalidOperationException("This map exporter does not support exporting main files!");
			}

			Header = true;

			using (this.Stream = new StreamWriter(stream)) {
				var mapExportSettings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

				WriteHeader(mapExportSettings, fileName);
				Stream.WriteLine();
				WriteSizeDefines(mapExportSettings);
				WriteSection(mapExportSettings, 0);
				WriteMapDataIncludes(gbmFile, gbrFile);
				WriteFooter(fileName);
			}

			this.Stream = null;
		}

		/// <summary>
		/// Writes the header at the top that includes version information and such.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="fileName"></param>
		protected void WriteHeader(GBMObjectMapExportSettings settings, String fileName) {
			if (!String.IsNullOrEmpty(HeaderBegin)) {
				Stream.WriteLine(HeaderBegin);
			}
			Stream.WriteLine(HeaderLine + " " + Path.GetFileName(fileName).ToUpperInvariant());
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + " Map {0} File.", Header ? "Include" : "Source");
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + " Info:");
			Stream.WriteLine(HeaderLine + "   Section       : {0}", settings.SectionName);
			Stream.WriteLine(HeaderLine + "   Bank          : {0}", settings.Bank);
			Stream.WriteLine(HeaderLine + "   Map size      : {0} x {1}", settings.Master.Width, settings.Master.Height);
			Stream.WriteLine(HeaderLine + "   Tile set      : {0}", Path.GetFileName(settings.Master.TileFile));
			Stream.WriteLine(HeaderLine + "   Plane count   : {0}", settings.PlaneCount.GetDisplayName());
			Stream.WriteLine(HeaderLine + "   Plane order   : {0}", settings.PlaneOrder.GetDisplayName());
			Stream.WriteLine(HeaderLine + "   Tile offset   : {0}", settings.TileOffset);
			Stream.WriteLine(HeaderLine + "   Split data    : {0}", (settings.Split ? "Yes" : "No"));
			Stream.WriteLine(HeaderLine + "   Block size    : {0}", settings.SplitSize);
			if (IncludeIncBank) {
				Stream.WriteLine(HeaderLine + "   Inc bank      : {0}", settings.ChangeBankEachSplit ? "Yes" : "No");
			}
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + " This file was generated by GBMB v1.8"); //TODO actual version.
			if (!String.IsNullOrEmpty(HeaderEnd)) {
				Stream.WriteLine(HeaderEnd);
			}
		}

		/// <summary>
		/// Writes the defines that state the width, height, and bank.
		/// </summary>
		protected abstract void WriteSizeDefines(GBMObjectMapExportSettings settings);

		/// <summary>
		/// Writes a section label.  Only relevant for ASM; by default this method does nothing.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="bankOffset"></param>
		protected virtual void WriteSection(GBMObjectMapExportSettings settings, int bankOffset) {
			//Do nothing by default.
		}

		/// <summary>
		/// Writes the entire map's data.
		/// </summary>
		protected void WriteMapData(GBMFile gbmFile, GBRFile gbrFile) {
			var settings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

			int planeCount = settings.PlaneCount.GetNumberOfPlanes();

			if (settings.PlaneOrder == PlaneOrder.Tiles_Are_Continues) {
				Byte[][] data = MapDataMaker.GetTileContinuousData(gbmFile, gbrFile);

				for (int i = 0; i < data.Length; i++) {
					WritePlaneLabel(settings, 0, i);
					WriteData(data[i]);
				}
			} else { //Planes are continues.
				Byte[,][] planedData = MapDataMaker.GetPlaneContinuousData(gbmFile, gbrFile);

				int blockCount = planedData.GetLength(1);

				for (int block = 0; block < blockCount; block++) {
					if (settings.ChangeBankEachSplit && block > 0) {
						WriteSection(settings, block);
					}

					for (int plane = 0; plane < planeCount; plane++ ) {
						WritePlaneLabel(settings, plane, block);
						WriteData(planedData[plane, block]);
					}
				}
			}
		}

		/// <summary>
		/// Writes the includes that are used for the map.
		/// </summary>
		protected void WriteMapDataIncludes(GBMFile gbmFile, GBRFile gbrFile) {
			var settings = gbmFile.GetOrCreateObjectOfType<GBMObjectMapExportSettings>();

			int planeCount = settings.PlaneCount.GetNumberOfPlanes();
			int blockCount = (int)Math.Ceiling((double)(settings.Master.Width * settings.Master.Height) / settings.SplitSize);

			if (settings.PlaneOrder == PlaneOrder.Tiles_Are_Continues) {
				for (int plane = 0; plane < planeCount; plane++) {
					WritePlaneLabel(settings, 0, plane);
				}
			} else {
				for (int block = 0; block < blockCount; block++) {
					for (int plane = 0; plane < planeCount; plane++) {
						WritePlaneLabel(settings, plane, block);
					}
				}
			}
		}

		/// <summary>
		/// Writes the label for the specified plane and block.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="plane"></param>
		/// <param name="block"></param>
		protected abstract void WritePlaneLabel(GBMObjectMapExportSettings settings, int plane, int block);

		/// <summary>
		/// Writes a section of data -- a plane or a block.
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
