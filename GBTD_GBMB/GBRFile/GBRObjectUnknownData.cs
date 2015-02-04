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

		public GBRObjectUnknownData(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRObjectUnknownData(GBRObjectHeader header, Stream stream) : base(header, stream) { }

		protected override void SaveToStream(Stream s) {
			s.Write(data, 0, (int)Header.Size);
		}

		protected override void LoadFromStream(Stream s) {
			data = new byte[Header.Size];
			s.Read(data, 0, (int)Header.Size);
		}

		public override string GetTypeName() {
			return "Unknown Data";
		}

		public override TreeNode ToTreeNode() {
			TreeNode returned = CreateRootTreeNode();
			returned.Nodes.Add(String.Join(" ", this.data.Select(x => x.ToString("X2")).ToArray()));

			AddExtraDataToTreeNode(returned);

			return returned;
		}
	}
}
