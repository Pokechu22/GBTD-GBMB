#define UGLY_BUT_CORRECT_BITS_SIZE //The origional app has it 1 pixel too far.

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
	internal class ExportPropertiesEditControl : Control
	{
		protected override Size DefaultSize { get { return new Size(193, 193); } }

		private GBMObjectMapPropertiesRecord[] properties = new GBMObjectMapPropertiesRecord[0];
		private GBMObjectMapExportPropertiesRecord[] exportProperties = new GBMObjectMapExportPropertiesRecord[0];

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false), ReadOnly(true)]
		public GBMObjectMapPropertiesRecord[] Properties {
			get { return properties; }
			set {
				if (value == null) {
					value = new GBMObjectMapPropertiesRecord[0];
				} else {
					value = (GBMObjectMapPropertiesRecord[])value.Clone();
				}
				properties = value;
				RegenerateControls();
				this.Invalidate(true);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false), ReadOnly(true)]
		public GBMObjectMapExportPropertiesRecord[] ExportProperties {
			get { SaveChanges(); return exportProperties; }
			set {
				if (value == null) {
					value = new GBMObjectMapExportPropertiesRecord[0];
				} else {
					value = (GBMObjectMapExportPropertiesRecord[])value.Clone();
				}
				exportProperties = value;
				LoadChanges();
				this.Invalidate(true);
			}
		}

		private ComboBox[] propertyBoxes = new ComboBox[0];
		private NumericTextBox[] bits = new NumericTextBox[0];

		public ExportPropertiesEditControl() {
			SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
		}

		volatile bool loadingOrSaving = false;

		public void SaveChanges() {
			if (loadingOrSaving) { return; }

			loadingOrSaving = true;
			
			exportProperties = new GBMObjectMapExportPropertiesRecord[propertyBoxes.Length];

			for (int i = 0; i < exportProperties.Length; i++) {
				exportProperties[i] = new GBMObjectMapExportPropertiesRecord();

				exportProperties[i].Property = (byte)propertyBoxes[i].SelectedIndex;
				exportProperties[i].Size = bits[i].Value;
			}

			loadingOrSaving = false;
		}

		public void LoadChanges() {
			if (loadingOrSaving) { return; }

			loadingOrSaving = true;

			//If the size changed and everything needs to be resized.
			if (propertyBoxes.Length != properties.Length) {
				RegenerateControls();
			}

			for (int i = 0; i < exportProperties.Length; i++) {
				propertyBoxes[i].SelectedIndex = exportProperties[i].Property;
				bits[i].Value = exportProperties[i].Size;
			}

			loadingOrSaving = false;
		}

		private void RegenerateControls() {
			const int LEFT_X = 2, TOP_Y = 2; //TopLeft coords (due to border).
			const int BOX_HEIGHT = 19;
			const int PROPERTY_X = LEFT_X + 21, PROPERTY_WIDTH = 121;
#if UGLY_BUT_CORRECT_BITS_SIZE
			const int BITS_X = LEFT_X + 142, BITS_WIDTH = 48;
#else
			const int BITS_X = LEFT_X + 142, BITS_WIDTH = 47;
#endif
			const int DATA_Y = TOP_Y + BOX_HEIGHT;

			this.SuspendLayout();

			foreach (Control c in propertyBoxes) { c.Dispose(); }
			foreach (Control c in bits) { c.Dispose(); }

			this.Controls.Clear();

			propertyBoxes = new ComboBox[exportProperties.Length];
			bits = new NumericTextBox[exportProperties.Length];

			for (int i = 0; i < exportProperties.Length; i++) {
				int y = DATA_Y + (i * BOX_HEIGHT);

				ComboBox propertyTextBox = new ComboBox();
				propertyBoxes[i] = propertyTextBox;
				propertyBoxes[i].SetBounds(PROPERTY_X, y, PROPERTY_WIDTH - 1, BOX_HEIGHT - 1);
				propertyBoxes[i].Name = "NameTextBox" + i;
				propertyBoxes[i].MaxLength = 31;
				propertyBoxes[i].Tag = i;
				propertyBoxes[i].Items.AddRange(new Object[] {
					"",
					"[Tile number]",
					"[Tile number: Low 8]",
					"[Tile number: High 9]",
					"[Vertical flip]",
					"[Horiztontal flip]",
					"[GBC Palette]",
					"[SGB Palette]",
					"[GBC BG Attribute]",
					"[0 filler]",
					"[1 filler]"
				});
				if (properties != null) {
					propertyBoxes[i].Items.AddRange(properties.Select(r => r.Name).ToArray());
				}
				propertyBoxes[i].SelectedIndexChanged += new EventHandler(propComboBox_SelectedIndexChanged);

				NumericTextBox bitsTextBox = new NumericTextBox();
				bits[i] = bitsTextBox;
				bits[i].Name = "MaxTextBox" + i;
				bits[i].BorderStyle = BorderStyle.None;
				bits[i].Dock = DockStyle.Fill;
				bits[i].Tag = i;

				bits[i].TextChanged += new EventHandler(bitsTextBox_TextChanged);

				Panel bitsPanel = new Panel();
				bitsPanel.SetBounds(BITS_X, y, BITS_WIDTH - 1, BOX_HEIGHT - 1);
				bitsPanel.BorderStyle = BorderStyle.None;
				bitsPanel.GotFocus += new EventHandler((s, a) => { bitsTextBox.Focus(); bitsTextBox.SelectAll(); });
				bitsPanel.Click += new EventHandler((s, a) => { bitsTextBox.Focus(); bitsTextBox.SelectAll(); });
				bitsPanel.Cursor = Cursors.IBeam;
				bitsPanel.Padding = new Padding(2, 2, 1, 1);
				bitsPanel.Name = "MaxPanel" + i;
				bitsPanel.BackColor = SystemColors.Control;

				bitsPanel.Controls.Add(bits[i]);

				this.Controls.Add(propertyBoxes[i]);
				this.Controls.Add(bitsPanel);
			}

			this.ResumeLayout(true);

			this.Invalidate(true);
		}

		void propertyPanel_Paint(object sender, PaintEventArgs e) {
			Control control = sender as Control;
			BorderPaint.DrawBorderFull(e.Graphics, new Rectangle(0, 0, control.Width, control.Height), Color.White,
				Border3DSide.Left | Border3DSide.Right | Border3DSide.Top | Border3DSide.Bottom);
			BorderPaint.DrawBorderFull(e.Graphics, new Rectangle(1, 1, control.Width - 2, control.Height - 2), Color.White,
				Border3DSide.Left | Border3DSide.Right | Border3DSide.Top | Border3DSide.Bottom);

			DrawEditRect(e.Graphics, 0, 0, control.Width, control.Height);
		}

		[Description("Called when one of the selected properties is changed.")]
		public event EventHandler PropertyComboBoxChanged;
		[Description("Called when one of the maximum values is changed.")]
		public event EventHandler BitsTextBoxChanged;
		[Description("Fires when the number of entries has changed or the amount of bits in one entry has changed.")]
		public event EventHandler SizeOrCountChanged;

		void propComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (loadingOrSaving) { return; }

			if (PropertyComboBoxChanged != null) {
				PropertyComboBoxChanged(this, new EventArgs());
			}
			if (SizeOrCountChanged != null) {
				SizeOrCountChanged(this, new EventArgs());
			}
		}

		private void bitsTextBox_TextChanged(object sender, EventArgs e) {
			if (loadingOrSaving) { return; }

			if (BitsTextBoxChanged != null) {
				BitsTextBoxChanged(this, e);
			}
			if (SizeOrCountChanged != null) {
				SizeOrCountChanged(this, new EventArgs());
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			const int LEFT_X = 2, TOP_Y = 2; //TopLeft coords (due to border).
			const int BOX_HEIGHT = 19;
			const int NUMBER_X = LEFT_X + 0, NUMBER_WIDTH = 21;
			const int PROPERTY_X = LEFT_X + 21, PROPERTY_WIDTH = 121;
#if UGLY_BUT_CORRECT_BITS_SIZE
			const int BITS_X = LEFT_X + 142, BITS_WIDTH = 48;
#else
			const int BITS_X = LEFT_X + 142, BITS_WIDTH = 47;
#endif
			const int DATA_Y = TOP_Y + BOX_HEIGHT;

			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			DrawTextRect(e.Graphics, "", NUMBER_X, TOP_Y, NUMBER_WIDTH, BOX_HEIGHT, format);
			DrawTextRect(e.Graphics, "Property", PROPERTY_X, TOP_Y, PROPERTY_WIDTH, BOX_HEIGHT, format);
			DrawTextRect(e.Graphics, "Bits", BITS_X, TOP_Y, BITS_WIDTH, BOX_HEIGHT, format);

			if (properties == null) { return; }

			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;

			for (int i = 0; i < exportProperties.Length; i++) {
				int y = (DATA_Y + (i * BOX_HEIGHT));
				DrawTextRect(e.Graphics, (i + 1).ToString(), NUMBER_X, y, NUMBER_WIDTH, BOX_HEIGHT, format);
				DrawEditRect(e.Graphics, PROPERTY_X, y, PROPERTY_WIDTH, BOX_HEIGHT);
				DrawEditRect(e.Graphics, BITS_X, y, BITS_WIDTH, BOX_HEIGHT);
			}

			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(Point.Empty, this.Size), Border3DStyle.Sunken,
				Border3DSide.Bottom | Border3DSide.Right | Border3DSide.Top | Border3DSide.Left);

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

		public void AddRow() {
			SaveChanges();
			GBMObjectMapPropertiesRecord[] newProps = new GBMObjectMapPropertiesRecord[properties.Length + 1];

			for (int i = 0; i < newProps.Length; i++) {
				if (i < properties.Length) {
					newProps[i] = properties[i];
				} else {
					newProps[i] = new GBMObjectMapPropertiesRecord();
				}
			}

			properties = newProps;
			LoadChanges();

			if (SizeOrCountChanged != null) {
				SizeOrCountChanged(this, new EventArgs());
			}
		}

		public void RemoveRow() {
			if (properties.Length == 0) {
				throw new InvalidOperationException("Cannot decrease the number of properties below 0!");
			}

			SaveChanges();
			GBMObjectMapPropertiesRecord[] newProps = new GBMObjectMapPropertiesRecord[properties.Length - 1];

			for (int i = 0; i < newProps.Length; i++) {
				if (i < properties.Length) {
					newProps[i] = properties[i];
				} else {
					newProps[i] = new GBMObjectMapPropertiesRecord();
				}
			}

			properties = newProps;
			LoadChanges();

			if (SizeOrCountChanged != null) {
				SizeOrCountChanged(this, new EventArgs());
			}
		}
	}
}
