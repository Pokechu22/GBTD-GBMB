namespace GBRenderer
{
	partial class ChoosePalette
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.gbColorSetSelector1 = new GBRenderer.GBColorSetSelector();
			this.colorPicker1 = new GBRenderer.TGammaPanel();
			this.SuspendLayout();
			// 
			// gbColorSetSelector1
			// 
			this.gbColorSetSelector1.CurrentColor = System.Drawing.Color.Gray;
			this.gbColorSetSelector1.EntryCount = 4;
			this.gbColorSetSelector1.EntryHeight = 19;
			this.gbColorSetSelector1.EntryWidth = 19;
			this.gbColorSetSelector1.Location = new System.Drawing.Point(12, 12);
			this.gbColorSetSelector1.MaximumSize = new System.Drawing.Size(76, 19);
			this.gbColorSetSelector1.MinimumSize = new System.Drawing.Size(76, 19);
			this.gbColorSetSelector1.Name = "gbColorSetSelector1";
			this.gbColorSetSelector1.SelectedIndex = 0;
			this.gbColorSetSelector1.Size = new System.Drawing.Size(76, 19);
			this.gbColorSetSelector1.TabIndex = 1;
			// 
			// colorPicker1
			// 
			this.colorPicker1.FirstColor = System.Drawing.Color.Black;
			this.colorPicker1.GBCFilter = false;
			this.colorPicker1.Location = new System.Drawing.Point(227, 12);
			this.colorPicker1.MaximumSize = new System.Drawing.Size(53, 230);
			this.colorPicker1.MinimumSize = new System.Drawing.Size(53, 230);
			this.colorPicker1.Name = "colorPicker1";
			this.colorPicker1.Size = new System.Drawing.Size(53, 230);
			this.colorPicker1.TabIndex = 0;
			this.colorPicker1.OnChange += new System.EventHandler(this.colorPicker1_OnChange);
			// 
			// ChoosePalette
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.gbColorSetSelector1);
			this.Controls.Add(this.colorPicker1);
			this.MaximumSize = new System.Drawing.Size(300, 300);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "ChoosePalette";
			this.Text = "Palettes";
			this.ResumeLayout(false);

		}

		#endregion

		private TGammaPanel colorPicker1;
		private GBColorSetSelector gbColorSetSelector1;

	}
}