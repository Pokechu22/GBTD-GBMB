using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.GBMB.Exporting
{
	public abstract class ASMExportBase : MapExportTextBase
	{
		protected override string HeaderBegin {
			get { return ""; }
		}

		protected override string HeaderLine {
			get { return ";"; }
		}

		protected override string HeaderEnd {
			get { return ""; }
		}

		protected override string FooterBegin {
			get { return ";"; }
		}

		protected override string FooterEnd {
			get { return ""; }
		}
	}
}
