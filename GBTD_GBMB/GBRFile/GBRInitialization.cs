using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
