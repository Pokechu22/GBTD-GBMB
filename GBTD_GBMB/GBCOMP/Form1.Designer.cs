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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonOpen = new System.Windows.Forms.Button();
			this.offsetUpDown = new System.Windows.Forms.NumericUpDown();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.buttonPalette = new System.Windows.Forms.Button();
			this.tileRenderer = new GB.Shared.Tile.PixelEditableTileRenderer();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.offsetUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
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
		private GB.Shared.Tile.PixelEditableTileRenderer tileRenderer;
		private System.Windows.Forms.Button buttonOpen;
		private System.Windows.Forms.NumericUpDown offsetUpDown;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button buttonPalette;



	}
}

