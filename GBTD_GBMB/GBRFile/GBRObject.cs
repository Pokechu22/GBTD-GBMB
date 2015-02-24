﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Something that can be exported to a .GBR file.
	/// </summary>
	public abstract class GBRObject
	{
		/// <summary>
		/// The data loaded when origionally deserialized.
		/// </summary>
		private byte[] loadedData;

		protected byte[] extraData;

		private static Dictionary<UInt16, Type> mapping = new Dictionary<UInt16, Type>();

		/// <summary>
		/// The Header of this object.
		/// </summary>
		public GBRObjectHeader Header { get; protected set; }

		protected GBRObject(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) {
			this.Header = new GBRObjectHeader(TypeID, UniqueID, Size);
			LoadObject(stream);
		}

		protected GBRObject(GBRObjectHeader header, Stream stream) {
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
				s.WriteHeader(this.Header);
				this.SaveToStream(s);
			} else {
				using (MemoryStream ns = new MemoryStream(loadedData, true)) {
					ns.Position = 0;
					this.SaveToStream(ns);

					this.Header = this.Header.Resize((UInt32)ns.Length);
					s.WriteHeader(this.Header);
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
		public static GBRObject ReadObject(Stream s) {
			GBRObjectHeader h = s.ReadHeader();

			GBRObject exportable;
			if (mapping.ContainsKey(h.ObjectID)) {
				var ctor = mapping[h.ObjectID].GetConstructor(new Type[] { typeof(GBRObjectHeader), typeof(Stream) });
				exportable = (GBRObject)ctor.Invoke(new Object[] { h, s });
			} else {
				exportable = new GBRObjectUnknownData(h, s);
			}

			return exportable;
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
			return new TreeNode(GetTypeName() + " (" + this.Header.ObjectID.ToString("X4") + ") - #" + this.Header.UniqueID.ToString("X4") + ", size " + this.Header.Size);
		}

		/// <summary>
		/// Adds the extradata to the treenode, if present.
		/// </summary>
		/// <param name="node"></param>
		protected void AddExtraDataToTreeNode(TreeNode node) {
			if (extraData != null) {
				TreeNode extraNode = new TreeNode("Extra unknown data (" + extraData.Length + " bytes)");
				extraNode.Nodes.Add(string.Join(" ", extraData.Select(x => x.ToString()).ToArray()));

				node.Nodes.Add(extraNode);
			}
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

		static GBRObject() {
			RegisterExportable(0xFF, typeof(GBRObjectDeleted));
			RegisterExportable(0x01, typeof(GBRObjectProducerInfo));
			RegisterExportable(0x02, typeof(GBRObjectTileData));
			RegisterExportable(0x03, typeof(GBRObjectTileSettings));
			RegisterExportable(0x04, typeof(GBRObjectTileExport));
			RegisterExportable(0x05, typeof(GBRObjectTileImport));
			RegisterExportable(0x0D, typeof(GBRObjectPalettes));
			RegisterExportable(0x0E, typeof(GBRObjectTilePalette));
		}
	}

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

	public interface IReferentialGBRObject
	{
		UInt16 ReferedObjectID { get; set; }
	}
}
