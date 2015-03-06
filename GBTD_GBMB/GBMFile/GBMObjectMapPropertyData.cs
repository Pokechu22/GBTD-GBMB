using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapPropertyData : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapPropertyData(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectMapPropertyData(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		/// <summary>
		/// The data for each property.
		/// 
		/// Order: [x, y, prop]
		/// </summary>
		public UInt16[, ,] Data;

		protected override void LoadFromStream(Stream s) {
			Data = new UInt16[Master.Width, Master.Height, Master.PropCount];

			for (int prop = 0; prop < Master.PropCount; prop++) {
				for (int y = 0; y < Master.Height; y++) {
					for (int x = 0; x < Master.Width; x++) {
						Data[x, y, prop] = s.ReadWord();
					}
				}
			}
		}

		protected override void SaveToStream(Stream s) {
			for (int prop = 0; prop < Master.PropCount; prop++) {
				for (int y = 0; y < Master.Height; y++) {
					for (int x = 0; x < Master.Width; x++) {
						s.WriteWord(Data[x, y, prop]);
					}
				}
			}
		}

		public override string GetTypeName() {
			return "Map Property Data";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			for (int y = 0; y < Master.Height; y++) {
				TreeNode row = new TreeNode("Row " + y);

				for (int x = 0; x < Master.Width; x++) {
					TreeNode tile = new TreeNode("Tile " + x + ", " + y);

					for (int prop = 0; prop < Master.PropCount; prop++) {
						tile.Nodes.Add(prop.ToString(), "Property " + prop + ": " + Data[x, y, prop]);
					}

					row.Nodes.Add(tile);
				}

				root.Nodes.Add(row);
			}

			return root;
		}
	}
}
