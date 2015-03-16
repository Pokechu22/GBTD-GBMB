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

		private void auListener1_OnMessage(object sender, MessageEventArgs args) {
			Console.WriteLine(args.Message);
		}
	}
}
