using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace GB.Shared.Controls
{
	/// <summary>
	/// Check box that displays an image.
	/// 
	/// Uses 3 foreground images and one background image: Clicked, Hovered, nonhovered, and selectedbackground.
	/// 
	/// A practical copy-paste of the radio button code.  I'm not sure of the right way of actually doing this.
	/// </summary>
	public class ImageCheckBox : Control
	{
		private bool mouseInside = false;
		private bool MouseInside {
			get { return mouseInside; }
			set {
				mouseInside = value;
				this.Invalidate(true);
			}
		}
		private bool mouseDown = false;
		private bool IsMouseDown {
			get { return mouseDown; }
			set {
				mouseDown = value;
				this.Invalidate(true);
			}
		}
		private bool @checked;
		public bool Checked {
			get { return @checked; }
			set {
				if (this.@checked != value) {
					@checked = value; this.OnCheckedChanged(new EventArgs()); this.Invalidate(true);
				}
			}
		}

		private Image nonhoveredImage = new Bitmap(16, 16);
		private Image hoveredImage = new Bitmap(16, 16);

		public event EventHandler CheckedChanged;
		protected virtual void OnCheckedChanged(EventArgs e) {
			if (CheckedChanged != null) {
				CheckedChanged(this, e);
			}

			this.Invalidate(true);
		}

		[Category("Appearance"), Description("The image to use when not hovered over.")]
		public Image NonhoveredImage {
			get { return nonhoveredImage; }
			set { if (value == null) { value = new Bitmap(16, 16); } nonhoveredImage = value; this.Invalidate(true); }
		}
		[Category("Appearance"), Description("The image to use when hovered over.")]
		public Image HoveredImage {
			get { return hoveredImage; }
			set { if (value == null) { value = new Bitmap(16, 16); } hoveredImage = value; this.Invalidate(true); }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		[Description("Irrelivant for this control.")]
		[DefaultValue("")]
		public override string Text {
			get { return base.Text; }
			set { base.Text = value; }
		}

		protected override Padding DefaultPadding {
			get { return new Padding(0, 0, 1, 1); }
		}

		public ImageCheckBox()
			: base() {
			AutoSize = false;
			this.SetStyle(ControlStyles.StandardDoubleClick, false);
		}

		protected override void OnMouseEnter(EventArgs e) {
			MouseInside = (new Rectangle(new Point(0, 0), this.Size).Contains(PointToClient(MousePosition)));

			base.OnMouseEnter(e);
		}
		protected override void OnMouseLeave(EventArgs e) {
			MouseInside = (new Rectangle(new Point(0, 0), this.Size).Contains(PointToClient(MousePosition)));

			base.OnMouseLeave(e);
		}
		protected override void OnMouseMove(MouseEventArgs e) {
			MouseInside = (new Rectangle(new Point(0, 0), this.Size).Contains(PointToClient(MousePosition)));

			base.OnMouseMove(e);
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
		protected override void OnClick(EventArgs e) {
			this.Checked ^= true;

			base.OnClick(e);
		}

		//Border painting.
		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

			//TODO enabled code
			if (Checked) {
				if (mouseDown) {
					if (mouseInside) {
						e.Graphics.DrawImageUnscaled(hoveredImage,
							(this.Width / 2) - (hoveredImage.Width / 2) + 1, (this.Height / 2) - (hoveredImage.Height / 2) + 1);
					} else {
						e.Graphics.DrawImageUnscaled(hoveredImage,
							(this.Width / 2) - (hoveredImage.Width / 2) + 1, (this.Height / 2) - (hoveredImage.Height / 2) + 1);
					}
				} else {
					if (mouseInside) {
						e.Graphics.DrawImageUnscaled(hoveredImage,
							(this.Width / 2) - (hoveredImage.Width / 2) + 1, (this.Height / 2) - (hoveredImage.Height / 2) + 1);
					} else {
						paintSelectedBackground(e);
						e.Graphics.DrawImageUnscaled(hoveredImage,
							(this.Width / 2) - (hoveredImage.Width / 2) + 1, (this.Height / 2) - (hoveredImage.Height / 2) + 1);
					}
				}

				if (mouseDown) {
					if (mouseInside) {
						ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenOuter);
					} else {
						ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenOuter);
					}
				} else {
					if (mouseInside) {
						ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenOuter);
					} else {
						ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenOuter);
					}
				}
			} else {
				if (mouseDown) {
					if (mouseInside) {
						e.Graphics.DrawImageUnscaled(hoveredImage,
							(this.Width / 2) - (hoveredImage.Width / 2) + 1, (this.Height / 2) - (hoveredImage.Height / 2) + 1);
					} else {
						e.Graphics.DrawImageUnscaled(hoveredImage,
							(this.Width / 2) - (hoveredImage.Width / 2), (this.Height / 2) - (hoveredImage.Height / 2));
					}
				} else {
					if (mouseInside) {
						e.Graphics.DrawImageUnscaled(hoveredImage,
							(this.Width / 2) - (hoveredImage.Width / 2), (this.Height / 2) - (hoveredImage.Height / 2));
					} else {
						e.Graphics.DrawImageUnscaled(nonhoveredImage,
							(this.Width / 2) - (nonhoveredImage.Width / 2), (this.Height / 2) - (nonhoveredImage.Height / 2));
					}
				}

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

			base.OnPaint(e);
		}

		private void paintSelectedBackground(PaintEventArgs e) {
			if (e.ClipRectangle.Width <= 0 || e.ClipRectangle.Height <= 0) {
				return; //This happens sometimes.
			}
			using (Bitmap b = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height)) {
				for (int x = 0; x < b.Width; x++) {
					for (int y = 0; y < b.Height; y++) {
						if (((x ^ y) & 0x01) == 0) {
							b.SetPixel(x, y, Color.White);
						} else {
							b.SetPixel(x, y, Color.Transparent);
						}
					}
				}

				e.Graphics.DrawImageUnscaled(b, e.ClipRectangle.Location);
			}
		}
	}
}
