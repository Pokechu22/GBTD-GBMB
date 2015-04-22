using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;

namespace GB.GBMB.Exporting
{
	/// <summary>
	/// An exporter that exports to GBDK C.
	/// </summary>
	public class GBDKCMapExporter : MapExportTextBase
	{
		protected override string HeaderBegin {
			get { return "/*" + Stream.NewLine; }
		}

		protected override string HeaderLine {
			get { return ""; }
		}

		protected override string HeaderEnd {
			get { return Stream.NewLine + "*/"; }
		}

		public override void WriteSizeDefines(GBMObjectMapExportSettings settings) {
			Stream.WriteLine("#define {0}Width {1}", settings.LabelName.Trim(), settings.Master.Width);
			Stream.WriteLine("#define {0}Height {1}", settings.LabelName.Trim(), settings.Master.Height);
			Stream.WriteLine("#define {0}Bank {1}", settings.LabelName.Trim(), settings.Bank);
		}
	}
}
