namespace GB.GBTD
{
	partial class TileEdit
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.MenuItem menuItem6;
			System.Windows.Forms.MenuItem seperatorMenuItem1;
			System.Windows.Forms.MenuItem seperatorMenuItem2;
			System.Windows.Forms.MenuItem seperatorMenuItem3;
			GB.Shared.Tiles.PixelTileEditor pixelTileEditor2 = new GB.Shared.Tiles.PixelTileEditor();
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.openMenuItem = new System.Windows.Forms.MenuItem();
			this.saveMenuItem = new System.Windows.Forms.MenuItem();
			this.saveAsMenuItem = new System.Windows.Forms.MenuItem();
			this.reopenMenuItem = new System.Windows.Forms.MenuItem();
			this.exportMenuItem = new System.Windows.Forms.MenuItem();
			this.exportToMenuItem = new System.Windows.Forms.MenuItem();
			this.importFromMenuItem = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.editMenuItem = new System.Windows.Forms.MenuItem();
			this.designMenuItem = new System.Windows.Forms.MenuItem();
			this.viewMenuItem = new System.Windows.Forms.MenuItem();
			this.helpMenuItem = new System.Windows.Forms.MenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.openToolStripButton = new GB.GBTD.TileEdit.HoverChangingToolStripButton();
			this.saveToolStripButton = new GB.GBTD.TileEdit.HoverChangingToolStripButton();
			this.exportToolStripButton = new GB.GBTD.TileEdit.HoverChangingToolStripButton();
			this.cutToolStripButton = new GB.GBTD.TileEdit.HoverChangingToolStripButton();
			this.copyToolStripButton = new GB.GBTD.TileEdit.HoverChangingToolStripButton();
			this.pasteToolStripButton = new GB.GBTD.TileEdit.HoverChangingToolStripButton();
			this.helpToolStripButton = new GB.GBTD.TileEdit.HoverChangingToolStripButton();
			this.previewRenderer1 = new GB.GBTD.PreviewRenderer();
			this.tileList1 = new GB.Shared.Tiles.TileListControl();
			this.tileEditBorder = new GB.Shared.Controls.Border();
			this.paletteChooser = new GB.Shared.Palettes.GBTDPaletteChooser();
			this.mainTileEdit = new GB.Shared.Tiles.EditableTileRenderer();
			this.toolList = new GB.GBTD.ToolList();
			menuItem6 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem1 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem2 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem3 = new System.Windows.Forms.MenuItem();
			this.toolStrip1.SuspendLayout();
			this.tileEditBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuItem6
			// 
			menuItem6.Index = 0;
			menuItem6.Text = "Stuff should be here";
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.designMenuItem,
            this.viewMenuItem,
            this.helpMenuItem});
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            seperatorMenuItem1,
            this.reopenMenuItem,
            seperatorMenuItem2,
            this.exportMenuItem,
            this.exportToMenuItem,
            this.importFromMenuItem,
            seperatorMenuItem3,
            this.exitMenuItem});
			this.fileMenuItem.Text = "&File";
			// 
			// openMenuItem
			// 
			this.openMenuItem.Index = 0;
			this.openMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.openMenuItem.Text = "&Open...";
			// 
			// saveMenuItem
			// 
			this.saveMenuItem.Index = 1;
			this.saveMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.saveMenuItem.Text = "&Save";
			// 
			// saveAsMenuItem
			// 
			this.saveAsMenuItem.Index = 2;
			this.saveAsMenuItem.Text = "Save &As...";
			// 
			// seperatorMenuItem1
			// 
			seperatorMenuItem1.Index = 3;
			seperatorMenuItem1.Text = "-";
			// 
			// reopenMenuItem
			// 
			this.reopenMenuItem.Index = 4;
			this.reopenMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            menuItem6});
			this.reopenMenuItem.Text = "&Reopen";
			// 
			// seperatorMenuItem2
			// 
			seperatorMenuItem2.Index = 5;
			seperatorMenuItem2.Text = "-";
			// 
			// exportMenuItem
			// 
			this.exportMenuItem.Index = 6;
			this.exportMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.exportMenuItem.Text = "&Export";
			// 
			// exportToMenuItem
			// 
			this.exportToMenuItem.Index = 7;
			this.exportToMenuItem.Text = "Ex&port to...";
			// 
			// importFromMenuItem
			// 
			this.importFromMenuItem.Index = 8;
			this.importFromMenuItem.Text = "&Import from...";
			// 
			// seperatorMenuItem3
			// 
			seperatorMenuItem3.Index = 9;
			seperatorMenuItem3.Text = "-";
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Index = 10;
			this.exitMenuItem.Text = "E&xit";
			// 
			// editMenuItem
			// 
			this.editMenuItem.Index = 1;
			this.editMenuItem.Text = "&Edit";
			// 
			// designMenuItem
			// 
			this.designMenuItem.Index = 2;
			this.designMenuItem.Text = "&Design";
			// 
			// viewMenuItem
			// 
			this.viewMenuItem.Index = 3;
			this.viewMenuItem.Text = "&View";
			// 
			// helpMenuItem
			// 
			this.helpMenuItem.Index = 4;
			this.helpMenuItem.Text = "&Help";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.AutoSize = false;
			this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(2, 0, 3, 0);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.AutoSize = false;
			this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(2, 0, 3, 0);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.exportToolStripButton,
            this.toolStripSeparator1,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator2,
            this.helpToolStripButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(7, 0, 1, 1);
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(397, 30);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.AutoSize = false;
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.HoveredImage = global::GB.GBTD.Properties.Resources.Open_Hover;
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, -1, 0);
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.NonHoveredImage = global::GB.GBTD.Properties.Resources.Open_NoHover;
			this.openToolStripButton.Padding = new System.Windows.Forms.Padding(1);
			this.openToolStripButton.Size = new System.Drawing.Size(24, 24);
			this.openToolStripButton.Text = "Open";
			this.openToolStripButton.ToolTipText = "Open";
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.AutoSize = false;
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.HoveredImage = global::GB.GBTD.Properties.Resources.Save_Hover;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, -1, 0);
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.NonHoveredImage = global::GB.GBTD.Properties.Resources.Save_NoHover;
			this.saveToolStripButton.Padding = new System.Windows.Forms.Padding(1);
			this.saveToolStripButton.Size = new System.Drawing.Size(24, 24);
			this.saveToolStripButton.Text = "Save";
			this.saveToolStripButton.ToolTipText = "Save";
			// 
			// exportToolStripButton
			// 
			this.exportToolStripButton.AutoSize = false;
			this.exportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.exportToolStripButton.HoveredImage = global::GB.GBTD.Properties.Resources.Export_Hover;
			this.exportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.exportToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, -1, 0);
			this.exportToolStripButton.Name = "exportToolStripButton";
			this.exportToolStripButton.NonHoveredImage = global::GB.GBTD.Properties.Resources.Export_NoHover;
			this.exportToolStripButton.Padding = new System.Windows.Forms.Padding(1);
			this.exportToolStripButton.Size = new System.Drawing.Size(24, 24);
			this.exportToolStripButton.Text = "Export";
			this.exportToolStripButton.ToolTipText = "Export";
			// 
			// cutToolStripButton
			// 
			this.cutToolStripButton.AutoSize = false;
			this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cutToolStripButton.HoveredImage = global::GB.GBTD.Properties.Resources.Cut_Hover;
			this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, -1, 0);
			this.cutToolStripButton.Name = "cutToolStripButton";
			this.cutToolStripButton.NonHoveredImage = global::GB.GBTD.Properties.Resources.Cut_NoHover;
			this.cutToolStripButton.Padding = new System.Windows.Forms.Padding(1);
			this.cutToolStripButton.Size = new System.Drawing.Size(24, 24);
			this.cutToolStripButton.Text = "z";
			this.cutToolStripButton.ToolTipText = "Cut";
			this.cutToolStripButton.Click += new System.EventHandler(this.cutButtonClicked);
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.AutoSize = false;
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.HoveredImage = global::GB.GBTD.Properties.Resources.Copy_Hover;
			this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, -1, 0);
			this.copyToolStripButton.Name = "copyToolStripButton";
			this.copyToolStripButton.NonHoveredImage = global::GB.GBTD.Properties.Resources.Copy_NoHover;
			this.copyToolStripButton.Padding = new System.Windows.Forms.Padding(1);
			this.copyToolStripButton.Size = new System.Drawing.Size(24, 24);
			this.copyToolStripButton.Text = "Copy";
			this.copyToolStripButton.ToolTipText = "Copy";
			this.copyToolStripButton.Click += new System.EventHandler(this.copyButtonClicked);
			// 
			// pasteToolStripButton
			// 
			this.pasteToolStripButton.AutoSize = false;
			this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pasteToolStripButton.Enabled = false;
			this.pasteToolStripButton.HoveredImage = global::GB.GBTD.Properties.Resources.Paste_Hover;
			this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, -1, 0);
			this.pasteToolStripButton.Name = "pasteToolStripButton";
			this.pasteToolStripButton.NonHoveredImage = global::GB.GBTD.Properties.Resources.Paste_NoHover;
			this.pasteToolStripButton.Padding = new System.Windows.Forms.Padding(1);
			this.pasteToolStripButton.Size = new System.Drawing.Size(24, 24);
			this.pasteToolStripButton.Text = "Paste";
			this.pasteToolStripButton.ToolTipText = "Paste";
			this.pasteToolStripButton.Click += new System.EventHandler(this.pasteButtonClicked);
			// 
			// helpToolStripButton
			// 
			this.helpToolStripButton.AutoSize = false;
			this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.helpToolStripButton.HoveredImage = global::GB.GBTD.Properties.Resources.Help_Hover;
			this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.helpToolStripButton.Margin = new System.Windows.Forms.Padding(0, 0, -1, 0);
			this.helpToolStripButton.Name = "helpToolStripButton";
			this.helpToolStripButton.NonHoveredImage = global::GB.GBTD.Properties.Resources.Help_NoHover;
			this.helpToolStripButton.Padding = new System.Windows.Forms.Padding(1);
			this.helpToolStripButton.Size = new System.Drawing.Size(24, 24);
			this.helpToolStripButton.Text = "Help topics";
			this.helpToolStripButton.ToolTipText = "Help topics";
			// 
			// previewRenderer1
			// 
			this.previewRenderer1.Location = new System.Drawing.Point(235, 39);
			this.previewRenderer1.Name = "previewRenderer1";
			this.previewRenderer1.Simple = false;
			this.previewRenderer1.Size = new System.Drawing.Size(98, 192);
			this.previewRenderer1.TabIndex = 6;
			// 
			// tileList1
			// 
			this.tileList1.Location = new System.Drawing.Point(341, 38);
			this.tileList1.Name = "tileList1";
			this.tileList1.NumberOfEntries = 16;
			this.tileList1.SelectedEntry = 0;
			this.tileList1.Size = new System.Drawing.Size(56, 223);
			this.tileList1.TabIndex = 5;
			this.tileList1.SelectedEntryChanged += new System.EventHandler(this.tileList1_SelectedEntryChanged);
			// 
			// tileEditBorder
			// 
			this.tileEditBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tileEditBorder.Controls.Add(this.paletteChooser);
			this.tileEditBorder.Controls.Add(this.mainTileEdit);
			this.tileEditBorder.Controls.Add(this.toolList);
			this.tileEditBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.tileEditBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tileEditBorder.Location = new System.Drawing.Point(0, 34);
			this.tileEditBorder.Name = "tileEditBorder";
			this.tileEditBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tileEditBorder.Size = new System.Drawing.Size(338, 230);
			this.tileEditBorder.TabIndex = 4;
			this.tileEditBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// paletteChooser
			// 
			this.paletteChooser.DisplayedButtons = ((System.Windows.Forms.MouseButtons)((System.Windows.Forms.MouseButtons.Left | System.Windows.Forms.MouseButtons.Right)));
			this.paletteChooser.Location = new System.Drawing.Point(34, 200);
			this.paletteChooser.Margin = new System.Windows.Forms.Padding(2);
			this.paletteChooser.Name = "paletteChooser";
			this.paletteChooser.Padding = new System.Windows.Forms.Padding(2);
			this.paletteChooser.SelectedRow = 0;
			this.paletteChooser.Size = new System.Drawing.Size(191, 26);
			this.paletteChooser.TabIndex = 2;
			this.paletteChooser.UseGBCFilter = false;
			this.paletteChooser.SelectedPaletteChanged += new System.EventHandler(this.paletteChooser_SelectedPaletteChanged);
			this.paletteChooser.MouseButtonColorChanged += new System.EventHandler(this.gbtdgbcPaletteChooser1_MouseButtonColorChanged);
			// 
			// mainTileEdit
			// 
			this.mainTileEdit.Border = true;
			this.mainTileEdit.BorderSides = ((System.Windows.Forms.Border3DSide)(((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom) 
            | System.Windows.Forms.Border3DSide.Middle)));
			this.mainTileEdit.Editor = pixelTileEditor2;
			this.mainTileEdit.EditorTypeID = GB.Shared.Tiles.TileEditorID.PixelEdit;
			this.mainTileEdit.Grid = false;
			this.mainTileEdit.LeftMouseColor = GB.Shared.Tiles.GBColor.BLACK;
			this.mainTileEdit.Location = new System.Drawing.Point(33, 4);
			this.mainTileEdit.Margin = new System.Windows.Forms.Padding(0);
			this.mainTileEdit.MiddleMouseColor = GB.Shared.Tiles.GBColor.DARK_GRAY;
			this.mainTileEdit.Name = "mainTileEdit";
			this.mainTileEdit.NibbleMarkers = false;
			this.mainTileEdit.PixelScale = 24;
			this.mainTileEdit.RightMouseColor = GB.Shared.Tiles.GBColor.WHITE;
			this.mainTileEdit.Size = new System.Drawing.Size(193, 193);
			this.mainTileEdit.TabIndex = 1;
			this.mainTileEdit.XButton1MouseColor = GB.Shared.Tiles.GBColor.WHITE;
			this.mainTileEdit.XButton2MouseColor = GB.Shared.Tiles.GBColor.WHITE;
			this.mainTileEdit.TileChanged += new System.EventHandler(this.mainTileEdit_TileChanged);
			this.mainTileEdit.PalatteChanged += new System.EventHandler(this.mainTileEdit_PalatteChanged);
			// 
			// toolList
			// 
			this.toolList.AutoUpdate = false;
			this.toolList.Location = new System.Drawing.Point(4, 4);
			this.toolList.Name = "toolList";
			this.toolList.SelectedTool = GB.Shared.Tiles.TileEditorID.PixelEdit;
			this.toolList.Size = new System.Drawing.Size(26, 217);
			this.toolList.TabIndex = 0;
			this.toolList.SelectedToolChanged += new System.EventHandler(this.toolList1_SelectedToolChanged);
			this.toolList.ScrollUpClicked += new System.EventHandler(this.scrollUpClicked);
			this.toolList.ScrollDownClicked += new System.EventHandler(this.scrollDownClicked);
			this.toolList.ScrollLeftClicked += new System.EventHandler(this.scrollLeftClicked);
			this.toolList.ScrollRightClicked += new System.EventHandler(this.scrollRightClicked);
			this.toolList.FlipVerticallyClicked += new System.EventHandler(this.flipVerticallyClicked);
			this.toolList.FlipHorizontallyClicked += new System.EventHandler(this.flipHorizontallyClicked);
			this.toolList.RotateClockwiseClicked += new System.EventHandler(this.rotateClockwiseClicked);
			// 
			// TileEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(397, 264);
			this.Controls.Add(this.previewRenderer1);
			this.Controls.Add(this.tileList1);
			this.Controls.Add(this.tileEditBorder);
			this.Controls.Add(this.toolStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu;
			this.Name = "TileEdit";
			this.Text = " ";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tileEditBorder.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MainMenu mainMenu;
		private TileEdit.HoverChangingToolStripButton openToolStripButton;
		private TileEdit.HoverChangingToolStripButton saveToolStripButton;
		private TileEdit.HoverChangingToolStripButton exportToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private TileEdit.HoverChangingToolStripButton cutToolStripButton;
		private TileEdit.HoverChangingToolStripButton copyToolStripButton;
		private TileEdit.HoverChangingToolStripButton pasteToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private TileEdit.HoverChangingToolStripButton helpToolStripButton;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private GB.Shared.Controls.Border tileEditBorder;
		private ToolList toolList;
		private Shared.Palettes.GBTDPaletteChooser paletteChooser;
		private Shared.Tiles.EditableTileRenderer mainTileEdit;
		private Shared.Tiles.TileListControl tileList1;
		private PreviewRenderer previewRenderer1;
		private System.Windows.Forms.MenuItem fileMenuItem;
		private System.Windows.Forms.MenuItem editMenuItem;
		private System.Windows.Forms.MenuItem designMenuItem;
		private System.Windows.Forms.MenuItem viewMenuItem;
		private System.Windows.Forms.MenuItem helpMenuItem;
		private System.Windows.Forms.MenuItem openMenuItem;
		private System.Windows.Forms.MenuItem saveMenuItem;
		private System.Windows.Forms.MenuItem saveAsMenuItem;
		private System.Windows.Forms.MenuItem reopenMenuItem;
		private System.Windows.Forms.MenuItem exportMenuItem;
		private System.Windows.Forms.MenuItem exportToMenuItem;
		private System.Windows.Forms.MenuItem importFromMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;

	}
}

