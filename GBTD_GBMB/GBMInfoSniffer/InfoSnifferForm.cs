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

		protected override void OnLoad(EventArgs e) {
			String[] args = Environment.GetCommandLineArgs();

			if (args.Length >= 2) {
				LoadFile(args[1]);
			}

			base.OnLoad(e);
		}

		private void openButton_Click(object sender, EventArgs e) {
			var result = openFileDialog.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK) {
				LoadFile(openFileDialog.FileName);
			}
		}

		private void LoadFile(String path) {
			if (!File.Exists(path)) {
				MessageBox.Show("File " + path + " not found!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			groupBox.Text = path;
			using (Stream stream = File.OpenRead(path)) {
				LoadTreeFromStream(stream);
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
