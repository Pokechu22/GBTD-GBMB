namespace GB.Shared.Controls
{
	partial class TileListEntryControl
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
			this.background = new System.Windows.Forms.Panel();
			this.textDisplay = new System.Windows.Forms.Panel();
			this.tileRenderer1 = new GB.Shared.Controls.TileRenderer();
			this.background.SuspendLayout();
			this.SuspendLayout();
			// 
			// background
			// 
			this.background.Controls.Add(this.textDisplay);
			this.background.Controls.Add(this.tileRenderer1);
			this.background.Dock = System.Windows.Forms.DockStyle.Fill;
			this.background.Location = new System.Drawing.Point(0, 0);
			this.background.Name = "background";
			this.background.Size = new System.Drawing.Size(38, 17);
			this.background.TabIndex = 2;
			this.background.Click += new System.EventHandler(this.subcontrol_Click);
			// 
			// textDisplay
			// 
			this.textDisplay.BackColor = System.Drawing.SystemColors.Control;
			this.textDisplay.Location = new System.Drawing.Point(0, 0);
			this.textDisplay.Margin = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.textDisplay.Name = "textDisplay";
			this.textDisplay.Size = new System.Drawing.Size(21, 16);
			this.textDisplay.TabIndex = 4;
			this.textDisplay.Click += new System.EventHandler(this.subcontrol_Click);
			this.textDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.textDisplay_Paint);
			// 
			// tileRenderer1
			// 
			this.tileRenderer1.Border = false;
			this.tileRenderer1.BorderSides = ((System.Windows.Forms.Border3DSide)(((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom) 
            | System.Windows.Forms.Border3DSide.Middle)));
			this.tileRenderer1.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_POCKET;
			this.tileRenderer1.Grid = false;
			this.tileRenderer1.Location = new System.Drawing.Point(22, 0);
			this.tileRenderer1.Margin = new System.Windows.Forms.Padding(0);
			this.tileRenderer1.Name = "tileRenderer1";
			this.tileRenderer1.NibbleMarkers = false;
			this.tileRenderer1.PixelScale = 2;
			this.tileRenderer1.Selected = false;
			this.tileRenderer1.Size = new System.Drawing.Size(16, 16);
			this.tileRenderer1.TabIndex = 3;
			this.tileRenderer1.Click += new System.EventHandler(this.subcontrol_Click);
			// 
			// TileListEntryControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(this.background);
			this.Name = "TileListEntryControl";
			this.Size = new System.Drawing.Size(38, 17);
			this.background.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TileRenderer tileRenderer1;
		private System.Windows.Forms.Panel textDisplay;
		private System.Windows.Forms.Panel background;



	}
}
