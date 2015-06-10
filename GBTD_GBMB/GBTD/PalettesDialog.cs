using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBRFile;
using GB.Shared.Palettes;

namespace GB.GBTD
{
	public partial class PalettesDialog : Form
	{
		private readonly GBRObjectPalettes palettes;
		private readonly ColorSet colorSet;

		private PalettesDialog() {
			InitializeComponent();
		}

		public PalettesDialog(GBRObjectPalettes palettes, ColorSet colorSet) : this() {
			this.palettes = palettes;
			this.colorSet = colorSet;

			this.groupBox.Text = colorSet.GetDisplayName() + " palettes";
		}

		private void okButton_Click(object sender, EventArgs e) {
			//TODO: Update palettes.

			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
