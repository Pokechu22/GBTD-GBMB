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
		public GBMObjectMapTileData(UInt16 UniqueID, GBMFile File) : base(UniqueID, File) {
			Tiles = new GBMObjectMapTileDataRecord[Master.Width, Master.Height];

			for (int x = 0; x < Master.Width; x++) {
				for (int y = 0; y < Master.Height; y++) {
					Tiles[x, y] = new GBMObjectMapTileDataRecord();
				}
			}

			Master.SizeChanged += new EventHandler(Master_SizeChanged);
		}

		public GBMObjectMapTileData(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) {
			Master.SizeChanged += new EventHandler(Master_SizeChanged);
		}

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

		private void Master_SizeChanged(object sender, EventArgs e) {
			uint oldWidth = (uint)Tiles.GetLength(0);
			uint oldHeight = (uint)Tiles.GetLength(1);
			uint newWidth = Master.Width;
			uint newHeight = Master.Height;

			GBMObjectMapTileDataRecord[,] newTiles = new GBMObjectMapTileDataRecord[newWidth, newHeight];

			for (uint y = 0; y < newHeight; y++) {
				for (uint x = 0; x < newWidth; x++) {
					if (x >= oldWidth || y >= oldHeight) { //If the value would be out of bounds in the origional tiles array.
						newTiles[x, y] = new GBMObjectMapTileDataRecord();
					} else {
						newTiles[x, y] = Tiles[x, y];
					}
				}
			}
			
			Tiles = newTiles;
		}
	}
}
