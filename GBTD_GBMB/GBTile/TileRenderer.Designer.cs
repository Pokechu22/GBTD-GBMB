namespace GB.Shared.Tile
{
	partial class TileRenderer
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
			this.SuspendLayout();
			// 
			// TileRenderer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "TileRenderer";
			this.Size = new System.Drawing.Size(8, 8);
			this.Load += new System.EventHandler(this.RenderData_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.RenderData_Paint);
			this.ResumeLayout(false);

		}

		#endregion
	}
}
