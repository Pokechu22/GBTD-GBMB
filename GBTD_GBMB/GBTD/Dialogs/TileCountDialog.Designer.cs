namespace GB.GBTD.Dialogs
{
	partial class TileCountDialog
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.tileCountGroupBox = new GB.Shared.Controls.GroupBox();
			this.labelTileCount = new GB.Shared.Controls.CleanLabel();
			this.tileCountTextBox = new System.Windows.Forms.TextBox();
			this.tileCountGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(51, 62);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(132, 62);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(218, 62);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 2;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// tileCountGroupBox
			// 
			this.tileCountGroupBox.Controls.Add(this.tileCountTextBox);
			this.tileCountGroupBox.Controls.Add(this.labelTileCount);
			this.tileCountGroupBox.Location = new System.Drawing.Point(0, 0);
			this.tileCountGroupBox.Name = "tileCountGroupBox";
			this.tileCountGroupBox.Size = new System.Drawing.Size(293, 56);
			this.tileCountGroupBox.TabIndex = 3;
			this.tileCountGroupBox.Text = "Tile Count";
			// 
			// labelTileCount
			// 
			this.labelTileCount.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelTileCount.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.labelTileCount.Format = stringFormat1;
			this.labelTileCount.Location = new System.Drawing.Point(9, 13);
			this.labelTileCount.Name = "labelTileCount";
			this.labelTileCount.Size = new System.Drawing.Size(141, 14);
			this.labelTileCount.TabIndex = 4;
			this.labelTileCount.TabStop = false;
			this.labelTileCount.Text = "&Tile Count (786 maximum):";
			// 
			// tileCountTextBox
			// 
			this.tileCountTextBox.Location = new System.Drawing.Point(153, 16);
			this.tileCountTextBox.Name = "tileCountTextBox";
			this.tileCountTextBox.Size = new System.Drawing.Size(100, 20);
			this.tileCountTextBox.TabIndex = 5;
			// 
			// TileCountDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(305, 96);
			this.Controls.Add(this.tileCountGroupBox);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TileCountDialog";
			this.Text = "Tile count";
			this.tileCountGroupBox.ResumeLayout(false);
			this.tileCountGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private Shared.Controls.GroupBox tileCountGroupBox;
		private System.Windows.Forms.TextBox tileCountTextBox;
		private Shared.Controls.CleanLabel labelTileCount;
	}
}