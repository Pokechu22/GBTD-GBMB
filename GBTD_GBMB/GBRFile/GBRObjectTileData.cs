using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using GB.Shared.Tiles;

namespace GB.Shared.GBRFile
{
	public class GBRObjectTileData : GBRObject
	{
		public GBRObjectTileData(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRObjectTileData(GBRObjectHeader header, Stream stream) : base(header, stream) { }

		private string name;
		/// <summary>
		/// The user-facing name of the tileset.
		/// </summary>
		public string Name {
			get { return name; }
			set { if (value == null) { throw new ArgumentNullException(); } value = name; }
		}
		/// <summary>
		/// Width of each individual tile.
		/// </summary>
		public UInt16 Width { get; set; }
		/// <summary>
		/// Height of each individual tile.
		/// </summary>
		public UInt16 Height { get; set; }
		/// <summary>
		/// Total number of tiles in the file.
		/// </summary>
		public UInt16 Count { get; set; }

		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color0Mapping { get; set; }
		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color1Mapping { get; set; }
		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color2Mapping { get; set; }
		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color3Mapping { get; set; }

		/// <summary>
		/// The actual tile data payload.
		/// </summary>
		public Tile[] tiles;

		protected override void SaveToStream(Stream s) {
			s.WriteString(name, 30);

			s.WriteWord(Width);
			s.WriteWord(Height);
			s.WriteWord(Count);

			s.WriteByte((byte)Color0Mapping);
			s.WriteByte((byte)Color1Mapping);
			s.WriteByte((byte)Color2Mapping);
			s.WriteByte((byte)Color3Mapping);

			for (int i = 0; i < tiles.Length; i++) {
				Tile tile = tiles[i];
				for (int y = 0; y < Height; y++) {
					for (int x = 0; x < Width; x++) {
						s.WriteByte((byte)tile[x, y]);
					}
				}
			}
		}

		protected override void LoadFromStream(Stream s) {
			name = s.ReadString(30);

			Width = s.ReadWord();
			Height = s.ReadWord();
			Count = s.ReadWord();

			Color0Mapping = (GBColor)s.ReadByte();
			Color1Mapping = (GBColor)s.ReadByte();
			Color2Mapping = (GBColor)s.ReadByte();
			Color3Mapping = (GBColor)s.ReadByte();

			tiles = new Tile[Count];

			for (int i = 0; i < tiles.Length; i++) {
				Tile tile = new Tile(Width, Height);
				for (int y = 0; y < Height; y++) {
					for (int x = 0; x < Width; x++) {
						int read = s.ReadByte();

						if (read < 0) { throw new EndOfStreamException(); }

						tile[x, y] = (GBColor)read;
					}
				}
				tiles[i] = tile;
			}
		}

		public override string GetTypeName() {
			//return "Tile data (" + name + ")";
			return "Tile data";
		}

		public override TreeNode ToTreeNode() {
			const char BLACK = '\u2588', DARK_GRAY = '\u2593', LIGHT_GRAY = '\u2592', WHITE = '\u2591';

			TreeNode returned = CreateRootTreeNode();

			returned.Nodes.Add("name", "Name: " + name);
			returned.Nodes.Add("width", "Width: " + Width);
			returned.Nodes.Add("height", "Height: " + Height);
			returned.Nodes.Add("count", "Count: " + Count);

			TreeNode colorMapping = new TreeNode("Color mapping");
			colorMapping.Nodes.Add("0 -> " + Color0Mapping + " (" + (byte)Color0Mapping + ")");
			colorMapping.Nodes.Add("1 -> " + Color1Mapping + " (" + (byte)Color1Mapping + ")");
			colorMapping.Nodes.Add("2 -> " + Color2Mapping + " (" + (byte)Color2Mapping + ")");
			colorMapping.Nodes.Add("3 -> " + Color3Mapping + " (" + (byte)Color3Mapping + ")");
			returned.Nodes.Add(colorMapping);

			TreeNode tileData = new TreeNode("Tile data");
			TreeNode byName = new TreeNode("By color name (not aligned)");
			TreeNode byNumber = new TreeNode("By numeric value (semi-aligned)");
			TreeNode byChar = new TreeNode("By char version of color (aligned)");

			int step = Width * Height;
			for (int i = 0; i < Count; i++) {
				TreeNode tileByName = new TreeNode("Tile " + i);
				TreeNode tileByNumber = new TreeNode("Tile " + i);
				TreeNode tileByChar = new TreeNode("Tile " + i);

				for (int y = 0; y < Height; y++) {
					StringBuilder named = new StringBuilder();
					StringBuilder numbered = new StringBuilder();
					StringBuilder chard = new StringBuilder();

					for (int x = 0; x < Width; x++) {
						named.Append(tiles[i][x, y]).Append(' ');
						numbered.Append(tiles[i][x, y]).Append(' ');
						switch (tiles[i][x, y]) {
						case GBColor.BLACK: chard.Append(BLACK).Append(' '); break;
						case GBColor.DARK_GRAY: chard.Append(DARK_GRAY).Append(' '); break;
						case GBColor.LIGHT_GRAY: chard.Append(LIGHT_GRAY).Append(' '); break;
						case GBColor.WHITE: chard.Append(WHITE).Append(' '); break;
						default: chard.Append("?").Append(' '); break;
						}
					}

					tileByName.Nodes.Add(named.ToString());
					tileByNumber.Nodes.Add(numbered.ToString());
					tileByChar.Nodes.Add(chard.ToString());
				}

				byName.Nodes.Add(tileByName);
				byNumber.Nodes.Add(tileByNumber);
				byChar.Nodes.Add(tileByChar);
			}
			tileData.Nodes.Add(byName);
			tileData.Nodes.Add(byNumber);
			tileData.Nodes.Add(byChar);

			returned.Nodes.Add(tileData);

			return returned;
		}
	}
}
