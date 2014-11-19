using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palette;

namespace GBRenderer
{
	public partial class ChoosePalette : Form
	{
		/// <summary>
		/// The whitemost color.
		/// </summary>
		public Color WhiteColor {
			get {
				return gbPaletteSetSelector1.Set.Rows[0][0];
			}
		}

		/// <summary>
		/// The light-gray color.
		/// </summary>
		public Color LightGrayColor {
			get {
				return gbPaletteSetSelector1.Set.Rows[0][1];
			}
		}

		/// <summary>
		/// The dark-gray color.
		/// </summary>
		public Color DarkGrayColor {
			get {
				return gbPaletteSetSelector1.Set.Rows[0][2];
			}
		}

		/// <summary>
		/// The black color.
		/// </summary>
		public Color BlackColor {
			get {
				return gbPaletteSetSelector1.Set.Rows[0][3];
			}
		}

		public ChoosePalette() {
			InitializeComponent();
			
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
}
