namespace GBAutoUpdateSniffer
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageFileData = new System.Windows.Forms.TabPage();
			this.groupBoxMessageHex = new System.Windows.Forms.GroupBox();
			this.labelMessageHex = new System.Windows.Forms.Label();
			this.groupBoxFileName = new System.Windows.Forms.GroupBox();
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.openButton = new System.Windows.Forms.Button();
			this.tabPageMemoryMappedFile = new System.Windows.Forms.TabPage();
			this.tabPageMessages = new System.Windows.Forms.TabPage();
			this.messageSplitContainer = new System.Windows.Forms.SplitContainer();
			this.listBoxMessages = new System.Windows.Forms.ListBox();
			this.groupBoxMessageInfo = new System.Windows.Forms.GroupBox();
			this.textBoxMessageInfo = new System.Windows.Forms.TextBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.auListener = new GB.Shared.AutoUpdate.AUListener();
			this.groupBoxMessageName = new System.Windows.Forms.GroupBox();
			this.labelMessageName = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tabPageFileData.SuspendLayout();
			this.groupBoxMessageHex.SuspendLayout();
			this.groupBoxFileName.SuspendLayout();
			this.tabPageMessages.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.messageSplitContainer)).BeginInit();
			this.messageSplitContainer.Panel1.SuspendLayout();
			this.messageSplitContainer.Panel2.SuspendLayout();
			this.messageSplitContainer.SuspendLayout();
			this.groupBoxMessageInfo.SuspendLayout();
			this.groupBoxMessageName.SuspendLayout();
			this.SuspendLayout();
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
			this.tabPageFileData.Controls.Add(this.groupBoxMessageName);
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
			// groupBoxMessageHex
			// 
			this.groupBoxMessageHex.Controls.Add(this.labelMessageHex);
			this.groupBoxMessageHex.Location = new System.Drawing.Point(8, 96);
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
			// tabPageMemoryMappedFile
			// 
			this.tabPageMemoryMappedFile.Location = new System.Drawing.Point(4, 22);
			this.tabPageMemoryMappedFile.Name = "tabPageMemoryMappedFile";
			this.tabPageMemoryMappedFile.Size = new System.Drawing.Size(284, 247);
			this.tabPageMemoryMappedFile.TabIndex = 2;
			this.tabPageMemoryMappedFile.Text = "Memory Mapped File";
			this.tabPageMemoryMappedFile.UseVisualStyleBackColor = true;
			// 
			// tabPageMessages
			// 
			this.tabPageMessages.Controls.Add(this.messageSplitContainer);
			this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
			this.tabPageMessages.Name = "tabPageMessages";
			this.tabPageMessages.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMessages.Size = new System.Drawing.Size(284, 247);
			this.tabPageMessages.TabIndex = 1;
			this.tabPageMessages.Text = "Messages";
			this.tabPageMessages.UseVisualStyleBackColor = true;
			// 
			// messageSplitContainer
			// 
			this.messageSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.messageSplitContainer.Location = new System.Drawing.Point(3, 3);
			this.messageSplitContainer.Name = "messageSplitContainer";
			this.messageSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// messageSplitContainer.Panel1
			// 
			this.messageSplitContainer.Panel1.Controls.Add(this.listBoxMessages);
			// 
			// messageSplitContainer.Panel2
			// 
			this.messageSplitContainer.Panel2.Controls.Add(this.groupBoxMessageInfo);
			this.messageSplitContainer.Size = new System.Drawing.Size(278, 241);
			this.messageSplitContainer.SplitterDistance = 92;
			this.messageSplitContainer.TabIndex = 0;
			// 
			// listBoxMessages
			// 
			this.listBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxMessages.FormatString = "S";
			this.listBoxMessages.FormattingEnabled = true;
			this.listBoxMessages.Location = new System.Drawing.Point(0, 0);
			this.listBoxMessages.Name = "listBoxMessages";
			this.listBoxMessages.ScrollAlwaysVisible = true;
			this.listBoxMessages.Size = new System.Drawing.Size(278, 92);
			this.listBoxMessages.TabIndex = 0;
			this.listBoxMessages.SelectedIndexChanged += new System.EventHandler(this.listBoxMessages_SelectedIndexChanged);
			// 
			// groupBoxMessageInfo
			// 
			this.groupBoxMessageInfo.Controls.Add(this.textBoxMessageInfo);
			this.groupBoxMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxMessageInfo.Location = new System.Drawing.Point(0, 0);
			this.groupBoxMessageInfo.Name = "groupBoxMessageInfo";
			this.groupBoxMessageInfo.Size = new System.Drawing.Size(278, 145);
			this.groupBoxMessageInfo.TabIndex = 0;
			this.groupBoxMessageInfo.TabStop = false;
			this.groupBoxMessageInfo.Text = "Message info";
			// 
			// textBoxMessageInfo
			// 
			this.textBoxMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMessageInfo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxMessageInfo.Location = new System.Drawing.Point(3, 16);
			this.textBoxMessageInfo.Multiline = true;
			this.textBoxMessageInfo.Name = "textBoxMessageInfo";
			this.textBoxMessageInfo.ReadOnly = true;
			this.textBoxMessageInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMessageInfo.Size = new System.Drawing.Size(272, 126);
			this.textBoxMessageInfo.TabIndex = 0;
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "GBR files|*.gbr|All files|*.*";
			// 
			// auListener
			// 
			this.auListener.FileName = null;
			this.auListener.OnTileChanged += new GB.Shared.AutoUpdate.TileChangedEventHandler(this.auListener_OnTileChanged);
			this.auListener.OnTotalRefreshNeeded += new GB.Shared.AutoUpdate.MessageEventHandler(this.auListener_OnTotalRefreshNeeded);
			this.auListener.OnTileRefreshNeeded += new GB.Shared.AutoUpdate.MessageEventHandler(this.auListener_OnTileRefreshNeeded);
			this.auListener.OnTileSizeChanged += new GB.Shared.AutoUpdate.MessageEventHandler(this.auListener_OnTileSizeChanged);
			this.auListener.OnGBPaletteChanged += new GB.Shared.AutoUpdate.MessageEventHandler(this.auListener_OnGBPaletteChanged);
			this.auListener.OnColorPaletteChanged += new GB.Shared.AutoUpdate.MessageEventHandler(this.auListener_OnColorPaletteChanged);
			// 
			// groupBoxMessageName
			// 
			this.groupBoxMessageName.Controls.Add(this.labelMessageName);
			this.groupBoxMessageName.Location = new System.Drawing.Point(8, 51);
			this.groupBoxMessageName.Name = "groupBoxMessageName";
			this.groupBoxMessageName.Size = new System.Drawing.Size(268, 39);
			this.groupBoxMessageName.TabIndex = 3;
			this.groupBoxMessageName.TabStop = false;
			this.groupBoxMessageName.Text = "Message name";
			// 
			// labelMessageName
			// 
			this.labelMessageName.AutoSize = true;
			this.labelMessageName.Location = new System.Drawing.Point(6, 16);
			this.labelMessageName.Name = "labelMessageName";
			this.labelMessageName.Size = new System.Drawing.Size(135, 13);
			this.labelMessageName.TabIndex = 0;
			this.labelMessageName.Text = "[Message name goes here]";
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
			this.groupBoxMessageHex.ResumeLayout(false);
			this.groupBoxMessageHex.PerformLayout();
			this.groupBoxFileName.ResumeLayout(false);
			this.groupBoxFileName.PerformLayout();
			this.tabPageMessages.ResumeLayout(false);
			this.messageSplitContainer.Panel1.ResumeLayout(false);
			this.messageSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.messageSplitContainer)).EndInit();
			this.messageSplitContainer.ResumeLayout(false);
			this.groupBoxMessageInfo.ResumeLayout(false);
			this.groupBoxMessageInfo.PerformLayout();
			this.groupBoxMessageName.ResumeLayout(false);
			this.groupBoxMessageName.PerformLayout();
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
		private System.Windows.Forms.SplitContainer messageSplitContainer;
		private System.Windows.Forms.ListBox listBoxMessages;
		private System.Windows.Forms.GroupBox groupBoxMessageInfo;
		private System.Windows.Forms.TextBox textBoxMessageInfo;
		private System.Windows.Forms.GroupBox groupBoxMessageName;
		private System.Windows.Forms.Label labelMessageName;
	}
}

