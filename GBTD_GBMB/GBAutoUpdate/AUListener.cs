using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GB.Shared.AutoUpdate
{
	/// <summary>
	/// Listens to AutoUpdate changes using the windows message API.
	/// </summary>
	public class AUListener : Control
	{
		public readonly uint AU_MESSAGE_FOR_FILE;

		public AUListener() {
			SetStyle(ControlStyles.EnableNotifyMessage, true);

			AU_MESSAGE_FOR_FILE = RegisterWindowMessage(@"GBHMTILEC:\Pokechu22\TestMap.gbr".ToUpperInvariant());
			Console.WriteLine("AU_MESSAGE_FOR_FILE: {0:x}", AU_MESSAGE_FOR_FILE);
		}

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern uint RegisterWindowMessage(string lpString);

		[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref Message m) {
			base.WndProc(ref m);

			//if (m.Msg == AU_MESSAGE_FOR_FILE) {
				
			//}
		}
	}
}
