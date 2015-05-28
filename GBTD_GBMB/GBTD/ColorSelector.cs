using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using GB.Shared.Tiles;
using System.ComponentModel;
using GB.Shared.GBRFile;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using GB.Shared.Palettes;
using GB.Shared.Controls;

namespace GB.GBTD
{
	public class ColorSelector : Control
	{
		protected override Size DefaultMaximumSize { get { return new Size(191, 26); } }
		protected override Size DefaultMinimumSize { get { return new Size(191, 26); } }
		protected override Size DefaultSize { get { return new Size(191, 26); } }

		/// <summary>
		/// Combobox used inside of ColorSelector to select a palette.
		/// Yes, there is a similar class in the shared library, ColorDropdown, that does this.
		/// A seperate version is just more useful.
		/// </summary>
		private class PaletteDropdown : ComboBox
		{
			private readonly ColorSelector parent;

			private volatile bool updating;

			public PaletteDropdown(ColorSelector parent) {
				this.parent = parent;

				this.DrawMode = DrawMode.OwnerDrawFixed;
				this.DropDownStyle = ComboBoxStyle.DropDownList;
				this.FormattingEnabled = true;
				this.ItemHeight = 13;
				this.Size = new Size(83, 19);

				this.RecreateItems();
			}

			protected override void OnDrawItem(DrawItemEventArgs e) {
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

				e.DrawBackground();
				e.DrawFocusRectangle();

				if (parent.palettes == null) {
					e.Graphics.DrawString("palettes is null!", Font, Brushes.Red, new Point(0, 0));
				} else if (parent.paletteMapping == null) {
					e.Graphics.DrawString("paletteMapping is null!", Font, Brushes.Red, new Point(0, 0));
				} else {
					if (e.Index == -1) {
						return;
					} else {
						var item = this.Items[e.Index];

						if ((item as string) == "Default") {
							e.Graphics.DrawString("Default", e.Font, SystemBrushes.ControlText, e.Bounds);
						} else {
							int paletteNum = Convert.ToInt32(item);

							float width = e.Bounds.Width / 5f;

							Palette palette = new Palette();
							palette[GBColor.BLACK] = GetColor(paletteNum, GBColor.BLACK);
							palette[GBColor.DARK_GRAY] = GetColor(paletteNum, GBColor.DARK_GRAY);
							palette[GBColor.LIGHT_GRAY] = GetColor(paletteNum, GBColor.LIGHT_GRAY);
							palette[GBColor.WHITE] = GetColor(paletteNum, GBColor.WHITE);

							RectangleF rect = new RectangleF(e.Bounds.X, e.Bounds.Y, width, e.Bounds.Height);

							for (int i = 0; i < 4; i++) {
								rect.X = e.Bounds.X + (width * i);
								using (SolidBrush brush = new SolidBrush(palette[i])) {
									e.Graphics.FillRectangle(brush, rect);
									e.Graphics.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
								}
							}
						}
					}
				}

				base.OnDrawItem(e);
			}

			private Color GetColor(int row, GBColor color) {
				switch (parent.colorSet) {
				case ColorSet.GAMEBOY_COLOR:
					return parent.palettes.GBCPalettes[row][color];
				case ColorSet.GAMEBOY_COLOR_FILTERED:
					return parent.palettes.GBCPalettes[row][color].FilterWithGBC();
				case ColorSet.SUPER_GAMEBOY:
					return parent.palettes.SGBPalettes[row][color];
				case ColorSet.GAMEBOY:
					return color.GetNormalColor();
				case ColorSet.GAMEBOY_POCKET:
					return color.GetPocketColor();
				default: throw new InvalidOperationException("Current colorset is unrecognised: " + parent.colorSet + 
					" (" + (int)parent.colorSet + ")!");
				}
			}

			internal void RecreateItems() {
				if (updating) {
					return;
				}

				if (parent.palettes == null) {
					return;
				}

				updating = true;

				this.SelectedIndex = -1;

				this.Items.Clear();

				int itemCount = (parent.colorSet.SupportsPaletteCustomization() ? parent.colorSet.GetNumberOfRows() : 1);

				for (int i = 0; i < itemCount; i++) {
					this.Items.Add(i);
				}

				switch (parent.colorSet) {
				case ColorSet.GAMEBOY_COLOR:
				case ColorSet.GAMEBOY_COLOR_FILTERED:
					this.SelectedIndex = (int)parent.paletteMapping.GBCPalettes[parent.selectedTile];
					break;
				case ColorSet.SUPER_GAMEBOY:
					this.SelectedIndex = (int)parent.paletteMapping.SGBPalettes[parent.selectedTile];
					break;
				default:
					this.SelectedIndex = 0;
					break;
				}

				updating = false;
			}

			protected override void OnSelectedIndexChanged(EventArgs e) {
				if (!updating && parent.paletteMapping != null) {
					switch (parent.colorSet) {
					case ColorSet.GAMEBOY_COLOR:
					case ColorSet.GAMEBOY_COLOR_FILTERED:
						parent.paletteMapping.GBCPalettes[parent.selectedTile] = (uint)this.SelectedIndex;
						if (parent.PaletteChanged != null) {
							parent.PaletteChanged(parent, e);
						}
						break;
					case ColorSet.SUPER_GAMEBOY:
						parent.paletteMapping.SGBPalettes[parent.selectedTile] = (uint)this.SelectedIndex;
						if (parent.PaletteChanged != null) {
							parent.PaletteChanged(parent, e);
						}
						break;
						//Do nothing on default case; we don't want to update on the default palette.
						//(Though mabye the background reg should be used eventually)
					}
				}

				base.OnSelectedIndexChanged(e);
			}
		}

