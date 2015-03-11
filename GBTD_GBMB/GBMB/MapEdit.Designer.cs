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
			GB.Shared.Palettes.PaletteData paletteData2 = new GB.Shared.Palettes.PaletteData();
			this.button1 = new System.Windows.Forms.Button();
			this.mapControl1 = new GB.GBMB.MapControl();
			this.mapEditBorder = new GB.Shared.Controls.Border();
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
			// mapControl1
			// 
			this.mapControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(208)))), ((int)(((byte)(212)))));
			this.mapControl1.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_COLOR;
			this.mapControl1.DefaultPalette = null;
			this.mapControl1.Location = new System.Drawing.Point(12, 12);
			this.mapControl1.Map = null;
			this.mapControl1.Name = "mapControl1";
			this.mapControl1.PaletteData = paletteData2;
			this.mapControl1.ShowDoubleMarkers = false;
			this.mapControl1.ShowGrid = true;
			this.mapControl1.Size = new System.Drawing.Size(292, 273);
			this.mapControl1.TabIndex = 0;
			this.mapControl1.Text = "mapControl1";
			this.mapControl1.TileSet = null;
			this.mapControl1.Zoom = 4F;
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
		private Shared.Controls.Border mapEditBorder;
	}
}

