using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBRFile
{
	public class GBRFile
	{
		public class GBRFileHeader
		{
			//The default data.  Encoded like this because ASCII.
			private const byte DEFAULT_MM1 = 0x47 /* 'G' */;
			private const byte DEFAULT_MM2 = 0x42 /* 'B' */;
			private const byte DEFAULT_MM3 = 0x4F /* 'O' */;
			private const byte DEFAULT_VER = 0x30 /* '0' */;

			public byte MagicMarker1 { get; private set; }
			public byte MagicMarker2 { get; private set; }
			public byte MagicMarker3 { get; private set; }

			public byte VersionCode { get; private set; }

			public GBRFileHeader() {
				//Default value.
				MagicMarker1 = DEFAULT_MM1;
				MagicMarker2 = DEFAULT_MM2;
				MagicMarker3 = DEFAULT_MM3;

				VersionCode = DEFAULT_VER;
			}

			public GBRFileHeader(byte mm1, byte mm2, byte mm3, byte version) {
				MagicMarker1 = mm1;
				MagicMarker2 = mm2;
				MagicMarker3 = mm3;

				VersionCode = version;
			}

			public GBRFileHeader(Stream s) {
				LoadFromStream(s);
			}

			public void SaveToStream(Stream s) {
				s.WriteByte(MagicMarker1);
				s.WriteByte(MagicMarker2);
				s.WriteByte(MagicMarker3);

				s.WriteByte(VersionCode);
			}

			public void LoadFromStream(Stream s) {
				byte[] bytes = new byte[4];
				s.Read(bytes, 0, 4);

				MagicMarker1 = bytes[0];
				MagicMarker2 = bytes[1];
				MagicMarker3 = bytes[2];

				VersionCode = bytes[3];
			}

			/// <summary>
			/// Checks if this is a valid GBR file.
			/// </summary>
			/// <returns></returns>
			public bool IsValidFile() {
				if (MagicMarker1 != DEFAULT_MM1) { return false; }
				if (MagicMarker2 != DEFAULT_MM2) { return false; }
				if (MagicMarker3 != DEFAULT_MM3) { return false; }

				return true;
			}

			/// <summary>
			/// Checks if this is a supported GBR version (Currently GBR 0).
			/// </summary>
			/// <returns></returns>
			public bool IsSupportedVersion() {
				return VersionCode == DEFAULT_VER;
			}
		}

		public readonly GBRFileHeader Header;

		public List<IGBRExportable> Objects;

		public GBRFile() {
			this.Header = new GBRFileHeader();
		}

		public GBRFile(Stream stream) {
			this.Header = new GBRFileHeader(stream);

			//TODO validation here.
			Objects = new List<IGBRExportable>();

			while (true) {
				try {
					Objects.Add(IGBRExportable.ReadObject(stream));
				} catch (EndOfStreamException) {
					break;
				}
			}
		}
	}
}
