using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palette;

namespace GB.Shared.Palette
{
	public partial class ChoosePalette<TSelector, TSet, TRow, TEntry> : Form
		where TSelector : GBPaletteSetSelector<TSet, TRow, TEntry>, new()
		where TSet : PaletteSetBase<TRow, TEntry>, new()
		where TRow : PaletteBase<TEntry>
		where TEntry : PaletteEntryBase
	{
		public ChoosePalette() {
			InitializeComponent();
		}

		public ChoosePalette(TSet set) : this() {
			this.Set = set;
		}

		public TSet Set {
			get { return gbPaletteSetSelector1.Set; }
			set { if (value == null) { throw new ArgumentNullException(); } gbPaletteSetSelector1.Set = value; }
		}

		private void colorPicker1_OnChange(object sender, EventArgs e) {
			try {
				gbPaletteSetSelector1.SelectedColor = colorPicker1.MainViewColor;
			} catch (Exception) { }
		}

		private void gbPaletteSetSelector1_SelectionChanged(object sender, EventArgs e) {
			colorPicker1.Enabled = (gbPaletteSetSelector1.SelectedX != -1 && gbPaletteSetSelector1.SelectedY != -1);
		}

		private void filterCheckBox_CheckedChanged(object sender, EventArgs e) {
			colorPicker1.GBCFilter = filterCheckBox.Checked;
			gbPaletteSetSelector1.GBCFilter = filterCheckBox.Checked;
		}
	}

	public class GBCChoosePalette : ChoosePalette<GBCPaletteSetSelector, GBCPaletteSet, GBCPalette, GBCPaletteEntry>
	{
		public GBCChoosePalette() : base() { }

		public GBCChoosePalette(GBCPaletteSet set) : base(set) { }
	}
}
