using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GBMFile
{
	/// <summary>
	/// A GBMObject that has been deleted.
	/// You cannot access its data.
	/// </summary>
	public class GBMObjectDeleted : GBMObject
	{
		private byte[] data;

		public GBMObjectDeleted(UInt16 TypeID, UInt16 UniqueID, UInt16 MasterID, UInt32 Size, Stream stream)
			: base(TypeID, UniqueID, MasterID, Size, stream) { }
		public GBMObjectDeleted(GBMObjectHeader header, Stream stream) : base(header, stream) { }

		protected override void SaveToStream(Stream s) {
			s.Write(data, 0, (int)Header.Size);
		}

		protected override void LoadFromStream(Stream s) {
			data = new byte[Header.Size];
			s.Read(data, 0, (int)Header.Size);
		}

		public override string GetTypeName() {
			return "[DELETED]";
		}

		public override TreeNode ToTreeNode() {
			TreeNode returned = CreateRootTreeNode();
			TreeNode data = new TreeNode("Data");
			data.Nodes.Add(String.Join(" ", this.data.Select(x => x.ToString("X2")).ToArray()));
			returned.Nodes.Add(data);

			return returned;
		}
	}
}
