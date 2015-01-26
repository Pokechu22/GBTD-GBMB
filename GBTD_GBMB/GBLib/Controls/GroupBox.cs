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
	[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))] 
	public partial class GroupBox : UserControl
	{
		private string text = "";

		[Browsable(true), Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text {
			get { return text; }
			set { this.text = value; this.OnTextChanged(new EventArgs()); this.Invalidate(); }
		}

		protected override void OnTextChanged(EventArgs e) {
			cleanLabel1.Text = this.Text;
			base.OnTextChanged(e);
		}

		public GroupBox() {
			InitializeComponent();
		}

		protected override void OnSizeChanged(EventArgs e) {
			base.OnSizeChanged(e);
			border1.Size = border2.Size = new Size(this.Width - 1, this.Height - 6);
			border3.Size = new Size(this.Width - 3, this.Height - 8);
		}
	}
}
