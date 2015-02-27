using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GBMFile
{
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
	}
}
