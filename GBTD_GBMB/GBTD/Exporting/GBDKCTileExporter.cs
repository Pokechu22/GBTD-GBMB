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

		protected override bool IncludeSectionAndBank {
			get { return false; }
		}

		protected override void WriteLabel(String label) {
			if (Header) {
				Stream.WriteLine("extern unsigned char {0}[];", label);
			} else {
				Stream.WriteLine("unsigned char {0}[] =", label);
			}
		}

		protected override void WriteLineComment(string contents) {
			Stream.WriteLine("/* " + contents + " */");
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
