namespace GB.GBTD.Dialogs
{
	partial class SplitOptionsDialog
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
			System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			this.dimensionsGroupBox = new GB.Shared.Controls.GroupBox();
			this.pasteButton = new System.Windows.Forms.Button();
			this.copyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.labelTileWidth = new GB.Shared.Controls.CleanLabel();
			this.labelTileHeight = new GB.Shared.Controls.CleanLabel();
			this.labelOrder = new GB.Shared.Controls.CleanLabel();
			this.widthTextBox = new GB.Shared.Controls.NumericTextBox();
			this.heightTextBox = new GB.Shared.Controls.NumericTextBox();
			this.orderDropDown = new System.Windows.Forms.ComboBox();
			this.dimensionsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// dimensionsGroupBox
			// 
			this.dimensionsGroupBox.Controls.Add(this.orderDropDown);
			this.dimensionsGroupBox.Controls.Add(this.heightTextBox);
			this.dimensionsGroupBox.Controls.Add(this.widthTextBox);
			this.dimensionsGroupBox.Controls.Add(this.labelOrder);
			this.dimensionsGroupBox.Controls.Add(this.labelTileHeight);
			this.dimensionsGroupBox.Controls.Add(this.labelTileWidth);
			this.dimensionsGroupBox.Location = new System.Drawing.Point(8, 8);
			this.dimensionsGroupBox.Name = "dimensionsGroupBox";
			this.dimensionsGroupBox.Size = new System.Drawing.Size(329, 96);
			this.dimensionsGroupBox.TabIndex = 0;
			this.dimensionsGroupBox.Text = "Dimensions";
			// 
			// pasteButton
			// 
			this.pasteButton.Location = new System.Drawing.Point(15, 113);
			this.pasteButton.Name = "pasteButton";
			this.pasteButton.Size = new System.Drawing.Size(75, 25);
			this.pasteButton.TabIndex = 1;
			this.pasteButton.Text = "Paste";
			this.pasteButton.UseVisualStyleBackColor = true;
			// 
			// copyButton
			// 
			this.copyButton.Location = new System.Drawing.Point(95, 113);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(75, 25);
			this.copyButton.TabIndex = 2;
			this.copyButton.Text = "Copy";
			this.copyButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(175, 113);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(255, 113);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 4;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// labelTileWidth
			// 
			this.labelTileWidth.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelTileWidth.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat3.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat3.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
			this.labelTileWidth.Format = stringFormat3;
			this.labelTileWidth.Location = new System.Drawing.Point(14, 19);
			this.labelTileWidth.Name = "labelTileWidth";
			this.labelTileWidth.Size = new System.Drawing.Size(52, 14);
			this.labelTileWidth.TabIndex = 4;
			this.labelTileWidth.TabStop = false;
			this.labelTileWidth.Text = "Tile &width";
			// 
			// labelTileHeight
			// 
			this.labelTileHeight.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelTileHeight.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat2.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat2.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
			this.labelTileHeight.Format = stringFormat2;
			this.labelTileHeight.Location = new System.Drawing.Point(14, 43);
			this.labelTileHeight.Name = "labelTileHeight";
			this.labelTileHeight.Size = new System.Drawing.Size(56, 14);
			this.labelTileHeight.TabIndex = 5;
			this.labelTileHeight.TabStop = false;
			this.labelTileHeight.Text = "Tile &height";
			// 
			// labelOrder
			// 
			this.labelOrder.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.labelOrder.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.labelOrder.Format = stringFormat1;
			this.labelOrder.Location = new System.Drawing.Point(14, 67);
			this.labelOrder.Name = "labelOrder";
			this.labelOrder.Size = new System.Drawing.Size(33, 14);
			this.labelOrder.TabIndex = 6;
			this.labelOrder.TabStop = false;
			this.labelOrder.Text = "&Order";
			// 
			// widthTextBox
			// 
			this.widthTextBox.Location = new System.Drawing.Point(75, 16);
			this.widthTextBox.Name = "widthTextBox";
			this.widthTextBox.Size = new System.Drawing.Size(33, 21);
			this.widthTextBox.TabIndex = 7;
			this.widthTextBox.Value = ((uint)(0u));
			// 
			// heightTextBox
			// 
			this.heightTextBox.Location = new System.Drawing.Point(75, 40);
			this.heightTextBox.Name = "heightTextBox";
			this.heightTextBox.Size = new System.Drawing.Size(33, 21);
			this.heightTextBox.TabIndex = 8;
			this.heightTextBox.Value = ((uint)(0u));
			// 
			// orderDropDown
			// 
			this.orderDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.orderDropDown.FormattingEnabled = true;
			this.orderDropDown.Items.AddRange(new object[] {
            "Left to right, top to bottom",
            "Top to bottom, left to right"});
			this.orderDropDown.Location = new System.Drawing.Point(75, 64);
			this.orderDropDown.Name = "orderDropDown";
			this.orderDropDown.Size = new System.Drawing.Size(174, 21);
			this.orderDropDown.TabIndex = 9;
			// 
			// SplitOptionsDialog
			// 
			this.AcceptButton = this.pasteButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(345, 144);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.copyButton);
			this.Controls.Add(this.pasteButton);
			this.Controls.Add(this.dimensionsGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplitOptionsDialog";
			this.ShowIcon = false;
			this.Text = "Split options";
			this.dimensionsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.GroupBox dimensionsGroupBox;
		private System.Windows.Forms.Button pasteButton;
		private System.Windows.Forms.Button copyButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.ComboBox orderDropDown;
		private Shared.Controls.NumericTextBox heightTextBox;
		private Shared.Controls.NumericTextBox widthTextBox;
		private Shared.Controls.CleanLabel labelOrder;
		private Shared.Controls.CleanLabel labelTileHeight;
		private Shared.Controls.CleanLabel labelTileWidth;
	}
}