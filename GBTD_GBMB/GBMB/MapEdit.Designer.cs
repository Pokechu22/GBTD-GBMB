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
			GB.Shared.Palettes.PaletteData paletteData1 = new GB.Shared.Palettes.PaletteData();
			System.Windows.Forms.MenuItem seperatorMenuItem1;
			System.Windows.Forms.MenuItem seperatorMenuItem2;
			System.Windows.Forms.MenuItem seperatorMenuItem3;
			System.Windows.Forms.MenuItem seperatorMenuItem4;
			this.button1 = new System.Windows.Forms.Button();
			this.mapEditBorder = new GB.Shared.Controls.Border();
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.fileMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.mapControl1 = new GB.GBMB.MapControl();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem1 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem2 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem3 = new System.Windows.Forms.MenuItem();
			seperatorMenuItem4 = new System.Windows.Forms.MenuItem();
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
			this.mapEditBorder.Size = new System.Drawing.Size(292, 273);
			this.mapEditBorder.TabIndex = 2;
			this.mapEditBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.mapEditBorder.Resize += new System.EventHandler(this.mapEditBorder_Resize);
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5});
			// 
			// fileMenuItem
			// 
			this.fileMenuItem.Index = 0;
			this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem6,
            this.menuItem7,
            seperatorMenuItem1,
            this.menuItem9,
            seperatorMenuItem2,
            this.menuItem12,
            this.menuItem13,
            this.menuItem14,
            seperatorMenuItem3,
            this.menuItem16,
            this.menuItem17,
            seperatorMenuItem4,
            this.menuItem19});
			this.fileMenuItem.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Edit";
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
			// mapControl1
			// 
			this.mapControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(208)))), ((int)(((byte)(212)))));
			this.mapControl1.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_COLOR;
			this.mapControl1.DefaultPalette = null;
			this.mapControl1.Location = new System.Drawing.Point(1, 1);
			this.mapControl1.Map = null;
			this.mapControl1.Name = "mapControl1";
			this.mapControl1.PaletteData = paletteData1;
			this.mapControl1.ShowDoubleMarkers = false;
			this.mapControl1.ShowGrid = true;
			this.mapControl1.Size = new System.Drawing.Size(290, 271);
			this.mapControl1.TabIndex = 0;
			this.mapControl1.Text = "mapControl1";
			this.mapControl1.TileSet = null;
			this.mapControl1.Zoom = 4F;
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Open...";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "Save";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.Text = "Save as...";
			// 
			// seperatorMenuItem1
			// 
			seperatorMenuItem1.Index = 3;
			seperatorMenuItem1.Text = "-";
			// 
			// menuItem9
			// 
			this.menuItem9.Enabled = false;
			this.menuItem9.Index = 4;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10});
			this.menuItem9.Text = "Reopen";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 0;
			this.menuItem10.Text = "NYI";
			// 
			// seperatorMenuItem2
			// 
			seperatorMenuItem2.Index = 5;
			seperatorMenuItem2.Text = "-";
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 6;
			this.menuItem12.Text = "Map properties...";
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 7;
			this.menuItem13.Text = "Location properties...";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 8;
			this.menuItem14.Text = "Default location properties...";
			// 
			// seperatorMenuItem3
			// 
			seperatorMenuItem3.Index = 9;
			seperatorMenuItem3.Text = "-";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 10;
			this.menuItem16.Text = "Export";
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 11;
			this.menuItem17.Text = "Export to...";
			// 
			// seperatorMenuItem4
			// 
			seperatorMenuItem4.Index = 12;
			seperatorMenuItem4.Text = "-";
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 13;
			this.menuItem19.Text = "Exit";
			// 
			// MapEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
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
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem19;
	}
}

