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
		public NumericTextBox() {
			this.Text = "0";
		}

		private int value = 0;

		[Category("Data"), Description("The numeric value.")]
		[DefaultValue(0)]
		public int Value {
			get {
				return this.value;
			}
			set {
				this.Text = value.ToString();
			}
		}

		[DefaultValue("0")]
		public override string Text {
			get { return base.Text; }
			set {
				int result = 0;
				if (Int32.TryParse(value, out result)) {
					this.value = result;
					base.Text = value;
				} else {
					//Do nothing as it is invalid input.
				}
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e) {
			if (!(char.IsNumber(e.KeyChar) || e.KeyChar == (char)0x08)) {
				e.Handled = true;
			}
			base.OnKeyPress(e);
		}
	}
}
