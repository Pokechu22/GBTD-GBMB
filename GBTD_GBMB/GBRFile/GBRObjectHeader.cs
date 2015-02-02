using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.GBRFile
{
	public struct GBRObjectHeader
	{
		/// <summary>
		/// The typeid of this object, which should remain constant.
		/// </summary>
		public readonly UInt16 ObjectID;

		/// <summary>
		/// The Unique ID of the object.
		/// </summary>
		public readonly UInt16 UniqueID;

		/// <summary>
		/// The size that this object was deserialized with.
		/// </summary>
		public readonly UInt32 Size;

		public GBRObjectHeader(UInt16 ObjectID, UInt16 UniqueID, UInt32 Size) {
			this.ObjectID = ObjectID;
			this.UniqueID = UniqueID;
			this.Size = Size;
		}

		public GBRObjectHeader Resize(UInt32 newSize) {
			return new GBRObjectHeader(this.ObjectID, this.UniqueID, newSize);
		}
	}
}
