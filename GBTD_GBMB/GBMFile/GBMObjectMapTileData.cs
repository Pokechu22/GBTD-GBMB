using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapTileData : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapTileData(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectMapTileData(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		/// <summary>
		/// Tiles, ordered by x, y.
		/// </summary>
		public GBMObjectMapTileDataRecord[,] Tiles { get; set; }

		protected override void SaveToStream(Stream s) {
			for (int y = 0; y < Master.Height; y++) {
				for (int x = 0; x < Master.Width; x++) {
					Tiles[x, y].SaveToStream(s);
				}
			}
		}

		protected override void LoadFromStream(Stream s) {
			Tiles = new GBMObjectMapTileDataRecord[Master.Width, Master.Height];

			for (int y = 0; y < Master.Height; y++) {
				for (int x = 0; x < Master.Width; x++) {
					Tiles[x, y] = new GBMObjectMapTileDataRecord(s);
				}
			}
		}

		public override string GetTypeName() {
			return "Map tile data";
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = CreateRootTreeNode();

			TreeNode tiles = new TreeNode("Tiles");
			for (int y = 0; y < Master.Height; y++) {
				TreeNode row = new TreeNode("Row " + y);
				for (int x = 0; x < Master.Width; x++) {
					row.Nodes.Add(Tiles[x, y].ToTreeNode("Tile " + x + ", " + y));
				}
				tiles.Nodes.Add(row);
			}
			node.Nodes.Add(tiles);

			return node;
		}
	}
}
