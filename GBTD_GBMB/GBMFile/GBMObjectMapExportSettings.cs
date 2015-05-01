using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

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
		/// </summary>
		public GBMExportFileType FileType { get; set; }
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
		/// </summary>
		public PlaneCount PlaneCount { get; set; }
		/// <summary>
		/// The order of the planes.
		/// </summary>
		public PlaneOrder PlaneOrder { get; set; }
		/// <summary>
		/// The layout of the map.
		/// </summary>
		public MapLayout MapLayout { get; set; }
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
			this.FileType = (GBMExportFileType)s.ReadByteEx();
			this.SectionName = s.ReadString(40);
			this.LabelName = s.ReadString(40);
			this.Bank = s.ReadByteEx();
			this.PlaneCount = (PlaneCount)s.ReadWord();
			this.PlaneOrder = (PlaneOrder)s.ReadWord();
			this.MapLayout = (MapLayout)s.ReadWord();
			this.Split = s.ReadBoolean();
			this.SplitSize = s.ReadInteger();
			this.ChangeBankEachSplit = s.ReadBoolean();
			this.SelTab = s.ReadByteEx();
			this.ExportPropCount = s.ReadWord();
			this.TileOffset = s.ReadWord(0);
		}

		protected override void SaveToStream(Stream s) {
			s.WriteString(FileName, 255);
			s.WriteByteEx((byte)FileType);
			s.WriteString(SectionName, 40);
			s.WriteString(LabelName, 40);
			s.WriteByteEx(Bank);
			s.WriteWord((UInt16)PlaneCount);
			s.WriteWord((UInt16)PlaneOrder);
			s.WriteWord((UInt16)MapLayout);
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

	/// <summary>
	/// Different export file modes.
	/// </summary>
	public enum GBMExportFileType : byte
	{
		RGBDS_Assembly_File = 0,
		RGBDS_Object_File = 1,
		TASM_Assembly_File = 2,
		GBDK_C_File = 3,
		All_Purpose_Binary_File = 4,
		ISAS_Assembly_File = 5
	}

	/// <summary>
	/// Different plane count modes.
	/// </summary>
	public enum PlaneCount : ushort
	{
		Half_Plane = 0,
		One_Plane = 1,
		Two_Planes = 2,
		Three_Planes = 3,
		Four_Planes = 4
	}

	/// <summary>
	/// Different plane order modes.
	/// </summary>
	public enum PlaneOrder : ushort
	{
		Tiles_Are_Continues = 0,
		Planes_Are_Continues = 1
	}

	/// <summary>
	/// The map layouts.
	/// </summary>
	public enum MapLayout : ushort
	{
		Rows = 0,
		Columns = 1
	}

	public static class ExportSettingsEnumExtensions
	{
		/// <summary>
		/// The display name for the given ExportFileType.
		/// </summary>
		public static string GetDisplayName(this GBMExportFileType type) {
			switch (type) {
			case GBMExportFileType.RGBDS_Assembly_File: return "RGBDS Assembly file";
			case GBMExportFileType.RGBDS_Object_File: return "RGBDS Object file";
			case GBMExportFileType.TASM_Assembly_File: return "TASM Assembly file";
			case GBMExportFileType.GBDK_C_File: return "GBDK C file";
			case GBMExportFileType.All_Purpose_Binary_File: return "All-purpose binary file";
			case GBMExportFileType.ISAS_Assembly_File: return "ISAS Assembly file";
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(GBMExportFileType));
			}
		}

		/// <summary>
		/// The display name for the given PlaneCount.
		/// </summary>
		public static string GetDisplayName(this PlaneCount type) {
			switch (type) {
			case PlaneCount.Half_Plane: return "0.5 plane (4 bits)";
			case PlaneCount.One_Plane: return "1 plane (8 bits)";
			case PlaneCount.Two_Planes: return "2 planes (16 bits)";
			case PlaneCount.Three_Planes: return "3 planes (24 bits)";
			case PlaneCount.Four_Planes: return "4 planes (32 bits)";
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(PlaneCount));
			}
		}

		/// <summary>
		/// The display name for the given PlaneOrder.
		/// </summary>
		public static string GetDisplayName(this PlaneOrder type) {
			switch (type) {
			case PlaneOrder.Tiles_Are_Continues: return "Tiles are continues";
			case PlaneOrder.Planes_Are_Continues: return "Planes are continues";
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(PlaneOrder));
			}
		}

		/// <summary>
		/// The file extension used by the main file of the given ExportFileType.
		/// </summary>
		public static string GetExtension(this GBMExportFileType type) {
			switch (type) {
			case GBMExportFileType.RGBDS_Assembly_File: return ".z80";
			case GBMExportFileType.RGBDS_Object_File: return ".obj";
			case GBMExportFileType.TASM_Assembly_File: return ".z80";
			case GBMExportFileType.GBDK_C_File: return ".c";
			case GBMExportFileType.All_Purpose_Binary_File: return ".bin";
			case GBMExportFileType.ISAS_Assembly_File: return ".s";
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(GBMExportFileType));
			}
		}

		/// <summary>
		/// The file extension used by the include file of the given ExportFileType.  If there is no such extension, an exception is thrown.
		/// </summary>
		public static string GetIncludeExtension(this GBMExportFileType type) {
			switch (type) {
			case GBMExportFileType.RGBDS_Assembly_File: return ".inc";
			case GBMExportFileType.RGBDS_Object_File: return ".inc";
			case GBMExportFileType.TASM_Assembly_File: throw new InvalidOperationException(type + " does not have an include file extension!");
			case GBMExportFileType.GBDK_C_File: return ".h";
			case GBMExportFileType.All_Purpose_Binary_File: throw new InvalidOperationException(type + " does not have an include file extension!");
			case GBMExportFileType.ISAS_Assembly_File: return ".inc";
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(GBMExportFileType));
			}
		}

		/// <summary>
		/// Whether or not the specified ExportFileType supports setting the bank and section in the exported file.
		/// </summary>
		public static bool SupportsBankAndSection(this GBMExportFileType type) {
			switch (type) {
			case GBMExportFileType.RGBDS_Assembly_File: return true;
			case GBMExportFileType.RGBDS_Object_File: return true;
			case GBMExportFileType.TASM_Assembly_File: return false;
			case GBMExportFileType.GBDK_C_File: return false; //It may be possible to use banks but I don't know how yet.
			case GBMExportFileType.All_Purpose_Binary_File: return false;
			case GBMExportFileType.ISAS_Assembly_File: return true;
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(GBMExportFileType));
			}
		}

		/// <summary>
		/// Whether or not the specified ExportFileType supports a custom label in the exported file.
		/// </summary>
		public static bool SupportsLabel(this GBMExportFileType type) {
			switch (type) {
			case GBMExportFileType.RGBDS_Assembly_File: return true;
			case GBMExportFileType.RGBDS_Object_File: return true;
			case GBMExportFileType.TASM_Assembly_File: return true;
			case GBMExportFileType.GBDK_C_File: return true;
			case GBMExportFileType.All_Purpose_Binary_File: return false;
			case GBMExportFileType.ISAS_Assembly_File: return true;
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(GBMExportFileType));
			}
		}

		/// <summary>
		/// Gets the number of planes that the given planecount contians.
		/// </summary>
		public static int GetNumberOfPlanes(this PlaneCount planeCount) {
			switch (planeCount) {
			case PlaneCount.Half_Plane: return 1;
			case PlaneCount.One_Plane: return 1;
			case PlaneCount.Two_Planes: return 2;
			case PlaneCount.Three_Planes: return 3;
			case PlaneCount.Four_Planes: return 4;
			default: throw new InvalidEnumArgumentException("planeCount", (int)planeCount, typeof(PlaneCount));
			}
		}
	}
}
