using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBRenderer
{
	public partial class ChoosePalette : Form
	{
		/// <summary>
		/// The whitemost color.
		/// </summary>
		public Color WhiteColor {
			get {
				return gbPaletteSetSelector1.Colors[0, 0];
			}
		}

		/// <summary>
		/// The light-gray color.
		/// </summary>
		public Color LightGrayColor {
			get {
				return gbPaletteSetSelector1.Colors[1, 0];
			}
		}

		/// <summary>
		/// The dark-gray color.
		/// </summary>
		public Color DarkGrayColor {
			get {
				return gbPaletteSetSelector1.Colors[2, 0];
			}
		}

		/// <summary>
		/// The black color.
		/// </summary>
		public Color BlackColor {
			get {
				return gbPaletteSetSelector1.Colors[3, 0];
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
	}
}
