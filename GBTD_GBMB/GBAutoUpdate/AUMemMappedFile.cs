using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using GB.Shared.Tiles;
using GB.Shared.Palettes;
using GB.Shared.GBRFile;

namespace GB.Shared.AutoUpdate
{
	/// <summary>
	/// Provides access to the shared data contained in the MemoryMappedFile.
	/// TODO: Have the AUListener send its messages on changes.
	/// </summary>
	public class AUMemMappedFile : IDisposable
	{
		#region Inner classes (for named indexers)
		/// <summary>
		/// List of tiles.
		/// </summary>
		public class MMFTileList
		{
			private const int TILE_WIDTH = 8, TILE_HEIGHT = 8;

			private readonly AUMemMappedFile file;
			internal MMFTileList(AUMemMappedFile file) {
				this.file = file;
			}

			public Tile this[UInt16 tile] {
				get {
					var stream = file.stream;
					stream.Position = TILES_INDEX + (tile * TILE_WIDTH * TILE_HEIGHT);

					byte[] data = new byte[TILE_WIDTH * TILE_HEIGHT];
					var read = stream.Read(data, 0, TILE_WIDTH * TILE_HEIGHT);
					if (read != TILE_WIDTH * TILE_HEIGHT) {
						throw new EndOfStreamException();
					}

					GBColor[,] pixels = new GBColor[TILE_WIDTH, TILE_HEIGHT];

					for (int y = 0; y < TILE_HEIGHT; y++) {
						for (int x = 0; x < TILE_WIDTH; x++) {
							pixels[x, y] = ByteToGBColor(data[x + (y * TILE_WIDTH)]);
						}
					}

					return new Tile(pixels);
				}
				set {
					var stream = file.stream;
					stream.Position = TILES_INDEX + (tile * TILE_WIDTH * TILE_HEIGHT);

					byte[] data = new byte[TILE_WIDTH * TILE_HEIGHT];

					if (value.Width != TILE_WIDTH) { throw new ArgumentException("Tile width MUST be " + TILE_WIDTH + ".", "value"); }
					if (value.Height != TILE_HEIGHT) { throw new ArgumentException("Tile height MUST be " + TILE_HEIGHT + ".", "value"); }

					for (int y = 0; y < TILE_HEIGHT; y++) {
						for (int x = 0; x < TILE_WIDTH; x++) {
							data[x + (y * TILE_WIDTH)] = GBColorToByte(value[x, y]);
						}
					}

					stream.Write(data, 0, TILE_WIDTH * TILE_HEIGHT);

					file.messenger.SendTileChangeMessage(tile);
				}
			}

			/// <summary>
			/// Creates a full tile array from the tiles represented by this.
			/// 
			/// If you only want one tile, use the indexer.
			/// </summary>
			public Tile[] GetTilesArray() {
				var stream = file.stream;
				stream.Position = TILES_INDEX;

				Tile[] returned = new Tile[file.TileCount];

				for (int tileNum = 0; tileNum < returned.Length; tileNum++) {
					byte[] data = new byte[TILE_WIDTH * TILE_HEIGHT];
					var read = stream.Read(data, 0, TILE_WIDTH * TILE_HEIGHT);
					if (read != TILE_WIDTH * TILE_HEIGHT) {
						throw new EndOfStreamException();
					}

					GBColor[,] pixels = new GBColor[TILE_WIDTH, TILE_HEIGHT];

					for (int y = 0; y < TILE_HEIGHT; y++) {
						for (int x = 0; x < TILE_WIDTH; x++) {
							pixels[x, y] = ByteToGBColor(data[x + (y * TILE_WIDTH)]);
						}
					}

					returned[tileNum] = new Tile(pixels);
				}

				return returned;
			}
		}

		/// <summary>
		/// Contains a list of all the tiles color mappings.
		/// </summary>
		public class MMFPalMapList
		{
			public struct PalMapEntry {
				public PalMapEntry(byte GBC, byte SGB) {
					this.GBC = GBC;
					this.SGB = SGB;
				}

