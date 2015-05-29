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
			this.Color1Mapping = GBColor.LIGHT_GRAY;
			this.Color2Mapping = GBColor.DARK_GRAY;
			this.Color3Mapping = GBColor.BLACK;
			this.Tiles = new Tile[Count];
			for (int i = 0; i < Tiles.Length; i++) {
				Tiles[i] = new Tile(Width, Height);
			}
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
				if (width != value) {
					width = value;

					ResizeTiles(width, height);
				}
			}
		}
		/// <summary>
		/// Height of each individual tile.
		/// </summary>
		public UInt16 Height {
			get { return height; }
			set {
				if (height != value) {
					height = value;

					ResizeTiles(width, height);
				}
			}
		}
		/// <summary>
		/// Total number of tiles in the file.
		/// </summary>
		public UInt16 Count {
			get { return count; }
			set {
				if (count != value) {
					count = value;

					Tile[] oldTiles = this.tiles;
					Tile[] newTiles = new Tile[count];

					for (int i = 0; i < count; i++) {
						if (i < tiles.Length) {
							newTiles[i] = oldTiles[i];
						} else {
							newTiles[i] = new Tile(width, height);
						}
					}

					this.tiles = newTiles;

					OnCountChanged();
				}
			}
		}

		private GBColor color0Mapping, color1Mapping, color2Mapping, color3Mapping;

		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color0Mapping {
			get { return color0Mapping; }
			set { color0Mapping = value; OnColorMappingChanged(); }
		}
		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color1Mapping {
			get { return color1Mapping; }
			set { color1Mapping = value; OnColorMappingChanged(); }
		}
		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color2Mapping {
			get { return color2Mapping; }
			set { color2Mapping = value; OnColorMappingChanged(); }
		}
		/// <summary>
		/// Color mapping between number and color (As with BGP_REG).
		/// </summary>
		public GBColor Color3Mapping {
			get { return color3Mapping; }
			set { color3Mapping = value; OnColorMappingChanged(); }
		}

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
		/// Gets the mapped color for the input.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public GBColor GetMappedColor(GBColor input) {
			switch (GBColorToByte(input)) {
			case 0: return color0Mapping;
			case 1: return color1Mapping;
			case 2: return color2Mapping;
			case 3: return color3Mapping;
			default: return input;
			}
		}

		/// <summary>
		/// Sets the mapped color for the input.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public void SetMappedColor(GBColor input, GBColor value) {
			switch (GBColorToByte(input)) {
			case 0: color0Mapping = value; break;
			case 1: color1Mapping = value; break;
			case 2: color2Mapping = value; break;
			case 3: color3Mapping = value; break;
			}

			OnColorMappingChanged();
		}

		/// <summary>
		/// Sets the entire color mapping.
		/// </summary>
		public void SetColorMapping(GBColor color0, GBColor color1, GBColor color2, GBColor color3) {
			this.color0Mapping = color0;
			this.color1Mapping = color1;
			this.color2Mapping = color2;
			this.color3Mapping = color3;

			OnColorMappingChanged();
		}

		/// <summary>
		/// Fires whenever the number of tiles has changed.  This will also occur when the tile size changes.
		/// </summary>
		public event EventHandler CountChanged;
		/// <summary>
		/// Fires whenever the size of each tile has changed.
		/// </summary>
		public event EventHandler SizeChanged;
		/// <summary>
		/// Fires whenever the color mapping has changed.
		/// </summary>
		public event EventHandler ColorMappingChanged;

		private void OnCountChanged() {
			if (CountChanged != null) {
				CountChanged(this, new EventArgs());
			}
		}
		private void OnSizeChanged() {
			if (SizeChanged != null) {
				SizeChanged(this, new EventArgs());
			}
		}
		private void OnColorMappingChanged() {
			if (ColorMappingChanged != null) {
				ColorMappingChanged(this, new EventArgs());
			}
		}

		protected internal override void SaveToStream(GBRFile file, Stream s) {
			s.WriteString(Name, 30);

			s.WriteWord(Width);
			s.WriteWord(Height);
			s.WriteWord(Count);

			s.WriteByte(GBColorToByte(Color0Mapping));
			s.WriteByte(GBColorToByte(Color1Mapping));
			s.WriteByte(GBColorToByte(Color2Mapping));
			s.WriteByte(GBColorToByte(Color3Mapping));

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

			Color0Mapping = ByteToGBColor(s.ReadByteEx());
			Color1Mapping = ByteToGBColor(s.ReadByteEx());
			Color2Mapping = ByteToGBColor(s.ReadByteEx());
			Color3Mapping = ByteToGBColor(s.ReadByteEx());

			Tiles = new Tile[Count];

			for (int i = 0; i < Tiles.Length; i++) {
				Tile tile = new Tile(Width, Height);
				for (int y = 0; y < Height; y++) {
					for (int x = 0; x < Width; x++) {
						tile[x, y] = ByteToGBColor(s.ReadByteEx());
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

		public void ResizeTiles(UInt16 newTileWidth, UInt16 newTileHeight) {
			UInt16 oldTileWidth = this.width;
			UInt16 oldTileHeight = this.height;

			this.width = newTileWidth;
			this.height = newTileHeight;

			if (oldTileHeight == newTileHeight && oldTileWidth == newTileWidth) {
				return;
			}

			uint oldTileCount = (uint)tiles.Length;
			GBColor[, ,] oldTiles = new GBColor[oldTileCount, oldTileWidth, oldTileHeight];

			uint newTileCount = (uint)(tiles.Length * ((oldTileWidth * oldTileHeight) / (float)(newTileWidth * newTileHeight)));
			GBColor[, ,] newTiles = new GBColor[newTileCount, newTileWidth, newTileHeight];

			for (int tile = 0; tile < oldTileCount; tile++) { 
				for (int y = 0; y < oldTileHeight; y++) {
					for (int x = 0; x < oldTileWidth; x++) {
						oldTiles[tile, x, y] = tiles[tile][x, y];
					}
				}
			}

			//Build a list of 8x8 tiles.
			const int SIZED_TILE_WIDTH = 8;
			const int SIZED_TILE_HEIGHT = 8;
			int sizedTileCount = (int)(tiles.Length * ((oldTileWidth * oldTileHeight) / (float)(8 * 8)));

			GBColor[, ,] sizedTiles = new GBColor[sizedTileCount, SIZED_TILE_WIDTH, SIZED_TILE_HEIGHT];

			int horizOldTileCount = (int)(oldTileWidth / SIZED_TILE_WIDTH);
			int vertOldTileCount = (int)(oldTileHeight / SIZED_TILE_HEIGHT);

			for (int tile = 0; tile < oldTileCount; tile++) {
				for (int y = 0; y < oldTileHeight; y++) {
					for (int x = 0; x < oldTileWidth; x++) {
						int sizedTileNum = ((x / SIZED_TILE_WIDTH) +
							((y / SIZED_TILE_HEIGHT) * horizOldTileCount) + (tile * vertOldTileCount * horizOldTileCount));

						if (sizedTileNum < sizedTileCount) {
							sizedTiles[sizedTileNum, x % SIZED_TILE_WIDTH, y % SIZED_TILE_HEIGHT] = oldTiles[tile, x, y];
						}
					}
				}
			}

			//Build the new tile list.
			int horizNewTileCount = (int)(newTileWidth / SIZED_TILE_WIDTH);
			int vertNewTileCount = (int)(newTileHeight / SIZED_TILE_HEIGHT);

			for (int tile = 0; tile < newTileCount; tile++) {
				for (int y = 0; y < newTileHeight; y++) {
					for (int x = 0; x < newTileWidth; x++) {
						int sizedTileNum = ((x / SIZED_TILE_WIDTH) +
							((y / SIZED_TILE_HEIGHT) * horizNewTileCount) + (tile * vertNewTileCount * horizNewTileCount));

						if (sizedTileNum < sizedTileCount) {
							newTiles[tile, x, y] = sizedTiles[sizedTileNum, x % SIZED_TILE_WIDTH, y % SIZED_TILE_HEIGHT];
						}
					}
				}
			}

			//Save the new tile list.
			this.tiles = new Tile[newTileCount];

			for (int tile = 0; tile < newTileCount; tile++) {
				GBColor[,] colors = new GBColor[newTileWidth, newTileHeight];
				for (int y = 0; y < newTileHeight; y++) {
					for (int x = 0; x < newTileWidth; x++) {
						colors[x, y] = newTiles[tile, x, y];
					}
				}

				tiles[tile] = new Tile(colors);
			}

			OnSizeChanged();
		}
	}
}
