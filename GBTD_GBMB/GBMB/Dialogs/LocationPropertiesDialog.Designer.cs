namespace GB.GBMB.Dialogs
{
	partial class LocationPropertiesDialog
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
			System.Drawing.StringFormat stringFormat8 = new System.Drawing.StringFormat();
			System.Drawing.StringFormat stringFormat7 = new System.Drawing.StringFormat();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.propColorsGroupBox = new GB.Shared.Controls.GroupBox();
			this.propertiesGroupBox = new GB.Shared.Controls.GroupBox();
			this.redLabel = new GB.Shared.Controls.CleanLabel();
			this.greenLabel = new GB.Shared.Controls.CleanLabel();
			this.redPropertyComboBox = new System.Windows.Forms.ComboBox();
			this.greenPropertyComboBox = new System.Windows.Forms.ComboBox();
			this.redOperatorComboBox = new System.Windows.Forms.ComboBox();
			this.greenOperatorComboBox = new System.Windows.Forms.ComboBox();
			this.redOperandTextBox = new GB.Shared.Controls.NumericTextBox();
			this.greenOperandTextBox = new GB.Shared.Controls.NumericTextBox();
			this.propColorsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(48, 324);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(127, 324);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(206, 324);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 2;
			this.helpButton.Text = "Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// propColorsGroupBox
			// 
			this.propColorsGroupBox.Controls.Add(this.greenOperandTextBox);
			this.propColorsGroupBox.Controls.Add(this.redOperandTextBox);
			this.propColorsGroupBox.Controls.Add(this.greenOperatorComboBox);
			this.propColorsGroupBox.Controls.Add(this.redOperatorComboBox);
			this.propColorsGroupBox.Controls.Add(this.greenPropertyComboBox);
			this.propColorsGroupBox.Controls.Add(this.redPropertyComboBox);
			this.propColorsGroupBox.Controls.Add(this.greenLabel);
			this.propColorsGroupBox.Controls.Add(this.redLabel);
			this.propColorsGroupBox.Location = new System.Drawing.Point(8, 239);
			this.propColorsGroupBox.Name = "propColorsGroupBox";
			this.propColorsGroupBox.Size = new System.Drawing.Size(273, 77);
			this.propColorsGroupBox.TabIndex = 4;
			this.propColorsGroupBox.Text = "Property colors";
			// 
			// propertiesGroupBox
			// 
			this.propertiesGroupBox.Location = new System.Drawing.Point(8, 8);
			this.propertiesGroupBox.Name = "propertiesGroupBox";
			this.propertiesGroupBox.Size = new System.Drawing.Size(273, 226);
			this.propertiesGroupBox.TabIndex = 3;
			this.propertiesGroupBox.Text = "Properties";
			// 
			// redLabel
			// 
			this.redLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.redLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat8.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat8.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat8.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat8.Trimming = System.Drawing.StringTrimming.Character;
			this.redLabel.Format = stringFormat8;
			this.redLabel.Location = new System.Drawing.Point(14, 23);
			this.redLabel.Name = "redLabel";
			this.redLabel.Size = new System.Drawing.Size(25, 14);
			this.redLabel.TabIndex = 4;
			this.redLabel.TabStop = false;
			this.redLabel.Text = "&Red";
			// 
			// greenLabel
			// 
			this.greenLabel.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.greenLabel.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			stringFormat7.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat7.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
			stringFormat7.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat7.Trimming = System.Drawing.StringTrimming.Character;
			this.greenLabel.Format = stringFormat7;
			this.greenLabel.Location = new System.Drawing.Point(14, 47);
			this.greenLabel.Name = "greenLabel";
			this.greenLabel.Size = new System.Drawing.Size(36, 14);
			this.greenLabel.TabIndex = 5;
			this.greenLabel.TabStop = false;
			this.greenLabel.Text = "&Green";
			// 
			// redPropertyComboBox
			// 
			this.redPropertyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.redPropertyComboBox.FormattingEnabled = true;
			this.redPropertyComboBox.Location = new System.Drawing.Point(64, 20);
			this.redPropertyComboBox.Name = "redPropertyComboBox";
			this.redPropertyComboBox.Size = new System.Drawing.Size(105, 21);
			this.redPropertyComboBox.TabIndex = 6;
			// 
			// greenPropertyComboBox
			// 
			this.greenPropertyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.greenPropertyComboBox.FormattingEnabled = true;
			this.greenPropertyComboBox.Location = new System.Drawing.Point(64, 44);
			this.greenPropertyComboBox.Name = "greenPropertyComboBox";
			this.greenPropertyComboBox.Size = new System.Drawing.Size(105, 21);
			this.greenPropertyComboBox.TabIndex = 7;
			// 
			// redOperatorComboBox
			// 
			this.redOperatorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.redOperatorComboBox.FormattingEnabled = true;
			this.redOperatorComboBox.Items.AddRange(new object[] {
            "=",
            "<>",
            "<",
            ">",
            "<=",
            ">="});
			this.redOperatorComboBox.Location = new System.Drawing.Point(176, 20);
			this.redOperatorComboBox.Name = "redOperatorComboBox";
			this.redOperatorComboBox.Size = new System.Drawing.Size(38, 21);
			this.redOperatorComboBox.TabIndex = 8;
			// 
			// greenOperatorComboBox
			// 
			this.greenOperatorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.greenOperatorComboBox.FormattingEnabled = true;
			this.greenOperatorComboBox.Items.AddRange(new object[] {
            "=",
            "<>",
            "<",
            ">",
            "<=",
            ">="});
			this.greenOperatorComboBox.Location = new System.Drawing.Point(176, 44);
			this.greenOperatorComboBox.Name = "greenOperatorComboBox";
			this.greenOperatorComboBox.Size = new System.Drawing.Size(38, 21);
			this.greenOperatorComboBox.TabIndex = 9;
			// 
			// redOperandTextBox
			// 
			this.redOperandTextBox.Location = new System.Drawing.Point(221, 20);
			this.redOperandTextBox.Name = "redOperandTextBox";
			this.redOperandTextBox.Size = new System.Drawing.Size(38, 21);
			this.redOperandTextBox.TabIndex = 10;
			this.redOperandTextBox.Value = ((uint)(0u));
			// 
			// greenOperandTextBox
			// 
			this.greenOperandTextBox.Location = new System.Drawing.Point(221, 44);
			this.greenOperandTextBox.Name = "greenOperandTextBox";
			this.greenOperandTextBox.Size = new System.Drawing.Size(38, 21);
			this.greenOperandTextBox.TabIndex = 11;
			this.greenOperandTextBox.Value = ((uint)(0u));
			// 
			// LocationPropertiesDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(289, 354);
			this.Controls.Add(this.propColorsGroupBox);
			this.Controls.Add(this.propertiesGroupBox);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LocationPropertiesDialog";
			this.Text = "Location properties";
			this.propColorsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private Shared.Controls.GroupBox propertiesGroupBox;
		private Shared.Controls.GroupBox propColorsGroupBox;
		private Shared.Controls.NumericTextBox greenOperandTextBox;
		private Shared.Controls.NumericTextBox redOperandTextBox;
		private System.Windows.Forms.ComboBox greenOperatorComboBox;
		private System.Windows.Forms.ComboBox redOperatorComboBox;
		private System.Windows.Forms.ComboBox greenPropertyComboBox;
		private System.Windows.Forms.ComboBox redPropertyComboBox;
		private Shared.Controls.CleanLabel greenLabel;
		private Shared.Controls.CleanLabel redLabel;
	}
}