using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.GBRFile;

namespace GB.GBTD.Exporting
{
	class GBDKCTileExporter : TileExportTextBase
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

		protected override string BlockBegin {
			get { return "{" + Stream.NewLine; }
		}

		protected override string BlockEnd {
			get { return "};"; }
		}

		protected override string FooterBegin {
			get { return "/*"; }
		}

		protected override string FooterEnd {
			get { return "*/"; }
		}

		protected override bool IncludeSectionAndBank {
			get { return false; }
		}

		protected override void WriteLabel(GBRObjectTileExport settings, int tileNum) {
			if (Header) {
				if (settings.StoreTilesInArray) {
					Stream.WriteLine("extern unsigned char {0}[];", settings.LabelName);
				} else {
					Stream.WriteLine("extern unsigned char {0}TLE{1}[];", settings.LabelName, tileNum);
				}
			} else {
				if (settings.StoreTilesInArray) {
					Stream.WriteLine("unsigned char {0}[] =", settings.LabelName);
				} else {
					Stream.WriteLine("unsigned char {0}TLE{1}[] =", settings.LabelName, tileNum);
				}
			}
		}

		protected override void WriteDataLine(byte[] bytes, ref int position, int count) {
			int endPos = position + count;

			Stream.Write("  ");
			while (position < endPos && position < bytes.Length) {
				Stream.Write("0x{0:X2}", bytes[position]);

				position++;

				if (position < endPos && position < bytes.Length) {
					Stream.Write(",");
				}
			}

			if (position != bytes.Length) {
				Stream.WriteLine(",");
			} else {
				Stream.WriteLine();
			}
		}
	}
}
