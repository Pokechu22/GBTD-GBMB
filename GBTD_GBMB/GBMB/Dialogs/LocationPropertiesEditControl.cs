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

		private GBMObjectMapPropertiesRecord[] properties;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public GBMObjectMapPropertiesRecord[] Properties {
			get { SaveChanges(); return properties; }
			set { properties = value; LoadChanges(); this.Invalidate(); }
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
				properties[i].Name = names[i].Text;
				properties[i].MaxValue = maximums[i].Value;
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
			names = new TextBox[properties.Length];
			maximums = new NumericTextBox[properties.Length];
			bits = new NumericTextBox[properties.Length];

			this.Controls.Clear();

			for (int i = 0; i < properties.Length; i++) {
				int y = DATA_Y + (i * BOX_HEIGHT);

				names[i] = new TextBox();
				names[i].Name = "PropTextBox" + i;
				names[i].Top = y;
				names[i].Left = NAME_X;
				names[i].Width = NAME_WIDTH;
				names[i].Height = BOX_HEIGHT;
				names[i].BorderStyle = BorderStyle.None;
				names[i].AutoSize = false;
				names[i].Tag = i;

				maximums[i] = new NumericTextBox();
				maximums[i].Name = "MaxTextBox" + i;
				maximums[i].Top = y;
				maximums[i].Left = MAX_X;
				maximums[i].Width = MAX_WIDTH;
				maximums[i].Height = BOX_HEIGHT;
				maximums[i].BorderStyle = BorderStyle.None;
				maximums[i].AutoSize = false;
				maximums[i].Tag = i;

				bits[i] = new NumericTextBox();
				bits[i].Name = "BitsTextBox" + i;
				bits[i].Top = y;
				bits[i].Left = BITS_X;
				bits[i].Width = BITS_WIDTH;
				bits[i].Height = BOX_HEIGHT;
				bits[i].BorderStyle = BorderStyle.None;
				bits[i].AutoSize = false;
				bits[i].ReadOnly = true;
				bits[i].Tag = i;

				this.Controls.Add(names[i]);
				this.Controls.Add(maximums[i]);
				this.Controls.Add(bits[i]);
			}

			this.ResumeLayout(true);
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
