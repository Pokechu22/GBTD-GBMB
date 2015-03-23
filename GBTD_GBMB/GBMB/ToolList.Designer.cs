namespace GB.GBMB
{
	partial class ToolList
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
			GB.Shared.Controls.Border border;
			this.border1 = new GB.Shared.Controls.Border();
			border = new GB.Shared.Controls.Border();
			this.SuspendLayout();
			// 
			// border
			// 
			border.BottomBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			border.Dock = System.Windows.Forms.DockStyle.Fill;
			border.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			border.LeftBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			border.Location = new System.Drawing.Point(0, 0);
			border.Name = "border";
			border.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			border.Size = new System.Drawing.Size(26, 174);
			border.TabIndex = 0;
			border.Text = "border1";
			border.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// border1
			// 
			this.border1.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.border1.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Bottom,
        System.Windows.Forms.Border3DSide.Left};
			this.border1.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.border1.Location = new System.Drawing.Point(2, 72);
			this.border1.Name = "border1";
			this.border1.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.border1.Size = new System.Drawing.Size(22, 82);
			this.border1.TabIndex = 1;
			this.border1.Text = "border1";
			this.border1.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// ToolList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.border1);
			this.Controls.Add(border);
			this.MaximumSize = new System.Drawing.Size(26, 174);
			this.MinimumSize = new System.Drawing.Size(26, 174);
			this.Name = "ToolList";
			this.Size = new System.Drawing.Size(26, 174);
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.Border border1;

	}
}
