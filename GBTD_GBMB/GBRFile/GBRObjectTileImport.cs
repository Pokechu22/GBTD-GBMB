using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBRFile
{
	public class GBRObjectTileImport : ReferentialGBRObject
	{
		public GBRObjectTileImport(GBRObjectHeader header, Stream stream) : base(header, stream) { }

		/// <summary>
		/// The coresponding object ID.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public override UInt16 ReferedObjectID { get; set; }
		/// <summary>
		/// The name of the file to export to.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public string FileName { get; set; }
		/// <summary>
		/// The type of the file.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ImportFileType FileType { get; set; }
		/// <summary>
		/// The first tile from a GBE file to import.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 FirstImportFileTile { get; set; }
		/// <summary>
		/// The first tile to put that tile into in the current app.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 FirstProgramTile { get; set; }
		/// <summary>
		/// The number of tiles to import.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public UInt16 TileCount { get; set; }
		/// <summary>
		/// The color conversion behavior.
		/// </summary>
		/// <remarks>Since: Initial version</remarks>
		public ImportColorConversion ColorConversion { get; set; }
		/// <summary>
		/// The first byte in the binary file.
		/// </summary>
		/// <remarks>Since: GBTD 1.5</remarks>
		public UInt32 FirstByte { get; set; }
		/// <summary>
		/// The format of the binary file type.
		/// </summary>
		/// <remarks>Since: GBTD 1.5</remarks>
		public ImportBinaryFileFormat BinaryFileFormat { get; set; }

		protected override void SaveToStream(Stream s) {
			s.WriteWord(ReferedObjectID);
			s.WriteString(FileName, 128);
			s.WriteByte((byte)FileType);
			s.WriteWord(FirstImportFileTile);
			s.WriteWord(FirstProgramTile);
			s.WriteWord(TileCount);
			s.WriteByte((byte)ColorConversion);
			s.WriteLong(FirstByte);
			s.WriteByte((byte)BinaryFileFormat);
		}

		protected override void LoadFromStream(Stream s) {
			this.ReferedObjectID = s.ReadWord();
			this.FileName = s.ReadString(128);
			this.FileType = (ImportFileType)s.ReadByteEx();
			this.FirstImportFileTile = s.ReadWord();
			this.FirstProgramTile = s.ReadWord();
			this.TileCount = s.ReadWord();
			this.ColorConversion = (ImportColorConversion)s.ReadByteEx();
			this.FirstByte = s.ReadLong(0);
			this.BinaryFileFormat = (ImportBinaryFileFormat)s.ReadByte((byte)ImportBinaryFileFormat.BytePerPixel);
		}

		public override string GetTypeName() {
			return "Tile Import Settings";
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = CreateRootTreeNode();

			node.Nodes.Add("ReferedObjectID", "ReferedObjectID: " + ReferedObjectID);
			TreeNode fileName = new TreeNode("FileName");
			fileName.Nodes.Add(FileName);
			node.Nodes.Add(fileName);
			node.Nodes.Add("FileType", "FileType: " + FileType);
			node.Nodes.Add("FirstImportFileTile", "FirstImportFileTile: " + FirstImportFileTile);
			node.Nodes.Add("FirstProgramTile", "FirstProgramTile: " + FirstProgramTile);
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
	public enum ImportFileType : byte
	{
		GBEFile = 0,
		Binary8x8 = 1
	}

	/// <summary>
	/// Valid values for ColorConversion.
	/// </summary>
	public enum ImportColorConversion : byte
	{
		ByColors = 0,
		ByIndex = 1
	}

	/// <summary>
	/// Valid values for FileType.
	/// </summary>
	public enum ImportBinaryFileFormat : byte
	{
		BytePerPixel = 0,
		TwoBitsPerPixel = 1,
		GameboyVRAM = 2
	}
}
