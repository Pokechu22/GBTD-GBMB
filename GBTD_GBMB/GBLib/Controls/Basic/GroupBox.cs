using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace GB.Shared.Controls
{
	/// <summary>
	/// Provides a GBTD-styled group box.
	/// </summary>
	public partial class GroupBox : Panel
	{
		private string text = "";
		protected override Padding DefaultPadding { get { return new Padding(5, 13, 5, 5); } }

		protected override void OnControlAdded(ControlEventArgs e) {
			//Check to make sure it isn't one of the default controls.

			if (!(Object.ReferenceEquals(e.Control, this.border1) ||
					Object.ReferenceEquals(e.Control, this.border2) ||
					Object.ReferenceEquals(e.Control, this.border3) ||
					Object.ReferenceEquals(e.Control, this.cleanLabel1))) {
				e.Control.BringToFront();
			}
			base.OnControlAdded(e);
		}

		[Browsable(true), Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text {
			get { return text; }
			set { this.text = value; this.OnTextChanged(new EventArgs()); this.Invalidate(true); }
		}

		protected override void OnTextChanged(EventArgs e) {
			cleanLabel1.Text = this.Text;
			base.OnTextChanged(e);
			this.Invalidate(true);
		}

		public GroupBox() {
			InitializeComponent();

			this.SetStyle(ControlStyles.ContainerControl, true);
			this.SetStyle(ControlStyles.Selectable, false);

			this.OnSizeChanged(new EventArgs());
			this.Text = this.Name;
			this.TabStop = false;
		}

		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);
			border1.Size = border2.Size = new Size(this.Width - 1, this.Height - 6);
			border3.Size = new Size(this.Width - 3, this.Height - 8);
			this.Invalidate(true);
		}
	}
}
