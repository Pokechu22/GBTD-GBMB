using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;

using GB.Shared.Tile;

namespace GB.Shared.Palette
{
	[EditorAttribute(typeof(PaletteObjectsEditor<GBCChoosePalette, GBCPaletteSetSelector, GBCPaletteSet, GBCPalette, GBCPaletteEntry>), typeof(System.Drawing.Design.UITypeEditor))]
	public class GBCPaletteSet : IPaletteSet<GBCPalette, GBCPaletteEntry> {

		public override int NumberOfRows {
			get { return 8; }
		}

		private GBCPalette[] rows = new GBCPalette[8] {
			new GBCPalette(),
			new GBCPalette(),
			new GBCPalette(),
			new GBCPalette(),
			new GBCPalette(),
			new GBCPalette(),
			new GBCPalette(),
			new GBCPalette(),
		};

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

	public class GBCPalette : IPalette<GBCPaletteEntry> {
		private GBCPaletteEntry entryWhite = new GBCPaletteEntry(GBColor.WHITE);
		private GBCPaletteEntry entryLightGray = new GBCPaletteEntry(GBColor.LIGHT_GRAY);
		private GBCPaletteEntry entryDarkGray = new GBCPaletteEntry(GBColor.DARK_GRAY);
		private GBCPaletteEntry entryBlack = new GBCPaletteEntry(GBColor.BLACK);

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

	public class GBCPaletteEntry : IPaletteEntry {
		/// <summary>
		/// Creates a PaletteEntry using the default color value.
		/// </summary>
		/// <param name="id"></param>
		public GBCPaletteEntry(GBColor color) : base(color) { }

		/// <summary>
		/// Creates a PaletteEntry using the default color value.
		/// </summary>
		/// <param name="id"></param>
		public GBCPaletteEntry(int color) : base(color) { }

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
