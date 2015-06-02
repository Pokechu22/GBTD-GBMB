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
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat4 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat5 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat6 = new System.Drawing.StringFormat();
			this.border = new GB.Shared.Controls.Border();
			this.okButton = new System.Windows.Forms.Button();
			this.titleLabel1 = new GB.Shared.Controls.CleanLabel();
			this.pictureBoxGBTD = new System.Windows.Forms.PictureBox();
			this.titleLabel2 = new GB.Shared.Controls.CleanLabel();
			this.versionLabel = new GB.Shared.Controls.CleanLabel();
			this.releasesLabel1 = new GB.Shared.Controls.CleanLabel();
			this.releasesLabel2 = new GB.Shared.Controls.CleanLabel();
			this.newCopyrightLabel = new GB.Shared.Controls.CleanLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGBTD)).BeginInit();
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
			this.border.Location = new System.Drawing.Point(1, 88);
			this.border.Name = "border";
			this.border.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.border.Size = new System.Drawing.Size(256, 57);
			this.border.TabIndex = 0;
			this.border.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(91, 176);
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
			// pictureBoxGBTD
			// 
			this.pictureBoxGBTD.Image = global::GB.GBTD.Properties.Resources.GBTD_Image;
			this.pictureBoxGBTD.Location = new System.Drawing.Point(59, 12);
			this.pictureBoxGBTD.Name = "pictureBoxGBTD";
			this.pictureBoxGBTD.Size = new System.Drawing.Size(32, 32);
			this.pictureBoxGBTD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBoxGBTD.TabIndex = 3;
			this.pictureBoxGBTD.TabStop = false;
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
			this.titleLabel2.Size = new System.Drawing.Size(107, 21);
			this.titleLabel2.TabIndex = 4;
			this.titleLabel2.TabStop = false;
			this.titleLabel2.Text = "Tile Designer";
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
			this.versionLabel.Location = new System.Drawing.Point(100, 56);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(61, 14);
			this.versionLabel.TabIndex = 5;
			this.versionLabel.TabStop = false;
			this.versionLabel.Text = "Version 2.2";
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
			this.releasesLabel1.Location = new System.Drawing.Point(51, 100);
			this.releasesLabel1.Name = "releasesLabel1";
			this.releasesLabel1.Size = new System.Drawing.Size(165, 14);
			this.releasesLabel1.TabIndex = 6;
			this.releasesLabel1.TabStop = false;
			this.releasesLabel1.Text = "For info and new releases, goto:";
			// 
			// releasesLabel2
			// 
			this.releasesLabel2.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.releasesLabel2.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat5.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat5.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat5.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat5.Trimming = System.Drawing.StringTrimming.Character;
			this.releasesLabel2.Format = stringFormat5;
			this.releasesLabel2.Location = new System.Drawing.Point(43, 116);
			this.releasesLabel2.Name = "releasesLabel2";
			this.releasesLabel2.Size = new System.Drawing.Size(176, 14);
			this.releasesLabel2.TabIndex = 7;
			this.releasesLabel2.TabStop = false;
			this.releasesLabel2.Text = "http://www.casema.net/~hpmulder";
			// 
			// newCopyrightLabel
			// 
			this.newCopyrightLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.newCopyrightLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat6.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat6.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat6.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat6.Trimming = System.Drawing.StringTrimming.Character;
			this.newCopyrightLabel.Format = stringFormat6;
			this.newCopyrightLabel.Location = new System.Drawing.Point(65, 152);
			this.newCopyrightLabel.Name = "newCopyrightLabel";
			this.newCopyrightLabel.Size = new System.Drawing.Size(136, 14);
			this.newCopyrightLabel.TabIndex = 9;
			this.newCopyrightLabel.TabStop = false;
			this.newCopyrightLabel.Text = "Copyright H. Mulder, 1999";
			// 
			// GBTDAboutBox
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(258, 208);
			this.Controls.Add(this.newCopyrightLabel);
			this.Controls.Add(this.releasesLabel2);
			this.Controls.Add(this.releasesLabel1);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.titleLabel2);
			this.Controls.Add(this.pictureBoxGBTD);
			this.Controls.Add(this.titleLabel1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.border);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = global::GB.GBTD.Properties.Resources.GBTDIcon;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GBTDAboutBox";
			this.Text = "About GBTD";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxGBTD)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Shared.Controls.Border border;
		private System.Windows.Forms.Button okButton;
		private Shared.Controls.CleanLabel titleLabel1;
		private System.Windows.Forms.PictureBox pictureBoxGBTD;
		private Shared.Controls.CleanLabel titleLabel2;
		private Shared.Controls.CleanLabel versionLabel;
		private Shared.Controls.CleanLabel releasesLabel1;
		private Shared.Controls.CleanLabel releasesLabel2;
		private Shared.Controls.CleanLabel newCopyrightLabel;
	}
}