namespace GB.GBTD.Dialogs
{
	partial class ColorPicker
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
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			GB.Shared.Controls.Border mainBorder2;
			GB.Shared.Controls.Border mainBorder1;
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.blueTextBox = new GB.Shared.Controls.NumericTextBox();
			this.greenTextBox = new GB.Shared.Controls.NumericTextBox();
			this.redTextBox = new GB.Shared.Controls.NumericTextBox();
			this.bLabel = new GB.Shared.Controls.CleanLabel();
			this.gLabel = new GB.Shared.Controls.CleanLabel();
			this.rLabel = new GB.Shared.Controls.CleanLabel();
			this.hoveredColorBox = new GB.Shared.Controls.Border();
			this.actualColorBox = new GB.Shared.Controls.Border();
			this.rgbBorder = new GB.Shared.Controls.Border();
			this.pictureBorder = new GB.Shared.Controls.Border();
			mainBorder2 = new GB.Shared.Controls.Border();
			mainBorder1 = new GB.Shared.Controls.Border();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox
			// 
			this.pictureBox.Location = new System.Drawing.Point(4, 4);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(45, 96);
			this.pictureBox.TabIndex = 7;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
			this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
			// 
			// blueTextBox
			// 
			this.blueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.blueTextBox.Location = new System.Drawing.Point(26, 175);
			this.blueTextBox.MaxLength = 2;
			this.blueTextBox.MaxValue = ((uint)(31u));
			this.blueTextBox.Name = "blueTextBox";
			this.blueTextBox.Size = new System.Drawing.Size(18, 17);
			this.blueTextBox.TabIndex = 13;
			this.blueTextBox.Value = ((uint)(31u));
			this.blueTextBox.TextChanged += new System.EventHandler(this.colorTextBox_TextChanged);
			// 
			// greenTextBox
			// 
			this.greenTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.greenTextBox.Location = new System.Drawing.Point(26, 157);
			this.greenTextBox.MaxLength = 2;
			this.greenTextBox.MaxValue = ((uint)(31u));
			this.greenTextBox.Name = "greenTextBox";
			this.greenTextBox.Size = new System.Drawing.Size(18, 17);
			this.greenTextBox.TabIndex = 12;
			this.greenTextBox.Value = ((uint)(31u));
			this.greenTextBox.TextChanged += new System.EventHandler(this.colorTextBox_TextChanged);
			// 
			// redTextBox
			// 
			this.redTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.redTextBox.Location = new System.Drawing.Point(26, 139);
			this.redTextBox.MaxLength = 2;
			this.redTextBox.MaxValue = ((uint)(31u));
			this.redTextBox.Name = "redTextBox";
			this.redTextBox.Size = new System.Drawing.Size(18, 17);
			this.redTextBox.TabIndex = 11;
			this.redTextBox.Value = ((uint)(31u));
			this.redTextBox.TextChanged += new System.EventHandler(this.colorTextBox_TextChanged);
			// 
			// bLabel
			// 
			this.bLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.bLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.bLabel.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.bLabel.Format = stringFormat1;
			this.bLabel.Location = new System.Drawing.Point(7, 177);
			this.bLabel.Name = "bLabel";
			this.bLabel.Size = new System.Drawing.Size(10, 12);
			this.bLabel.TabIndex = 10;
			this.bLabel.TabStop = false;
			this.bLabel.Text = "&B";
			// 
			// gLabel
			// 
			this.gLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.gLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.gLabel.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.gLabel.Format = stringFormat2;
			this.gLabel.Location = new System.Drawing.Point(7, 159);
			this.gLabel.Name = "gLabel";
			this.gLabel.Size = new System.Drawing.Size(11, 12);
			this.gLabel.TabIndex = 9;
			this.gLabel.TabStop = false;
			this.gLabel.Text = "&G";
			// 
			// rLabel
			// 
			this.rLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.rLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.rLabel.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.rLabel.Format = stringFormat3;
			this.rLabel.Location = new System.Drawing.Point(7, 141);
			this.rLabel.Name = "rLabel";
			this.rLabel.Size = new System.Drawing.Size(10, 12);
			this.rLabel.TabIndex = 8;
			this.rLabel.TabStop = false;
			this.rLabel.Text = "&R";
			// 
			// hoveredColorBox
			// 
			this.hoveredColorBox.BackColor = System.Drawing.Color.White;
			this.hoveredColorBox.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.hoveredColorBox.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.hoveredColorBox.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.hoveredColorBox.Location = new System.Drawing.Point(5, 194);
			this.hoveredColorBox.Name = "hoveredColorBox";
			this.hoveredColorBox.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.hoveredColorBox.Size = new System.Drawing.Size(42, 31);
			this.hoveredColorBox.TabIndex = 6;
			this.hoveredColorBox.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// actualColorBox
			// 
			this.actualColorBox.BackColor = System.Drawing.Color.White;
			this.actualColorBox.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.actualColorBox.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.actualColorBox.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.actualColorBox.Location = new System.Drawing.Point(2, 104);
			this.actualColorBox.Name = "actualColorBox";
			this.actualColorBox.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.actualColorBox.Size = new System.Drawing.Size(49, 30);
			this.actualColorBox.TabIndex = 5;
			this.actualColorBox.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.actualColorBox.MouseEnter += new System.EventHandler(this.actualColorBox_MouseEnter);
			// 
			// rgbBorder
			// 
			this.rgbBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.rgbBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.rgbBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.rgbBorder.Location = new System.Drawing.Point(2, 136);
			this.rgbBorder.Name = "rgbBorder";
			this.rgbBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.rgbBorder.Size = new System.Drawing.Size(49, 92);
			this.rgbBorder.TabIndex = 4;
			this.rgbBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// pictureBorder
			// 
			this.pictureBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pictureBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.pictureBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pictureBorder.Location = new System.Drawing.Point(2, 2);
			this.pictureBorder.Name = "pictureBorder";
			this.pictureBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pictureBorder.Size = new System.Drawing.Size(49, 100);
			this.pictureBorder.TabIndex = 3;
			this.pictureBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// mainBorder2
			// 
			mainBorder2.BottomBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			mainBorder2.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			mainBorder2.LeftBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			mainBorder2.Location = new System.Drawing.Point(1, 1);
			mainBorder2.Name = "mainBorder2";
			mainBorder2.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			mainBorder2.Size = new System.Drawing.Size(51, 228);
			mainBorder2.TabIndex = 1;
			mainBorder2.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// mainBorder1
			// 
			mainBorder1.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			mainBorder1.Dock = System.Windows.Forms.DockStyle.Fill;
			mainBorder1.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			mainBorder1.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			mainBorder1.Location = new System.Drawing.Point(0, 0);
			mainBorder1.Name = "mainBorder1";
			mainBorder1.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			mainBorder1.Size = new System.Drawing.Size(53, 230);
			mainBorder1.TabIndex = 0;
			mainBorder1.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// ColorPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.blueTextBox);
			this.Controls.Add(this.greenTextBox);
			this.Controls.Add(this.redTextBox);
			this.Controls.Add(this.bLabel);
			this.Controls.Add(this.gLabel);
			this.Controls.Add(this.rLabel);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.hoveredColorBox);
			this.Controls.Add(this.actualColorBox);
			this.Controls.Add(this.rgbBorder);
			this.Controls.Add(this.pictureBorder);
			this.Controls.Add(mainBorder2);
			this.Controls.Add(mainBorder1);
			this.Name = "ColorPicker";
			this.Size = new System.Drawing.Size(53, 230);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.Border pictureBorder;
		private Shared.Controls.Border rgbBorder;
		private Shared.Controls.Border actualColorBox;
		private Shared.Controls.Border hoveredColorBox;
		private System.Windows.Forms.PictureBox pictureBox;
		private Shared.Controls.CleanLabel rLabel;
		private Shared.Controls.CleanLabel gLabel;
		private Shared.Controls.CleanLabel bLabel;
		private Shared.Controls.NumericTextBox redTextBox;
		private Shared.Controls.NumericTextBox greenTextBox;
		private Shared.Controls.NumericTextBox blueTextBox;

	}
}
