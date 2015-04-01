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
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) {

			Master.SizeChanged += new EventHandler(Master_SizeChanged);
			Master.PropCountChanged += new EventHandler(Master_PropCountChanged);
		}

		public GBMObjectMapPropertyData(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) {
			Master.SizeChanged += new EventHandler(Master_SizeChanged);
			Master.PropCountChanged += new EventHandler(Master_PropCountChanged);
		}

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

		private void Master_PropCountChanged(object sender, EventArgs e) {
			uint width = (uint)Data.GetLength(0);
			uint height = (uint)Data.GetLength(1);
			uint oldPropCount = (uint)Data.GetLength(2);
			uint newPropCount = Master.PropCount;

			UInt16[, ,] newData = new UInt16[width, height, newPropCount];

			for (int i = 0; i < newPropCount; i++) {
				for (uint y = 0; y < height; y++) {
					for (uint x = 0; x < width; x++) {
						if (i >= oldPropCount) { //If the value would be out of bounds in the origional array.
							newData[x, y, i] = 0;
						} else {
							newData[x, y, i] = Data[x, y, i];
						}
					}
				}
			}
			
			Data = newData;
		}

		private void Master_SizeChanged(object sender, EventArgs e) {
			uint propCount = (uint)Data.GetLength(2);
			uint oldWidth = (uint)Data.GetLength(0);
			uint oldHeight = (uint)Data.GetLength(1);
			uint newWidth = Master.Width;
			uint newHeight = Master.Height;

			UInt16[, ,] newData = new UInt16[newWidth, newHeight, propCount];

			for (int i = 0; i < propCount; i++) {
				for (uint y = 0; y < newHeight; y++) {
					for (uint x = 0; x < newWidth; x++) {
						if (x >= oldWidth || y >= oldHeight) { //If the value would be out of bounds in the origional array.
							newData[x, y, i] = 0;
						} else {
							newData[x, y, i] = Data[x, y, i];
						}
					}
				}
			}

			Data = newData;
		}
	}
}
