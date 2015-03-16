﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GB.Shared.AutoUpdate
{
	/// <summary>
	/// Provides a hidden window so that I can use WndProc.
	/// 
	/// http://stackoverflow.com/a/2061741/3991344
	/// TODO: Can I use NativeWindow instead?
	/// </summary>
	class MessageListener : Form
	{
		public static event MessageEventHandler OnMessage;
		private static MessageListener instance;

		public static void Start() {
			if (instance != null) {
				return; //Multiple instances may occur.  Not sure of how to handle yet.
				//throw new InvalidOperationException("Notifier aleady started");
			}
			Thread t = new Thread(runForm);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			t.SetApartmentState(ApartmentState.STA);
			t.IsBackground = true;
			t.Start();
		}

		static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
			MessageBox.Show("Thread Exception!  " + e.ToString() + " " + e.Exception.ToString());
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
}
