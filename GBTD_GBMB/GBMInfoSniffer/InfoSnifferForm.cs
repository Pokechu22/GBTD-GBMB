using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GB.Shared.GBMFile;

namespace GBMInfoSniffer
{
	public partial class InfoSnifferForm : Form
	{
		public InfoSnifferForm() {
			InitializeComponent();
		}

		private void openButton_Click(object sender, EventArgs e) {
			var result = openFileDialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK) {
				groupBox.Text = openFileDialog.FileName;
				using (Stream stream = openFileDialog.OpenFile()) {
					LoadTreeFromStream(stream);
				}
			}
		}

		public void LoadTreeFromStream(Stream stream) {
			treeView1.Nodes.Clear();

			try {
				GB.Shared.GBMFile.GBMFile file = new GB.Shared.GBMFile.GBMFile(stream);
				foreach (GBMObject obj in file.Objects.Values) {
					treeView1.Nodes.Add(obj.ToTreeNode());
				}
			} catch (Exception e) {
				MessageBox.Show("An exception occured while loading the GBM file: " + e.ToString() + "\n\nThe application will now crash.",
					"An exception occured - The application will now crash.",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				throw;
			}
		}
	}
}
