using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;
using System.IO;
using GB.Shared.GBRFile;

namespace GB.GBMB.Exporting
{
	public class RGBDSAssemblyMapExporter : ASMExportBase
	{
		protected override string DefineEQU {
			get { return "EQU"; }
		}

		public override void WritePlaneLabel(GBMObjectMapExportSettings settings, int plane, int block, bool header) {
			Stream.WriteLine();

			String s;

			if (!settings.Split) {
				s = settings.LabelName.Trim();
			} else {
				s = settings.LabelName.Trim() + "BLK" + block;
			}

			if (plane == 0) {
				Stream.WriteLine("{0}::", s);
				if (settings.PlaneOrder == PlaneOrder.Planes_Are_Continues) {
					Stream.WriteLine("{0}PLN0::", s);
				}
			} else {
				Stream.WriteLine("{0}PLN{1}::", s, plane);
			}
		}

		public override void WriteDataLine(byte[] bytes, ref int position, int count) {
			int endPos = position + count;

			Stream.Write("DB ");
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
	}
}
