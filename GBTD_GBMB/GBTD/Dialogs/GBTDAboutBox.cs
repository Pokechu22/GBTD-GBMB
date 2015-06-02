using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace GB.GBTD.Dialogs
{
	public partial class GBTDAboutBox : Form
	{
		public GBTDAboutBox() {
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			versionLabel.Text = String.Format("Version {0}", Application.ProductVersion);
			buildLabel.Text = String.Format("Build {0}", Assembly.GetExecutingAssembly().GetName().Version);
			buildDateLabel.Text = String.Format("Build date {0}", RetrieveLinkerTimestamp().ToString());
			base.OnLoad(e);
		}

		/// <summary>
		/// Retreives the date of program compilation.  Magic!
		/// 
		/// http://stackoverflow.com/a/1600990/3991344
		/// </summary>
		/// <returns></returns>
		private DateTime RetrieveLinkerTimestamp() {
			string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
			const int c_PeHeaderOffset = 60;
			const int c_LinkerTimestampOffset = 8;
			byte[] b = new byte[2048];
			System.IO.Stream s = null;

			try {
				s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				s.Read(b, 0, 2048);
			} finally {
				if (s != null) {
					s.Close();
				}
			}

			int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
			int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
			DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			dt = dt.AddSeconds(secondsSince1970);
			dt = dt.ToLocalTime();
			return dt;
		}

		private void labelWithLink_Click(object sender, EventArgs args) {
			Control control = sender as Control;
			if (control != null) {
				if (control.Tag is String) {
					System.Diagnostics.Process.Start((String)control.Tag); //If the tag is a URL this opens the right web browser.
				}
			}
		}
	}
}
