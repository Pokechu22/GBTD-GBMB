namespace GB.GBTD
{
	partial class ToolList
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
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.radioButton2 = new GB.GBTD.ToolList.ToolListRadioButton();
			this.radioButton1 = new GB.GBTD.ToolList.ToolListRadioButton();
			this.SuspendLayout();
			// 
			// radioButton2
			// 
			this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.radioButton2.HoveredImage = global::GB.GBTD.Properties.Resources.Flood_Hover;
			this.radioButton2.Location = new System.Drawing.Point(4, 36);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.NonhoveredImage = global::GB.GBTD.Properties.Resources.Flood_NoHover;
			this.radioButton2.SelectedBackgroundImage = global::GB.GBTD.Properties.Resources.SelectionBackground;
			this.radioButton2.Size = new System.Drawing.Size(23, 24);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatAppearance.BorderSize = 0;
			this.radioButton1.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.radioButton1.HoveredImage = global::GB.GBTD.Properties.Resources.Pen_Hover;
			this.radioButton1.Location = new System.Drawing.Point(4, 6);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.NonhoveredImage = global::GB.GBTD.Properties.Resources.Pen_NoHover;
			this.radioButton1.SelectedBackgroundImage = global::GB.GBTD.Properties.Resources.SelectionBackground;
			this.radioButton1.Size = new System.Drawing.Size(23, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// ToolList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.MaximumSize = new System.Drawing.Size(27, 217);
			this.MinimumSize = new System.Drawing.Size(27, 217);
			this.Name = "ToolList";
			this.Size = new System.Drawing.Size(27, 217);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.paintBorder);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private ToolListRadioButton radioButton1;
		private ToolListRadioButton radioButton2;
	}
}
