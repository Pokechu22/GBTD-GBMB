using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBMFile;

namespace GB.GBMB.Exporting
{
	public class ISASAssemblyMapExporter : ASMExportBase
	{
		protected override string DefineEQU {
			get { return "EQU"; }
		}

		protected override void WriteSection(GBMObjectMapExportSettings settings, int bankOffset) {
			if (Header) { return; }

			Stream.WriteLine();

			if (settings.Split && settings.ChangeBankEachSplit) {
				Stream.WriteLine("{0}BLK{1} GROUP {2}", settings.SectionName.Trim(), bankOffset, settings.Bank + bankOffset);
			} else {
				Stream.WriteLine("{0} GROUP {1}", settings.SectionName.Trim(), settings.Bank + bankOffset);
			}
		}

		protected override void WritePlaneLabel(GBMObjectMapExportSettings settings, int plane, int block) {
			string s;

			if (!settings.Split) {
				s = settings.LabelName.Trim();
			} else {
				s = settings.LabelName.Trim() + "BLK" + block;
			}

			if (Header) {
				if (plane == 0) {
					Stream.WriteLine();

					Stream.WriteLine(" EXTERN {0}", s);
					if (settings.PlaneOrder == PlaneOrder.Planes_Are_Continues) {
						Stream.WriteLine(" EXTERN {0}PLN0", s);
					}
				} else {
					Stream.WriteLine(" EXTERN {0}PLN{1}", s, plane);
				}
			} else {
				Stream.WriteLine();

				if (plane == 0) {
					Stream.WriteLine();
					Stream.WriteLine(" PUBLIC {0}", s);
					Stream.WriteLine("{0}", s);

					if (settings.PlaneOrder == PlaneOrder.Planes_Are_Continues) {
						Stream.WriteLine();
						Stream.WriteLine(" PUBLIC {0}PLN0", s);
						Stream.WriteLine("{0}PLN0", s);
					}
				} else {
					Stream.WriteLine();
					Stream.WriteLine(" PUBLIC {0}PLN{1}", s, plane);
					Stream.WriteLine("{0}PLN{1}", s, plane);
				}
			}
		}

		protected override void WriteDataLine(byte[] bytes, ref int position, int count) {
			int endPos = position + count;

			Stream.Write(" DB ");
			while (position < endPos && position < bytes.Length) {
				Stream.Write("0{0:X2}h", bytes[position]);

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
