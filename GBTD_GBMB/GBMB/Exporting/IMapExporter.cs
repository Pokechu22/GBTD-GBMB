using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.IO;
using GB.Shared.GBRFile;

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
}
