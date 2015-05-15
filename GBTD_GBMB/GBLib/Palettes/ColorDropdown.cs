using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GB.Shared.Palettes
{
	public class ColorDropdown : ComboBox
	{
		private PaletteData paletteData;
		private ColorSet colorSet;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		[Description("The current PaletteData.")]
		public PaletteData PaletteData {
			get { return this.paletteData; }
			set { this.paletteData = value; this.Invalidate(true); }
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		[Description("The current ColorSet.")]
		public ColorSet ColorSet {
			get { return this.colorSet; }
			set {
				this.colorSet = value;

				if (colorSet == ColorSet.GAMEBOY || colorSet == ColorSet.GAMEBOY_POCKET) {
					this.Enabled = false;
				} else {
					this.Enabled = true;
				}

				RecreateItems();

				this.Invalidate(true);
			}
		}

		private bool allowDefault;
		/// <summary>
		/// Whether or not the "Default" setting is allowed.
		/// </summary>
		[Category("Behavior"), Description("Whether or not the \"Default\" setting is allowed.")]
		public bool AllowDefault {
			get { return allowDefault; }
			set {
				suspendUpdating = true;

				this.allowDefault = value;

				if (allowDefault && !this.Items.Contains("Default")) {
					this.Items.Insert(0, "Default");
				} else if (!allowDefault && this.Items.Contains("Default")) {
					this.Items.Remove("Default");
				}

				suspendUpdating = false;
			}
		}

		/// <summary>
		/// Whether the color set is being changed.  If true, don't update the palette with SelectedIndex.
		/// </summary>
		private bool suspendUpdating;

		private void UpdateSelectedItem() {
			SetSelectedItem(GetSelectedPalette());
		}

		private void SetSelectedItem(int? palette) {
			if (palette == null) {
				this.SelectedIndex = -1;
			} else {
				if (palette.Value < 0) {
					if (this.allowDefault) {
						this.SelectedItem = "Default";
					} else {
						this.SelectedIndex = -1;
					}
				} else {
					this.SelectedItem = palette.Value;
				}
			}
		}

		private int? GetSelectedPalette() {
			switch (this.colorSet) {
			case ColorSet.GAMEBOY: return null;
			case ColorSet.GAMEBOY_POCKET: return null;
			case ColorSet.GAMEBOY_COLOR: return gbcSelectedPalette;
			case ColorSet.GAMEBOY_COLOR_FILTERED: return gbcSelectedPalette;
			case ColorSet.SUPER_GAMEBOY: return sgbSelectedPalette;
			default: throw new Exception("Colorset is invalid: is " + ColorSet + " (" + (int)ColorSet + ").");
			}
		}

		private void UpdateSelectedPalette() {
			suspendUpdating = true;

			int? palette;

			if (this.SelectedIndex == -1) {
				palette = null;
			} else {
				if ((this.SelectedItem as string) == "Default") {
					if (this.allowDefault) {
						palette = -1;
					} else {
						palette = null;
						this.SelectedIndex = -1;
					}
				} else {
					palette = Convert.ToInt32(this.SelectedItem);
				}
			}

			switch (this.colorSet) {
			case ColorSet.GAMEBOY_COLOR: this.gbcSelectedPalette = palette; break;
			case ColorSet.GAMEBOY_COLOR_FILTERED: this.gbcSelectedPalette = palette; break;
			case ColorSet.SUPER_GAMEBOY: this.sgbSelectedPalette = palette; break;
			}

			suspendUpdating = false;
		}

		/// <summary>
		/// (re) creates all of the items.
		/// </summary>
		private void RecreateItems() {
			suspendUpdating = true;

			this.SelectedIndex = -1;

			this.Items.Clear();

			if (allowDefault) {
				Items.Add("Default");
			}
			for (int i = 0; i < colorSet.GetNumberOfRows(); i++) {
				Items.Add(i);
			}

			suspendUpdating = false;
		}

		private int? gbcSelectedPalette;
		private int? sgbSelectedPalette;

		/// <summary>
		/// The current palette index selected for the active ColorSet.
		/// If -1, there are multiple values.  If null, the default value is chosen.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public int? ActiveSelectedPalette {
			get {
				switch (colorSet) {
				case ColorSet.GAMEBOY: throw new InvalidOperationException("Currently selected ColorSet doesn't have a palette!");
				case ColorSet.GAMEBOY_POCKET: throw new InvalidOperationException("Currently selected ColorSet doesn't have a palette!");
				case ColorSet.GAMEBOY_COLOR: return GBCSelectedPalette;
				case ColorSet.GAMEBOY_COLOR_FILTERED: return GBCSelectedPalette;
				case ColorSet.SUPER_GAMEBOY: return SGBSelectedPalette;
				default: throw new InvalidOperationException("Currently selected ColorSet is unrecognised: " + colorSet + "(" + (int)colorSet + ").");
				}
			}
			set {
				switch (colorSet) {
				case ColorSet.GAMEBOY: throw new InvalidOperationException("Currently selected ColorSet doesn't have a palette!");
				case ColorSet.GAMEBOY_POCKET: throw new InvalidOperationException("Currently selected ColorSet doesn't have a palette!");
				case ColorSet.GAMEBOY_COLOR: GBCSelectedPalette = value; break;
				case ColorSet.GAMEBOY_COLOR_FILTERED: GBCSelectedPalette = value; break;
				case ColorSet.SUPER_GAMEBOY: SGBSelectedPalette = value; break;
				default: throw new InvalidOperationException("Currently selected ColorSet is unrecognised: " + colorSet + "(" + (int)colorSet + ").");
				}
			}
		}

		/// <summary>
		/// The current palette index selected for GBC.
		/// If -1, there are multiple values.  If null, the default value is chosen.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public int? GBCSelectedPalette {
			get { return gbcSelectedPalette; }
			set {
				gbcSelectedPalette = value;
				UpdateSelectedItem();
			}
		}
		/// <summary>
		/// The current palette index selected for SGB.
		/// If -1, there are multiple values.  If null, the default value is chosen.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
		public int? SGBSelectedPalette {
			get { return sgbSelectedPalette; }
			set {
				sgbSelectedPalette = value;
				UpdateSelectedItem();
			}
		}

		public ColorDropdown() {
			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			this.FormattingEnabled = true;
			this.ItemHeight = 13;
			this.Size = new Size(83, 19);

			RecreateItems();
		}

		protected override void OnDrawItem(DrawItemEventArgs e) {
			e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

			e.DrawBackground();
			e.DrawFocusRectangle();

			if (paletteData == null) {
				return;
			}

			if (e.Index == -1) {
				return;
			} else {
				var item = this.Items[e.Index];

				if ((item as string) == "Default") {
					e.Graphics.DrawString("Default", e.Font, SystemBrushes.ControlText, e.Bounds);
				} else {
					int paletteNum = Convert.ToInt32(item);

					Palette palette = PaletteData.GetPaletteSet(this.ColorSet)[paletteNum];

					float width = e.Bounds.Width / 5f;

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

			base.OnDrawItem(e);
		}

		protected override void OnSelectedItemChanged(EventArgs e) {
			if (!suspendUpdating) {
				UpdateSelectedPalette();

				base.OnSelectedIndexChanged(e);
			}
		}
	}
}
