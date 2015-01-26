namespace GB.GBTD
{
	partial class ToolList
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolList));
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.autoUpdateCheckbox = new GB.GBTD.ToolList.ToolListCheckBox();
			this.rotateClockwiseButton = new GB.GBTD.ToolList.ToolListButton();
			this.flipHorizButton = new GB.GBTD.ToolList.ToolListButton();
			this.flipVertButton = new GB.GBTD.ToolList.ToolListButton();
			this.scrollDownButton = new GB.GBTD.ToolList.ToolListButton();
			this.scrollRightButton = new GB.GBTD.ToolList.ToolListButton();
			this.scrollLeftButton = new GB.GBTD.ToolList.ToolListButton();
			this.scrollUpButton = new GB.GBTD.ToolList.ToolListButton();
			this.floodButton = new GB.GBTD.ToolList.ToolListRadioButton();
			this.penButton = new GB.GBTD.ToolList.ToolListRadioButton();
			this.flipBorder = new GB.Shared.Controls.Border();
			this.moveBorder = new GB.Shared.Controls.Border();
			this.mainBorder = new GB.Shared.Controls.Border();
			this.flipBorder.SuspendLayout();
			this.moveBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// autoUpdateCheckbox
			// 
			this.autoUpdateCheckbox.Appearance = System.Windows.Forms.Appearance.Button;
			this.autoUpdateCheckbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("autoUpdateCheckbox.BackgroundImage")));
			this.autoUpdateCheckbox.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.autoUpdateCheckbox.FlatAppearance.BorderSize = 0;
			this.autoUpdateCheckbox.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.autoUpdateCheckbox.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.autoUpdateCheckbox.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.autoUpdateCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.autoUpdateCheckbox.HoveredImage = global::GB.GBTD.Properties.Resources.AutoUpdate_Hover;
			this.autoUpdateCheckbox.Image = global::GB.GBTD.Properties.Resources.AutoUpdate_NoHover;
			this.autoUpdateCheckbox.Location = new System.Drawing.Point(2, 200);
			this.autoUpdateCheckbox.Name = "autoUpdateCheckbox";
			this.autoUpdateCheckbox.NonhoveredImage = global::GB.GBTD.Properties.Resources.AutoUpdate_NoHover;
			this.autoUpdateCheckbox.PressedImage = global::GB.GBTD.Properties.Resources.AutoUpdate_Pressed;
			this.autoUpdateCheckbox.SelectedBackgroundImage = global::GB.GBTD.Properties.Resources.SelectionBackground;
			this.autoUpdateCheckbox.Size = new System.Drawing.Size(22, 15);
			this.autoUpdateCheckbox.TabIndex = 4;
			this.toolTip.SetToolTip(this.autoUpdateCheckbox, "Auto update");
			this.autoUpdateCheckbox.UseVisualStyleBackColor = true;
			this.autoUpdateCheckbox.CheckedChanged += new System.EventHandler(this.OnAutoUpdateChanged);
			// 
			// rotateClockwiseButton
			// 
			this.rotateClockwiseButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.rotateClockwiseButton.FlatAppearance.BorderSize = 0;
			this.rotateClockwiseButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.rotateClockwiseButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.rotateClockwiseButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.rotateClockwiseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rotateClockwiseButton.HoveredImage = global::GB.GBTD.Properties.Resources.RotateClockwise_Hover;
			this.rotateClockwiseButton.Image = global::GB.GBTD.Properties.Resources.RotateClockwise_NoHover;
			this.rotateClockwiseButton.Location = new System.Drawing.Point(1, 41);
			this.rotateClockwiseButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.rotateClockwiseButton.Name = "rotateClockwiseButton";
			this.rotateClockwiseButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.RotateClockwise_NoHover;
			this.rotateClockwiseButton.PressedImage = global::GB.GBTD.Properties.Resources.RotateClockwise_Pressed;
			this.rotateClockwiseButton.Size = new System.Drawing.Size(20, 20);
			this.rotateClockwiseButton.TabIndex = 2;
			this.toolTip.SetToolTip(this.rotateClockwiseButton, "Rotate clockwise");
			this.rotateClockwiseButton.UseVisualStyleBackColor = true;
			this.rotateClockwiseButton.Click += new System.EventHandler(this.OnRotateClockwiseClicked);
			// 
			// flipHorizButton
			// 
			this.flipHorizButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.flipHorizButton.FlatAppearance.BorderSize = 0;
			this.flipHorizButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.flipHorizButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.flipHorizButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.flipHorizButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.flipHorizButton.HoveredImage = global::GB.GBTD.Properties.Resources.FlipHorizontally_Hover;
			this.flipHorizButton.Image = global::GB.GBTD.Properties.Resources.FlipHorizontally_NoHover;
			this.flipHorizButton.Location = new System.Drawing.Point(1, 21);
			this.flipHorizButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.flipHorizButton.Name = "flipHorizButton";
			this.flipHorizButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.FlipHorizontally_NoHover;
			this.flipHorizButton.PressedImage = global::GB.GBTD.Properties.Resources.FlipHorizontally_Pressed;
			this.flipHorizButton.Size = new System.Drawing.Size(20, 20);
			this.flipHorizButton.TabIndex = 1;
			this.toolTip.SetToolTip(this.flipHorizButton, "Flip horizontally");
			this.flipHorizButton.UseVisualStyleBackColor = true;
			this.flipHorizButton.Click += new System.EventHandler(this.OnFlipHorizontallyClicked);
			// 
			// flipVertButton
			// 
			this.flipVertButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.flipVertButton.FlatAppearance.BorderSize = 0;
			this.flipVertButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.flipVertButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.flipVertButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.flipVertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.flipVertButton.HoveredImage = global::GB.GBTD.Properties.Resources.FlipVertically_Hover;
			this.flipVertButton.Image = global::GB.GBTD.Properties.Resources.FlipVertically_NoHover;
			this.flipVertButton.Location = new System.Drawing.Point(1, 1);
			this.flipVertButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.flipVertButton.Name = "flipVertButton";
			this.flipVertButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.FlipVertically_NoHover;
			this.flipVertButton.PressedImage = global::GB.GBTD.Properties.Resources.FlipVertically_Pressed;
			this.flipVertButton.Size = new System.Drawing.Size(20, 20);
			this.flipVertButton.TabIndex = 0;
			this.toolTip.SetToolTip(this.flipVertButton, "Flip vertically");
			this.flipVertButton.UseVisualStyleBackColor = true;
			this.flipVertButton.Click += new System.EventHandler(this.OnFlipVerticallyClicked);
			// 
			// scrollDownButton
			// 
			this.scrollDownButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.scrollDownButton.FlatAppearance.BorderSize = 0;
			this.scrollDownButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.scrollDownButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.scrollDownButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.scrollDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.scrollDownButton.HoveredImage = global::GB.GBTD.Properties.Resources.ScrollDown_Hover;
			this.scrollDownButton.Image = global::GB.GBTD.Properties.Resources.ScrollDown_NoHover;
			this.scrollDownButton.Location = new System.Drawing.Point(1, 61);
			this.scrollDownButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.scrollDownButton.Name = "scrollDownButton";
			this.scrollDownButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.ScrollDown_NoHover;
			this.scrollDownButton.PressedImage = global::GB.GBTD.Properties.Resources.ScrollDown_Pressed;
			this.scrollDownButton.Size = new System.Drawing.Size(20, 20);
			this.scrollDownButton.TabIndex = 3;
			this.toolTip.SetToolTip(this.scrollDownButton, "Scroll down");
			this.scrollDownButton.UseVisualStyleBackColor = true;
			this.scrollDownButton.Click += new System.EventHandler(this.OnScrollDownClicked);
			// 
			// scrollRightButton
			// 
			this.scrollRightButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.scrollRightButton.FlatAppearance.BorderSize = 0;
			this.scrollRightButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.scrollRightButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.scrollRightButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.scrollRightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.scrollRightButton.HoveredImage = global::GB.GBTD.Properties.Resources.ScrollRight_Hover;
			this.scrollRightButton.Image = global::GB.GBTD.Properties.Resources.ScrollRight_NoHover;
			this.scrollRightButton.Location = new System.Drawing.Point(1, 41);
			this.scrollRightButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.scrollRightButton.Name = "scrollRightButton";
			this.scrollRightButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.ScrollRight_NoHover;
			this.scrollRightButton.PressedImage = global::GB.GBTD.Properties.Resources.ScrollRight_Pressed;
			this.scrollRightButton.Size = new System.Drawing.Size(20, 20);
			this.scrollRightButton.TabIndex = 2;
			this.toolTip.SetToolTip(this.scrollRightButton, "Scroll right");
			this.scrollRightButton.UseVisualStyleBackColor = true;
			this.scrollRightButton.Click += new System.EventHandler(this.OnScrollRightClicked);
			// 
			// scrollLeftButton
			// 
			this.scrollLeftButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.scrollLeftButton.FlatAppearance.BorderSize = 0;
			this.scrollLeftButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.scrollLeftButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.scrollLeftButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.scrollLeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.scrollLeftButton.HoveredImage = global::GB.GBTD.Properties.Resources.ScrollLeft_Hover;
			this.scrollLeftButton.Image = global::GB.GBTD.Properties.Resources.ScrollLeft_NoHover;
			this.scrollLeftButton.Location = new System.Drawing.Point(1, 21);
			this.scrollLeftButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.scrollLeftButton.Name = "scrollLeftButton";
			this.scrollLeftButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.ScrollLeft_NoHover;
			this.scrollLeftButton.PressedImage = global::GB.GBTD.Properties.Resources.ScrollLeft_Pressed;
			this.scrollLeftButton.Size = new System.Drawing.Size(20, 20);
			this.scrollLeftButton.TabIndex = 1;
			this.toolTip.SetToolTip(this.scrollLeftButton, "Scroll left");
			this.scrollLeftButton.UseVisualStyleBackColor = true;
			this.scrollLeftButton.Click += new System.EventHandler(this.OnScrollLeftClicked);
			// 
			// scrollUpButton
			// 
			this.scrollUpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.scrollUpButton.FlatAppearance.BorderSize = 0;
			this.scrollUpButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.scrollUpButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.scrollUpButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.scrollUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.scrollUpButton.HoveredImage = global::GB.GBTD.Properties.Resources.ScrollUp_Hover;
			this.scrollUpButton.Image = global::GB.GBTD.Properties.Resources.ScrollUp_NoHover;
			this.scrollUpButton.Location = new System.Drawing.Point(1, 1);
			this.scrollUpButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
			this.scrollUpButton.Name = "scrollUpButton";
			this.scrollUpButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.ScrollUp_NoHover;
			this.scrollUpButton.PressedImage = global::GB.GBTD.Properties.Resources.ScrollUp_Pressed;
			this.scrollUpButton.Size = new System.Drawing.Size(20, 20);
			this.scrollUpButton.TabIndex = 0;
			this.toolTip.SetToolTip(this.scrollUpButton, "Scroll up");
			this.scrollUpButton.UseVisualStyleBackColor = true;
			this.scrollUpButton.Click += new System.EventHandler(this.OnScrollUpClicked);
			// 
			// floodButton
			// 
			this.floodButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.floodButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("floodButton.BackgroundImage")));
			this.floodButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.floodButton.FlatAppearance.BorderSize = 0;
			this.floodButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.floodButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.floodButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.floodButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.floodButton.HoveredImage = global::GB.GBTD.Properties.Resources.Flood_Hover;
			this.floodButton.Image = global::GB.GBTD.Properties.Resources.Flood_NoHover;
			this.floodButton.Location = new System.Drawing.Point(2, 24);
			this.floodButton.Name = "floodButton";
			this.floodButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.Flood_NoHover;
			this.floodButton.PressedImage = global::GB.GBTD.Properties.Resources.Flood_Pressed;
			this.floodButton.SelectedBackgroundImage = global::GB.GBTD.Properties.Resources.SelectionBackground;
			this.floodButton.Size = new System.Drawing.Size(22, 22);
			this.floodButton.TabIndex = 1;
			this.floodButton.TabStop = true;
			this.toolTip.SetToolTip(this.floodButton, "Flood fill");
			this.floodButton.UseVisualStyleBackColor = true;
			this.floodButton.CheckedChanged += new System.EventHandler(this.OnSelectedToolChanged);
			// 
			// penButton
			// 
			this.penButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.penButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("penButton.BackgroundImage")));
			this.penButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.penButton.FlatAppearance.BorderSize = 0;
			this.penButton.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.penButton.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.penButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.penButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.penButton.HoveredImage = global::GB.GBTD.Properties.Resources.Pen_Hover;
			this.penButton.Image = global::GB.GBTD.Properties.Resources.Pen_NoHover;
			this.penButton.Location = new System.Drawing.Point(2, 2);
			this.penButton.Name = "penButton";
			this.penButton.NonhoveredImage = global::GB.GBTD.Properties.Resources.Pen_NoHover;
			this.penButton.PressedImage = global::GB.GBTD.Properties.Resources.Pen_Pressed;
			this.penButton.SelectedBackgroundImage = global::GB.GBTD.Properties.Resources.SelectionBackground;
			this.penButton.Size = new System.Drawing.Size(22, 22);
			this.penButton.TabIndex = 0;
			this.penButton.TabStop = true;
			this.toolTip.SetToolTip(this.penButton, "Pen");
			this.penButton.UseVisualStyleBackColor = true;
			this.penButton.CheckedChanged += new System.EventHandler(this.OnSelectedToolChanged);
			// 
			// flipBorder
			// 
			this.flipBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.flipBorder.Controls.Add(this.rotateClockwiseButton);
			this.flipBorder.Controls.Add(this.flipHorizButton);
			this.flipBorder.Controls.Add(this.flipVertButton);
			this.flipBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Bottom,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left};
			this.flipBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.flipBorder.Location = new System.Drawing.Point(2, 134);
			this.flipBorder.Name = "flipBorder";
			this.flipBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.flipBorder.Size = new System.Drawing.Size(22, 62);
			this.flipBorder.TabIndex = 3;
			this.flipBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// moveBorder
			// 
			this.moveBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.moveBorder.Controls.Add(this.scrollDownButton);
			this.moveBorder.Controls.Add(this.scrollRightButton);
			this.moveBorder.Controls.Add(this.scrollLeftButton);
			this.moveBorder.Controls.Add(this.scrollUpButton);
			this.moveBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Bottom,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left};
			this.moveBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.moveBorder.Location = new System.Drawing.Point(2, 49);
			this.moveBorder.Name = "moveBorder";
			this.moveBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.moveBorder.Size = new System.Drawing.Size(22, 82);
			this.moveBorder.TabIndex = 2;
			this.moveBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// mainBorder
			// 
			this.mainBorder.BottomBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.mainBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.mainBorder.LeftBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.mainBorder.Location = new System.Drawing.Point(0, 0);
			this.mainBorder.Name = "mainBorder";
			this.mainBorder.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.mainBorder.Size = new System.Drawing.Size(26, 217);
			this.mainBorder.TabIndex = 5;
			this.mainBorder.Text = "border1";
			this.mainBorder.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// ToolList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.autoUpdateCheckbox);
			this.Controls.Add(this.flipBorder);
			this.Controls.Add(this.moveBorder);
			this.Controls.Add(this.floodButton);
			this.Controls.Add(this.penButton);
			this.Controls.Add(this.mainBorder);
			this.MaximumSize = new System.Drawing.Size(26, 217);
			this.MinimumSize = new System.Drawing.Size(26, 217);
			this.Name = "ToolList";
			this.Size = new System.Drawing.Size(26, 217);
			this.flipBorder.ResumeLayout(false);
			this.moveBorder.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip;
		private ToolList.ToolListRadioButton floodButton;
		private ToolList.ToolListRadioButton penButton;
		private GB.Shared.Controls.Border flipBorder;
		private ToolList.ToolListButton scrollDownButton;
		private ToolList.ToolListButton scrollRightButton;
		private ToolList.ToolListButton scrollLeftButton;
		private ToolList.ToolListButton scrollUpButton;
		private ToolList.ToolListButton rotateClockwiseButton;
		private ToolList.ToolListButton flipHorizButton;
		private ToolList.ToolListButton flipVertButton;
		private ToolList.ToolListCheckBox autoUpdateCheckbox;
		private Shared.Controls.Border mainBorder;
		private Shared.Controls.Border moveBorder;
	}
}
