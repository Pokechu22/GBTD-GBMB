﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// A list of names and types of all properties.
	/// </summary>
	public class GBMObjectMapProperties : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapProperties(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
				: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectMapProperties(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		public class GBMObjectMapPropertiesRecord
		{
			/// <summary>
			/// The type of the property, currently only 0x00000000.
			/// This value is currently ignored durring deserialization, because there's only one type.
			/// </summary>
			public UInt32 Type { get; set; }
			/// <summary>
			/// The size of the property, currently only 2.
			/// This value is currently ignored durring deserialization, because there's only one type.
			/// </summary>
			public UInt32 Size { get; set; }
			/// <summary>
			/// The name of the property.
			/// </summary>
			public String Name { get; set; }

			public GBMObjectMapPropertiesRecord(Stream s) {
				this.Type = s.ReadInteger();
				this.Size = s.ReadInteger();
				this.Name = s.ReadString(32);

				//Validation that this is of a recognised type and size.
				if (Type != 0x00000000) {
					throw new Exception("Unrecognized map property type: " + Type.ToString("X8"));
				}
				if (Size != 2) {
					throw new Exception("Unrecognized property size: " + Type + " (of type " + Type.ToString("X8") + ")");
				}
			}

			public void SaveToStream(Stream s) {
				s.WriteInteger(Type);
				s.WriteInteger(Size);
				s.WriteString(Name, 32);
			}

			public TreeNode ToTreeNode(string name) {
				TreeNode node = new TreeNode(name);

				node.Nodes.Add("Type", "Type: " + Type.ToString("X8"));
				node.Nodes.Add("Size", "Size: " + Size);
				node.Nodes.Add("Name", "Name: " + Name);

				return node;
			}
		}

		public GBMObjectMapPropertiesRecord[] Properties { get; set; }

		protected override void LoadFromStream(Stream s) {
			this.Properties = new GBMObjectMapPropertiesRecord[Master.PropCount];

			for (int i = 0; i < Properties.Length; i++) {
				Properties[i] = new GBMObjectMapPropertiesRecord(s);
			}
		}

		protected override void SaveToStream(Stream s) {
			for (int i = 0; i < Properties.Length; i++) {
				Properties[i].SaveToStream(s);
			}
		}

		public override string GetTypeName() {
			return "Map properties list";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			for (int i = 0; i < Properties.Length; i++) {
				root.Nodes.Add(Properties[i].ToTreeNode("Property " + i));
			}

			return root;
		}
	}
}
