using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapExportSettings : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapExportSettings(GBMObjectMap Master, UInt16 TypeID, UInt16 UniqueID, UInt16? MasterID, UInt32 Size, Stream stream)
			: base(Master, TypeID, UniqueID, MasterID, Size, stream) { }

		public GBMObjectMapExportSettings(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		/// <summary>
		/// The name of the file (256 chars)
		/// </summary>
		public String FileName { get; set; }
		/// <summary>
		/// The type of the file.
		/// 
		/// TODO: Enum?
		/// </summary>
		public byte FileType { get; set; }
		/// <summary>
		/// The name of the section.
		/// </summary>
		public String SectionName { get; set; }
		/// <summary>
		/// The label.
		/// </summary>
		public String LabelName { get; set; }
		/// <summary>
		/// The bank used.
		/// </summary>
		public byte Bank { get; set; }
		/// <summary>
		/// The number of planes.
		/// TODO: Enum
		/// </summary>
		public UInt16 PlaneCount { get; set; }
		/// <summary>
		/// The order of the planes.
		/// TODO: Enum
		/// </summary>
		public UInt16 PlaneOrder { get; set; }
		/// <summary>
		/// The layout of the map.
		/// TODO: Enum
		/// </summary>
		public UInt16 MapLayout { get; set; }
		/// <summary>
		/// Whether or not to split the tiles.
		/// </summary>
		public bool Split { get; set; }
		/// <summary>
		/// The size of each split block.
		/// </summary>
		public UInt32 SplitSize { get; set; }
		/// <summary>
		/// Should each split block start on a new bank?
		/// </summary>
		public bool ChangeBankEachSplit { get; set; }
		/// <summary>
		/// The selected tab.
		/// </summary>
		public byte SelTab { get; set; }
		private UInt16 exportPropCount;
		/// <summary>
		/// The number of properties.
		/// </summary>
		public UInt16 ExportPropCount {
			get { return exportPropCount; }
			set { exportPropCount = value; if (ExportPropCountChanged != null) { ExportPropCountChanged(this, new EventArgs()); } }
		}
		/// <summary>
		/// The offset to add to each tile number.
		/// </summary>
		/// <remarks>Since: GBMB 1.2</remarks>
		public UInt16 TileOffset { get; set; }

		public event EventHandler ExportPropCountChanged;

		protected override void LoadFromStream(Stream s) {
			this.FileName = s.ReadString(255);
			this.FileType = s.ReadByteEx();
			this.SectionName = s.ReadString(40);
			this.LabelName = s.ReadString(40);
			this.Bank = s.ReadByteEx();
			this.PlaneCount = s.ReadWord();
			this.PlaneOrder = s.ReadWord();
			this.MapLayout = s.ReadWord();
			this.Split = s.ReadBoolean();
			this.SplitSize = s.ReadInteger();
			this.ChangeBankEachSplit = s.ReadBoolean();
			this.SelTab = s.ReadByteEx();
			this.ExportPropCount = s.ReadWord();
			this.TileOffset = s.ReadWord(0);
		}

		protected override void SaveToStream(Stream s) {
			s.WriteString(FileName, 255);
			s.WriteByteEx(FileType);
			s.WriteString(SectionName, 40);
			s.WriteString(LabelName, 40);
			s.WriteByteEx(Bank);
			s.WriteWord(PlaneCount);
			s.WriteWord(PlaneOrder);
			s.WriteWord(MapLayout);
			s.WriteBoolean(Split);
			s.WriteInteger(SplitSize);
			s.WriteBoolean(ChangeBankEachSplit);
			s.WriteByte(SelTab);
			s.WriteWord(ExportPropCount);
			s.WriteWord(TileOffset);
		}

		public override string GetTypeName() {
			return "Map export settings";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			root.Nodes.Add("FileName", "FileName: " + FileName);
			root.Nodes.Add("FileType", "FileType: " + FileType);
			root.Nodes.Add("SectionName", "SectionName: " + SectionName);
			root.Nodes.Add("LabelName", "LabelName: " + LabelName);
			root.Nodes.Add("Bank", "Bank: " + Bank);
			root.Nodes.Add("PlaneCount", "PlaneCount: " + PlaneCount);
			root.Nodes.Add("PlaneOrder", "PlaneOrder: " + PlaneOrder);
			root.Nodes.Add("MapLayout", "MapLayout: " + MapLayout);
			root.Nodes.Add("Split", "Split: " + Split);
			root.Nodes.Add("SplitSize", "SplitSize: " + SplitSize);
			root.Nodes.Add("ChangeBankEachSplit", "ChangeBankEachSplit: " + ChangeBankEachSplit);
			root.Nodes.Add("SelTab", "SelTab: " + SelTab);
			root.Nodes.Add("ExportPropCount", "ExportPropCount: " + ExportPropCount);
			root.Nodes.Add("TileOffset", "TileOffset: " + TileOffset);

			return root;
		}
	}
}
