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
			this.pixelEditableTileRenderer1 = new GB.Shared.Tile.PixelEditableTileRenderer();
			this.SuspendLayout();
			// 
			// pixelEditableTileRenderer1
			// 
			this.pixelEditableTileRenderer1.BlackColor = System.Drawing.Color.Black;
			this.pixelEditableTileRenderer1.DarkGrayColor = System.Drawing.Color.Gray;
			this.pixelEditableTileRenderer1.Grid = true;
			this.pixelEditableTileRenderer1.LeftMouseColor = GB.Shared.Tile.GBColor.BLACK;
			this.pixelEditableTileRenderer1.LightGrayColor = System.Drawing.Color.LightGray;
			this.pixelEditableTileRenderer1.Location = new System.Drawing.Point(79, 43);
			this.pixelEditableTileRenderer1.Margin = new System.Windows.Forms.Padding(0);
			this.pixelEditableTileRenderer1.MiddleMouseColor = GB.Shared.Tile.GBColor.DARK_GRAY;
			this.pixelEditableTileRenderer1.Name = "pixelEditableTileRenderer1";
			this.pixelEditableTileRenderer1.RightMouseColor = GB.Shared.Tile.GBColor.WHITE;
			this.pixelEditableTileRenderer1.Size = new System.Drawing.Size(192, 192);
			this.pixelEditableTileRenderer1.TabIndex = 0;
			this.pixelEditableTileRenderer1.WhiteColor = System.Drawing.Color.White;
			this.pixelEditableTileRenderer1.XButton1MouseColor = GB.Shared.Tile.GBColor.WHITE;
			this.pixelEditableTileRenderer1.XButton2MouseColor = GB.Shared.Tile.GBColor.WHITE;
			// 
			// TileEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.pixelEditableTileRenderer1);
			this.Name = "TileEdit";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Tile.PixelEditableTileRenderer pixelEditableTileRenderer1;
	}
}

