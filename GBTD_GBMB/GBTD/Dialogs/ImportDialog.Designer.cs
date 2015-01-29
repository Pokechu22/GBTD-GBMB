namespace GB.GBTD.Dialogs
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
			System.Drawing.StringFormat stringFormat12 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat11 = new System.Drawing.StringFormat();
			this.fileGroupBox = new GB.Shared.Controls.GroupBox();
			this.settingsGroupBox = new GB.Shared.Controls.GroupBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.labelFileName = new GB.Shared.Controls.CleanLabel();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.labelType = new GB.Shared.Controls.CleanLabel();
			this.typeComboBox = new System.Windows.Forms.ComboBox();
			this.fileGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// fileGroupBox
			// 
			this.fileGroupBox.Controls.Add(this.typeComboBox);
			this.fileGroupBox.Controls.Add(this.labelType);
			this.fileGroupBox.Controls.Add(this.browseButton);
			this.fileGroupBox.Controls.Add(this.fileNameTextBox);
			this.fileGroupBox.Controls.Add(this.labelFileName);
			this.fileGroupBox.Location = new System.Drawing.Point(8, 8);
			this.fileGroupBox.Name = "fileGroupBox";
			this.fileGroupBox.Size = new System.Drawing.Size(337, 73);
			this.fileGroupBox.TabIndex = 3;
			this.fileGroupBox.Text = "File";
			// 
			// settingsGroupBox
			// 
			this.settingsGroupBox.Location = new System.Drawing.Point(8, 96);
			this.settingsGroupBox.Name = "settingsGroupBox";
			this.settingsGroupBox.Size = new System.Drawing.Size(337, 145);
			this.settingsGroupBox.TabIndex = 4;
			this.settingsGroupBox.Text = "Settings";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(105, 249);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
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
			// labelFileName
			// 
			this.labelFileName.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFileName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat12.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat12.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat12.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat12.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFileName.Format = stringFormat12;
			this.labelFileName.Location = new System.Drawing.Point(14, 19);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(51, 14);
			this.labelFileName.TabIndex = 4;
			this.labelFileName.TabStop = false;
			this.labelFileName.Text = "File&name";
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Location = new System.Drawing.Point(72, 16);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(177, 20);
			this.fileNameTextBox.TabIndex = 5;
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
			// labelType
			// 
			this.labelType.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelType.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat11.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat11.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat11.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat11.Trimming = System.Drawing.StringTrimming.Character;
			this.labelType.Format = stringFormat11;
			this.labelType.Location = new System.Drawing.Point(14, 43);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(29, 14);
			this.labelType.TabIndex = 7;
			this.labelType.TabStop = false;
			this.labelType.Text = "T&ype";
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
	}
}