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
	/// </summary>
	public class AUMemMappedFile : IDisposable
	{
		#region Inner classes (for named indexers)
		/// <summary>
		/// List of tiles.
		/// </summary>
		public class MMFTileList
		{
			private readonly AUMemMappedFile file;

			private int TileWidth = 8;
			private int TileHeight = 8;

			internal MMFTileList(AUMemMappedFile file) {
				this.file = file;
			}

			public Tile this[UInt16 tile] {
				get {
					if (file.TileWidth != this.TileWidth || file.TileHeight != this.TileHeight) {
						this.TileWidth = (int)file.TileWidth;
						this.TileHeight = (int)file.TileHeight;
					}

					var stream = file.stream;

					stream.Position = TILES_INDEX + (tile * TileWidth * TileHeight);

					byte[] data = new byte[TileWidth * TileHeight];
					var read = stream.Read(data, 0, TileWidth * TileHeight);
					if (read != TileWidth * TileHeight) {
						throw new EndOfStreamException();
					}

					GBColor[,] pixels = new GBColor[TileWidth, TileHeight];

					for (int y = 0; y < TileHeight; y++) {
						for (int x = 0; x < TileWidth; x++) {
							pixels[x, y] = ByteToGBColor(data[x + (y * TileWidth)]);
						}
					}

					return new Tile(pixels);
				}
				set {
					if (file.TileWidth != this.TileWidth || file.TileHeight != this.TileHeight) {
						this.TileWidth = (int)file.TileWidth;
						this.TileHeight = (int)file.TileHeight;
					}

					var stream = file.stream;
					stream.Position = TILES_INDEX + (tile * TileWidth * TileHeight);

					byte[] data = new byte[TileWidth * TileHeight];

					if (value.Width != TileWidth) { throw new ArgumentException("Tile width MUST be " + TileWidth + ".", "value"); }
					if (value.Height != TileHeight) { throw new ArgumentException("Tile height MUST be " + TileHeight + ".", "value"); }

					for (int y = 0; y < TileHeight; y++) {
						for (int x = 0; x < TileWidth; x++) {
							data[x + (y * TileWidth)] = GBColorToByte(value[x, y]);
						}
					}

					stream.Write(data, 0, TileWidth * TileHeight);

					file.messenger.SendTileChangeMessage(tile);
				}
			}

			/// <summary>
			/// Creates a full tile array from the tiles represented by this.
			/// 
			/// If you only want one tile, use the indexer.
			/// </summary>
			public Tile[] GetTilesArray() {
				if (file.TileWidth != this.TileWidth || file.TileHeight != this.TileHeight) {
					this.TileWidth = (int)file.TileWidth;
					this.TileHeight = (int)file.TileHeight;
				}

				var stream = file.stream;

				Tile[] returned = new Tile[file.TileCount];

				stream.Position = TILES_INDEX;

				for (int tileNum = 0; tileNum < returned.Length; tileNum++) {
					byte[] data = new byte[TileWidth * TileHeight];
					var read = stream.Read(data, 0, TileWidth * TileHeight);
					if (read != TileWidth * TileHeight) {
						throw new EndOfStreamException();
					}

					GBColor[,] pixels = new GBColor[TileWidth, TileHeight];

					for (int y = 0; y < TileHeight; y++) {
						for (int x = 0; x < TileWidth; x++) {
							pixels[x, y] = ByteToGBColor(data[x + (y * TileWidth)]);
						}
					}

					returned[tileNum] = new Tile(pixels);
				}

				return returned;
			}

			/// <summary>
			/// Set all of the tiles to the new size.
			/// 
			/// Only call this when the app is resizing the tiles; if it was done by another app this is unneeded.
			/// </summary>
			internal void ResizeTiles(UInt32 oldTileWidth, UInt32 oldTileHeight, UInt32 newTileWidth, UInt32 newTileHeight) {
				this.TileWidth = (int)newTileWidth;
				this.TileHeight = (int)newTileHeight;

				if (oldTileHeight == newTileHeight && oldTileWidth == newTileWidth) {
					return;
				}

				var stream = file.stream;

				uint oldTileCount = TILES_SIZE / (oldTileWidth * oldTileHeight);
				byte[, ,] oldTiles = new byte[oldTileCount, oldTileWidth, oldTileHeight];

				uint newTileCount = TILES_SIZE / (newTileWidth * newTileHeight);
				byte[, ,] newTiles = new byte[newTileCount, newTileWidth, newTileHeight];

				//Build the old tile list.
				stream.Position = TILES_INDEX;

				for (int tile = 0; tile < oldTileCount; tile++) { 
					for (int y = 0; y < oldTileHeight; y++) {
						for (int x = 0; x < oldTileWidth; x++) {
							oldTiles[tile, x, y] = stream.ReadByteEx();
						}
					}
				}

				//Build a list of 8x8 tiles.
				const int SIZED_TILE_WIDTH = 8;
				const int SIZED_TILE_HEIGHT = 8;
				const int SIZED_TILE_COUNT = TILES_SIZE / (SIZED_TILE_WIDTH * SIZED_TILE_HEIGHT);
				byte[, ,] sizedTiles = new byte[SIZED_TILE_COUNT, SIZED_TILE_WIDTH, SIZED_TILE_HEIGHT];

				int horizOldTileCount = (int)(oldTileWidth / SIZED_TILE_WIDTH);
				int vertOldTileCount = (int)(oldTileHeight / SIZED_TILE_HEIGHT);

				for (int tile = 0; tile < oldTileCount; tile++) {
					for (int y = 0; y < oldTileHeight; y++) {
						for (int x = 0; x < oldTileWidth; x++) {
							int sizedTileNum = ((x / SIZED_TILE_WIDTH) +
								((y / SIZED_TILE_HEIGHT) * horizOldTileCount) + (tile * vertOldTileCount * horizOldTileCount));

							if (sizedTileNum < SIZED_TILE_COUNT) {
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

							if (sizedTileNum < SIZED_TILE_COUNT) {
								newTiles[tile, x, y] = sizedTiles[sizedTileNum, x % SIZED_TILE_WIDTH, y % SIZED_TILE_HEIGHT];
							}
						}
					}
				}

				//Save the new tile list.
				stream.Position = TILES_INDEX;

				for (int tile = 0; tile < newTileCount; tile++) {
					for (int y = 0; y < newTileHeight; y++) {
						for (int x = 0; x < newTileWidth; x++) {
							stream.WriteByteEx(newTiles[tile, x, y]);
						}
					}
				}
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

			/// <summary>
			/// List of the single palettes.
			/// </summary>
			public class MMFSinglePalMapList
			{
				private readonly AUMemMappedFile file;
				private readonly int offset;

				/// <param name="file">The mmf associated with this object.</param>
				/// <param name="offset">The offset for the group of palettes.</param>
				internal MMFSinglePalMapList(AUMemMappedFile file, int offset) {
					this.file = file;
					this.offset = offset;
				}

				public byte this[UInt16 tile] {
					get {
						var stream = file.stream;
						stream.Position = PALMAPS_INDEX + (tile * 2);

						return stream.ReadByteEx();
					}
					set {
						var stream = file.stream;
						stream.Position = PALMAPS_INDEX + (tile * 2);

						stream.WriteByteEx(value);

						file.messenger.SendTileChangeMessage(tile);
					}
				}
			}

			private readonly AUMemMappedFile file;
			internal MMFPalMapList(AUMemMappedFile file) {
				this.file = file;

				this.GBC = new MMFSinglePalMapList(file, 0);
				this.SGB = new MMFSinglePalMapList(file, 1);
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

			public MMFSinglePalMapList GBC { get; private set; }
			public MMFSinglePalMapList SGB { get; private set; }
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

			public void SetPalettes(byte color0, byte color1, byte color2, byte color3) {
				Stream stream = file.stream;
				stream.Position = GBPAL_INDEX;

				byte[] oldValues = stream.ReadBytesEx(4);

				if (color0 == oldValues[0] &&
					color1 == oldValues[1] &&
					color2 == oldValues[2] &&
					color3 == oldValues[3]) {

					return;
				}

				stream.Position = GBPAL_INDEX;

				stream.WriteByteEx(color0);
				stream.WriteByteEx(color1);
				stream.WriteByteEx(color2);
				stream.WriteByteEx(color3);

				file.messenger.SendColorMappingsMessage();
			}

			public void SetPalettes(GBColor color0, GBColor color1, GBColor color2, GBColor color3) {
				this.SetPalettes(GBColorToByte(color0), GBColorToByte(color1), GBColorToByte(color2), GBColorToByte(color3));
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

				var tiles = loadedFile.GetObjectOfType<GBRObjectTileData>();
				var pals = loadedFile.GetObjectOfType<GBRObjectPalettes>();
				var palMaps = loadedFile.GetObjectOfType<GBRObjectTilePalette>();

				TileWidth = tiles.Width;
				TileHeight = tiles.Height;
				TileCount = tiles.Count;

				GBPalettes.GBColor0 = tiles.Color0Mapping;
				GBPalettes.GBColor1 = tiles.Color1Mapping;
				GBPalettes.GBColor2 = tiles.Color2Mapping;
				GBPalettes.GBColor3 = tiles.Color3Mapping;

				for (UInt16 i = 0; i < tiles.Count; i++) {
					Tiles[i] = tiles.Tiles[i];
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
				UInt32 oldTileWidth = this.TileWidth;
				UInt32 oldTileCount = this.TileCount;

				if (oldTileWidth != 0 && TileHeight != 0) {
					Tiles.ResizeTiles(oldTileWidth, TileHeight, value, TileHeight);
				}

				UInt32 newTileCount = (UInt32)(oldTileCount * ((oldTileWidth) / (float)(value)));

				stream.Position = TILEWIDTH_INDEX;
				stream.WriteInteger(value);

				stream.Position = TILECOUNT_INDEX;
				stream.WriteInteger(newTileCount);

				messenger.SendTileDimensionsMessage();
			}
		}

		public UInt32 TileHeight {
			get {
				stream.Position = TILEHEIGHT_INDEX;
				return stream.ReadInteger();
			}
			set {
				UInt32 oldTileHeight = this.TileHeight;
				UInt32 oldTileCount = this.TileCount;

				if (oldTileHeight != 0 && TileWidth != 0) {
					Tiles.ResizeTiles(TileWidth, oldTileHeight, TileWidth, value);
				}

				UInt32 newTileCount = (UInt32)(oldTileCount * ((oldTileHeight) / (float)(value)));

				stream.Position = TILEHEIGHT_INDEX;
				stream.WriteInteger(value);

				stream.Position = TILECOUNT_INDEX;
				stream.WriteInteger(newTileCount);

				messenger.SendTileDimensionsMessage();
			}
		}
		
		/// <summary>
		/// Sets both the <see cref="TileWidth"/> and <see cref="TileHeight"/> at the same time.
		/// </summary>
		public void SetTileSize(UInt32 newTileWidth, UInt32 newTileHeight) {
			UInt32 oldTileWidth = this.TileWidth;
			UInt32 oldTileHeight = this.TileHeight;
			UInt32 oldTileCount = this.TileCount;

			if (oldTileWidth != 0 && oldTileHeight != 0) {
				Tiles.ResizeTiles(oldTileWidth, oldTileHeight, newTileWidth, newTileHeight);
			}

			UInt32 newTileCount = (UInt32)(oldTileCount * ((oldTileWidth * oldTileHeight) / (float)(newTileWidth * newTileHeight)));

			stream.Position = TILECOUNT_INDEX;
			stream.WriteInteger(newTileCount);

			stream.Position = TILEWIDTH_INDEX;
			stream.WriteInteger(newTileWidth);

			stream.Position = TILEHEIGHT_INDEX;
			stream.WriteInteger(newTileHeight);

			messenger.SendTileListRefreshMessage(); //Sent to make sure that GBTD doesn't crash -- the previous tile number might be invalid now.

			messenger.SendTileDimensionsMessage();
		}

		/// <summary>
		/// The DMG palettes.
		/// </summary>
		public MMFColorMapping GBPalettes { get; private set; }

		public MMFGBColorSet GBCPalettes { get; private set; }
		public MMFGBColorSet SGBPalettes { get; private set; }
	}
}
