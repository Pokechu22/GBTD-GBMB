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

		[Category("Data"), Description("The numeric value.")]
		[DefaultValue(0)]
		public UInt32 Value {
			get {
				if (String.IsNullOrEmpty(this.Text)) {
					return 0;
				}
				try {
					return UInt32.Parse(this.Text);
				} catch (OverflowException) {
					this.Value = UInt32.MaxValue;
					return UInt32.MaxValue;
				} catch (Exception) {
					throw;
				}
			}
			set {
				this.Text = value.ToString();
			}	
		}

		[DefaultValue("0")]
		public override string Text {
			get { return base.Text; }
			set {
				UInt32 result = 0;
				if (UInt32.TryParse(value, out result)) {
					base.Text = value;
				} else {
					//Do nothing as it is invalid input.
				}
			}
		}

		protected override void OnKeyPress(KeyPressEventArgs e) {
			if (!(char.IsNumber(e.KeyChar) 
					|| e.KeyChar == (char)0x08)) {
				e.Handled = true;
			}
			base.OnKeyPress(e);
		}
	}
}
