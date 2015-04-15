using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using GB.Shared.GBMFile;
using GB.Shared.Controls;

namespace GB.GBMB.Dialogs
{
	internal class LocationPropertiesEditControl : Control
	{
		protected override Size DefaultSize { get { return new Size(241, 174); } }

		private GBMObjectMapPropertiesRecord[] properties = new GBMObjectMapPropertiesRecord[0];

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false), ReadOnly(true)]
		public GBMObjectMapPropertiesRecord[] Properties {
			get { SaveChanges(); return properties; }
			set {
				if (value == null) {
					value = new GBMObjectMapPropertiesRecord[0];
				} else {
					value = (GBMObjectMapPropertiesRecord[])value.Clone();
				}
				properties = value;
				LoadChanges();
				this.Invalidate(true);
			}
		}

		private TextBox[] names = new TextBox[0];
		private NumericTextBox[] maximums = new NumericTextBox[0];
		private NumericTextBox[] bits = new NumericTextBox[0];

		public LocationPropertiesEditControl() {
			SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
		}

		public void SaveChanges() {
			properties = new GBMObjectMapPropertiesRecord[names.Length];

			for (int i = 0; i < properties.Length; i++) {
				properties[i] = new GBMObjectMapPropertiesRecord();

				properties[i].Name = names[i].Text;
				properties[i].MaxValue = maximums[i].Value;
				properties[i].Type = 0;
			}
		}

		public void LoadChanges() {
			//If the size changed and everything needs to be resized.
			if (names.Length != properties.Length) {
				ResizeControls();
			}

			for (int i = 0; i < properties.Length; i++) {
				names[i].Text = properties[i].Name;
				maximums[i].Value = properties[i].MaxValue;
				bits[i].Value = GetNumberOfBits(properties[i].MaxValue);
			}
		}

		/// <summary>
		/// Gets the number of bits used for a number.
		/// </summary>
		private UInt32 GetNumberOfBits(UInt32 number) {
			UInt32 bits = 32;

			while (bits > 0) {
				//If the topmost bit is set.
				if ((number & 0x80000000U) != 0) {
					break;
				}
				number <<= 1;
				bits--;
			}

			return bits;
		}

		private void ResizeControls() {
			const int LEFT_X = 2, TOP_Y = 2; //TopLeft coords (due to border).
			const int BOX_HEIGHT = 19;
			const int NAME_X = LEFT_X + 21, NAME_WIDTH = 121;
			const int MAX_X = LEFT_X + 142, MAX_WIDTH = 50;
			const int BITS_X = LEFT_X + 192, BITS_WIDTH = 45;
			const int DATA_Y = TOP_Y + BOX_HEIGHT;

			this.SuspendLayout();

			foreach (Control c in names) { c.Dispose(); }
			foreach (Control c in maximums) { c.Dispose(); }
			foreach (Control c in bits) { c.Dispose(); }

			this.Controls.Clear();

			names = new TextBox[properties.Length];
			maximums = new NumericTextBox[properties.Length];
			bits = new NumericTextBox[properties.Length];

			for (int i = 0; i < properties.Length; i++) {
				int y = DATA_Y + (i * BOX_HEIGHT);

				TextBox nameTextBox = new TextBox();
				names[i] = nameTextBox;
				names[i].Name = "NameTextBox" + i;
				names[i].BorderStyle = BorderStyle.None;
				names[i].Dock = DockStyle.Fill;
				names[i].MaxLength = 31;
				names[i].Tag = i;

				names[i].TextChanged += new EventHandler(nameTextBox_TextChanged);

				Panel namePanel = new Panel();
				namePanel.SetBounds(NAME_X, y, NAME_WIDTH - 1, BOX_HEIGHT - 1);
				namePanel.BorderStyle = BorderStyle.None;
				namePanel.GotFocus += new EventHandler((s, a) => { nameTextBox.Focus(); nameTextBox.SelectAll(); });
				namePanel.Click += new EventHandler((s, a) => { nameTextBox.Focus(); nameTextBox.SelectAll(); });
				namePanel.Padding = new Padding(2, 2, 1, 1);
				namePanel.Cursor = Cursors.IBeam;
				namePanel.Name = "NamePanel" + i;

				namePanel.Controls.Add(names[i]);

				NumericTextBox maximumTextBox = new NumericTextBox();
				maximums[i] = maximumTextBox;
				maximums[i].Name = "MaxTextBox" + i;
				maximums[i].BorderStyle = BorderStyle.None;
				maximums[i].Dock = DockStyle.Fill;
				maximums[i].MaxLength = 10; //10 is the length of the max UInt32 value.
				maximums[i].Tag = i;

				maximums[i].TextChanged += new EventHandler(maximumTextBox_TextChanged);

				Panel maximumPanel = new Panel();
				maximumPanel.SetBounds(MAX_X, y, MAX_WIDTH - 1, BOX_HEIGHT - 1);
				maximumPanel.BorderStyle = BorderStyle.None;
				maximumPanel.GotFocus += new EventHandler((s, a) => { maximumTextBox.Focus(); maximumTextBox.SelectAll(); });
				maximumPanel.Click += new EventHandler((s, a) => { maximumTextBox.Focus(); maximumTextBox.SelectAll(); });
				maximumPanel.Padding = new Padding(2, 2, 1, 1);
				maximumPanel.Cursor = Cursors.IBeam;
				maximumPanel.Name = "MaxPanel" + i;

				maximumPanel.Controls.Add(maximums[i]);

				NumericTextBox bitsTextBox = new NumericTextBox();
				bits[i] = bitsTextBox;
				bits[i].Name = "MaxTextBox" + i;
				bits[i].BorderStyle = BorderStyle.None;
				bits[i].Dock = DockStyle.Fill;
				bits[i].Tag = i;
				bits[i].ReadOnly = true;

				Panel bitsPanel = new Panel();
				bitsPanel.SetBounds(BITS_X, y, BITS_WIDTH - 1, BOX_HEIGHT - 1);
				bitsPanel.BorderStyle = BorderStyle.None;
				bitsPanel.GotFocus += new EventHandler((s, a) => { bitsTextBox.Focus(); bitsTextBox.SelectAll(); });
				bitsPanel.Click += new EventHandler((s, a) => { bitsTextBox.Focus(); bitsTextBox.SelectAll(); });
				bitsPanel.Padding = new Padding(2, 2, 1, 1);
				bitsPanel.Cursor = Cursors.IBeam;
				bitsPanel.Name = "MaxPanel" + i;
				bitsPanel.BackColor = SystemColors.Control;

				bitsPanel.Controls.Add(bits[i]);

				this.Controls.Add(namePanel);
				this.Controls.Add(maximumPanel);
				this.Controls.Add(bitsPanel);
			}

			this.ResumeLayout(true);
		}

		[Description("Called when one of the names is changed.")]
		public event EventHandler NameTextBoxChanged;
		[Description("Called when one of the maximum values is changed.")]
		public event EventHandler MaximumTextBoxChanged;

		private void nameTextBox_TextChanged(object sender, EventArgs e) {
			if (NameTextBoxChanged != null) {
				NameTextBoxChanged(this, e);
			}
		}

		private void maximumTextBox_TextChanged(object sender, EventArgs e) {
			if (MaximumTextBoxChanged != null) {
				MaximumTextBoxChanged(this, e);
			}

			NumericTextBox textBox = sender as NumericTextBox;
			if (textBox != null) {
				int index = (int)textBox.Tag;

				if (String.IsNullOrEmpty(textBox.Text)) {
					bits[index].Value = 1;
					return;
				}

				bits[index].Value = GetNumberOfBits(textBox.Value);
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			const int LEFT_X = 2, TOP_Y = 2; //TopLeft coords (due to border).
			const int BOX_HEIGHT = 19;
			const int NUMBER_X = LEFT_X + 0, NUMBER_WIDTH = 21;
			const int NAME_X = LEFT_X + 21, NAME_WIDTH = 121;
			const int MAX_X = LEFT_X + 142, MAX_WIDTH = 50;
			const int BITS_X = LEFT_X + 192, BITS_WIDTH = 45;
			const int DATA_Y = TOP_Y + BOX_HEIGHT;

			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(Point.Empty, this.Size), Border3DStyle.Sunken, 
				Border3DSide.Bottom | Border3DSide.Right | Border3DSide.Top | Border3DSide.Left);

			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			DrawTextRect(e.Graphics, "", NUMBER_X, TOP_Y, NUMBER_WIDTH, BOX_HEIGHT, format);
			DrawTextRect(e.Graphics, "Name", NAME_X, TOP_Y, NAME_WIDTH, BOX_HEIGHT, format);
			DrawTextRect(e.Graphics, "Max", MAX_X, TOP_Y, MAX_WIDTH, BOX_HEIGHT, format);
			DrawTextRect(e.Graphics, "Bits", BITS_X, TOP_Y, BITS_WIDTH, BOX_HEIGHT, format);

			if (properties == null) { return; }

			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;

			for (int i = 0; i < properties.Length; i++) {
				int y = (DATA_Y + (i * BOX_HEIGHT));
				DrawTextRect(e.Graphics, (i + 1).ToString(), NUMBER_X, y, NUMBER_WIDTH, BOX_HEIGHT, format);
				DrawEditRect(e.Graphics, NAME_X, y, NAME_WIDTH, BOX_HEIGHT);
				DrawEditRect(e.Graphics, MAX_X, y, MAX_WIDTH, BOX_HEIGHT);
				DrawEditRect(e.Graphics, BITS_X, y, BITS_WIDTH, BOX_HEIGHT);
			}

			base.OnPaint(e);
		}

		private void DrawTextRect(Graphics g, String text, int x, int y, int width, int height, StringFormat format) {
			Rectangle OuterBorderRect = new Rectangle(x, y, width, height);
			Rectangle InnerBorderRect = new Rectangle(x, y, width - 1, height - 1);
			Rectangle TextRect = new Rectangle(x + 0, y + 1, width - 2, height - 3);

			g.FillRectangle(SystemBrushes.Control, OuterBorderRect);

			BorderPaint.DrawBorderFull(g, OuterBorderRect, Color.Black, Border3DSide.Bottom | Border3DSide.Right);
			BorderPaint.DrawBorderClipped(g, InnerBorderRect, Color.White, Border3DSide.Left | Border3DSide.Top);
			BorderPaint.DrawBorderClipped(g, InnerBorderRect, SystemColors.ControlDark, Border3DSide.Right | Border3DSide.Bottom);

			g.DrawString(text, this.Font, SystemBrushes.ControlText, TextRect, format);
		}

		private void DrawEditRect(Graphics g, int x, int y, int width, int height) {
			Color color = Color.FromArgb(192, 192, 192);

			BorderPaint.DrawBorderFull(g, new Rectangle(x, y, width, height), color, Border3DSide.Right | Border3DSide.Bottom);
		}
	}
}
