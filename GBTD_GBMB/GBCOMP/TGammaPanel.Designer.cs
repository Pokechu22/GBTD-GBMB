namespace GBRenderer
{
	partial class TGammaPanel
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
			this.FGamma = new System.Windows.Forms.PictureBox();
			this.FImagePanel = new System.Windows.Forms.Panel();
			this.FStatePanel = new System.Windows.Forms.Panel();
			this.FViewWindow = new System.Windows.Forms.Label();
			this.REd = new System.Windows.Forms.TextBox();
			this.BEd = new System.Windows.Forms.TextBox();
			this.GEd = new System.Windows.Forms.TextBox();
			this.BLabel = new System.Windows.Forms.Label();
			this.GLabel = new System.Windows.Forms.Label();
			this.RLabel = new System.Windows.Forms.Label();
			this.FControlPanel = new System.Windows.Forms.Panel();
			this.FFirst = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.FGamma)).BeginInit();
			this.FStatePanel.SuspendLayout();
			this.FControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// FGamma
			// 
			this.FGamma.Image = global::GBRenderer.Properties.Resources.GAMMA;
			this.FGamma.Location = new System.Drawing.Point(4, 4);
			this.FGamma.Name = "FGamma";
			this.FGamma.Size = new System.Drawing.Size(45, 96);
			this.FGamma.TabIndex = 6;
			this.FGamma.TabStop = false;
			this.FGamma.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GammaClick);
			this.FGamma.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImgMouseMove);
			// 
			// FImagePanel
			// 
			this.FImagePanel.Location = new System.Drawing.Point(2, 2);
			this.FImagePanel.Name = "FImagePanel";
			this.FImagePanel.Size = new System.Drawing.Size(49, 100);
			this.FImagePanel.TabIndex = 5;
			this.FImagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FImagePanel_Paint);
			this.FImagePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AllMouseMove);
			// 
			// FStatePanel
			// 
			this.FStatePanel.Controls.Add(this.FViewWindow);
			this.FStatePanel.Controls.Add(this.REd);
			this.FStatePanel.Controls.Add(this.BEd);
			this.FStatePanel.Controls.Add(this.GEd);
			this.FStatePanel.Controls.Add(this.BLabel);
			this.FStatePanel.Controls.Add(this.GLabel);
			this.FStatePanel.Controls.Add(this.RLabel);
			this.FStatePanel.Location = new System.Drawing.Point(2, 136);
			this.FStatePanel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.FStatePanel.Name = "FStatePanel";
			this.FStatePanel.Size = new System.Drawing.Size(49, 92);
			this.FStatePanel.TabIndex = 1;
			this.FStatePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FStatePanel_Paint);
			// 
			// FViewWindow
			// 
			this.FViewWindow.Location = new System.Drawing.Point(3, 58);
			this.FViewWindow.Name = "FViewWindow";
			this.FViewWindow.Size = new System.Drawing.Size(42, 31);
			this.FViewWindow.TabIndex = 7;
			this.FViewWindow.Text = "None";
			this.FViewWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.FViewWindow.Paint += new System.Windows.Forms.PaintEventHandler(this.FViewWindow_Paint);
			// 
			// REd
			// 
			this.REd.Location = new System.Drawing.Point(24, 3);
			this.REd.MaxLength = 2;
			this.REd.Name = "REd";
			this.REd.Size = new System.Drawing.Size(18, 20);
			this.REd.TabIndex = 2;
			this.REd.TextChanged += new System.EventHandler(this.EdChange);
			this.REd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EdKeyUp);
			// 
			// BEd
			// 
			this.BEd.Location = new System.Drawing.Point(24, 39);
			this.BEd.Name = "BEd";
			this.BEd.Size = new System.Drawing.Size(18, 20);
			this.BEd.TabIndex = 4;
			this.BEd.TextChanged += new System.EventHandler(this.EdChange);
			this.BEd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EdKeyUp);
			// 
			// GEd
			// 
			this.GEd.Location = new System.Drawing.Point(24, 21);
			this.GEd.Name = "GEd";
			this.GEd.Size = new System.Drawing.Size(18, 20);
			this.GEd.TabIndex = 3;
			this.GEd.TextChanged += new System.EventHandler(this.EdChange);
			this.GEd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EdKeyUp);
			// 
			// BLabel
			// 
			this.BLabel.Location = new System.Drawing.Point(7, 42);
			this.BLabel.Name = "BLabel";
			this.BLabel.Size = new System.Drawing.Size(21, 11);
			this.BLabel.TabIndex = 6;
			this.BLabel.Text = "&B";
			// 
			// GLabel
			// 
			this.GLabel.Location = new System.Drawing.Point(7, 24);
			this.GLabel.Name = "GLabel";
			this.GLabel.Size = new System.Drawing.Size(21, 11);
			this.GLabel.TabIndex = 5;
			this.GLabel.Text = "&G";
			// 
			// RLabel
			// 
			this.RLabel.Location = new System.Drawing.Point(7, 6);
			this.RLabel.Name = "RLabel";
			this.RLabel.Size = new System.Drawing.Size(21, 11);
			this.RLabel.TabIndex = 1;
			this.RLabel.Text = "&R";
			// 
			// FControlPanel
			// 
			this.FControlPanel.Controls.Add(this.FFirst);
			this.FControlPanel.Location = new System.Drawing.Point(2, 104);
			this.FControlPanel.Name = "FControlPanel";
			this.FControlPanel.Size = new System.Drawing.Size(49, 30);
			this.FControlPanel.TabIndex = 7;
			this.FControlPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FControlPanel_Paint);
			// 
			// FFirst
			// 
			this.FFirst.Location = new System.Drawing.Point(1, 1);
			this.FFirst.Name = "FFirst";
			this.FFirst.Size = new System.Drawing.Size(47, 28);
			this.FFirst.TabIndex = 0;
			this.FFirst.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FirstMouseDown);
			this.FFirst.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FirstMouseMove);
			// 
			// TGammaPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.FControlPanel);
			this.Controls.Add(this.FGamma);
			this.Controls.Add(this.FImagePanel);
			this.Controls.Add(this.FStatePanel);
			this.MaximumSize = new System.Drawing.Size(53, 230);
			this.MinimumSize = new System.Drawing.Size(53, 230);
			this.Name = "TGammaPanel";
			this.Size = new System.Drawing.Size(53, 230);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorPicker_Paint);
			this.Resize += new System.EventHandler(this.TGammaPanel_Resize);
			this.StyleChanged += new System.EventHandler(this.TGammaPanel_StyleChanged);
			this.SystemColorsChanged += new System.EventHandler(this.TGammaPanel_SystemColorsChanged);
			((System.ComponentModel.ISupportInitialize)(this.FGamma)).EndInit();
			this.FStatePanel.ResumeLayout(false);
			this.FStatePanel.PerformLayout();
			this.FControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// Pannel surrounding entire RGB selectors and FViewWindow.
		/// 
		/// Defined on lines 187-202: 
		///   FStatePanel:= TPanel.Create(Self);
		///   FStatePanel.Parent:=Self;
		///   FStatePanel.SetBounds(2, 136, 49, 80+12);
		///   FStatePanel.Align :=alNone;
		///   FStatePanel.BevelInner:=bvLowered;
		///   FStatePanel.BevelOuter:=bvNone;
		///   FStatePanel.BevelWidth:=1;
		///   FStatePanel.BorderStyle:=bsNone;
		///   FStatePanel.BorderWidth:=0;
		///   FStatePanel.ParentColor:=True;
		///   FStatePanel.Color:=Color;
		///   FStatePanel.Caption:='';
		///   FStatePanel.Cursor:=crDefault;
		///   FStatePanel.PopupMenu:=PopupMenu;
		///   FStatePanel.Visible:=True;
		///   FStatePanel.Enabled := True;
		/// </summary>
		private System.Windows.Forms.Panel FStatePanel;
		/// <summary>
		/// Pannel surrounding the image itself, providing a border.
		/// 
		/// From lines 170 to 185: 
		///   FImagePanel:= TPanel.Create(Self);
		///   FImagePanel.Parent:=Self;
		///   FImagePanel.SetBounds(2, 2, 49, 100);
		///   FImagePanel.Align :=alNone;
		///   FImagePanel.BevelInner:=bvLowered;
		///   FImagePanel.BevelOuter:=bvNone;
		///   FImagePanel.BevelWidth:=1;
		///   FImagePanel.BorderStyle:=bsNone;
		///   FImagePanel.BorderWidth:=0;
		///   FImagePanel.Color:=Color;
		///   FImagePanel.Caption:='';
		///   FImagePanel.Cursor:=PipCursor;
		///   FImagePanel.PopupMenu:=PopupMenu;
		///   FImagePanel.Visible:=True;
		///   FImagePanel.Enabled := True;
		///   FImagePanel.OnMouseMove :=AllMouseMove;
		/// </summary>
		private System.Windows.Forms.Panel FImagePanel;
		/// <summary>
		/// The main image which contains all the needed colors.  
		/// 
		/// The main definition is on lines 363-377:
		///   FGamma:= TImage.Create(Self);
		///   with FGamma do
		///   begin
		///     SetBounds(0, 0 , 48, 98);
		///     Parent:=FImagePanel;
		///     Align:=alClient;
		///     Stretch:=False;
		///     AutoSize:=True;
		///     Center:=True;
		///     Cursor:=PipCursor;
		///     Visible:=True;
		///     Enabled := True;
		///     OnClick:= GammaClick;
		///     OnMouseMove :=ImgMouseMove;
		///   end;
		/// 
		/// The bitmap used is defined on lines 148-149: 
		///   FBmpSelector := TBitmap.Create;
		///   FBmpSelector.LoadFromResourceName(HInstance,'GAMMA');
		/// </summary>
		private System.Windows.Forms.PictureBox FGamma;
		/// <summary>
		/// Contains the currently selected color, in between the color chooser and FStatePanel.
		/// 
		/// Defined on lines 204-219:
		///   FControlPanel:= TPanel.Create(Self);
		///   FControlPanel.Parent:=Self;
		///   FControlPanel.SetBounds(2, 104, 49, 30);
		///   FControlPanel.Align :=alNone;
		///   FControlPanel.BevelInner:=bvLowered;
		///   FControlPanel.BevelOuter:=bvNone;//bvRaised;
		///   FControlPanel.BevelWidth:=1;
		///   FControlPanel.BorderStyle:=bsNone;
		///   FControlPanel.BorderWidth:=0;
		///   FControlPanel.ParentColor:=True;
		///   FControlPanel.Color:=Color;
		///   FControlPanel.Caption:='';
		///   FControlPanel.Cursor:=crDefault;
		///   FControlPanel.PopupMenu:=PopupMenu;
		///   FControlPanel.Visible:=True;
		///   FControlPanel.Enabled := True;
		/// 
		/// </summary>
		private System.Windows.Forms.Panel FControlPanel;
		/// <summary>
		/// Label for red.  
		/// 
		/// Defined on lines 257-274
		///   RLabel:= TLabel.Create(Self);
		///   with RLabel do
		///   begin
		///     SetBounds(7, 3+3, 21, 11);
		///     Parent:=FStatePanel;
		///     Align :=alNone;
		///     Alignment:=taLeftJustify;
		///     AutoSize:=True;
		///     ParentColor:=True;
		///     FocusControl := REd;
		///     Color:=Color;
		///     Font:=Font;
		///     Caption:='&R';
		///     Cursor:=crDefault;
		///     Visible:=True;
		///     Enabled := True;
		/// 
		///   end;
		/// </summary>
		private System.Windows.Forms.Label RLabel;
		/// <summary>
		/// Label for green.
		/// 
		/// Lines 295-311:
		///   GLabel:= TLabel.Create(Self);
		///   with GLabel do
		///   begin
		///     SetBounds(7, 21+3, 21, 11);
		///     Parent:=FStatePanel;
		///     Align :=alNone;
		///     Alignment:=taLeftJustify;
		///     AutoSize:=True;
		///     ParentColor:=True;
		///     FocusControl:=GEd;
		///     Color:=Color;
		///     Font:=Font;
		///     Caption:='&G';
		///     Cursor:=crDefault;
		///     Visible:=True;
		///     Enabled := True;
		///   end;
		/// </summary>
		private System.Windows.Forms.Label GLabel;
		/// <summary>
		/// Label for blue.
		/// 
		/// Defined on lines 333-349:
		///   BLabel:= TLabel.Create(Self);
		///   with BLabel do
		///   begin
		///     SetBounds(7, 39+3, 21, 11);
		///     Parent:=FStatePanel;
		///     Align :=alNone;
		///     Alignment:=taLeftJustify;
		///     AutoSize:=True;
		///     ParentColor:=True;
		///     FocusControl:=BEd;
		///     Color:=Color;
		///     Font:=Font;
		///     Caption:='&B';
		///     Cursor:=crDefault;
		///     Visible:=True;
		///     Enabled := True;
		///   end;
		/// </summary>
		private System.Windows.Forms.Label BLabel;
		/// <summary>
		/// The icon for the currently-selected color.
		/// 
		/// Defined on lines 351-361:
		///   FFirst:= TShape.Create(Self);
		///   with FFirst do
		///   begin
		///     Parent:=FControlPanel;
		///     SetBounds(1, 1, 47, 28);
		///     SetFirstControls(FFirstColor);
		///     Visible:=True;
		///     Enabled := True;
		///     OnMouseDown := FirstMouseDown;
		///     OnMouseMove := FirstMouseMove;
		///   end;
		/// </summary>
		private System.Windows.Forms.Panel FFirst;
		private System.Windows.Forms.Label FViewWindow;
		private System.Windows.Forms.TextBox REd;
		private System.Windows.Forms.TextBox BEd;
		private System.Windows.Forms.TextBox GEd;
	}
}
