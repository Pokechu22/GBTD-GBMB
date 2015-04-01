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
		/// The value is based off of the position in the dropdown.
		/// 
		/// Internally, the file writes this as a UInt32.  But it only processes the first byte, and the rest is garbage data.  Irritating.
		/// </summary>
		public byte Property { get; set; }
		/// <summary>
		/// The size of the property, in bits(?)
		/// </summary>
		public UInt32 Size { get; set; }

		/// <summary>
		/// Creates a GBMObjectMapExportPropertiesRecord with the default values.
		/// </summary>
		public GBMObjectMapExportPropertiesRecord() {
			this.Property = 0;
			this.Size = 0;
		}

		/// <summary>
		/// Deserializes the GBMObjectMapExportPropertiesRecord from the given stream.
		/// </summary>
		public GBMObjectMapExportPropertiesRecord(Stream s) {
			//Intentional - this is a byte stored as an integer.  Yes, I have no idea how that happened.  But it's garbage data beyond the byte.
			//Formats are weird.
			this.Property = (byte)(s.ReadInteger() & 0xff);
			this.Size = s.ReadInteger();
		}

		public void SaveToStream(Stream s) {
			//Intentional - this is a byte stored as an integer.  Yes, I have no idea how that happened.  But it's garbage data beyond the byte.
			//Formats are weird.
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
