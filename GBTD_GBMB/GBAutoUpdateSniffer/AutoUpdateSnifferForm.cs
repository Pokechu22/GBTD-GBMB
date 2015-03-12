using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBAutoUpdateSniffer
{
	public partial class AutoUpdateSnifferForm : Form
	{
		public AutoUpdateSnifferForm() {
			InitializeComponent();
		}

		private void auListener1_Click(object sender, EventArgs e) {

		}

		protected override void WndProc(ref Message m) {
			base.WndProc(ref m);

			if (m.Msg > 0x4000) {
				Console.WriteLine(m);
			}
		}
	}
}
