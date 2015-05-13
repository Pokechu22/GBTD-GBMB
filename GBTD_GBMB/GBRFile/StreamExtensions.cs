using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Provides functionality to modify streams per the GBR spec.
	/// It's an internal class because it would otherwise conflict with the GBM spec.
	/// </summary>
	internal static class StreamExtensions
	{
		/// <summary>
		/// Writes a string to the specified stream, in the expected format.
		/// <para>Per the doc: 
		/// String (xx)	C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).</para>
		/// <para>This method will truncate the string if necessary.
		/// </para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="s">The string to write.</param>
		/// <param name="length">The maximum allowed length.  Will be padded up to if needed.</param>
		internal static void WriteString(this Stream stream, String s, uint length) {
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

		/// <summary>
		/// Writes a 16-bit (2 byte) unsigned number, hi-endian, to the specified stream.
		/// <param name="stream">The stream to write to.</param>
		/// <param name="n">The number to write.</param>
		/// </summary>
		internal static void WriteWord(this Stream stream, UInt16 n) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteByte((byte)((n >> 0) & 0xFF));
			stream.WriteByte((byte)((n >> 8) & 0xFF));
		}

		/// <summary>
		/// Writes a 32-bit (4 byte) unsigned number, hi-endian, to the specified stream.
		/// <param name="stream">The stream to write to.</param>
		/// <param name="n">The number to write.</param>
		/// </summary>
		internal static void WriteLong(this Stream stream, UInt32 n) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteByte((byte)((n >> 0) & 0xFF));
			stream.WriteByte((byte)((n >> 8) & 0xFF));
			stream.WriteByte((byte)((n >> 16) & 0xFF));
			stream.WriteByte((byte)((n >> 24) & 0xFF));
		}

		/// <summary>
		/// Writes a boolean to the specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="b">The boolean to write.</param>
		internal static void WriteBool(this Stream stream, bool b) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			//I would use the ternary opperator here, but it stangely requiers byte casts, so this seems cleaner for now.
			if (b) {
				stream.WriteByte(1);
			} else {
				stream.WriteByte(0);
			}
		}

		/// <summary>
		/// Writes the object Header to specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		internal static void WriteHeader(this Stream stream, GBRObjectHeader header) {
			if (!stream.CanWrite) {
				throw new NotSupportedException("Stream cannot be written to.");
			}

			stream.WriteWord(header.ObjectTypeID);
			stream.WriteWord(header.UniqueID);
			stream.WriteLong(header.Size);
		}

		/// <summary>
		/// Reads a string to the specified stream, in the expected format.
		/// <para>Per the doc: 
		/// String (xx)	C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).</para>
		/// <para>This method will read until a null terminator or the end of the length.
		/// </para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="length">The maximum expected length.</param>
		internal static String ReadString(this Stream stream, uint length) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[length];
			int read = stream.Read(bytes, 0, (int)length);

			if (read != length) {
				throw new EndOfStreamException();
			}

			return Encoding.ASCII.GetString(bytes).TrimEnd('\0');
		}

		/// <summary>
		/// Reads a 16-bit (2 byte) unsigned number, hi-endian, from the specified stream.
		/// <param name="stream">The stream to write to.</param>
		/// </summary>
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
		/// Reads a 32-bit (4 byte) unsigned number, hi-endian, from the specified stream.
		/// <param name="stream">The stream to write to.</param>
		/// </summary>
		internal static UInt32 ReadLong(this Stream stream) {
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
		/// Reads a boolean from the specified stream.
		/// </summary>
		/// <returns></returns>
		internal static bool ReadBool(this Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) {
				throw new EndOfStreamException();
			}

			return (bytes[0] != 0);
		}

		/// <summary>
		/// Reads a byte from the specified stream, throwing an exception if at the end of the stream.
		/// </summary>
		/// <returns></returns>
		internal static byte ReadByteEx(this Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) {
				throw new EndOfStreamException();
			}

			return (bytes[0]);
		}

		/// <summary>
		/// Reads the object Header from specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		internal static GBRObjectHeader ReadHeader(this Stream stream) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			UInt16 TypeID = stream.ReadWord();
			UInt16 UniqueID = stream.ReadWord();
			UInt32 Size = stream.ReadLong();

			return new GBRObjectHeader(TypeID, UniqueID, Size);
		}

		/// <summary>
		/// Reads a string to the specified stream, in the expected format.
		/// <para>Per the doc: 
		/// String (xx)	C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).</para>
		/// <para>This method will read until a null terminator or the end of the length.
		/// </para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="length">The maximum expected length.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		internal static String ReadString(this Stream stream, uint length, string def) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[length];
			int read = stream.Read(bytes, 0, (int)length);

			if (read != length) {
				return def;
			}

			return Encoding.ASCII.GetString(bytes).TrimEnd('\0');
		}

		/// <summary>
		/// Reads a 16-bit (2 byte) unsigned number, hi-endian, from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
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
		/// Reads a 32-bit (4 byte) unsigned number, hi-endian, from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		internal static UInt32 ReadLong(this Stream stream, UInt32 def) {
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
		/// Reads a boolean from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <returns></returns>
		internal static bool ReadBool(this Stream stream, bool def) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) {
				return def;
			}

			return (bytes[0] != 0);
		}

		/// <summary>
		/// Reads a boolean from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <returns></returns>
		internal static byte ReadByte(this Stream stream, byte def) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) {
				return def;
			}

			return bytes[0];
		}

		/// <summary>
		/// Reads a string to the specified stream, in the expected format.
		/// <para>Per the doc: 
		/// String (xx)	C-style string; ie. ends with hex 00, with a maximum length of xx (including end-marker).</para>
		/// <para>This method will read until a null terminator or the end of the length.
		/// </para>
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="length">The maximum expected length.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <param name="ended">
		/// A reference to a boolean that is paletteData to true if the end is reached and otherwise left alone.
		/// <para>Use it to keep track of the end.
		/// If it's already true it won't be reset.  Use it to check if the object needs to be re-written due to reaching the end.</para>
		/// </param>
		internal static String ReadString(this Stream stream, uint length, string def, ref bool ended) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[length];
			int read = stream.Read(bytes, 0, (int)length);

			if (read != length) {
				ended = true;
				return def;
			}

			return Encoding.ASCII.GetString(bytes).TrimEnd('\0');
		}

		/// <summary>
		/// Reads a 16-bit (2 byte) unsigned number, hi-endian, from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <param name="ended">
		/// A reference to a boolean that is paletteData to true if the end is reached and otherwise left alone.
		/// <para>Use it to keep track of the end.
		/// If it's already true it won't be reset.  Use it to check if the object needs to be re-written due to reaching the end.</para>
		/// </param>
		internal static UInt16 ReadWord(this Stream stream, UInt16 def, ref bool ended) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[2];
			int read = stream.Read(bytes, 0, 2);

			if (read != 2) {
				ended = true;
				return def;
			}

			return (UInt16)((bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Reads a 32-bit (4 byte) unsigned number, hi-endian, from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <param name="ended">
		/// A reference to a boolean that is paletteData to true if the end is reached and otherwise left alone.
		/// <para>Use it to keep track of the end.
		/// If it's already true it won't be reset.  Use it to check if the object needs to be re-written due to reaching the end.</para>
		/// </param>
		internal static UInt32 ReadLong(this Stream stream, UInt32 def, ref bool ended) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[4];
			int read = stream.Read(bytes, 0, 4);

			if (read != 4) {
				ended = true;
				return def;
			}

			return (UInt32)((bytes[3] << 24) | (bytes[2] << 16) | (bytes[1] << 8) | (bytes[0] << 0));
		}

		/// <summary>
		/// Reads a boolean from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <param name="ended">
		/// A reference to a boolean that is paletteData to true if the end is reached and otherwise left alone.
		/// <para>Use it to keep track of the end.
		/// If it's already true it won't be reset.  Use it to check if the object needs to be re-written due to reaching the end.</para>
		/// </param>
		/// <returns></returns>
		internal static bool ReadBool(this Stream stream, bool def, ref bool ended) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) {
				ended = true;
				return def;
			}

			return (bytes[0] != 0);
		}

		/// <summary>
		/// Reads a boolean from the specified stream.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="def">The default value to return if at the end of the stream.</param>
		/// <returns></returns>
		internal static byte ReadByte(this Stream stream, byte def, ref bool ended) {
			if (!stream.CanRead) {
				throw new NotSupportedException("Stream cannot be read from.");
			}

			byte[] bytes = new byte[1];
			int read = stream.Read(bytes, 0, 1);

			if (read != 1) {
				ended = true;
				return def;
			}

			return bytes[0];
		}
	}
}
