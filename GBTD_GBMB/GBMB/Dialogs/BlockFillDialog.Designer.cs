namespace GB.GBMB.Dialogs
{
	partial class BlockFillDialog
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
			GB.Shared.Controls.Border tileListBorder;
			System.Drawing.StringFormat stringFormat5 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat4 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockFillDialog));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.groupBoxSettings = new GB.Shared.Controls.GroupBox();
			this.heightTextBox = new GB.Shared.Controls.NumericTextBox();
			this.topTextBox = new GB.Shared.Controls.NumericTextBox();
			this.widthTextBox = new GB.Shared.Controls.NumericTextBox();
			this.leftTextBox = new GB.Shared.Controls.NumericTextBox();
			this.paternComboBox = new System.Windows.Forms.ComboBox();
			this.heightLabel = new GB.Shared.Controls.CleanLabel();
			this.topLabel = new GB.Shared.Controls.CleanLabel();
			this.widthLabel = new GB.Shared.Controls.CleanLabel();
			this.leftLabel = new GB.Shared.Controls.CleanLabel();
			this.paternLabel = new GB.Shared.Controls.CleanLabel();
			this.tileList = new GB.GBMB.TileList();
			tileListBorder = new GB.Shared.Controls.Border();
			this.groupBoxSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// tileListBorder
			// 
			tileListBorder.BottomBorder = null;
			tileListBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			tileListBorder.LeftBorder = null;
			tileListBorder.Location = new System.Drawing.Point(61, 13);
			tileListBorder.Name = "tileListBorder";
			tileListBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			tileListBorder.Size = new System.Drawing.Size(1, 137);
			tileListBorder.TabIndex = 5;
			tileListBorder.TopBorder = null;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(78, 159);
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
			this.cancelButton.Location = new System.Drawing.Point(157, 159);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(236, 159);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 2;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// groupBoxSettings
			// 
			this.groupBoxSettings.Controls.Add(this.paternLabel);
			this.groupBoxSettings.Controls.Add(this.leftLabel);
			this.groupBoxSettings.Controls.Add(this.widthLabel);
			this.groupBoxSettings.Controls.Add(this.topLabel);
			this.groupBoxSettings.Controls.Add(this.heightLabel);
			this.groupBoxSettings.Controls.Add(this.paternComboBox);
			this.groupBoxSettings.Controls.Add(this.leftTextBox);
			this.groupBoxSettings.Controls.Add(this.widthTextBox);
			this.groupBoxSettings.Controls.Add(this.topTextBox);
			this.groupBoxSettings.Controls.Add(this.heightTextBox);
			this.groupBoxSettings.Location = new System.Drawing.Point(70, 8);
			this.groupBoxSettings.Name = "groupBoxSettings";
			this.groupBoxSettings.Size = new System.Drawing.Size(241, 142);
			this.groupBoxSettings.TabIndex = 4;
			this.groupBoxSettings.Text = "Settings";
			// 
			// heightTextBox
			// 
			this.heightTextBox.Location = new System.Drawing.Point(184, 64);
			this.heightTextBox.Name = "heightTextBox";
			this.heightTextBox.Size = new System.Drawing.Size(41, 21);
			this.heightTextBox.TabIndex = 13;
			this.heightTextBox.Value = ((uint)(0u));
			// 
			// topTextBox
			// 
			this.topTextBox.Location = new System.Drawing.Point(184, 40);
			this.topTextBox.Name = "topTextBox";
			this.topTextBox.Size = new System.Drawing.Size(41, 21);
			this.topTextBox.TabIndex = 12;
			this.topTextBox.Value = ((uint)(0u));
			// 
			// widthTextBox
			// 
			this.widthTextBox.Location = new System.Drawing.Point(64, 64);
			this.widthTextBox.Name = "widthTextBox";
			this.widthTextBox.Size = new System.Drawing.Size(41, 21);
			this.widthTextBox.TabIndex = 11;
			this.widthTextBox.Value = ((uint)(0u));
			// 
			// leftTextBox
			// 
			this.leftTextBox.Location = new System.Drawing.Point(64, 40);
			this.leftTextBox.Name = "leftTextBox";
			this.leftTextBox.Size = new System.Drawing.Size(41, 21);
			this.leftTextBox.TabIndex = 10;
			this.leftTextBox.Value = ((uint)(0u));
			// 
			// paternComboBox
			// 
			this.paternComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.paternComboBox.FormattingEnabled = true;
			this.paternComboBox.Items.AddRange(new object[] {
            "Selected tile",
            "Left to right",
            "Left to right, top to bottom",
            "Top to bottom",
            "Top to bottom, left to right",
            "Right to left",
            "Right to left, top to bottom",
            "Bottom to top",
            "Bottom to top, right to left"});
			this.paternComboBox.Location = new System.Drawing.Point(64, 16);
			this.paternComboBox.Name = "paternComboBox";
			this.paternComboBox.Size = new System.Drawing.Size(161, 21);
			this.paternComboBox.TabIndex = 9;
			// 
			// heightLabel
			// 
			this.heightLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.heightLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat5.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat5.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat5.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat5.Trimming = System.Drawing.StringTrimming.Character;
			this.heightLabel.Format = stringFormat5;
			this.heightLabel.Location = new System.Drawing.Point(134, 67);
			this.heightLabel.Name = "heightLabel";
			this.heightLabel.Size = new System.Drawing.Size(37, 14);
			this.heightLabel.TabIndex = 8;
			this.heightLabel.TabStop = false;
			this.heightLabel.Text = "&Height";
			// 
			// topLabel
			// 
			this.topLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.topLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat4.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat4.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat4.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat4.Trimming = System.Drawing.StringTrimming.Character;
			this.topLabel.Format = stringFormat4;
			this.topLabel.Location = new System.Drawing.Point(134, 43);
			this.topLabel.Name = "topLabel";
			this.topLabel.Size = new System.Drawing.Size(24, 14);
			this.topLabel.TabIndex = 7;
			this.topLabel.TabStop = false;
			this.topLabel.Text = "&Top";
			// 
			// widthLabel
			// 
			this.widthLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.widthLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.widthLabel.Format = stringFormat3;
			this.widthLabel.Location = new System.Drawing.Point(14, 67);
			this.widthLabel.Name = "widthLabel";
			this.widthLabel.Size = new System.Drawing.Size(33, 14);
			this.widthLabel.TabIndex = 6;
			this.widthLabel.TabStop = false;
			this.widthLabel.Text = "&Width";
			// 
			// leftLabel
			// 
			this.leftLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.leftLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.leftLabel.Format = stringFormat2;
			this.leftLabel.Location = new System.Drawing.Point(14, 43);
			this.leftLabel.Name = "leftLabel";
			this.leftLabel.Size = new System.Drawing.Size(23, 14);
			this.leftLabel.TabIndex = 5;
			this.leftLabel.TabStop = false;
			this.leftLabel.Text = "&Left";
			// 
			// paternLabel
			// 
			this.paternLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.paternLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.paternLabel.Format = stringFormat1;
			this.paternLabel.Location = new System.Drawing.Point(14, 20);
			this.paternLabel.Name = "paternLabel";
			this.paternLabel.Size = new System.Drawing.Size(41, 14);
			this.paternLabel.TabIndex = 4;
			this.paternLabel.TabStop = false;
			this.paternLabel.Text = "&Pattern";
			// 
			// tileList
			// 
			this.tileList.Bookmark1Icon = ((System.Drawing.Image)(resources.GetObject("tileList.Bookmark1Icon")));
			this.tileList.Bookmark2Icon = ((System.Drawing.Image)(resources.GetObject("tileList.Bookmark2Icon")));
			this.tileList.Bookmark3Icon = ((System.Drawing.Image)(resources.GetObject("tileList.Bookmark3Icon")));
			this.tileList.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_POCKET;
			this.tileList.Location = new System.Drawing.Point(5, 13);
			this.tileList.Name = "tileList";
			this.tileList.PaletteData = null;
			this.tileList.PaletteMapping = null;
			this.tileList.SelectedTile = ((ushort)(0));
			this.tileList.Size = new System.Drawing.Size(56, 137);
			this.tileList.TabIndex = 3;
			this.tileList.TileSet = null;
			// 
			// BlockFillDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(317, 192);
			this.Controls.Add(tileListBorder);
			this.Controls.Add(this.groupBoxSettings);
			this.Controls.Add(this.tileList);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BlockFillDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Block fill";
			this.groupBoxSettings.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private TileList tileList;
		private Shared.Controls.GroupBox groupBoxSettings;
		private Shared.Controls.NumericTextBox heightTextBox;
		private Shared.Controls.NumericTextBox topTextBox;
		private Shared.Controls.NumericTextBox widthTextBox;
		private System.Windows.Forms.ComboBox paternComboBox;
		private Shared.Controls.CleanLabel heightLabel;
		private Shared.Controls.CleanLabel topLabel;
		private Shared.Controls.CleanLabel widthLabel;
		private Shared.Controls.CleanLabel leftLabel;
		private Shared.Controls.CleanLabel paternLabel;
		private Shared.Controls.NumericTextBox leftTextBox;
	}
}