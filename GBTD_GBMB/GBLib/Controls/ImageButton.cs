﻿using System;
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
	public class ImageButton : Control
	{
		protected override Size DefaultSize { get { return new Size(24, 24); } }

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

		private Image nonhoveredImage = new Bitmap(16, 16);
		private Image hoveredImage = new Bitmap(16, 16);

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

		public ImageButton()
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

		//Border painting.
		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

			if (Enabled) {
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
			} else {
				//Silly method to draw an image as a disabled style, in the simple, monochrome method.
				//DrawImageDisabled usually makes things grayscale instead, unfortunately.
				using (Bitmap light = MakeMonochrome(nonhoveredImage, Color.FromArgb(255, 255, 255))) {
					e.Graphics.DrawImageUnscaled(light,
							(this.Width / 2) - (hoveredImage.Width / 2) + 1, (this.Height / 2) - (hoveredImage.Height / 2) + 1);
				}
				using (Bitmap dark = MakeMonochrome(nonhoveredImage, Color.FromArgb(128, 128, 128))) {
					e.Graphics.DrawImageUnscaled(dark,
							(this.Width / 2) - (hoveredImage.Width / 2), (this.Height / 2) - (hoveredImage.Height / 2));
				}
			}

			base.OnPaint(e);
		}

		/// <summary>
		/// Creates a monochrome version of a bitmap - All non-transparent pixels are paletteData to a single color.
		/// This method is NOT efficiant whatsoever, but it's good enough with 16x16 pixels.
		/// This only exists because the full DrawImageDisabled code is private.
		/// </summary>
		private Bitmap MakeMonochrome(Image image, Color c) {
			Bitmap returned = new Bitmap(image.Width, image.Height);
			returned.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (Bitmap bmp = new Bitmap(image)) {
				for (int x = 0; x < bmp.Width; x++) {
					for (int y = 0; y < bmp.Height; y++) {
						if (bmp.GetPixel(x, y).A < 255 || bmp.GetPixel(x, y).GetBrightness() > .9f) {
							returned.SetPixel(x, y, Color.Transparent);
						} else {
							returned.SetPixel(x, y, c);
						}
					}
				}
			}

			return returned;
		}
	}
}
