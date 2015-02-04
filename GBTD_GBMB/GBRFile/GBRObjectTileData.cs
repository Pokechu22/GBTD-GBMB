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
		/// 
		/// TODO: A simple GBColor[] isn't a good way of making this.
		/// </summary>
		public GBColor[] data;

		protected override void SaveToStream(Stream s) {
			s.WriteString(name, 30);

			s.WriteWord(Width);
			s.WriteWord(Height);
			s.WriteWord(Count);

			s.WriteByte((byte)Color0Mapping);
			s.WriteByte((byte)Color1Mapping);
			s.WriteByte((byte)Color2Mapping);
			s.WriteByte((byte)Color3Mapping);

			for (int i = 0; i < data.Length; i++) {
				s.WriteByte((byte)data[i]);
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

			data = new GBColor[Width * Height * Count];

			for (int i = 0; i < data.Length; i++) {
				data[i] = (GBColor)s.ReadByte();
			}
		}

		public override string GetTypeName() {
			//return "Tile data (" + name + ")";
			return "Tile data";
		}

		public override TreeNode ToTreeNode() {
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
			int step = Width * Height;
			for (int i = 0; i < Count; i++) {
				TreeNode tile = new TreeNode("Tile " + i);

				for (int y = 0; y < Height; y++) {
					StringBuilder b = new StringBuilder();
					for (int x = 0; x < Width; x++) {
						b.Append(data[(i * step) + (y * Width) + x]).Append(' ');
					}

					tile.Nodes.Add(b.ToString());
				}

				tileData.Nodes.Add(tile);
			}
			returned.Nodes.Add(tileData);

			return returned;
		}
	}
}
