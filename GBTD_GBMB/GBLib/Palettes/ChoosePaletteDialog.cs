using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palettes;

namespace GB.Shared.Palettes
{
	public partial class ChoosePalette : Form
	{
		public ChoosePalette() {
			InitializeComponent();
		}

		public ChoosePalette(PaletteSet set) : this() {
			this.Set = set;
		}

		public PaletteSet Set {
			get { return gbPaletteSetSelector1.Set; }
			set { gbPaletteSetSelector1.Set = value; }
		}

		private void colorPicker1_OnChange(object sender, EventArgs e) {
			try {
				gbPaletteSetSelector1.SelectedColor = colorPicker1.MainViewColor;
			} catch (Exception) { }
		}

		private void gbPaletteSetSelector1_SelectionChanged(object sender, EventArgs e) {
			colorPicker1.Enabled = (gbPaletteSetSelector1.SelectedX != -1 && gbPaletteSetSelector1.SelectedY != -1);
			if (colorPicker1.Enabled) {
				colorPicker1.FirstColor = gbPaletteSetSelector1.SelectedColor;
			}
		}

		private void filterCheckBox_CheckedChanged(object sender, EventArgs e) {
			colorPicker1.GBCFilter = filterCheckBox.Checked;
			gbPaletteSetSelector1.GBCFilter = filterCheckBox.Checked;
		}
	}

	/*public class GBCChoosePalette : ChoosePalette<GBCPaletteSetSelector, GBCPaletteSet, GBCPalette, GBCPaletteEntry>
	{
		public GBCChoosePalette() : base() { }

		public GBCChoosePalette(GBCPaletteSet set) : base(set) { }
	}*/
}
