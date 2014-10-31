using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace GB.Shared.Tile
{
	/// <summary>
	/// Handles GB compression.  Based off of GBCompress.pas.
	/// </summary>
	static class GBCompression
	{
		/// <summary>
		/// The byte used to mark the end of a GB-Compresed file.
		/// </summary>
		protected internal const byte EOFMarker = 0;

		/// <summary>
		/// Writes the EOF marker to the stream.
		/// </summary>
		/// <param name="stream"></param>
		protected static void write_end(Stream stream) {
			stream.WriteByte(EOFMarker);
		}

		/// <summary>
		/// Writes a byte to the stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="len"></param>
		/// <param name="data"></param>
		protected static void write_byte(Stream stream, byte len, byte data) {
			len = (byte)((len - 1) & 63); //63 = 0x3F = 0b00111111

			stream.WriteByte(len);
			stream.WriteByte(data);
		}

		/// <summary>
		/// Writes a word (ushort) to the stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="len"></param>
		/// <param name="data"></param>
		protected static void write_word(Stream stream, byte len, ushort data) {
			len = (byte)(((len-1) & 63) | 64);

			stream.WriteByte(len);
			stream.WriteByte((byte)((data >> 8) & 0xFF));
			stream.WriteByte((byte)(data & 0xFF));
		}

		/// <summary>
		/// Writes a ushort to the output.  I don't know where it got "string" from, but it is a uint16.  Different endianity, though.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="len"></param>
		/// <param name="data"></param>
		protected static void write_string(Stream stream, byte len, ushort data) {
			len = (byte)(((len - 1) & 63) | 64);

			stream.WriteByte(len);
			stream.WriteByte((byte)(data & 0xFF));
			stream.WriteByte((byte)((data >> 8) & 0xFF));
		}

		/// <summary>
		/// Writes data?
		/// I get this one's name less than the previous...
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="len"></param>
		/// <param name="data"></param>
		/// <param name="offset"></param>
		protected static void write_trash(Stream stream, byte len, byte[] data, int offset) {
			len = (byte)(((len - 1) & 63) | 192);

			//This will probably have issues if len is different from the real byte array length, but it is the algoritm.
			for (int i = 0; i < len; i++) {
				stream.WriteByte(data[i + offset]);
			}
		}

		/// <summary>
		/// Returns true if the end of input has been reached.
		/// Does not advance position.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		protected static Boolean reached_end(Stream input) {
			int read = input.ReadByte();
			input.Position--; //reset position in streem.

			return (read == EOFMarker);
		}

		/// <summary>
		/// GB-Compresses data.
		/// This origionally returned an integer, but that was the size of the stream, and thus is not needed.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="output"></param>
		public static void GBCompressBuf(byte[] input, Stream output) {
			int bp, tb;
			byte x;
			UInt16 y;
			int rr, sr, rl;
			int r_rb, r_rw, r_rs;

			bp = 0;
			tb = 0;

			while (bp < input.Length) {
				
				x = input[bp];
				r_rb = 1;
				while ((input[bp + r_rb] == x) & ((bp+r_rb) < input.Length) & (r_rb < 64)) {
					r_rb ++;
				}

				y = (UInt16)((input[bp] << 8) + input[bp + 1]);
				r_rw = 1;
				while (((UInt16)((input[bp + (r_rw * 2)] << 8) + input[bp + 1 + (r_rw * 2)]) == y) & ((bp + (r_rw*2)) < input.Length) & (r_rw < 64)) {
					r_rw++;
				}

				rr = 0;
				sr = 0;

				r_rs = 0;

				while (rr < bp) {
					rl = 0;

					while (((bp + rl) < input.Length) & (input[rr + rl] == input[bp + rl]) & ((rr + rl) < bp) & (rl < 64)) {
						rl++;
					}

					if (rl > r_rs) {
						sr = rr - bp;
						r_rs = rl;
					}

					rr++;
				}

				if ((r_rb > 2) & (r_rb > r_rw) & (r_rb > r_rs)) {
					if (tb > 0) {
						write_trash(output, (byte)tb, input, bp-tb);
						tb = 0;
					}

					write_byte(output, (byte)r_rb, x);
					bp += r_rb;
				} else {
					if ((r_rw > 8) & ((r_rw * 2) > r_rs)) {
						if (tb > 0) {
							write_trash(output, (byte)tb, input, bp - tb);
							tb = 0;
						}

						write_word(output, (byte)r_rw, y);
						bp += r_rw * 2;
					} else {
						if (r_rs > 3) {
							if (tb > 0) {
								write_trash(output, (byte)tb, input, bp - tb);
								tb = 0;
							}

							write_string(output, (byte)r_rs, (ushort)sr);
							bp += r_rs;
						} else {
							if (tb > 64) {
								write_trash(output, (byte)tb, input, bp - tb);
								tb = 0;
							} else {
								tb++;
								bp++;
							}
						}
					}
				}
			}

			if (tb > 0) {
				write_trash(output, (byte)tb, input, bp - tb);
			}

			write_end(output);
		}

		public static void write_data(byte[] input, Stream output) {
			using (Stream stream = new MemoryStream(input)) {
				write_data(stream, output);
			}
		}

		public static void write_data(Stream input, Stream output) {
			while (!reached_end(input)) {
				
			}
		}
	}
}
