using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// Represents a record used by <see cref="GBMObjectMapExportProperties"/>.
	/// </summary>
	public class GBMObjectMapExportPropertiesRecord
	{
		/// <summary>
		/// The property used.
		/// 
		/// TODO: I have no idea what values would be here.  Enum... mabye?
		/// </summary>
		public UInt32 Property { get; set; }
		/// <summary>
		/// The size of the property, in bits(?)
		/// </summary>
		public UInt32 Size { get; set; }

		public GBMObjectMapExportPropertiesRecord(Stream s) {
			this.Property = s.ReadInteger();
			this.Size = s.ReadInteger();
		}

		public void SaveToStream(Stream s) {
			s.WriteInteger(Property);
			s.WriteInteger(Size);
		}

		public TreeNode ToTreeNode(string name) {
			TreeNode node = new TreeNode(name);

			node.Nodes.Add("Property", "Property: " + Property);
			node.Nodes.Add("Value", "Value: " + Size);

			return node;
		}
	}
}
