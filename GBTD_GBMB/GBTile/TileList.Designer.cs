namespace GB.Shared.Tile
{
	partial class TileList
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.background = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.tileListEntry2 = new GB.Shared.Tile.TileListEntry();
			this.tileListEntry1 = new GB.Shared.Tile.TileListEntry();
			this.background.SuspendLayout();
			this.SuspendLayout();
			// 
			// background
			// 
			this.background.BackColor = System.Drawing.SystemColors.ControlDark;
			this.background.Controls.Add(this.tileListEntry2);
			this.background.Controls.Add(this.vScrollBar1);
			this.background.Controls.Add(this.tileListEntry1);
			this.background.Dock = System.Windows.Forms.DockStyle.Fill;
			this.background.Location = new System.Drawing.Point(0, 0);
			this.background.Name = "background";
			this.background.Padding = new System.Windows.Forms.Padding(1, 1, 0, 1);
			this.background.Size = new System.Drawing.Size(56, 150);
			this.background.TabIndex = 2;
			this.background.Paint += new System.Windows.Forms.PaintEventHandler(this.background_Paint);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.Location = new System.Drawing.Point(40, 1);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.vScrollBar1.Size = new System.Drawing.Size(16, 148);
			this.vScrollBar1.TabIndex = 2;
			// 
			// tileListEntry2
			// 
			this.tileListEntry2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.tileListEntry2.Black = System.Drawing.Color.Black;
			this.tileListEntry2.DarkGray = System.Drawing.Color.DarkGray;
			this.tileListEntry2.LightGray = System.Drawing.Color.LightGray;
			this.tileListEntry2.Location = new System.Drawing.Point(1, 18);
			this.tileListEntry2.Name = "tileListEntry2";
			this.tileListEntry2.Number = 0;
			this.tileListEntry2.Size = new System.Drawing.Size(38, 17);
			this.tileListEntry2.TabIndex = 3;
			this.tileListEntry2.White = System.Drawing.Color.White;
			// 
			// tileListEntry1
			// 
			this.tileListEntry1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.tileListEntry1.Black = System.Drawing.Color.Black;
			this.tileListEntry1.DarkGray = System.Drawing.Color.DarkGray;
			this.tileListEntry1.LightGray = System.Drawing.Color.LightGray;
			this.tileListEntry1.Location = new System.Drawing.Point(1, 1);
			this.tileListEntry1.Name = "tileListEntry1";
			this.tileListEntry1.Number = 0;
			this.tileListEntry1.Size = new System.Drawing.Size(38, 17);
			this.tileListEntry1.TabIndex = 1;
			this.tileListEntry1.White = System.Drawing.Color.White;
			// 
			// TileList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.background);
			this.Name = "TileList";
			this.Size = new System.Drawing.Size(56, 150);
			this.background.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TileListEntry tileListEntry1;
		private System.Windows.Forms.Panel background;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private TileListEntry tileListEntry2;
	}
}
