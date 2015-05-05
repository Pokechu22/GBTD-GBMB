using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GB.GBMB.Dialogs
{
	public partial class GBMBAboutBox : Form
	{
		public GBMBAboutBox() {
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			versionLabel.Text = String.Format("Version {0}", Application.ProductVersion);

			base.OnLoad(e);
		}
	}
}