		private UInt16 selectedTile;
		private ColorSet colorSet;
		private GBRObjectPalettes palettes;
		private GBRObjectTilePalette paletteMapping;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public UInt16 SelectedTile {
			get { return selectedTile; }
			set {
				selectedTile = value;
				this.Invalidate(true);
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public ColorSet ColorSet {
			get { return colorSet; }
			set { colorSet = value; paletteDropdown.RecreateItems(); this.Invalidate(true); }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBRObjectPalettes Palettes {
			get { return palettes; }
			set {
				palettes = value;
				this.Invalidate(true);
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBRObjectTilePalette PaletteMapping {
			get { return paletteMapping; }
			set {
				paletteMapping = value;
				this.Invalidate(true);
			}
		}

		private GBColor leftColor, rightColor, middleColor, x1Color, x2Color;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor LeftColor {
			get { return leftColor; }
			set {
				if (value != leftColor) {
					leftColor = value;
					if (MouseColorChanged != null) { MouseColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor RightColor {
			get { return rightColor; }
			set {
				if (value != rightColor) {
					rightColor = value;
					if (MouseColorChanged != null) { MouseColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor MiddleColor {
			get { return middleColor; }
			set {
				if (value != middleColor) {
					middleColor = value;
					if (MouseColorChanged != null) { MouseColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor X1Color {
			get { return x1Color; }
			set {
				if (value != x1Color) {
					x1Color = value;
					if (MouseColorChanged != null) { MouseColorChanged(this, new EventArgs()); }
				}
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public GBColor X2Color {
			get { return x2Color; }
			set {
				if (value != x2Color) {
					x2Color = value;
					if (MouseColorChanged != null) { MouseColorChanged(this, new EventArgs()); }
				}
			}
		}

		private PaletteDropdown paletteDropdown;

		public ColorSelector() {
			DoubleBuffered = true;

			SetStyle(ControlStyles.ResizeRedraw, true);

			this.paletteDropdown = new PaletteDropdown(this);
			this.paletteDropdown.Location = new Point(60, 2); //TODO

			this.Controls.Add(paletteDropdown);
		}
		
		[Description("Fires when one of the mouse colors has changed.")]
		public event EventHandler MouseColorChanged;
		[Description("Fires when the palette for the curent tile has changed.")]
		public event EventHandler PaletteChanged;

		protected override void OnPaint(PaintEventArgs e) {
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			e.Graphics.SmoothingMode = SmoothingMode.None;
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
			e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

			if (palettes == null) {
				e.Graphics.DrawString("palettes is null!", Font, Brushes.Red, new Point(0, 0));
			} else if (paletteMapping == null) {
				e.Graphics.DrawString("paletteMapping is null!", Font, Brushes.Red, new Point(0, 0));
			} else {
				BorderPaint.DrawBorderFull(e.Graphics, 0, 0, Width, Height, SystemColors.ControlLightLight, Border3DSide.Left | Border3DSide.Top);
				BorderPaint.DrawBorderFull(e.Graphics, 0, 0, Width, Height, SystemColors.ControlDark, Border3DSide.Right | Border3DSide.Bottom);

				DrawMouseButtonDisplay(e, "L", leftColor, 2, 2);
				DrawMouseButtonDisplay(e, "R", rightColor, 39, 2);
			}
			
			base.OnPaint(e);
		}

		private void DrawMouseButtonDisplay(PaintEventArgs e, String text, GBColor color, int x, int y) {
			e.Graphics.PixelOffsetMode = PixelOffsetMode.None;

			BorderPaint.DrawBorderFull(e.Graphics, x, y, 36, 22, SystemColors.ControlDark, Border3DSide.Left | Border3DSide.Top);
			BorderPaint.DrawBorderFull(e.Graphics, x, y, 36, 22, SystemColors.ControlLightLight, Border3DSide.Right | Border3DSide.Bottom);

			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Near;
			format.LineAlignment = StringAlignment.Center;

			e.Graphics.DrawString(text, Font, Brushes.Black, new Rectangle(x + 3, y + 3, 11, 18), format);

			int colorNum;
			switch (color) {
			case GBColor.WHITE: colorNum = 0; break;
			case GBColor.LIGHT_GRAY: colorNum = 1; break;
			case GBColor.DARK_GRAY: colorNum = 2; break;
			case GBColor.BLACK: colorNum = 3; break;
			default: colorNum = (int)color; break;
			}

			using (SolidBrush brush = new SolidBrush(GetColor(color))) {
				e.Graphics.FillRectangle(brush, x + 15, y + 1, 19, 19);
			}
			e.Graphics.DrawRectangle(Pens.Black, x + 15, y + 1, 19, 19);

			e.Graphics.DrawString(colorNum.ToString(), Font, Brushes.Black, new Rectangle(x + 19, y + 3, 11, 18), format);
		}

		protected override void OnResize(EventArgs e) {
			this.Size = new Size(191, 26);
			
			base.OnResize(e);
		}

		/// <summary>
		/// Gets the proper color for the given ColorSet.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		private Color GetColor(GBColor color) {
			switch (this.colorSet) {
			case ColorSet.GAMEBOY_COLOR:
				return palettes.GBCPalettes[paletteMapping.GBCPalettes[this.selectedTile]][color];
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				return palettes.GBCPalettes[paletteMapping.GBCPalettes[this.selectedTile]][color].FilterWithGBC();
			case ColorSet.SUPER_GAMEBOY:
				return palettes.SGBPalettes[paletteMapping.SGBPalettes[this.selectedTile]][color];
			case ColorSet.GAMEBOY:
				return color.GetNormalColor();
			case ColorSet.GAMEBOY_POCKET:
				return color.GetPocketColor();
			default: throw new InvalidOperationException("Current colorset is unrecognised: " + this.colorSet + " (" + (int)this.colorSet + ")!");
			}
		}
	}
}
