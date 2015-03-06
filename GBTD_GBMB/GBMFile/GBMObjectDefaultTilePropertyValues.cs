using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// Contains the default values for each property for each tile.
	/// </summary>
	public class GBMObjectDefaultTilePropertyValues : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectDefaultTilePropertyValues(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectDefaultTilePropertyValues(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		/// <summary>
		/// All of the data.
		/// 
		/// Aranged by [Tile, Property].
		/// </summary>
		public UInt16[,] Data { get; set; }

		protected override void LoadFromStream(Stream s) {
			Data = new UInt16[Master.TileCount, Master.PropCount];

			for (int prop = 0; prop < Master.PropCount; prop++) {
				for (int tile = 0; tile < Master.TileCount; tile++) {
					Data[tile, prop] = s.ReadWord();
				}
			}
		}

		protected override void SaveToStream(Stream s) {
			for (int prop = 0; prop < Master.PropCount; prop++) {
				for (int tile = 0; tile < Master.TileCount; tile++) {
					s.WriteWord(Data[tile, prop]);
				}
			}
		}

		public override string GetTypeName() {
			return "Default tile property values";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			for (int tile = 0; tile < Master.TileCount; tile++) {
				TreeNode tileNode = new TreeNode("Tile " + tile);

				for (int prop = 0; prop < Master.PropCount; prop++) {
					tileNode.Nodes.Add(prop.ToString(), "Property " + prop + ": " + Data[tile, prop]);
				}

				root.Nodes.Add(tileNode);
			}

			return root;
		}
	}
}
