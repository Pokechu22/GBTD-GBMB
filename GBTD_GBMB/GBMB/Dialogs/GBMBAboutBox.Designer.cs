namespace GB.GBMB.Dialogs
{
	partial class GBMBAboutBox
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat4 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat5 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat6 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat7 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat8 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat9 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat10 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat11 = new System.Drawing.StringFormat();
			this.border = new GB.Shared.Controls.Border();
			this.okButton = new System.Windows.Forms.Button();
			this.titleLabel1 = new GB.Shared.Controls.CleanLabel();
			this.pictureBoxGBMB = new System.Windows.Forms.PictureBox();
			this.titleLabel2 = new GB.Shared.Controls.CleanLabel();
			this.versionLabel = new GB.Shared.Controls.CleanLabel();
			this.releasesLabel1 = new GB.Shared.Controls.CleanLabel();
			this.releasesLabel2 = new GB.Shared.Controls.CleanLabel();
			this.origionalCopyrightLabel = new GB.Shared.Controls.CleanLabel();
			this.newCopyrightLabel = new GB.Shared.Controls.CleanLabel();
			this.releasesLabel3 = new GB.Shared.Controls.CleanLabel();
			this.releasesLabel4 = new GB.Shared.Controls.CleanLabel();
			this.buildLabel = new GB.Shared.Controls.CleanLabel();
			this.buildDateLabel = new GB.Shared.Controls.CleanLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGBMB)).BeginInit();
			this.SuspendLayout();
			// 
			// border
			// 
			this.border.BottomBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.border.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.border.LeftBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.border.Location = new System.Drawing.Point(1, 112);
			this.border.Name = "border";
			this.border.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.border.Size = new System.Drawing.Size(256, 97);
			this.border.TabIndex = 0;
			this.border.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(91, 260);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// titleLabel1
			// 
			this.titleLabel1.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.titleLabel1.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.titleLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.titleLabel1.Format = stringFormat1;
			this.titleLabel1.Location = new System.Drawing.Point(91, 8);
			this.titleLabel1.Name = "titleLabel1";
			this.titleLabel1.Size = new System.Drawing.Size(80, 21);
			this.titleLabel1.TabIndex = 2;
			this.titleLabel1.TabStop = false;
			this.titleLabel1.Text = "Gameboy";
			// 
			// pictureBoxGBMB
			// 
			this.pictureBoxGBMB.Image = global::GB.GBMB.Properties.Resources.GBMB_image;
			this.pictureBoxGBMB.Location = new System.Drawing.Point(59, 12);
			this.pictureBoxGBMB.Name = "pictureBoxGBMB";
			this.pictureBoxGBMB.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxGBMB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxGBMB.TabIndex = 3;
			this.pictureBoxGBMB.TabStop = false;
			// 
			// titleLabel2
			// 
			this.titleLabel2.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.titleLabel2.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.titleLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.titleLabel2.Format = stringFormat2;
			this.titleLabel2.Location = new System.Drawing.Point(91, 28);
			this.titleLabel2.Name = "titleLabel2";
			this.titleLabel2.Size = new System.Drawing.Size(97, 21);
			this.titleLabel2.TabIndex = 4;
			this.titleLabel2.TabStop = false;
			this.titleLabel2.Text = "Map Builder";
			// 
			// versionLabel
			// 
			this.versionLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.versionLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.versionLabel.Format = stringFormat3;
			this.versionLabel.Location = new System.Drawing.Point(27, 55);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(43, 14);
			this.versionLabel.TabIndex = 5;
			this.versionLabel.TabStop = false;
			this.versionLabel.Text = "Version";
			// 
			// releasesLabel1
			// 
			this.releasesLabel1.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.releasesLabel1.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat4.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat4.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat4.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat4.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel1.Format = stringFormat4;
			this.releasesLabel1.Location = new System.Drawing.Point(51, 124);
			this.releasesLabel1.Name = "releasesLabel1";
			this.releasesLabel1.Size = new System.Drawing.Size(165, 14);
			this.releasesLabel1.TabIndex = 6;
			this.releasesLabel1.TabStop = false;
			this.releasesLabel1.Text = "For info and new releases, goto:";
			// 
			// releasesLabel2
			// 
			this.releasesLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.releasesLabel2.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.releasesLabel2.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.releasesLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.releasesLabel2.ForeColor = System.Drawing.Color.Blue;
			stringFormat5.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat5.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat5.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat5.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel2.Format = stringFormat5;
			this.releasesLabel2.Location = new System.Drawing.Point(19, 140);
			this.releasesLabel2.Name = "releasesLabel2";
			this.releasesLabel2.Size = new System.Drawing.Size(225, 14);
			this.releasesLabel2.TabIndex = 7;
			this.releasesLabel2.TabStop = false;
			this.releasesLabel2.Tag = "https://github.com/Pokechu22/GBTD-GBMB";
			this.releasesLabel2.Text = "https://github.com/Pokechu22/GBTD-GBMB";
			this.releasesLabel2.Click += new System.EventHandler(this.labelWithLink_Click);
			// 
			// origionalCopyrightLabel
			// 
			this.origionalCopyrightLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.origionalCopyrightLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat6.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat6.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat6.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat6.Trimming = System.Drawing.StringTrimming.Character;
			this.origionalCopyrightLabel.Format = stringFormat6;
			this.origionalCopyrightLabel.Location = new System.Drawing.Point(27, 237);
			this.origionalCopyrightLabel.Name = "origionalCopyrightLabel";
			this.origionalCopyrightLabel.Size = new System.Drawing.Size(208, 14);
			this.origionalCopyrightLabel.TabIndex = 8;
			this.origionalCopyrightLabel.TabStop = false;
			this.origionalCopyrightLabel.Text = "Origional App Copyright H. Mulder, 1999";
			// 
			// newCopyrightLabel
			// 
			this.newCopyrightLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.newCopyrightLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat7.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat7.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat7.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat7.Trimming = System.Drawing.StringTrimming.Character;
			this.newCopyrightLabel.Format = stringFormat7;
			this.newCopyrightLabel.Location = new System.Drawing.Point(27, 221);
			this.newCopyrightLabel.Name = "newCopyrightLabel";
			this.newCopyrightLabel.Size = new System.Drawing.Size(144, 14);
			this.newCopyrightLabel.TabIndex = 9;
			this.newCopyrightLabel.TabStop = false;
			this.newCopyrightLabel.Text = "Copyright Pokechu22, 2015";
			// 
			// releasesLabel3
			// 
			this.releasesLabel3.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.releasesLabel3.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat8.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat8.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat8.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat8.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel3.Format = stringFormat8;
			this.releasesLabel3.Location = new System.Drawing.Point(67, 166);
			this.releasesLabel3.Name = "releasesLabel3";
			this.releasesLabel3.Size = new System.Drawing.Size(129, 14);
			this.releasesLabel3.TabIndex = 10;
			this.releasesLabel3.TabStop = false;
			this.releasesLabel3.Text = "For the old version, goto:";
			// 
			// releasesLabel4
			// 
			this.releasesLabel4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.releasesLabel4.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.releasesLabel4.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.releasesLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.releasesLabel4.ForeColor = System.Drawing.Color.Blue;
			stringFormat9.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat9.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat9.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat9.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel4.Format = stringFormat9;
			this.releasesLabel4.Location = new System.Drawing.Point(26, 182);
			this.releasesLabel4.Name = "releasesLabel4";
			this.releasesLabel4.Size = new System.Drawing.Size(210, 14);
			this.releasesLabel4.TabIndex = 11;
			this.releasesLabel4.TabStop = false;
			this.releasesLabel4.Tag = "http://www.devrs.com/gb/hmgd/intro.html";
			this.releasesLabel4.Text = "http://www.devrs.com/gb/hmgd/intro.html";
			this.releasesLabel4.Click += new System.EventHandler(this.labelWithLink_Click);
			// 
			// buildLabel
			// 
			this.buildLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.buildLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat10.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat10.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat10.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat10.Trimming = System.Drawing.StringTrimming.Character;
			this.buildLabel.Format = stringFormat10;
			this.buildLabel.Location = new System.Drawing.Point(27, 71);
			this.buildLabel.Name = "buildLabel";
			this.buildLabel.Size = new System.Drawing.Size(29, 14);
			this.buildLabel.TabIndex = 12;
			this.buildLabel.TabStop = false;
			this.buildLabel.Text = "Build";
			// 
			// buildDateLabel
			// 
			this.buildDateLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.buildDateLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat11.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat11.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat11.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat11.Trimming = System.Drawing.StringTrimming.Character;
			this.buildDateLabel.Format = stringFormat11;
			this.buildDateLabel.Location = new System.Drawing.Point(27, 87);
			this.buildDateLabel.Name = "buildDateLabel";
			this.buildDateLabel.Size = new System.Drawing.Size(55, 14);
			this.buildDateLabel.TabIndex = 13;
			this.buildDateLabel.TabStop = false;
			this.buildDateLabel.Text = "Build date";
			// 
			// GBMBAboutBox
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(257, 295);
			this.Controls.Add(this.buildDateLabel);
			this.Controls.Add(this.buildLabel);
			this.Controls.Add(this.releasesLabel4);
			this.Controls.Add(this.releasesLabel3);
			this.Controls.Add(this.newCopyrightLabel);
			this.Controls.Add(this.origionalCopyrightLabel);
			this.Controls.Add(this.releasesLabel2);
			this.Controls.Add(this.releasesLabel1);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.titleLabel2);
			this.Controls.Add(this.pictureBoxGBMB);
			this.Controls.Add(this.titleLabel1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.border);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = global::GB.GBMB.Properties.Resources.GBMB_Icon;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GBMBAboutBox";
			this.ShowInTaskbar = false;
			this.Text = "About GBMB";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGBMB)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Shared.Controls.Border border;
		private System.Windows.Forms.Button okButton;
		private Shared.Controls.CleanLabel titleLabel1;
		private System.Windows.Forms.PictureBox pictureBoxGBMB;
		private Shared.Controls.CleanLabel titleLabel2;
		private Shared.Controls.CleanLabel versionLabel;
		private Shared.Controls.CleanLabel releasesLabel1;
		private Shared.Controls.CleanLabel releasesLabel2;
		private Shared.Controls.CleanLabel origionalCopyrightLabel;
		private Shared.Controls.CleanLabel newCopyrightLabel;
		private Shared.Controls.CleanLabel releasesLabel3;
		private Shared.Controls.CleanLabel releasesLabel4;
		private Shared.Controls.CleanLabel buildLabel;
		private Shared.Controls.CleanLabel buildDateLabel;
	}
}