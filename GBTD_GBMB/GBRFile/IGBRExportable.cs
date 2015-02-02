using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBRFile
{
	public delegate IGBRExportable GBRExportableContstructor(Stream s);

	/// <summary>
	/// Something that can be exported to a .GBR file.
	/// </summary>
	public abstract class IGBRExportable
	{
		private static Dictionary<UInt16, GBRExportableContstructor> mapping = new Dictionary<ushort, GBRExportableContstructor>();

		/// <summary>
		/// The header of this object.
		/// </summary>
		protected GBRObjectHeader header;

		protected IGBRExportable(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) {
			this.header = new GBRObjectHeader(TypeID, UniqueID, Size);

			//TODO
		}

		protected IGBRExportable(GBRObjectHeader header, Stream stream) {
			this.header = header;

			//TODO
		}

		public abstract void SaveToStream(Stream s);
		public abstract void LoadFromStream(Stream s);

		/// <summary>
		/// Reads an object and its header and returns said object.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static IGBRExportable ReadObject(Stream s) {
			//TODO
			return null;
		}
	}
}
