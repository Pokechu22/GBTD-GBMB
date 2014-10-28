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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.colorPicker1 = new GBRenderer.TGammaPanel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(31, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(19, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "0";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPaint);
			this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMouseDown);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(53, 33);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(19, 19);
			this.label2.TabIndex = 4;
			this.label2.Text = "1";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label2.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPaint);
			this.label2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMouseDown);
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
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.colorPicker1);
			this.MaximumSize = new System.Drawing.Size(300, 300);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "ChoosePalette";
			this.Text = "Palettes";
			this.ResumeLayout(false);

		}

		#endregion

		private TGammaPanel colorPicker1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;

	}
}