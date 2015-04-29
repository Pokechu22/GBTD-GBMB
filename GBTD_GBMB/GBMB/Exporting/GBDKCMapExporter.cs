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

		protected override string BlockBegin {
			get { return "{" + Stream.NewLine; }
		}

		protected override string BlockEnd {
			get { return "};" + Stream.NewLine; }
		}

		protected override string FooterBegin {
			get { return "/*"; }
		}

		protected override string FooterEnd {
			get { return "*/"; }
		}

		protected override bool IncludeIncBank {
			get { return false; }
		}

		public override void WriteSizeDefines(GBMObjectMapExportSettings settings) {
			Stream.WriteLine("#define {0}Width {1}", settings.LabelName.Trim(), settings.Master.Width);
			Stream.WriteLine("#define {0}Height {1}", settings.LabelName.Trim(), settings.Master.Height);
			Stream.WriteLine("#define {0}Bank {1}", settings.LabelName.Trim(), settings.Bank);

			if (Header) {
				Stream.WriteLine();
			}
		}

		public override void WritePlaneLabel(GBMObjectMapExportSettings settings, int plane, int block) {
			String name;

			if (settings.Split) {
				name = settings.LabelName + "BLK" + block.ToString();
			} else {
				name = settings.LabelName;
			}

			if (Header) {
				if (plane == 0) {
					if (settings.PlaneOrder == PlaneOrder.Tiles_Are_Continues) {
						Stream.WriteLine("extern unsigned char {0}[];", name);
					} else {
						Stream.WriteLine("#define {0} {0}PLN{1}", name, plane);
						Stream.WriteLine("extern unsigned char {0}PLN{1}[];", name, plane);
					}
				} else {
					Stream.WriteLine("extern unsigned char {0}PLN{1}[];", name, plane);
				}
			} else {
				if (plane == 0) {
					Stream.WriteLine();
					if (settings.PlaneOrder == PlaneOrder.Tiles_Are_Continues) {
						Stream.WriteLine("unsigned char {0}[] =", name);
					} else {
						Stream.WriteLine("#define {0} {0}PLN{1}", name, plane);
						Stream.WriteLine("unsigned char {0}PLN{1}[] =", name, plane);
					}
				} else {
					Stream.WriteLine("unsigned char {0}PLN{1}[] =", name, plane);
				}
			}
		}

		public override void WriteDataLine(byte[] bytes, ref int position, int count) {
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

		public override void WriteFooter(string fileName) {
			if (Header) {
				Stream.WriteLine();
			}
			base.WriteFooter(fileName);
		}
	}
}
