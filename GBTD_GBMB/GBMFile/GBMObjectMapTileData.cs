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

				ulong l = (ulong)((b2 << 0) | (b1 << 8) | (b0 << 16));

				this.tileNumber = (UInt16)GetBitRange(l, 0, 10);
				this.GBCPalette = (byte)GetBitRange(l, 10, 5);
				this.unused1 = GetBitRange(l, 15, 1) != 0;
				this.sgbPalette = (byte)GetBitRange(l, 16, 3);
				this.unused2 = (byte)GetBitRange(l, 19, 3);
				this.flippedHorizontally = GetBitRange(l, 22, 1) != 0;
				this.flippedVertically = GetBitRange(l, 23, 1) != 0;
			}

			/// <summary>
			/// Gets the sequence of bits specified from the given long.
			/// It starts at start and reads length bits.  
			/// <para>The value is shifted so that the bit at start is the first bit of the returned value.</para>
			/// </summary>
			private ulong GetBitRange(ulong l, ushort start, ushort length) {
				ulong mask = 0;
				for (ushort i = start; i < start + length; i++) {
					mask |= (1U << i);
				}

				return (l & mask) >> start;
			}

			/// <summary>
			/// Sets a sequence of bits specified in the given long.
			/// </summary>
			/// <param name="l">THe long to modify</param>
			/// <param name="bits">The value to write.</param>
			/// <param name="start">The first value in the given long to write (bits always starts at 0)</param>
			/// <param name="length">The length to write from bits.</param>
			private void SetBitRange(ref ulong l, ulong bits, ushort start, ushort length) {
				ulong mask = 0;
				for (ushort i = 0; i < length; i++) {
					mask |= (1U << i);
				}

				l |= (bits & mask) << start;
			}

			public GBMObjectMapTileDataRecord(Stream s) {
				
			}
		}
		//TODO: Everything.
	}
}
