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
		public GBRObjectTileData(UInt16 UniqueID) : base(UniqueID) {
			this.Name = "";
			this.width = 8;
			this.height = 8;
			this.count = 128;
			this.Color0Mapping = GBColor.WHITE;
			this.Color1Mapping = GBColor.DARK_GRAY;
			this.Color2Mapping = GBColor.LIGHT_GRAY;
			this.Color3Mapping = GBColor.BLACK;
			this.Tiles = new Tile[Count]; //TODO: This may be wrong.
		}

		/// <summary>
		/// The user-facing name of the tileset.
		/// </summary>
		public string Name { get; set; }

		private UInt16 width;
		private UInt16 height;
		private UInt16 count;

		/// <summary>
		/// Width of each individual tile.
		/// </summary>
		public UInt16 Width {
			get { return width; }
			set {
				if (value != width) {
					value = width;
					OnSizeChanged();
				}
			}
		}
		/// <summary>
		/// Height of each individual tile.
		/// </summary>
		public UInt16 Height {
			get { return height; }
			set {
				if (value != height) {
					value = height;
					OnSizeChanged();
				}
			}
		}
		/// <summary>
		/// Total number of tiles in the file.
		/// </summary>
		public UInt16 Count {
			get { return count; }
			set {
				if (value != count) {
					value = count;
					OnCountChanged();
				}
			}
		}

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

		private Tile[] tiles;
		/// <summary>
		/// The actual tile data payload.
		/// </summary>
		public Tile[] Tiles {
			get { return tiles; }
			set {
				tiles = value;
				if (tiles.Length != Count) {
					Count = (UInt16)tiles.Length;
				}
			}
		}

		/// <summary>
		/// Fires whenever the number of tiles changes.  This will also occur when the tile size changes.
		/// </summary>
		public event EventHandler CountChanged;
		/// <summary>
		/// Fires whenever the size of each tlie changes.
		/// </summary>
		public event EventHandler SizeChanged;

		private void OnCountChanged() {
			//TODO: Resize tiles array.

			if (CountChanged != null) {
				CountChanged(this, new EventArgs());
			}
		}
		private void OnSizeChanged() {
			//TODO: Resize tiles array.

			if (SizeChanged != null) {
				SizeChanged(this, new EventArgs());
			}
		}

		protected internal override void SaveToStream(GBRFile file, Stream s) {
			s.WriteString(Name, 30);

			s.WriteWord(Width);
			s.WriteWord(Height);
			s.WriteWord(Count);

			s.WriteByte((byte)Color0Mapping);
			s.WriteByte((byte)Color1Mapping);
			s.WriteByte((byte)Color2Mapping);
			s.WriteByte((byte)Color3Mapping);

			for (int i = 0; i < Tiles.Length; i++) {
				Tile tile = Tiles[i];
				for (int y = 0; y < Height; y++) {
					for (int x = 0; x < Width; x++) {
						s.WriteByte(GBColorToByte(tile[x, y]));
					}
				}
			}
		}

		protected internal override void LoadFromStream(GBRFile file, Stream s) {
			Name = s.ReadString(30);

			Width = s.ReadWord();
			Height = s.ReadWord();
			Count = s.ReadWord();

			Color0Mapping = (GBColor)s.ReadByte();
			Color1Mapping = (GBColor)s.ReadByte();
			Color2Mapping = (GBColor)s.ReadByte();
			Color3Mapping = (GBColor)s.ReadByte();

			Tiles = new Tile[Count];

			for (int i = 0; i < Tiles.Length; i++) {
				Tile tile = new Tile(Width, Height);
				for (int y = 0; y < Height; y++) {
					for (int x = 0; x < Width; x++) {
						int read = s.ReadByte();

						if (read < 0) { throw new EndOfStreamException(); }

						tile[x, y] = ByteToGBColor((byte)read);
					}
				}
				Tiles[i] = tile;
			}
		}

		private static GBColor ByteToGBColor(byte b) {
			switch (b) {
			case 0: return GBColor.WHITE;
			case 1: return GBColor.LIGHT_GRAY;
			case 2: return GBColor.DARK_GRAY;
			case 3: return GBColor.BLACK;
			default: return (GBColor)b; //Will be invalid, but we shouldn't mess with it.
			}
		}

		private static byte GBColorToByte(GBColor c) {
			switch (c) {
			case GBColor.WHITE: return 0;
			case GBColor.LIGHT_GRAY: return 1;
			case GBColor.DARK_GRAY: return 2;
			case GBColor.BLACK: return 3;
			default: return (byte)c;
			}
		}

		public override TreeNode ToTreeNode() {
			const char BLACK = '\u2588', DARK_GRAY = '\u2593', LIGHT_GRAY = '\u2592', WHITE = '\u2591';

			TreeNode returned = base.ToTreeNode();

			returned.Nodes.Add("name", "Name: " + Name);
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
						named.Append(Tiles[i][x, y]).Append(' ');
						numbered.Append(Tiles[i][x, y]).Append(' ');
						switch (Tiles[i][x, y]) {
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
