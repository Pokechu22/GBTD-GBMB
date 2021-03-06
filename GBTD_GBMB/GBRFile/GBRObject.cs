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
		internal byte[] loadedData;
		/// <summary>
		/// Extra data after the end of the object.
		/// </summary>
		internal byte[] extraData;

		/// <summary>
		/// The Header of this object.
		/// </summary>
		public GBRObjectHeader Header { get; protected set; }

		protected GBRObject(UInt16 UniqueID) {
			this.Header = new GBRObjectHeader(GBRInitialization.GetTypeID(this.GetType()), UniqueID, 0);
		}

		/// <summary>
		/// Saves the object to the given stream.
		/// </summary>
		/// <param name="file">The current GBRFile.</param>
		/// <param name="s">The stream to save to.</param>
		protected internal abstract void SaveToStream(GBRFile file, Stream s);
		/// <summary>
		/// Loads the object from the given stream.
		/// </summary>
		/// <param name="file">The current GBRFile.</param>
		/// <param name="s">The stream to load from.</param>
		protected internal abstract void LoadFromStream(GBRFile file, Stream s);
		/// <summary>
		/// Used to set up the given object when creating a new one.  Set up any initialization logic here, such as events or master objects.
		/// 
		/// <para>Will NOT be called when loading from the stream.</para>
		/// </summary>
		/// <param name="file"></param>
		protected internal virtual void SetupObject(GBRFile file) { }

		/// <summary>
		/// Converts to a treenode, for debug purposes.
		/// </summary>
		/// <returns></returns>
		public virtual TreeNode ToTreeNode() {
			TreeNode node = new TreeNode(Header.ToString());

			TreeNode headerNode = new TreeNode("Header");
			headerNode.Nodes.Add("ObjectTypeID", "ObjectTypeID: " + Header.ObjectTypeID + "(" + GBRInitialization.GetTypeString(this) + ")");
			headerNode.Nodes.Add("UniqueID", "UniqueID: " + Header.UniqueID);
			headerNode.Nodes.Add("Size", "Size: " + Header.Size);

			if (extraData != null) {
				TreeNode extraNode = new TreeNode("Extra unknown data (" + extraData.Length + " bytes)");
				extraNode.Nodes.Add(string.Join(" ", extraData.Select(x => x.ToString()).ToArray()));

				node.Nodes.Add(extraNode);
			}

			return node;
		}
	}

	public class GBRObjectHeader
	{
		/// <summary>
		/// The typeid of this object, which should remain constant.
		/// </summary>
		public UInt16 ObjectTypeID { get; private set; }

		/// <summary>
		/// The Unique ID of the object.
		/// </summary>
		public UInt16 UniqueID { get; private set; }

		/// <summary>
		/// The size that this object was deserialized with.
		/// </summary>
		public UInt32 Size { get; internal set; }

		public GBRObjectHeader(UInt16 ObjectTypeID, UInt16 UniqueID, UInt32 Size) {
			this.ObjectTypeID = ObjectTypeID;
			this.UniqueID = UniqueID;
			this.Size = Size;
		}

		public override string ToString() {
			return String.Format("{0} (0x{1:X4}) - ID {2:X4}, size {3}", GBRInitialization.GetTypeString(ObjectTypeID), ObjectTypeID, UniqueID, Size);
		}
	}

	/// <summary>
	/// A GBRObject that refers to another GBRObject.
	/// </summary>
	public abstract class ReferentialGBRObject<TRefered> : GBRObject
		where TRefered : GBRObject
	{
		protected ReferentialGBRObject(UInt16 UniqueID) : base(UniqueID) {
			this.ReferedObject = null;
		}

		protected internal override void LoadFromStream(GBRFile file, Stream s) {
			UInt16 ReferedObjectUniqueID = s.ReadWord();
			this.ReferedObject = file.GetObjectWithID<TRefered>(ReferedObjectUniqueID);
		}

		protected internal override void SaveToStream(GBRFile file, Stream s) {
			s.WriteWord(ReferedObjectUniqueID);
		}

		protected internal override void SetupObject(GBRFile file) {
			base.SetupObject(file);

			this.ReferedObject = file.GetOrCreateObjectOfType<TRefered>();
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = base.ToTreeNode();

			node.Nodes.Add("ReferedObjectUniqueID", "ReferedObjectUniqueID: " + ReferedObjectUniqueID);

			return node;
		}

		public TRefered ReferedObject { get; private set; }
		public UInt16 ReferedObjectUniqueID { get { return ReferedObject.Header.UniqueID; } }
	}
}
