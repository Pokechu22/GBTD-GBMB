using GB.Shared.Palette;
namespace GB.Shared.Palette
{
	partial class ChoosePalette<TSelector, TSet, TRow, TEntry>
		where TSelector : GBPaletteSetSelector<TSet, TRow, TEntry>, new()
		where TSet : PaletteSetBase<TRow, TEntry>, new()
		where TRow : PaletteBase<TEntry>
		where TEntry : PaletteEntryBase
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
			TSet set = new TSet();
			this.gbPaletteSetSelector1 = new TSelector();

			this.colorPicker1 = new GB.Shared.Palette.TGammaPanel();
			this.filterCheckBox = new System.Windows.Forms.CheckBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// gbPaletteSetSelector1
			// 
			this.gbPaletteSetSelector1.DefaultBlackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.gbPaletteSetSelector1.DefaultDarkGrayColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(144)))), ((int)(((byte)(144)))));
			this.gbPaletteSetSelector1.DefaultLightGrayColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.gbPaletteSetSelector1.DefaultWhiteColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.gbPaletteSetSelector1.GBCFilter = false;
			this.gbPaletteSetSelector1.Location = new System.Drawing.Point(12, 12);
			this.gbPaletteSetSelector1.Name = "gbPaletteSetSelector1";
			this.gbPaletteSetSelector1.SelectedX = -1;
			this.gbPaletteSetSelector1.SelectedY = -1;
			this.gbPaletteSetSelector1.Set = set;
			this.gbPaletteSetSelector1.Size = new System.Drawing.Size(112, 262);
			this.gbPaletteSetSelector1.TabIndex = 1;
			this.gbPaletteSetSelector1.SelectionChanged += new System.EventHandler(this.gbPaletteSetSelector1_SelectionChanged);
			// 
			// colorPicker1
			// 
			this.colorPicker1.Enabled = false;
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
			// filterCheckBox
			// 
			this.filterCheckBox.AutoSize = true;
			this.filterCheckBox.Location = new System.Drawing.Point(141, 32);
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
			this.okButton.Location = new System.Drawing.Point(141, 174);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(141, 203);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// ChoosePalette
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.filterCheckBox);
			this.Controls.Add(this.gbPaletteSetSelector1);
			this.Controls.Add(this.colorPicker1);
			this.MaximumSize = new System.Drawing.Size(300, 300);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "ChoosePalette";
			this.Text = "Palettes";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GB.Shared.Palette.TGammaPanel colorPicker1;
		private TSelector gbPaletteSetSelector1;
		private System.Windows.Forms.CheckBox filterCheckBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;

	}
}