namespace GBRenderer
{
	partial class Form1
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
			GB.Shared.Palette.Palette_ gbcPalette1 = new GB.Shared.Palette.Palette_();
			GB.Shared.Palette.Palette_ gbcPalette2 = new GB.Shared.Palette.Palette_();
			GB.Shared.Palette.Palette_ gbcPalette3 = new GB.Shared.Palette.Palette_();
			GB.Shared.Palette.Palette_ gbcPalette4 = new GB.Shared.Palette.Palette_();
			GB.Shared.Palette.Palette_ gbcPalette5 = new GB.Shared.Palette.Palette_();
			GB.Shared.Palette.Palette_ gbcPalette6 = new GB.Shared.Palette.Palette_();
			GB.Shared.Palette.Palette_ gbcPalette7 = new GB.Shared.Palette.Palette_();
			GB.Shared.Palette.Palette_ gbcPalette8 = new GB.Shared.Palette.Palette_();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.offsetUpDown = new System.Windows.Forms.NumericUpDown();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.buttonPalette = new System.Windows.Forms.Button();
			this.gbtdPaletteChooser1 = new GB.Shared.Palette.GBTDPaletteChooser();
			this.tileRenderer = new GB.Shared.Tile.EditableTileRenderer();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.offsetUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.gbtdPaletteChooser1);
			this.groupBox1.Controls.Add(this.tileRenderer);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(268, 249);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Tile";
			// 
			// buttonOpen
			// 
			this.buttonOpen.Location = new System.Drawing.Point(12, 267);
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.Size = new System.Drawing.Size(75, 23);
			this.buttonOpen.TabIndex = 1;
			this.buttonOpen.Text = "Open...";
			this.buttonOpen.UseVisualStyleBackColor = true;
			this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
			// 
			// offsetUpDown
			// 
			this.offsetUpDown.Hexadecimal = true;
			this.offsetUpDown.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.offsetUpDown.Location = new System.Drawing.Point(174, 270);
			this.offsetUpDown.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.offsetUpDown.Name = "offsetUpDown";
			this.offsetUpDown.Size = new System.Drawing.Size(106, 20);
			this.offsetUpDown.TabIndex = 2;
			this.offsetUpDown.ValueChanged += new System.EventHandler(this.offsetUpDown_ValueChanged);
			// 
			// buttonPalette
			// 
			this.buttonPalette.Location = new System.Drawing.Point(93, 267);
			this.buttonPalette.Name = "buttonPalette";
			this.buttonPalette.Size = new System.Drawing.Size(75, 23);
			this.buttonPalette.TabIndex = 3;
			this.buttonPalette.Text = "Palette....";
			this.buttonPalette.UseVisualStyleBackColor = true;
			this.buttonPalette.Click += new System.EventHandler(this.buttonPalette_Click);
			// 
			// gbtdPaletteChooser1
			// 
			this.gbtdPaletteChooser1.DisplayedButtons = ((System.Windows.Forms.MouseButtons)((System.Windows.Forms.MouseButtons.Left | System.Windows.Forms.MouseButtons.Right)));
			this.gbtdPaletteChooser1.Location = new System.Drawing.Point(54, 51);
			this.gbtdPaletteChooser1.Margin = new System.Windows.Forms.Padding(2);
			this.gbtdPaletteChooser1.Name = "gbtdPaletteChooser1";
			this.gbtdPaletteChooser1.Padding = new System.Windows.Forms.Padding(2);
			gbcPalette1.entry3.color = System.Drawing.Color.Black;
			gbcPalette1.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette1.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette1.entry0.color = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			gbcPalette2.entry3.color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			gbcPalette2.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette2.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette2.entry0.color = System.Drawing.Color.White;
			gbcPalette3.entry3.color = System.Drawing.Color.Black;
			gbcPalette3.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette3.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette3.entry0.color = System.Drawing.Color.White;
			gbcPalette4.entry3.color = System.Drawing.Color.Black;
			gbcPalette4.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette4.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette4.entry0.color = System.Drawing.Color.White;
			gbcPalette5.entry3.color = System.Drawing.Color.Black;
			gbcPalette5.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette5.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette5.entry0.color = System.Drawing.Color.White;
			gbcPalette6.entry3.color = System.Drawing.Color.Black;
			gbcPalette6.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette6.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette6.entry0.color = System.Drawing.Color.White;
			gbcPalette7.entry3.color = System.Drawing.Color.Black;
			gbcPalette7.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette7.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette7.entry0.color = System.Drawing.Color.White;
			gbcPalette8.entry3.color = System.Drawing.Color.Black;
			gbcPalette8.entry2.color = System.Drawing.Color.DarkGray;
			gbcPalette8.entry1.color = System.Drawing.Color.LightGray;
			gbcPalette8.entry0.color = System.Drawing.Color.White;
			/*this.gbtdPaletteChooser1.Set.palettes = new GB.Shared.Palette.Palette_[] {
        gbcPalette1,
        gbcPalette2,
        gbcPalette3,
        gbcPalette4,
        gbcPalette5,
        gbcPalette6,
        gbcPalette7,
        gbcPalette8};*/
			this.gbtdPaletteChooser1.Size = new System.Drawing.Size(191, 26);
			this.gbtdPaletteChooser1.TabIndex = 2;
			this.gbtdPaletteChooser1.UseGBCFilter = false;
			// 
			// tileRenderer
			// 
			this.tileRenderer.BlackColor = System.Drawing.Color.Black;
			this.tileRenderer.DarkGrayColor = System.Drawing.Color.Gray;
			this.tileRenderer.LeftMouseColor = GB.Shared.Tile.GBColor.BLACK;
			this.tileRenderer.LightGrayColor = System.Drawing.Color.LightGray;
			this.tileRenderer.Location = new System.Drawing.Point(6, 19);
			this.tileRenderer.Margin = new System.Windows.Forms.Padding(0);
			this.tileRenderer.MiddleMouseColor = GB.Shared.Tile.GBColor.DARK_GRAY;
			this.tileRenderer.Name = "tileRenderer";
			this.tileRenderer.RightMouseColor = GB.Shared.Tile.GBColor.WHITE;
			this.tileRenderer.Size = new System.Drawing.Size(256, 224);
			this.tileRenderer.TabIndex = 1;
			this.tileRenderer.WhiteColor = System.Drawing.Color.White;
			this.tileRenderer.XButton1MouseColor = GB.Shared.Tile.GBColor.WHITE;
			this.tileRenderer.XButton2MouseColor = GB.Shared.Tile.GBColor.WHITE;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 302);
			this.Controls.Add(this.buttonPalette);
			this.Controls.Add(this.offsetUpDown);
			this.Controls.Add(this.buttonOpen);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.offsetUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private GB.Shared.Tile.EditableTileRenderer tileRenderer;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.NumericUpDown offsetUpDown;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button buttonPalette;
		private GB.Shared.Palette.GBTDPaletteChooser gbtdPaletteChooser1;



	}
}

