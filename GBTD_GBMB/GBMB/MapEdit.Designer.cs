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
			GB.Shared.Palettes.PaletteData paletteData1 = new GB.Shared.Palettes.PaletteData();
			this.button1 = new System.Windows.Forms.Button();
			this.mapControl1 = new GB.GBMB.MapControl();
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
			this.mapControl1.BackColor = System.Drawing.Color.FromArgb(200, 208, 212);
			this.mapControl1.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_POCKET;
			this.mapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapControl1.Location = new System.Drawing.Point(0, 0);
			this.mapControl1.Map = null;
			this.mapControl1.Name = "mapControl1";
			this.mapControl1.PaletteData = paletteData1;
			this.mapControl1.Size = new System.Drawing.Size(292, 273);
			this.mapControl1.TabIndex = 0;
			this.mapControl1.Text = "mapControl1";
			this.mapControl1.TileSet = null;
			this.mapControl1.Zoom = 4F;
			// 
			// MapEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.mapControl1);
			this.Name = "MapEdit";
			this.Text = "Gameboy Map Builder";
			this.ResumeLayout(false);

		}

		#endregion

		private MapControl mapControl1;
		private System.Windows.Forms.Button button1;
	}
}

