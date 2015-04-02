namespace GB.GBMB.Dialogs
{
	partial class MapPropertiesDialog
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
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.groupBoxSize = new GB.Shared.Controls.GroupBox();
			this.groupBoxTileSet = new GB.Shared.Controls.GroupBox();
			this.labelWidth = new GB.Shared.Controls.CleanLabel();
			this.labelHeight = new GB.Shared.Controls.CleanLabel();
			this.widthTextBox = new GB.Shared.Controls.NumericTextBox();
			this.heightTextBox = new GB.Shared.Controls.NumericTextBox();
			this.labelFileName = new GB.Shared.Controls.CleanLabel();
			this.fileNameTextBox = new System.Windows.Forms.TextBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.groupBoxSize.SuspendLayout();
			this.groupBoxTileSet.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(104, 122);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(183, 122);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(262, 122);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 2;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// groupBoxSize
			// 
			this.groupBoxSize.Controls.Add(this.heightTextBox);
			this.groupBoxSize.Controls.Add(this.widthTextBox);
			this.groupBoxSize.Controls.Add(this.labelHeight);
			this.groupBoxSize.Controls.Add(this.labelWidth);
			this.groupBoxSize.Location = new System.Drawing.Point(8, 8);
			this.groupBoxSize.Name = "groupBoxSize";
			this.groupBoxSize.Size = new System.Drawing.Size(329, 49);
			this.groupBoxSize.TabIndex = 3;
			this.groupBoxSize.Text = "Size";
			// 
			// groupBoxTileSet
			// 
			this.groupBoxTileSet.Controls.Add(this.browseButton);
			this.groupBoxTileSet.Controls.Add(this.fileNameTextBox);
			this.groupBoxTileSet.Controls.Add(this.labelFileName);
			this.groupBoxTileSet.Location = new System.Drawing.Point(8, 64);
			this.groupBoxTileSet.Name = "groupBoxTileSet";
			this.groupBoxTileSet.Size = new System.Drawing.Size(329, 49);
			this.groupBoxTileSet.TabIndex = 4;
			this.groupBoxTileSet.Text = "Tileset";
			// 
			// labelWidth
			// 
			this.labelWidth.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelWidth.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.labelWidth.Format = stringFormat2;
			this.labelWidth.Location = new System.Drawing.Point(14, 19);
			this.labelWidth.Name = "labelWidth";
			this.labelWidth.Size = new System.Drawing.Size(33, 14);
			this.labelWidth.TabIndex = 4;
			this.labelWidth.TabStop = false;
			this.labelWidth.Text = "&Width";
			// 
			// labelHeight
			// 
			this.labelHeight.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelHeight.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.labelHeight.Format = stringFormat1;
			this.labelHeight.Location = new System.Drawing.Point(150, 19);
			this.labelHeight.Name = "labelHeight";
			this.labelHeight.Size = new System.Drawing.Size(37, 14);
			this.labelHeight.TabIndex = 5;
			this.labelHeight.TabStop = false;
			this.labelHeight.Text = "&Height";
			// 
			// widthTextBox
			// 
			this.widthTextBox.Location = new System.Drawing.Point(64, 16);
			this.widthTextBox.Name = "widthTextBox";
			this.widthTextBox.Size = new System.Drawing.Size(41, 21);
			this.widthTextBox.TabIndex = 6;
			// 
			// heightTextBox
			// 
			this.heightTextBox.Location = new System.Drawing.Point(200, 16);
			this.heightTextBox.Name = "heightTextBox";
			this.heightTextBox.Size = new System.Drawing.Size(41, 21);
			this.heightTextBox.TabIndex = 7;
			// 
			// labelFileName
			// 
			this.labelFileName.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelFileName.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.labelFileName.Format = stringFormat3;
			this.labelFileName.Location = new System.Drawing.Point(14, 19);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(51, 14);
			this.labelFileName.TabIndex = 4;
			this.labelFileName.TabStop = false;
			this.labelFileName.Text = "&Filename";
			// 
			// fileNameTextBox
			// 
			this.fileNameTextBox.Location = new System.Drawing.Point(72, 16);
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.Size = new System.Drawing.Size(169, 20);
			this.fileNameTextBox.TabIndex = 5;
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(248, 16);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(65, 21);
			this.browseButton.TabIndex = 6;
			this.browseButton.Text = "&Browse...";
			this.browseButton.UseVisualStyleBackColor = true;
			// 
			// MapPropertiesDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(343, 154);
			this.Controls.Add(this.groupBoxTileSet);
			this.Controls.Add(this.groupBoxSize);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MapPropertiesDialog";
			this.ShowIcon = false;
			this.Text = "Map properties";
			this.groupBoxSize.ResumeLayout(false);
			this.groupBoxTileSet.ResumeLayout(false);
			this.groupBoxTileSet.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private Shared.Controls.GroupBox groupBoxSize;
		private Shared.Controls.GroupBox groupBoxTileSet;
		private Shared.Controls.NumericTextBox heightTextBox;
		private Shared.Controls.NumericTextBox widthTextBox;
		private Shared.Controls.CleanLabel labelHeight;
		private Shared.Controls.CleanLabel labelWidth;
		private Shared.Controls.CleanLabel labelFileName;
		private System.Windows.Forms.TextBox fileNameTextBox;
		private System.Windows.Forms.Button browseButton;
	}
}