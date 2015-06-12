using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBRFile;
using System.ComponentModel;
using System.IO;

namespace GB.GBTD.Exporting
{
	public interface ITileExporter
	{
		/// <summary>
		/// Whether or not <see cref="ExportMain"/> is supported.
		/// <para>This should be true all the time, but is included for clarity.</para>
		/// </summary>
		bool SupportsExportMain { get; }
		/// <summary>
		/// Whether or not <see cref="ExportInclude"/> is supported.
		/// </summary>
		bool SupportsExportInclude { get; }

		/// <summary>
		/// Exports the main data file to the specified stream.
		/// </summary>
		/// <param name="gbrFile">The GBR file.</param>
		/// <param name="stream">The stream to export to.</param>
		/// <param name="fileName">The file name to use in the export information.</param>
		void ExportMain(GBRFile gbrFile, Stream stream, String fileName);
		/// <summary>
		/// Exports the include file to the specified stream.
		/// </summary>
		/// <param name="gbrFile">The GBR file.</param>
		/// <param name="stream">The stream to export to.</param>
		/// <param name="fileName">The file name to use in the export information.</param>
		void ExportInclude(GBRFile gbrFile, Stream stream, String fileName);
	}

	public static class TileExporterUtils
	{
		/// <summary>
		/// Creates a new instance of the exporter that is for the given file type.
		/// </summary>
		/// <param name="fileType">The type to use.</param>
		/// <exception cref="WarningException">When fileType is not a recognized type.</exception>
		public static ITileExporter CreateExporter(this GBRExportFileType fileType) {
			switch (fileType) {
			//TODO
			default:
				try {
					throw new WarningException(String.Format("File type {0} ({1} : {2}) is not a recognized / supported export mode!",
						fileType.GetDisplayName(), fileType, (int)fileType));
				} catch (InvalidEnumArgumentException e) { //Thrown when fileType.GetDisplayName() fails.
					throw new WarningException(String.Format("File type {1} ({2}) is not a recognized / supported export mode!  Aditionally, " +
							"an exception occured when trying to get the display name for said type!",
							fileType, (int)fileType), e);
				}
			}
		}

		/// <summary>
		/// Gets the display string for a GBRExportFileType.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static String GetDisplayName(this GBRExportFileType fileType) {
			switch (fileType) {
			case GBRExportFileType.RGBDSAssemblyFile: return "RGBDS Assembly file";
			case GBRExportFileType.RGBDSObjectFile: return "RGBDS Object file";
			case GBRExportFileType.TASMAssemblyFile: return "TASM Assembly file";
			case GBRExportFileType.GBDKCFile: return "GBDK C file";
			case GBRExportFileType.BinaryFile: return "All-purpose binary file";
			case GBRExportFileType.ISASAssemblyFile: return "ISAS Assembly file";
			default: throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(GBRExportFileType));
			}
		}
	}
}
