using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBRFile
{
	public class GBRFile
	{
		public class GBRFileHeader
		{
			//The default data.  Encoded like this because ASCII.
			private const byte DEFAULT_MM1 = 0x47 /* 'G' */;
			private const byte DEFAULT_MM2 = 0x42 /* 'B' */;
			private const byte DEFAULT_MM3 = 0x4F /* 'O' */;
			private const byte DEFAULT_VER = 0x30 /* '0' */;

			public byte MagicMarker1 { get; private set; }
			public byte MagicMarker2 { get; private set; }
			public byte MagicMarker3 { get; private set; }

			public byte VersionCode { get; private set; }

			public GBRFileHeader() {
				//Default value.
				MagicMarker1 = DEFAULT_MM1;
				MagicMarker2 = DEFAULT_MM2;
				MagicMarker3 = DEFAULT_MM3;

				VersionCode = DEFAULT_VER;
			}

			public GBRFileHeader(byte mm1, byte mm2, byte mm3, byte version) {
				MagicMarker1 = mm1;
				MagicMarker2 = mm2;
				MagicMarker3 = mm3;

				VersionCode = version;
			}

			public GBRFileHeader(Stream s) {
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

		/// <summary>
		/// The header of the file.
		/// </summary>
		public readonly GBRFileHeader Header;

		/// <summary>
		/// All actual objects, arranged by their ID.
		/// </summary>
		public Dictionary<UInt16, GBRObject> Objects;

		/// <summary>
		/// Gets the Object with the specified objectID, ignoring objects that have been deleted.
		/// </summary>
		/// <param name="ObjectID"></param>
		/// <returns>The specified object, or <c>null</c> if none exist.</returns>
		public GBRObject GetObjectWithID(UInt16 ID) {
			return Objects[ID];
		}
		/// <summary>
		/// Gets the Object with the specified objectID and of the specified type, ignoring objects that have been deleted.
		/// </summary>
		/// <param name="ObjectID"></param>
		/// <typeparam name="TObjectType">The type of object to search for.</typeparam>
		/// <returns>The specified object, or <c>null</c> if none exist.</returns>
		public TObjectType GetObjectWithID<TObjectType>(UInt16 ID) where TObjectType : GBRObject {
			return (Objects[ID] as TObjectType);
		}

		/// <summary>
		/// Gets the only object of the given type.
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <returns></returns>
		public TObjectType GetObjectOfType<TObjectType>() where TObjectType : GBRObject {
			return Objects.Values.OfType<TObjectType>().Single();
		}

		/// <summary>
		/// Gets the only object of the given type, or creates one if it does not exist.
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <returns></returns>
		public TObjectType GetOrCreateObjectOfType<TObjectType>() where TObjectType : GBRObject {
			if (Objects.Values.OfType<TObjectType>().Count() == 0) {
				TObjectType obj = GBRInitialization.CreateObject<TObjectType>(this);
				this.Objects.Add(obj.Header.UniqueID, obj);
			}

			return Objects.Values.OfType<TObjectType>().Single();
		}

		/// <summary>
		/// Gets all of the objects that are of the specified type.
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <returns></returns>
		public List<TObjectType> GetObjectsOfType<TObjectType>() where TObjectType : GBRObject {
			return new List<TObjectType>(Objects.Values.OfType<TObjectType>());
		}

		/// <summary>
		/// Gets the object that is refered to by the specified object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public TRefferedType GetReferedObject<TRefferedType>(ReferentialGBRObject<TRefferedType> obj) where TRefferedType : GBRObject {
			return Objects
				.OfType<TRefferedType>()
				.Single(o => o.Header.UniqueID == obj.ReferedObjectUniqueID);
		}

		/// <summary>
		/// Gets all objects that refer to the specified object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<ReferentialGBRObject<TObjectType>> GetReferingObjects<TObjectType>(TObjectType obj) where TObjectType : GBRObject {
			return new List<ReferentialGBRObject<TObjectType>>(Objects.Values
				.OfType<ReferentialGBRObject<TObjectType>>()
				.Where(o => o.ReferedObjectUniqueID == obj.Header.UniqueID));
		}

		public GBRFile() {
			this.Header = new GBRFileHeader();

			Objects = new Dictionary<UInt16, GBRObject>();

			this.Objects.Add(0, GBRInitialization.CreateObject<GBRObjectProducerInfo>(0, this));
			this.Objects.Add(1, GBRInitialization.CreateObject<GBRObjectTileData>(1, this));
			this.Objects.Add(2, GBRInitialization.CreateObject<GBRObjectTileSettings>(2, this));
			this.Objects.Add(3, GBRInitialization.CreateObject<GBRObjectTileExport>(3, this));
			this.Objects.Add(4, GBRInitialization.CreateObject<GBRObjectTileImport>(4, this));
			this.Objects.Add(5, GBRInitialization.CreateObject<GBRObjectPalettes>(5, this));
			this.Objects.Add(6, GBRInitialization.CreateObject<GBRObjectTilePalette>(6, this));
		}

		/// <summary>
		/// Contains the location and header of an object that may not yet have been deserialized.
		/// </summary>
		private struct GBRObjectReference
		{
			public GBRObjectReference(GBRObjectHeader Header, long Position) {
				this.Header = Header;
				this.Position = Position;
			}

			public readonly GBRObjectHeader Header;
			public readonly long Position;

			public override string ToString() {
				return "Header: " + Header + "; Position: " + Position;
			}
		}

		public GBRFile(Stream stream) {
			if (!stream.CanSeek) {
				throw new NotSupportedException("Stream does not support seeking; that is required for this functionality");
			}

			const UInt16 DELETED_OBJECT_TYPE = 0x00FF;

			this.Header = new GBRFileHeader(stream);
			//TODO: this.Header.Validate();

			Objects = new Dictionary<UInt16, GBRObject>();

			Dictionary<UInt16, GBRObjectReference> dict = new Dictionary<UInt16, GBRObjectReference>();
			try {
				while (true) {
					GBRObjectHeader header = stream.ReadHeader();

					if (header.ObjectTypeID != DELETED_OBJECT_TYPE) {
						GBRObjectReference r = new GBRObjectReference(header, stream.Position);
						dict.Add(header.UniqueID, r);
					}

					//Advance the stream by the specified amount.
					stream.Seek(header.Size, SeekOrigin.Current);
				}
			} catch (EndOfStreamException) {
				//End of the stream; we want to go back and read all of the objects now.
			}

			foreach (GBRObjectReference reference in dict.Values) {
				stream.Position = reference.Position;

				this.Objects.Add(reference.Header.UniqueID, GBRInitialization.ReadObject(reference.Header, this, stream));
			}
		}

		public void SaveToStream(Stream s) {
			if (!s.CanWrite) { throw new NotSupportedException("Cannot write to stream."); }
			if (!s.CanSeek) { throw new NotSupportedException("Cannot seek stream."); }

			s.Position = 0;
			this.Header.SaveToStream(s);

			foreach (GBRObject obj in this.Objects.Values) {
				GBRInitialization.SaveObject(s, this, obj);
			}
		}
	}
}
