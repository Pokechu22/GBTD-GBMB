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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.propColorsGroupBox = new GB.Shared.Controls.GroupBox();
			this.propertiesGroupBox = new GB.Shared.Controls.GroupBox();
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private Shared.Controls.GroupBox propertiesGroupBox;
		private Shared.Controls.GroupBox propColorsGroupBox;
	}
}