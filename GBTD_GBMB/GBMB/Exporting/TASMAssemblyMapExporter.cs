using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;

namespace GB.GBMB.Exporting
{
	public class TASMAssemblyMapExporter : ASMExportBase
	{
		protected override string DefineEQU {
			get { return ".equ"; }
		}

		public override bool SupportsExportInclude {
			get { return false; }
		}

		public override void WriteSection(GBMObjectMapExportSettings settings, int bankOffset) {
			//Do nothing.
		}

		public override void WritePlaneLabel(GBMObjectMapExportSettings settings, int plane, int block) {
			string s;

			if (!settings.Split) {
				s = settings.LabelName.Trim();
			} else {
				s = settings.LabelName.Trim() + "BLK" + block;
			}

			Stream.WriteLine();

			if (plane == 0) {
				Stream.WriteLine("{0}: ", s);

				if (settings.PlaneOrder == PlaneOrder.Planes_Are_Continues) {
					Stream.WriteLine("{0}PLN0: ", s);
				}
			} else {
				Stream.WriteLine("{0}PLN{1}: ", s, plane);
			}
		}

		public override void WriteDataLine(byte[] bytes, ref int position, int count) {
			int endPos = position + count;

			Stream.Write(".byte ");
			while (position < endPos && position < bytes.Length) {
				Stream.Write("${0:X2}", bytes[position]);

				position++;

				if (position < endPos && position < bytes.Length) {
					Stream.Write(",");
				}
			}

			if (position != bytes.Length) {
				Stream.WriteLine();
			}
		}

		public override void WriteFooter(string fileName) {
			//TASM needs '.end' before the footer.
			Stream.WriteLine();
			Stream.WriteLine(".end");

			base.WriteFooter(fileName);
		}
	}
}
