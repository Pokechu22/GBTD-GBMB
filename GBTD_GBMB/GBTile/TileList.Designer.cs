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
			this.@this = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.@this.SuspendLayout();
			this.SuspendLayout();
			// 
			// @this
			// 
			this.@this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.@this.Controls.Add(this.vScrollBar1);
			this.@this.Dock = System.Windows.Forms.DockStyle.Fill;
			this.@this.Location = new System.Drawing.Point(0, 0);
			this.@this.Name = "@this";
			this.@this.Padding = new System.Windows.Forms.Padding(1, 1, 0, 1);
			this.@this.Size = new System.Drawing.Size(56, 150);
			this.@this.TabIndex = 2;
			this.@this.Paint += new System.Windows.Forms.PaintEventHandler(this.background_Paint);
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
			// TileList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.@this);
			this.Name = "TileList";
			this.Size = new System.Drawing.Size(56, 150);
			this.Load += new System.EventHandler(this.TileList_Load);
			this.Resize += new System.EventHandler(this.TileList_Resize);
			this.@this.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel @this;
		private System.Windows.Forms.VScrollBar vScrollBar1;
	}
}
