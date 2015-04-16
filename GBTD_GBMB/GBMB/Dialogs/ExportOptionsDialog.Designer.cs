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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.standardTabPage = new System.Windows.Forms.TabPage();
			this.locationFormatTabPage = new System.Windows.Forms.TabPage();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage locationFormatTabPage;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.TabPage standardTabPage;
	}
}