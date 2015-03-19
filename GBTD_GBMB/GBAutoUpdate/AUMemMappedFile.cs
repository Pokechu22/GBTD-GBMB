using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.ComponentModel;
using System.Drawing;

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
		/// List of tiles.  TODO: Use tile instead of byte array.
		/// </summary>
		public class MMFTileList
		{
			private readonly AUMemMappedFile file;
			internal MMFTileList(AUMemMappedFile file) {
				this.file = file;
			}

			public byte[,] this[UInt16 tile] {
				get {
					var stream = file.stream;
					stream.Position = TILES_INDEX + (tile * 8 * 8);

					byte[] data = new byte[8 * 8];
					var read = stream.Read(data, 0, 8 * 8);
					if (read != 8 * 8) {
						throw new EndOfStreamException();
					}

					byte[,] result = new byte[8, 8];

					for (int y = 0; y < 8; y++) {
						for (int x = 0; x < 8; x++) {
							result[x, y] = data[x + (y * 8) + (tile * 8 * 8)];
						}
					}

					return result;
				}
				set {
					var stream = file.stream;
					stream.Position = TILES_INDEX + (tile * 8 * 8);

					byte[] data = new byte[8 * 8];

					if (value.GetLength(0) != 8) { throw new ArgumentOutOfRangeException("value", "0th length is invalid (must be 8)"); }
					if (value.GetLength(1) != 8) { throw new ArgumentOutOfRangeException("value", "1th length is invalid (must be 8)"); }

					for (int y = 0; y < 8; y++) {
						for (int x = 0; x < 8; x++) {
							data[x + (y * 8)] = value[x, y];
						}
					}

					stream.Write(data, 0, 8 * 8);

					file.listener.SendTileChangeMessage(tile);
				}
			}
		}

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

			public Color[] this[int row] {
				get {
					var stream = file.stream;
					stream.Position = USED_INDEX + (row * NUM_OF_ENTRIES * SIZE_OF_COLOR);

					Color[] colors = new Color[NUM_OF_ENTRIES];

					for (int i = 0; i < colors.Length; i++) {
						colors[i] = stream.ReadColor();
					}

					return colors;
				}
				set {
					var stream = file.stream;
					stream.Position = USED_INDEX + (row * NUM_OF_ENTRIES * SIZE_OF_COLOR);

					if (value.Length != NUM_OF_ENTRIES) {
						throw new ArgumentException("Number of provided entries is incorrect - should be exactly " + NUM_OF_ENTRIES + ".", "value");
					}

					for (int i = 0; i < value.Length; i++) {
						stream.WriteColor(value[i]);
					}
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
		private const int GBPAL_INDEX = 50704, GBPAL_SIZE = 50704;
		private const int GBCCOLSET_INDEX = 50708, GBCCOLSET_SIZE = 8 * 4 * 4;
		private const int SGBCOLSET_INDEX = 50836, SGBCOLSET_SIZE = 8 * 4 * 4;
		//The size of the memory block, in total.
		private const int MEM_BLOCK_SIZE = 50964;

		private string fileName;
		private AUMessenger listener;
		private MemoryMappedFile file;
		private MemoryMappedViewStream stream;

		public AUMemMappedFile(String fileName, AUMessenger listener) {
			this.fileName = fileName;
			
			//The hidden step.
			fileName = fileName.ToUpperInvariant().Replace('\\', '@');

			try {
				this.file = MemoryMappedFile.OpenExisting(fileName, MemoryMappedFileRights.ReadWrite);
			} catch (FileNotFoundException) {
				//TODO initialize the defaults.
				//this.file = MemoryMappedFile.CreateNew(fileName, MEM_BLOCK_SIZE, MemoryMappedFileAccess.ReadWrite);
				//TODO send the message for everything to update.

				throw; //TODO We don't want to set the default right now.
			}

			this.stream = file.CreateViewStream();

			Tiles = new MMFTileList(this);
			PalMaps = new MMFPalMapList(this);

			this.listener = listener;
		}

		~AUMemMappedFile() {
			this.Dispose();
		}

		public void Dispose() {
			file.Dispose();
			stream.Dispose();
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
			}
		}

		/// <summary>
		/// The DMG palettes.  
		/// TODO make this a struct, and use GBColor.
		/// </summary>
		public byte[] GBPalettes {
			get {
				stream.Position = GBPAL_INDEX;

				byte[] bytes = new byte[4];
				int read = stream.Read(bytes, 0, 4);

				if (read != 4) {
					throw new EndOfStreamException();
				}

				return bytes;
			}
			set {
				stream.Position = GBPAL_INDEX;

				if (value.Length != 4) {
					throw new ArgumentException("Value must be of length 4", "value");
				}

				stream.Write(value, 0, 4);
			}
		}
		
		//TODO: GBCPalettes and SGBPalettes.
	}
}
