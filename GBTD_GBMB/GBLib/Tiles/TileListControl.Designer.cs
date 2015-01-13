namespace GB.Shared.Tiles
{
	partial class TileListControl
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
			this.entriesPanel = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// entriesPanel
			// 
			this.entriesPanel.BackColor = System.Drawing.SystemColors.ControlDark;
			this.entriesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.entriesPanel.Location = new System.Drawing.Point(0, 0);
			this.entriesPanel.Name = "entriesPanel";
			this.entriesPanel.Padding = new System.Windows.Forms.Padding(1, 1, 0, 1);
			this.entriesPanel.Size = new System.Drawing.Size(56, 150);
			this.entriesPanel.TabIndex = 2;
			this.entriesPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.background_Paint);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.LargeChange = 1;
			this.vScrollBar1.Location = new System.Drawing.Point(40, 0);
			this.vScrollBar1.Maximum = 4;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
			this.vScrollBar1.Size = new System.Drawing.Size(16, 150);
			this.vScrollBar1.TabIndex = 2;
			this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
			// 
			// TileList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.entriesPanel);
			this.Name = "TileList";
			this.Size = new System.Drawing.Size(56, 150);
			this.Load += new System.EventHandler(this.TileList_Load);
			this.Resize += new System.EventHandler(this.TileList_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel entriesPanel;
		private System.Windows.Forms.VScrollBar vScrollBar1;
	}
}
