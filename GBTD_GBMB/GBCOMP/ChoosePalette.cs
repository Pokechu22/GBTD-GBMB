﻿using System;
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
		private GBColorSetSelector selectedControl = null;

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
