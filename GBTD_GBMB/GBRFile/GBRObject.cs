using System;
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

		protected GBRObject(UInt16 UniqueID) {
			this.Header = new GBRObjectHeader(GBRInitialization.GetTypeID(this.GetType()), UniqueID, 0);
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
				using (MemoryStream ns = new MemoryStream(loadedData.Length)) {
					ns.Position = 0;
					ns.Write(loadedData, 0, loadedData.Length);
					ns.Position = 0;
					this.SaveToStream(ns);

					this.Header.Size = (UInt32)ns.Length;
					s.WriteHeader(this.Header);
					s.Write(ns.ToArray(), 0, (int)this.Header.Size);
				}
			}
		}

		/// <summary>
		/// Saves the object to the given stream.
		/// </summary>
		/// <param name="file">The current GBRFile.</param>
		/// <param name="s">The stream to save to.</param>
		protected abstract void SaveToStream(GBRFile file, Stream s);
		/// <summary>
		/// Loads the object from the given stream.
		/// </summary>
		/// <param name="file">The current GBRFile.</param>
		/// <param name="s">The stream to load from.</param>
		protected abstract void LoadFromStream(GBRFile file, Stream s);
		/// <summary>
		/// Used to set up the given object when creating a new one.  Set up any initialization logic here, such as events or master objects.
		/// 
		/// <para>Will NOT be called when loading from the stream.</para>
		/// </summary>
		/// <param name="file"></param>
		protected virtual void SetupObject(GBRFile file) { }

		/// <summary>
		/// Reads an object and its Header and returns said object.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static GBRObject ReadObject(Stream s) {
			GBRObjectHeader h = s.ReadHeader();

			GBRObject exportable;
			if (mapping.ContainsKey(h.ObjectTypeID)) {
				var ctor = mapping[h.ObjectTypeID].GetConstructor(new Type[] { typeof(GBRObjectHeader), typeof(Stream) });
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
		public virtual TreeNode ToTreeNode() {
			TreeNode node = new TreeNode(String.Format("{0} ({1:X4}) - #{2:X4}, size {3}", GetTypeName(), Header.ObjectTypeID, Header.UniqueID, Header.Size)); //TODO: this should all be done in Header.ToString().  But we'd need to have the name in GBRInitialization.

			if (extraData != null) {
				TreeNode extraNode = new TreeNode("Extra unknown data (" + extraData.Length + " bytes)");
				extraNode.Nodes.Add(string.Join(" ", extraData.Select(x => x.ToString()).ToArray()));

				node.Nodes.Add(extraNode);
			}

			return node;
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

	public class GBRObjectHeader
	{
		/// <summary>
		/// The typeid of this object, which should remain constant.
		/// </summary>
		public readonly UInt16 ObjectTypeID;

		/// <summary>
		/// The Unique ID of the object.
		/// </summary>
		public readonly UInt16 UniqueID;

		/// <summary>
		/// The size that this object was deserialized with.
		/// </summary>
		public UInt32 Size;

		public GBRObjectHeader(UInt16 ObjectTypeID, UInt16 UniqueID, UInt32 Size) {
			this.ObjectTypeID = ObjectTypeID;
			this.UniqueID = UniqueID;
			this.Size = Size;
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

		protected override void LoadFromStream(GBRFile file, Stream s) {
			UInt16 ReferedObjectUniqueID = s.ReadWord();
			this.ReferedObject = file.GetObjectWithID<TRefered>(ReferedObjectUniqueID);
		}

		protected override void SaveToStream(GBRFile file, Stream s) {
			s.WriteWord(ReferedObjectUniqueID);
		}

		protected override void SetupObject(GBRFile file) {
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
