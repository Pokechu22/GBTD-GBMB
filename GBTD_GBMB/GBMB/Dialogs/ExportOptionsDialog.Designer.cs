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
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat4 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat6 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat5 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat10 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat9 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat8 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat7 = new System.Drawing.StringFormat();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.standardTabPage = new System.Windows.Forms.TabPage();
			this.splitDataGroupBox = new GB.Shared.Controls.GroupBox();
			this.changeBankForEachBlockCheckBox = new System.Windows.Forms.CheckBox();
			this.blockSizeTextBox = new GB.Shared.Controls.NumericTextBox();
			this.labelBlockSize = new GB.Shared.Controls.CleanLabel();
			this.splitDataCheckBox = new System.Windows.Forms.CheckBox();
			this.settingsGroupBox = new GB.Shared.Controls.GroupBox();
			this.bankTextBox = new GB.Shared.Controls.NumericTextBox();
			this.sectionTextBox = new System.Windows.Forms.TextBox();
			this.labelTextBox = new System.Windows.Forms.TextBox();
			this.labelBank = new GB.Shared.Controls.CleanLabel();
			this.labelSection = new GB.Shared.Controls.CleanLabel();
			this.labelLabel = new GB.Shared.Controls.CleanLabel();
			this.fileGroupBox = new GB.Shared.Controls.GroupBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.typeDropDown = new System.Windows.Forms.ComboBox();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.labelType = new GB.Shared.Controls.CleanLabel();
			this.labelFileName = new GB.Shared.Controls.CleanLabel();
			this.locationFormatTabPage = new System.Windows.Forms.TabPage();
			this.locationFormatGroupBox = new GB.Shared.Controls.GroupBox();
			this.removeRowButton = new System.Windows.Forms.Button();
			this.addRowButton = new System.Windows.Forms.Button();
			this.labelMapLayout = new GB.Shared.Controls.CleanLabel();
			this.labelPlaneCount = new GB.Shared.Controls.CleanLabel();
			this.cleanLabel3 = new GB.Shared.Controls.CleanLabel();
			this.cleanLabel4 = new GB.Shared.Controls.CleanLabel();
			this.mapLayoutComboBox = new System.Windows.Forms.ComboBox();
			this.planeCountComboBox = new System.Windows.Forms.ComboBox();
			this.planeOrderComboBox = new System.Windows.Forms.ComboBox();
			this.tileOffsetTextBox = new GB.Shared.Controls.NumericTextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.propEditControl = new GB.GBMB.Dialogs.ExportPropertiesEditControl();
			this.resultingPlanesControl = new GB.GBMB.Dialogs.ResultingExportPlanesControl();
			this.tabControl.SuspendLayout();
			this.standardTabPage.SuspendLayout();
			this.splitDataGroupBox.SuspendLayout();
			this.settingsGroupBox.SuspendLayout();
			this.fileGroupBox.SuspendLayout();
			this.locationFormatTabPage.SuspendLayout();
			this.locationFormatGroupBox.SuspendLayout();
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
			// splitDataGroupBox
			// 
			this.splitDataGroupBox.Controls.Add(this.splitDataCheckBox);
			this.splitDataGroupBox.Controls.Add(this.labelBlockSize);
			this.splitDataGroupBox.Controls.Add(this.blockSizeTextBox);
			this.splitDataGroupBox.Controls.Add(this.changeBankForEachBlockCheckBox);
			this.splitDataGroupBox.Location = new System.Drawing.Point(8, 194);
			this.splitDataGroupBox.Name = "splitDataGroupBox";
			this.splitDataGroupBox.Size = new System.Drawing.Size(433, 73);
			this.splitDataGroupBox.TabIndex = 2;
			this.splitDataGroupBox.Text = "Split data";
			// 
			// changeBankForEachBlockCheckBox
			// 
			this.changeBankForEachBlockCheckBox.AutoSize = true;
			this.changeBankForEachBlockCheckBox.Enabled = false;
			this.changeBankForEachBlockCheckBox.Location = new System.Drawing.Point(144, 44);
			this.changeBankForEachBlockCheckBox.Name = "changeBankForEachBlockCheckBox";
			this.changeBankForEachBlockCheckBox.Size = new System.Drawing.Size(161, 17);
			this.changeBankForEachBlockCheckBox.TabIndex = 7;
			this.changeBankForEachBlockCheckBox.Text = "&Change bank for each block";
			this.changeBankForEachBlockCheckBox.UseVisualStyleBackColor = true;
			// 
			// blockSizeTextBox
			// 
			this.blockSizeTextBox.Enabled = false;
			this.blockSizeTextBox.Location = new System.Drawing.Point(72, 41);
			this.blockSizeTextBox.Name = "blockSizeTextBox";
			this.blockSizeTextBox.Size = new System.Drawing.Size(57, 21);
			this.blockSizeTextBox.TabIndex = 6;
			this.blockSizeTextBox.Value = ((uint)(0u));
			// 
			// labelBlockSize
			// 
			this.labelBlockSize.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelBlockSize.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.labelBlockSize.Enabled = false;
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.labelBlockSize.Format = stringFormat1;
			this.labelBlockSize.Location = new System.Drawing.Point(14, 43);
			this.labelBlockSize.Name = "labelBlockSize";
			this.labelBlockSize.Size = new System.Drawing.Size(55, 14);
			this.labelBlockSize.TabIndex = 5;
			this.labelBlockSize.TabStop = false;
			this.labelBlockSize.Text = "Bl&ock size";
			// 
			// splitDataCheckBox
			// 
			this.splitDataCheckBox.AutoSize = true;
			this.splitDataCheckBox.Location = new System.Drawing.Point(16, 20);
			this.splitDataCheckBox.Name = "splitDataCheckBox";
			this.splitDataCheckBox.Size = new System.Drawing.Size(70, 17);
			this.splitDataCheckBox.TabIndex = 4;
			this.splitDataCheckBox.Text = "S&plit data";
			this.splitDataCheckBox.UseVisualStyleBackColor = true;
			this.splitDataCheckBox.CheckedChanged += new System.EventHandler(this.splitDataCheckBox_CheckedChanged);
			// 
			// settingsGroupBox
			// 
			this.settingsGroupBox.Controls.Add(this.labelLabel);
			this.settingsGroupBox.Controls.Add(this.labelSection);
			this.settingsGroupBox.Controls.Add(this.labelBank);
			this.settingsGroupBox.Controls.Add(this.labelTextBox);
			this.settingsGroupBox.Controls.Add(this.sectionTextBox);
			this.settingsGroupBox.Controls.Add(this.bankTextBox);
			this.settingsGroupBox.Location = new System.Drawing.Point(8, 90);
			this.settingsGroupBox.Name = "settingsGroupBox";
			this.settingsGroupBox.Size = new System.Drawing.Size(433, 97);
			this.settingsGroupBox.TabIndex = 1;
			this.settingsGroupBox.Text = "Settings";
			// 
			// bankTextBox
			// 
			this.bankTextBox.Location = new System.Drawing.Point(72, 64);
			this.bankTextBox.Name = "bankTextBox";
			this.bankTextBox.Size = new System.Drawing.Size(33, 21);
			this.bankTextBox.TabIndex = 9;
			this.bankTextBox.Value = ((uint)(0u));
			// 
			// sectionTextBox
			// 
			this.sectionTextBox.Location = new System.Drawing.Point(72, 40);
			this.sectionTextBox.Name = "sectionTextBox";
			this.sectionTextBox.Size = new System.Drawing.Size(249, 20);
			this.sectionTextBox.TabIndex = 8;
			// 
			// labelTextBox
			// 
			this.labelTextBox.Location = new System.Drawing.Point(72, 16);
			this.labelTextBox.Name = "labelTextBox";
			this.labelTextBox.Size = new System.Drawing.Size(351, 20);
			this.labelTextBox.TabIndex = 7;
			// 
			// labelBank
			// 
			this.labelBank.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelBank.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat4.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat4.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat4.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat4.Trimming = System.Drawing.StringTrimming.Character;
			this.labelBank.Format = stringFormat4;
			this.labelBank.Location = new System.Drawing.Point(14, 67);
			this.labelBank.Name = "labelBank";
			this.labelBank.Size = new System.Drawing.Size(30, 14);
			this.labelBank.TabIndex = 6;
			this.labelBank.TabStop = false;
			this.labelBank.Text = "B&ank";
			// 
			// labelSection
			// 
			this.labelSection.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelSection.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.labelSection.Format = stringFormat3;
			this.labelSection.Location = new System.Drawing.Point(14, 43);
			this.labelSection.Name = "labelSection";
			this.labelSection.Size = new System.Drawing.Size(42, 14);
			this.labelSection.TabIndex = 5;
			this.labelSection.TabStop = false;
			this.labelSection.Text = "&Section";
			// 
			// labelLabel
			// 
			this.labelLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.labelLabel.Format = stringFormat2;
			this.labelLabel.Location = new System.Drawing.Point(14, 19);
			this.labelLabel.Name = "labelLabel";
			this.labelLabel.Size = new System.Drawing.Size(32, 14);
			this.labelLabel.TabIndex = 4;
			this.labelLabel.TabStop = false;
			this.labelLabel.Text = "&Label";
			// 
			// fileGroupBox
			// 
			this.fileGroupBox.Controls.Add(this.labelFileName);
			this.fileGroupBox.Controls.Add(this.labelType);
			this.fileGroupBox.Controls.Add(this.fileNameTextBox);
			this.fileGroupBox.Controls.Add(this.typeDropDown);
			this.fileGroupBox.Controls.Add(this.browseButton);
			this.fileGroupBox.Location = new System.Drawing.Point(8, 10);
			this.fileGroupBox.Name = "fileGroupBox";
			this.fileGroupBox.Size = new System.Drawing.Size(433, 73);
			this.fileGroupBox.TabIndex = 0;
			this.fileGroupBox.Text = "File";
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(359, 16);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(65, 21);
			this.browseButton.TabIndex = 8;
			this.browseButton.Text = "&Browse...";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// typeDropDown
			// 
			this.typeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.typeDropDown.FormattingEnabled = true;
			this.typeDropDown.Items.AddRange(new object[] {
            "RGBDS Assembly file (*.z80)",
            "RGBDS Object file (*.obj)",
            "TASM Assembly file (*.z80)",
            "GBDK C file (*.c)",
            "All-purpose binary file (*.bin)",
            "ISAS Assembly file (*.s)"});
			this.typeDropDown.Location = new System.Drawing.Point(72, 40);
			this.typeDropDown.Name = "typeDropDown";
			this.typeDropDown.Size = new System.Drawing.Size(177, 21);
			this.typeDropDown.TabIndex = 7;
			this.typeDropDown.SelectedIndexChanged += new System.EventHandler(this.typeDropDown_SelectedIndexChanged);
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Location = new System.Drawing.Point(72, 16);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(281, 20);
			this.fileNameTextBox.TabIndex = 6;
			// 
			// labelType
			// 
			this.labelType.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelType.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat6.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat6.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat6.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat6.Trimming = System.Drawing.StringTrimming.Character;
			this.labelType.Format = stringFormat6;
			this.labelType.Location = new System.Drawing.Point(14, 43);
			this.labelType.Name = "labelType";
			this.labelType.Size = new System.Drawing.Size(29, 14);
			this.labelType.TabIndex = 5;
			this.labelType.TabStop = false;
			this.labelType.Text = "T&ype";
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
			// locationFormatTabPage
			// 
			this.locationFormatTabPage.Controls.Add(this.locationFormatGroupBox);
			this.locationFormatTabPage.Location = new System.Drawing.Point(4, 22);
			this.locationFormatTabPage.Name = "locationFormatTabPage";
			this.locationFormatTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.locationFormatTabPage.Size = new System.Drawing.Size(449, 277);
			this.locationFormatTabPage.TabIndex = 1;
			this.locationFormatTabPage.Text = "Location format";
			this.locationFormatTabPage.UseVisualStyleBackColor = true;
			// 
			// locationFormatGroupBox
			// 
			this.locationFormatGroupBox.Controls.Add(this.tileOffsetTextBox);
			this.locationFormatGroupBox.Controls.Add(this.planeOrderComboBox);
			this.locationFormatGroupBox.Controls.Add(this.planeCountComboBox);
			this.locationFormatGroupBox.Controls.Add(this.mapLayoutComboBox);
			this.locationFormatGroupBox.Controls.Add(this.cleanLabel4);
			this.locationFormatGroupBox.Controls.Add(this.cleanLabel3);
			this.locationFormatGroupBox.Controls.Add(this.labelPlaneCount);
			this.locationFormatGroupBox.Controls.Add(this.labelMapLayout);
			this.locationFormatGroupBox.Controls.Add(this.resultingPlanesControl);
			this.locationFormatGroupBox.Controls.Add(this.propEditControl);
			this.locationFormatGroupBox.Controls.Add(this.addRowButton);
			this.locationFormatGroupBox.Controls.Add(this.removeRowButton);
			this.locationFormatGroupBox.Location = new System.Drawing.Point(8, 10);
			this.locationFormatGroupBox.Name = "locationFormatGroupBox";
			this.locationFormatGroupBox.Size = new System.Drawing.Size(433, 257);
			this.locationFormatGroupBox.TabIndex = 1;
			this.locationFormatGroupBox.Text = "Location format";
			// 
			// removeRowButton
			// 
			this.removeRowButton.Location = new System.Drawing.Point(141, 220);
			this.removeRowButton.Name = "removeRowButton";
			this.removeRowButton.Size = new System.Drawing.Size(60, 19);
			this.removeRowButton.TabIndex = 6;
			this.removeRowButton.Text = "&Delete";
			this.removeRowButton.UseVisualStyleBackColor = true;
			this.removeRowButton.Click += new System.EventHandler(this.removeRowButton_Click);
			// 
			// addRowButton
			// 
			this.addRowButton.Location = new System.Drawing.Point(78, 220);
			this.addRowButton.Name = "addRowButton";
			this.addRowButton.Size = new System.Drawing.Size(60, 19);
			this.addRowButton.TabIndex = 5;
			this.addRowButton.Text = "&Add";
			this.addRowButton.UseVisualStyleBackColor = true;
			this.addRowButton.Click += new System.EventHandler(this.addRowButton_Click);
			// 
			// labelMapLayout
			// 
			this.labelMapLayout.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelMapLayout.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat10.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat10.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat10.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat10.Trimming = System.Drawing.StringTrimming.Character;
			this.labelMapLayout.Format = stringFormat10;
			this.labelMapLayout.Location = new System.Drawing.Point(222, 24);
			this.labelMapLayout.Name = "labelMapLayout";
			this.labelMapLayout.Size = new System.Drawing.Size(60, 14);
			this.labelMapLayout.TabIndex = 8;
			this.labelMapLayout.TabStop = false;
			this.labelMapLayout.Text = "&Map layout";
			// 
			// labelPlaneCount
			// 
			this.labelPlaneCount.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelPlaneCount.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat9.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat9.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat9.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat9.Trimming = System.Drawing.StringTrimming.Character;
			this.labelPlaneCount.Format = stringFormat9;
			this.labelPlaneCount.Location = new System.Drawing.Point(222, 48);
			this.labelPlaneCount.Name = "labelPlaneCount";
			this.labelPlaneCount.Size = new System.Drawing.Size(64, 14);
			this.labelPlaneCount.TabIndex = 9;
			this.labelPlaneCount.TabStop = false;
			this.labelPlaneCount.Text = "&Plane count";
			// 
			// cleanLabel3
			// 
			this.cleanLabel3.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.cleanLabel3.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat8.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat8.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat8.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat8.Trimming = System.Drawing.StringTrimming.Character;
			this.cleanLabel3.Format = stringFormat8;
			this.cleanLabel3.Location = new System.Drawing.Point(222, 72);
			this.cleanLabel3.Name = "cleanLabel3";
			this.cleanLabel3.Size = new System.Drawing.Size(63, 14);
			this.cleanLabel3.TabIndex = 10;
			this.cleanLabel3.TabStop = false;
			this.cleanLabel3.Text = "Plane &order";
			// 
			// cleanLabel4
			// 
			this.cleanLabel4.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.cleanLabel4.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat7.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat7.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat7.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat7.Trimming = System.Drawing.StringTrimming.Character;
			this.cleanLabel4.Format = stringFormat7;
			this.cleanLabel4.Location = new System.Drawing.Point(222, 95);
			this.cleanLabel4.Name = "cleanLabel4";
			this.cleanLabel4.Size = new System.Drawing.Size(53, 14);
			this.cleanLabel4.TabIndex = 11;
			this.cleanLabel4.TabStop = false;
			this.cleanLabel4.Text = "&Tile offset";
			// 
			// mapLayoutComboBox
			// 
			this.mapLayoutComboBox.FormattingEnabled = true;
			this.mapLayoutComboBox.Items.AddRange(new object[] {
            "Rows",
            "Columns"});
			this.mapLayoutComboBox.Location = new System.Drawing.Point(296, 20);
			this.mapLayoutComboBox.Name = "mapLayoutComboBox";
			this.mapLayoutComboBox.Size = new System.Drawing.Size(129, 21);
			this.mapLayoutComboBox.TabIndex = 12;
			// 
			// planeCountComboBox
			// 
			this.planeCountComboBox.FormattingEnabled = true;
			this.planeCountComboBox.Items.AddRange(new object[] {
            "0.5 planes (4 bits)",
            "1 plane (8 bits)",
            "2 planes (16 bits)",
            "3 planes (24 bits)",
            "4 planes (32 bits)\t"});
			this.planeCountComboBox.Location = new System.Drawing.Point(296, 44);
			this.planeCountComboBox.Name = "planeCountComboBox";
			this.planeCountComboBox.Size = new System.Drawing.Size(129, 21);
			this.planeCountComboBox.TabIndex = 13;
			// 
			// planeOrderComboBox
			// 
			this.planeOrderComboBox.FormattingEnabled = true;
			this.planeOrderComboBox.Items.AddRange(new object[] {
            "Tiles are continues",
            "Planes are continues"});
			this.planeOrderComboBox.Location = new System.Drawing.Point(296, 68);
			this.planeOrderComboBox.Name = "planeOrderComboBox";
			this.planeOrderComboBox.Size = new System.Drawing.Size(129, 21);
			this.planeOrderComboBox.TabIndex = 14;
			// 
			// tileOffsetTextBox
			// 
			this.tileOffsetTextBox.Location = new System.Drawing.Point(296, 92);
			this.tileOffsetTextBox.Name = "tileOffsetTextBox";
			this.tileOffsetTextBox.Size = new System.Drawing.Size(33, 21);
			this.tileOffsetTextBox.TabIndex = 15;
			this.tileOffsetTextBox.Value = ((uint)(0u));
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(224, 318);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
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
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
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
			// propEditControl
			// 
			this.propEditControl.BackColor = System.Drawing.Color.White;
			this.propEditControl.Location = new System.Drawing.Point(16, 20);
			this.propEditControl.Name = "propEditControl";
			this.propEditControl.Size = new System.Drawing.Size(193, 193);
			this.propEditControl.TabIndex = 4;
			this.propEditControl.SizeOrCountChanged += new System.EventHandler(this.propEditControl_SizeOrCountChanged);
			// 
			// resultingPlanesControl
			// 
			this.resultingPlanesControl.Location = new System.Drawing.Point(225, 125);
			this.resultingPlanesControl.Name = "resultingPlanesControl";
			this.resultingPlanesControl.Size = new System.Drawing.Size(197, 116);
			this.resultingPlanesControl.TabIndex = 7;
			this.resultingPlanesControl.Text = "Resulting planes";
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
			this.splitDataGroupBox.ResumeLayout(false);
			this.splitDataGroupBox.PerformLayout();
			this.settingsGroupBox.ResumeLayout(false);
			this.settingsGroupBox.PerformLayout();
			this.fileGroupBox.ResumeLayout(false);
			this.fileGroupBox.PerformLayout();
			this.locationFormatTabPage.ResumeLayout(false);
			this.locationFormatGroupBox.ResumeLayout(false);
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
		private System.Windows.Forms.CheckBox changeBankForEachBlockCheckBox;
		private Shared.Controls.NumericTextBox blockSizeTextBox;
		private Shared.Controls.CleanLabel labelBlockSize;
		private System.Windows.Forms.CheckBox splitDataCheckBox;
		private Shared.Controls.GroupBox locationFormatGroupBox;
		private ExportPropertiesEditControl propEditControl;
		private System.Windows.Forms.Button removeRowButton;
		private System.Windows.Forms.Button addRowButton;
		private ResultingExportPlanesControl resultingPlanesControl;
		private Shared.Controls.NumericTextBox tileOffsetTextBox;
		private System.Windows.Forms.ComboBox planeOrderComboBox;
		private System.Windows.Forms.ComboBox planeCountComboBox;
		private System.Windows.Forms.ComboBox mapLayoutComboBox;
		private Shared.Controls.CleanLabel cleanLabel4;
		private Shared.Controls.CleanLabel cleanLabel3;
		private Shared.Controls.CleanLabel labelPlaneCount;
		private Shared.Controls.CleanLabel labelMapLayout;
	}
}