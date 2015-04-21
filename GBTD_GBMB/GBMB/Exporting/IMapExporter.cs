using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.IO;

namespace GB.GBMB.Exporting
{
	/// <summary>
	/// Represents something that can export map data.
	/// 
	/// TODO define this further.
	/// </summary>
	public interface IMapExporter
	{
		public void Export(GBMFile file, Stream stream, String fileName);
	}
}
