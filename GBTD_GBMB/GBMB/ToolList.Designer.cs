namespace GB.GBMB
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
			GB.Shared.Controls.Border border;
			this.autoUpdateCheckBox = new GB.Shared.Controls.ImageCheckBox();
			this.removeColButton = new GB.Shared.Controls.ImageButton();
			this.removeRowButton = new GB.Shared.Controls.ImageButton();
			this.addColButton = new GB.Shared.Controls.ImageButton();
			this.addRowButton = new GB.Shared.Controls.ImageButton();
			this.dropperRadioButton = new GB.Shared.Controls.ImageRadioButton();
			this.floodRadioButton = new GB.Shared.Controls.ImageRadioButton();
			this.penRadioButton = new GB.Shared.Controls.ImageRadioButton();
			this.borderInner = new GB.Shared.Controls.Border();
			border = new GB.Shared.Controls.Border();
			this.SuspendLayout();
			// 
			// border
			// 
			border.BottomBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			border.Dock = System.Windows.Forms.DockStyle.Fill;
			border.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Left,
        System.Windows.Forms.Border3DSide.Bottom};
			border.LeftBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			border.Location = new System.Drawing.Point(0, 0);
			border.Name = "border";
			border.RightBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			border.Size = new System.Drawing.Size(26, 174);
			border.TabIndex = 0;
			border.Text = "border1";
			border.TopBorder = System.Windows.Forms.Border3DStyle.RaisedInner;
			// 
			// autoUpdateCheckBox
			// 
			this.autoUpdateCheckBox.Checked = false;
			this.autoUpdateCheckBox.HoveredImage = global::GB.GBMB.Properties.Resources.AutoUpdate_selected;
			this.autoUpdateCheckBox.Location = new System.Drawing.Point(2, 155);
			this.autoUpdateCheckBox.Name = "autoUpdateCheckBox";
			this.autoUpdateCheckBox.NonhoveredImage = global::GB.GBMB.Properties.Resources.AutoUpdate_nonselected;
			this.autoUpdateCheckBox.Size = new System.Drawing.Size(22, 17);
			this.autoUpdateCheckBox.TabIndex = 9;
			this.autoUpdateCheckBox.CheckedChanged += new System.EventHandler(this.OnAutoUpdateChanged);
			// 
			// removeColButton
			// 
			this.removeColButton.HoveredImage = global::GB.GBMB.Properties.Resources.RemoveCol_selected;
			this.removeColButton.Location = new System.Drawing.Point(3, 132);
			this.removeColButton.Name = "removeColButton";
			this.removeColButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.RemoveCol_nonselected;
			this.removeColButton.Size = new System.Drawing.Size(20, 20);
			this.removeColButton.TabIndex = 8;
			this.removeColButton.Click += new System.EventHandler(this.OnRemoveColumnClicked);
			// 
			// removeRowButton
			// 
			this.removeRowButton.HoveredImage = global::GB.GBMB.Properties.Resources.RemoveRow_selected;
			this.removeRowButton.Location = new System.Drawing.Point(3, 112);
			this.removeRowButton.Name = "removeRowButton";
			this.removeRowButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.RemoveRow_nonselected;
			this.removeRowButton.Size = new System.Drawing.Size(20, 20);
			this.removeRowButton.TabIndex = 7;
			this.removeRowButton.Click += new System.EventHandler(this.OnRemoveRowClicked);
			// 
			// addColButton
			// 
			this.addColButton.HoveredImage = global::GB.GBMB.Properties.Resources.AddCol_selected;
			this.addColButton.Location = new System.Drawing.Point(3, 92);
			this.addColButton.Name = "addColButton";
			this.addColButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.AddCol_nonselected;
			this.addColButton.Size = new System.Drawing.Size(20, 20);
			this.addColButton.TabIndex = 6;
			this.addColButton.Click += new System.EventHandler(this.OnAddColumnClicked);
			// 
			// addRowButton
			// 
			this.addRowButton.HoveredImage = global::GB.GBMB.Properties.Resources.AddRow_selected;
			this.addRowButton.Location = new System.Drawing.Point(3, 72);
			this.addRowButton.Name = "addRowButton";
			this.addRowButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.AddRow_nonselected;
			this.addRowButton.Size = new System.Drawing.Size(20, 20);
			this.addRowButton.TabIndex = 5;
			this.addRowButton.Click += new System.EventHandler(this.OnAddRowClicked);
			// 
			// dropperRadioButton
			// 
			this.dropperRadioButton.Checked = false;
			this.dropperRadioButton.HoveredImage = global::GB.GBMB.Properties.Resources.Dropper_selected;
			this.dropperRadioButton.Location = new System.Drawing.Point(2, 46);
			this.dropperRadioButton.Name = "dropperRadioButton";
			this.dropperRadioButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.Dropper_nonselected;
			this.dropperRadioButton.Size = new System.Drawing.Size(22, 22);
			this.dropperRadioButton.TabIndex = 4;
			this.dropperRadioButton.CheckedChanged += new System.EventHandler(this.OnSelectedToolChanged);
			// 
			// floodRadioButton
			// 
			this.floodRadioButton.Checked = false;
			this.floodRadioButton.HoveredImage = global::GB.GBMB.Properties.Resources.Flood_selected;
			this.floodRadioButton.Location = new System.Drawing.Point(2, 24);
			this.floodRadioButton.Name = "floodRadioButton";
			this.floodRadioButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.Flood_nonselected;
			this.floodRadioButton.Size = new System.Drawing.Size(22, 22);
			this.floodRadioButton.TabIndex = 3;
			this.floodRadioButton.CheckedChanged += new System.EventHandler(this.OnSelectedToolChanged);
			// 
			// penRadioButton
			// 
			this.penRadioButton.Checked = false;
			this.penRadioButton.HoveredImage = global::GB.GBMB.Properties.Resources.Pen_selected;
			this.penRadioButton.Location = new System.Drawing.Point(2, 2);
			this.penRadioButton.Name = "penRadioButton";
			this.penRadioButton.NonhoveredImage = global::GB.GBMB.Properties.Resources.Pen_nonselected;
			this.penRadioButton.Size = new System.Drawing.Size(22, 22);
			this.penRadioButton.TabIndex = 2;
			this.penRadioButton.CheckedChanged += new System.EventHandler(this.OnSelectedToolChanged);
			// 
			// borderInner
			// 
			this.borderInner.BottomBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.borderInner.DrawOrder = new System.Windows.Forms.Border3DSide[] {
        System.Windows.Forms.Border3DSide.Top,
        System.Windows.Forms.Border3DSide.Right,
        System.Windows.Forms.Border3DSide.Bottom,
        System.Windows.Forms.Border3DSide.Left};
			this.borderInner.Enabled = false;
			this.borderInner.LeftBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.borderInner.Location = new System.Drawing.Point(2, 71);
			this.borderInner.Name = "borderInner";
			this.borderInner.RightBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.borderInner.Size = new System.Drawing.Size(22, 82);
			this.borderInner.TabIndex = 1;
			this.borderInner.Text = "border1";
			this.borderInner.TopBorder = System.Windows.Forms.Border3DStyle.SunkenOuter;
			// 
			// ToolList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.autoUpdateCheckBox);
			this.Controls.Add(this.removeColButton);
			this.Controls.Add(this.removeRowButton);
			this.Controls.Add(this.addColButton);
			this.Controls.Add(this.addRowButton);
			this.Controls.Add(this.dropperRadioButton);
			this.Controls.Add(this.floodRadioButton);
			this.Controls.Add(this.penRadioButton);
			this.Controls.Add(this.borderInner);
			this.Controls.Add(border);
			this.MaximumSize = new System.Drawing.Size(26, 174);
			this.MinimumSize = new System.Drawing.Size(26, 174);
			this.Name = "ToolList";
			this.Size = new System.Drawing.Size(26, 174);
			this.ResumeLayout(false);

		}

		#endregion

		private Shared.Controls.Border borderInner;
		private Shared.Controls.ImageRadioButton penRadioButton;
		private Shared.Controls.ImageRadioButton floodRadioButton;
		private Shared.Controls.ImageRadioButton dropperRadioButton;
		private Shared.Controls.ImageButton addRowButton;
		private Shared.Controls.ImageButton addColButton;
		private Shared.Controls.ImageButton removeRowButton;
		private Shared.Controls.ImageButton removeColButton;
		private Shared.Controls.ImageCheckBox autoUpdateCheckBox;

	}
}
