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
		
		/// <summary>
		/// The Header of this object.
		/// </summary>
		public GBMObjectHeader Header { get; protected set; }

		/// <summary>
		/// The master object, or null if there is none.
		/// </summary>
		public GBMObject Master { get; protected set; }

		/// <summary>
		/// Loads a GBMObject from the given <paramref name="stream"/>.
		/// </summary>
		/// <param name="Master">The master object.  May be null.</param>
		/// <param name="header">The header of the object.</param>
		/// <param name="stream">The stream to load the object from.</param>
		protected GBMObject(GBMObject Master, GBMObjectHeader header, Stream stream) {
			this.Master = Master;

			this.Header = header;
			LoadObject(stream);
		}

		/// <summary>
		/// Creates a new GBMObject with the given UniqueID.
		/// </summary>
		/// <param name="UniqueID">The Unique ID of the object.</param>
		protected GBMObject(UInt16 UniqueID) {
			this.Header = new GBMObjectHeader(GBMInitialization.GetTypeID(this.GetType()), UniqueID, null, 0);
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
			using (MemoryStream ms = new MemoryStream()) {
				ms.Position = 0;

				if (loadedData != null) {
					//Write data, but then allow overwriting.
					ms.Write(loadedData, 0, loadedData.Length);
					ms.Position = 0;
				}
				this.SaveToStream(ms);

				this.Header.SetSize((UInt32)ms.Length);
				s.WriteGBMObjectHeader(this.Header);

				ms.Position = 0;
				ms.CopyTo(s);
				//s.Write(ms.ToArray(), 0, (int)this.Header.Size);
			}
		}

		protected abstract void LoadFromStream(Stream s);
		protected abstract void SaveToStream(Stream s);

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
			TreeNode root = new TreeNode(Header.ToString());

			root.Nodes.Add(Header.ToTreeNode());

			if (extraData != null) {
				TreeNode extraNode = new TreeNode("Extra unknown data (" + extraData.Length + " bytes)");
				extraNode.Nodes.Add(string.Join(" ", extraData.Select(x => x.ToString()).ToArray()));

				root.Nodes.Add(extraNode);
			}

			return root;
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
			protected set { base.Master = value; this.Header.SetMaster(this.Master); }
		}

		protected MasteredGBMObject(UInt16 UniqueID, GBMFile file) : base(UniqueID) {
			this.Master = file.GetOrCreateObjectOfType<TMaster>();

			this.Header.SetMaster(this.Master);
		}

		protected MasteredGBMObject(TMaster Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }
	}
	
	public class GBMObjectHeader
	{
		/// <summary>
		/// The marker text - should ALWAYS be "HPJMTL".
		/// </summary>
		public byte[] Marker { get; private set; }

		/// <summary>
		/// The typeid of this object, which should remain constant.
		/// </summary>
		public UInt16 ObjectType { get; private set; }

		/// <summary>
		/// The Unique Object ID of the object.
		/// </summary>
		public UInt16 ObjectID { get; private set; }

		/// <summary>
		/// If this object is a sub-object, this contains the main object's ID.
		/// If it is null, this is not a subobject.  A value of 0 is treated as null.
		/// </summary>
		public UInt16? MasterID { get; private set; }

		/// <summary>
		/// The CRC of the object, which is currently unused.
		/// 
		/// If 0, it has not yet been calculated.
		/// </summary>
		public UInt32 CRC { get; private set; }

		/// <summary>
		/// The size that this object was deserialized with.
		/// </summary>
		public UInt32 Size{ get; private set; }

		public GBMObjectHeader(UInt16 ObjectType, UInt16 ObjectID, UInt16? MasterID, UInt32 Size) {
			this.Marker = new byte[] { 0x48, 0x50, 0x4A, 0x4D, 0x54, 0x4C }; //"HPJTML"
			this.ObjectType = ObjectType;
			this.ObjectID = ObjectID;
			this.MasterID = MasterID;
			this.CRC = 0x00000000U;
			this.Size = Size;
		}
		
		public void SetSize(UInt32 newSize) {
			this.Size = newSize;
		}

		public void SetMaster(GBMObject Master) {
			if (Master == null) {
				this.MasterID = null;
			} else {
				if (Master.Header.ObjectID == 0) {
					throw new InvalidOperationException("Cannot set the master object to object 0!  Please don't!");
				} else {
					this.MasterID = Master.Header.ObjectID;
				}
			}
		}

		public GBMObjectHeader(byte[] Marker, UInt16 ObjectType, UInt16 ObjectID, UInt16? MasterID, UInt32 CRC, UInt32 Size) {
			this.Marker = Marker;
			this.ObjectType = ObjectType;
			this.ObjectID = ObjectID;
			this.MasterID = MasterID;
			this.CRC = CRC;
			this.Size = Size;
		}
		
		/// <summary>
		/// Validates the marker text (and mabye the CRC later).
		/// If all goes well, this method does nothing.  Otherwise, an exception is thrown.
		/// </summary>
		/// <param name="index">Optional value that provides the location in the file of this object, if available.  Otherwise, <c>null</c>.</param>
		/// <exception cref="Exception">Thrown when the marker is invalid.</exception>
		public void Validate(long? index = null) {
			if (!this.Marker.SequenceEqual(new byte[] { 0x48, 0x50, 0x4A, 0x4D, 0x54, 0x4C })) {
				String wantedHex = String.Join(" ", Encoding.ASCII.GetBytes("HPJMTL").Select(b => b.ToString("X2")));
				String markerHex = String.Join(" ", Marker.Select(b => b.ToString("X2")));
				
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

		public override string ToString() {
			return String.Format("{0} (0x{1:X4}) - ID {2:X4}, size {3}", GBMInitialization.GetTypeString(ObjectType), ObjectType, ObjectID, Size);
		}

		public TreeNode ToTreeNode() {
			TreeNode node = new TreeNode("Header");

			TreeNode marker = new TreeNode("Marker: " + Encoding.ASCII.GetString(this.Marker));
			for (int i = 0; i < this.Marker.Length; i++) {
				marker.Nodes.Add(i.ToString(), String.Format("{0:X2} ('{1}')", this.Marker[i],
					Encoding.ASCII.GetString(this.Marker, i, 1))); //Get the ascii char version of Marker[i].
			}

			node.Nodes.Add(marker);
			node.Nodes.Add("Type", "Type: " + GBMInitialization.GetTypeString(this.ObjectType) + " (0x" + this.ObjectType.ToString("X4") + ")");
			node.Nodes.Add("ObjectID", "ObjectID: " + this.ObjectID.ToString("X4"));
			node.Nodes.Add("MasterID", "MasterID: " + (this.MasterID.HasValue ? this.MasterID.Value.ToString("X4") : "None"));
			node.Nodes.Add("CRC", "CRC: " + this.CRC.ToString("X8"));
			node.Nodes.Add("Size", "Size: " + this.Size);

			return node;
		}
	}
}
