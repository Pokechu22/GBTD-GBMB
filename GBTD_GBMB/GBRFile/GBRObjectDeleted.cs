using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Deleted GBR file data.
	/// Just an array of bytes; can't be used to do anything.
	/// </summary>
	public class GBRObjectDeleted : GBRObject
	{
		private byte[] data;

		public GBRObjectDeleted(UInt16 UniqueID) : base(UniqueID) {
			this.data = new byte[0];
		}

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
			returned.Nodes.Add(String.Join(" ", this.data.Select(x => x.ToString("X2")).ToArray()));

			AddExtraDataToTreeNode(returned);

			return returned;
		}
	}
}
