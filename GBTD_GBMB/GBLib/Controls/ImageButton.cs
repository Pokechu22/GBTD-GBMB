using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace GB.Shared.Controls
{
	/// <summary>
	/// Button that provides several images.
	/// 
	/// It has three images: Mouse over, mouse not over; mouse down and over.
	/// </summary>
	public class ImageButton : Button
	{
		private bool mouseInside = false;
		private bool MouseInside {
			get { return mouseInside; }
			set { mouseInside = value; UpdateImage(); }
		}
		private bool mouseDown = false;
		private bool IsMouseDown {
			get { return mouseDown; }
			set { mouseDown = value; UpdateImage(); }
		}

		private Image nonhoveredImage = new Bitmap(16, 16);
		private Image hoveredImage = new Bitmap(16, 16);
		private Image pressedImage = new Bitmap(16, 16);

		[Category("Appearance"), Description("The image to use when not hovered over.")]
		public Image NonhoveredImage {
			get { return nonhoveredImage; }
			set { if (value == null) { value = new Bitmap(16, 16); } nonhoveredImage = value; UpdateImage(); }
		}
		[Category("Appearance"), Description("The image to use when hovered over.")]
		public Image HoveredImage {
			get { return hoveredImage; }
			set { if (value == null) { value = new Bitmap(16, 16); } hoveredImage = value; UpdateImage(); }
		}
		[Category("Appearance"), Description("The image to use when pressed.")]
		public Image PressedImage {
			get { return pressedImage; }
			set { if (value == null) { value = new Bitmap(16, 16); } pressedImage = value; UpdateImage(); }
		}

		[DefaultValue("")]
		public override string Text {
			get { return base.Text; }
			set { base.Text = value; }
		}

		protected override Padding DefaultPadding {
			get { return new Padding(0, 0, 1, 1); }
		}

		public ImageButton()
			: base() {
			AutoSize = false;
		}

		protected override void OnMouseEnter(EventArgs eventargs) {
			MouseInside = true;
			base.OnMouseEnter(eventargs);
		}
		protected override void OnMouseLeave(EventArgs eventargs) {
			IsMouseDown = false;
			MouseInside = false;
			base.OnMouseLeave(eventargs);
		}
		protected override void OnMouseDown(MouseEventArgs e) {
			if (e.Button.HasFlag(MouseButtons.Left)) {
				IsMouseDown = true;
			}
			base.OnMouseDown(e);
		}
		protected override void OnMouseUp(MouseEventArgs e) {
			if (e.Button.HasFlag(MouseButtons.Left)) {
				IsMouseDown = false;
			}
			base.OnMouseUp(e);
		}

		protected void UpdateImage() {
			if (mouseDown) {
				if (mouseInside) {
					this.Image = pressedImage;
				} else {
					this.Image = hoveredImage;
				}
			} else {
				if (mouseInside) {
					this.Image = hoveredImage;
				} else {
					this.Image = nonhoveredImage;
				}
			}
			this.Invalidate(true);
		}

		//Border painting.
		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);

			if (mouseDown) {
				if (mouseInside) {
					ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenOuter);
				} else {
					ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.RaisedInner);
				}
			} else {
				if (mouseInside) {
					ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.RaisedInner);
				} else {
					//Do nothing.
				}
			}
		}
	}
}
