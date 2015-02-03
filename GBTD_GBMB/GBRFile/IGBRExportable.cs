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
		private static Dictionary<UInt16, Type> mapping = new Dictionary<UInt16, Type>();

		/// <summary>
		/// The header of this object.
		/// </summary>
		protected GBRObjectHeader header;

		protected IGBRExportable(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) {
			this.header = new GBRObjectHeader(TypeID, UniqueID, Size);
			LoadObject(stream);
		}

		protected IGBRExportable(GBRObjectHeader header, Stream stream) {
			this.header = header;
			LoadObject(stream);
		}

		private void LoadObject(Stream s) {
			byte[] data = new byte[header.Size];
			int read = s.Read(data, 0, (int)header.Size);

			if (read != header.Size) {
				throw new EndOfStreamException();
			}

			using (MemoryStream ns = new MemoryStream(data, false)) {
				LoadFromStream(ns);
			}
		}

		public abstract void SaveToStream(Stream s);
		public abstract void LoadFromStream(Stream s);

		/// <summary>
		/// Reads an object and its header and returns said object.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static IGBRExportable ReadObject(Stream s) {
			GBRObjectHeader h = s.ReadHeader();

			IGBRExportable exportable;
			if (mapping.ContainsKey(h.ObjectID)) {
				var ctor = mapping[h.ObjectID].GetConstructor(new Type[] { typeof(GBRObjectHeader), typeof(Stream) });
				exportable = (IGBRExportable)ctor.Invoke(new Object[] { h, s });
			} else {
				exportable = new GBRUnknownData(h, s);
			}

			return exportable;
		}

		public static void RegisterExportable(UInt16 ID, Type type) {
			if (mapping.ContainsKey(ID)) {
				throw new InvalidOperationException("Already registered mapping for ID " + ID);
			}
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			
			mapping.Add(ID, type);
		}

		static IGBRExportable() {
			RegisterExportable(0xFF, typeof(GBRUnknownData));
		}
	}
}
