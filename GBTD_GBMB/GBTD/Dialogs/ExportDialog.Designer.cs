namespace GB.GBTD.Dialogs
{
	partial class ExportDialog
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
			System.Drawing.StringFormat stringFormat16 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat15 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat14 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat13 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat12 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat11 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat10 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat17 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat18 = new System.Drawing.StringFormat();
			this.okButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageStandard = new System.Windows.Forms.TabPage();
			this.tabPageAdvanced = new System.Windows.Forms.TabPage();
			this.groupBoxSettings = new GB.Shared.Controls.GroupBox();
			this.formatComboBox = new System.Windows.Forms.ComboBox();
			this.toTextBox = new System.Windows.Forms.TextBox();
			this.fromTextBox = new System.Windows.Forms.TextBox();
			this.bankTextBox = new System.Windows.Forms.TextBox();
			this.sectionTextBox = new System.Windows.Forms.TextBox();
			this.labelTextBox = new System.Windows.Forms.TextBox();
			this.labelCounter = new GB.Shared.Controls.CleanLabel();
			this.labelFormat = new GB.Shared.Controls.CleanLabel();
			this.labelTo = new GB.Shared.Controls.CleanLabel();
			this.labelFrom = new GB.Shared.Controls.CleanLabel();
			this.labelBank = new GB.Shared.Controls.CleanLabel();
			this.labelSection = new GB.Shared.Controls.CleanLabel();
			this.labelLabel = new GB.Shared.Controls.CleanLabel();
			this.counterComboBox = new System.Windows.Forms.ComboBox();
			this.singleUnitCheckBox = new System.Windows.Forms.CheckBox();
			this.gbCompressCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBoxFile = new GB.Shared.Controls.GroupBox();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.labelType = new GB.Shared.Controls.CleanLabel();
			this.labelFileName = new GB.Shared.Controls.CleanLabel();
			this.fileTypeComboBox = new System.Windows.Forms.ComboBox();
			this.groupBoxSplitData = new GB.Shared.Controls.GroupBox();
			this.groupBoxMetatiles = new GB.Shared.Controls.GroupBox();
			this.groupBoxColors = new GB.Shared.Controls.GroupBox();
			this.tabControl.SuspendLayout();
			this.tabPageStandard.SuspendLayout();
			this.tabPageAdvanced.SuspendLayout();
			this.groupBoxSettings.SuspendLayout();
			this.groupBoxFile.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(225, 346);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(385, 346);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 1;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(305, 346);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageStandard);
			this.tabControl.Controls.Add(this.tabPageAdvanced);
			this.tabControl.Location = new System.Drawing.Point(8, 8);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(457, 329);
			this.tabControl.TabIndex = 3;
			// 
			// tabPageStandard
			// 
			this.tabPageStandard.Controls.Add(this.groupBoxSettings);
			this.tabPageStandard.Controls.Add(this.groupBoxFile);
			this.tabPageStandard.Location = new System.Drawing.Point(4, 22);
			this.tabPageStandard.Name = "tabPageStandard";
			this.tabPageStandard.Padding = new System.Windows.Forms.Padding(5, 7, 5, 9);
			this.tabPageStandard.Size = new System.Drawing.Size(449, 303);
			this.tabPageStandard.TabIndex = 0;
			this.tabPageStandard.Text = "Standard";
			this.tabPageStandard.UseVisualStyleBackColor = true;
			// 
			// tabPageAdvanced
			// 
			this.tabPageAdvanced.Controls.Add(this.groupBoxSplitData);
			this.tabPageAdvanced.Controls.Add(this.groupBoxMetatiles);
			this.tabPageAdvanced.Controls.Add(this.groupBoxColors);
			this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
			this.tabPageAdvanced.Name = "tabPageAdvanced";
			this.tabPageAdvanced.Padding = new System.Windows.Forms.Padding(5, 7, 5, 9);
			this.tabPageAdvanced.Size = new System.Drawing.Size(449, 303);
			this.tabPageAdvanced.TabIndex = 1;
			this.tabPageAdvanced.Text = "Advanced";
			this.tabPageAdvanced.UseVisualStyleBackColor = true;
			// 
			// groupBoxSettings
			// 
			this.groupBoxSettings.Controls.Add(this.gbCompressCheckBox);
			this.groupBoxSettings.Controls.Add(this.singleUnitCheckBox);
			this.groupBoxSettings.Controls.Add(this.counterComboBox);
			this.groupBoxSettings.Controls.Add(this.labelLabel);
			this.groupBoxSettings.Controls.Add(this.labelSection);
			this.groupBoxSettings.Controls.Add(this.labelBank);
			this.groupBoxSettings.Controls.Add(this.labelFrom);
			this.groupBoxSettings.Controls.Add(this.labelTo);
			this.groupBoxSettings.Controls.Add(this.labelFormat);
			this.groupBoxSettings.Controls.Add(this.labelCounter);
			this.groupBoxSettings.Controls.Add(this.labelTextBox);
			this.groupBoxSettings.Controls.Add(this.sectionTextBox);
			this.groupBoxSettings.Controls.Add(this.bankTextBox);
			this.groupBoxSettings.Controls.Add(this.fromTextBox);
			this.groupBoxSettings.Controls.Add(this.toTextBox);
			this.groupBoxSettings.Controls.Add(this.formatComboBox);
			this.groupBoxSettings.Location = new System.Drawing.Point(8, 90);
			this.groupBoxSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.groupBoxSettings.Name = "groupBoxSettings";
			this.groupBoxSettings.Size = new System.Drawing.Size(433, 201);
			this.groupBoxSettings.TabIndex = 1;
			this.groupBoxSettings.Text = "Settings";
			// 
			// formatComboBox
			// 
			this.formatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.formatComboBox.FormattingEnabled = true;
			this.formatComboBox.Items.AddRange(new object[] {
            "Gameboy 4 color",
            "Gameboy 2 color",
            "Byte per color",
            "Consecutive 4 color"});
			this.formatComboBox.Location = new System.Drawing.Point(72, 120);
			this.formatComboBox.Name = "formatComboBox";
			this.formatComboBox.Size = new System.Drawing.Size(185, 21);
			this.formatComboBox.TabIndex = 11;
			// 
			// toTextBox
			// 
			this.toTextBox.Location = new System.Drawing.Point(144, 91);
			this.toTextBox.Name = "toTextBox";
			this.toTextBox.Size = new System.Drawing.Size(33, 20);
			this.toTextBox.TabIndex = 9;
			this.toTextBox.Text = "0";
			// 
			// fromTextBox
			// 
			this.fromTextBox.Location = new System.Drawing.Point(72, 91);
			this.fromTextBox.Name = "fromTextBox";
			this.fromTextBox.Size = new System.Drawing.Size(33, 20);
			this.fromTextBox.TabIndex = 7;
			this.fromTextBox.Text = "0";
			// 
			// bankTextBox
			// 
			this.bankTextBox.Location = new System.Drawing.Point(72, 64);
			this.bankTextBox.Name = "bankTextBox";
			this.bankTextBox.Size = new System.Drawing.Size(33, 20);
			this.bankTextBox.TabIndex = 5;
			this.bankTextBox.Text = "0";
			// 
			// sectionTextBox
			// 
			this.sectionTextBox.Location = new System.Drawing.Point(72, 40);
			this.sectionTextBox.Name = "sectionTextBox";
			this.sectionTextBox.Size = new System.Drawing.Size(185, 20);
			this.sectionTextBox.TabIndex = 3;
			// 
			// labelTextBox
			// 
			this.labelTextBox.Location = new System.Drawing.Point(72, 16);
			this.labelTextBox.Name = "labelTextBox";
			this.labelTextBox.Size = new System.Drawing.Size(185, 20);
			this.labelTextBox.TabIndex = 1;
			// 
			// labelCounter
			// 
			stringFormat16.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat16.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat16.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat16.Trimming = System.Drawing.StringTrimming.Character;
			this.labelCounter.Format = stringFormat16;
			this.labelCounter.Location = new System.Drawing.Point(14, 148);
			this.labelCounter.Name = "labelCounter";
			this.labelCounter.Size = new System.Drawing.Size(44, 14);
			this.labelCounter.TabIndex = 12;
			this.labelCounter.Text = "&Counter";
			// 
			// labelFormat
			// 
			stringFormat15.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat15.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat15.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat15.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFormat.Format = stringFormat15;
			this.labelFormat.Location = new System.Drawing.Point(14, 124);
			this.labelFormat.Name = "labelFormat";
			this.labelFormat.Size = new System.Drawing.Size(40, 14);
			this.labelFormat.TabIndex = 10;
			this.labelFormat.Text = "F&ormat";
			// 
			// labelTo
			// 
			stringFormat14.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat14.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat14.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat14.Trimming = System.Drawing.StringTrimming.Character;
			this.labelTo.Format = stringFormat14;
			this.labelTo.Location = new System.Drawing.Point(118, 94);
			this.labelTo.Name = "labelTo";
			this.labelTo.Size = new System.Drawing.Size(17, 14);
			this.labelTo.TabIndex = 8;
			this.labelTo.Text = "&To";
			// 
			// labelFrom
			// 
			stringFormat13.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat13.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat13.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat13.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFrom.Format = stringFormat13;
			this.labelFrom.Location = new System.Drawing.Point(14, 94);
			this.labelFrom.Name = "labelFrom";
			this.labelFrom.Size = new System.Drawing.Size(31, 14);
			this.labelFrom.TabIndex = 6;
			this.labelFrom.Text = "&From";
			// 
			// labelBank
			// 
			stringFormat12.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat12.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat12.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat12.Trimming = System.Drawing.StringTrimming.Character;
			this.labelBank.Format = stringFormat12;
			this.labelBank.Location = new System.Drawing.Point(14, 67);
			this.labelBank.Name = "labelBank";
			this.labelBank.Size = new System.Drawing.Size(30, 14);
			this.labelBank.TabIndex = 4;
			this.labelBank.Text = "B&ank";
			// 
			// labelSection
			// 
			stringFormat11.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat11.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat11.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat11.Trimming = System.Drawing.StringTrimming.Character;
			this.labelSection.Format = stringFormat11;
			this.labelSection.Location = new System.Drawing.Point(14, 43);
			this.labelSection.Name = "labelSection";
			this.labelSection.Size = new System.Drawing.Size(42, 14);
			this.labelSection.TabIndex = 2;
			this.labelSection.Text = "&Section";
			// 
			// labelLabel
			// 
			stringFormat10.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat10.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat10.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat10.Trimming = System.Drawing.StringTrimming.Character;
			this.labelLabel.Format = stringFormat10;
			this.labelLabel.Location = new System.Drawing.Point(14, 19);
			this.labelLabel.Name = "labelLabel";
			this.labelLabel.Size = new System.Drawing.Size(32, 14);
			this.labelLabel.TabIndex = 0;
			this.labelLabel.Text = "&Label";
			// 
			// counterComboBox
			// 
			this.counterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.counterComboBox.FormattingEnabled = true;
			this.counterComboBox.Items.AddRange(new object[] {
            "None",
            "Byte-count as Byte",
            "Byte-count as Word",
            "Byte-count as Constant",
            "Tile-count as Byte",
            "Tile-count as Word",
            "Tile-count as Constant",
            "8x8-count as Byte",
            "8x8-count as Word",
            "8x8-count as Constant"});
			this.counterComboBox.Location = new System.Drawing.Point(72, 144);
			this.counterComboBox.Name = "counterComboBox";
			this.counterComboBox.Size = new System.Drawing.Size(185, 21);
			this.counterComboBox.TabIndex = 13;
			// 
			// singleUnitCheckBox
			// 
			this.singleUnitCheckBox.AutoSize = true;
			this.singleUnitCheckBox.Location = new System.Drawing.Point(280, 16);
			this.singleUnitCheckBox.Name = "singleUnitCheckBox";
			this.singleUnitCheckBox.Size = new System.Drawing.Size(132, 17);
			this.singleUnitCheckBox.TabIndex = 14;
			this.singleUnitCheckBox.Text = "&Export tiles as one unit";
			this.singleUnitCheckBox.UseVisualStyleBackColor = true;
			// 
			// gbCompressCheckBox
			// 
			this.gbCompressCheckBox.AutoSize = true;
			this.gbCompressCheckBox.Location = new System.Drawing.Point(280, 33);
			this.gbCompressCheckBox.Name = "gbCompressCheckBox";
			this.gbCompressCheckBox.Size = new System.Drawing.Size(114, 17);
			this.gbCompressCheckBox.TabIndex = 15;
			this.gbCompressCheckBox.Text = "GB-Comp&ress data";
			this.gbCompressCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupBoxFile
			// 
			this.groupBoxFile.Controls.Add(this.fileTypeComboBox);
			this.groupBoxFile.Controls.Add(this.labelFileName);
			this.groupBoxFile.Controls.Add(this.labelType);
			this.groupBoxFile.Controls.Add(this.browseButton);
			this.groupBoxFile.Controls.Add(this.fileNameTextBox);
			this.groupBoxFile.Location = new System.Drawing.Point(8, 10);
			this.groupBoxFile.Name = "groupBoxFile";
			this.groupBoxFile.Size = new System.Drawing.Size(433, 73);
			this.groupBoxFile.TabIndex = 0;
			this.groupBoxFile.Text = "File";
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Location = new System.Drawing.Point(72, 16);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(281, 20);
			this.fileNameTextBox.TabIndex = 1;
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(360, 16);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(65, 21);
			this.browseButton.TabIndex = 2;
			this.browseButton.Text = "&Browse...";
			this.browseButton.UseVisualStyleBackColor = true;
			// 
			// labelType
			// 
			stringFormat17.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat17.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat17.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat17.Trimming = System.Drawing.StringTrimming.Character;
			this.labelType.Format = stringFormat17;
			this.labelType.Location = new System.Drawing.Point(14, 43);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(29, 14);
			this.labelType.TabIndex = 3;
			this.labelType.Text = "T&ype";
			// 
			// labelFileName
			// 
			stringFormat18.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat18.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat18.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat18.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFileName.Format = stringFormat18;
			this.labelFileName.Location = new System.Drawing.Point(14, 19);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(51, 14);
			this.labelFileName.TabIndex = 0;
			this.labelFileName.Text = "Filena&me";
			// 
			// fileTypeComboBox
			// 
			this.fileTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fileTypeComboBox.FormattingEnabled = true;
			this.fileTypeComboBox.Items.AddRange(new object[] {
            "RGBDS Assembly file (*.z80)",
            "RGBDS Object file (*.obj)",
            "TASM Assembly file (*.z80)",
            "GBDK C file (*.c)",
            "All-purpose binary file (*.bin)",
            "ISAS Assembly file (*.s)"});
			this.fileTypeComboBox.Location = new System.Drawing.Point(72, 40);
			this.fileTypeComboBox.Name = "fileTypeComboBox";
			this.fileTypeComboBox.Size = new System.Drawing.Size(169, 21);
			this.fileTypeComboBox.TabIndex = 4;
			// 
			// groupBoxSplitData
			// 
			this.groupBoxSplitData.Location = new System.Drawing.Point(8, 218);
			this.groupBoxSplitData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.groupBoxSplitData.Name = "groupBoxSplitData";
			this.groupBoxSplitData.Size = new System.Drawing.Size(433, 73);
			this.groupBoxSplitData.TabIndex = 2;
			this.groupBoxSplitData.Text = "Split data";
			// 
			// groupBoxMetatiles
			// 
			this.groupBoxMetatiles.Location = new System.Drawing.Point(8, 114);
			this.groupBoxMetatiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.groupBoxMetatiles.Name = "groupBoxMetatiles";
			this.groupBoxMetatiles.Size = new System.Drawing.Size(433, 97);
			this.groupBoxMetatiles.TabIndex = 1;
			this.groupBoxMetatiles.Text = "Metatiles";
			// 
			// groupBoxColors
			// 
			this.groupBoxColors.Location = new System.Drawing.Point(8, 10);
			this.groupBoxColors.Name = "groupBoxColors";
			this.groupBoxColors.Size = new System.Drawing.Size(433, 97);
			this.groupBoxColors.TabIndex = 0;
			this.groupBoxColors.Text = "Colors";
			// 
			// ExportDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(477, 379);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportDialog";
			this.Text = "Export";
			this.tabControl.ResumeLayout(false);
			this.tabPageStandard.ResumeLayout(false);
			this.tabPageAdvanced.ResumeLayout(false);
			this.groupBoxSettings.ResumeLayout(false);
			this.groupBoxSettings.PerformLayout();
			this.groupBoxFile.ResumeLayout(false);
			this.groupBoxFile.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageStandard;
		private System.Windows.Forms.TabPage tabPageAdvanced;
		private Shared.Controls.GroupBox groupBoxFile;
		private Shared.Controls.GroupBox groupBoxSettings;
		private Shared.Controls.GroupBox groupBoxSplitData;
		private Shared.Controls.GroupBox groupBoxMetatiles;
		private Shared.Controls.GroupBox groupBoxColors;
		private Shared.Controls.CleanLabel labelType;
		private Shared.Controls.CleanLabel labelFileName;
		private System.Windows.Forms.TextBox fileNameTextBox;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.ComboBox fileTypeComboBox;
		private System.Windows.Forms.TextBox labelTextBox;
		private Shared.Controls.CleanLabel labelCounter;
		private Shared.Controls.CleanLabel labelFormat;
		private Shared.Controls.CleanLabel labelTo;
		private Shared.Controls.CleanLabel labelFrom;
		private Shared.Controls.CleanLabel labelBank;
		private Shared.Controls.CleanLabel labelSection;
		private Shared.Controls.CleanLabel labelLabel;
		private System.Windows.Forms.TextBox sectionTextBox;
		private System.Windows.Forms.ComboBox counterComboBox;
		private System.Windows.Forms.TextBox bankTextBox;
		private System.Windows.Forms.TextBox fromTextBox;
		private System.Windows.Forms.TextBox toTextBox;
		private System.Windows.Forms.ComboBox formatComboBox;
		private System.Windows.Forms.CheckBox gbCompressCheckBox;
		private System.Windows.Forms.CheckBox singleUnitCheckBox;
	}
}