namespace GB.GBMB
{
	partial class MapEdit
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
			System.Windows.Forms.MenuItem seperatorMenuItem1;
			System.Windows.Forms.MenuItem menuItem10;
			System.Windows.Forms.MenuItem seperatorMenuItem2;
			System.Windows.Forms.MenuItem seperatorMenuItem3;
			System.Windows.Forms.MenuItem seperatorMenuItem4;
			GB.Shared.Palettes.PaletteData paletteData2 = new GB.Shared.Palettes.PaletteData();
			System.Windows.Forms.MenuItem seperatorMenuItem5;
			System.Windows.Forms.MenuItem seperatorMenuItem6;
			this.button1 = new System.Windows.Forms.Button();
			this.mapEditBorder = new GB.Shared.Controls.Border();
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.editMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.openMenuItem = new System.Windows.Forms.MenuItem();
			this.saveMenuItem = new System.Windows.Forms.MenuItem();
			this.saveAsMenuItem = new System.Windows.Forms.MenuItem();
			this.reopenMenuItem = new System.Windows.Forms.MenuItem();
			this.mapPropertiesMenuItem = new System.Windows.Forms.MenuItem();
			this.locationPropertiesMenuItem = new System.Windows.Forms.MenuItem();
			this.defaultLocationPropertiesMenuItem = new System.Windows.Forms.MenuItem();
			this.exportMenuItem = new System.Windows.Forms.MenuItem();
			this.exportToMenuItem = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.mapControl1 = new GB.GBMB.MapControl();
			this.undoMenuItem = new System.Windows.Forms.MenuItem();
			this.cutMenuItem = new System.Windows.Forms.MenuItem();
			this.copyMenuItem = new System.Windows.Forms.MenuItem();
			this.pasteMenuItem = new System.Windows.Forms.MenuItem();
			this.copyAsBitmapMenuItem = new System.Windows.Forms.MenuItem();
			seperatorMenuItem1 = new System.Windows.Forms.MenuItem();
			menuItem10 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem2 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem3 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem4 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem5 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem6 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(173, 31);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// mapEditBorder
			// 
			this.mapEditBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.mapEditBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapEditBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Bottom,
        System.Windows.Forms.Border3DSide.Left};
			this.mapEditBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.mapEditBorder.Location = new System.Drawing.Point(0, 0);
			this.mapEditBorder.Name = "mapEditBorder";
			this.mapEditBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.mapEditBorder.Size = new System.Drawing.Size(292, 253);
			this.mapEditBorder.TabIndex = 2;
			this.mapEditBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.mapEditBorder.Resize += new System.EventHandler(this.mapEditBorder_Resize);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
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
            this.mapPropertiesMenuItem,
            this.locationPropertiesMenuItem,
            this.defaultLocationPropertiesMenuItem,
            seperatorMenuItem3,
            this.exportMenuItem,
            this.exportToMenuItem,
            seperatorMenuItem4,
            this.exitMenuItem});
			this.fileMenuItem.Text = "&File";
			// 
			// editMenuItem
			// 
			this.editMenuItem.Index = 1;
			this.editMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.undoMenuItem,
            seperatorMenuItem5,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            seperatorMenuItem6,
            this.copyAsBitmapMenuItem});
			this.editMenuItem.Text = "&Edit";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Design";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "View";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "Help";
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
			this.saveMenuItem.Text = "&Save";
			// 
			// saveAsMenuItem
			// 
			this.saveAsMenuItem.Index = 2;
			this.saveAsMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.saveAsMenuItem.Text = "Save &as...";
			// 
			// seperatorMenuItem1
			// 
			seperatorMenuItem1.Index = 3;
			seperatorMenuItem1.Text = "-";
			// 
			// reopenMenuItem
			// 
			this.reopenMenuItem.Enabled = false;
			this.reopenMenuItem.Index = 4;
			this.reopenMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            menuItem10});
			this.reopenMenuItem.Text = "&Reopen";
			// 
			// menuItem10
			// 
			menuItem10.Index = 0;
			menuItem10.Text = "NYI";
			// 
			// seperatorMenuItem2
			// 
			seperatorMenuItem2.Index = 5;
			seperatorMenuItem2.Text = "-";
			// 
			// mapPropertiesMenuItem
			// 
			this.mapPropertiesMenuItem.Index = 6;
			this.mapPropertiesMenuItem.Text = "&Map properties...";
			// 
			// locationPropertiesMenuItem
			// 
			this.locationPropertiesMenuItem.Index = 7;
			this.locationPropertiesMenuItem.Text = "&Location properties...";
			// 
			// defaultLocationPropertiesMenuItem
			// 
			this.defaultLocationPropertiesMenuItem.Index = 8;
			this.defaultLocationPropertiesMenuItem.Text = "&Default location properties...";
			// 
			// seperatorMenuItem3
			// 
			seperatorMenuItem3.Index = 9;
			seperatorMenuItem3.Text = "-";
			// 
			// exportMenuItem
			// 
			this.exportMenuItem.Index = 10;
			this.exportMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.exportMenuItem.Text = "&Export";
			// 
			// exportToMenuItem
			// 
			this.exportToMenuItem.Index = 11;
			this.exportToMenuItem.Text = "Ex&port to...";
			// 
			// seperatorMenuItem4
			// 
			seperatorMenuItem4.Index = 12;
			seperatorMenuItem4.Text = "-";
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Index = 13;
			this.exitMenuItem.Text = "E&xit";
			// 
			// mapControl1
			// 
			this.mapControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(208)))), ((int)(((byte)(212)))));
			this.mapControl1.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_COLOR;
			this.mapControl1.DefaultPalette = null;
			this.mapControl1.Location = new System.Drawing.Point(1, 1);
			this.mapControl1.Map = null;
			this.mapControl1.Name = "mapControl1";
			this.mapControl1.PaletteData = paletteData2;
			this.mapControl1.ShowDoubleMarkers = false;
			this.mapControl1.ShowGrid = true;
			this.mapControl1.Size = new System.Drawing.Size(290, 271);
			this.mapControl1.TabIndex = 0;
			this.mapControl1.Text = "mapControl1";
			this.mapControl1.TileSet = null;
			this.mapControl1.Zoom = 4F;
			// 
			// undoMenuItem
			// 
			this.undoMenuItem.Index = 0;
			this.undoMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
			this.undoMenuItem.Text = "&Undo";
			// 
			// seperatorMenuItem5
			// 
			seperatorMenuItem5.Index = 1;
			seperatorMenuItem5.Text = "-";
			// 
			// cutMenuItem
			// 
			this.cutMenuItem.Index = 2;
			this.cutMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.cutMenuItem.Text = "Cu&t";
			// 
			// copyMenuItem
			// 
			this.copyMenuItem.Index = 3;
			this.copyMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.copyMenuItem.Text = "&Copy";
			// 
			// pasteMenuItem
			// 
			this.pasteMenuItem.Index = 4;
			this.pasteMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.pasteMenuItem.Text = "&Paste";
			// 
			// seperatorMenuItem6
			// 
			seperatorMenuItem6.Index = 5;
			seperatorMenuItem6.Text = "-";
			// 
			// copyAsBitmapMenuItem
			// 
			this.copyAsBitmapMenuItem.Index = 6;
			this.copyAsBitmapMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
			this.copyAsBitmapMenuItem.Text = "Copy as bit&map";
			// 
			// MapEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 253);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.mapControl1);
			this.Controls.Add(this.mapEditBorder);
			this.Name = "MapEdit";
			this.Text = "Gameboy Map Builder";
			this.ResumeLayout(false);

		}

		#endregion

		private MapControl mapControl1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MainMenu mainMenu;
		private Shared.Controls.Border mapEditBorder;
		private System.Windows.Forms.MenuItem fileMenuItem;
		private System.Windows.Forms.MenuItem editMenuItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem openMenuItem;
		private System.Windows.Forms.MenuItem saveMenuItem;
		private System.Windows.Forms.MenuItem saveAsMenuItem;
		private System.Windows.Forms.MenuItem reopenMenuItem;
		private System.Windows.Forms.MenuItem mapPropertiesMenuItem;
		private System.Windows.Forms.MenuItem locationPropertiesMenuItem;
		private System.Windows.Forms.MenuItem defaultLocationPropertiesMenuItem;
		private System.Windows.Forms.MenuItem exportMenuItem;
		private System.Windows.Forms.MenuItem exportToMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.MenuItem undoMenuItem;
		private System.Windows.Forms.MenuItem cutMenuItem;
		private System.Windows.Forms.MenuItem copyMenuItem;
		private System.Windows.Forms.MenuItem pasteMenuItem;
		private System.Windows.Forms.MenuItem copyAsBitmapMenuItem;
	}
}

