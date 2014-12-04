using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBTD
{
	public partial class ToolList : UserControl
	{
		/// <summary>
		/// Radio button used within the ToolList.
		/// 
		/// Uses 3 images: Clicked, nonclicked, hovered.
		/// </summary>
		private class ToolListRadioButton : RadioButton
		{
			private bool mouseInside = false;
			private bool MouseInside {
				get { return mouseInside; }
				set { mouseInside = value; UpdateImage(); }
			}

			private Image nonhoveredImage = new Bitmap(16, 16);
			private Image hoveredImage = new Bitmap(16, 16);
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

			public ToolListRadioButton() : base() {
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
					this.Image = hoveredImage;
				} else {
					this.BackgroundImage = nonselectedBackgroundImage;
					if (mouseInside) {
						this.Image = hoveredImage;
					} else {
						this.Image = nonhoveredImage;
					}
				}
				this.Refresh();
				/*if (mouseInside) {
					this.Image = hoveredImage;
				} else {
					if (this.Checked) {
						this.Image = selectedImage;
					} else {
						this.Image = nonselectedImage;
					}
				}*/
			}

			//Border painting.
			protected override void OnPaint(PaintEventArgs e) {
				PaintEventArgs ranged = new PaintEventArgs(e.Graphics, new Rectangle(e.ClipRectangle.X + 2, e.ClipRectangle.Y + 2, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2));
				base.OnPaint(ranged);

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

		//Sizes.
		protected override Size DefaultSize { get { return new Size(27, 217); } }
		protected override Size DefaultMaximumSize { get { return new Size(27, 217); } }
		protected override Size DefaultMinimumSize { get { return new Size(27, 217); } }
		//Margin.
		protected override Padding DefaultMargin { get { return new Padding(4); } }

		public ToolList() {
			InitializeComponent();
		}

		private void paintBorder(object sender, PaintEventArgs e) {
			Control control = sender as Control;
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, control.Width, control.Height, Border3DStyle.RaisedInner);
		}
	}
}
