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

		protected override void SaveToStream(Stream s) {
			throw new NotImplementedException();
		}

		protected override void LoadFromStream(Stream s) {
			throw new NotImplementedException();
		}

		public override string GetTypeName() {
			throw new NotImplementedException();
		}

		public override TreeNode ToTreeNode() {
			throw new NotImplementedException();
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
