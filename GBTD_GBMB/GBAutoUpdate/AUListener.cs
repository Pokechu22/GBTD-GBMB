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
		public delegate void MessageEventHandler(object sender, MessageEventArgs args);
		public delegate void TileChangedEventHandler(object sender, TileChangedEventArgs args);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern uint RegisterWindowMessage(string lpString);

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
				if (OnMessage != null) { OnMessage(this, new MessageEventArgs(m)); }
			}
		}

		/// <summary>
		/// The marker that is prepended to all messages.
		/// </summary>
		private const string MARKER = "GBHMTILE";

		/// <summary>
		/// The ID used by the AutoUpdate message.
		/// </summary>
		public uint AutoUpdateMessageID { get; protected set; }
		/// <summary>
		/// The name of the AutoUpdate message.
		/// </summary>
		public string AutoUpdateMessageName { get; protected set; }

		private string fileName;
		/// <summary>
		/// The name of the file to watch.
		/// </summary>
		[Category("Data"), Description("The name of the file to watch.")]
		public string FileName {
			get {
				return fileName;
			}
			set {
				if (fileName == value) { return; } //Don't bother updating it if it is the same.
				fileName = value;

				AutoUpdateMessageName = MARKER + fileName.ToUpperInvariant();
				AutoUpdateMessageID = RegisterWindowMessage(AutoUpdateMessageName);
			}
		}

		public AUListener() {
			MessageListener.Start();
			MessageListener.OnMessage += new MessageEventHandler(onListenerMessage);
		}

		void onListenerMessage(object sender, MessageEventArgs args) {
			if (args.Message.Msg == AutoUpdateMessageID) {
				if (OnAutoUpdateMessage != null) {
					OnAutoUpdateMessage(this, args);
				}
			}
		}

		public event MessageEventHandler OnAutoUpdateMessage;
		public event TileChangedEventHandler OnTileChanged;
	}

	/// <summary>
	/// Simple eventargs for when there is a message sent.
	/// </summary>
	public class MessageEventArgs
	{
		public readonly Message Message;
		public MessageEventArgs(Message message) {
			this.Message = message;
		}
	}
	/// <summary>
	/// Simple eventargs for when a tile is changed.
	/// </summary>
	public class TileChangedEventArgs
	{
		public readonly UInt16 TileID;
		public TileChangedEventArgs(UInt16 TileID) {
			this.TileID = TileID;
		}
	}
}
