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
				UInt16 WParam = (UInt16)args.Message.WParam;
				if ((WParam & 0x8000U) != 0) {
					//A special message type has occured.
				} else {

				}

				//Finally, send the full change message.
				if (OnAutoUpdateMessage != null) {
					OnAutoUpdateMessage(this, args);
				}
			}
		}

		[Description("Raw event for accessing the raw message used")]
		public event MessageEventHandler OnAutoUpdateMessage;
		[Description(@"Called when a tile changes.
Changes can be different pixels, or changes in the selected palette entries for this type.")]
		public event TileChangedEventHandler OnTileChanged;
		[Description(@"Called when a total refresh is necessary.
The TILEMSGTOTAL (0x8000) message (or an unrecognised one) was sent.
A full and total refresh of all parts of the tileset should take place. This can be seen as a combination of all other messages.")]
		public event MessageEventHandler OnTotalRefreshNeeded;
		[Description(@"The graphics and palettes for all tiles should be refreshed, but nothing else.
The TILEMSGLIST (0x8001) message was sent.
All tiles should be refreshed, but only the actual pixels and palette entries. Note that this is also called when the actual tile count has changed.")]
		public event MessageEventHandler OnTileRefreshNeeded;
		[Description(@"The size of the tiles (height and width) has been changed.
The TILEMSGDIM (0x8002) message was sent.
The dimensions of the tiles (“Tile size..”) have changed.")]
		public event MessageEventHandler OnTileSizeChanged;
		[Description(@"The DMG palette has changed.
The TILEMSGPAL (0x8003) message was sent.
The Gameboy palette has changed (ie: the values used by IO-ports $47-$49, not the color palettes).")]
		public event MessageEventHandler OnGBPaletteChanged;
		[Description(@"One of the color palettes has changed.
The TILEMSGCOLSETS (0x8004) message was sent.
Either the GBC or SGB palette colors have changed.")]
		public event MessageEventHandler OnColorPaletteChanged;
	}

	/// <summary>
	/// Simple eventargs for when there is a message sent.
	/// </summary>
	public class MessageEventArgs : EventArgs
	{
		public readonly Message Message;
		public MessageEventArgs(Message message) {
			this.Message = message;
		}
	}
	/// <summary>
	/// Simple eventargs for when a tile is changed.
	/// </summary>
	public class TileChangedEventArgs : MessageEventArgs
	{
		public readonly UInt16 TileID;
		public TileChangedEventArgs(UInt16 TileID, Message message)
			: base(message) {
			this.TileID = TileID;
		}
	}
}
