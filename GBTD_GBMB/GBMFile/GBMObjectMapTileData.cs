using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapTileData
	{
		public class GBMObjectMapTileDataRecord
		{
			private UInt16 tileNumber;
			private byte gbcPalette;
			private bool unused1; //An unused value; saved for compatability but not used for anything else.
			private byte sgbPalette;
			private byte unused2; //An unused value; saved for compatability but not used for anything else.
			private bool flippedHorizontally;
			private bool flippedVertically;

			public UInt16 TileNumber {
				get { return tileNumber; }
				set {
					if (value > 767) { throw new ArgumentOutOfRangeException("value", value, "Must be between 0 and 767 (inclusive)."); }
					tileNumber = value;
				}
			}
			public byte GBCPalette {
				get { return gbcPalette; }
				set {
					if (value > 31) { throw new ArgumentOutOfRangeException("value", value, "Must be between 0 and 31 (inclusive)."); }
					gbcPalette = value;
				}
			}
			public byte SGBPalette {
				get { return sgbPalette; }
				set {
					if (value > 31) { throw new ArgumentOutOfRangeException("value", value, "Must be between 0 and 7 (inclusive)."); }
					sgbPalette = value;
				}
			}
			public bool FlippedHorizontally {
				get { return flippedHorizontally; }
				set { flippedHorizontally = value; }
			}
			public bool FlippedVertically {
				get { return flippedVertically; }
				set { flippedVertically = value; }
			}

			public void SaveToStream(Stream s) {
				byte b0 = s.ReadByteEx();
				byte b1 = s.ReadByteEx();
				byte b2 = s.ReadByteEx();

				long l = (b2 << 0) | (b1 << 8) | (b0 << 16);

				this.tileNumber = (UInt16)BitRange(l, 0, 10);
				this.GBCPalette = (byte)BitRange(l, 10, 5);
				this.unused1 = BitRange(l, 15, 1) != 0;
				this.sgbPalette = (byte)BitRange(l, 16, 3);
				this.unused2 = (byte)BitRange(l, 19, 3);
				this.flippedHorizontally = BitRange(l, 22, 1) != 0;
				this.flippedVertically = BitRange(l, 23, 1) != 0;
			}

			/// <summary>
			/// Gets the range of bits specified, shifted right at the start.
			/// TODO better doc for this method.
			/// </summary>
			private long BitRange(long l, ushort start, ushort length) {
				long mask = 0;
				for (ushort i = start; i < start + length; i++) {
					mask |= (1U << i);
				}

				return (l & mask) >> start;
			}

			public GBMObjectMapTileDataRecord(Stream s) {

			}
		}
		//TODO: Everything.
	}
}
