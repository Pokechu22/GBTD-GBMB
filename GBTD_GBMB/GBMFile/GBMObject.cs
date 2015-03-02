using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GBMFile
{
	/// <summary>
	/// Any object that can be saved to a GBRFile.
	/// </summary>
	public abstract class GBMObject
	{
	}

	public struct GBMObjectHeader
	{
		/// <summary>
		/// The marker text - should ALWAYS be "HPJMTL".
		/// </summary>
		[Obsolete("Currently not yet used.")]
		public readonly String Marker;

		/// <summary>
		/// The typeid of this object, which should remain constant.
		/// </summary>
		public readonly UInt16 ObjectType;

		/// <summary>
		/// The Unique Object ID of the object.
		/// </summary>
		public readonly UInt16 ObjectID;

		/// <summary>
		/// If this object is a sub-object, this contains the main object's ID.
		/// If it is null, this is not a subobject.  A value of 0 is treated as null.
		/// </summary>
		public readonly UInt16? MasterID;

		/// <summary>
		/// The CRC of the object, which is currently unused.
		/// 
		/// If 0, it has not yet been calculated.
		/// </summary>
		[Obsolete("Currently not yet used - will always be 0.")]
		public readonly UInt32 CRC;

		/// <summary>
		/// The size that this object was deserialized with.
		/// </summary>
		public readonly UInt32 Size;

#pragma warning disable 618 //Disables obsolete warnings - http://stackoverflow.com/q/968293/3991344
		public GBMObjectHeader(UInt16 ObjectType, UInt16 ObjectID, UInt16? MasterID, UInt32 Size) {
			this.Marker = "HPJMTL";
			this.ObjectType = ObjectType;
			this.ObjectID = ObjectID;
			this.MasterID = MasterID;
			this.CRC = 0x00000000U;
			this.Size = Size;
		}

		public GBMObjectHeader Resize(UInt32 newSize) {
			return new GBMObjectHeader(this.Marker, this.ObjectType, this.ObjectID, this.MasterID, this.CRC, newSize);
		}

		[Obsolete("Sets unused values; you probably want the other one.")]
		public GBMObjectHeader(String Marker, UInt16 ObjectType, UInt16 ObjectID, UInt16? MasterID, UInt32 CRC, UInt32 Size) {
			this.Marker = Marker;
			this.ObjectType = ObjectType;
			this.ObjectID = ObjectID;
			this.MasterID = MasterID;
			this.CRC = CRC;
			this.Size = Size;
		}
#pragma warning restore 618

	}
}
