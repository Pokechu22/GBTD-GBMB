namespace GB.Shared.Palette
{
	partial class GBPaletteChooser<TSet, TRow, TEntry>
		where TSet : PaletteSetBase<TRow, TEntry>, new()
		where TRow : PaletteBase<TEntry>
		where TEntry: PaletteEntryBase
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
			this.vScrollBar = new System.Windows.Forms.VScrollBar();
			this.dropDown = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// vScrollBar
			// 
			this.vScrollBar.LargeChange = 1;
			this.vScrollBar.Location = new System.Drawing.Point(0, 0);
			this.vScrollBar.Name = "vScrollBar";
			this.vScrollBar.Size = new System.Drawing.Size(16, 21);
			this.vScrollBar.TabIndex = 0;
			this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
			// 
			// dropDown
			// 
			this.dropDown.Dock = System.Windows.Forms.DockStyle.Right;
			this.dropDown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.dropDown.DropDownHeight = 160;
			this.dropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.dropDown.DropDownWidth = 95;
			this.dropDown.FormattingEnabled = true;
			this.dropDown.IntegralHeight = false;
			this.dropDown.ItemHeight = 16;
			this.dropDown.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
			this.dropDown.Location = new System.Drawing.Point(16, 0);
			this.dropDown.Margin = new System.Windows.Forms.Padding(0);
			this.dropDown.Name = "dropDown";
			this.dropDown.Size = new System.Drawing.Size(95, 22);
			this.dropDown.TabIndex = 1;
			this.dropDown.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.dropDown_DrawItem);
			this.dropDown.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.dropDown_MeasureItem);
			this.dropDown.SelectedIndexChanged += new System.EventHandler(this.dropDown_SelectedIndexChanged);
			this.dropDown.SelectionChangeCommitted += new System.EventHandler(this.dropDown_SelectionChangeCommitted);
			// 
			// GBPaletteChooser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dropDown);
			this.Controls.Add(this.vScrollBar);
			this.MaximumSize = new System.Drawing.Size(111, 22);
			this.MinimumSize = new System.Drawing.Size(111, 22);
			this.Name = "GBPaletteChooser";
			this.Size = new System.Drawing.Size(111, 22);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.VScrollBar vScrollBar;
		private System.Windows.Forms.ComboBox dropDown;
	}
}
