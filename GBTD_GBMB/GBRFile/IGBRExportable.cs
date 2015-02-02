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

		protected IGBRExportable(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) {
			this.TypeID = TypeID;
			this.UniqueID = UniqueID;
			this.Size = Size;
		}

		public abstract void LoadFromStream(Stream s) {
			
		}

		/// <summary>
		/// Writes the object header to specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		protected void WriteHeader(Stream stream) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteWord(TypeID);
			stream.WriteWord(UniqueID);
			stream.WriteLong(Size);
		}

		/// <summary>
		/// Reads the object header from specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		protected void ReadHeader(Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			this.TypeID = stream.ReadWord();
			this.UniqueID = stream.ReadWord();
			this.Size = Size;
		}

		public static IGBRExportable Read(Stream s) {
			
		}
	}
}
