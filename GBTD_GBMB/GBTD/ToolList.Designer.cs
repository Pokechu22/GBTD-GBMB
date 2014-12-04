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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolList));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.radioButton2 = new GB.GBTD.ToolList.ToolListRadioButton();
			this.radioButton1 = new GB.GBTD.ToolList.ToolListRadioButton();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(2, 49);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(22, 82);
			this.panel1.TabIndex = 2;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.paintIndention);
			// 
			// panel2
			// 
			this.panel2.Location = new System.Drawing.Point(2, 134);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(22, 62);
			this.panel2.TabIndex = 3;
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.paintIndention);
			// 
			// radioButton2
			// 
			this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton2.BackgroundImage")));
			this.radioButton2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.radioButton2.FlatAppearance.BorderSize = 0;
			this.radioButton2.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.radioButton2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.radioButton2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.radioButton2.HoveredImage = global::GB.GBTD.Properties.Resources.Flood_Hover;
			this.radioButton2.Image = global::GB.GBTD.Properties.Resources.Flood_NoHover;
			this.radioButton2.Location = new System.Drawing.Point(2, 24);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.NonhoveredImage = global::GB.GBTD.Properties.Resources.Flood_NoHover;
			this.radioButton2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.radioButton2.PressedImage = global::GB.GBTD.Properties.Resources.Flood_Pressed;
			this.radioButton2.SelectedBackgroundImage = global::GB.GBTD.Properties.Resources.SelectionBackground;
			this.radioButton2.Size = new System.Drawing.Size(22, 22);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("radioButton1.BackgroundImage")));
			this.radioButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatAppearance.BorderSize = 0;
			this.radioButton1.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
			this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.radioButton1.HoveredImage = global::GB.GBTD.Properties.Resources.Pen_Hover;
			this.radioButton1.Image = global::GB.GBTD.Properties.Resources.Pen_NoHover;
			this.radioButton1.Location = new System.Drawing.Point(2, 2);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.NonhoveredImage = global::GB.GBTD.Properties.Resources.Pen_NoHover;
			this.radioButton1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.radioButton1.PressedImage = global::GB.GBTD.Properties.Resources.Pen_Pressed;
			this.radioButton1.SelectedBackgroundImage = global::GB.GBTD.Properties.Resources.SelectionBackground;
			this.radioButton1.Size = new System.Drawing.Size(22, 22);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// ToolList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.MaximumSize = new System.Drawing.Size(26, 217);
			this.MinimumSize = new System.Drawing.Size(26, 217);
			this.Name = "ToolList";
			this.Size = new System.Drawing.Size(26, 217);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.paintBorder);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private ToolList.ToolListRadioButton radioButton2;
		private ToolList.ToolListRadioButton radioButton1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
	}
}
