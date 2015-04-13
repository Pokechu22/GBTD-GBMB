using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBMFile;
using GB.Shared.Controls;

namespace GB.GBMB.Dialogs
{
	internal partial class DefaultLocationPropertiesEditControl : UserControl
	{
		protected override Size DefaultSize { get { return new Size(201, 169); } }

		private GBMObjectMapProperties properties;
		private GBMObjectDefaultTilePropertyValues defaultProperties;
		private UInt16 selectedTile;

		public GBMObjectMapProperties Properties {
			get { return properties; }
			set { properties = value; RecreateTextBoxes(); this.Invalidate(true); }
		}
		public GBMObjectDefaultTilePropertyValues DefaultProperties {
			get { return defaultProperties; }
			set { defaultProperties = value; LoadTile(selectedTile); this.Invalidate(true); }
		}

		/// <summary>
		/// The selected tile.  Before changing, check that the data is valid!
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public UInt16 SelectedTile {
			get { return selectedTile; }
			set {
				if (!IsValid()) {
					throw new Exception("One of the textboxes is invalid; abort tile change!");
				}
				
				SelectedTileChanged(selectedTile, value);

				selectedTile = value;
			}
		}

		public DefaultLocationPropertiesEditControl() {
			InitializeComponent();
		}

		private NumericTextBox[] textBoxes = new NumericTextBox[0];

		private void RecreateTextBoxes() {
			const int BOX_WIDTH = 62, BOX_HEIGHT = 19;
			const int BOX_X = 135;

			if (properties != null) {
				foreach (Control c in textBoxes) { c.Dispose(); }

				textBoxes = new NumericTextBox[properties.Master.PropCount];

				this.SuspendLayout();

				for (int i = 0; i < textBoxes.Length; i++) {
					int y = (i + 1) * BOX_HEIGHT;

					NumericTextBox t = new NumericTextBox();
					
					Panel panel = new Panel();
					panel.Location = new Point(BOX_X, y);
					panel.Size = new Size(BOX_WIDTH, BOX_HEIGHT);
					panel.BorderStyle = BorderStyle.None;
					panel.GotFocus += new EventHandler((s, a) => { t.Focus(); t.SelectAll(); });
					panel.Click += new EventHandler((s, a) => { t.Focus(); t.SelectAll(); });
					panel.Padding = new Padding(2, 2, 2, 2);
					panel.Cursor = Cursors.IBeam;
					t.Name = "PropPanel" + i;

					t.Dock = DockStyle.Fill;
					t.BorderStyle = BorderStyle.None;
					t.Value = 0;
					t.TabStop = false;
					t.Name = "PropTextBox" + i;
					t.Tag = properties.Properties[i].Name;

					textBoxes[i] = t;

					panel.Controls.Add(textBoxes[i]);
					this.Controls.Add(panel);
				}

				this.ResumeLayout();
			}
		}

		private void LoadTile(UInt16 tile) {
			for (int i = 0; i < textBoxes.Length; i++) {
				defaultProperties.Data[tile, i] = (UInt16)textBoxes[i].Value;
			}
		}
		private void SelectedTileChanged(UInt16 oldTile, UInt16 newTile) {
			for (int i = 0; i < textBoxes.Length; i++) {
				defaultProperties.Data[oldTile, i] = (UInt16)textBoxes[i].Value;
			}
			for (int i = 0; i < textBoxes.Length; i++) {
				textBoxes[i].Value = defaultProperties.Data[newTile, i];
			}
		}

		/// <summary>
		/// Saves the current data (if it is valid).
		/// </summary>
		public void SaveCurrent() {
			if (IsValid()) {
				for (int i = 0; i < textBoxes.Length; i++) {
					defaultProperties.Data[selectedTile, i] = (UInt16)textBoxes[i].Value;
				}
			}
		}

		/// <summary>
		/// Checks if the data is valid.  If the data is invalid, an error message is displayed.
		/// </summary>
		/// <returns></returns>
		public bool IsValid() {
			if (properties == null) {
				throw new WarningException("Cannot validate when properties is null -- this should not happen!");
			}
			if (defaultProperties == null) {
				throw new WarningException("Cannot validate when defaultProperties is null -- this should not happen!");
			}

			for (int i = 0; i < textBoxes.Length; i++) {
				if (String.IsNullOrWhiteSpace(textBoxes[i].Text)) {
					//TODO A "is required" message?
					MessageBox.Show(textBoxes[i].Tag + " should be at least 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				if (textBoxes[i].Value < 0) {
					MessageBox.Show(textBoxes[i].Tag + " should be at least 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
				if (textBoxes[i].Value > properties.Properties[i].MaxValue) {
					MessageBox.Show(textBoxes[i].Tag + " should be lower or equal to " + properties.Properties[i].MaxValue + ".", 
						"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}
			return true;
		}

		protected override void OnPaint(PaintEventArgs e) {
			const int BOX_HEIGHT = 19;
			const int PROPERTY_X = 0, PROPERTY_WIDTH = 135;
			const int VALUE_X = 135, VALUE_WIDTH = 62;

			base.OnPaint(e);

			DrawTextRect(e.Graphics, "Property", PROPERTY_X, 0, PROPERTY_WIDTH, BOX_HEIGHT);
			DrawTextRect(e.Graphics, "Value", VALUE_X, 0, VALUE_WIDTH, BOX_HEIGHT);

			if (properties == null || defaultProperties == null) { return; }

			for (int i = 0; i < properties.Master.PropCount; i++) {
				int y = (i + 1) * BOX_HEIGHT;

				DrawTextRect(e.Graphics, properties.Properties[i].Name, PROPERTY_X, y, PROPERTY_WIDTH, BOX_HEIGHT);
				DrawEditRect(e.Graphics, VALUE_X, y, VALUE_WIDTH, BOX_HEIGHT);
			}
		}

		private void DrawTextRect(Graphics g, String text, int x, int y, int width, int height) {
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

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
