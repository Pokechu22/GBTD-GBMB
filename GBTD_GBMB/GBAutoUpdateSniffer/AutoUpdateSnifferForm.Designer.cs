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
			this.groupBoxMessageName = new System.Windows.Forms.GroupBox();
			this.labelMessageName = new System.Windows.Forms.Label();
			this.groupBoxFileName = new System.Windows.Forms.GroupBox();
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.openButton = new System.Windows.Forms.Button();
			this.tabPageMemoryMappedFile = new System.Windows.Forms.TabPage();
			this.mmfSimpleTileInfoGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfSimpleInfoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.mmfTileWidthGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfTileWidthTextBox = new System.Windows.Forms.NumericUpDown();
			this.mmfTileHeightGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfTileHeightTextBox = new System.Windows.Forms.NumericUpDown();
			this.mmfTileCountGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfTileCountTextBox = new System.Windows.Forms.NumericUpDown();
			this.mmfIDAndColorMappingTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.mmfMagicMarkerGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfIDTextBox = new System.Windows.Forms.TextBox();
			this.mmfColorMappingGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfColorMappingTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.mmfColor0MappingGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfColor0MappingTextBox = new System.Windows.Forms.NumericUpDown();
			this.mmfColor1MappingGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfColor1MappingTextBox = new System.Windows.Forms.NumericUpDown();
			this.mmfColor2MappingGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfColor2MappingTextBox = new System.Windows.Forms.NumericUpDown();
			this.mmfColor3MappingGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfColor3MappingTextBox = new System.Windows.Forms.NumericUpDown();
			this.mmfFileNameGroupBox = new System.Windows.Forms.GroupBox();
			this.mmfFileNameTextBox = new System.Windows.Forms.TextBox();
			this.tabPageMessages = new System.Windows.Forms.TabPage();
			this.messageSplitContainer = new System.Windows.Forms.SplitContainer();
			this.listBoxMessages = new System.Windows.Forms.ListBox();
			this.groupBoxMessageInfo = new System.Windows.Forms.GroupBox();
			this.textBoxMessageInfo = new System.Windows.Forms.TextBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.auListener = new GB.Shared.AutoUpdate.AUMessenger();
			this.tabControl.SuspendLayout();
			this.tabPageFileData.SuspendLayout();
			this.groupBoxMessageHex.SuspendLayout();
			this.groupBoxMessageName.SuspendLayout();
			this.groupBoxFileName.SuspendLayout();
			this.tabPageMemoryMappedFile.SuspendLayout();
			this.mmfSimpleTileInfoGroupBox.SuspendLayout();
			this.mmfSimpleInfoTableLayoutPanel.SuspendLayout();
			this.mmfTileWidthGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mmfTileWidthTextBox)).BeginInit();
			this.mmfTileHeightGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mmfTileHeightTextBox)).BeginInit();
			this.mmfTileCountGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mmfTileCountTextBox)).BeginInit();
			this.mmfIDAndColorMappingTableLayoutPanel.SuspendLayout();
			this.mmfMagicMarkerGroupBox.SuspendLayout();
			this.mmfColorMappingGroupBox.SuspendLayout();
			this.mmfColorMappingTableLayoutPanel.SuspendLayout();
			this.mmfColor0MappingGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mmfColor0MappingTextBox)).BeginInit();
			this.mmfColor1MappingGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mmfColor1MappingTextBox)).BeginInit();
			this.mmfColor2MappingGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mmfColor2MappingTextBox)).BeginInit();
			this.mmfColor3MappingGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mmfColor3MappingTextBox)).BeginInit();
			this.mmfFileNameGroupBox.SuspendLayout();
			this.tabPageMessages.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.messageSplitContainer)).BeginInit();
			this.messageSplitContainer.Panel1.SuspendLayout();
			this.messageSplitContainer.Panel2.SuspendLayout();
			this.messageSplitContainer.SuspendLayout();
			this.groupBoxMessageInfo.SuspendLayout();
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
			this.tabPageFileData.Controls.Add(this.groupBoxMessageHex);
			this.tabPageFileData.Controls.Add(this.groupBoxMessageName);
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
			this.groupBoxMessageHex.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxMessageHex.Location = new System.Drawing.Point(3, 81);
			this.groupBoxMessageHex.Name = "groupBoxMessageHex";
			this.groupBoxMessageHex.Size = new System.Drawing.Size(278, 39);
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
			// groupBoxMessageName
			// 
			this.groupBoxMessageName.Controls.Add(this.labelMessageName);
			this.groupBoxMessageName.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxMessageName.Location = new System.Drawing.Point(3, 42);
			this.groupBoxMessageName.Name = "groupBoxMessageName";
			this.groupBoxMessageName.Size = new System.Drawing.Size(278, 39);
			this.groupBoxMessageName.TabIndex = 1;
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
			// groupBoxFileName
			// 
			this.groupBoxFileName.Controls.Add(this.fileNameLabel);
			this.groupBoxFileName.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxFileName.Location = new System.Drawing.Point(3, 3);
			this.groupBoxFileName.Name = "groupBoxFileName";
			this.groupBoxFileName.Size = new System.Drawing.Size(278, 39);
			this.groupBoxFileName.TabIndex = 0;
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
			this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.openButton.Location = new System.Drawing.Point(201, 216);
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(75, 23);
			this.openButton.TabIndex = 3;
			this.openButton.Text = "Open";
			this.openButton.UseVisualStyleBackColor = true;
			this.openButton.Click += new System.EventHandler(this.openButton_Click);
			// 
			// tabPageMemoryMappedFile
			// 
			this.tabPageMemoryMappedFile.Controls.Add(this.mmfIDAndColorMappingTableLayoutPanel);
			this.tabPageMemoryMappedFile.Controls.Add(this.mmfSimpleTileInfoGroupBox);
			this.tabPageMemoryMappedFile.Controls.Add(this.mmfFileNameGroupBox);
			this.tabPageMemoryMappedFile.Location = new System.Drawing.Point(4, 22);
			this.tabPageMemoryMappedFile.Name = "tabPageMemoryMappedFile";
			this.tabPageMemoryMappedFile.Size = new System.Drawing.Size(284, 247);
			this.tabPageMemoryMappedFile.TabIndex = 2;
			this.tabPageMemoryMappedFile.Text = "Memory Mapped File";
			this.tabPageMemoryMappedFile.UseVisualStyleBackColor = true;
			// 
			// mmfSimpleTileInfoGroupBox
			// 
			this.mmfSimpleTileInfoGroupBox.Controls.Add(this.mmfSimpleInfoTableLayoutPanel);
			this.mmfSimpleTileInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.mmfSimpleTileInfoGroupBox.Location = new System.Drawing.Point(0, 109);
			this.mmfSimpleTileInfoGroupBox.Name = "mmfSimpleTileInfoGroupBox";
			this.mmfSimpleTileInfoGroupBox.Size = new System.Drawing.Size(284, 64);
			this.mmfSimpleTileInfoGroupBox.TabIndex = 1;
			this.mmfSimpleTileInfoGroupBox.TabStop = false;
			this.mmfSimpleTileInfoGroupBox.Text = "Simple Info";
			// 
			// mmfSimpleInfoTableLayoutPanel
			// 
			this.mmfSimpleInfoTableLayoutPanel.ColumnCount = 3;
			this.mmfSimpleInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.mmfSimpleInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.mmfSimpleInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.mmfSimpleInfoTableLayoutPanel.Controls.Add(this.mmfTileWidthGroupBox, 1, 0);
			this.mmfSimpleInfoTableLayoutPanel.Controls.Add(this.mmfTileHeightGroupBox, 2, 0);
			this.mmfSimpleInfoTableLayoutPanel.Controls.Add(this.mmfTileCountGroupBox, 0, 0);
			this.mmfSimpleInfoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfSimpleInfoTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
			this.mmfSimpleInfoTableLayoutPanel.Name = "mmfSimpleInfoTableLayoutPanel";
			this.mmfSimpleInfoTableLayoutPanel.RowCount = 1;
			this.mmfSimpleInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mmfSimpleInfoTableLayoutPanel.Size = new System.Drawing.Size(278, 45);
			this.mmfSimpleInfoTableLayoutPanel.TabIndex = 0;
			// 
			// mmfTileWidthGroupBox
			// 
			this.mmfTileWidthGroupBox.Controls.Add(this.mmfTileWidthTextBox);
			this.mmfTileWidthGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfTileWidthGroupBox.Location = new System.Drawing.Point(95, 0);
			this.mmfTileWidthGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfTileWidthGroupBox.Name = "mmfTileWidthGroupBox";
			this.mmfTileWidthGroupBox.Size = new System.Drawing.Size(86, 42);
			this.mmfTileWidthGroupBox.TabIndex = 1;
			this.mmfTileWidthGroupBox.TabStop = false;
			this.mmfTileWidthGroupBox.Text = "Tile width";
			// 
			// mmfTileWidthTextBox
			// 
			this.mmfTileWidthTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfTileWidthTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfTileWidthTextBox.Name = "mmfTileWidthTextBox";
			this.mmfTileWidthTextBox.Size = new System.Drawing.Size(80, 20);
			this.mmfTileWidthTextBox.TabIndex = 0;
			// 
			// mmfTileHeightGroupBox
			// 
			this.mmfTileHeightGroupBox.Controls.Add(this.mmfTileHeightTextBox);
			this.mmfTileHeightGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfTileHeightGroupBox.Location = new System.Drawing.Point(187, 0);
			this.mmfTileHeightGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfTileHeightGroupBox.Name = "mmfTileHeightGroupBox";
			this.mmfTileHeightGroupBox.Size = new System.Drawing.Size(88, 42);
			this.mmfTileHeightGroupBox.TabIndex = 2;
			this.mmfTileHeightGroupBox.TabStop = false;
			this.mmfTileHeightGroupBox.Text = "Tile height";
			// 
			// mmfTileHeightTextBox
			// 
			this.mmfTileHeightTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfTileHeightTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfTileHeightTextBox.Name = "mmfTileHeightTextBox";
			this.mmfTileHeightTextBox.Size = new System.Drawing.Size(82, 20);
			this.mmfTileHeightTextBox.TabIndex = 0;
			// 
			// mmfTileCountGroupBox
			// 
			this.mmfTileCountGroupBox.Controls.Add(this.mmfTileCountTextBox);
			this.mmfTileCountGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfTileCountGroupBox.Location = new System.Drawing.Point(3, 0);
			this.mmfTileCountGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfTileCountGroupBox.Name = "mmfTileCountGroupBox";
			this.mmfTileCountGroupBox.Size = new System.Drawing.Size(86, 42);
			this.mmfTileCountGroupBox.TabIndex = 0;
			this.mmfTileCountGroupBox.TabStop = false;
			this.mmfTileCountGroupBox.Text = "Tile count";
			// 
			// mmfTileCountTextBox
			// 
			this.mmfTileCountTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfTileCountTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfTileCountTextBox.Name = "mmfTileCountTextBox";
			this.mmfTileCountTextBox.Size = new System.Drawing.Size(80, 20);
			this.mmfTileCountTextBox.TabIndex = 0;
			// 
			// mmfIDAndColorMappingTableLayoutPanel
			// 
			this.mmfIDAndColorMappingTableLayoutPanel.ColumnCount = 2;
			this.mmfIDAndColorMappingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.mmfIDAndColorMappingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.mmfIDAndColorMappingTableLayoutPanel.Controls.Add(this.mmfMagicMarkerGroupBox, 0, 0);
			this.mmfIDAndColorMappingTableLayoutPanel.Controls.Add(this.mmfColorMappingGroupBox, 1, 0);
			this.mmfIDAndColorMappingTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.mmfIDAndColorMappingTableLayoutPanel.Location = new System.Drawing.Point(0, 42);
			this.mmfIDAndColorMappingTableLayoutPanel.Name = "mmfIDAndColorMappingTableLayoutPanel";
			this.mmfIDAndColorMappingTableLayoutPanel.RowCount = 1;
			this.mmfIDAndColorMappingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mmfIDAndColorMappingTableLayoutPanel.Size = new System.Drawing.Size(284, 67);
			this.mmfIDAndColorMappingTableLayoutPanel.TabIndex = 2;
			// 
			// mmfMagicMarkerGroupBox
			// 
			this.mmfMagicMarkerGroupBox.Controls.Add(this.mmfIDTextBox);
			this.mmfMagicMarkerGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfMagicMarkerGroupBox.Location = new System.Drawing.Point(3, 0);
			this.mmfMagicMarkerGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfMagicMarkerGroupBox.Name = "mmfMagicMarkerGroupBox";
			this.mmfMagicMarkerGroupBox.Size = new System.Drawing.Size(50, 64);
			this.mmfMagicMarkerGroupBox.TabIndex = 0;
			this.mmfMagicMarkerGroupBox.TabStop = false;
			this.mmfMagicMarkerGroupBox.Text = "ID";
			// 
			// mmfIDTextBox
			// 
			this.mmfIDTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfIDTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfIDTextBox.Name = "mmfIDTextBox";
			this.mmfIDTextBox.Size = new System.Drawing.Size(44, 20);
			this.mmfIDTextBox.TabIndex = 0;
			// 
			// mmfColorMappingGroupBox
			// 
			this.mmfColorMappingGroupBox.Controls.Add(this.mmfColorMappingTableLayoutPanel);
			this.mmfColorMappingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfColorMappingGroupBox.Location = new System.Drawing.Point(59, 0);
			this.mmfColorMappingGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfColorMappingGroupBox.Name = "mmfColorMappingGroupBox";
			this.mmfColorMappingGroupBox.Size = new System.Drawing.Size(222, 64);
			this.mmfColorMappingGroupBox.TabIndex = 1;
			this.mmfColorMappingGroupBox.TabStop = false;
			this.mmfColorMappingGroupBox.Text = "Color Mapping";
			// 
			// mmfColorMappingTableLayoutPanel
			// 
			this.mmfColorMappingTableLayoutPanel.ColumnCount = 4;
			this.mmfColorMappingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mmfColorMappingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mmfColorMappingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mmfColorMappingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.mmfColorMappingTableLayoutPanel.Controls.Add(this.mmfColor0MappingGroupBox, 0, 0);
			this.mmfColorMappingTableLayoutPanel.Controls.Add(this.mmfColor1MappingGroupBox, 1, 0);
			this.mmfColorMappingTableLayoutPanel.Controls.Add(this.mmfColor2MappingGroupBox, 2, 0);
			this.mmfColorMappingTableLayoutPanel.Controls.Add(this.mmfColor3MappingGroupBox, 3, 0);
			this.mmfColorMappingTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfColorMappingTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
			this.mmfColorMappingTableLayoutPanel.Name = "mmfColorMappingTableLayoutPanel";
			this.mmfColorMappingTableLayoutPanel.RowCount = 1;
			this.mmfColorMappingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.mmfColorMappingTableLayoutPanel.Size = new System.Drawing.Size(216, 45);
			this.mmfColorMappingTableLayoutPanel.TabIndex = 0;
			// 
			// mmfColor0MappingGroupBox
			// 
			this.mmfColor0MappingGroupBox.Controls.Add(this.mmfColor0MappingTextBox);
			this.mmfColor0MappingGroupBox.Location = new System.Drawing.Point(3, 0);
			this.mmfColor0MappingGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfColor0MappingGroupBox.Name = "mmfColor0MappingGroupBox";
			this.mmfColor0MappingGroupBox.Size = new System.Drawing.Size(48, 42);
			this.mmfColor0MappingGroupBox.TabIndex = 0;
			this.mmfColor0MappingGroupBox.TabStop = false;
			this.mmfColor0MappingGroupBox.Text = "0";
			// 
			// mmfColor0MappingTextBox
			// 
			this.mmfColor0MappingTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfColor0MappingTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfColor0MappingTextBox.Name = "mmfColor0MappingTextBox";
			this.mmfColor0MappingTextBox.Size = new System.Drawing.Size(42, 20);
			this.mmfColor0MappingTextBox.TabIndex = 0;
			// 
			// mmfColor1MappingGroupBox
			// 
			this.mmfColor1MappingGroupBox.Controls.Add(this.mmfColor1MappingTextBox);
			this.mmfColor1MappingGroupBox.Location = new System.Drawing.Point(57, 0);
			this.mmfColor1MappingGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfColor1MappingGroupBox.Name = "mmfColor1MappingGroupBox";
			this.mmfColor1MappingGroupBox.Size = new System.Drawing.Size(48, 42);
			this.mmfColor1MappingGroupBox.TabIndex = 1;
			this.mmfColor1MappingGroupBox.TabStop = false;
			this.mmfColor1MappingGroupBox.Text = "1";
			// 
			// mmfColor1MappingTextBox
			// 
			this.mmfColor1MappingTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfColor1MappingTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfColor1MappingTextBox.Name = "mmfColor1MappingTextBox";
			this.mmfColor1MappingTextBox.Size = new System.Drawing.Size(42, 20);
			this.mmfColor1MappingTextBox.TabIndex = 1;
			// 
			// mmfColor2MappingGroupBox
			// 
			this.mmfColor2MappingGroupBox.Controls.Add(this.mmfColor2MappingTextBox);
			this.mmfColor2MappingGroupBox.Location = new System.Drawing.Point(111, 0);
			this.mmfColor2MappingGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfColor2MappingGroupBox.Name = "mmfColor2MappingGroupBox";
			this.mmfColor2MappingGroupBox.Size = new System.Drawing.Size(48, 42);
			this.mmfColor2MappingGroupBox.TabIndex = 2;
			this.mmfColor2MappingGroupBox.TabStop = false;
			this.mmfColor2MappingGroupBox.Text = "2";
			// 
			// mmfColor2MappingTextBox
			// 
			this.mmfColor2MappingTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfColor2MappingTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfColor2MappingTextBox.Name = "mmfColor2MappingTextBox";
			this.mmfColor2MappingTextBox.Size = new System.Drawing.Size(42, 20);
			this.mmfColor2MappingTextBox.TabIndex = 1;
			// 
			// mmfColor3MappingGroupBox
			// 
			this.mmfColor3MappingGroupBox.Controls.Add(this.mmfColor3MappingTextBox);
			this.mmfColor3MappingGroupBox.Location = new System.Drawing.Point(165, 0);
			this.mmfColor3MappingGroupBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.mmfColor3MappingGroupBox.Name = "mmfColor3MappingGroupBox";
			this.mmfColor3MappingGroupBox.Size = new System.Drawing.Size(48, 42);
			this.mmfColor3MappingGroupBox.TabIndex = 3;
			this.mmfColor3MappingGroupBox.TabStop = false;
			this.mmfColor3MappingGroupBox.Text = "3";
			// 
			// mmfColor3MappingTextBox
			// 
			this.mmfColor3MappingTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfColor3MappingTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfColor3MappingTextBox.Name = "mmfColor3MappingTextBox";
			this.mmfColor3MappingTextBox.Size = new System.Drawing.Size(42, 20);
			this.mmfColor3MappingTextBox.TabIndex = 1;
			// 
			// mmfFileNameGroupBox
			// 
			this.mmfFileNameGroupBox.Controls.Add(this.mmfFileNameTextBox);
			this.mmfFileNameGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.mmfFileNameGroupBox.Location = new System.Drawing.Point(0, 0);
			this.mmfFileNameGroupBox.Name = "mmfFileNameGroupBox";
			this.mmfFileNameGroupBox.Size = new System.Drawing.Size(284, 42);
			this.mmfFileNameGroupBox.TabIndex = 0;
			this.mmfFileNameGroupBox.TabStop = false;
			this.mmfFileNameGroupBox.Text = "File name";
			// 
			// mmfFileNameTextBox
			// 
			this.mmfFileNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mmfFileNameTextBox.Location = new System.Drawing.Point(3, 16);
			this.mmfFileNameTextBox.Name = "mmfFileNameTextBox";
			this.mmfFileNameTextBox.ReadOnly = true;
			this.mmfFileNameTextBox.Size = new System.Drawing.Size(278, 20);
			this.mmfFileNameTextBox.TabIndex = 0;
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
			this.groupBoxMessageName.ResumeLayout(false);
			this.groupBoxMessageName.PerformLayout();
			this.groupBoxFileName.ResumeLayout(false);
			this.groupBoxFileName.PerformLayout();
			this.tabPageMemoryMappedFile.ResumeLayout(false);
			this.mmfSimpleTileInfoGroupBox.ResumeLayout(false);
			this.mmfSimpleInfoTableLayoutPanel.ResumeLayout(false);
			this.mmfTileWidthGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mmfTileWidthTextBox)).EndInit();
			this.mmfTileHeightGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mmfTileHeightTextBox)).EndInit();
			this.mmfTileCountGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mmfTileCountTextBox)).EndInit();
			this.mmfIDAndColorMappingTableLayoutPanel.ResumeLayout(false);
			this.mmfMagicMarkerGroupBox.ResumeLayout(false);
			this.mmfMagicMarkerGroupBox.PerformLayout();
			this.mmfColorMappingGroupBox.ResumeLayout(false);
			this.mmfColorMappingTableLayoutPanel.ResumeLayout(false);
			this.mmfColor0MappingGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mmfColor0MappingTextBox)).EndInit();
			this.mmfColor1MappingGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mmfColor1MappingTextBox)).EndInit();
			this.mmfColor2MappingGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mmfColor2MappingTextBox)).EndInit();
			this.mmfColor3MappingGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mmfColor3MappingTextBox)).EndInit();
			this.mmfFileNameGroupBox.ResumeLayout(false);
			this.mmfFileNameGroupBox.PerformLayout();
			this.tabPageMessages.ResumeLayout(false);
			this.messageSplitContainer.Panel1.ResumeLayout(false);
			this.messageSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.messageSplitContainer)).EndInit();
			this.messageSplitContainer.ResumeLayout(false);
			this.groupBoxMessageInfo.ResumeLayout(false);
			this.groupBoxMessageInfo.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private GB.Shared.AutoUpdate.AUMessenger auListener;
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
		private System.Windows.Forms.TableLayoutPanel mmfIDAndColorMappingTableLayoutPanel;
		private System.Windows.Forms.GroupBox mmfMagicMarkerGroupBox;
		private System.Windows.Forms.TextBox mmfIDTextBox;
		private System.Windows.Forms.GroupBox mmfColorMappingGroupBox;
		private System.Windows.Forms.TableLayoutPanel mmfColorMappingTableLayoutPanel;
		private System.Windows.Forms.GroupBox mmfColor0MappingGroupBox;
		private System.Windows.Forms.NumericUpDown mmfColor0MappingTextBox;
		private System.Windows.Forms.GroupBox mmfColor1MappingGroupBox;
		private System.Windows.Forms.NumericUpDown mmfColor1MappingTextBox;
		private System.Windows.Forms.GroupBox mmfColor2MappingGroupBox;
		private System.Windows.Forms.NumericUpDown mmfColor2MappingTextBox;
		private System.Windows.Forms.GroupBox mmfColor3MappingGroupBox;
		private System.Windows.Forms.NumericUpDown mmfColor3MappingTextBox;
		private System.Windows.Forms.GroupBox mmfFileNameGroupBox;
		private System.Windows.Forms.TextBox mmfFileNameTextBox;
		private System.Windows.Forms.GroupBox mmfSimpleTileInfoGroupBox;
		private System.Windows.Forms.TableLayoutPanel mmfSimpleInfoTableLayoutPanel;
		private System.Windows.Forms.GroupBox mmfTileWidthGroupBox;
		private System.Windows.Forms.NumericUpDown mmfTileWidthTextBox;
		private System.Windows.Forms.GroupBox mmfTileHeightGroupBox;
		private System.Windows.Forms.NumericUpDown mmfTileHeightTextBox;
		private System.Windows.Forms.GroupBox mmfTileCountGroupBox;
		private System.Windows.Forms.NumericUpDown mmfTileCountTextBox;
	}
}

