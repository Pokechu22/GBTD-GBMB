using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.IO;
using GB.Shared.GBRFile;
using System.ComponentModel;

namespace GB.GBMB.Exporting
{
	/// <summary>
	/// Represents something that can export map data.
	/// 
	/// TODO define this further.
	/// </summary>
	public interface IMapExporter
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
		/// <param name="gbmFile">The GBM file.</param>
		/// <param name="gbrFile">The GBR file.</param>
		/// <param name="stream">The stream to export to.</param>
		/// <param name="fileName">The file name to use in the export information.</param>
		void ExportMain(GBMFile gbmFile, GBRFile gbrFile, Stream stream, String fileName);
		/// <summary>
		/// Exports the include file to the specified stream.
		/// </summary>
		/// <param name="gbmFile">The GBM file.</param>
		/// <param name="gbrFile">The GBR file.</param>
		/// <param name="stream">The stream to export to.</param>
		/// <param name="fileName">The file name to use in the export information.</param>
		void ExportInclude(GBMFile gbmFile, GBRFile gbrFile, Stream stream, String fileName);
	}

	public static class MapExporterUtils
	{
		/// <summary>
		/// Creates a new instance of the exporter that is for the given file type.
		/// </summary>
		/// <param name="fileType">The type to use.</param>
		/// <exception cref="WarningException">When fileType is not a recognized type.</exception>
		public static IMapExporter CreateExporter(this GBMExportFileType fileType) {
			switch (fileType) {
			case GBMExportFileType.GBDK_C_File: return new GBDKCMapExporter();
			case GBMExportFileType.RGBDS_Assembly_File: return new RGBDSAssemblyMapExporter();
			case GBMExportFileType.ISAS_Assembly_File: return new ISASAssemblyMapExporter();
			case GBMExportFileType.TASM_Assembly_File: return new TASMAssemblyMapExporter();
			case GBMExportFileType.All_Purpose_Binary_File: return new BinaryMapExporter();
			case GBMExportFileType.RGBDS_Object_File: return new RGBDSObjMapExporter();
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
	}
}
