﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GB.Shared.GBMFile
{
	public class GBMObjectUnknownData : GBMObject
	{
		private byte[] data;

		public GBMObjectUnknownData(GBMObject Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

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
			TreeNode data = new TreeNode("Data");
			data.Nodes.Add(String.Join(" ", this.data.Select(x => x.ToString("X2")).ToArray()));
			returned.Nodes.Add(data);

			return returned;
		}
	}
}
