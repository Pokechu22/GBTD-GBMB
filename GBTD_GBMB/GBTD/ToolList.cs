using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Tile;

namespace GB.GBTD
{
	public partial class ToolList : UserControl
	{
		#region Inner classes

		/// <summary>
		/// Radio button used within the ToolList.
		/// 
		/// Uses 3 foreground images and one background image: Clicked, Hovered, nonhovered, and selectedbackground.
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
					this.Image = pressedImage;
				} else {
					this.BackgroundImage = nonselectedBackgroundImage;
					if (mouseInside) {
						this.Image = hoveredImage;
					} else {
						this.Image = nonhoveredImage;
					}
				}
				this.Refresh();
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

		/// <summary>
		/// Regular button used within the toollist.
		/// 
		/// It has three images: Mouse over, mouse not over; mouse down and over.
		/// </summary>
		private class ToolListButton : Button
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

			public ToolListButton() : base() {
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
				this.Refresh();
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

		/// <summary>
		/// Check box used within the ToolList.
		/// 
		/// Uses 3 foreground images and one background image: Clicked, Hovered, nonhovered, and selectedbackground.
		/// 
		/// A practical copy-paste of the radio button code.  I'm not sure of the right way of actually doing this.
		/// </summary>
		private class ToolListCheckBox : CheckBox
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

			public ToolListCheckBox() : base() {
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
				this.Refresh();
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
		#endregion

		//Sizes.
		protected override Size DefaultSize { get { return new Size(26, 217); } }
		protected override Size DefaultMaximumSize { get { return new Size(26, 217); } }
		protected override Size DefaultMinimumSize { get { return new Size(26, 217); } }
		//Margin.
		protected override Padding DefaultMargin { get { return new Padding(4); } }

		public ToolList() {
			InitializeComponent();
		}

		private void paintBorder(object sender, PaintEventArgs e) {
			Control control = sender as Control;
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, control.Width, control.Height, Border3DStyle.RaisedInner);
		}

		private void paintIndention(object sender, PaintEventArgs e) {
			Control control = sender as Control;
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, control.Width, control.Height, Border3DStyle.SunkenOuter);
		}

		#region Events
		/// <summary>
		/// Triggers when the selected tool is changed.
		/// </summary>
		[Category("Action"), Description("Triggers when the selected tool is changed.")]
		public event EventHandler SelectedToolChanged;

		/// <summary>
		/// Triggers when the scroll up button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll up button is clicked.")]
		public event EventHandler ScrollUpClicked;
		/// <summary>
		/// Triggers when the scroll down button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll down button is clicked.")]
		public event EventHandler ScrollDownClicked;
		/// <summary>
		/// Triggers when the scroll left button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll left button is clicked.")]
		public event EventHandler ScrollLeftClicked;
		/// <summary>
		/// Triggers when the scroll right button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll right button is clicked.")]
		public event EventHandler ScrollRightClicked;

		/// <summary>
		/// Triggers when the flip vertically button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the flip vertically button is clicked.")]
		public event EventHandler FlipVerticallyClicked;
		/// <summary>
		/// Triggers when the flip horizontally button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the flip horizontally button is clicked.")]
		public event EventHandler FlipHorizontallyClicked;
		/// <summary>
		/// Triggers when the rotate clockwise button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the rotate clockwise button is clicked.")]
		public event EventHandler RotateClockwiseClicked;

		/// <summary>
		/// Triggers when the Auto Update mode is changed.
		/// </summary>
		[Category("Action"), Description("Triggers when the Auto Update mode is changed.")]
		public event EventHandler AutoUpdateChanged;
		#endregion

		//The silly-looking parameters allow it to be called with or without args, and thus used both normally and as event handlers.
		//However, those arguments are never used.  They are merely there for convinience.

		private void OnSelectedToolChanged(object sender = null, EventArgs value = null) {
			if (SelectedToolChanged != null) {
				SelectedToolChanged(this, new EventArgs());
			}
		}

		private void OnScrollUpClicked(object sender = null, EventArgs value = null) {
			if (ScrollUpClicked != null) {
				ScrollUpClicked(this, new EventArgs());
			}
		}

		private void OnScrollLeftClicked(object sender = null, EventArgs value = null) {
			if (ScrollLeftClicked != null) {
				ScrollLeftClicked(this, new EventArgs());
			}
		}

		private void OnScrollRightClicked(object sender = null, EventArgs value = null) {
			if (ScrollRightClicked != null) {
				ScrollRightClicked(this, new EventArgs());
			}
		}

		private void OnScrollDownClicked(object sender = null, EventArgs value = null) {
			if (ScrollDownClicked != null) {
				ScrollDownClicked(this, new EventArgs());
			}
		}

		private void OnFlipVerticallyClicked(object sender = null, EventArgs value = null) {
			if (FlipVerticallyClicked != null) {
				FlipVerticallyClicked(this, new EventArgs());
			}
		}

		private void OnFlipHorizontallyClicked(object sender = null, EventArgs value = null) {
			if (FlipHorizontallyClicked != null) {
				FlipHorizontallyClicked(this, new EventArgs());
			}
		}

		private void OnRotateClockwiseClicked(object sender = null, EventArgs value = null) {
			if (RotateClockwiseClicked != null) {
				RotateClockwiseClicked(this, new EventArgs());
			}
		}

		private void OnAutoUpdateChanged(object sender = null, EventArgs value = null) {
			if (AutoUpdateChanged != null) {
				AutoUpdateChanged(this, new EventArgs());
			}
		}

		/// <summary>
		/// The currently selected tool.
		/// TODO replace int with an enum.
		/// </summary>
		[Category("Data"), Description("The currently-selected tool.")]
		public TileEditorID SelectedTool {
			get {
				if (penButton.Checked) {
					return TileEditorID.PixelEdit;
				}
				if (floodButton.Checked) {
					return TileEditorID.FloodFill;
				}
				penButton.Checked = true;
				return TileEditorID.PixelEdit;
			}
			set {
				if (value == TileEditorID.PixelEdit) {
					penButton.Checked = true;
					return;
				}
				if (value == TileEditorID.FloodFill) {
					floodButton.Checked = true;
					return;
				}
				if (value == TileEditorID.NoEdit) {
					penButton.Checked = false;
					floodButton.Checked = false;
					return;
				}
				throw new InvalidEnumArgumentException();
			}
		}

		/// <summary>
		/// Should auto update?
		/// </summary>
		[Category("Data"), Description("Is automatic updating enabled?")]
		public bool AutoUpdate {
			get {
				return autoUpdateCheckbox.Checked;
			}
			set {
				autoUpdateCheckbox.Checked = value;
			}
		}
	}
}
