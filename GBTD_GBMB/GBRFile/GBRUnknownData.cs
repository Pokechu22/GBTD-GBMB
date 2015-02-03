using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Unknown GBR file data.
	/// Just an array of bytes; can't be used to do anything.
	/// Also used to represent FF, deleted data.
	/// </summary>
	public class GBRUnknownData : IGBRExportable
	{
		private byte[] data;

		public GBRUnknownData(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRUnknownData(GBRObjectHeader header, Stream stream) : base(header, stream) { }

		public override void SaveToStream(Stream s) {
			s.Write(data, 0, (int)header.Size);
		}

		public override void LoadFromStream(Stream s) {
			data = new byte[header.Size];
			s.Read(data, 0, (int)header.Size);
		}

		/// <summary>
		/// Provides a string representation.
		/// This probably should be cached, but I doubt that's of important yet.
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			//http://stackoverflow.com/a/2245604/3991344
			return String.Join(", ", this.data.Select(x => x.ToString("X2")).ToArray());
		}
	}
}
