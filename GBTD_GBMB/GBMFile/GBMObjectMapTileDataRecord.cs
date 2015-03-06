using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// An individual record used by <see cref="GBMObjectMapTileData"/>.
	/// </summary>
	public class GBMObjectMapTileDataRecord
	{
		private UInt16 tileNumber;
		private byte? gbcPalette;
		private bool unused1; //An unused value; saved for compatability but not used for anything else.
		private byte? sgbPalette;
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
		/// <summary>
		/// The 0-indexed GBC palette number, or null if the default palette is to be used.
		/// </summary>
		public byte? GBCPalette {
			get { return gbcPalette; }
			set {
				if (value != null && value > 30) {
					throw new ArgumentOutOfRangeException("value", value, "Must be between 0 and 30 (inclusive).");
				}
				gbcPalette = value;
			}
		}
		/// <summary>
		/// The 0-indexed SGB palette number, or null if the default palette is to be used.
		/// </summary>
		public byte? SGBPalette {
			get { return sgbPalette; }
			set {
				if (value != null && value > 6) {
					throw new ArgumentOutOfRangeException("value", value, "Must be between 0 and 6 (inclusive).");
				}
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

		public GBMObjectMapTileDataRecord(Stream s) {
			byte b0 = s.ReadByteEx();
			byte b1 = s.ReadByteEx();
			byte b2 = s.ReadByteEx();

			ulong l = (ulong)((b2 << 0) | (b1 << 8) | (b0 << 16));

			byte gbcPalRead, sgbPalRead;

			this.tileNumber = (UInt16)GetBitRange(l, 0, 10);
			gbcPalRead = (byte)GetBitRange(l, 10, 5);
			this.unused1 = GetBitRange(l, 15, 1) != 0;
			sgbPalRead = (byte)GetBitRange(l, 16, 3);
			this.unused2 = (byte)GetBitRange(l, 19, 3);
			this.flippedHorizontally = GetBitRange(l, 22, 1) != 0;
			this.flippedVertically = GetBitRange(l, 23, 1) != 0;

			this.gbcPalette = (gbcPalRead == 0 ? null : (byte?)(gbcPalRead - 1));
			this.sgbPalette = (sgbPalRead == 0 ? null : (byte?)(sgbPalRead - 1));
		}

		public void SaveToStream(Stream s) {
			ulong l = 0U;

			byte gbcPalWritten = (byte)(gbcPalette != null ? gbcPalette.Value + 1 : 0);
			byte sgbPalWritten = (byte)(sgbPalette != null ? sgbPalette.Value + 1 : 0);

			SetBitRange(ref l, tileNumber, 0, 10);
			SetBitRange(ref l, gbcPalWritten, 10, 5);
			SetBitRange(ref l, unused1 ? 1U : 0U, 15, 1);
			SetBitRange(ref l, sgbPalWritten, 16, 3);
			SetBitRange(ref l, unused2, 19, 3);
			SetBitRange(ref l, flippedHorizontally ? 1U : 0U, 22, 1);
			SetBitRange(ref l, flippedVertically ? 1U : 0U, 23, 1);

			byte b2 = (byte)((l >> 0) & 0xFF);
			byte b1 = (byte)((l >> 8) & 0xFF);
			byte b0 = (byte)((l >> 16) & 0xFF);

			s.WriteByteEx(b0);
			s.WriteByteEx(b1);
			s.WriteByteEx(b2);
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

		public TreeNode ToTreeNode(string name) {
			TreeNode node = new TreeNode(name);

			node.Nodes.Add("TileNumber", "TimeNumber: " + this.TileNumber);
			node.Nodes.Add("GBCPalette", "GBCPalette: " + (this.GBCPalette != null ? this.GBCPalette.ToString() : "default"));
			node.Nodes.Add("Unused1", "Unused1: " + unused1);
			node.Nodes.Add("SGBPalette", "SGBPalette: " + (this.SGBPalette != null ? this.GBCPalette.ToString() : "default"));
			node.Nodes.Add("Unused1", "Unused1: " + unused1);
			node.Nodes.Add("FlippedHorizontally", "FlippedHorizontally: " + this.FlippedHorizontally);
			node.Nodes.Add("FlippedVertically", "FlippedVertically: " + this.FlippedVertically);

			return node;
		}
	}
}
