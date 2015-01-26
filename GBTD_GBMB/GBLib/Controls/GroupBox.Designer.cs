namespace GB.Shared.Controls
{
	partial class GroupBox
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
			System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
			this.cleanLabel1 = new GB.Shared.Controls.CleanLabel();
			this.border3 = new GB.Shared.Controls.Border();
			this.border1 = new GB.Shared.Controls.Border();
			this.border2 = new GB.Shared.Controls.Border();
			this.SuspendLayout();
			// 
			// cleanLabel1
			// 
			stringFormat1.Alignment = System.Drawing.StringAlignment.Near;
			stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
			stringFormat1.LineAlignment = System.Drawing.StringAlignment.Near;
			stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
			this.cleanLabel1.Format = stringFormat1;
			this.cleanLabel1.Location = new System.Drawing.Point(6, 0);
			this.cleanLabel1.Name = "cleanLabel1";
			this.cleanLabel1.Size = new System.Drawing.Size(65, 14);
			this.cleanLabel1.TabIndex = 2;
			this.cleanLabel1.Text = "cleanLabel1";
			// 
			// border3
			// 
			this.border3.BackColor = System.Drawing.Color.Transparent;
			this.border3.BottomBorder = null;
			this.border3.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.border3.LeftBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.border3.Location = new System.Drawing.Point(1, 6);
			this.border3.Name = "border3";
			this.border3.RightBorder = null;
			this.border3.Size = new System.Drawing.Size(147, 142);
			this.border3.TabIndex = 3;
			this.border3.Text = "border3";
			this.border3.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// border1
			// 
			this.border1.BackColor = System.Drawing.Color.Transparent;
			this.border1.BottomBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.border1.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Bottom};
			this.border1.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.border1.Location = new System.Drawing.Point(0, 5);
			this.border1.Name = "border1";
			this.border1.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			this.border1.Size = new System.Drawing.Size(149, 144);
			this.border1.TabIndex = 0;
			this.border1.Text = "border1";
			this.border1.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// border2
			// 
			this.border2.BackColor = System.Drawing.Color.Transparent;
			this.border2.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.border2.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.border2.LeftBorder = null;
			this.border2.Location = new System.Drawing.Point(1, 6);
			this.border2.Name = "border2";
			this.border2.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.border2.Size = new System.Drawing.Size(149, 144);
			this.border2.TabIndex = 1;
			this.border2.Text = "border2";
			this.border2.TopBorder = null;
			// 
			// GroupBox
			// 
			this.Controls.Add(this.cleanLabel1);
			this.Controls.Add(this.border3);
			this.Controls.Add(this.border1);
			this.Controls.Add(this.border2);
			this.Name = "GroupBox";
			this.ResumeLayout(false);

		}

		#endregion

		private Border border1;
		private Border border2;
		private CleanLabel cleanLabel1;
		private Border border3;
	}
}
