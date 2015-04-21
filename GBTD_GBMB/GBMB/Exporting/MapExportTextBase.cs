using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.IO;

namespace GB.GBMB.Exporting
{
	/// <summary>
	/// Base class for export fromats that are text-based.
	/// </summary>
	public abstract class MapExportTextBase : IMapExporter
	{
		/// <summary>
		/// The stream that is being written to.
		/// </summary>
		protected StreamWriter Stream { get; private set; }

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

		public void Export(GBMFile file, Stream stream, String fileName) {
			using (this.Stream = new StreamWriter(stream)) {
				var mapExportSettings = file.GetObjectOfType<GBMObjectMapExportSettings>();

				WriteHeader(mapExportSettings, fileName);
			}
		}

		public void WriteHeader(GBMObjectMapExportSettings settings, String fileName) {
			//TODO handle source vs include file.

			Stream.WriteLine(HeaderBegin);
			Stream.WriteLine(HeaderLine + Path.GetFileName(fileName));
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + "Map Source File.");
			Stream.WriteLine(HeaderLine);
			Stream.WriteLine(HeaderLine + "Info:");
			Stream.WriteLine(HeaderLine + "  Section       : " + settings.SectionName);
			Stream.WriteLine(HeaderLine + "  Bank          : " + settings.Bank);
			Stream.WriteLine(HeaderLine + "  Map size      : " + settings.Master.Width + " x " + settings.Master.Height);

		}
	}
}
