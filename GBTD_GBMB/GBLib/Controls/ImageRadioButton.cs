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
	/// Radio button that displays an image based off of state (and doesn't look like a radio button)
	/// 
	/// Uses 3 foreground images and one background image: Clicked, Hovered, nonhovered, and selectedbackground.
	/// </summary>
	public class ImageRadioButton : RadioButton
	{
		private bool mouseInside = false;
		private bool MouseInside {
			get { return mouseInside; }
			set { mouseInside = value; UpdateImage(); }
		}

		private Image nonhoveredImage = new Bitmap(16, 16);
		private Image hoveredImage = new Bitmap(16, 16);
		private Image pressedImage = new Bitmap(16, 16);
		private Image selectedBackgroundImage = new Bitmap(16, 16);
		private Image nonselectedBackgroundImage = new Bitmap(16, 16);

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
		[Category("Appearance"), Description("The image to put in the background when selected.")]
		public Image SelectedBackgroundImage {
			get { return selectedBackgroundImage; }
			set { if (value == null) { value = new Bitmap(16, 16); } selectedBackgroundImage = value; UpdateImage(); }
		}

		[DefaultValue("")]
		public override string Text {
			get { return base.Text; }
			set { base.Text = value; }
		}

		protected override Padding DefaultPadding {
			get { return new Padding(0, 0, 1, 1); }
		}

		public ImageRadioButton()
			: base() {
			AutoSize = false;
		}

		protected override void OnMouseEnter(EventArgs eventargs) {
			mouseInside = true;
			base.OnMouseEnter(eventargs);
			UpdateImage();
		}
		protected override void OnMouseLeave(EventArgs eventargs) {
			mouseInside = false;
			base.OnMouseLeave(eventargs);
			UpdateImage();
		}
		protected override void OnCheckedChanged(EventArgs e) {
			base.OnCheckedChanged(e);
			UpdateImage();
		}

		protected void UpdateImage() {
			if (this.Checked) {
				if (mouseInside) {
					this.BackgroundImage = nonselectedBackgroundImage;
				} else {
					this.BackgroundImage = selectedBackgroundImage;
				}
				this.Image = pressedImage;
			} else {
				this.BackgroundImage = nonselectedBackgroundImage;
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

			if (Checked) {
				ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.SunkenOuter);
			} else {
				if (mouseInside) {
					ControlPaint.DrawBorder3D(e.Graphics, 0, 0, Width, Height, Border3DStyle.RaisedInner);
				} else {
					//Draw nothing.
				}
			}
		}
	}
}
