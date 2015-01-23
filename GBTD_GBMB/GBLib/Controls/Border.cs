using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace GB.Shared.Controls
{
	public class Border : Control
	{
		public Border() {
			this.AllBorders = Border3DStyle.Raised;
		}

		private Border3DStyle? leftBorder, rightBorder, topBorder, bottomBorder;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(true)]
		[Category("Display"), Description("Gets or sets all of the values.")]
		public Border3DStyle? AllBorders {
			set {
				if (value.HasValue) {
					leftBorder = rightBorder = topBorder = bottomBorder = value;
				} else {
					leftBorder = rightBorder = topBorder = bottomBorder = null;
				}
				Invalidate();
			}
			get {
				if (leftBorder == rightBorder &&
					leftBorder == topBorder &&
					leftBorder == bottomBorder) {
					return leftBorder;
				} else {
					return null;
				}
			}
		}
		[Category("Display"), Description("Controls the left border.")]
		public Border3DStyle? LeftBorder {
			get { return leftBorder; }
			set { leftBorder = value; Invalidate(); }
		}
		[Category("Display"), Description("Controls the right border.")]
		public Border3DStyle? RightBorder {
			get { return rightBorder; }
			set { rightBorder = value; Invalidate(); }
		}
		[Category("Display"), Description("Controls the top border.")]
		public Border3DStyle? TopBorder {
			get { return topBorder; }
			set { topBorder = value; Invalidate(); }
		}
		[Category("Display"), Description("Controls the bottom border.")]
		public Border3DStyle? BottomBorder {
			get { return bottomBorder; }
			set { bottomBorder = value; Invalidate(); }
		}

		protected override void OnPaint(PaintEventArgs e) {
			if (leftBorder.HasValue) { ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, leftBorder.Value, Border3DSide.Left); }
			if (rightBorder.HasValue) { ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, rightBorder.Value, Border3DSide.Right); }
			if (topBorder.HasValue) { ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, topBorder.Value, Border3DSide.Top); }
			if (bottomBorder.HasValue) { ControlPaint.DrawBorder3D(e.Graphics, e.ClipRectangle, bottomBorder.Value, Border3DSide.Bottom); }

			base.OnPaint(e);
		}
	}
}
