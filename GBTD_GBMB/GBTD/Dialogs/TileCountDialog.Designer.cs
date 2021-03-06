﻿namespace GB.GBTD.Dialogs
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
			this.tileCountTextBox = new GB.Shared.Controls.NumericTextBox();
			this.tileCountGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(53, 64);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(133, 64);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(213, 64);
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
			this.tileCountGroupBox.Location = new System.Drawing.Point(8, 8);
			this.tileCountGroupBox.Name = "tileCountGroupBox";
			this.tileCountGroupBox.Size = new System.Drawing.Size(289, 47);
			this.tileCountGroupBox.TabIndex = 3;
			this.tileCountGroupBox.Text = "Tile count";
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
			this.labelTileCount.Location = new System.Drawing.Point(14, 19);
			this.labelTileCount.Name = "labelTileCount";
			this.labelTileCount.Size = new System.Drawing.Size(138, 14);
			this.labelTileCount.TabIndex = 4;
			this.labelTileCount.TabStop = false;
			this.labelTileCount.Text = "&Tile count (768 maximum):";
			// 
			// tileCountTextBox
			// 
			this.tileCountTextBox.Location = new System.Drawing.Point(152, 16);
			this.tileCountTextBox.Name = "tileCountTextBox";
			this.tileCountTextBox.Size = new System.Drawing.Size(49, 20);
			this.tileCountTextBox.TabIndex = 5;
			this.tileCountTextBox.Value = ((uint)(0u));
			// 
			// TileCountDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(305, 96);
			this.Controls.Add(this.tileCountGroupBox);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TileCountDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Tile count";
			this.tileCountGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private Shared.Controls.GroupBox tileCountGroupBox;
		private GB.Shared.Controls.NumericTextBox tileCountTextBox;
		private Shared.Controls.CleanLabel labelTileCount;
	}
}