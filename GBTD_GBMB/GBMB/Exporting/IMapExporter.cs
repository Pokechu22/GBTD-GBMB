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
		void Export(GBMFile gbmFile, GBRFile gbrFile, Stream stream, String fileName);
	}
}
