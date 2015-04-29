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

		public override void WritePlaneLabel(GBMObjectMapExportSettings settings, int plane, int block) {
			String s;

			if (!settings.Split) {
				s = settings.LabelName.Trim();
			} else {
				s = settings.LabelName.Trim() + "BLK" + block;
			}

			if (Header) {
				if (plane == 0) {
					Stream.WriteLine();

					Stream.WriteLine(" GLOBAL {0}", s);
					if (settings.PlaneOrder == PlaneOrder.Planes_Are_Continues) {
						Stream.WriteLine(" GLOBAL {0}PLN0", s);
					}
				} else {
					Stream.WriteLine(" GLOBAL {0}PLN{1}", s, plane);
				}
			} else {
				Stream.WriteLine();

				if (plane == 0) {
					Stream.WriteLine("{0}::", s);
					if (settings.PlaneOrder == PlaneOrder.Planes_Are_Continues) {
						Stream.WriteLine("{0}PLN0::", s);
					}
				} else {
					Stream.WriteLine("{0}PLN{1}::", s, plane);
				}
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

		public override void WriteSection(GBMObjectMapExportSettings settings, int bankOffset) {
			if (Header) { return; } //No sections in the header.

			Stream.WriteLine();

			String s;

			if (settings.Split && settings.ChangeBankEachSplit) {
				s = settings.SectionName.Trim() + "BLK" + bankOffset;
			} else {
				s = settings.SectionName.Trim();
			}

			if (settings.Bank + bankOffset > 0) {
				Stream.WriteLine(" SECTION \"{0}\", DATA, BANK[{1}]", s, settings.Bank + bankOffset);
			} else {
				Stream.WriteLine(" SECTION \"{0}\", HOME", s);
			}
		}
	}
}
