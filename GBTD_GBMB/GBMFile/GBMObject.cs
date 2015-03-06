using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// Any object that can be saved to a GBRFile.
	/// </summary>
	public abstract class GBMObject
	{
		/// <summary>
		/// The data loaded when origionally deserialized.
		/// </summary>
		private byte[] loadedData;

		/// <summary>
		/// Any aditional data that was not read, which is added to the end.
		/// </summary>
		private byte[] extraData;

		private static Dictionary<UInt16, Type> mapping = new Dictionary<UInt16, Type>();
		
		/// <summary>
		/// The Header of this object.
		/// </summary>
		public GBMObjectHeader Header { get; protected set; }

		/// <summary>
		/// The master object, or null if there is none.
		/// </summary>
		public GBMObject Master { get; protected set; }

		protected GBMObject(GBMObject Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream) {
			this.Master = Master;
			
			this.Header = new GBMObjectHeader(TypeID, UniqueID, MasterID, Size);
			LoadObject(stream);
		}

		protected GBMObject(GBMObject Master, GBMObjectHeader header, Stream stream) {
			this.Master = Master;

			this.Header = header;
			LoadObject(stream);
		}

		private void LoadObject(Stream s) {
			byte[] data = new byte[Header.Size];
			int read = s.Read(data, 0, (int)Header.Size);

			if (read != Header.Size) {
				throw new EndOfStreamException();
			}

			loadedData = data;

			using (MemoryStream ns = new MemoryStream(data, false)) {
				LoadFromStream(ns);

				if (ns.Position != ns.Length) {
					extraData = new byte[ns.Length - ns.Position];
					ns.Read(extraData, 0, (int)(ns.Length - ns.Position));
				}
			}
		}

		/// <summary>
		/// Saves this object to the specified stream.
		/// </summary>
		/// <param name="s"></param>
		public void SaveObject(Stream s) {
			if (loadedData == null) {
				s.WriteGBMObjectHeader(this.Header);
				this.SaveToStream(s);
			} else {
				using (MemoryStream ns = new MemoryStream(loadedData, true)) {
					ns.Position = 0;
					this.SaveToStream(ns);

					this.Header = this.Header.Resize((UInt32)ns.Length);
					s.WriteGBMObjectHeader(this.Header);
					s.Write(ns.ToArray(), 0, (int)this.Header.Size);
				}
			}
		}

		protected abstract void LoadFromStream(Stream s);
		protected abstract void SaveToStream(Stream s);

		/// <summary>
		/// Reads an object and its Header and returns said object.
		/// </summary>
		/// <param name="master">The master object or null if there is no master.</param>
		/// <param name="header">The header of the object.</param>
		/// <param name="s">The stream to read from.</param>
		/// <returns></returns>
		public static GBMObject ReadObject(GBMObject master, GBMObjectHeader header, Stream s) {

			GBMObject obj;
			if (mapping.ContainsKey(header.ObjectType)) {
				Type masterType = (master != null ? master.GetType() : typeof(GBMObject));

				//Use reflection to create an instance of the specified object.
				var ctor = mapping[header.ObjectType].GetConstructor(new Type[] { masterType, typeof(GBMObjectHeader), typeof(Stream) });
				if (ctor == null) {
					throw new Exception(String.Format("Failed to find a constructor for {0} taking arguments {1}, {2}, and {3}.", 
						mapping[header.ObjectType], masterType, typeof(GBMObjectHeader), typeof(Stream)));
				}

				obj = (GBMObject)ctor.Invoke(new Object[] { master, header, s });
			} else {
				obj = new GBMObjectUnknownData(master, header, s);
			}

			return obj;
		}

		/// <summary>
		/// Gets the name of the object type, which should be constant for all instances.
		/// </summary>
		/// <returns></returns>
		public abstract string GetTypeName();

		/// <summary>
		/// Converts to a treenode, for debug purposes.
		/// </summary>
		/// <returns></returns>
		public abstract TreeNode ToTreeNode();

		/// <summary>
		/// Creates a treenode for the root level, which has the proper name.
		/// </summary>
		/// <returns></returns>
		protected TreeNode CreateRootTreeNode() {
			TreeNode root = new TreeNode(GetTypeName() + " - #" + this.Header.ObjectID.ToString("X4"));

#pragma warning disable 618 //Disables obsolete warnings - http://stackoverflow.com/q/968293/3991344
			
			TreeNode header = new TreeNode("Header");
			
			header.Nodes.Add("Marker", "Marker: " + this.Header.Marker);
			header.Nodes.Add("Type", "Type: " + GetTypeName() + " (" + this.Header.ObjectType.ToString("X4") + ")");
			header.Nodes.Add("ObjectID", "ObjectID: " + this.Header.ObjectID.ToString("X4"));
			header.Nodes.Add("MasterID", "MasterID: " + (this.Header.MasterID.HasValue ? this.Header.MasterID.Value.ToString("X4") : "None"));
			header.Nodes.Add("CRC", "CRC: " + this.Header.CRC.ToString("X8"));
			header.Nodes.Add("Size", "Size: " + this.Header.Size);

			root.Nodes.Add(header);

#pragma warning restore 618

			if (extraData != null) {
				TreeNode extraNode = new TreeNode("Extra unknown data (" + extraData.Length + " bytes)");
				extraNode.Nodes.Add(string.Join(" ", extraData.Select(x => x.ToString()).ToArray()));

				root.Nodes.Add(extraNode);
			}

			return root;
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

		static GBMObject() {
			//TODO
			RegisterExportable(0xFFFF, typeof(GBMObjectDeleted));
			RegisterExportable(0x01, typeof(GBMObjectProducerInfo));
			RegisterExportable(0x02, typeof(GBMObjectMap));
			RegisterExportable(0x03, typeof(GBMObjectMapTileData));
			RegisterExportable(0x04, typeof(GBMObjectMapProperties));
			RegisterExportable(0x05, typeof(GBMObjectMapPropertyData));
			RegisterExportable(0x06, typeof(GBMObjectDefaultTilePropertyValues));
			RegisterExportable(0x07, typeof(GBMObjectMapSettings));
			RegisterExportable(0x08, typeof(GBMObjectMapPropertyColors));
			RegisterExportable(0x09, typeof(GBMObjectMapExportSettings));
		}
	}

	/// <summary>
	/// Any object that makes explicit use of the Master should use this class.
	/// </summary>
	/// <typeparam name="TMaster"></typeparam>
	public abstract class MasteredGBMObject<TMaster> : GBMObject where TMaster : GBMObject
	{
		/// <summary>
		/// The Master object.
		/// </summary>
		public new TMaster Master {
			get { return (TMaster)base.Master; }
			protected set { base.Master = value; }
		}

		protected MasteredGBMObject(TMaster Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }
		protected MasteredGBMObject(TMaster Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }
	}
	
	public struct GBMObjectHeader
	{
		/// <summary>
		/// The marker text - should ALWAYS be "HPJMTL".
		/// </summary>
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
		
		/// <summary>
		/// Validates the marker text (and mabye the CRC later).
		/// If all goes well, this method does nothing.  Otherwise, an exception is thrown.
		/// </summary>
		/// <param name="index">Optional value that provides the location in the file of this object, if available.  Otherwise, <c>null</c>.</param>
		/// <exception cref="Exception">Thrown when the marker is invalid.</exception>
		public void Validate(long? index = null) {
			if (this.Marker != "HPJMTL") {
				String wantedHex = String.Join(" ", Encoding.ASCII.GetBytes("HPJMTL").Select(b => b.ToString("X2")));
				String markerHex = String.Join(" ", Encoding.ASCII.GetBytes(Marker).Select(b => b.ToString("X2")));
				
				//TODO better exception type.
				if (index.HasValue) {
					throw new Exception(String.Format(
						"Marker text for object #{0:X4} of type {1:X4} is not valid - should be \"HPJMTL\" ({2}) but was actualy {3} ({4}).", 
							ObjectID, ObjectType, wantedHex, Marker, markerHex));
				} else {
					throw new Exception(String.Format(
						"Marker text for object #{0:X4} of type {1:X4} at offset {2:X16} is not valid - should be \"HPJMTL\" ({3}) but was actualy {4} ({5}).", ObjectID, ObjectType, index.Value, wantedHex, Marker, markerHex));
				}
			}
		}
	}
}
