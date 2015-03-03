using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// Provides a general set of functions that handle writing things to streams in compliance with the GBM format definitions.
	/// 
	/// <para>For reference: </para>
	/// 
	/// <para>Word -> 16-bit hi-endian.</para>
	/// <para>Unsigned long -> 32-bit hi-endian.</para>
	/// <para>Integer -> 32-bit hi-endian, unsigned.</para>
	/// <para>String (xx) -> C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).
	/// This is padded with 0s if the end has not been reached..</para>
	/// </summary>
	internal static class StreamExtensions
	{
		#region Byte-handling methods
		/// <summary>
		/// Reads a single byte from the stream, throwing an exception if at the end.
		/// </summary>
		/// <param name="stream">The stream to read from</param>
		/// <exception cref="EndOfStreamException">When at the end of the stream</exception>
		/// <exception cref="NotSupportedException">When the stream does not support reading.</exception>
		internal static byte ReadByteEx(this Stream stream) {
			if (!stream.CanRead) { throw new NotSupportedException("Stream cannot be read!"); }

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) { throw new EndOfStreamException(); }

			return bytes[0];
		}

		/// <summary>
		/// Reads a single byte from the stream, returning default if at the end.
		/// </summary>
		/// <param name="stream">The stream to read from</param>
		/// <param name="def">The default value to return.</param>
		/// <exception cref="NotSupportedException">When the stream does not support reading.</exception>
		internal static byte ReadByteEx(this Stream stream, byte def) {
			if (!stream.CanRead) { throw new NotSupportedException("Stream cannot be read!"); }

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) { return def; }

			return bytes[0];
		}

		/// <summary>
		/// Writes a single byte to the stream.
		/// <para>Yes, this is somewhat redundant, but it seems like a good idea to include it for consistency.</para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="value">The byte to write.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be written to.</exception>
		internal static void WriteByteEx(this Stream stream, byte value) {
			if (!stream.CanWrite) { throw new NotSupportedException("Stream cannot be written to!"); }

			stream.WriteByte(value);
		}
		#endregion

		#region Word-handling methods
		/// <summary>
		/// Reads a 16-bit (2 byte) unsigned number, hi-endian, from the specified stream, throwing an exception if at the end.
		/// <param name="stream">The stream to read from.</param>
		/// </summary>
		/// <exception cref="NotSupportedException">When the stream cannot be read from.</exception>
		/// <exception cref="EndOfStreamException">When the end of the stream has been reached.</exception>
		internal static UInt16 ReadWord(this Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[2];
			int read = stream.Read(bytes, 0, 2);

			if (read != 2) {
				throw new EndOfStreamException();
			}

			return (UInt16)((bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Reads a 16-bit (2 byte) unsigned number, hi-endian, from the specified stream, returning the default value if at the end.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be read from.</exception>
		internal static UInt16 ReadWord(this Stream stream, UInt16 def) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[2];
			int read = stream.Read(bytes, 0, 2);

			if (read != 2) {
				return def;
			}

			return (UInt16)((bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Writes a 16-bit (2 byte) unsigned number, hi-endian, to the specified stream.
		/// <param name="stream">The stream to write to.</param>
		/// <param name="n">The number to write.</param>
		/// </summary>
		/// <exception cref="NotSupportedException">When the stream cannot be written to.</exception>
		internal static void WriteWord(this Stream stream, UInt16 n) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteByte((byte)((n >> 0) & 0xFF));
			stream.WriteByte((byte)((n >> 8) & 0xFF));
		}
		#endregion

		#region Unsigned Long-handling methods
		/// <summary>
		/// Reads a 32-bit (4 byte) unsigned number, hi-endian, from the specified stream, throwing an exception if at the end of the stream.
		/// <para>Despite the fact that this method name says that it uses ULongs, the actual type is UInt32.  Just the way the spec works.</para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be read.</exception>
		/// <exception cref="EndOfStreamException">When the end of the stream has been reached.</exception>
		internal static UInt32 ReadUnsignedLong(this Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[4];
			int read = stream.Read(bytes, 0, 4);

			if (read != 4) {
				throw new EndOfStreamException();
			}

			return (UInt32)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Reads a 32-bit (4 byte) unsigned number, hi-endian, from the specified stream, returning the default if at end of stream.
		/// <para>Despite the fact that this method name says that it uses ULongs, the actual type is UInt32.  Just the way the spec works.</para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be read.</exception>
		internal static UInt32 ReadUnsignedLong(this Stream stream, UInt32 def) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[4];
			int read = stream.Read(bytes, 0, 4);

			if (read != 4) {
				return def;
			}

			return (UInt32)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Writes a 32-bit (4 byte) unsigned number, hi-endian, to the specified stream.
		/// <para>Despite the fact that this method name says that it uses ULongs, the actual type is UInt32.  Just the way the spec works.</para>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="n">The number to write.</param>
		/// </summary>
		/// <exception cref="NotSupportedException">When the stream cannot be written to.</exception>
		internal static void WriteUnsignedLong(this Stream stream, UInt32 n) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteByte((byte)((n >> 0) & 0xFF));
			stream.WriteByte((byte)((n >> 8) & 0xFF));
			stream.WriteByte((byte)((n >> 16) & 0xFF));
			stream.WriteByte((byte)((n >> 24) & 0xFF));
		}
		#endregion

		#region Integer-handling methods
		/// <summary>
		/// Reads a 32-bit (4 byte) unsigned number, hi-endian, from the specified stream, throwing an exception if at the end of the stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be read.</exception>
		/// <exception cref="EndOfStreamException">When the end of the stream has been reached.</exception>
		internal static UInt32 ReadInteger(this Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[4];
			int read = stream.Read(bytes, 0, 4);

			if (read != 4) {
				throw new EndOfStreamException();
			}

			return (UInt32)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Reads a 32-bit (4 byte) unsigned number, hi-endian, from the specified stream, returning the default if at end of stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be read.</exception>
		internal static UInt32 ReadInteger(this Stream stream, UInt32 def) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[4];
			int read = stream.Read(bytes, 0, 4);

			if (read != 4) {
				return def;
			}

			return (UInt32)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Writes a 32-bit (4 byte) unsigned number, hi-endian, to the specified stream.
		/// <param name="stream">The stream to write to.</param>
		/// <param name="n">The number to write.</param>
		/// </summary>
		/// <exception cref="NotSupportedException">When the stream cannot be written to.</exception>
		internal static void WriteInteger(this Stream stream, UInt32 n) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteByte((byte)((n >> 0) & 0xFF));
			stream.WriteByte((byte)((n >> 8) & 0xFF));
			stream.WriteByte((byte)((n >> 16) & 0xFF));
			stream.WriteByte((byte)((n >> 24) & 0xFF));
		}
		#endregion

		#region String-handling methods
		/// <summary>
		/// Reads a string to the specified stream, in the expected format, throwing an exception if the end of the stream is reached.
		/// <para>Per the doc: 
		/// String (xx)	C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).</para>
		/// <para>This method will read until a null terminator or the end of the length, IE it will not return any <c>\0</c>'s in the string.</para>
		/// <para>This method also uses ANSI for encoding.</para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="length">The maximum expected length.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be read from.</exception>
		/// <exception cref="EndOfStreamException">When the end of the stream has been reached.</exception>
		/// <exception cref="ArgumentOutOfRangeException">When length is negative.</exception>
		internal static String ReadString(this Stream stream, int length) {
			//A length of 0 is legal (though pointless).
			if (length < 0) {
				throw new ArgumentOutOfRangeException("length", length, "Length must not be negative!");
			}
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[length];
			int read = stream.Read(bytes, 0, length);

			if (read != length) {
				throw new EndOfStreamException();
			}

			return Encoding.ASCII.GetString(bytes).TrimEnd('\0');
		}

		/// <summary>
		/// Reads a string to the specified stream, in the expected format, returning a default value if the stream has ended.
		/// <para>Per the doc: 
		/// String (xx)	C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).</para>
		/// <para>This method will read until a null terminator or the end of the length, IE it will not return any <c>\0</c>'s in the string.</para>
		/// <para>This method also uses ANSI for encoding.</para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="length">The maximum expected length.</param>
		/// <param name="def">The default value to return.  Can be <c>null</c>.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be read from.</exception>
		/// <exception cref="ArgumentOutOfRangeException">When length is negative.</exception>
		internal static String ReadString(this Stream stream, int length, string def) {
			//A length of 0 is legal (though pointless).
			if (length < 0) {
				throw new ArgumentOutOfRangeException("length", length, "Length must not be negative!");
			}
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[length];
			int read = stream.Read(bytes, 0, length);

			if (read != length) {
				return def;
			}

			return Encoding.ASCII.GetString(bytes).TrimEnd('\0');
		}
		/// <summary>
		/// Writes a string to the specified stream, in the expected format.
		/// Reads a string to the specified stream, in the expected format, returning a default value if the stream has ended.
		/// <para>Per the doc: 
		/// String (xx)	C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).</para>
		/// <para>This method truncates the string if necessary.</para>
		/// <para>This method also uses ANSI for encoding.</para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="s">The string to write.</param>
		/// <param name="length">The maximum allowed length.  Will be padded up to if needed.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be written to.</exception>
		/// <exception cref="ArgumentOutOfRangeException">When length is negative.</exception>
		/// <exception cref="ArgumentNullException">When s is null.</exception>
		internal static void WriteString(this Stream stream, String s, uint length) {
			if (s == null) {
				throw new ArgumentNullException("s");
			}
			if (length < 0) {
				throw new ArgumentOutOfRangeException("length", length, "Length must not be negative!");
			}
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}
			byte[] bytes = Encoding.ASCII.GetBytes(s);

			//Go until the end -1, so that if a terminator is needed it shall be added.
			for (int i = 0; i < length - 1; i++) {
				if (i < bytes.Length) {
					stream.WriteByte(bytes[i]);
				} else {
					stream.WriteByte(0x00);
				}
			}
			//Force a terminator.
			stream.WriteByte(0x00);
		}
		#endregion

		#region GBMObjectHeader-handling methods
#pragma warning disable 618 //Disables obsolete warnings - http://stackoverflow.com/q/968293/3991344

		/// <summary>
		/// Reads a <see cref="GBMObjectHeader"/> from the stream, throwing an exception if at the end.
		/// </summary>
		/// <param name="stream">The stream to read from</param>
		/// <exception cref="EndOfStreamException">When at the end of the stream</exception>
		/// <exception cref="NotSupportedException">When the stream does not support reading.</exception>
		internal static GBMObjectHeader ReadGBMObjectHeader(this Stream stream) {
			if (!stream.CanRead) { throw new NotSupportedException("Stream cannot be read!"); }

			String marker = stream.ReadString(6);
			UInt16 objectType = stream.ReadWord();
			UInt16 objectId = stream.ReadWord();
			UInt16 masterId = stream.ReadWord();
			UInt32 crc = stream.ReadUnsignedLong();
			UInt32 size = stream.ReadUnsignedLong();
			
			return new GBMObjectHeader(marker, objectType, objectId, masterId != 0 ? masterId : (UInt16?) null, crc, size);
		}

		/// <summary>
		/// Reads a <see cref="GBMObjectHeader"/> from the stream, returning default if at the end.
		/// </summary>
		/// <param name="stream">The stream to read from</param>
		/// <param name="def">The default value to return.</param>
		/// <exception cref="NotSupportedException">When the stream does not support reading.</exception>
		internal static GBMObjectHeader ReadGBMObjectHeader(this Stream stream, GBMObjectHeader def) {
			if (!stream.CanRead) { throw new NotSupportedException("Stream cannot be read!"); }

			try {
				String marker = stream.ReadString(6);
				UInt16 objectType = stream.ReadWord();
				UInt16 objectId = stream.ReadWord();
				UInt16 masterId = stream.ReadWord();
				UInt32 crc = stream.ReadUnsignedLong();
				UInt32 size = stream.ReadUnsignedLong();

				return new GBMObjectHeader(marker, objectType, objectId, masterId != 0 ? masterId : (UInt16?) null, crc, size);
			} catch (EndOfStreamException) {
				return def;
			}
		}

		/// <summary>
		/// Writes a <see cref="GBMObjectHeader"/> to the stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="value">The GBMObjectHeader to write.</param>
		/// <exception cref="NotSupportedException">When the stream cannot be written to.</exception>
		internal static void WriteGBMObjectHeader(this Stream stream, GBMObjectHeader value) {
			if (!stream.CanWrite) { throw new NotSupportedException("Stream cannot be written to!"); }

			stream.WriteString(value.Marker, 6);
			stream.WriteWord(value.ObjectType);
			stream.WriteWord(value.ObjectID);
			stream.WriteWord(value.MasterID.HasValue ? value.MasterID.Value : (UInt16)0);
			stream.WriteUnsignedLong(value.CRC);
			stream.WriteUnsignedLong(value.Size);
		}
#pragma warning restore 618
		#endregion
	}
}