				public byte GBC;
				public byte SGB;

				public override string ToString() {
					return "GBC: " + GBC + "; SGB: " + SGB;
				}
			}

			private readonly AUMemMappedFile file;
			internal MMFPalMapList(AUMemMappedFile file) {
				this.file = file;
			}

			public PalMapEntry this[UInt16 tile] {
				get {
					var stream = file.stream;
					stream.Position = PALMAPS_INDEX + (tile * 2);

					byte gbc = stream.ReadByteEx();
					byte sgb = stream.ReadByteEx();

					return new PalMapEntry(gbc, sgb);
				}
				set {
					var stream = file.stream;
					stream.Position = PALMAPS_INDEX + (tile * 2);

					stream.WriteByte(value.GBC);
					stream.WriteByte(value.SGB);

					file.messenger.SendTileChangeMessage(tile);
				}
			}
		}

		/// <summary>
		/// Contains the DMG color mapping.
		/// 
		/// Provides options for GBColors and bytes.
		/// </summary>
		public class MMFColorMapping
		{
			private readonly AUMemMappedFile file;
			internal MMFColorMapping(AUMemMappedFile file) {
				this.file = file;
			}

			public byte Color0 {
				get {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 0;

					return stream.ReadByteEx();
				}
				set {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 0;

					stream.WriteByteEx(value);

					file.messenger.SendColorMappingsMessage();
				}
			}

			public byte Color1 {
				get {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 1;

					return stream.ReadByteEx();
				}
				set {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 1;

					stream.WriteByteEx(value);

					file.messenger.SendColorMappingsMessage();
				}
			}

			public byte Color2 {
				get {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 2;

					return stream.ReadByteEx();
				}
				set {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 2;

					stream.WriteByteEx(value);

					file.messenger.SendColorMappingsMessage();
				}
			}

			public byte Color3 {
				get {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 3;

					return stream.ReadByteEx();
				}
				set {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + 3;

					stream.WriteByteEx(value);

					file.messenger.SendColorMappingsMessage();
				}
			}

			public GBColor GBColor0 {
				get { return ByteToGBColor(Color0); }
				set { Color0 = GBColorToByte(value); }
			}

			public GBColor GBColor1 {
				get { return ByteToGBColor(Color1); }
				set { Color1 = GBColorToByte(value); }
			}

			public GBColor GBColor2 {
				get { return ByteToGBColor(Color2); }
				set { Color2 = GBColorToByte(value); }
			}

			public GBColor GBColor3 {
				get { return ByteToGBColor(Color3); }
				set { Color3 = GBColorToByte(value); }
			}

			public byte this[byte b] {
				get {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + b;

					return stream.ReadByteEx();
				}
				set {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + b;

					stream.WriteByteEx(value);

					file.messenger.SendColorMappingsMessage();
				}
			}

			public GBColor this[GBColor color] {
				get {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + GBColorToByte(color);

					return ByteToGBColor(stream.ReadByteEx());
				}
				set {
					Stream stream = file.stream;
					stream.Position = GBPAL_INDEX + GBColorToByte(color);

					stream.WriteByteEx(GBColorToByte(value));

					file.messenger.SendColorMappingsMessage();
				}
			}
		}

		/// <summary>
		/// Color set.  TODO: Use the more official type - palette, ect.
		/// </summary>
		public class MMFGBColorSet
		{
			/// <summary>
			/// The size of a color.
			/// </summary>
			private const int SIZE_OF_COLOR = 4;
			private const int NUM_OF_ENTRIES = 4;

			private readonly AUMemMappedFile file;
			/// <summary>
			/// THe index used, because this class uses two different indecies.
			/// TODO: this seems like an ugly sollution.
			/// </summary>
			private readonly int USED_INDEX;

			internal MMFGBColorSet(AUMemMappedFile file, int usedIndex) {
				this.file = file;
				this.USED_INDEX = usedIndex;
			}

