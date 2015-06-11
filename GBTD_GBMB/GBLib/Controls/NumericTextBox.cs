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
			this.MaxValue = UInt32.MaxValue;
			this.AutoSize = false;
		}

		[Category("Data"), Description("The numeric value.")]
		[DefaultValue(0)]
		public UInt32 Value {
			get {
				UInt32 returned;
				if (String.IsNullOrEmpty(this.Text)) {
					return 0;
				}
				try {
					returned = UInt32.Parse(this.Text);
				} catch (OverflowException) {
					this.Text = maxValue.ToString();
					returned = maxValue;
				} catch (Exception) {
					throw;
				}

				if (returned > maxValue) {
					returned = maxValue;
					this.Text = maxValue.ToString();
				}

				return returned;
			}
			set {
				this.Text = value.ToString();
			}	
		}

		private UInt32 maxValue;
		[Category("Data"), Description("The maximum numeric value.")]
		[DefaultValue(typeof(UInt32), "4294967295")]
		public UInt32 MaxValue {
			get {return this.maxValue;}
			set {
				maxValue = value;
				if (this.Value > maxValue) {
					this.Value = maxValue;
				}
			}
		}

		[DefaultValue("0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
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

		protected override void OnTextChanged(EventArgs e) {
			if (this.Value > this.maxValue) {
				this.Value = this.maxValue;
			}

			base.OnTextChanged(e);
		}
	}
}
