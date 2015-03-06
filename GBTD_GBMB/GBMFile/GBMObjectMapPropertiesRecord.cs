using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// A single record contained within a <see cref="GBMObjectMapProperties"/>
	/// </summary>
	public class GBMObjectMapPropertiesRecord
	{
		/// <summary>
		/// The type of the property.
		/// This value does not appear to be used.
		/// </summary>
		public UInt32 Type { get; set; }
		/// <summary>
		/// The maximum value for the property.
		/// The docs name this "Size", but that seems like a misnomer.
		/// </summary>
		public UInt32 MaxValue { get; set; }
		/// <summary>
		/// The name of the property.
		/// </summary>
		public String Name { get; set; }

		public GBMObjectMapPropertiesRecord(Stream s) {
			this.Type = s.ReadInteger();
			this.MaxValue = s.ReadInteger();
			this.Name = s.ReadString(32);
		}

		public void SaveToStream(Stream s) {
			s.WriteInteger(Type);
			s.WriteInteger(MaxValue);
			s.WriteString(Name, 32);
		}

		public TreeNode ToTreeNode(string name) {
			TreeNode node = new TreeNode(name);

			node.Nodes.Add("Type", "Type: " + Type.ToString("X8"));
			node.Nodes.Add("MaxValue", "MaxValue: " + MaxValue);
			node.Nodes.Add("Name", "Name: " + Name);

			return node;
		}
	}
}
