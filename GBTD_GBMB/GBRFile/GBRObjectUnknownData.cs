using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Unknown GBR file data.
	/// Just an array of bytes; can't be used to do anything.
	/// </summary>
	public class GBRObjectUnknownData : GBRObject
	{
		private byte[] data;

		public GBRObjectUnknownData(GBRObjectHeader Header) : base(Header.UniqueID) {
			this.Header = Header;
			this.data = new byte[0];
		}

		protected internal override void SaveToStream(GBRFile file, Stream s) {
			s.Write(data, 0, (int)Header.Size);
		}

		protected internal override void LoadFromStream(GBRFile file, Stream s) {
			data = new byte[Header.Size];
			s.Read(data, 0, (int)Header.Size);
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = base.ToTreeNode();

			node.Nodes.Add(String.Join(" ", this.data.Select(x => x.ToString("X2")).ToArray()));

			return node;
		}
	}
}
