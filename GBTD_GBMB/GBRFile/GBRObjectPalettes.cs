using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using GB.Shared.Palettes;
using GB.Shared.Tiles;

namespace GB.Shared.GBRFile
{
	public class GBRObjectPalettes : ReferentialGBRObject<GBRObjectTileData>
	{
		public GBRObjectPalettes(UInt16 UniqueID) : base(UniqueID) {
			//TODO: Set defaults
		}

		/// <summary>
		/// The Gameboy Color palettes.
		/// </summary>
		public PaletteSet GBCPalettes { get; set; }
		/// <summary>
		/// The Super Gameboy palettes.
		/// </summary>
		public PaletteSet SGBPalettes { get; set; }

		protected override void SaveToStream(GBRFile file, Stream s) {
			base.SaveToStream(file, s);
			
			s.WriteWord(GBCPalettes.Size);
			for (int i = 0; i < GBCPalettes.Size; i++) {
				for (int x = 0; x < 4; x++) {
					s.WriteByte(GBCPalettes[i][x].R);
					s.WriteByte(GBCPalettes[i][x].G);
					s.WriteByte(GBCPalettes[i][x].B);
					s.WriteByte(GBCPalettes[i][x].A);
				}
			}
			
			s.WriteWord(SGBPalettes.Size);
			for (int i = 0; i < SGBPalettes.Size; i++) {
				for (int x = 0; x < 4; x++) {
					s.WriteByte(SGBPalettes[i][x].R);
					s.WriteByte(SGBPalettes[i][x].G);
					s.WriteByte(SGBPalettes[i][x].B);
					s.WriteByte(SGBPalettes[i][x].A);
				}
			}
		}

		protected override void LoadFromStream(GBRFile file, Stream s) {
			base.LoadFromStream(file, s);
			
			this.GBCPalettes = new PaletteSet(s.ReadWord());
			for (int i = 0; i < GBCPalettes.Size; i++) {
				for (int x = 0; x < 4; x++) {
					byte[] bytes = new byte[4];

					int read = s.Read(bytes, 0, 4);
					if (read != 4) {
						throw new EndOfStreamException();
					}

					GBCPalettes[i][x] = Color.FromArgb(255, bytes[0], bytes[1], bytes[2]);
				}
			}

			this.SGBPalettes = new PaletteSet(s.ReadWord());
			for (int i = 0; i < SGBPalettes.Size; i++) {
				for (int x = 0; x < 4; x++) {
					byte[] bytes = new byte[4];

					int read = s.Read(bytes, 0, 4);
					if (read != 4) {
						throw new EndOfStreamException();
					}

					SGBPalettes[i][x] = Color.FromArgb(255, bytes[0], bytes[1], bytes[2]);
				}
			}
		}

		public override string GetTypeName() {
			return "Palettes";
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = base.ToTreeNode();
			
			TreeNode GBCPalettesNode = new TreeNode("GBCPalettes (Size: " + GBCPalettes.Size + ")");
			for (int y = 0; y < GBCPalettes.Size; y++) {
				TreeNode t = new TreeNode(y.ToString());
				for (int x = 0; x < 4; x++) {
					TreeNode color = new TreeNode(((GBColor)x) + ": " + GBCPalettes[y][x].ToString());

					color.Nodes.Add("a", "A: " + GBCPalettes[y][x].A);
					color.Nodes.Add("r", "R: " + GBCPalettes[y][x].R);
					color.Nodes.Add("g", "G: " + GBCPalettes[y][x].G);
					color.Nodes.Add("b", "B: " + GBCPalettes[y][x].B);

					if (GBCPalettes[y][x].GetBrightness() < .3) {
						color.ForeColor = Color.White;
					} else {
						color.ForeColor = Color.Black;
					}
					color.BackColor = GBCPalettes[y][x];

					t.Nodes.Add(color);
				}

				GBCPalettesNode.Nodes.Add(t);
			}

			TreeNode SGBPalettesNode = new TreeNode("SGBPalettes (Size: " + SGBPalettes.Size + ")");
			for (int y = 0; y < SGBPalettes.Size; y++) {
				TreeNode t = new TreeNode(y.ToString());
				for (int x = 0; x < 4; x++) {
					TreeNode color = new TreeNode(((GBColor)x) + ": " + SGBPalettes[y][x].ToString());

					color.Nodes.Add("a", "A: " + SGBPalettes[y][x].A);
					color.Nodes.Add("r", "R: " + SGBPalettes[y][x].R);
					color.Nodes.Add("g", "G: " + SGBPalettes[y][x].G);
					color.Nodes.Add("b", "B: " + SGBPalettes[y][x].B);

					if (SGBPalettes[y][x].GetBrightness() < .3) {
						color.ForeColor = Color.White;
					} else {
						color.ForeColor = Color.Black;
					}
					color.BackColor = SGBPalettes[y][x];

					t.Nodes.Add(color);
				}

				SGBPalettesNode.Nodes.Add(t);
			}

			node.Nodes.Add(GBCPalettesNode);
			node.Nodes.Add(SGBPalettesNode);

			return node;
		}
	}
}
