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
	public class GBRObjectTilePalette : ReferentialGBRObject<GBRObjectTileData>
	{
		public GBRObjectTilePalette(UInt16 UniqueID) : base(UniqueID) {
			this.GBCPalettes = new UInt32[0];
			this.SGBPalettes = new UInt32[0];
		}

		/// <summary>
		/// The Gameboy Color Palette Mapping.
		/// </summary>
		public UInt32[] GBCPalettes { get; set; }
		/// <summary>
		/// The Super Gameboy Palette Mapping.
		/// </summary>
		public UInt32[] SGBPalettes { get; set; }

		protected internal override void SaveToStream(GBRFile file, Stream s) {
			base.SaveToStream(file, s);

			s.WriteWord((UInt16)GBCPalettes.Length);
			for (int i = 0; i < GBCPalettes.Length; i++) {
				s.WriteLong(GBCPalettes[i]);
			}

			s.WriteWord((UInt16)SGBPalettes.Length);
			for (int i = 0; i < SGBPalettes.Length; i++) {
				s.WriteLong(GBCPalettes[i]);
			}
		}

		protected internal override void LoadFromStream(GBRFile file, Stream s) {
			base.LoadFromStream(file, s);

			GBCPalettes = new UInt32[s.ReadWord()];
			for (int i = 0; i < GBCPalettes.Length; i++) {
				GBCPalettes[i] = s.ReadLong();
			}

			SGBPalettes = new UInt32[s.ReadWord()];
			for (int i = 0; i < SGBPalettes.Length; i++) {
				SGBPalettes[i] = s.ReadLong();
			}
		}

		protected internal override void SetupObject(GBRFile file) {
			base.SetupObject(file);

			this.GBCPalettes = new UInt32[ReferedObject.Tiles.Length]; //TODO: When there is an onchanged event for length, use that.
			this.SGBPalettes = new UInt32[ReferedObject.Tiles.Length];
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = base.ToTreeNode();

			TreeNode gbcPal = new TreeNode("GBC Palettes: Length " + GBCPalettes.Length);
			for (int i = 0; i < GBCPalettes.Length; i++) {
				gbcPal.Nodes.Add(i.ToString() + ": " + GBCPalettes[i]);
			}

			TreeNode sgbPal = new TreeNode("SGB Palettes: Length " + SGBPalettes.Length);
			for (int i = 0; i < SGBPalettes.Length; i++) {
				sgbPal.Nodes.Add(i.ToString() + ": " + SGBPalettes[i]);
			}

			node.Nodes.Add(gbcPal);
			node.Nodes.Add(sgbPal);

			return node;
		}
	}
}
