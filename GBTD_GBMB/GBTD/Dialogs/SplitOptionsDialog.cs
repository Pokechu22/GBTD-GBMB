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
		private SplitOptionsDialog() {
			InitializeComponent();
		}

		public SplitOptionsDialog(GBRObjectTileSettings settings) : this() {
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
	}
}
