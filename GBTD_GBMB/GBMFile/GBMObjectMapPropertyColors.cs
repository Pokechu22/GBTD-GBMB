using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapPropertyColors : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapPropertyColors(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectMapPropertyColors(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		protected override void LoadFromStream(Stream s) {
			throw new NotImplementedException();
		}

		protected override void SaveToStream(Stream s) {
			throw new NotImplementedException();
		}

		public override string GetTypeName() {
			throw new NotImplementedException();
		}

		public override TreeNode ToTreeNode() {
			throw new NotImplementedException();
		}
	}
}