			public Color this[int row, int pal] {
				get {
					var stream = file.stream;
					stream.Position = USED_INDEX + (row * NUM_OF_ENTRIES * SIZE_OF_COLOR) + (pal * SIZE_OF_COLOR);
					
					return stream.ReadColor();
				}
				set {
					var stream = file.stream;
					stream.Position = USED_INDEX + (row * NUM_OF_ENTRIES * SIZE_OF_COLOR) + (pal * SIZE_OF_COLOR);

					stream.WriteColor(value);
				}
			}

			public Palette this[int row] {
				get {
					var stream = file.stream;
					stream.Position = USED_INDEX + (row * NUM_OF_ENTRIES * SIZE_OF_COLOR);
					
					Color[] colors = new Color[NUM_OF_ENTRIES];

					for (int i = 0; i < colors.Length; i++) {
						colors[i] = stream.ReadColor();
					}
					
					return new Palette(colors);
				}
				set {
					var stream = file.stream;
					stream.Position = USED_INDEX + (row * NUM_OF_ENTRIES * SIZE_OF_COLOR);

					for (int i = 0; i < NUM_OF_ENTRIES; i++) {
						stream.WriteColor(value[i]);
					}
					
					file.messenger.SendColorSetsMessage();
				}
			}
		}
		#endregion

		//All of the indecies of the different features of the file.
		private const int ID_INDEX = 0, ID_SIZE = 4;
		private const int TILES_INDEX = 4, TILES_SIZE = 8 * 8 * 768;
		private const int PALMAPS_INDEX = 49156, PALMAPS_SIZE = 768 * 2;
		private const int TILECOUNT_INDEX = 50692, TILECOUNT_SIZE = 4;
		private const int TILEWIDTH_INDEX = 50696, TILEWIDTH_SIZE = 4;
		private const int TILEHEIGHT_INDEX = 50700, TILEHEIGHT_SIZE = 4;
		private const int GBPAL_INDEX = 50704, GBPAL_SIZE = 4;
		private const int GBCCOLSET_INDEX = 50708, GBCCOLSET_SIZE = 8 * 4 * 4;
		private const int SGBCOLSET_INDEX = 50836, SGBCOLSET_SIZE = 8 * 4 * 4;
		//The size of the memory block, in total.
		private const int MEM_BLOCK_SIZE = 50964;

		private string fileName;
		private string mmfName;
		private AUMessenger messenger;
		private MemoryMappedFile file;
		private MemoryMappedViewStream stream;

