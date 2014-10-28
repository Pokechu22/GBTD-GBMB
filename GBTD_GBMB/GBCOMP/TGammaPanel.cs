using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBRenderer
{
	/// <summary>
	/// Based off of TGammaPanel, from the origional source, written in delfi, by TRSOFT.  See http://www.abcnet.de/TRSOFT/
	/// Ported to C# by Pokechu22.
	/// 
	/// 
    /// Copyright © 1998 by TRSOFT  All Rights Reserved.
    /// Thomas Radtke Software Entwicklung.
    /// http://www.abcnet.de/TRSOFT/
	/// </summary>
	public partial class TGammaPanel : UserControl
	{
		/// Feilds which are the same as the private values of the origional code.
		/// These won't usually be directly accessed.
		#region Private Fields

		/// <summary>Full width of control; cannot be changed.</summary>
		private const int FULLWIDTH = 53;
		/// <summary>Full width of control; cannot be changed.</summary>
		private const int FULLHEIGHT = 230;

		/// <summary>
		/// The main image.
		/// 
		/// Declared line 66: 
		///      FBmpSelector   : TBitmap;
		///      
		/// Defined lines 148-149: 
		///   FBmpSelector := TBitmap.Create;
		///   FBmpSelector.LoadFromResourceName(HInstance,'GAMMA');
		///   
		/// Converted to a bitmap from a basic image so that pixels can be directly accessed.
		/// </summary>
		private Bitmap FBmpSelector = new Bitmap(global::GBRenderer.Properties.Resources.GAMMA);

		/// <summary>
		/// Use the GBC filter?
		/// 
		/// Declared line 64: 
		///      FGBCFilter     : boolean;
		/// </summary>
		private bool FGBCFilter = false;

		/// <summary>
		/// Current color?
		/// 
		/// Declared line 67:
		///      FClrSelector   : TColor;
		/// </summary>
		private Color FClrSelector = Color.Black;

		/// <summary>
		/// The actual color for the "FFirst" display.
		/// Defined line 56:
		/// FFirstColor    : TColor;
		/// </summary>
		private Color FFirstColor = Color.Black;

		/// <summary>
		/// Whether or not the UI is frozen?
		/// 
		/// Declared line 64.
		///      FGBCFilter     : boolean;
		/// </summary>
		private bool Freezed = false;
		#endregion

		/// Methods that set/get various fields.  Most are private.
		#region Private setter methods
		/// <summary>
		/// Sets the FFirstColor to that value.
		/// 
		/// Declared on line 69: 
		///      procedure SetFirstColor(Value : TColor);
		/// 
		/// Defined lines 532-538:
		/// procedure TGammaPanel.SetFirstColor(Value : TColor);
		/// begin
		///   FFirstColor:=Value;
		///   SetFirstControls( FFirstColor );
		///   ChangeColor(FFirstColor);
		///   if Assigned(FOnChange) then FOnChange(Self);
		/// end;
		/// </summary>
		/// <param name="value"></param>
		private void SetFirstColor(Color value) {
			FFirstColor = value;
			SetFirstControls(FFirstColor);
			ChangeColor(FFirstColor);
			OnChanged();
		}

		/// <summary>
		/// Sets a color.
		/// 
		/// Declared on 78: 
		///      procedure SetViewColor( c : TColor );
		///      
		/// Defined 412-419:
		/// procedure TGammaPanel.SetViewColor( c : TColor );
		/// begin
		///   FClrSelector := c;
		///   if FGBCFilter then
		///     FViewWindow.Color := TranslateToGBCColor( FClrSelector )
		///   else
		///     FViewWindow.Color := FClrSelector;
		/// end;
		/// </summary>
		/// <param name="c"></param>
		private void SetViewColor(Color c) {
			FClrSelector = c;
			if (FGBCFilter) {
				FViewWindow.BackColor = TranslateToGBCColor(FClrSelector);
			} else {
				FViewWindow.BackColor = FClrSelector;
			}
			FViewWindow.Refresh();
		}

		/// <summary>
		/// Translates a color.
		/// 
		/// Declared line 63 ColorControlor.pas:
		/// function TranslateToGBCColor( clr : TColor) : TColor;
		/// 
		/// Defined lines 78-110 ColorControlor.pas: 
		/// function TranslateToGBCColor( clr : TColor) : TColor;
		/// 
		/// //procedure translate(var rgb : rgbtype);
		/// const intensity : array[0..$1f] of byte = (
		///  $00,$10,$20,$30,$40,$50,$5e,$6c,$7a,$88,$94,$a0,$ae,$b7,$bf,$c6,
		///  $ce,$d3,$d9,$df,$e3,$e7,$eb,$ef,$f3,$f6,$f9,$fb,$fd,$fe,$ff,$ff);
		/// 
		/// const influence : array[1..3,1..3] of byte = ((16,4,4),(8,16,8),(0,8,16));
		/// var
		///   m   : array[1..3,1..3] of byte;
		///   i,j : byte;
		///   rgb : rgbtype;
		///   c : tColor;
		/// begin
		///   rgb[1] := (clr and $0000FF) shr (0+3);
		///   rgb[2] := (clr and $00FF00) shr (8+3);
		///   rgb[3] := (clr and $FF0000) shr (16+3);
		/// 
		///   for i:=1 to 3 do                
		///     for j:=1 to 3 do
		///       m[i,j] := (intensity[rgb[i]] * influence[i,j]) shr 5;
		/// 
		///   for i:=1 to 3 do begin
		///     if m[1,i]>m[2,i] then begin j:=m[1,i]; m[1,i]:=m[2,i]; m[2,i]:=j; end;
		///     if m[2,i]>m[3,i] then begin j:=m[2,i]; m[2,i]:=m[3,i]; m[3,i]:=j; end;
		///     if m[1,i]>m[2,i] then begin j:=m[1,i]; m[1,i]:=m[2,i]; m[2,i]:=j; end;
		///     rgb[i]:=(((m[1,i]+m[2,i]*2+m[3,i]*4)*5) shr 4)+32;
		///   end;
		/// 
		/// //  Result := TColor( rgb[1] + (rgb[2] shl 8) + (rgb[3] shl 16) );
		///   c := TColor( rgb[1] + (rgb[2] shl 8) + (rgb[3] shl 16) );
		///   Result := c;
		/// end;
		/// </summary>
		/// <param name="clr"></param>
		/// <returns></returns>
		private Color TranslateToGBCColor(Color clr) {
			byte[] intensity = new byte[0x20] {
				0x00,0x10,0x20,0x30,0x40,0x50,0x5e,0x6c,0x7a,0x88,0x94,0xa0,0xae,0xb7,0xbf,0xc6,
				0xce,0xd3,0xd9,0xdf,0xe3,0xe7,0xeb,0xef,0xf3,0xf6,0xf9,0xfb,0xfd,0xfe,0xff,0xff
			};

			byte[,] influence = new byte[3,3] {{16,4,4},{8,16,8},{0,8,16}};

			byte[,] m = new byte[3,3];
			byte i,j;
			byte[] rgb = new byte[3];
			Color c;
			
			rgb[0] = (byte)(clr.R >> 3);
			rgb[1] = (byte)(clr.G >> 3);
			rgb[2] = (byte)(clr.B >> 3);

			for (i = 0; i < 3; i++) {
				for (j = 0; j < 3; j++) {
					m[i,j] = (byte)((intensity[rgb[i]] * influence[i,j]) >> 5);
				}
			}
			for (i = 1; i < 3; i++) {
				if (m[0,i] > m[1,i]) {j = m[0,i]; m[0,i] = m[1,i]; m[1,i] = j;}
				if (m[1,i] > m[2,i]) {j = m[1,i]; m[1,i] = m[2,i]; m[2,i] = j;};
				if (m[0,i] > m[1,i]) {j = m[0,i]; m[0,i] = m[1,i]; m[1,i] = j;};
				rgb[i] = (byte)((((m[0,i]+m[1,i]*2+m[2,i]*4)*5) >> 4)+32);
			}

		  c = Color.FromArgb(rgb[0], rgb[1], rgb[2]);
		  return c;
		}

		/// <summary>
		/// Sets whether or not to use the GBC filter.
		/// 
		/// Defined lines 402-410:
		/// procedure TGammaPanel.SetGBCFilter( b : boolean);
		/// begin
		///   FGBCFilter := b;
		/// 
		///   if b then
		///     FGamma.Picture.Bitmap.LoadFromResourceName(HInstance,'GBCGAMMA')
		///   else
		///     FGamma.Picture.Bitmap := FBmpSelector;
		/// end;
		/// </summary>
		/// <param name="b"></param>
		private void SetGBCFilter(Boolean b) {
			FGBCFilter = b;

			//Silly way to do this, but it is more consise than if/else.
			switch (b) {
			case false: this.FGamma.Image = global::GBRenderer.Properties.Resources.GAMMA; break;
			case true: this.FGamma.Image = global::GBRenderer.Properties.Resources.GBCGAMMA; break;
			}
		}

		/// <summary>
		/// Sets the colors of FFirst.
		/// 
		/// Declared on line 79:
		///      procedure SetFirstControls( c : TColor);
		/// 
		/// Defined on lines 569-581:
		/// procedure TGammaPanel.SetFirstControls( c : TColor);
		/// begin
		///   if FGBCFilter then
		///   begin
		///     FFirst.Brush.Color := TranslateToGBCColor(c);
		///     FFirst.Pen.Color   := TranslateToGBCColor(c);
		///   end
		///   else
		///   begin
		///     FFirst.Brush.Color := c;
		///     FFirst.Pen.Color   := c;
		///   end;
		/// end;
		/// </summary>
		/// <param name="c"></param>
		private void SetFirstControls(Color c) {
			switch (FGBCFilter) {
			case true: FFirst.BackColor = TranslateToGBCColor(c); break;
			case false: FFirst.BackColor = c; break;
			}
			FFirst.Refresh();
		}

		/// <summary>
		/// Changes the current color.  
		/// 
		/// Defined on lines 443-457: 
		/// procedure TGammaPanel.ChangeColor( c : TColor);
		/// var
		///   R,G,B : integer;
		/// begin
		///   R := GetRValue(ColorToRGB(c));
		///   G := GetGValue(ColorToRGB(c));
		///   B := GetBValue(ColorToRGB(c));
		/// 
		///   Freezed := True;
		///   REd.Text :=IntToStr(R shr 3);
		///   GEd.Text :=IntToStr(G shr 3);
		///   BEd.Text :=IntToStr(B shr 3);
		///   Freezed := False;
		///   UpdateColor;
		/// end;
		/// </summary>
		/// <param name="c"></param>
		private void ChangeColor(Color c) {
			Freezed = true;
			REd.Text = (c.R >> 3).ToString();
			GEd.Text = (c.G >> 3).ToString();
			BEd.Text = (c.B >> 3).ToString();
			Freezed = false;
			UpdateColor();
		}

		/// <summary>
		/// Updates the current color.
		/// 
		/// Defined on lines 460-485:
		/// procedure TGammaPanel.UpdateColor;
		/// var Ok : boolean;
		/// var R,G,B : integer;
		/// begin
		/// 
		///   Ok := True;
		///   try
		///     R := StrToInt(REd.Text);
		///     G := StrToInt(GEd.Text);
		///     B := StrToInt(BEd.Text);
		///   except
		///     Ok := False;
		///   end;
		/// 
		///   if Ok then
		///   begin
		///     FViewWindow.Caption:='';
		///     ViewColor           := RGB(R shl 3,G shl 3,B shl 3);
		///   end
		///   else
		///   begin
		///     FViewWindow.Caption := 'None';
		///     ViewColor           := clGray;
		///   end;
		/// 
		/// end;
		/// </summary>
		private void UpdateColor() {
			//Whether accepted or not.
			bool Ok = true;
			//RGB values
			int R = 0, G = 0, B = 0;

			try {
				R = Convert.ToInt32(REd.Text);
				G = Convert.ToInt32(GEd.Text);
				B = Convert.ToInt32(BEd.Text);
			} catch (Exception) {
				Ok = false;
			}

			if (Ok) {
				FViewWindow.Text = "";
				ViewColor = Color.FromArgb(R << 3, G << 3, B << 3);
			} else {
				FViewWindow.Text = "None";
				ViewColor = Color.Gray;
			}
		}
		#endregion

		///The protected functions and fields.
		#region Protected functions and fields
		protected Color ViewColor {
			get { return FClrSelector; }
			set { SetViewColor(value); }
		}
		#endregion

		#region Public Events
		/// <summary>
		/// Event handler for when this is changed.
		/// </summary>
		[Category("Action"), Description("Fires when the value is changed")]
		public event EventHandler OnChange;

		/// <summary>
		/// Called when the value is changed.
		/// 
		/// Matches the event code seen thorughout: 
		/// if Assigned(FOnChange) then FOnChange(Self);
		/// </summary>
		protected virtual void OnChanged() {
			// Raise the event
			if (OnChange != null) {
				OnChange(this, new EventArgs());
			}
		}
		#endregion

		/// The publicly used feilds.
		#region Public Fields
		/// <summary>
		/// Use the GBC Filter?
		/// 
		/// Declared line 123:
		///      property GBCFilter : boolean read FGBCFilter write SetGBCFilter;
		/// </summary>
		[Description("Use the regular GBC filter, rather than the regular colors.  GBC colors are paler."), Category("Data")]
		public bool GBCFilter {
			get { return FGBCFilter; }
			set { SetGBCFilter(value); }
		}

		/// <summary>
		/// Defined on line 102:
		///      property FirstColor : TColor read FFirstColor write SetFirstColor default clBlack;
		/// </summary>
		[Description("The color used for the First box, which is currently hovered over."), Category("Data")]
		public Color FirstColor {
			get { return FFirstColor; }
			set { SetFirstColor(value); }
		}

		[Description("The color used for the First box, which is currently hovered over."), Category("Data")]
		public Color MainViewColor {
			get { return ViewColor; }
		}

		#endregion

		public TGammaPanel() {
			InitializeComponent();
		}

		private void ColorPicker_Paint(object sender, PaintEventArgs e) {
			/*
			 * Equivilant to the following code from lines 158-162:
  BevelInner:=bvRaised;
  BevelOuter:=bvLowered;
  BevelWidth:=1;
  BorderStyle:=bsNone;
  BorderWidth:=0;
			 */
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.Width, this.Height, Border3DStyle.Etched, Border3DSide.All);
		}

		private void FImagePanel_Paint(object sender, PaintEventArgs e) {
			/*
			 * Equivilant to the following code from lines 174-178:
  FStatePanel.BevelInner:=bvLowered;
  FStatePanel.BevelOuter:=bvNone;
  FStatePanel.BevelWidth:=1;
  FStatePanel.BorderStyle:=bsNone;
  FStatePanel.BorderWidth:=0;
			 */
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, FImagePanel.Width, FImagePanel.Height, Border3DStyle.SunkenOuter, Border3DSide.All);
		}

		private void FControlPanel_Paint(object sender, PaintEventArgs e) {
			/*
			 * From lines 208-212
  FControlPanel.BevelInner:=bvLowered;
  FControlPanel.BevelOuter:=bvNone;//bvRaised;
  FControlPanel.BevelWidth:=1;
  FControlPanel.BorderStyle:=bsNone;
  FControlPanel.BorderWidth:=0;
			 */
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, FControlPanel.Width, FControlPanel.Height, Border3DStyle.SunkenOuter, Border3DSide.All);
		}

		private void FStatePanel_Paint(object sender, PaintEventArgs e) {
			/*
			 * From lines 191-195
  FStatePanel.BevelInner:=bvLowered;
  FStatePanel.BevelOuter:=bvNone;
  FStatePanel.BevelWidth:=1;
  FStatePanel.BorderStyle:=bsNone;
  FStatePanel.BorderWidth:=0;
			*/
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, FStatePanel.Width, FStatePanel.Height, Border3DStyle.SunkenOuter, Border3DSide.All);
		}

		private void FViewWindow_Paint(object sender, PaintEventArgs e) {
			/*
			 * From lines 226-230
  FViewWindow.BevelInner:=bvNone;
  FViewWindow.BevelOuter:=bvLowered;
  FViewWindow.BevelWidth:=1;
  FViewWindow.BorderStyle:=bsNone;
  FViewWindow.BorderWidth:=0;
			 */
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, FViewWindow.Width, FViewWindow.Height, Border3DStyle.SunkenOuter, Border3DSide.All);
			using (Brush b = new Pen(FViewWindow.BackColor).Brush) { //Most definitely not the right way of getting a brush, but it works.
				e.Graphics.FillRectangle(b, 1, 1, FViewWindow.Width - 2, FViewWindow.Height - 2);
			}
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;
			e.Graphics.DrawString(FViewWindow.Text, this.Font, Brushes.Black, new RectangleF(0f, 0f, FViewWindow.Width, FViewWindow.Height), format);
		}

		/// <summary>
		/// Called when the mouse moves in 'FFirst'.
		/// Sets the color used to whatever FFirst's color is.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void FirstMouseMove(object sender, MouseEventArgs e) {
			/*
			 * 504-508:
  FViewWindow.Caption:='';
  ViewColor := FFirst.Brush.Color;
  REd.Text := IntToStr(GetRValue(ColorToRGB(FFirst.Brush.Color)) shr 3);
  GEd.Text := IntToStr(GetGValue(ColorToRGB(FFirst.Brush.Color)) shr 3);
  BEd.Text := IntToStr(GetBValue(ColorToRGB(FFirst.Brush.Color)) shr 3);
			 */
			FViewWindow.Text = "";
			ViewColor = FFirst.BackColor;
			REd.Text = (FFirst.BackColor.R >> 3).ToString();
			GEd.Text = (FFirst.BackColor.G >> 3).ToString();
			BEd.Text = (FFirst.BackColor.B >> 3).ToString();
		}

		/// <summary>
		/// Despite the name, this only applies to FImagePanel...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void AllMouseMove(object sender, MouseEventArgs e) {
			/*
			 * 420-430:
procedure TGammaPanel.AllMouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
begin
  Freezed := True;
  REd.Text := '';
  GEd.Text := '';
  BEd.Text := '';
  Freezed := False;
  UpdateColor;
end;
			 */
			Freezed = true;
			REd.Text = "";
			GEd.Text = "";
			BEd.Text = "";
			Freezed = false;
			UpdateColor();
		}

		/// <summary>
		/// When the mouse moves in FGamma.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ImgMouseMove(object sender, MouseEventArgs e) {
			/*
			 * 432-439:
procedure TGammaPanel.ImgMouseMove(Sender: TObject; Shift: TShiftState; X, Y: Integer);
var c : TColor;
var R,G,B : integer;
begin
  FViewWindow.Caption:='';
  c := FBmpSelector.Canvas.Pixels[x,y];
  ChangeColor(c);
end;
			 */
			Color c;
			//int R, G, B; //Useless?

			FViewWindow.Text = "";
			try {
				c = FBmpSelector.GetPixel(e.X, e.Y);
			} catch (ArgumentOutOfRangeException) {
				//For whatever reason I somehow clicked outside of the control.  Doesn't seem like something that should happen, but when I double-click it does.
				c = Color.Gray;
				FViewWindow.Text = "None";
			}
			ChangeColor(c);
		}

		/// <summary>
		/// Called when any of the editors are changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EdChange(object sender, EventArgs e) {
			/*
			 * 548-551:
procedure TGammaPanel.EdChange(Sender: TObject);
begin
  ForceNumbers(TCustomEdit(Sender));
end;
			 */
			if (sender is TextBox) {
				TextBox box = (TextBox)sender;
				int num = 0;
				if (!int.TryParse(box.Text, out num)) {
					//Resets to previous number.
					box.Text = "0";
				}
			}
		}

		/// <summary>
		/// Called when any of the editors have the key released.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EdKeyUp(object sender, KeyEventArgs e) {
			/*
			 * 554-567
procedure TGammaPanel.EdKeyUp(Sender: TObject; var Key: Word; Shift: TShiftState);
begin
  with TEdit(Sender) do
    if ((Text <> '') and (StrToInt(Text) > 31)) then TEdit(Sender).Text := '31';

  if not Freezed then UpdateColor;

  if ( FViewWindow.Caption = '') then
  begin
    FFirstColor        := ViewColor;
    SetFirstControls(ViewColor);
    if Assigned(FOnChange) then FOnChange(Self);
  end;
end;
			 */
			if (sender is TextBox) {
				TextBox box = (TextBox)sender;
				try {
					int intVal = int.Parse(box.Text);
					if (intVal < 0) {
						box.Text = "0";
					}
					if (intVal > 31) {
						box.Text = "31";
					}
				} catch (FormatException) {
					box.Text = "0";
				} catch (OverflowException) {
					box.Text = "0";
				}

				if (!Freezed) {
					UpdateColor();
				}

				if (FViewWindow.Text != "None") {
					FFirstColor = ViewColor;
					SetFirstControls(ViewColor);

					this.OnChanged();
				}
			}
		}

		/// <summary>
		/// Mouse down on FFirst.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void FirstMouseDown(object sender, MouseEventArgs e) {
			/*
			 * 496-500
procedure TGammaPanel.FirstMouseDown(Sender: TObject;
                Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
begin
  if Assigned(FOnChange) then FOnChange(Self);
end;
			 */
			OnChanged();
		}

		/// <summary>
		/// When FGamma clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void GammaClick(object sender, MouseEventArgs e) {
			/*
			 * 488-493
procedure TGammaPanel.GammaClick(Sender: TObject);
begin
  FFirstColor := ViewColor;
  SetFirstControls( ViewColor );
  if Assigned(FOnChange) then FOnChange(Self);
end;
			 */
			FFirstColor = ViewColor;
			SetFirstControls(ViewColor);
			OnChanged();
		}

		/// <summary>
		/// 512-516; called when the system colors are changed.
		/// procedure TGammaPanel.CMColorChanged (var Message: TMessage);
		/// begin
		///   inherited;
		///   Invalidate;
		/// end;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TGammaPanel_SystemColorsChanged(object sender, EventArgs e) {
			this.Refresh();
		}

		/// <summary>
		/// Called when resized.  Denies resizing.
		/// 
		/// 518-524:
		/// 
		/// procedure TGammaPanel.WMSize (var Message: TWMSize);
		/// begin
		///   inherited;
		///   Width :=  FULLWIDTH;
		///   Height := FULLHEIGHT;
		///   Invalidate;
		/// end;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TGammaPanel_Resize(object sender, EventArgs e) {
			Size = new Size(FULLWIDTH, FULLHEIGHT);

			Refresh();
		}

		/// <summary>
		/// 526-530; called when the style is changed (font).
		/// procedure TGammaPanel.CMFontChanged (var Message: TMessage);
		/// begin
		///   inherited;
		///   Invalidate;
		/// end;
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TGammaPanel_StyleChanged(object sender, EventArgs e) {
			Refresh();
		}
	}
}
