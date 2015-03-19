using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace GB.Shared.AutoUpdate
{
	/// <summary>
	/// Listens to AutoUpdate changes using the windows message API.
	/// </summary>
	public class AUMessenger : Component
	{
		/// <summary>
		/// http://pinvoke.net/default.aspx/user32/RegisterWindowMessage.html
		/// </summary>
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern uint RegisterWindowMessage(string lpString);
		/// <summary>
		/// http://pinvoke.net/default.aspx/user32/SendMessage.html
		/// </summary>
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// http://pinvoke.net/default.aspx/Constants/HWND.html
		/// </summary>
		private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);

		/// <summary>
		/// The marker that is prepended to all messages.
		/// </summary>
		private const string MARKER = "GBHMTILE";

		//Tile message hexes.
		private const UInt16 TILEMSGTOTAL = 0x8000;
		private const UInt16 TILEMSGLIST = 0x8001;
		private const UInt16 TILEMSGDIM = 0x8002;
		private const UInt16 TILEMSGPAL = 0x8003;
		private const UInt16 TILEMSGCOLSETS = 0x8004;

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

		public AUMessenger() {
			MessageListener.Start();
			MessageListener.OnMessage += new MessageEventHandler(onListenerMessage);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				MessageListener.OnMessage -= new MessageEventHandler(onListenerMessage);
			}
			base.Dispose(disposing);
		}

		void onListenerMessage(object sender, MessageEventArgs args) {
			if (args.Message.Msg == AutoUpdateMessageID) {
				if (args.Message.LParam == MessageListener.MessageListenerHandle) { return; } //To avoid an infinite loop.

				UInt16 WParam = (UInt16)args.Message.WParam;
				if ((WParam & 0x8000U) != 0) {
					//A special message type has occured.
					switch (WParam) {
					case TILEMSGTOTAL:
						if (OnTotalRefreshNeeded != null) {
							OnTotalRefreshNeeded(this, args);
						}
						break;
					case TILEMSGLIST:
						if (OnTileRefreshNeeded != null) {
							OnTileRefreshNeeded(this, args);
						}
						break;
					case TILEMSGDIM:
						if (OnTileSizeChanged != null) {
							OnTileSizeChanged(this, args);
						}
						break;
					case TILEMSGPAL:
						if (OnGBPaletteChanged != null) {
							OnGBPaletteChanged(this, args);
						}
						break;
					case TILEMSGCOLSETS:
						if (OnColorPaletteChanged != null) {
							OnColorPaletteChanged(this, args);
						}
						break;
					default:
						if (OnTotalRefreshNeeded != null) {
							OnTotalRefreshNeeded(this, args);
						}
						break;
					}
				} else {
					if (OnTileChanged != null) {
						OnTileChanged(this, new TileChangedEventArgs(WParam, args.Message));
					}
				}

				//Finally, send the full change message.
				if (OnAutoUpdateMessage != null) {
					OnAutoUpdateMessage(this, args);
				}
			}
		}

		public void SendTileChangeMessage(UInt16 tile) {
			tile &= 0x7FFF; //If someone is trying to send an invalid tile change message, too bad.
			
			SendMessage(
				hWnd: HWND_BROADCAST,
				Msg: AutoUpdateMessageID,
				wParam: new IntPtr(tile),
				lParam: MessageListener.MessageListenerHandle);
		}

		public void SendTotalRefreshMessage() {
			SendMessage(
				hWnd: HWND_BROADCAST,
				Msg: AutoUpdateMessageID,
				wParam: new IntPtr(TILEMSGTOTAL),
				lParam: MessageListener.MessageListenerHandle);
		}

		public void SendTileListRefreshMessage() {
			SendMessage(
				hWnd: HWND_BROADCAST,
				Msg: AutoUpdateMessageID,
				wParam: new IntPtr(TILEMSGLIST),
				lParam: MessageListener.MessageListenerHandle);
		}

		public void SendTileDimensionsMessage() {
			SendMessage(
				hWnd: HWND_BROADCAST,
				Msg: AutoUpdateMessageID,
				wParam: new IntPtr(TILEMSGDIM),
				lParam: MessageListener.MessageListenerHandle);
		}

		public void SendColorMappingsMessage() {
			SendMessage(
				hWnd: HWND_BROADCAST,
				Msg: AutoUpdateMessageID,
				wParam: new IntPtr(TILEMSGPAL),
				lParam: MessageListener.MessageListenerHandle);
		}

		public void SendColorSetsMessage() {
			SendMessage(
				hWnd: HWND_BROADCAST,
				Msg: AutoUpdateMessageID,
				wParam: new IntPtr(TILEMSGCOLSETS),
				lParam: MessageListener.MessageListenerHandle);
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

	public delegate void MessageEventHandler(object sender, MessageEventArgs args);
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
	public delegate void TileChangedEventHandler(object sender, TileChangedEventArgs args);
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
