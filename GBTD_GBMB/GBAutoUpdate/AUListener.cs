using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace GB.Shared.AutoUpdate
{
	/// <summary>
	/// Listens to AutoUpdate changes using the windows message API.
	/// </summary>
	public class AUListener : Component
	{
		public delegate void MessageEventHandler(ref Message m);

		/// <summary>
		/// Provides a hidden window so that I can use WndProc.
		/// 
		/// http://stackoverflow.com/a/2061741/3991344
		/// TODO: Can I use NativeWindow instead?
		/// </summary>
		private class MessageListener : Form
		{
			public static event MessageEventHandler OnMessage;
			private static MessageListener instance;

			public static void Start() {
				if (instance != null) {
					return; //Multiple instances may occur.  Not sure of how to handle yet.
					//throw new InvalidOperationException("Notifier aleady started");
				}
				Thread t = new Thread(runForm);
				t.SetApartmentState(ApartmentState.STA);
				t.IsBackground = true;
				t.Start();
			}
			public static void Stop() {
				if (instance == null) { throw new InvalidOperationException("Notifier not yet started"); }
				OnMessage = null;
				instance.Invoke(new MethodInvoker(instance.endForm));
			}
			private static void runForm() {
				Application.Run(new MessageListener());
			}

			private void endForm() {
				this.Close();
			}
			protected override void SetVisibleCore(bool value) {
				// Prevent window getting visible
				if (instance == null) CreateHandle();
				instance = this;
				value = false;
				base.SetVisibleCore(value);
			}
			protected override void WndProc(ref Message m) {
				base.WndProc(ref m);
				if (OnMessage != null) { OnMessage(ref m); }
			}
		}

		public readonly uint autoUpdateMessage;

		MessageListener listener;

		public AUListener() {
			autoUpdateMessage = RegisterWindowMessage(@"GBHMTILEC:\Pokechu22\TestMap.gbr".ToUpperInvariant());
			Console.WriteLine(autoUpdateMessage.ToString("x"));

			MessageListener.Start();
			MessageListener.OnMessage += new MessageEventHandler(onListenerMessage);
		}

		void onListenerMessage(ref Message m) {
			//if (m.Msg == autoUpdateMessage) {
			if (m.Msg >= 0x4000) {
				if (OnMessage != null) {
					OnMessage(ref m);
				}
			}
		}

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern uint RegisterWindowMessage(string lpString);

		public event MessageEventHandler OnMessage;
	}
}
