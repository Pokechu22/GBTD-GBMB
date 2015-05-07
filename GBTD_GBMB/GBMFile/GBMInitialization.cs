using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.GBMFile
{
	public static class GBMInitialization
	{                                                                                                                                 
		/// <summary>
		/// Reads an object and its Header and returns said object.
		/// </summary>
		/// <param name="master">The master object or null if there is no master.</param>
		/// <param name="header">The header of the object.</param>
		/// <param name="s">The stream to read from.</param>
		/// <returns></returns>
		public static GBMObject ReadObject(GBMObject master, GBMObjectHeader header, Stream s) {
			if (mapping.ContainsKey(header.ObjectID)) {
				return mapping[header.ObjectID].Read(master, header, s);
			} else {
				return new GBMObjectUnknownData(master, header, s);
			}
		}

		/// <summary>
		/// Creates an object of the specified type in the given file.  The object is added to the file, and an ID is assigned.
		/// 
		/// <para>If a master object is needed, it will be created.</para>
		/// </summary>
		/// <typeparam name="TObjectType"></typeparam>
		/// <param name="file"></param>
		/// <returns></returns>
		public static GBMObject CreateObject<TObjectType>(GBMFile file) {
			return null;
		}

		private class ObjectMapping
		{
			public ObjectMapping(UInt16 ID, Type Type, Func<GBMFile, UInt16, GBMObject> Create,
					Func<GBMObject, GBMObjectHeader, Stream, GBMObject> Read) {
				this.ID = ID;
				this.Type = Type;
				this.Create = Create;
				this.Read = Read;
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
			/// Function that creates a new object of the given type.  The file should be modified to add it as well.
			/// </summary>
			public readonly Func<GBMFile, UInt16, GBMObject> Create;
			/// <summary>
			/// Function that reads the given object from the stream.
			/// </summary>
			public readonly Func<GBMObject, GBMObjectHeader, Stream, GBMObject> Read;

			public override string ToString()
			{
				return String.Format("ID: 0x{0:X4}\nType: {1}\nCreate: {2}\nRead: {3}", this.ID, this.Type, this.Create, this.Read);
			}
		}

		private static Dictionary<UInt16, ObjectMapping> mapping = new Dictionary<UInt16, ObjectMapping>();

		public static void RegisterExportable<TObjectType>(UInt16 ID, Func<GBMFile, UInt16, TObjectType> Create, 
					Func<GBMObject, GBMObjectHeader, Stream, TObjectType> Read) where TObjectType : GBMObject {

			if (mapping.ContainsKey(ID)) {
				throw new InvalidOperationException(String.Format("Already registered mapping for ID 0x{0:X4}:\n{1}", ID, mapping[ID]));
			}

			mapping.Add(ID, new ObjectMapping(ID, typeof(TObjectType), Create, Read));
		}

		public static UInt16 GetTypeID<TObjectType>() where TObjectType : GBMObject {
			return GetTypeID(typeof(TObjectType));
		}

		public static UInt16 GetTypeID(Type type) {
			return mapping.Values.Single(o => (o.Type == type)).ID;
		}

		static GBMInitialization() {
			RegisterExportable<GBMObjectDeleted>(0xFFFF, (f, i) => new GBMObjectDeleted(i), (o, h, s) => new GBMObjectDeleted(o, h, s));
			RegisterExportable<GBMObjectProducerInfo>(0x0001, (f, i) => new GBMObjectProducerInfo(i), (o, h, s) => new GBMObjectProducerInfo(o, h, s));
			RegisterExportable<GBMObjectMap>(0x0002, (f, i) => new GBMObjectMap(i), (o, h, s) => new GBMObjectMap(o, h, s));
			RegisterExportable<GBMObjectMapTileData>(0x0003, (f, i) => new GBMObjectMapTileData(i, f), (o, h, s) => new GBMObjectMapTileData((GBMObjectMap)o, h, s));
			RegisterExportable<GBMObjectMapProperties>(0x0004, (f, i) => new GBMObjectMapProperties(i, f), (o, h, s) => new GBMObjectMapProperties((GBMObjectMap)o, h, s));
			RegisterExportable<GBMObjectMapPropertyData>(0x0005, (f, i) => new GBMObjectMapPropertyData(i, f), (o, h, s) => new GBMObjectMapPropertyData((GBMObjectMap)o, h, s));
			RegisterExportable<GBMObjectDefaultTilePropertyValues>(0x0006, (f, i) => new GBMObjectDefaultTilePropertyValues(i, f), (o, h, s) => new GBMObjectDefaultTilePropertyValues((GBMObjectMap)o, h, s));
			RegisterExportable<GBMObjectMapSettings>(0x0007, (f, i) => new GBMObjectMapSettings(i, f), (o, h, s) => new GBMObjectMapSettings((GBMObjectMap)o, h, s));
			RegisterExportable<GBMObjectMapPropertyColors>(0x0008, (f, i) => new GBMObjectMapPropertyColors(i, f), (o, h, s) => new GBMObjectMapPropertyColors((GBMObjectMap)o, h, s));
			RegisterExportable<GBMObjectMapExportSettings>(0x0009, (f, i) => new GBMObjectMapExportSettings(i, f), (o, h, s) => new GBMObjectMapExportSettings((GBMObjectMap)o, h, s));
			RegisterExportable<GBMObjectMapExportProperties>(0x000A, (f, i) => new GBMObjectMapExportProperties(i, f), (o, h, s) => new GBMObjectMapExportProperties((GBMObjectMapExportSettings)o, h, s));
		}
	}
}
