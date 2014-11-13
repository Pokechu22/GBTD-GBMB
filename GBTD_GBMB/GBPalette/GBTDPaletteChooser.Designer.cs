namespace GB.Shared.Palette
{
	partial class GBTDPaletteChooser
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
			this.gbPaletteChooser1 = new GB.Shared.Palette.GBPaletteChooser();
			this.SuspendLayout();
			// 
			// gbPaletteChooser1
			// 
			this.gbPaletteChooser1.Dock = System.Windows.Forms.DockStyle.Right;
			this.gbPaletteChooser1.Location = new System.Drawing.Point(78, 2);
			this.gbPaletteChooser1.Margin = new System.Windows.Forms.Padding(0);
			this.gbPaletteChooser1.Name = "gbPaletteChooser1";
			this.gbPaletteChooser1.SelectedIndex = -1;
			this.gbPaletteChooser1.SelectOnLeftClick = true;
			this.gbPaletteChooser1.Size = new System.Drawing.Size(111, 22);
			this.gbPaletteChooser1.TabIndex = 0;
			this.gbPaletteChooser1.UseGBCFilter = false;
			this.gbPaletteChooser1.SelectedPaletteChanged += new GB.Shared.Palette.SelectedPaletteChangeEventHandler(this.gbPaletteChooser1_SelectedPaletteChanged);
			this.gbPaletteChooser1.PaletteEntryClicked += new GB.Shared.Palette.PaletteEntryClickEventHandler(this.gbPaletteChooser1_PaletteEntryClicked);
			// 
			// GBTDPaletteChooser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbPaletteChooser1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "GBTDPaletteChooser";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.Size = new System.Drawing.Size(191, 26);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GBTDPaletteChooser_Paint);
			this.ResumeLayout(false);

		}

		#endregion

		private GBPaletteChooser gbPaletteChooser1;
	}
}
