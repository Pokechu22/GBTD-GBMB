using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapExportProperties : MasteredGBMObject<GBMObjectMapExportSettings>
	{
		public GBMObjectMapExportProperties(GBMObjectMapExportSettings Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID,
			UInt32 Size, Stream stream) : base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectMapExportProperties(GBMObjectMapExportSettings Master, GBMObjectHeader header, Stream stream)
			: base(Master, header, stream) { }

		public GBMObjectMapExportPropertiesRecord[] Data { get; set; }

		protected override void LoadFromStream(Stream s) {
			Data = new GBMObjectMapExportPropertiesRecord[Master.PropCount];

			for (int i = 0; i < Data.Length; i++) {
				Data[i] = new GBMObjectMapExportPropertiesRecord(s);
			}
		}

		protected override void SaveToStream(Stream s) {
			for (int i = 0; i < Data.Length; i++) {
				Data[i].SaveToStream(s);
			}
		}

		public override string GetTypeName() {
			return "Map export properties";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			for (int i = 0; i < Data.Length; i++) {
				root.Nodes.Add(Data[i].ToTreeNode(i.ToString()));
			}

			return root;
		}
	}
}
