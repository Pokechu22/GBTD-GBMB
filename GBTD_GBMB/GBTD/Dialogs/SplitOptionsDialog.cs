using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBRFile;

namespace GB.GBTD.Dialogs
{
	public partial class SplitOptionsDialog : Form
	{
		private readonly GBRObjectTileSettings settings;

		/// <summary>
		/// Whether or not Paste was selected.  (Ignore if DialogResult is not OK)
		/// </summary>
		public bool Paste { get; private set; }

		private SplitOptionsDialog() {
			InitializeComponent();
		}

		public SplitOptionsDialog(GBRObjectTileSettings settings) : this() {
			this.settings = settings;

			this.widthTextBox.Value = settings.SplitWidth;
			this.heightTextBox.Value = settings.SplitHeight;
			switch (settings.SplitOrder) {
			case SplitOrder.LEFT_TO_RIGHT_FIRST:
				this.orderDropDown.SelectedIndex = 0;
				break;
			case SplitOrder.TOP_TO_BOTTOM_FIRST:
				this.orderDropDown.SelectedIndex = 1;
				break;
			}

			pasteButton.Enabled = Clipboard.ContainsImage();

			if (pasteButton.Enabled) {
				this.AcceptButton = pasteButton;
			} else {
				this.AcceptButton = copyButton;
			}
		}

		private bool IsDataValid() {
			if (widthTextBox.Value < 1) {
				MessageBox.Show("Tile width should be at least one.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				widthTextBox.Focus();
				return false;
			}
			if (heightTextBox.Value < 1) {
				MessageBox.Show("Tile height should be at least one.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				heightTextBox.Focus();
				return false;
			}

			return true;
		}

		private void pasteButton_Click(object sender, EventArgs e) {
			if (!IsDataValid()) { return; }

			this.DialogResult = DialogResult.OK;

			settings.SplitWidth = (UInt16)widthTextBox.Value;
			settings.SplitHeight = (UInt16)heightTextBox.Value;
			settings.SplitOrder = (SplitOrder)orderDropDown.SelectedIndex;

			Paste = true;
		}

		private void copyButton_Click(object sender, EventArgs e) {
			if (!IsDataValid()) { return; }

			this.DialogResult = DialogResult.OK;

			settings.SplitWidth = (UInt16)widthTextBox.Value;
			settings.SplitHeight = (UInt16)heightTextBox.Value;
			settings.SplitOrder = (SplitOrder)orderDropDown.SelectedIndex;

			Paste = false;
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
