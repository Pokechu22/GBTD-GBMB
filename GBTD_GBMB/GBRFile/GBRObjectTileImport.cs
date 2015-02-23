using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBRFile
{
	class GBRObjectTileImport : GBRObject
	{
		public GBRObjectTileImport(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRObjectTileImport(GBRObjectHeader header, Stream stream) : base(header, stream) { }

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
		public FileType FileType { get; set; }
		/// <summary>
		/// The first tile from a GBE file to import.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 FromTile { get; set; }
		/// <summary>
		/// The final tile from a GBE file to import.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 ToTile { get; set; }
		/// <summary>
		/// The number of tiles to import.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 TileCount { get; set; }
		/// <summary>
		/// The color conversion behavior.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ColorConversion ColorConversion { get; set; }
		/// <summary>
		/// The first byte in the binary file.
		/// </summary>
		/// <remarks>Since: GBTD 1.5</remarks>
		public UInt32 FirstByte { get; set; }
		/// <summary>
		/// The format of the binary file type.
		/// </summary>
		/// <remarks>Since: GBTD 1.5</remarks>
		public BinaryFileFormat BinaryFileFormat { get; set; }

		protected override void SaveToStream(Stream s) {
			s.WriteWord(CorrespondingID);
			s.WriteString(FileName, 128);
			s.WriteByte((byte)FileType);
			s.WriteWord(FromTile);
			s.WriteWord(ToTile);
			s.WriteWord(TileCount);
			s.WriteByte((byte)ColorConversion);
			s.WriteLong(FirstByte);
			s.WriteByte((byte)BinaryFileFormat);
		}

		protected override void LoadFromStream(Stream s) {
			this.CorrespondingID = s.ReadWord();
			this.FileName = s.ReadString(128);
			this.FileType = (FileType)s.ReadByteEx();
			this.FromTile = s.ReadWord();
			this.ToTile = s.ReadWord();
			this.TileCount = s.ReadWord();
			this.ColorConversion = (ColorConversion)s.ReadByteEx();
			this.FirstByte = s.ReadLong(0);
			this.BinaryFileFormat = (BinaryFileFormat)s.ReadByte((byte)BinaryFileFormat.BytePerPixel);
		}

		public override string GetTypeName() {
			return "Tile Import Settings";
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = CreateRootTreeNode();

			node.Nodes.Add("CorrespondingID", "CorrespondingID: " + CorrespondingID);
			TreeNode fileName = new TreeNode("FileName");
			fileName.Nodes.Add(FileName);
			node.Nodes.Add(fileName);
			node.Nodes.Add("FileType", "FileType: " + FileType);
			node.Nodes.Add("FromTile", "FromTile: " + FromTile);
			node.Nodes.Add("ToTile", "ToTile: " + ToTile);
			node.Nodes.Add("TileCount", "TileCount: " + TileCount);
			node.Nodes.Add("ColorConversion", "ColorConversion: " + ColorConversion);
			node.Nodes.Add("FirstByte", "FirstByte: " + FirstByte);
			node.Nodes.Add("BinaryFileFormat", "BinaryFileFormat: " + BinaryFileFormat);

			return node;
		}
	}

	/// <summary>
	/// Valid values for FileType.
	/// </summary>
	public enum FileType : byte
	{
		GBEFile = 0,
		Binary8x8 = 1
	}

	/// <summary>
	/// Valid values for ColorConversion.
	/// </summary>
	public enum ColorConversion : byte
	{
		ByColors = 0,
		ByIndex = 1
	}

	/// <summary>
	/// Valid values for FileType.
	/// </summary>
	public enum BinaryFileFormat : byte
	{
		BytePerPixel = 0,
		TwoBitsPerPixel = 1,
		GameboyVRAM = 2
	}
}
