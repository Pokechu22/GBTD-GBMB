using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// This object contains basic information about a map, and is the master for various Objects contains the actual data.
	/// <para>This does not (appear to) store the actual tiles; instead it stores settings.</para>
	/// </summary>
	public class GBMObjectMap : GBMObject
	{
		public GBMObjectMap(GBMObject Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
			: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }
		public GBMObjectMap(GBMObject Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		/// <summary>
		/// The name of the map (currently ignored)
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// The width of the map.
		/// </summary>
		public UInt32 Width { get; set; }
		/// <summary>
		/// The height of the map.
		/// </summary>
		public UInt32 Height { get; set; }
		/// <summary>
		/// The number of properties.
		/// </summary>
		public UInt32 PropCount { get; set; }
		/// <summary>
		/// The name of the GBR file.
		/// </summary>
		public string TileFile { get; set; }
		/// <summary>
		/// The number of tiles in the GBR file.
		/// <para>
		/// Note from the specs: 
		/// TileCount does not actually refer to the number of tiles in the GBR-file, as this can be easily changed without the 
		/// GBM-file “knowing” about it. It is used to determine the size of tile-related information in the GBM-file, 
		/// mainly the default properties.
		/// </para>
		/// </summary>
		public UInt32 TileCount { get; set; }
		/// <summary>
		/// Number of property colors.
		/// </summary>
		public UInt32 PropColorCount { get; set; }

		protected override void SaveToStream(Stream s) {
			s.WriteString(Name, 128);
			s.WriteInteger(Width);
			s.WriteInteger(Height);
			s.WriteInteger(PropCount);
			s.WriteString(TileFile, 256);
			s.WriteInteger(TileCount);
			s.WriteInteger(PropCount);
		}

		protected override void LoadFromStream(Stream s) {
			Name = s.ReadString(128);
			Width = s.ReadInteger();
			Height = s.ReadInteger();
			PropCount = s.ReadInteger();
			TileFile = s.ReadString(256);
			TileCount = s.ReadInteger();
			PropCount = s.ReadInteger();
		}

		public override string GetTypeName() {
			return "Map";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			root.Nodes.Add("Name", "Name: " + Name);
			root.Nodes.Add("Width", "Width: " + Width);
			root.Nodes.Add("Height", "Height: " + Height);
			root.Nodes.Add("PropCount", "PropCount: " + PropCount);
			root.Nodes.Add("TileFile", "TileFile: " + TileFile);
			root.Nodes.Add("TileCount", "TileCount: " + TileCount);
			root.Nodes.Add("PropColorCount", "PropColorCount: " + PropColorCount);

			return root;
		}
	}
}
