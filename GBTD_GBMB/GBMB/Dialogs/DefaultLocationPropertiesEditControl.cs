using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBMFile;

namespace GB.GBMB.Dialogs
{
	internal partial class DefaultLocationPropertiesEditControl : UserControl
	{
		protected override Size DefaultSize { get { return new Size(201, 169); } }

		private GBMObjectMapProperties properties;
		private GBMObjectDefaultTilePropertyValues defaultProperties;

		public GBMObjectMapProperties Properties {
			get { return properties; }
			set { properties = value; this.Invalidate(true); }
		}
		public GBMObjectDefaultTilePropertyValues DefaultProperties {
			get { return defaultProperties; }
			set { defaultProperties = value; this.Invalidate(true); }
		}

		public DefaultLocationPropertiesEditControl() {
			InitializeComponent();
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
	}
}
