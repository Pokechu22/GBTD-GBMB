using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBRFile
{
	public static class GBRInitialization
	{
		public static UInt16 GetTypeID<TObjectType>() where TObjectType : GBRObject {
			return GetTypeID(typeof(TObjectType));
		}

		public static UInt16 GetTypeID(Type type) {
			return mapping.Values.Single(o => (o.Type == type)).ID;
		}

		/// <summary>
		/// Reads an object and its Header and returns said object.
		/// </summary>
		/// <param name="master">The master object or null if there is no master.</param>
		/// <param name="header">The header of the object.</param>
		/// <param name="s">The stream to read from.</param>
		/// <returns></returns>
		public static GBRObject ReadObject(GBRObjectHeader header, GBRFile file, Stream s) {
			GBRObject obj;

			if (mapping.ContainsKey(header.ObjectTypeID)) {
				obj = mapping[header.ObjectTypeID].Create(header.UniqueID);
			} else {
				obj = new GBRObjectUnknownData(header);
			}

			byte[] data = new byte[header.Size];
			int read = s.Read(data, 0, (int)header.Size);

			if (read != header.Size) {
				throw new EndOfStreamException();
			}

			obj.loadedData = data;

			using (MemoryStream ns = new MemoryStream(data, false)) {
				obj.LoadFromStream(file, ns);

				if (ns.Position != ns.Length) {
					obj.extraData = new byte[ns.Length - ns.Position];
					ns.Read(obj.extraData, 0, (int)(ns.Length - ns.Position));
				}
			}

			return obj;
		}

		/// <summary>
		/// Creates an object of the specified type.  An unique ID is assigned to the object, 
		/// but the object is NOT added to the file.
		/// 
		/// <para>If a master object is needed, it will be created.  The file is used for locating the master.</para>
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <param name="file"></param>
		/// <returns></returns>
		public static TObjectType CreateObject<TObjectType>(GBRFile file) where TObjectType : GBRObject {
			return CreateObject<TObjectType>(GetNextUniqueID(file), file);
		}

		/// <summary>
		/// Creates an object of the specified type.  An unique ID is assigned to the object, 
		/// but the object is NOT added to the file.
		/// 
		/// <para>If a master object is needed, it will be created.  The file is used for locating the master.</para>
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <param name="file"></param>
		/// <returns></returns>
		public static GBRObject CreateObject(UInt16 ObjectType, GBRFile file) {
			return CreateObject(ObjectType, GetNextUniqueID(file), file);
		}

		/// <summary>
		/// Creates an object of the specified type.  An unique ID is assigned to the object, 
		/// but the object is NOT added to the file.
		/// 
		/// <para>If a master object is needed, it will be created.  The file is used for locating the master.</para>
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <param name="file"></param>
		/// <returns></returns>
		public static TObjectType CreateObject<TObjectType>(UInt16 ObjectID, GBRFile file) where TObjectType : GBRObject {
			var objectMapping = mapping.Values.Single(o => (o.Type == typeof(TObjectType)));

			TObjectType obj = (TObjectType)objectMapping.Create(ObjectID);
			obj.SetupObject(file);

			return obj;
		}

		/// <summary>
		/// Creates an object of the specified type with the given unique ID.
		/// The object is NOT added to the file.
		/// 
		/// <para>If a master object is needed, it will be created.  The file is used for locating the master.</para>
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <param name="file"></param>
		/// <returns></returns>
		public static GBRObject CreateObject(UInt16 ObjectType, UInt16 ObjectID, GBRFile file) {
			var objectMapping = mapping[ObjectType];

			GBRObject obj = objectMapping.Create(ObjectID);
			obj.SetupObject(file);

			return obj;
		}

		/// <summary>
		/// Gets the next open UniqueID in the given file.
		/// </summary>
		/// <param name="file"></param>
		private static UInt16 GetNextUniqueID(GBRFile file) {
			for (UInt16 id = 1; id < UInt16.MaxValue; id++) {
				if (!file.Objects.ContainsKey(id)) {
					return id;
				}
			}

			throw new InvalidOperationException("There are no more unique object IDs!");
		}

		private class ObjectMapping
		{
			public ObjectMapping(UInt16 ID, Type Type, Func<UInt16, GBRObject> Create) {
				this.ID = ID;
				this.Type = Type;
				this.Create = Create;
			}

			/// <summary>
			/// Numeric object ID for serialization.
			/// </summary>
			public readonly UInt16 ID;
			/// <summary>
			/// The type of the object.
			/// </summary>
			public readonly Type Type;
			/// <summary>
			/// Function that creates a new object, but doesn't populate the object.
			/// </summary>
			public readonly Func<UInt16, GBRObject> Create;

			public override string ToString()
			{
				return String.Format("ID: 0x{0:X4}\nType: {1}\nCreate: {2}\n", this.ID, this.Type, this.Create);
			}
		}

		private static Dictionary<UInt16, ObjectMapping> mapping = new Dictionary<UInt16, ObjectMapping>();

		public static void RegisterExportable<TObjectType>(UInt16 ID, Func<UInt16, TObjectType> Constructor) where TObjectType : GBRObject {

			if (mapping.ContainsKey(ID)) {
				throw new InvalidOperationException(String.Format("Already registered mapping for ID 0x{0:X4}:\n{1}", ID, mapping[ID]));
			}

			mapping.Add(ID, new ObjectMapping(ID, typeof(TObjectType), Constructor));
		}

		static GBRInitialization() {
			RegisterExportable<GBRObjectProducerInfo>(0x0001, (u) => new GBRObjectProducerInfo(u));
			RegisterExportable<GBRObjectTileData>(0x0002, (u) => new GBRObjectTileData(u));
			RegisterExportable<GBRObjectTileSettings>(0x0003, (u) => new GBRObjectTileSettings(u));
			RegisterExportable<GBRObjectTileExport>(0x0004, (u) => new GBRObjectTileExport(u));
			RegisterExportable<GBRObjectTileImport>(0x0005, (u) => new GBRObjectTileImport(u));
			RegisterExportable<GBRObjectPalettes>(0x000D, (u) => new GBRObjectPalettes(u));
			RegisterExportable<GBRObjectTilePalette>(0x000E, (u) => new GBRObjectTilePalette(u));

			RegisterExportable<GBRObjectDeleted>(0x00FF, (u) => new GBRObjectDeleted(u));
		}
	}
}
