using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace GB.Shared.GBRFile
{
	public class GBRObjectTileExport : ReferentialGBRObject<GBRObjectTileData>
	{
		public GBRObjectTileExport(UInt16 UniqueID) : base(UniqueID) {
			this.FileName = "";
			this.FileType = GBRExportFileType.RGBDSAssemblyFile;
			this.SectionName = "";
			this.LabelName = "";
			this.Bank = 0;
			this.StoreTilesInArray = true;
			this.Format = ExportFormat.GameBoy4Color;
			this.CounterType = ExportCounterType.None;
			this.FromTile = 0;
			this.ToTile = 0;
			this.CompressionType = ExportCompressionMode.None;
			this.IncludeColors = false;
			this.SGBPalMode = ExportPaletteMode.None;
			this.GBCPalMode = ExportPaletteMode.None;
			this.MakeMetaTiles = false;
			this.MetaTileOffset = 0;
			this.MetaCounterFormat = ExportCounterType.None;
			this.Split = false;
			this.BlockSize = 0;
			this.SelectedTab = 0;
		}

		/// <summary>
		/// The name of the file to export to.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public string FileName { get; set; }
		/// <summary>
		/// The type of the file.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public GBRExportFileType FileType { get; set; }
		/// <summary>
		/// The name of the section (when using ASM).
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public string SectionName { get; set; }
		/// <summary>
		/// The name of the label / identifier to use.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public string LabelName { get; set; }
		/// <summary>
		/// The bank to store it in.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public byte Bank { get; set; }
		/// <summary>
		/// Store the tiles in an array?
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public bool StoreTilesInArray { get; set; }
		/// <summary>
		/// The format to use for the tile data.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ExportFormat Format { get; set; }
		/// <summary>
		/// The type of counter to use.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ExportCounterType CounterType { get; set; }
		/// <summary>
		/// The first tile to export.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 FromTile { get; set; }
		/// <summary>
		/// The last tile to export.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 ToTile { get; set; }
		/// <summary>
		/// What type of compression to use.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ExportCompressionMode CompressionType { get; set; }
		/// <summary>
		/// Whether or not the *global* palette should be included.
		/// </summary>
		/// <remarks>Since: GBTD 1.3</remarks>
		public bool IncludeColors { get; set; }
		/// <summary>
		/// Whether or not an array continaing the per-tile SGB palettes should be included.
		/// </summary>
		/// <remarks>Since: GBTD 1.4</remarks>
		public ExportPaletteMode SGBPalMode { get; set; }
		/// <summary>
		/// Whether or not an array continaing the per-tile GBC palettes should be included.
		/// </summary>
		/// <remarks>Since: GBTD 1.4</remarks>
		public ExportPaletteMode GBCPalMode { get; set; }
		/// <summary>
		/// Whether or not meta tiles should be generated.
		/// </summary>
		/// <remarks>Since: GBTD 1.5</remarks>
		public bool MakeMetaTiles { get; set; }
		/// <summary>
		/// The offset for meta tiles.
		/// </summary>
		/// <remarks>Since: GBTD 1.5</remarks>
		public UInt32 MetaTileOffset { get; set; }
		/// <summary>
		/// The format used for the MetaTile counter.
		/// </summary>
		/// <remarks>Since: GBTD 1.5</remarks>
		public ExportCounterType MetaCounterFormat { get; set; }
		/// <summary>
		/// Whether data should be split.
		/// </summary>
		/// <remarks>Since: GBTD 1.8</remarks>
		public bool Split { get; set; }
		/// <summary>
		/// The size to split blocks into.
		/// </summary>
		/// <remarks>Since: GBTD 1.8</remarks>
		public UInt32 BlockSize { get; set; }
		/// <summary>
		/// The currently selected tab.
		/// A value of 0 is "Standard", 1 is "Advanced".
		/// </summary>
		/// <remarks>Since: GBTD 1.8</remarks>
		public byte SelectedTab { get; set; }

		protected internal override void SaveToStream(GBRFile file, Stream s) {
			base.SaveToStream(file, s);

			s.WriteString(FileName, 128);
			s.WriteByte((byte)FileType);
			s.WriteString(SectionName, 20);
			s.WriteString(LabelName, 20);
			s.WriteByte(Bank);
			s.WriteBool(StoreTilesInArray);
			s.WriteByte((byte)Format);
			s.WriteByte((byte)CounterType);
			s.WriteWord(FromTile);
			s.WriteWord(ToTile);
			s.WriteByte((byte)CompressionType);
			s.WriteBool(IncludeColors);
			s.WriteByte((byte)SGBPalMode);
			s.WriteByte((byte)GBCPalMode);
			s.WriteBool(MakeMetaTiles);
			s.WriteLong(MetaTileOffset);
			s.WriteByte((byte)MetaCounterFormat);
			s.WriteBool(Split);
			s.WriteLong(BlockSize);
			s.WriteByte(SelectedTab);
		}

		protected internal override void LoadFromStream(GBRFile file, Stream s) {
			base.LoadFromStream(file, s);

			this.FileName = s.ReadString(128);
			this.FileType = (GBRExportFileType)s.ReadByte();
			this.SectionName = s.ReadString(20);
			this.LabelName = s.ReadString(20);
			this.Bank = s.ReadByteEx();
			this.StoreTilesInArray = s.ReadBool();
			this.Format = (ExportFormat)s.ReadByte();
			this.CounterType = (ExportCounterType)s.ReadByte();
			this.FromTile = s.ReadWord();
			this.ToTile = s.ReadWord();
			this.CompressionType = (ExportCompressionMode)s.ReadByte();
			this.IncludeColors = s.ReadBool(false);
			this.SGBPalMode = (ExportPaletteMode)s.ReadByte((byte)ExportPaletteMode.None);
			this.GBCPalMode = (ExportPaletteMode)s.ReadByte((byte)ExportPaletteMode.None);
			this.MakeMetaTiles = s.ReadBool(false);
			this.MetaTileOffset = s.ReadLong(0);
			this.MetaCounterFormat = (ExportCounterType)s.ReadByte((byte)ExportCounterType.None);
			this.Split = s.ReadBool(false);
			this.BlockSize = s.ReadLong(0);
			this.SelectedTab = s.ReadByte(0);
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = base.ToTreeNode();

			TreeNode fileName = new TreeNode("File name");
			fileName.Nodes.Add(FileName);
			root.Nodes.Add(fileName);
			root.Nodes.Add("FileType", "FileType: " + FileType);
			TreeNode sectionName = new TreeNode("Section name");
			sectionName.Nodes.Add(SectionName);
			root.Nodes.Add(sectionName);
			TreeNode labelName = new TreeNode("Label name");
			labelName.Nodes.Add(LabelName);
			root.Nodes.Add(labelName);
			root.Nodes.Add("Bank", "Bank: " + Bank);
			root.Nodes.Add("TilesArray", "StoreTilesInArray: " + StoreTilesInArray);
			root.Nodes.Add("Format", "Format: " + Format);
			root.Nodes.Add("Counter", "Counter: " + CounterType);
			root.Nodes.Add("FromTile", "FromTile: " + FromTile);
			root.Nodes.Add("ToTile", "ToTile: " + ToTile);
			root.Nodes.Add("CompressionType", "CompressionType: " + CompressionType);
			root.Nodes.Add("IncludeColors", "IncludeColors: " + IncludeColors);
			root.Nodes.Add("SGBPalettes", "SGB Palette Mode: " + SGBPalMode);
			root.Nodes.Add("GBCPalettes", "GBC Palette Mode: " + GBCPalMode);
			root.Nodes.Add("MakeMetaTiles", "MakeMetaTiles: " + MakeMetaTiles);
			root.Nodes.Add("MetaTileOffset", "MetaTileOffset: " + MetaTileOffset);
			root.Nodes.Add("MetaCounterFormat", "Meta Tile Counter Format: " + MetaCounterFormat);
			root.Nodes.Add("Split", "Split: " + Split);
			root.Nodes.Add("BlockSize", "BlockSize: " + BlockSize);
			root.Nodes.Add("SelectedTab", "SelectedTab: " + SelectedTab);
			return root;
		}
	}

	public enum GBRExportFileType : byte
	{
		RGBDSAssemblyFile = 0,
		RGBDSObjectFile = 1,
		TASMAssemblyFile = 2,
		GBDKCFile = 3,
		BinaryFile = 4,
		ISASAssemblyFile = 5
	}

	public enum ExportFormat : byte
	{
		GameBoy4Color = 0, 
		GameBoy2Color = 1,
		BytePerColor = 2,
		ConsecutiveFourColor = 3
	}

	public enum ExportCounterType : byte
	{
		None = 0,
		ByteCountAsByte = 1,
		ByteCountAsWord = 2,
		ByteCountAsConstant = 3,
		TileCountAsByte = 4,
		TileCountAsWord = 5,
		TileCountAsConstant = 6,
		_8x8CountAsByte = 7,
		_8x8CountAsWord = 8,
		_8x8CountAsConstant = 9
	}

	public enum ExportCompressionMode : byte
	{
		None = 0,
		GBCompress = 1
	}

	public enum ExportPaletteMode : byte
	{
		None = 0,
		ConstantPerEntry = 1,
		_2BitsPerEntry = 2,
		_4BitsPerEntry = 3,
		_1BytePerEntry = 4
	}

	public static class GBRExportEnumExtensions
	{
		/// <summary>
		/// Gets the display string for a GBRExportFileType.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static String GetDisplayName(this GBRExportFileType fileType) {
			switch (fileType) {
			case GBRExportFileType.RGBDSAssemblyFile: return "RGBDS Assembly file";
			case GBRExportFileType.RGBDSObjectFile: return "RGBDS Object file";
			case GBRExportFileType.TASMAssemblyFile: return "TASM Assembly file";
			case GBRExportFileType.GBDKCFile: return "GBDK C file";
			case GBRExportFileType.BinaryFile: return "All-purpose binary file";
			case GBRExportFileType.ISASAssemblyFile: return "ISAS Assembly file";
			default: throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(GBRExportFileType));
			}
		}

		/// <summary>
		/// Gets the main file extension for a GBRExportFileType.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static String GetMainExtension(this GBRExportFileType fileType) {
			switch (fileType) {
			case GBRExportFileType.RGBDSAssemblyFile: return "z80";
			case GBRExportFileType.RGBDSObjectFile: return "obj";
			case GBRExportFileType.TASMAssemblyFile: return "z80";
			case GBRExportFileType.GBDKCFile: return "c";
			case GBRExportFileType.BinaryFile: return "bin";
			case GBRExportFileType.ISASAssemblyFile: return "s";
			default: throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(GBRExportFileType));
			}
		}

		/// <summary>
		/// Gets the include file extension for a GBRExportFileType.
		/// </summary>
		/// <param name="fileType"></param>
		/// <returns></returns>
		public static String GetIncludeExtension(this GBRExportFileType fileType) {
			switch (fileType) {
			case GBRExportFileType.RGBDSAssemblyFile: return "inc";
			case GBRExportFileType.RGBDSObjectFile: return "inc";
			case GBRExportFileType.TASMAssemblyFile: throw new InvalidOperationException();
			case GBRExportFileType.GBDKCFile: return "h";
			case GBRExportFileType.BinaryFile: throw new InvalidOperationException();
			case GBRExportFileType.ISASAssemblyFile: return "inc";
			default: throw new InvalidEnumArgumentException("fileType", (int)fileType, typeof(GBRExportFileType));
			}
		}

		public static String GetDisplayString(this ExportFormat format) {
			switch (format) {
			case ExportFormat.GameBoy4Color: return "Gameboy 4 color";
			case ExportFormat.GameBoy2Color: return "Gameboy 2 color";
			case ExportFormat.BytePerColor: return "Byte per color";
			case ExportFormat.ConsecutiveFourColor: return "Consecutive 4 color";
			default: throw new InvalidEnumArgumentException("format", (int)format, typeof(ExportFormat));
			}
		}

		public static String GetDisplayString(this ExportCounterType type) {
			switch (type) {
			case ExportCounterType.None: return "None";
			case ExportCounterType.ByteCountAsByte: return "Byte-count as Byte";
			case ExportCounterType.ByteCountAsWord: return "Byte-count as Word";
			case ExportCounterType.ByteCountAsConstant: return "Byte-count as Constant";
			case ExportCounterType.TileCountAsByte: return "Tile-count as Byte";
			case ExportCounterType.TileCountAsWord: return "Tile-count as Word";
			case ExportCounterType.TileCountAsConstant: return "Tile-count as Constant";
			case ExportCounterType._8x8CountAsByte: return "8x8-count as Byte";
			case ExportCounterType._8x8CountAsWord: return "8x8-count as Word";
			case ExportCounterType._8x8CountAsConstant: return "8x8-count as Constant";
			default: throw new InvalidEnumArgumentException("type", (int)type, typeof(ExportCounterType));
			}
		}

		public static String GetDisplayString(this ExportCompressionMode mode) {
			switch (mode) {
			case ExportCompressionMode.None: return "None";
			case ExportCompressionMode.GBCompress: return "GB-Compress";
			default: throw new InvalidEnumArgumentException("mode", (int)mode, typeof(ExportCompressionMode));
			}
		}

		public static String GetDisplayString(this ExportPaletteMode mode) {
			switch (mode) {
			case ExportPaletteMode.None: return "None";
			case ExportPaletteMode.ConstantPerEntry: return "Constant per entry";
			case ExportPaletteMode._2BitsPerEntry: return "2 Bits per entry";
			case ExportPaletteMode._4BitsPerEntry: return "4 Bits per entry";
			case ExportPaletteMode._1BytePerEntry: return "1 Byte per entry";
			default: throw new InvalidEnumArgumentException("mode", (int)mode, typeof(ExportPaletteMode));
			}
		}
	}
}
