using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// A list of names and types of all properties.
	/// </summary>
	public class GBMObjectMapProperties : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapProperties(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectMapProperties(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		public GBMObjectMapPropertiesRecord[] Properties { get; set; }

		protected override void LoadFromStream(Stream s) {
			this.Properties = new GBMObjectMapPropertiesRecord[Master.PropCount];

			for (int i = 0; i < Properties.Length; i++) {
				Properties[i] = new GBMObjectMapPropertiesRecord(s);
			}
		}

		protected override void SaveToStream(Stream s) {
			for (int i = 0; i < Properties.Length; i++) {
				Properties[i].SaveToStream(s);
			}
		}

		public override string GetTypeName() {
			return "Map properties list";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			for (int i = 0; i < Properties.Length; i++) {
				root.Nodes.Add(Properties[i].ToTreeNode("Property " + i));
			}

			return root;
		}
	}
}
