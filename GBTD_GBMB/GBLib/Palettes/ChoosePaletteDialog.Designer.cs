using GB.Shared.Palettes;
namespace GB.Shared.Palettes
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
			this.filterCheckBox = new System.Windows.Forms.CheckBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.groupBox1 = new GB.Shared.Controls.GroupBox();
			this.gbPaletteSetSelector1 = new GB.Shared.Palettes.GBPaletteSetSelector();
			this.colorPicker1 = new GB.Shared.Palettes.TGammaPanel();
			this.copyAllButton = new System.Windows.Forms.Button();
			this.copyButton = new System.Windows.Forms.Button();
			this.pasteButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// filterCheckBox
			// 
			this.filterCheckBox.AutoSize = true;
			this.filterCheckBox.Location = new System.Drawing.Point(165, -1);
			this.filterCheckBox.Name = "filterCheckBox";
			this.filterCheckBox.Size = new System.Drawing.Size(79, 17);
			this.filterCheckBox.TabIndex = 2;
			this.filterCheckBox.Text = "Filter colors";
			this.filterCheckBox.UseVisualStyleBackColor = true;
			this.filterCheckBox.CheckedChanged += new System.EventHandler(this.filterCheckBox_CheckedChanged);
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(17, 297);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(97, 297);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(177, 297);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 6;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.pasteButton);
			this.groupBox1.Controls.Add(this.copyButton);
			this.groupBox1.Controls.Add(this.copyAllButton);
			this.groupBox1.Controls.Add(this.colorPicker1);
			this.groupBox1.Controls.Add(this.gbPaletteSetSelector1);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(249, 280);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.Text = "Gameboy Color palettes";
			// 
			// gbPaletteSetSelector1
			// 
			this.gbPaletteSetSelector1.DefaultBlackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.gbPaletteSetSelector1.DefaultDarkGrayColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
			this.gbPaletteSetSelector1.DefaultLightGrayColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.gbPaletteSetSelector1.DefaultWhiteColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.gbPaletteSetSelector1.GBCFilter = false;
			this.gbPaletteSetSelector1.Location = new System.Drawing.Point(16, 19);
			this.gbPaletteSetSelector1.Name = "gbPaletteSetSelector1";
			this.gbPaletteSetSelector1.SelectedX = -1;
			this.gbPaletteSetSelector1.SelectedY = -1;
			this.gbPaletteSetSelector1.Size = new System.Drawing.Size(96, 216);
			this.gbPaletteSetSelector1.TabIndex = 1;
			this.gbPaletteSetSelector1.SelectionChanged += new System.EventHandler(this.gbPaletteSetSelector1_SelectionChanged);
			// 
			// colorPicker1
			// 
			this.colorPicker1.Enabled = false;
			this.colorPicker1.FirstColor = System.Drawing.Color.Black;
			this.colorPicker1.GBCFilter = false;
			this.colorPicker1.Location = new System.Drawing.Point(179, 19);
			this.colorPicker1.MaximumSize = new System.Drawing.Size(53, 230);
			this.colorPicker1.MinimumSize = new System.Drawing.Size(53, 230);
			this.colorPicker1.Name = "colorPicker1";
			this.colorPicker1.Size = new System.Drawing.Size(53, 230);
			this.colorPicker1.TabIndex = 0;
			this.colorPicker1.OnChange += new System.EventHandler(this.colorPicker1_OnChange);
			// 
			// copyAllButton
			// 
			this.copyAllButton.Location = new System.Drawing.Point(32, 254);
			this.copyAllButton.Name = "copyAllButton";
			this.copyAllButton.Size = new System.Drawing.Size(57, 19);
			this.copyAllButton.TabIndex = 4;
			this.copyAllButton.Text = "Copy &all";
			this.copyAllButton.UseVisualStyleBackColor = true;
			this.copyAllButton.Click += new System.EventHandler(this.copyAllButton_Click);
			// 
			// copyButton
			// 
			this.copyButton.Location = new System.Drawing.Point(96, 254);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(57, 19);
			this.copyButton.TabIndex = 5;
			this.copyButton.Text = "&Copy";
			this.copyButton.UseVisualStyleBackColor = true;
			this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
			// 
			// pasteButton
			// 
			this.pasteButton.Location = new System.Drawing.Point(160, 254);
			this.pasteButton.Name = "pasteButton";
			this.pasteButton.Size = new System.Drawing.Size(57, 19);
			this.pasteButton.TabIndex = 6;
			this.pasteButton.Text = "&Paste";
			this.pasteButton.UseVisualStyleBackColor = true;
			this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
			// 
			// ChoosePalette
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(265, 329);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.filterCheckBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(271, 354);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(271, 354);
			this.Name = "ChoosePalette";
			this.Text = "Palettes";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox filterCheckBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private TGammaPanel colorPicker1;
		private GBPaletteSetSelector gbPaletteSetSelector1;
		private Controls.GroupBox groupBox1;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Button pasteButton;
		private System.Windows.Forms.Button copyButton;
		private System.Windows.Forms.Button copyAllButton;

	}
}