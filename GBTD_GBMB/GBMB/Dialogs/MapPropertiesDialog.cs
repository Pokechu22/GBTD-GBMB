using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBMB.Dialogs
{
	public partial class MapPropertiesDialog : Form
	{
		public MapPropertiesDialog() {
			InitializeComponent();

			fileNameTextBox.AutoSize = false;
			fileNameTextBox.Size = new Size(169, 21);
		}
	}
}
