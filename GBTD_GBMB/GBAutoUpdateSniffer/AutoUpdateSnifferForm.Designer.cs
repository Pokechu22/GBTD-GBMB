namespace GBAutoUpdateSniffer
{
	partial class AutoUpdateSnifferForm
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
			this.auListener1 = new GB.Shared.AutoUpdate.AUListener();
			this.SuspendLayout();
			// 
			// auListener1
			// 
			this.auListener1.Location = new System.Drawing.Point(135, 116);
			this.auListener1.Name = "auListener1";
			this.auListener1.Size = new System.Drawing.Size(75, 23);
			this.auListener1.TabIndex = 0;
			this.auListener1.Text = "auListener1";
			this.auListener1.Click += new System.EventHandler(this.auListener1_Click);
			// 
			// AutoUpdateSnifferForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.auListener1);
			this.Name = "AutoUpdateSnifferForm";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private GB.Shared.AutoUpdate.AUListener auListener1;
	}
}

