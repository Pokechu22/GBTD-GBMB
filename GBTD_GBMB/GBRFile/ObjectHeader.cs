using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GB.Shared.GBRFile
{
	public struct GBRObjectHeader
	{
		public readonly UInt16 ObjectID;
		public readonly UInt16 UniqueID;
		public readonly UInt32 Size;

		public GBRObjectHeader(UInt16 ObjectID, UInt16 UniqueID, UInt32 Size) {
			this.ObjectID = ObjectID;
			this.UniqueID = UniqueID;
			this.Size = Size;
		}
	}
}
