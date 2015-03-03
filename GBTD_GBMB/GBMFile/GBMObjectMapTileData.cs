using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

			//TODO: Saving, loading, constructors.
		}
		//TODO: Everything.
	}
}
