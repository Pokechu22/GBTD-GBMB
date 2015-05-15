using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Palettes;
using GB.Shared.Tiles;
using GB.Shared;
using GB.GBTD.Dialogs;
using GB.Shared.GBRFile;

namespace GB.GBTD
{
	public partial class TileEdit : Form
	{
		private string filePath;
		private GBRFile GBRFile;

		public TileEdit() {
			InitializeComponent();

			initClipboardChangeCheck();
		}

		protected override void OnLoad(EventArgs e) {
			//Loads the menu.
			//http://stackoverflow.com/a/28462365/3991344
			this.Menu = this.mainMenu;

			base.OnLoad(e);
		}

		private void initClipboardChangeCheck() {
			NativeMethods.AddClipboardFormatListener(Handle);
			OnClipboardUpdate();
		}

		private void OnClipboardUpdate() {
			//TODO
		}

		protected override void WndProc(ref Message m) {
			if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE) {
				OnClipboardUpdate();
			}

			base.WndProc(ref m);
		}
	}
}
