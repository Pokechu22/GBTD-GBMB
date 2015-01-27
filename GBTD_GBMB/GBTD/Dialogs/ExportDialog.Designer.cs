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
			this.okButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageStandard = new System.Windows.Forms.TabPage();
			this.tabPageAdvanced = new System.Windows.Forms.TabPage();
			this.groupBoxSettings = new GB.Shared.Controls.GroupBox();
			this.groupBoxFile = new GB.Shared.Controls.GroupBox();
			this.groupBoxSplitData = new GB.Shared.Controls.GroupBox();
			this.groupBoxMetatiles = new GB.Shared.Controls.GroupBox();
			this.groupBoxColors = new GB.Shared.Controls.GroupBox();
			this.tabControl.SuspendLayout();
			this.tabPageStandard.SuspendLayout();
			this.tabPageAdvanced.SuspendLayout();
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
			this.groupBoxSettings.Location = new System.Drawing.Point(8, 90);
			this.groupBoxSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.groupBoxSettings.Name = "groupBoxSettings";
			this.groupBoxSettings.Size = new System.Drawing.Size(433, 201);
			this.groupBoxSettings.TabIndex = 1;
			this.groupBoxSettings.Text = "Settings";
			// 
			// groupBoxFile
			// 
			this.groupBoxFile.Location = new System.Drawing.Point(8, 10);
			this.groupBoxFile.Name = "groupBoxFile";
			this.groupBoxFile.Size = new System.Drawing.Size(433, 73);
			this.groupBoxFile.TabIndex = 0;
			this.groupBoxFile.Text = "File";
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
	}
}