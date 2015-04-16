namespace GB.GBMB.Dialogs
{
	partial class ExportOptionsDialog
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.standardTabPage = new System.Windows.Forms.TabPage();
			this.locationFormatTabPage = new System.Windows.Forms.TabPage();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.fileGroupBox = new GB.Shared.Controls.GroupBox();
			this.settingsGroupBox = new GB.Shared.Controls.GroupBox();
			this.splitDataGroupBox = new GB.Shared.Controls.GroupBox();
			this.labelFileName = new GB.Shared.Controls.CleanLabel();
			this.labelType = new GB.Shared.Controls.CleanLabel();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.typeDropDown = new System.Windows.Forms.ComboBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.labelLabel = new GB.Shared.Controls.CleanLabel();
			this.labelSection = new GB.Shared.Controls.CleanLabel();
			this.labelBank = new GB.Shared.Controls.CleanLabel();
			this.labelTextBox = new System.Windows.Forms.TextBox();
			this.sectionTextBox = new System.Windows.Forms.TextBox();
			this.bankTextBox = new GB.Shared.Controls.NumericTextBox();
			this.tabControl.SuspendLayout();
			this.standardTabPage.SuspendLayout();
			this.fileGroupBox.SuspendLayout();
			this.settingsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.standardTabPage);
			this.tabControl.Controls.Add(this.locationFormatTabPage);
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(457, 303);
			this.tabControl.TabIndex = 0;
			// 
			// standardTabPage
			// 
			this.standardTabPage.Controls.Add(this.splitDataGroupBox);
			this.standardTabPage.Controls.Add(this.settingsGroupBox);
			this.standardTabPage.Controls.Add(this.fileGroupBox);
			this.standardTabPage.Location = new System.Drawing.Point(4, 22);
			this.standardTabPage.Name = "standardTabPage";
			this.standardTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.standardTabPage.Size = new System.Drawing.Size(449, 277);
			this.standardTabPage.TabIndex = 0;
			this.standardTabPage.Text = "Standard";
			this.standardTabPage.UseVisualStyleBackColor = true;
			// 
			// locationFormatTabPage
			// 
			this.locationFormatTabPage.Location = new System.Drawing.Point(4, 22);
			this.locationFormatTabPage.Name = "locationFormatTabPage";
			this.locationFormatTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.locationFormatTabPage.Size = new System.Drawing.Size(440, 267);
			this.locationFormatTabPage.TabIndex = 1;
			this.locationFormatTabPage.Text = "Location format";
			this.locationFormatTabPage.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(224, 318);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(303, 318);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(382, 318);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 3;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// fileGroupBox
			// 
			this.fileGroupBox.Controls.Add(this.browseButton);
			this.fileGroupBox.Controls.Add(this.typeDropDown);
			this.fileGroupBox.Controls.Add(this.fileNameTextBox);
			this.fileGroupBox.Controls.Add(this.labelType);
			this.fileGroupBox.Controls.Add(this.labelFileName);
			this.fileGroupBox.Location = new System.Drawing.Point(8, 10);
			this.fileGroupBox.Name = "fileGroupBox";
			this.fileGroupBox.Size = new System.Drawing.Size(433, 73);
			this.fileGroupBox.TabIndex = 0;
			this.fileGroupBox.Text = "File";
			// 
			// settingsGroupBox
			// 
			this.settingsGroupBox.Controls.Add(this.bankTextBox);
			this.settingsGroupBox.Controls.Add(this.sectionTextBox);
			this.settingsGroupBox.Controls.Add(this.labelTextBox);
			this.settingsGroupBox.Controls.Add(this.labelBank);
			this.settingsGroupBox.Controls.Add(this.labelSection);
			this.settingsGroupBox.Controls.Add(this.labelLabel);
			this.settingsGroupBox.Location = new System.Drawing.Point(8, 90);
			this.settingsGroupBox.Name = "settingsGroupBox";
			this.settingsGroupBox.Size = new System.Drawing.Size(433, 97);
			this.settingsGroupBox.TabIndex = 1;
			this.settingsGroupBox.Text = "Settings";
			// 
			// splitDataGroupBox
			// 
			this.splitDataGroupBox.Location = new System.Drawing.Point(8, 194);
			this.splitDataGroupBox.Name = "splitDataGroupBox";
			this.splitDataGroupBox.Size = new System.Drawing.Size(433, 73);
			this.splitDataGroupBox.TabIndex = 2;
			this.splitDataGroupBox.Text = "Split data";
			// 
			// labelFileName
			// 
			this.labelFileName.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFileName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat5.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat5.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat5.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat5.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFileName.Format = stringFormat5;
			this.labelFileName.Location = new System.Drawing.Point(14, 19);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(51, 14);
			this.labelFileName.TabIndex = 4;
			this.labelFileName.TabStop = false;
			this.labelFileName.Text = "File&name";
			// 
			// labelType
			// 
			this.labelType.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelType.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat4.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat4.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat4.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat4.Trimming = System.Drawing.StringTrimming.Character;
			this.labelType.Format = stringFormat4;
			this.labelType.Location = new System.Drawing.Point(14, 43);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(29, 14);
			this.labelType.TabIndex = 5;
			this.labelType.TabStop = false;
			this.labelType.Text = "T&ype";
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Location = new System.Drawing.Point(72, 16);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(281, 20);
			this.fileNameTextBox.TabIndex = 6;
			// 
			// typeDropDown
			// 
			this.typeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.typeDropDown.FormattingEnabled = true;
			this.typeDropDown.Location = new System.Drawing.Point(72, 40);
			this.typeDropDown.Name = "typeDropDown";
			this.typeDropDown.Size = new System.Drawing.Size(177, 21);
			this.typeDropDown.TabIndex = 7;
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(359, 16);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(65, 21);
			this.browseButton.TabIndex = 8;
			this.browseButton.Text = "&Browse...";
			this.browseButton.UseVisualStyleBackColor = true;
			// 
			// labelLabel
			// 
			this.labelLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.labelLabel.Format = stringFormat3;
			this.labelLabel.Location = new System.Drawing.Point(14, 19);
			this.labelLabel.Name = "labelLabel";
			this.labelLabel.Size = new System.Drawing.Size(32, 14);
			this.labelLabel.TabIndex = 4;
			this.labelLabel.TabStop = false;
			this.labelLabel.Text = "&Label";
			// 
			// labelSection
			// 
			this.labelSection.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelSection.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.labelSection.Format = stringFormat2;
			this.labelSection.Location = new System.Drawing.Point(14, 43);
			this.labelSection.Name = "labelSection";
			this.labelSection.Size = new System.Drawing.Size(42, 14);
			this.labelSection.TabIndex = 5;
			this.labelSection.TabStop = false;
			this.labelSection.Text = "&Section";
			// 
			// labelBank
			// 
			this.labelBank.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelBank.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.labelBank.Format = stringFormat1;
			this.labelBank.Location = new System.Drawing.Point(14, 67);
			this.labelBank.Name = "labelBank";
			this.labelBank.Size = new System.Drawing.Size(30, 14);
			this.labelBank.TabIndex = 6;
			this.labelBank.TabStop = false;
			this.labelBank.Text = "B&ank";
			// 
			// labelTextBox
			// 
			this.labelTextBox.Location = new System.Drawing.Point(72, 16);
			this.labelTextBox.Name = "labelTextBox";
			this.labelTextBox.Size = new System.Drawing.Size(351, 20);
			this.labelTextBox.TabIndex = 7;
			// 
			// sectionTextBox
			// 
			this.sectionTextBox.Location = new System.Drawing.Point(72, 40);
			this.sectionTextBox.Name = "sectionTextBox";
			this.sectionTextBox.Size = new System.Drawing.Size(249, 20);
			this.sectionTextBox.TabIndex = 8;
			// 
			// bankTextBox
			// 
			this.bankTextBox.Location = new System.Drawing.Point(72, 64);
			this.bankTextBox.Name = "bankTextBox";
			this.bankTextBox.Size = new System.Drawing.Size(33, 21);
			this.bankTextBox.TabIndex = 9;
			this.bankTextBox.Value = ((uint)(0u));
			// 
			// ExportOptionsDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(472, 351);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportOptionsDialog";
			this.Text = "Export options";
			this.tabControl.ResumeLayout(false);
			this.standardTabPage.ResumeLayout(false);
			this.fileGroupBox.ResumeLayout(false);
			this.fileGroupBox.PerformLayout();
			this.settingsGroupBox.ResumeLayout(false);
			this.settingsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage locationFormatTabPage;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.TabPage standardTabPage;
		private Shared.Controls.GroupBox splitDataGroupBox;
		private Shared.Controls.GroupBox settingsGroupBox;
		private Shared.Controls.GroupBox fileGroupBox;
		private System.Windows.Forms.ComboBox typeDropDown;
		private System.Windows.Forms.TextBox fileNameTextBox;
		private Shared.Controls.CleanLabel labelType;
		private Shared.Controls.CleanLabel labelFileName;
		private System.Windows.Forms.Button browseButton;
		private Shared.Controls.NumericTextBox bankTextBox;
		private System.Windows.Forms.TextBox sectionTextBox;
		private System.Windows.Forms.TextBox labelTextBox;
		private Shared.Controls.CleanLabel labelBank;
		private Shared.Controls.CleanLabel labelSection;
		private Shared.Controls.CleanLabel labelLabel;
	}
}