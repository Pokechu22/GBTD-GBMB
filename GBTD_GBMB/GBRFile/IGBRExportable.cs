using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Something that can be exported to a .GBR file.
	/// </summary>
	public abstract class IGBRExportable
	{
		/// <summary>
		/// The typeid of this object, which should remain constant.
		/// </summary>
		public UInt16 TypeID { get; protected set; }

		/// <summary>
		/// The Unique ID of this object.
		/// </summary>
		public UInt16 UniqueID { get; protected set; }

		/// <summary>
		/// The size that this object was deserialized with.
		/// </summary>
		public UInt32 Size { get; protected set; }

		/// <summary>
		/// Writes the object header to specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		public void WriteHeader(Stream stream) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteWord(TypeID);
			stream.WriteWord(UniqueID);
			stream.WriteLong(GetSize());
		}

		/// <summary>
		/// Reads the object header from specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		public void ReadHeader(Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			this.TypeID = stream.ReadWord();
			this.UniqueID = stream.ReadWord();
			stream.ReadLong(); //TODO
		}

		public abstract UInt32 GetSize();
	}
}
