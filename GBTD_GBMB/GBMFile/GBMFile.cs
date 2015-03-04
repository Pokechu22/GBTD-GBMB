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

			public GBMFileHeader() {
				//Default value.
				MagicMarker1 = DEFAULT_MM1;
				MagicMarker2 = DEFAULT_MM2;
				MagicMarker3 = DEFAULT_MM3;

				VersionCode = DEFAULT_VER;
			}

			public GBMFileHeader(byte mm1, byte mm2, byte mm3, byte version) {
				MagicMarker1 = mm1;
				MagicMarker2 = mm2;
				MagicMarker3 = mm3;

				VersionCode = version;
			}

			public GBMFileHeader(Stream s) {
				LoadFromStream(s);
			}

			public void SaveToStream(Stream s) {
				s.WriteByte(MagicMarker1);
				s.WriteByte(MagicMarker2);
				s.WriteByte(MagicMarker3);

				s.WriteByte(VersionCode);
			}

			public void LoadFromStream(Stream s) {
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
		}

		public readonly GBMFileHeader FileHeader;

		/// <summary>
		/// All objects that were read, in order, including deleted ones.
		/// Please don't use this for actual data.
		/// </summary>
		[Obsolete]
		public List<GBMObject> ReadObjects;
		/// <summary>
		/// All actual objects, arranged by their ID.
		/// </summary>
		public Dictionary<UInt16, GBMObject> Objects;

		public GBMFile() {
			this.FileHeader = new GBMFileHeader();
		}

		public GBMFile(Stream stream) {
			if (!stream.CanSeek) {
				throw new NotSupportedException("Stream does not support seeking; that is required for this functionality");
			}

			this.FileHeader = new GBMFileHeader(stream);

			//TODO validation here.
			ReadObjects = new List<GBMObject>();
			Objects = new Dictionary<UInt16, GBMObject>();

			Dictionary<UInt16, GBMObjectReference> dict = new Dictionary<UInt16, GBMObjectReference>();
			try {
				while (true) {
					GBMObjectHeader header = stream.ReadGBMObjectHeader();

					if (header.ObjectType == 0xFFFF) { //TODO: Make it clearer that this is deleted with a constant.
						ReadObjects.Add(GBMObject.ReadObject(header, stream)); //Add the read object, but do nothing else with it.

					} else {
						GBMObjectReference r = new GBMObjectReference(header, stream.Position);
						dict.Add(header.ObjectID, r);
						//Advance the stream by the specified ammount.
						stream.Seek(header.Size, SeekOrigin.Current);
					}
				}
			} catch (EndOfStreamException) {
				//End of the stream; we want to go back and read all of the objects now.
				//TODO: Can we be 100% sure that this will ACTUALLY happen, and the file won't grow indefinitely?
			}

			foreach (GBMObjectReference reference in dict.Values) {
				ReadObjectAndMaster(stream, reference, dict);
			}
		}

		private void ReadObjectAndMaster(Stream stream, GBMObjectReference toRead, Dictionary<UInt16, GBMObjectReference> references) {
			//If there is a master object that may need to be loaded.
			if (toRead.Header.MasterID.HasValue) {
				UInt16 masterID = toRead.Header.MasterID.Value;

				if (!references.ContainsKey(masterID)) {
					//TODO: Throwing simply an exception might not make sense here.  Select a better type.
					throw new Exception("The master object " + masterID.ToString("X4") + " for object " + toRead.Header.ObjectID.ToString("X4") + " could not be found.  (Corrupt / invalid GBM file?)");
				}

				//If the specified object hasn't yet been loaded, load it (and any of its masters).
				if (!this.Objects.ContainsKey(masterID)) {
					ReadObjectAndMaster(stream, references[masterID], references);
				}
			}

			//OK, now that the master has been loaded if needed, load the object itself.

			//If the object has already been loaded, something weird is happening, because there should ONLY be one object with a specific ID.
			if (Objects.ContainsKey(toRead.Header.ObjectID)) {
				//TODO: Better type of excpetion
				//TODO: Include string version of the objects?
				throw new Exception("Object id " + toRead.Header.ObjectID.ToString("X4") + " has already been registered.  (Corrupt / invalid GBM file?)");
			}

			stream.Seek(toRead.Position, SeekOrigin.Begin);
			GBMObject obj = GBMObject.ReadObject(toRead.Header, stream);

			ReadObjects.Add(obj);
			Objects.Add(obj.Header.ObjectID, obj);
		}
	}
}
