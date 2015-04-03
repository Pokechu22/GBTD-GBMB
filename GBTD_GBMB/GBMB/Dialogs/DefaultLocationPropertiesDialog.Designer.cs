﻿namespace GB.GBMB.Dialogs
{
	partial class DefaultLocationPropertiesDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultLocationPropertiesDialog));
			this.okButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.tileListBorder = new GB.Shared.Controls.Border();
			this.tileList = new GB.GBMB.TileList();
			this.tileGroupBox = new GB.Shared.Controls.GroupBox();
			this.defaultLocationPropertiesEditControl1 = new GB.GBMB.Dialogs.DefaultLocationPropertiesEditControl();
			this.tileGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(133, 210);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(212, 210);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 1;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// tileListBorder
			// 
			this.tileListBorder.BottomBorder = null;
			this.tileListBorder.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Right};
			this.tileListBorder.LeftBorder = null;
			this.tileListBorder.Location = new System.Drawing.Point(61, 13);
			this.tileListBorder.Name = "tileListBorder";
			this.tileListBorder.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.tileListBorder.Size = new System.Drawing.Size(1, 188);
			this.tileListBorder.TabIndex = 4;
			this.tileListBorder.TopBorder = null;
			// 
			// tileList
			// 
			this.tileList.Bookmark1Icon = ((System.Drawing.Image)(resources.GetObject("tileList.Bookmark1Icon")));
			this.tileList.Bookmark2Icon = ((System.Drawing.Image)(resources.GetObject("tileList.Bookmark2Icon")));
			this.tileList.Bookmark3Icon = ((System.Drawing.Image)(resources.GetObject("tileList.Bookmark3Icon")));
			this.tileList.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_POCKET;
			this.tileList.Location = new System.Drawing.Point(5, 13);
			this.tileList.Name = "tileList";
			this.tileList.PaletteData = null;
			this.tileList.PaletteMapping = null;
			this.tileList.SelectedTile = ((ushort)(0));
			this.tileList.Size = new System.Drawing.Size(56, 188);
			this.tileList.TabIndex = 3;
			this.tileList.TileSet = null;
			// 
			// tileGroupBox
			// 
			this.tileGroupBox.Controls.Add(this.defaultLocationPropertiesEditControl1);
			this.tileGroupBox.Location = new System.Drawing.Point(70, 8);
			this.tileGroupBox.Name = "tileGroupBox";
			this.tileGroupBox.Size = new System.Drawing.Size(217, 193);
			this.tileGroupBox.TabIndex = 2;
			this.tileGroupBox.Text = "Tile 0";
			// 
			// defaultLocationPropertiesEditControl1
			// 
			this.defaultLocationPropertiesEditControl1.BackColor = System.Drawing.Color.White;
			this.defaultLocationPropertiesEditControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.defaultLocationPropertiesEditControl1.Location = new System.Drawing.Point(8, 16);
			this.defaultLocationPropertiesEditControl1.Name = "defaultLocationPropertiesEditControl1";
			this.defaultLocationPropertiesEditControl1.Size = new System.Drawing.Size(201, 169);
			this.defaultLocationPropertiesEditControl1.TabIndex = 4;
			// 
			// DefaultLocationPropertiesDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(294, 242);
			this.Controls.Add(this.tileListBorder);
			this.Controls.Add(this.tileList);
			this.Controls.Add(this.tileGroupBox);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DefaultLocationPropertiesDialog";
			this.Text = "Default location properties";
			this.tileGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Button okButton;
		private Shared.Controls.GroupBox tileGroupBox;
		private TileList tileList;
		private Shared.Controls.Border tileListBorder;
		private DefaultLocationPropertiesEditControl defaultLocationPropertiesEditControl1;
	}
}