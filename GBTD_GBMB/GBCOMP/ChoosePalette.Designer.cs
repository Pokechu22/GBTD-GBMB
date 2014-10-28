namespace GBRenderer
{
	partial class ChoosePalette
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
			this.gbPaletteSetSelector1 = new GBRenderer.GBPaletteSetSelector();
			this.colorPicker1 = new GBRenderer.TGammaPanel();
			this.SuspendLayout();
			// 
			// gbPaletteSetSelector1
			// 
			this.gbPaletteSetSelector1.BlackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.gbPaletteSetSelector1.DarkGrayColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
			this.gbPaletteSetSelector1.LightGrayColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.gbPaletteSetSelector1.Location = new System.Drawing.Point(12, 12);
			this.gbPaletteSetSelector1.Name = "gbPaletteSetSelector1";
			this.gbPaletteSetSelector1.Rows = 8;
			this.gbPaletteSetSelector1.SelectedX = -1;
			this.gbPaletteSetSelector1.SelectedY = -1;
			this.gbPaletteSetSelector1.Size = new System.Drawing.Size(112, 262);
			this.gbPaletteSetSelector1.TabIndex = 1;
			this.gbPaletteSetSelector1.WhiteColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			// 
			// colorPicker1
			// 
			this.colorPicker1.FirstColor = System.Drawing.Color.Black;
			this.colorPicker1.GBCFilter = false;
			this.colorPicker1.Location = new System.Drawing.Point(227, 12);
			this.colorPicker1.MaximumSize = new System.Drawing.Size(53, 230);
			this.colorPicker1.MinimumSize = new System.Drawing.Size(53, 230);
			this.colorPicker1.Name = "colorPicker1";
			this.colorPicker1.Size = new System.Drawing.Size(53, 230);
			this.colorPicker1.TabIndex = 0;
			this.colorPicker1.OnChange += new System.EventHandler(this.colorPicker1_OnChange);
			// 
			// ChoosePalette
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.gbPaletteSetSelector1);
			this.Controls.Add(this.colorPicker1);
			this.MaximumSize = new System.Drawing.Size(300, 300);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "ChoosePalette";
			this.Text = "Palettes";
			this.ResumeLayout(false);

		}

		#endregion

		private TGammaPanel colorPicker1;
		private GBPaletteSetSelector gbPaletteSetSelector1;

	}
}