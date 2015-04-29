using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.IO;
using GB.Shared.GBRFile;

namespace GB.GBMB.Exporting
{
	public abstract class ASMExportBase : MapExportTextBase
	{
		/// <summary>
		/// The text used in the SizeDefines section between the term and the value.
		/// 
		/// EG RGBDS uses "<c>EQU</c>".  Do not include spaces.
		/// </summary>
		protected abstract string DefineEQU { get; }

		protected override string HeaderBegin {
			get { return ""; }
		}

		protected override string HeaderLine {
			get { return ";"; }
		}

		protected override string HeaderEnd {
			get { return ""; }
		}

		protected override string BlockBegin {
			get { return ""; }
		}

		protected override string BlockEnd {
			get { return ""; }
		}

		protected override string FooterBegin {
			get { return Stream.NewLine + ";"; }
		}

		protected override string FooterEnd {
			get { return ""; }
		}

		public override void WriteSizeDefines(GBMObjectMapExportSettings settings) {
			Stream.WriteLine("{0}Width  {1} {2}", settings.LabelName.Trim(), DefineEQU, settings.Master.Width);
			Stream.WriteLine("{0}Height {1} {2}", settings.LabelName.Trim(), DefineEQU, settings.Master.Height);
			Stream.WriteLine("{0}Bank   {1} {2}", settings.LabelName.Trim(), DefineEQU, settings.Bank);
		}
	}
}
