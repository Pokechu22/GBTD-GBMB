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
			this.removeColButton = new GB.Shared.Controls.ImageButton();
			this.removeRowButton = new GB.Shared.Controls.ImageButton();
			this.addColButton = new GB.Shared.Controls.ImageButton();
			this.addRowButton = new GB.Shared.Controls.ImageButton();
			this.imageRadioButton3 = new GB.Shared.Controls.ImageRadioButton();
			this.imageRadioButton2 = new GB.Shared.Controls.ImageRadioButton();
			this.imageRadioButton1 = new GB.Shared.Controls.ImageRadioButton();
			this.borderInner = new GB.Shared.Controls.Border();
			border = new GB.Shared.Controls.Border();
			this.SuspendLayout();
			// 
			// removeColButton
			// 
			this.removeColButton.HoveredImage = global::GB.GBMB.Properties.Resources.RemoveCol_selected;
			this.removeColButton.Location = new System.Drawing.Point(3, 132);
			this.removeColButton.Name = "removeColButton";
			this.removeColButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.RemoveCol_nonselected;
			this.removeColButton.Size = new System.Drawing.Size(20, 20);
			this.removeColButton.TabIndex = 8;
			this.removeColButton.Text = "removeColButton";
			// 
			// removeRowButton
			// 
			this.removeRowButton.HoveredImage = global::GB.GBMB.Properties.Resources.RemoveRow_selected;
			this.removeRowButton.Location = new System.Drawing.Point(3, 112);
			this.removeRowButton.Name = "removeRowButton";
			this.removeRowButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.RemoveRow_nonselected;
			this.removeRowButton.Size = new System.Drawing.Size(20, 20);
			this.removeRowButton.TabIndex = 7;
			this.removeRowButton.Text = "removeRowButton";
			// 
			// addColButton
			// 
			this.addColButton.HoveredImage = global::GB.GBMB.Properties.Resources.AddCol_selected;
			this.addColButton.Location = new System.Drawing.Point(3, 92);
			this.addColButton.Name = "addColButton";
			this.addColButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.AddCol_nonselected;
			this.addColButton.Size = new System.Drawing.Size(20, 20);
			this.addColButton.TabIndex = 6;
			this.addColButton.Text = "addColButton";
			// 
			// addRowButton
			// 
			this.addRowButton.HoveredImage = global::GB.GBMB.Properties.Resources.AddRow_selected;
			this.addRowButton.Location = new System.Drawing.Point(3, 72);
			this.addRowButton.Name = "addRowButton";
			this.addRowButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.AddRow_nonselected;
			this.addRowButton.Size = new System.Drawing.Size(20, 20);
			this.addRowButton.TabIndex = 5;
			this.addRowButton.Text = "addRowButton";
			// 
			// imageRadioButton3
			// 
			this.imageRadioButton3.Checked = false;
			this.imageRadioButton3.HoveredImage = global::GB.GBMB.Properties.Resources.Dropper_selected;
			this.imageRadioButton3.Location = new System.Drawing.Point(2, 46);
			this.imageRadioButton3.Name = "imageRadioButton3";
			this.imageRadioButton3.NonhoveredImage = global::GB.GBMB.Properties.Resources.Dropper_nonselected;
			this.imageRadioButton3.Size = new System.Drawing.Size(22, 22);
			this.imageRadioButton3.TabIndex = 4;
			this.imageRadioButton3.Text = "imageRadioButton3";
			// 
			// imageRadioButton2
			// 
			this.imageRadioButton2.Checked = false;
			this.imageRadioButton2.HoveredImage = global::GB.GBMB.Properties.Resources.Flood_selected;
			this.imageRadioButton2.Location = new System.Drawing.Point(2, 24);
			this.imageRadioButton2.Name = "imageRadioButton2";
			this.imageRadioButton2.NonhoveredImage = global::GB.GBMB.Properties.Resources.Flood_nonselected;
			this.imageRadioButton2.Size = new System.Drawing.Size(22, 22);
			this.imageRadioButton2.TabIndex = 3;
			this.imageRadioButton2.Text = "imageRadioButton2";
			// 
			// imageRadioButton1
			// 
			this.imageRadioButton1.Checked = false;
			this.imageRadioButton1.HoveredImage = global::GB.GBMB.Properties.Resources.Pen_selected;
			this.imageRadioButton1.Location = new System.Drawing.Point(2, 2);
			this.imageRadioButton1.Name = "imageRadioButton1";
			this.imageRadioButton1.NonhoveredImage = global::GB.GBMB.Properties.Resources.Pen_nonselected;
			this.imageRadioButton1.Size = new System.Drawing.Size(22, 22);
			this.imageRadioButton1.TabIndex = 2;
			this.imageRadioButton1.Text = "imageRadioButton1";
			// 
			// borderInner
			// 
			this.borderInner.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.borderInner.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Bottom,
        System.Windows.Forms.Border3DSide.Left};
			this.borderInner.Enabled = false;
			this.borderInner.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.borderInner.Location = new System.Drawing.Point(2, 71);
			this.borderInner.Name = "borderInner";
			this.borderInner.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.borderInner.Size = new System.Drawing.Size(22, 82);
			this.borderInner.TabIndex = 1;
			this.borderInner.Text = "border1";
			this.borderInner.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
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
			// ToolList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.removeColButton);
			this.Controls.Add(this.removeRowButton);
			this.Controls.Add(this.addColButton);
			this.Controls.Add(this.addRowButton);
			this.Controls.Add(this.imageRadioButton3);
			this.Controls.Add(this.imageRadioButton2);
			this.Controls.Add(this.imageRadioButton1);
			this.Controls.Add(this.borderInner);
			this.Controls.Add(border);
			this.MaximumSize = new System.Drawing.Size(26, 174);
			this.MinimumSize = new System.Drawing.Size(26, 174);
			this.Name = "ToolList";
			this.Size = new System.Drawing.Size(26, 174);
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.Border borderInner;
		private Shared.Controls.ImageRadioButton imageRadioButton1;
		private Shared.Controls.ImageRadioButton imageRadioButton2;
		private Shared.Controls.ImageRadioButton imageRadioButton3;
		private Shared.Controls.ImageButton addRowButton;
		private Shared.Controls.ImageButton addColButton;
		private Shared.Controls.ImageButton removeRowButton;
		private Shared.Controls.ImageButton removeColButton;

	}
}
