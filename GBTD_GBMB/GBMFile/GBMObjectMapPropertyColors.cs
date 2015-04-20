using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapPropertyColors : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapPropertyColors(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) {

			Master.PropColorCountChanged += new EventHandler(Master_PropColorCountChanged);
		}

		public GBMObjectMapPropertyColors(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) {
			Master.PropColorCountChanged += new EventHandler(Master_PropColorCountChanged);
		}

		/// <summary>
		/// The available colors in regular GBMB.
		/// </summary>
		public const int RED = 0, GREEN = 1;

		private GBMObjectMapPropertyColorsRecord[] data;
		/// <summary>
		/// The individual colors.
		/// </summary>
		public GBMObjectMapPropertyColorsRecord[] Data {
			get { return data; }
			set {
				data = value;
				if (Master.PropColorCount != data.Length) {
					Master.PropColorCount = (uint)data.Length;
				}
			}
		}

		protected override void LoadFromStream(Stream s) {
			Data = new GBMObjectMapPropertyColorsRecord[Master.PropColorCount];

			for (int i = 0; i < Data.Length; i++) {
				Data[i] = new GBMObjectMapPropertyColorsRecord(s);
			}
		}

		protected override void SaveToStream(Stream s) {
			for (int i = 0; i < Data.Length; i++) {
				Data[i].SaveToStream(s);
			}
		}

		public override string GetTypeName() {
			return "Map property colors";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			for (int i = 0; i < Data.Length; i++) {
				root.Nodes.Add(Data[i].ToTreeNode("Color " + i));
			}

			return root;
		}

		private void Master_PropColorCountChanged(object sender, EventArgs e) {
			uint oldPropColorCount = (uint)Data.Length;
			uint newPropColorCount = Master.PropColorCount;

			GBMObjectMapPropertyColorsRecord[] newData = new GBMObjectMapPropertyColorsRecord[newPropColorCount];

			for (int i = 0; i < newPropColorCount; i++) {
				if (i >= oldPropColorCount) { //If i is out of bounds in the old data array.
					newData[i] = new GBMObjectMapPropertyColorsRecord();
				} else {
					newData[i] = this.Data[i];
				}
			}

			this.Data = newData;
		}
	}
}
