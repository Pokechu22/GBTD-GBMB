using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.GBRFile;
using GB.Shared.Palettes;
using GB.Shared.Controls;
using GB.Shared.Tiles;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace GB.GBTD.Dialogs
{
	public partial class PalettesDialog : Form
	{
		/// <summary>
		/// Origionally provided Palette; don't modify until saving.
		/// </summary>
		private readonly GBRObjectPalettes palettes;
		/// <summary>
		/// Palette that can be modified.
		/// </summary>
		private readonly PaletteSet workPalette;

		private readonly ColorSet colorSet;

		private GBColor selectedX = GBColor.WHITE;
		private int selectedY = 0;

		private PalettesDialog() {
			InitializeComponent();
		}

		public PalettesDialog(GBRObjectPalettes palettes, ColorSet colorSet) : this() {
			if (!colorSet.SupportsPaletteCustomization()) {
				throw new ArgumentException("Color set doesn't support customization!", "colorSet");
			}

			this.palettes = palettes;

			this.colorSet = colorSet;
			switch (colorSet) {
			case ColorSet.GAMEBOY_COLOR:
				workPalette = (PaletteSet)palettes.GBCPalettes.Clone();
				colorPicker.DisplayImage = global::GB.GBTD.Properties.Resources.GAMMA;
				break;
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				workPalette = (PaletteSet)palettes.GBCPalettes.Clone();
				colorPicker.DisplayImage = global::GB.GBTD.Properties.Resources.GBCGAMMA;
				break;
			case ColorSet.SUPER_GAMEBOY:
				workPalette = (PaletteSet)palettes.SGBPalettes.Clone();
				colorPicker.DisplayImage = global::GB.GBTD.Properties.Resources.GAMMA;
				break;
			default: throw new InvalidEnumArgumentException("colorSet", (int)colorSet, typeof(ColorSet));
			}

			colorPicker.ColorSet = colorSet;
			colorPicker.SelectedColor = workPalette[0][0];

			this.groupBox.Text = colorSet.GetDisplayName() + " palettes";

			this.SuspendLayout();


			for (int i = 0; i < colorSet.GetNumberOfRows(); i++) {
				CleanLabel label = new CleanLabel();
				label.Text = i.ToString();
				label.Name = "label" + i;
				label.Location = new Point(14, 23 + (i * 28));

				groupBox.Controls.Add(label);

				PaletteButton whiteButton = new PaletteButton(this, GBColor.WHITE, i);
				whiteButton.Name = "palButtonWhite" + i;
				whiteButton.Location = new Point(36, 19 + (i * 28));
				PaletteButton lightGrayButton = new PaletteButton(this, GBColor.LIGHT_GRAY, i);
				lightGrayButton.Name = "palButtonLightGray" + i;
				lightGrayButton.Location = new Point(55, 19 + (i * 28));
				PaletteButton darkGrayButton = new PaletteButton(this, GBColor.DARK_GRAY, i);
				darkGrayButton.Name = "palButtonDarkGray" + i;
				darkGrayButton.Location = new Point(74, 19 + (i * 28));
				PaletteButton blackButton = new PaletteButton(this, GBColor.BLACK, i);
				blackButton.Name = "palButtonBlack" + i;
				blackButton.Location = new Point(93, 19 + (i * 28));

				groupBox.Controls.Add(whiteButton);
				groupBox.Controls.Add(lightGrayButton);
				groupBox.Controls.Add(darkGrayButton);
				groupBox.Controls.Add(blackButton);
			}

			this.ResumeLayout();
		}

		private void okButton_Click(object sender, EventArgs e) {
			switch (colorSet) {
			case ColorSet.GAMEBOY_COLOR:
				for (int i = 0; i < palettes.GBCPalettes.Size; i++) {
					palettes.GBCPalettes[i] = workPalette[i];
				}
				break;
			case ColorSet.GAMEBOY_COLOR_FILTERED:
				for (int i = 0; i < palettes.GBCPalettes.Size; i++) {
					palettes.GBCPalettes[i] = workPalette[i];
				}
				break;
			case ColorSet.SUPER_GAMEBOY:
				for (int i = 0; i < palettes.SGBPalettes.Size; i++) {
					palettes.SGBPalettes[i] = workPalette[i];
				}
				break;
			}

			this.DialogResult = DialogResult.OK;

			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// PaletteButtons will call this after they change selectedX and selectedY.
		/// </summary>
		private void OnPaletteButtonClicked() {
			colorPicker.SelectedColor = workPalette[selectedY][selectedX];

			this.Invalidate(true);
		}

		private void colorPicker_SelectedColorChanged(object sender, EventArgs e) {
			workPalette[selectedY][selectedX] = colorPicker.SelectedColor;

			if (colorSet == ColorSet.SUPER_GAMEBOY && selectedX == GBColor.WHITE) {
				for (int i = 0; i < workPalette.Size; i++) {
					workPalette[i][GBColor.WHITE] = colorPicker.SelectedColor;
				}
			}

			this.Invalidate(true);
		}

		private class PaletteButton : Control
		{
			protected override Size DefaultSize { get { return new Size(19, 19); } }

			private readonly PalettesDialog parent;
			private readonly GBColor ID;
			private readonly int row;

			internal PaletteButton(PalettesDialog parent, GBColor ID, int row) {
				DoubleBuffered = true;
				SetStyle(ControlStyles.ResizeRedraw, true);

				this.parent = parent;
				this.ID = ID;
				this.row = row;

				this.Size = new Size(19, 19);
			}

			protected override void OnPaint(PaintEventArgs e) {
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				e.Graphics.SmoothingMode = SmoothingMode.None;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
				e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

				Color drawColor = parent.workPalette[row][ID];
				if (parent.colorSet.IsFiltered()) {
					drawColor = drawColor.FilterWithGBC();
				}

				using (Brush b = new SolidBrush(drawColor)) {
					e.Graphics.FillRectangle(b, 0, 0, Width - 1, Height - 1);
				}

				e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

				if (this.ID == parent.selectedX && this.row == parent.selectedY) {
					e.Graphics.DrawRectangle(SystemPens.Highlight, 1, 1, Width - 3, Height - 3);
				}

				StringFormat format = new StringFormat();
				format.Alignment = StringAlignment.Center;
				format.LineAlignment = StringAlignment.Center;

				int colorNum;
				switch (this.ID) {
				case GBColor.WHITE: colorNum = 0; break;
				case GBColor.LIGHT_GRAY: colorNum = 1; break;
				case GBColor.DARK_GRAY: colorNum = 2; break;
				case GBColor.BLACK: colorNum = 3; break;
				default: colorNum = (int)ID; break;
				}

				int count = 0;
				if (drawColor.R < 20) { count++; }
				if (drawColor.G < 20) { count++; }
				if (drawColor.B < 20) { count++; }

				if (count > 2) {
					e.Graphics.DrawString(colorNum.ToString(), Font, Brushes.White, new Rectangle(0, 0, Width, Height), format);
				} else {
					e.Graphics.DrawString(colorNum.ToString(), Font, Brushes.Black, new Rectangle(0, 0, Width, Height), format);
				}

				base.OnPaint(e);
			}

			protected override void OnMouseDown(MouseEventArgs e) {
				if (e.Button.HasFlag(MouseButtons.Left)) {
					parent.selectedX = this.ID;
					parent.selectedY = this.row;
					parent.OnPaletteButtonClicked();
				}

				base.OnMouseClick(e);
			}
		}

		private void copyAllButton_Click(object sender, EventArgs e) {
			StringBuilder builder = new StringBuilder();
			for (int y = 0; y < workPalette.Size; y++) {
				for (int x = 0; x < 4; x++) {
					builder.Append(ColorToString(workPalette[y][x]));

					if (!(y == workPalette.Size - 1 && x == 3)) {
						builder.AppendLine();
					}
				}
			}

			Clipboard.SetText(builder.ToString());
		}

		private void copyButton_Click(object sender, EventArgs e) {
			//Copy the current row.

			StringBuilder builder = new StringBuilder();

			builder.AppendLine(ColorToString(workPalette[selectedY][0]));
			builder.AppendLine(ColorToString(workPalette[selectedY][1]));
			builder.AppendLine(ColorToString(workPalette[selectedY][2]));
			builder.Append(ColorToString(workPalette[selectedY][3]));

			Clipboard.SetText(builder.ToString());
		}

		private void pasteButton_Click(object sender, EventArgs e) {
			if (!Clipboard.ContainsText()) {
				return;
			}

			Color[] colors = StringToColors(Clipboard.GetText());

			int colorIndex = 0;

			for (int y = selectedY; y < workPalette.Size; y++) {
				for (int x = 0; x < 4; x++) {
					if (colorIndex >= colors.Length) {
						return;
					}

					workPalette[y][x] = colors[colorIndex];
					colorIndex++;
				}
			}

			this.Invalidate(true);
		}

		/// <summary>
		/// Converts a color into a clipboard-format color string.
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private String ColorToString(Color c) {
			return String.Format("{0}\t{1}\t{2}", c.R / 8, c.G / 8, c.B / 8);
		}

		/// <summary>
		/// Converts a clipboard-format colors string into the colors themselves.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		private Color[] StringToColors(String s) {
			var matches = Regex.Matches(s, @"(\d+)\t(\d+)\t(\d+)");

			Queue<Color> colors = new Queue<Color>();

			foreach (Match match in matches) {
				int r = Convert.ToInt32(match.Groups[1].Value);
				int g = Convert.ToInt32(match.Groups[2].Value);
				int b = Convert.ToInt32(match.Groups[3].Value);

				if (r > 31) { throw new ArgumentException("R value is greater than max (31)"); }
				if (r < 0) { throw new ArgumentException("R value is less than 0"); }
				if (g > 31) { throw new ArgumentException("G value is greater than max (31)"); }
				if (g < 0) { throw new ArgumentException("G value is less than 0"); }
				if (b > 31) { throw new ArgumentException("B value is greater than max (31)"); }
				if (b < 0) { throw new ArgumentException("B value is less than 0"); }

				colors.Enqueue(Color.FromArgb(r * 8, g * 8, b * 8));
			}

			return colors.ToArray();
		}
	}
}
