﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBRInfoSniffer
{
	public partial class InfoSnifferForm : Form
	{
		public InfoSnifferForm() {
			InitializeComponent();
		}

		private void openButton_Click(object sender, EventArgs e) {
			var result = openFileDialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK) {
				//TODO open file...
			}
		}
	}
}