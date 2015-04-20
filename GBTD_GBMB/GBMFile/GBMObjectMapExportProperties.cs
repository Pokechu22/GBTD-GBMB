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
				UInt32 Size, Stream stream) : base(Master, TypeID, UniqueID, MasterID, Size, stream) {

			Master.ExportPropCountChanged += new EventHandler(Master_ExportPropCountChanged);
		}

		public GBMObjectMapExportProperties(GBMObjectMapExportSettings Master, GBMObjectHeader header, Stream stream)
				: base(Master, header, stream) {

			Master.ExportPropCountChanged += new EventHandler(Master_ExportPropCountChanged);
		}

		private GBMObjectMapExportPropertiesRecord[] data;
		public GBMObjectMapExportPropertiesRecord[] Data {
			get { return data; }
			set {
				data = value;
				if (Master.ExportPropCount != data.Length) {
					Master.ExportPropCount = (UInt16)data.Length;
				}
			}
		}

		protected override void LoadFromStream(Stream s) {
			Data = new GBMObjectMapExportPropertiesRecord[Master.ExportPropCount];

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

		private void Master_ExportPropCountChanged(object sender, EventArgs e) {
			uint oldExportPropCount = (uint)Data.Length;
			uint newExportPropCount = Master.ExportPropCount;

			GBMObjectMapExportPropertiesRecord[] newData = new GBMObjectMapExportPropertiesRecord[newExportPropCount];

			for (int i = 0; i < newExportPropCount; i++) {
				if (i > oldExportPropCount) { //Would be out of bounds in old data
					newData[i] = new GBMObjectMapExportPropertiesRecord();
				} else {
					newData[i] = Data[i];
				}
			}

			this.Data = newData;
		}
	}
}
