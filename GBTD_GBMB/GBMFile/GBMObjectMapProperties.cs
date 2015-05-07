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
		public GBMObjectMapProperties(UInt16 UniqueID, GBMFile File) : base(UniqueID, File) {
			this.properties = new GBMObjectMapPropertiesRecord[Master.PropCount];

			for (int i = 0; i < properties.Length; i++) {
				properties[i] = new GBMObjectMapPropertiesRecord();
			}

			Master.PropCountChanged += new EventHandler(Master_PropCountChanged);
		}

		public GBMObjectMapProperties(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) {
			Master.PropCountChanged += new EventHandler(Master_PropCountChanged);
		}

		private GBMObjectMapPropertiesRecord[] properties;

		public GBMObjectMapPropertiesRecord[] Properties {
			get { return properties; }
			set {
				properties = value;
				if (Master.PropCount != properties.Length) {
					Master.PropCount = (uint)properties.Length;
				}
			}
		}

		protected override void LoadFromStream(Stream s) {
			this.properties = new GBMObjectMapPropertiesRecord[Master.PropCount];

			for (int i = 0; i < Properties.Length; i++) {
				properties[i] = new GBMObjectMapPropertiesRecord(s);
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

		private void Master_PropCountChanged(object sender, EventArgs e) {
			uint oldPropCount = (uint)Properties.Length;
			uint newPropCount = Master.PropCount;

			GBMObjectMapPropertiesRecord[] newProperties = new GBMObjectMapPropertiesRecord[newPropCount];

			for (int i = 0; i < newPropCount; i++) {
				if (i >= oldPropCount) { //Would be out of bounds in origional data
					newProperties[i] = new GBMObjectMapPropertiesRecord();
				} else {
					newProperties[i] = Properties[i];
				}
			}

			this.Properties = newProperties;
		}
	}
}
