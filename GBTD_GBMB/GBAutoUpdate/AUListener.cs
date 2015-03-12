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
		public class Test : IMessageFilter
		{
			public readonly uint val;

			public Test(uint val) {
				this.val = val;
			}

			public bool PreFilterMessage(ref Message m) {
				//if (m.Msg == val) {
					Console.WriteLine(m);
				//}
				return false;
			}
		}

		public readonly uint AU_MESSAGE_FOR_FILE;

		public AUListener() {
			AU_MESSAGE_FOR_FILE = RegisterWindowMessage(@"GBHMTILEC:\Pokechu22\TestMap.gbr".ToUpperInvariant());
			Console.WriteLine("AU_MESSAGE_FOR_FILE: {0:x}", AU_MESSAGE_FOR_FILE);

			Application.AddMessageFilter(new Test(AU_MESSAGE_FOR_FILE));
		}

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern uint RegisterWindowMessage(string lpString);
	}
}
