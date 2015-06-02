using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GB.Shared.GBRFile;

namespace GBRInfoSniffer
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

		public void LoadFile(String path) {
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

			GBRFile file = new GBRFile(stream);
			foreach (GBRObject obj in file.Objects.Values) {
				treeView1.Nodes.Add(obj.ToTreeNode());
			}
		}
	}
}
