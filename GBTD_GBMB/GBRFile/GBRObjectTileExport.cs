using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBRFile
{
	class GBRObjectTileExport : GBRObject
	{
		public GBRObjectTileExport(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRObjectTileExport(GBRObjectHeader header, Stream stream) : base(header, stream) { }

		/// <summary>
		/// The coresponding object ID.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 CorrespondingID { get; set; }
		/// <summary>
		/// The name of the file to export to.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public string FileName { get; set; }
		/// <summary>
		/// The type of the file.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ExportFileType FileType { get; set; }
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
		/// Whether or not GBCompression should be used.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ExportCompressionMode UseCompression { get; set; }
		/// <summary>
		/// Whether or not palette colors should be included.
		/// </summary>
		/// <remarks>Since: GBTD 1.3</remarks>
		public bool IncludeColors { get; set; }
		/// <summary>
		/// The mode used for SGB palettes, if they are enabled.
		/// </summary>
		/// <remarks>Since: GBTD 1.4</remarks>
		public ExportPaletteMode SGBPalMode { get; set; }
		/// <summary>
		/// The mode used for GBC palettes, if they are enabled.
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

		protected override void SaveToStream(Stream s) {
			s.WriteWord(CorrespondingID);
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
			s.WriteByte((byte)UseCompression);
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

		protected override void LoadFromStream(Stream s) {
			this.CorrespondingID = s.ReadWord();
			this.FileName = s.ReadString(128);
			this.FileType = (ExportFileType)s.ReadByte();
			this.SectionName = s.ReadString(20);
			this.LabelName = s.ReadString(20);
			this.Bank = s.ReadByteEx();
			this.StoreTilesInArray = s.ReadBool();
			this.Format = (ExportFormat)s.ReadByte();
			this.CounterType = (ExportCounterType)s.ReadByte();
			this.FromTile = s.ReadWord();
			this.ToTile = s.ReadWord();
			this.UseCompression = (ExportCompressionMode)s.ReadByte();
			this.IncludeColors = s.ReadBool(false);
			this.SGBPalMode = (ExportPaletteMode)s.ReadByte((byte)ExportPaletteMode.None);
			this.GBCPalMode = (ExportPaletteMode)s.ReadByte((byte)ExportPaletteMode.None);
			this.MakeMetaTiles = s.ReadBool(false);
			this.MetaTileOffset = s.ReadLong(0);
			this.MetaCounterFormat = (ExportCounterType)s.ReadByte((byte)ExportPaletteMode.None);
			this.Split = s.ReadBool(false);
			this.BlockSize = s.ReadLong(0);
			this.SelectedTab = s.ReadByte(0);
		}

		public override string GetTypeName() {
			return "Tile export settings";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			root.Nodes.Add("CorrespondingID", "CorrespondingID: " + CorrespondingID);
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
			root.Nodes.Add("UseCompression", "UseCompression: " + UseCompression);
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

	public enum ExportFileType : byte
	{
		RGBDSAssemblyFile = 0,
		RGBDSObjectFile = 1,
		TASMAssemblyFile = 2,
		GBDKCFile = 3,
		BinaryFile = 4
	}

	public enum ExportFormat : byte
	{
		//TODO this might not be right
		GameBoy4Color = 0, 
		GameBoy2Color = 1,
		BytePerColor = 3,
		//Where's consecutive 4 color?
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
}
