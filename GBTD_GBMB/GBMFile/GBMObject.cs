﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GBMFile
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

		protected GBMObject(UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream) {
			this.Header = new GBMObjectHeader(TypeID, UniqueID, MasterID, Size);
			LoadObject(stream);
		}

		protected GBMObject(GBMObjectHeader header, Stream stream) {
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

		protected abstract void SaveToStream(Stream s);
		protected abstract void LoadFromStream(Stream s);

		/// <summary>
		/// Reads an object and its Header and returns said object.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static GBMObject ReadObject(Stream s) {
			GBMObjectHeader h = s.ReadGBMObjectHeader();

			GBMObject obj;
			if (mapping.ContainsKey(h.ObjectType)) {
				//Use reflection to create an instance of the specified object.
				var ctor = mapping[h.ObjectType].GetConstructor(new Type[] { typeof(GBMObjectHeader), typeof(Stream) });
				obj = (GBMObject)ctor.Invoke(new Object[] { h, s });
			} else {
				obj = new GBMObjectUnknownData(h, s);
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
			//RegisterExportable(0x01, typeof(GBRObjectProducerInfo));
			//RegisterExportable(0x02, typeof(GBRObjectTileData));
			//RegisterExportable(0x03, typeof(GBRObjectTileSettings));
			//RegisterExportable(0x04, typeof(GBRObjectTileExport));
			//RegisterExportable(0x05, typeof(GBRObjectTileImport));
			//RegisterExportable(0x0D, typeof(GBRObjectPalettes));
			//RegisterExportable(0x0E, typeof(GBRObjectTilePalette));
		}
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