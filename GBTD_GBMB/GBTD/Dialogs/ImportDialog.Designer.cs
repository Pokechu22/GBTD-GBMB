﻿namespace GB.GBTD.Dialogs
{
	partial class ImportDialog
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
			System.Drawing.StringFormat stringFormat5 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat4 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat7 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat6 = new System.Drawing.StringFormat();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.settingsGroupBox = new GB.Shared.Controls.GroupBox();
			this.formatComboBox = new System.Windows.Forms.ComboBox();
			this.firstByteToUseTextBox = new GB.Shared.Controls.NumericTextBox();
			this.firstImportFileTileTextBox = new GB.Shared.Controls.NumericTextBox();
			this.tileCountTextBox = new GB.Shared.Controls.NumericTextBox();
			this.firstGBTDTileTextBox = new GB.Shared.Controls.NumericTextBox();
			this.labelFormat = new GB.Shared.Controls.CleanLabel();
			this.labelFirstByteToUse = new GB.Shared.Controls.CleanLabel();
			this.labelFirstImportFileTile = new GB.Shared.Controls.CleanLabel();
			this.labelTileCount = new GB.Shared.Controls.CleanLabel();
			this.labelFirstGBTDTile = new GB.Shared.Controls.CleanLabel();
			this.fileGroupBox = new GB.Shared.Controls.GroupBox();
			this.typeComboBox = new System.Windows.Forms.ComboBox();
			this.labelType = new GB.Shared.Controls.CleanLabel();
			this.browseButton = new System.Windows.Forms.Button();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.labelFileName = new GB.Shared.Controls.CleanLabel();
			this.settingsGroupBox.SuspendLayout();
			this.fileGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(105, 249);
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
			this.cancelButton.Location = new System.Drawing.Point(185, 249);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(265, 249);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 2;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// settingsGroupBox
			// 
			this.settingsGroupBox.Controls.Add(this.labelFirstGBTDTile);
			this.settingsGroupBox.Controls.Add(this.labelTileCount);
			this.settingsGroupBox.Controls.Add(this.labelFirstImportFileTile);
			this.settingsGroupBox.Controls.Add(this.labelFirstByteToUse);
			this.settingsGroupBox.Controls.Add(this.labelFormat);
			this.settingsGroupBox.Controls.Add(this.firstGBTDTileTextBox);
			this.settingsGroupBox.Controls.Add(this.tileCountTextBox);
			this.settingsGroupBox.Controls.Add(this.firstImportFileTileTextBox);
			this.settingsGroupBox.Controls.Add(this.firstByteToUseTextBox);
			this.settingsGroupBox.Controls.Add(this.formatComboBox);
			this.settingsGroupBox.Location = new System.Drawing.Point(8, 96);
			this.settingsGroupBox.Name = "settingsGroupBox";
			this.settingsGroupBox.Size = new System.Drawing.Size(337, 145);
			this.settingsGroupBox.TabIndex = 4;
			this.settingsGroupBox.Text = "Settings";
			// 
			// formatComboBox
			// 
			this.formatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.formatComboBox.FormattingEnabled = true;
			this.formatComboBox.Items.AddRange(new object[] {
            "Byte per pixel",
            "2 bits per pixel",
            "Gameboy VRAM"});
			this.formatComboBox.Location = new System.Drawing.Point(144, 112);
			this.formatComboBox.Name = "formatComboBox";
			this.formatComboBox.Size = new System.Drawing.Size(145, 21);
			this.formatComboBox.TabIndex = 13;
			// 
			// firstByteToUseTextBox
			// 
			this.firstByteToUseTextBox.Location = new System.Drawing.Point(144, 88);
			this.firstByteToUseTextBox.Name = "firstByteToUseTextBox";
			this.firstByteToUseTextBox.Size = new System.Drawing.Size(57, 20);
			this.firstByteToUseTextBox.TabIndex = 12;
			// 
			// firstImportFileTileTextBox
			// 
			this.firstImportFileTileTextBox.Location = new System.Drawing.Point(144, 64);
			this.firstImportFileTileTextBox.Name = "firstImportFileTileTextBox";
			this.firstImportFileTileTextBox.Size = new System.Drawing.Size(33, 20);
			this.firstImportFileTileTextBox.TabIndex = 11;
			// 
			// tileCountTextBox
			// 
			this.tileCountTextBox.Location = new System.Drawing.Point(144, 40);
			this.tileCountTextBox.Name = "tileCountTextBox";
			this.tileCountTextBox.Size = new System.Drawing.Size(33, 20);
			this.tileCountTextBox.TabIndex = 10;
			// 
			// firstGBTDTileTextBox
			// 
			this.firstGBTDTileTextBox.Location = new System.Drawing.Point(144, 16);
			this.firstGBTDTileTextBox.Name = "firstGBTDTileTextBox";
			this.firstGBTDTileTextBox.Size = new System.Drawing.Size(33, 20);
			this.firstGBTDTileTextBox.TabIndex = 9;
			// 
			// labelFormat
			// 
			this.labelFormat.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFormat.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat5.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat5.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat5.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat5.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFormat.Format = stringFormat5;
			this.labelFormat.Location = new System.Drawing.Point(14, 120);
			this.labelFormat.Name = "labelFormat";
			this.labelFormat.Size = new System.Drawing.Size(40, 14);
			this.labelFormat.TabIndex = 8;
			this.labelFormat.TabStop = false;
			this.labelFormat.Text = "F&ormat";
			// 
			// labelFirstByteToUse
			// 
			this.labelFirstByteToUse.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFirstByteToUse.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat4.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat4.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat4.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat4.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFirstByteToUse.Format = stringFormat4;
			this.labelFirstByteToUse.Location = new System.Drawing.Point(14, 92);
			this.labelFirstByteToUse.Name = "labelFirstByteToUse";
			this.labelFirstByteToUse.Size = new System.Drawing.Size(84, 14);
			this.labelFirstByteToUse.TabIndex = 7;
			this.labelFirstByteToUse.TabStop = false;
			this.labelFirstByteToUse.Text = "&First byte to use";
			// 
			// labelFirstImportFileTile
			// 
			this.labelFirstImportFileTile.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFirstImportFileTile.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFirstImportFileTile.Format = stringFormat3;
			this.labelFirstImportFileTile.Location = new System.Drawing.Point(14, 69);
			this.labelFirstImportFileTile.Name = "labelFirstImportFileTile";
			this.labelFirstImportFileTile.Size = new System.Drawing.Size(122, 14);
			this.labelFirstImportFileTile.TabIndex = 6;
			this.labelFirstImportFileTile.TabStop = false;
			this.labelFirstImportFileTile.Text = "&First tile from import file";
			// 
			// labelTileCount
			// 
			this.labelTileCount.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelTileCount.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.labelTileCount.Format = stringFormat2;
			this.labelTileCount.Location = new System.Drawing.Point(14, 46);
			this.labelTileCount.Name = "labelTileCount";
			this.labelTileCount.Size = new System.Drawing.Size(53, 14);
			this.labelTileCount.TabIndex = 5;
			this.labelTileCount.TabStop = false;
			this.labelTileCount.Text = "Tile &count";
			// 
			// labelFirstGBTDTile
			// 
			this.labelFirstGBTDTile.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFirstGBTDTile.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFirstGBTDTile.Format = stringFormat1;
			this.labelFirstGBTDTile.Location = new System.Drawing.Point(14, 24);
			this.labelFirstGBTDTile.Name = "labelFirstGBTDTile";
			this.labelFirstGBTDTile.Size = new System.Drawing.Size(90, 14);
			this.labelFirstGBTDTile.TabIndex = 4;
			this.labelFirstGBTDTile.TabStop = false;
			this.labelFirstGBTDTile.Text = "First &tile in GBTD";
			// 
			// fileGroupBox
			// 
			this.fileGroupBox.Controls.Add(this.labelFileName);
			this.fileGroupBox.Controls.Add(this.fileNameTextBox);
			this.fileGroupBox.Controls.Add(this.browseButton);
			this.fileGroupBox.Controls.Add(this.labelType);
			this.fileGroupBox.Controls.Add(this.typeComboBox);
			this.fileGroupBox.Location = new System.Drawing.Point(8, 8);
			this.fileGroupBox.Name = "fileGroupBox";
			this.fileGroupBox.Size = new System.Drawing.Size(337, 73);
			this.fileGroupBox.TabIndex = 3;
			this.fileGroupBox.Text = "File";
			// 
			// typeComboBox
			// 
			this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.typeComboBox.FormattingEnabled = true;
			this.typeComboBox.Items.AddRange(new object[] {
            "GBE Files",
            "Binary 8x8 tiles"});
			this.typeComboBox.Location = new System.Drawing.Point(72, 40);
			this.typeComboBox.Name = "typeComboBox";
			this.typeComboBox.Size = new System.Drawing.Size(177, 21);
			this.typeComboBox.TabIndex = 8;
			// 
			// labelType
			// 
			this.labelType.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelType.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat7.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat7.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat7.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat7.Trimming = System.Drawing.StringTrimming.Character;
			this.labelType.Format = stringFormat7;
			this.labelType.Location = new System.Drawing.Point(14, 43);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(29, 14);
			this.labelType.TabIndex = 7;
			this.labelType.TabStop = false;
			this.labelType.Text = "T&ype";
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(256, 16);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(65, 21);
			this.browseButton.TabIndex = 6;
			this.browseButton.Text = "&Browse...";
			this.browseButton.UseVisualStyleBackColor = true;
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Location = new System.Drawing.Point(72, 16);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(177, 20);
			this.fileNameTextBox.TabIndex = 5;
			// 
			// labelFileName
			// 
			this.labelFileName.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFileName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat6.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat6.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat6.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat6.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFileName.Format = stringFormat6;
			this.labelFileName.Location = new System.Drawing.Point(14, 19);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(51, 14);
			this.labelFileName.TabIndex = 4;
			this.labelFileName.TabStop = false;
			this.labelFileName.Text = "File&name";
			// 
			// ImportDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(353, 282);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.settingsGroupBox);
			this.Controls.Add(this.fileGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportDialog";
			this.Text = "Import";
			this.settingsGroupBox.ResumeLayout(false);
			this.settingsGroupBox.PerformLayout();
			this.fileGroupBox.ResumeLayout(false);
			this.fileGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.GroupBox fileGroupBox;
		private Shared.Controls.GroupBox settingsGroupBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.ComboBox typeComboBox;
		private Shared.Controls.CleanLabel labelType;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.TextBox fileNameTextBox;
		private Shared.Controls.CleanLabel labelFileName;
		private Shared.Controls.CleanLabel labelTileCount;
		private Shared.Controls.CleanLabel labelFirstGBTDTile;
		private System.Windows.Forms.ComboBox formatComboBox;
		private GB.Shared.Controls.NumericTextBox firstByteToUseTextBox;
		private GB.Shared.Controls.NumericTextBox firstImportFileTileTextBox;
		private GB.Shared.Controls.NumericTextBox tileCountTextBox;
		private GB.Shared.Controls.NumericTextBox firstGBTDTileTextBox;
		private Shared.Controls.CleanLabel labelFormat;
		private Shared.Controls.CleanLabel labelFirstByteToUse;
		private Shared.Controls.CleanLabel labelFirstImportFileTile;
	}
}