namespace GB.GBTD.Dialogs
{
	partial class PalettesDialog
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.groupBox = new GB.Shared.Controls.GroupBox();
			this.copyAllButton = new System.Windows.Forms.Button();
			this.copyButton = new System.Windows.Forms.Button();
			this.pasteButton = new System.Windows.Forms.Button();
			this.colorPicker = new GB.GBTD.Dialogs.ColorPicker();
			this.groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(17, 297);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(97, 297);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(177, 297);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 25);
			this.helpButton.TabIndex = 2;
			this.helpButton.Text = "&Help";
			this.helpButton.UseVisualStyleBackColor = true;
			// 
			// groupBox
			// 
			this.groupBox.Controls.Add(this.pasteButton);
			this.groupBox.Controls.Add(this.copyButton);
			this.groupBox.Controls.Add(this.copyAllButton);
			this.groupBox.Controls.Add(this.colorPicker);
			this.groupBox.Location = new System.Drawing.Point(8, 8);
			this.groupBox.Name = "groupBox";
			this.groupBox.Size = new System.Drawing.Size(249, 280);
			this.groupBox.TabIndex = 3;
			this.groupBox.Text = "??? palettes";
			// 
			// copyAllButton
			// 
			this.copyAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.copyAllButton.Location = new System.Drawing.Point(32, 254);
			this.copyAllButton.Name = "copyAllButton";
			this.copyAllButton.Size = new System.Drawing.Size(57, 19);
			this.copyAllButton.TabIndex = 5;
			this.copyAllButton.Text = "Copy &all";
			this.copyAllButton.UseVisualStyleBackColor = true;
			this.copyAllButton.Click += new System.EventHandler(this.copyAllButton_Click);
			// 
			// copyButton
			// 
			this.copyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.copyButton.Location = new System.Drawing.Point(96, 254);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(57, 19);
			this.copyButton.TabIndex = 6;
			this.copyButton.Text = "&Copy";
			this.copyButton.UseVisualStyleBackColor = true;
			this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
			// 
			// pasteButton
			// 
			this.pasteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pasteButton.Location = new System.Drawing.Point(160, 254);
			this.pasteButton.Name = "pasteButton";
			this.pasteButton.Size = new System.Drawing.Size(57, 19);
			this.pasteButton.TabIndex = 7;
			this.pasteButton.Text = "&Paste";
			this.pasteButton.UseVisualStyleBackColor = true;
			this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
			// 
			// colorPicker
			// 
			this.colorPicker.ColorSet = GB.Shared.Palettes.ColorSet.GAMEBOY_POCKET;
			this.colorPicker.DisplayImage = global::GB.GBTD.Properties.Resources.GAMMA;
			this.colorPicker.Location = new System.Drawing.Point(179, 19);
			this.colorPicker.Name = "colorPicker";
			this.colorPicker.PixelImage = global::GB.GBTD.Properties.Resources.GAMMA;
			this.colorPicker.SelectedColor = System.Drawing.Color.Empty;
			this.colorPicker.Size = new System.Drawing.Size(53, 230);
			this.colorPicker.TabIndex = 4;
			this.colorPicker.SelectedColorChanged += new System.EventHandler(this.colorPicker_SelectedColorChanged);
			// 
			// PalettesDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(265, 329);
			this.Controls.Add(this.groupBox);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PalettesDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Palettes";
			this.groupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button helpButton;
		private Shared.Controls.GroupBox groupBox;
		private Dialogs.ColorPicker colorPicker;
		private System.Windows.Forms.Button pasteButton;
		private System.Windows.Forms.Button copyButton;
		private System.Windows.Forms.Button copyAllButton;
	}
}