		/// <summary>
		/// Creates a memory mapped file.
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="listener"></param>
		/// <param name="loadedFile">The data from a GBRFile to use if the file does not exist (or <paramref name="overwrite"/> is set to <c>true</c>).  If <c>null</c>, it will not be used (and an exception will be thrown if there is currently no MMF)</param>
		/// <param name="overwrite">Whether or not to overwrite existing data in the MMF.  If true, loadedFile must NOT be null.</param>
		public AUMemMappedFile(String fileName, AUMessenger listener, GBRFile.GBRFile loadedFile = null, bool overwrite = false) {
			this.fileName = fileName;
			
			//The hidden step.
			mmfName = fileName.ToUpperInvariant().Replace('\\', '@');

			try {
				this.file = MemoryMappedFile.OpenExisting(mmfName, MemoryMappedFileRights.ReadWrite);
			} catch (FileNotFoundException ex) {
				if (loadedFile == null) {
					throw new Exception(
						"MemmoryMappedFile " + mmfName + " does not exist and loadedFile was not supplied / is null; MMF cannot be created.", ex);
				}

				this.file = MemoryMappedFile.CreateNew(mmfName, MEM_BLOCK_SIZE, MemoryMappedFileAccess.ReadWrite);

				//Force loading from the file.
				overwrite = true;
			}

			if (overwrite && (loadedFile == null)) {
				throw new Exception("Cannot overwrite MemmoryMappedFile contents when loadedFile is null!");
			}

			this.stream = file.CreateViewStream();

			Tiles = new MMFTileList(this);
			PalMaps = new MMFPalMapList(this);

			GBPalettes = new MMFColorMapping(this);

			GBCPalettes = new MMFGBColorSet(this, GBCCOLSET_INDEX);
			SGBPalettes = new MMFGBColorSet(this, SGBCOLSET_INDEX);

			this.messenger = listener;

			if (overwrite) {
				messenger.SupressSendingMessages();

				ID = "TU01";

				var tiles = loadedFile.GetObjectsOfType<GBRObjectTileData>().First();
				var pals = loadedFile.GetObjectsOfType<GBRObjectPalettes>().First();
				var palMaps = loadedFile.GetObjectsOfType<GBRObjectTilePalette>().First();

				TileCount = tiles.Count;
				TileWidth = tiles.Width;
				TileHeight = tiles.Height;

				GBPalettes.GBColor0 = tiles.Color0Mapping;
				GBPalettes.GBColor1 = tiles.Color1Mapping;
				GBPalettes.GBColor2 = tiles.Color2Mapping;
				GBPalettes.GBColor3 = tiles.Color3Mapping;

				for (UInt16 i = 0; i < tiles.Count; i++) {
					Tiles[i] = tiles.tiles[i];
				}

				for (UInt16 i = 0; i < pals.GBCPalettes.Size; i++) {
					GBCPalettes[i] = pals.GBCPalettes[i];
				}
				for (UInt16 i = 0; i < pals.SGBPalettes.Size; i++) {
					SGBPalettes[i] = pals.SGBPalettes[i];
				}

				for (UInt16 i = 0; i < tiles.Count; i++) {
					PalMaps[i] = new MMFPalMapList.PalMapEntry((byte)palMaps.GBCPalettes[i], (byte)palMaps.SGBPalettes[i]);
				}

				messenger.ResumeSendingMessages();
			}
		}

		~AUMemMappedFile() {
			this.Dispose();
		}

		public void Dispose() {
			file.Dispose();
			stream.Dispose();
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

		/// <summary>
		/// The name the MMF was origionally created with.
		/// </summary>
		[Description("The name the MMF was origionally created with.")]
		public string FileName {
			get { return fileName; }
		}
		/// <summary>
		/// The name the MMF actually uses.
		/// </summary>
		[Description("The name the MMF actually uses.")]
		public string MMFName {
			get { return mmfName; }
		}

		/// <summary>
		/// The ID of the file.
		/// </summary>
		public string ID {
			get {
				stream.Position = ID_INDEX;
				return stream.ReadString(ID_SIZE);
			}
			set {
				stream.Position = ID_INDEX;
				stream.WriteString(value, ID_SIZE);
			}
		}

		public MMFTileList Tiles { get; private set; }
		public MMFPalMapList PalMaps { get; private set; }

		public UInt32 TileCount {
			get {
				stream.Position = TILECOUNT_INDEX;
				return stream.ReadInteger();
			}
			set {
				stream.Position = TILECOUNT_INDEX;
				stream.WriteInteger(value);

				messenger.SendTileListRefreshMessage();
			}
		}

		public UInt32 TileWidth {
			get {
				stream.Position = TILEWIDTH_INDEX;
				return stream.ReadInteger();
			}
			set {
				stream.Position = TILEWIDTH_INDEX;
				stream.WriteInteger(value);

				messenger.SendTileDimensionsMessage();
			}
		}

		public UInt32 TileHeight {
			get {
				stream.Position = TILEHEIGHT_INDEX;
				return stream.ReadInteger();
			}
			set {
				stream.Position = TILEHEIGHT_INDEX;
				stream.WriteInteger(value);

				messenger.SendTileDimensionsMessage();
			}
		}

		/// <summary>
		/// The DMG palettes.
		/// </summary>
		public MMFColorMapping GBPalettes { get; private set; }

		public MMFGBColorSet GBCPalettes { get; private set; }
		public MMFGBColorSet SGBPalettes { get; private set; }
	}
}
