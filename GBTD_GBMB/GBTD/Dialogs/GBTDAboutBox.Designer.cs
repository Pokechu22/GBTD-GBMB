namespace GB.GBTD.Dialogs
{
	partial class GBTDAboutBox
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
			System.Drawing.StringFormat stringFormat12 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat13 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat14 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat15 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat16 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat17 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat18 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat19 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat20 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat21 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat22 = new System.Drawing.StringFormat();
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
			stringFormat12.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat12.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat12.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat12.Trimming = System.Drawing.StringTrimming.Character;
			this.titleLabel1.Format = stringFormat12;
			this.titleLabel1.Location = new System.Drawing.Point(91, 8);
			this.titleLabel1.Name = "titleLabel1";
			this.titleLabel1.Size = new System.Drawing.Size(80, 21);
			this.titleLabel1.TabIndex = 2;
			this.titleLabel1.TabStop = false;
			this.titleLabel1.Text = "Gameboy";
			// 
			// pictureBoxGBMB
			// 
			this.pictureBoxGBMB.Image = global::GB.GBTD.Properties.Resources.GBTD_Image;
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
			stringFormat13.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat13.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat13.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat13.Trimming = System.Drawing.StringTrimming.Character;
			this.titleLabel2.Format = stringFormat13;
			this.titleLabel2.Location = new System.Drawing.Point(91, 28);
			this.titleLabel2.Name = "titleLabel2";
			this.titleLabel2.Size = new System.Drawing.Size(107, 21);
			this.titleLabel2.TabIndex = 4;
			this.titleLabel2.TabStop = false;
			this.titleLabel2.Text = "Tile Designer";
			// 
			// versionLabel
			// 
			this.versionLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.versionLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat14.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat14.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat14.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat14.Trimming = System.Drawing.StringTrimming.Character;
			this.versionLabel.Format = stringFormat14;
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
			stringFormat15.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat15.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat15.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat15.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel1.Format = stringFormat15;
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
			stringFormat16.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat16.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat16.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat16.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel2.Format = stringFormat16;
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
			stringFormat17.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat17.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat17.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat17.Trimming = System.Drawing.StringTrimming.Character;
			this.origionalCopyrightLabel.Format = stringFormat17;
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
			stringFormat18.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat18.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat18.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat18.Trimming = System.Drawing.StringTrimming.Character;
			this.newCopyrightLabel.Format = stringFormat18;
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
			stringFormat19.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat19.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat19.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat19.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel3.Format = stringFormat19;
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
			stringFormat20.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat20.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat20.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat20.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel4.Format = stringFormat20;
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
			stringFormat21.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat21.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat21.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat21.Trimming = System.Drawing.StringTrimming.Character;
			this.buildLabel.Format = stringFormat21;
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
			stringFormat22.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat22.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat22.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat22.Trimming = System.Drawing.StringTrimming.Character;
			this.buildDateLabel.Format = stringFormat22;
			this.buildDateLabel.Location = new System.Drawing.Point(27, 87);
			this.buildDateLabel.Name = "buildDateLabel";
			this.buildDateLabel.Size = new System.Drawing.Size(55, 14);
			this.buildDateLabel.TabIndex = 13;
			this.buildDateLabel.TabStop = false;
			this.buildDateLabel.Text = "Build date";
			// 
			// GBTDAboutBox
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
			this.Icon = global::GB.GBTD.Properties.Resources.GBTDIcon;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GBTDAboutBox";
			this.Text = "About GBTD";
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