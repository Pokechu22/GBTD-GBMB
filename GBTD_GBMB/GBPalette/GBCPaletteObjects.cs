using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using GB.Shared.Tile;

namespace GB.Shared.Palette
{
	public class GBCPaletteSet : GBPaletteSet<GBCPalette, GBCPaletteEntry> {

		public override int NumberOfRows {
			get { return 8; }
		}

		private GBCPalette[] rows;

		public override GBCPalette[] Rows {
			get {
				return rows;
			}
			set {
				if (value.Length != rows.Length) {
					throw new ArgumentOutOfRangeException("Must match length exactly: Expected " + rows.Length + ", got " + value.Length + ".");
				}
				rows = value;
			}
		}
	}

	public class GBCPalette : GBPalette<GBCPaletteEntry> {
		private GBCPaletteEntry entryWhite, entryLightGray, entryDarkGray, entryBlack;

		public override GBCPaletteEntry EntryWhite {
			get { return entryWhite; }
			set { entryWhite = value; }
		}

		public override GBCPaletteEntry EntryLightGray {
			get { return entryLightGray; }
			set { entryLightGray = value; }
		}

		public override GBCPaletteEntry EntryDarkGray {
			get { return entryDarkGray; }
			set { entryDarkGray = value; }
		}

		public override GBCPaletteEntry EntryBlack {
			get { return entryBlack; }
			set { entryBlack = value; }
		}
	}

	public class GBCPaletteEntry : GBPaletteEntry {
		/// <summary>
		/// Creates a PaletteEntry using the default color value.
		/// </summary>
		/// <param name="id"></param>
		public GBCPaletteEntry() : base(0) { }

		private Color color;

		public override Color Color {
			get {
				return color;
			}
			set {
				color = value;
			}
		}

		public override void SetToDefaultColor() {
			switch (this.CorespondingColor) {
			case GBColor.WHITE: this.Color = Color.White; return;
			case GBColor.DARK_GRAY: this.Color = Color.DarkGray; return;
			case GBColor.LIGHT_GRAY: this.Color = Color.LightGray; return;
			case GBColor.BLACK: this.Color = Color.Black; return;
			default: throw new InvalidOperationException("Got invalid GBColor " + this.CorespondingColor + "(int: " + ((int)this.CorespondingColor) + ")");
			}
		}
	}
}
