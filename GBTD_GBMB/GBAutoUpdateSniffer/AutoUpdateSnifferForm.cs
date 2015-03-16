using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.AutoUpdate;

namespace GBAutoUpdateSniffer
{
	public partial class AutoUpdateSnifferForm : Form
	{
		public AutoUpdateSnifferForm() {
			InitializeComponent();
		}

		private void openButton_Click(object sender, EventArgs e) {
			var result = openFileDialog.ShowDialog();
			if (result != DialogResult.OK) {
				return;
			}

			fileNameLabel.Text = openFileDialog.FileName;
			auListener.FileName = openFileDialog.FileName;

			listBoxMessages.Items.Clear();

			labelMessageHex.Text = auListener.AutoUpdateMessageID.ToString("X4");
		}

		private void listBoxMessages_SelectedIndexChanged(object sender, EventArgs e) {
			if (listBoxMessages.SelectedIndex == -1) {
				textBoxMessageInfo.Text = "";
			} else {
				textBoxMessageInfo.Text = listBoxMessages.SelectedItem.ToString();
			}
		}

		private void auListener_OnAutoUpdateMessage(object sender, MessageEventArgs args) {
			try {
				//Because this might come from a seperate thread, invoke the change on the current thread.
				//http://stackoverflow.com/a/142069/3991344
				listBoxMessages.Invoke(new MethodInvoker(delegate {
					listBoxMessages.Items.Add(new AUEventInfo(args, AUEventType.Single_tile));
				}));
			} catch (Exception e) {
				MessageBox.Show(e.ToString(), "An exception occured while adding an item to the list.", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
