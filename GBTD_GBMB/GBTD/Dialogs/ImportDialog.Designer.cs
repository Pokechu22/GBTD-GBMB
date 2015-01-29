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
			this.fileGroupBox = new GB.Shared.Controls.GroupBox();
			this.settingsGroupBox = new GB.Shared.Controls.GroupBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// fileGroupBox
			// 
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
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.GroupBox fileGroupBox;
		private Shared.Controls.GroupBox settingsGroupBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
	}
}