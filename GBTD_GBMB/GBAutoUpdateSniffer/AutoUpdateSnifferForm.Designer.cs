﻿namespace GBAutoUpdateSniffer
{
	partial class AutoUpdateSnifferForm
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
			this.auListener = new GB.Shared.AutoUpdate.AUListener();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageFileData = new System.Windows.Forms.TabPage();
			this.tabPageMessages = new System.Windows.Forms.TabPage();
			this.openButton = new System.Windows.Forms.Button();
			this.groupBoxFileName = new System.Windows.Forms.GroupBox();
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.groupBoxMessageHex = new System.Windows.Forms.GroupBox();
			this.labelMessageHex = new System.Windows.Forms.Label();
			this.tabPageMemoryMappedFile = new System.Windows.Forms.TabPage();
			this.tabControl.SuspendLayout();
			this.tabPageFileData.SuspendLayout();
			this.groupBoxFileName.SuspendLayout();
			this.groupBoxMessageHex.SuspendLayout();
			this.SuspendLayout();
			// 
			// auListener
			// 
			this.auListener.FileName = null;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageFileData);
			this.tabControl.Controls.Add(this.tabPageMemoryMappedFile);
			this.tabControl.Controls.Add(this.tabPageMessages);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(292, 273);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageFileData
			// 
			this.tabPageFileData.Controls.Add(this.groupBoxMessageHex);
			this.tabPageFileData.Controls.Add(this.groupBoxFileName);
			this.tabPageFileData.Controls.Add(this.openButton);
			this.tabPageFileData.Location = new System.Drawing.Point(4, 22);
			this.tabPageFileData.Name = "tabPageFileData";
			this.tabPageFileData.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFileData.Size = new System.Drawing.Size(284, 247);
			this.tabPageFileData.TabIndex = 0;
			this.tabPageFileData.Text = "File data";
			this.tabPageFileData.UseVisualStyleBackColor = true;
			// 
			// tabPageMessages
			// 
			this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
			this.tabPageMessages.Name = "tabPageMessages";
			this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMessages.Size = new System.Drawing.Size(284, 247);
			this.tabPageMessages.TabIndex = 1;
			this.tabPageMessages.Text = "Messages";
			this.tabPageMessages.UseVisualStyleBackColor = true;
			// 
			// openButton
			// 
			this.openButton.Location = new System.Drawing.Point(201, 216);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(75, 23);
			this.openButton.TabIndex = 0;
			this.openButton.Text = "Open";
			this.openButton.UseVisualStyleBackColor = true;
			this.openButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// groupBoxFileName
			// 
			this.groupBoxFileName.Controls.Add(this.fileNameLabel);
			this.groupBoxFileName.Location = new System.Drawing.Point(8, 6);
			this.groupBoxFileName.Name = "groupBoxFileName";
			this.groupBoxFileName.Size = new System.Drawing.Size(268, 39);
			this.groupBoxFileName.TabIndex = 1;
			this.groupBoxFileName.TabStop = false;
			this.groupBoxFileName.Text = "File name";
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.AutoSize = true;
			this.fileNameLabel.Location = new System.Drawing.Point(6, 16);
			this.fileNameLabel.Name = "fileNameLabel";
			this.fileNameLabel.Size = new System.Drawing.Size(108, 13);
			this.fileNameLabel.TabIndex = 0;
			this.fileNameLabel.Text = "[File name goes here]";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "GBR files|*.gbr|All files|*.*";
			// 
			// groupBoxMessageHex
			// 
			this.groupBoxMessageHex.Controls.Add(this.labelMessageHex);
			this.groupBoxMessageHex.Location = new System.Drawing.Point(8, 51);
			this.groupBoxMessageHex.Name = "groupBoxMessageHex";
			this.groupBoxMessageHex.Size = new System.Drawing.Size(268, 39);
			this.groupBoxMessageHex.TabIndex = 2;
			this.groupBoxMessageHex.TabStop = false;
			this.groupBoxMessageHex.Text = "Message Hex ID";
			// 
			// labelMessageHex
			// 
			this.labelMessageHex.AutoSize = true;
			this.labelMessageHex.Location = new System.Drawing.Point(6, 16);
			this.labelMessageHex.Name = "labelMessageHex";
			this.labelMessageHex.Size = new System.Drawing.Size(126, 13);
			this.labelMessageHex.TabIndex = 3;
			this.labelMessageHex.Text = "[Message hex goes here]";
			// 
			// tabPageMemoryMappedFile
			// 
			this.tabPageMemoryMappedFile.Location = new System.Drawing.Point(4, 22);
			this.tabPageMemoryMappedFile.Name = "tabPageMemoryMappedFile";
			this.tabPageMemoryMappedFile.Size = new System.Drawing.Size(284, 247);
			this.tabPageMemoryMappedFile.TabIndex = 2;
			this.tabPageMemoryMappedFile.Text = "Memory Mapped File";
			this.tabPageMemoryMappedFile.UseVisualStyleBackColor = true;
			// 
			// AutoUpdateSnifferForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.tabControl);
			this.Name = "AutoUpdateSnifferForm";
			this.Text = "Form1";
			this.tabControl.ResumeLayout(false);
			this.tabPageFileData.ResumeLayout(false);
			this.groupBoxFileName.ResumeLayout(false);
			this.groupBoxFileName.PerformLayout();
			this.groupBoxMessageHex.ResumeLayout(false);
			this.groupBoxMessageHex.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private GB.Shared.AutoUpdate.AUListener auListener;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageFileData;
		private System.Windows.Forms.TabPage tabPageMessages;
		private System.Windows.Forms.Button openButton;
		private System.Windows.Forms.GroupBox groupBoxFileName;
		private System.Windows.Forms.Label fileNameLabel;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.GroupBox groupBoxMessageHex;
		private System.Windows.Forms.Label labelMessageHex;
		private System.Windows.Forms.TabPage tabPageMemoryMappedFile;
	}
}

