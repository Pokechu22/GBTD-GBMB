using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace GB.Shared.Controls
{
	public class Border : Control
	{
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[Description("This is not useful for this control")]
		public override string Text { get { return base.Text; } set { base.Text = value; } }

		protected override Padding DefaultMargin {
			get { return new Padding(0, 0, 0, 0); }
		}

		public Border() {
			this.AllBorders = Border3DStyle.Raised;
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			SetStyle(ControlStyles.ResizeRedraw, true);
		}

		private Border3DStyle? leftBorder, rightBorder, topBorder, bottomBorder;

		private Border3DSide[] drawOrder = new Border3DSide[4] {
			Border3DSide.Top, Border3DSide.Right, Border3DSide.Left, Border3DSide.Bottom
		};

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(true)]
		[Category("Display"), Description("Gets or sets all of the values.")]
		public Border3DStyle? AllBorders {
			set {
				if (value.HasValue) {
					leftBorder = rightBorder = topBorder = bottomBorder = value;
				} else {
					leftBorder = rightBorder = topBorder = bottomBorder = null;
				}
				Invalidate(true);
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
			set { leftBorder = value; Invalidate(true); }
		}
		[Category("Display"), Description("Controls the right border.")]
		public Border3DStyle? RightBorder {
			get { return rightBorder; }
			set { rightBorder = value; Invalidate(true); }
		}
		[Category("Display"), Description("Controls the top border.")]
		public Border3DStyle? TopBorder {
			get { return topBorder; }
			set { topBorder = value; Invalidate(true); }
		}
		[Category("Display"), Description("Controls the bottom border.")]
		public Border3DStyle? BottomBorder {
			get { return bottomBorder; }
			set { bottomBorder = value; Invalidate(true); }
		}

		[Category("Display"), Description("Controls the order in which the sides are painted.")]
		public Border3DSide[] DrawOrder {
			set { if (value == null) { throw new ArgumentNullException(); } drawOrder = value; }
			get { return drawOrder; }
		}

		[ReadOnly(true), Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Display"), Description("Indexer for individual Border3DStyles by side.")]
		public Border3DStyle? this[Border3DSide side] {
			get {
				switch (side) {
				case Border3DSide.Top: return this.topBorder;
				case Border3DSide.Left: return this.leftBorder;
				case Border3DSide.Right: return this.rightBorder;
				case Border3DSide.Bottom: return this.bottomBorder;
				case Border3DSide.All: return this.AllBorders;
				default: return null;
				}
			}
			set {
				switch (side) {
				case Border3DSide.Top: this.topBorder = value; return;
				case Border3DSide.Left: this.leftBorder = value; return;
				case Border3DSide.Right: this.rightBorder = value; return;
				case Border3DSide.Bottom: this.bottomBorder = value; return;
				case Border3DSide.All: this.AllBorders = value; return;
				default: return;
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e) {
			foreach (Border3DSide side in this.drawOrder) {
				Border3DStyle? style = this[side];
				if (style.HasValue) { ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, this.Width, this.Height), style.Value, side); }
			}

			base.OnPaint(e);
		}
	}
}
