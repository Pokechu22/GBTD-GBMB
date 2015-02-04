using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Represents the palette *mappings* for a group of tiles.
	/// </summary>
	public class GBRObjectTilePalette : GBRObject
	{
		public GBRObjectTilePalette(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRObjectTilePalette(GBRObjectHeader header, Stream stream) : base(header, stream) { }

		/// <summary>
		/// The ObjectID of the object that is refered to by this object (Usually a TileData).
		/// <para>TODO: Give direct access to that object (but it would need a reference to the total file for that...)</para>
		/// </summary>
		public UInt16 ReferedObjectID;

		/// <summary>
		/// The Gameboy Color Palette Mapping.
		/// </summary>
		public UInt32[] GBCPalettes;
		/// <summary>
		/// The Super Gameboy Palette Mapping.
		/// </summary>
		public UInt32[] SGBPalettes;

		protected override void SaveToStream(Stream s) {
			s.WriteWord(ReferedObjectID);

			s.WriteWord((UInt16)GBCPalettes.Length);
			for (int i = 0; i < GBCPalettes.Length; i++) {
				s.WriteLong(GBCPalettes[i]);
			}

			s.WriteWord((UInt16)SGBPalettes.Length);
			for (int i = 0; i < SGBPalettes.Length; i++) {
				s.WriteLong(GBCPalettes[i]);
			}
		}

		protected override void LoadFromStream(Stream s) {
			this.ReferedObjectID = s.ReadWord();

			GBCPalettes = new UInt32[s.ReadWord()];
			for (int i = 0; i < GBCPalettes.Length; i++) {
				GBCPalettes[i] = s.ReadLong();
			}

			SGBPalettes = new UInt32[s.ReadWord()];
			for (int i = 0; i < SGBPalettes.Length; i++) {
				SGBPalettes[i] = s.ReadLong();
			}
		}

		public override string GetTypeName() {
			return "Tile-Palette Mapping";
		}

		public override TreeNode ToTreeNode() {
			TreeNode returned = new TreeNode(GetTreeNodeText());

			returned.Nodes.Add("Corresponding Object: " + this.ReferedObjectID.ToString("X4"));

			TreeNode gbcPal = new TreeNode("GBC Palettes: Length " + GBCPalettes.Length);
			for (int i = 0; i < GBCPalettes.Length; i++) {
				gbcPal.Nodes.Add(i.ToString() + ": " + GBCPalettes[i]);
			}

			TreeNode sgbPal = new TreeNode("SGB Palettes: Length " + SGBPalettes.Length);
			for (int i = 0; i < SGBPalettes.Length; i++) {
				sgbPal.Nodes.Add(i.ToString() + ": " + SGBPalettes[i]);
			}

			returned.Nodes.Add(gbcPal);
			returned.Nodes.Add(sgbPal);

			return returned;
		}
	}
}
