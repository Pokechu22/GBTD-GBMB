﻿namespace GB.Shared.Tiles
{
	partial class TileListEntry
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
			System.Windows.Forms.Panel background;
			this.textDisplay = new System.Windows.Forms.Panel();
			this.tileRenderer1 = new GB.Shared.Tiles.TileRenderer();
			background = new System.Windows.Forms.Panel();
			background.SuspendLayout();
			this.SuspendLayout();
			// 
			// background
			// 
			background.Controls.Add(this.textDisplay);
			background.Controls.Add(this.tileRenderer1);
			background.Dock = System.Windows.Forms.DockStyle.Fill;
			background.Location = new System.Drawing.Point(0, 0);
			background.Name = "background";
			background.Size = new System.Drawing.Size(38, 17);
			background.TabIndex = 2;
			// 
			// textDisplay
			// 
			this.textDisplay.BackColor = System.Drawing.SystemColors.Control;
			this.textDisplay.Location = new System.Drawing.Point(0, 0);
			this.textDisplay.Margin = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.textDisplay.Name = "textDisplay";
			this.textDisplay.Size = new System.Drawing.Size(21, 16);
			this.textDisplay.TabIndex = 4;
			this.textDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.textDisplay_Paint);
			// 
			// tileRenderer1
			// 
			this.tileRenderer1.BlackColor = System.Drawing.Color.Black;
			this.tileRenderer1.Border = false;
			this.tileRenderer1.BorderSides = ((System.Windows.Forms.Border3DSide)(((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
            | System.Windows.Forms.Border3DSide.Right) 
            | System.Windows.Forms.Border3DSide.Bottom) 
            | System.Windows.Forms.Border3DSide.Middle)));
			this.tileRenderer1.DarkGrayColor = System.Drawing.Color.Gray;
			this.tileRenderer1.Grid = false;
			this.tileRenderer1.LightGrayColor = System.Drawing.Color.LightGray;
			this.tileRenderer1.Location = new System.Drawing.Point(22, 0);
			this.tileRenderer1.Margin = new System.Windows.Forms.Padding(0);
			this.tileRenderer1.Name = "tileRenderer1";
			this.tileRenderer1.Size = new System.Drawing.Size(16, 16);
			this.tileRenderer1.TabIndex = 3;
			this.tileRenderer1.WhiteColor = System.Drawing.Color.White;
			// 
			// TileListEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(background);
			this.Name = "TileListEntry";
			this.Size = new System.Drawing.Size(38, 17);
			background.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TileRenderer tileRenderer1;
		private System.Windows.Forms.Panel textDisplay;



	}
}