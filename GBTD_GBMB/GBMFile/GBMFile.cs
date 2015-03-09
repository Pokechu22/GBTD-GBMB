using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBMFile
{
	public class GBMFile
	{
		/// <summary>
		/// Contains the location and header of an object that may not yet have been deserialized.
		/// </summary>
		private struct GBMObjectReference
		{
			public GBMObjectReference(GBMObjectHeader Header, long Position) {
				this.Header = Header;
				this.Position = Position;
			}

			public readonly GBMObjectHeader Header;
			public readonly long Position;

			public override string ToString() {
				return "Header: " + Header + "; Position: " + Position;
			}
		}

		/// <summary>
		/// Contains the header for a GBM file.
		/// </summary>
		public class GBMFileHeader
		{
			//The default data.  Encoded like this because ASCII.
			private const byte DEFAULT_MM1 = 0x47 /* 'G' */;
			private const byte DEFAULT_MM2 = 0x42 /* 'B' */;
			private const byte DEFAULT_MM3 = 0x4F /* 'O' */;
			private const byte DEFAULT_VER = 0x31 /* '1' */;

			public byte MagicMarker1 { get; private set; }
			public byte MagicMarker2 { get; private set; }
			public byte MagicMarker3 { get; private set; }

			public byte VersionCode { get; private set; }

			/// <summary>
			/// Creates a GBMFileHeader with the default values.
			/// </summary>
			public GBMFileHeader() {
				//Default value.
				MagicMarker1 = DEFAULT_MM1;
				MagicMarker2 = DEFAULT_MM2;
				MagicMarker3 = DEFAULT_MM3;

				VersionCode = DEFAULT_VER;
			}

			/// <summary>
			/// Creates a GBMFileHeader with the specified data.
			/// </summary>
			/// <param name="mm1">The first letter of the magic marker.</param>
			/// <param name="mm2">The second letter of the magic marker.</param>
			/// <param name="mm3">The third letter of the magic marker.</param>
			/// <param name="version">The file version. </param>
			public GBMFileHeader(byte mm1, byte mm2, byte mm3, byte version) {
				MagicMarker1 = mm1;
				MagicMarker2 = mm2;
				MagicMarker3 = mm3;

				VersionCode = version;
			}

			/// <summary>
			/// Creates a GBMFileHeader from the given stream.
			/// </summary>
			/// <param name="s">The stream to load from.</param>
			public GBMFileHeader(Stream s) {
				LoadFromStream(s);
			}

			/// <summary>
			/// Saves the header to the given stream.
			/// </summary>
			public void SaveToStream(Stream s) {
				if (!s.CanWrite) {
					throw new NotSupportedException("Cannot write to stream");
				}

				s.WriteByte(MagicMarker1);
				s.WriteByte(MagicMarker2);
				s.WriteByte(MagicMarker3);

				s.WriteByte(VersionCode);
			}

			/// <summary>
			/// Loads the header from the given stream.
			/// </summary>
			public void LoadFromStream(Stream s) {
				if (!s.CanRead) {
					throw new NotSupportedException("Cannot read from stream");
				}

				byte[] bytes = new byte[4];
				s.Read(bytes, 0, 4);

				MagicMarker1 = bytes[0];
				MagicMarker2 = bytes[1];
				MagicMarker3 = bytes[2];

				VersionCode = bytes[3];
			}

			/// <summary>
			/// Checks if this is a valid GBR file.
			/// </summary>
			/// <returns></returns>
			public bool IsValidFile() {
				if (MagicMarker1 != DEFAULT_MM1) { return false; }
				if (MagicMarker2 != DEFAULT_MM2) { return false; }
				if (MagicMarker3 != DEFAULT_MM3) { return false; }

				return true;
			}

			/// <summary>
			/// Checks if this is a supported GBR version (Currently GBR 0).
			/// </summary>
			/// <returns></returns>
			public bool IsSupportedVersion() {
				return VersionCode == DEFAULT_VER;
			}

			/// <summary>
			/// Validates that this is a valid, supported file -- i.e. the magic markers are correct and the version is supported.
			/// <para>This method does nothing if it is a valid file, and throws an exception if invalid.</para>
			/// </summary>
			/// <exception cref="Exception">When the file is invalid.</exception>
			public void Validate() {
				if (MagicMarker1 != DEFAULT_MM1) {
					throw new Exception("GBM file magic marker 1 is invalid - got " + MagicMarker1 + ", expected " + DEFAULT_MM1 + ".");
				}
				if (MagicMarker2 != DEFAULT_MM2) {
					throw new Exception("GBM file magic marker 2 is invalid - got " + MagicMarker2 + ", expected " + DEFAULT_MM2 + ".");
				}
				if (MagicMarker3 != DEFAULT_MM3) {
					throw new Exception("GBM file magic marker 3 is invalid - got " + MagicMarker3 + ", expected " + DEFAULT_MM3 + ".");
				}
				if (VersionCode != DEFAULT_VER) {
					throw new Exception("GBM file version is invalid - got " + VersionCode + ", expected " + DEFAULT_VER + ".\n" +
						"(Did you accidently load a GBR file?)");
				}
			}
		}

		/// <summary>
		/// The header of the file.
		/// </summary>
		public readonly GBMFileHeader FileHeader;

		/// <summary>
		/// All actual objects, arranged by their ID.
		/// </summary>
		public Dictionary<UInt16, GBMObject> Objects;
		
		/// <summary>
		/// Creates an empty GBMFile.
		/// </summary>
		public GBMFile() {
			this.FileHeader = new GBMFileHeader();
			this.Objects = new Dictionary<ushort, GBMObject>();
		}

		/// <summary>
		/// Gets the first object of the specified type.
		/// </summary>
		/// <typeparam name="TObject"></typeparam>
		/// <returns></returns>
		public TObject GetObjectOfType<TObject>() where TObject : GBMObject {
			return this.Objects.Values.OfType<TObject>().First();
		}

		/// <summary>
		/// Creates a GBMFile from the specified stream's contents.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		public GBMFile(Stream stream) {
			if (!stream.CanSeek) {
				throw new NotSupportedException("Stream does not support seeking; that is required for this functionality");
			}

			const UInt16 DELETED_OBJECT_TYPE = 0xFFFF;

			this.FileHeader = new GBMFileHeader(stream);
			this.FileHeader.Validate();
			
			Objects = new Dictionary<UInt16, GBMObject>();

			Dictionary<UInt16, GBMObjectReference> dict = new Dictionary<UInt16, GBMObjectReference>();
			try {
				while (true) {
					GBMObjectHeader header = stream.ReadGBMObjectHeader();

					if (header.ObjectType != DELETED_OBJECT_TYPE) {
						GBMObjectReference r = new GBMObjectReference(header, stream.Position);
						dict.Add(header.ObjectID, r);
					}

					//Advance the stream by the specified amount.
					stream.Seek(header.Size, SeekOrigin.Current);
				}
			} catch (EndOfStreamException) {
				//End of the stream; we want to go back and read all of the objects now.
				//TODO: Can we be 100% sure that this will ACTUALLY happen, and the file won't grow indefinitely?
			}

			List<long> readOffsets = new List<long>();
			foreach (GBMObjectReference reference in dict.Values) {
				ReadObjectAndMaster(stream, reference, dict, readOffsets);
			}
		}

		/// <summary>
		/// The maximum depth for master objects before it is treated as an infinite loop.
		/// </summary>
		const int MAXIMUM_MASTER_DEPTH = 20;

		/// <summary>
		/// Reads a single object, and its master objects if there are any.
		/// <para>The master objects are read before the object itself, so that all needed objects are loaded.</para>
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="toRead">The object that should be read at this moment.</param>
		/// <param name="references">A collection of all existing objects.</param>
		/// <param name="readOffsets">A list of all objects that have been read - used to avoid re-reading objects.</param>
		/// <param name="currentDepth">The current depth in master objects.
		/// DO NOT SPECIFY IF YOU ARE CALLING THIS METHOD NORMALLY.  It is only of use when recursing through the masters.</param>
		/// <returns>The object read, but also adds it to the array.  You probably won't use this unless you're recursing.</returns>
		private GBMObject ReadObjectAndMaster(Stream stream, GBMObjectReference toRead, Dictionary<UInt16, GBMObjectReference> references,
				List<long> readOffsets, int currentDepth = MAXIMUM_MASTER_DEPTH) {
			toRead.Header.Validate(toRead.Position);

			if (currentDepth <= 0) {
				//TODO Better text and exception type.
				throw new Exception("Master object depth too high - over " + MAXIMUM_MASTER_DEPTH + " objects (is there an infinite loop?)");
			}

			//The master object, if there is one.
			//Set later on if it is needed.
			GBMObject master = null;

			//If there is a master object that may need to be loaded.
			if (toRead.Header.MasterID.HasValue) {
				UInt16 masterID = toRead.Header.MasterID.Value;

				if (!references.ContainsKey(masterID)) {
					//TODO: Throwing simply an exception might not make sense here.  Select a better type.
					throw new Exception("The master object " + masterID.ToString("X4") + " for object " + toRead.Header.ObjectID.ToString("X4") + " could not be found.  (Corrupt / invalid GBM file?)");
				}

				//If the specified object hasn't yet been loaded, load it (and any of its masters).
				if (!this.Objects.ContainsKey(masterID)) {
					master = ReadObjectAndMaster(stream, references[masterID], references, readOffsets, currentDepth - 1);
				} else {
					master = this.Objects[masterID];
				}
			}

			//OK, now that the master has been loaded if needed, load the object itself.

			//If the object at that offset has already been read, abort.
			if (readOffsets.Contains(toRead.Position)) {
				return Objects[toRead.Header.ObjectID];
			}
			//If an object with that ID has already been loaded, but it is not the same object, throw an exception.
			if (Objects.ContainsKey(toRead.Header.ObjectID)) {
				//TODO: Better type of excpetion
				//TODO: Include string version of the objects?
				throw new Exception("Object id " + toRead.Header.ObjectID.ToString("X4") + " has already been registered.  (Corrupt / invalid GBM file?)");
			}

			stream.Seek(toRead.Position, SeekOrigin.Begin);
			GBMObject obj = GBMObject.ReadObject(master, toRead.Header, stream);

			Objects.Add(obj.Header.ObjectID, obj);

			//Mark the given offset as read.
			readOffsets.Add(toRead.Position);

			return obj;
		}
	}
}
