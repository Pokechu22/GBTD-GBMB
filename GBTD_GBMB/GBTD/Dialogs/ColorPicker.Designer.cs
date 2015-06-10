namespace GB.GBTD.Dialogs
{
	partial class ColorPicker
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
			GB.Shared.Controls.Border mainBorder1;
			GB.Shared.Controls.Border mainBorder2;
			this.pictureBorder = new GB.Shared.Controls.Border();
			this.rgbBorder = new GB.Shared.Controls.Border();
			this.actualColor = new GB.Shared.Controls.Border();
			this.hoveredColor = new GB.Shared.Controls.Border();
			mainBorder1 = new GB.Shared.Controls.Border();
			mainBorder2 = new GB.Shared.Controls.Border();
			this.SuspendLayout();
			// 
			// mainBorder1
			// 
			mainBorder1.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			mainBorder1.Dock = System.Windows.Forms.DockStyle.Fill;
			mainBorder1.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			mainBorder1.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			mainBorder1.Location = new System.Drawing.Point(0, 0);
			mainBorder1.Name = "mainBorder1";
			mainBorder1.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			mainBorder1.Size = new System.Drawing.Size(53, 230);
			mainBorder1.TabIndex = 0;
			mainBorder1.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// mainBorder2
			// 
			mainBorder2.BottomBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			mainBorder2.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			mainBorder2.LeftBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			mainBorder2.Location = new System.Drawing.Point(1, 1);
			mainBorder2.Name = "mainBorder2";
			mainBorder2.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			mainBorder2.Size = new System.Drawing.Size(51, 228);
			mainBorder2.TabIndex = 1;
			mainBorder2.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// pictureBorder
			// 
			this.pictureBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pictureBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.pictureBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pictureBorder.Location = new System.Drawing.Point(2, 2);
			this.pictureBorder.Name = "pictureBorder";
			this.pictureBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.pictureBorder.Size = new System.Drawing.Size(49, 100);
			this.pictureBorder.TabIndex = 3;
			this.pictureBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// rgbBorder
			// 
			this.rgbBorder.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.rgbBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.rgbBorder.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.rgbBorder.Location = new System.Drawing.Point(2, 136);
			this.rgbBorder.Name = "rgbBorder";
			this.rgbBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.rgbBorder.Size = new System.Drawing.Size(49, 92);
			this.rgbBorder.TabIndex = 4;
			this.rgbBorder.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// actualColor
			// 
			this.actualColor.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.actualColor.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.actualColor.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.actualColor.Location = new System.Drawing.Point(2, 104);
			this.actualColor.Name = "actualColor";
			this.actualColor.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.actualColor.Size = new System.Drawing.Size(49, 30);
			this.actualColor.TabIndex = 5;
			this.actualColor.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// hoveredColor
			// 
			this.hoveredColor.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.hoveredColor.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			this.hoveredColor.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.hoveredColor.Location = new System.Drawing.Point(5, 194);
			this.hoveredColor.Name = "hoveredColor";
			this.hoveredColor.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.hoveredColor.Size = new System.Drawing.Size(42, 31);
			this.hoveredColor.TabIndex = 6;
			this.hoveredColor.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// ColorPicker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.hoveredColor);
			this.Controls.Add(this.actualColor);
			this.Controls.Add(this.rgbBorder);
			this.Controls.Add(this.pictureBorder);
			this.Controls.Add(mainBorder2);
			this.Controls.Add(mainBorder1);
			this.Name = "ColorPicker";
			this.Size = new System.Drawing.Size(53, 230);
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.Border pictureBorder;
		private Shared.Controls.Border rgbBorder;
		private Shared.Controls.Border actualColor;
		private Shared.Controls.Border hoveredColor;

	}
}
