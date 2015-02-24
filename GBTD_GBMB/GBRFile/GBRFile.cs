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

		public readonly GBRFileHeader Header;

		public List<GBRObject> Objects;

		/// <summary>
		/// Gets the Object with the specified objectID, ignoring objects that have been deleted.
		/// </summary>
		/// <param name="ObjectID"></param>
		/// <returns>The specified object, or <c>null</c> if none exist.</returns>
		public GBRObject GetObjectWithID(UInt16 ID) {
			foreach (GBRObject obj in this.Objects) {
				if (obj is GBRObjectDeleted) {
					continue;
				}
				if (obj.Header.UniqueID == ID) {
					return obj;
				}
			}
			return null;
		}
		/// <summary>
		/// Gets the Object with the specified objectID and of the specified type, ignoring objects that have been deleted.
		/// </summary>
		/// <param name="ObjectID"></param>
		/// <typeparam name="TObjectType">The type of object to search for.</typeparam>
		/// <returns>The specified object, or <c>null</c> if none exist.</returns>
		public TObjectType GetObjectWithID<TObjectType>(UInt16 ID) where TObjectType : GBRObject {
			foreach (GBRObject obj in this.Objects) {
				if (obj is GBRObjectDeleted) {
					continue;
				}
				if (obj is TObjectType) {
					if (obj.Header.UniqueID == ID) {
						return obj as TObjectType;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Gets all of the objects that are of the specified type.
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <returns></returns>
		public List<TObjectType> GetObjectsOfType<TObjectType>() where TObjectType : GBRObject {
			//Woot, Linq!
			return new List<TObjectType>(Objects
				.Where(o => o is TObjectType)
				.Select(o => o as TObjectType));
		}

		/// <summary>
		/// Gets the object that is refered to by the specified object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public GBRObject GetReferedObject(ReferentialGBRObject obj) {
			return Objects
				.First(o => o.Header.UniqueID == obj.ReferedObjectID);
		}

		/// <summary>
		/// Gets all objects that refer to the specified object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<ReferentialGBRObject> GetReferingObjects(GBRObject obj) {
			return new List<ReferentialGBRObject>(Objects
				.Where(o => o is ReferentialGBRObject)
				.Select(o => o as ReferentialGBRObject)
				.Where(o => o.ReferedObjectID == obj.Header.UniqueID));
		}

		/// <summary>
		/// Gets all objects of the specified type that refer to the specified object.
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public List<TObjectType> GetReferingObjects<TObjectType>(GBRObject obj) where TObjectType : ReferentialGBRObject {
			return new List<TObjectType>(Objects
				.Where(o => o is TObjectType)
				.Select(o => o as TObjectType)
				.Where(o => o.ReferedObjectID == obj.Header.UniqueID));
		}

		public GBRFile() {
			this.Header = new GBRFileHeader();
		}

		public GBRFile(Stream stream) {
			this.Header = new GBRFileHeader(stream);

			//TODO validation here.
			Objects = new List<GBRObject>();

			while (true) {
				try {
					Objects.Add(GBRObject.ReadObject(stream));
				} catch (EndOfStreamException) {
					break;
				}
			}
		}
	}
}
