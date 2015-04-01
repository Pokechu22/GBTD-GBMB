using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GB.Shared.Controls
{
	public class NumericTextBox : TextBox
	{
		[DefaultValue(false)]
		public override bool AutoSize { get { return base.AutoSize; } set { base.AutoSize = value; } }

		public NumericTextBox() {
			this.Text = "0";
			this.AutoSize = false;
		}

		private int value = 0;
		private bool acceptsSigned = false;

		[Category("Data"), Description("The numeric value.")]
		[DefaultValue(0)]
		public int Value {
			get {
				return this.value;
			}
			set {
				if (!acceptsSigned && value < 0) {
					return;
				}
				this.Text = value.ToString();
			}
		}

		[Category("Data"), Description("Whether or not numbers are allowed to be signed (negative).")]
		[DefaultValue(false)]
		public bool AcceptsSigned {
			get { return this.acceptsSigned; }
			set { this.acceptsSigned = value; }
		}

		[DefaultValue("0")]
		public override string Text {
			get { return base.Text; }
			set {
				int result = 0;
				if (Int32.TryParse(value, out result)) {
					if (!acceptsSigned && result < 0) {
						return;
					}
					this.value = result;
					base.Text = value;
				} else {
					//Do nothing as it is invalid input.
				}
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e) {
			if (!(char.IsNumber(e.KeyChar) 
					|| e.KeyChar == (char)0x08
					|| (acceptsSigned && TextLength == 0 && e.KeyChar == '-'))) {
				e.Handled = true;
			}
			base.OnKeyPress(e);
		}
	}
}
