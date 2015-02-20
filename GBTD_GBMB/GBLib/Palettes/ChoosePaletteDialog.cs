using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GB.Shared.Palettes;

namespace GB.Shared.Palettes
{
	public partial class ChoosePalette : Form
	{
		public ChoosePalette() {
			InitializeComponent();
			//initClipboardChangeCheck();
		}

		public ChoosePalette(PaletteData data, ColorSet set) : this() {
			this.Palette = data;
			this.ColorSet = set;
		}

		public PaletteData Palette {
			get { return gbPaletteSetSelector1.PaletteData; }
			set { gbPaletteSetSelector1.PaletteData = value; }
		}

		public ColorSet ColorSet {
			get { return gbPaletteSetSelector1.SelectedSet; }
			set { 
				gbPaletteSetSelector1.SelectedSet = value;
				this.colorPicker1.GBCFilter = value.IsFiltered();
			}
		}

		private void colorPicker1_OnChange(object sender, EventArgs e) {
			try {
				gbPaletteSetSelector1.SelectedColor = colorPicker1.MainViewColor;
			} catch (Exception) { }
		}

		private void gbPaletteSetSelector1_SelectionChanged(object sender, EventArgs e) {
			colorPicker1.Enabled = (gbPaletteSetSelector1.SelectedX != -1 && gbPaletteSetSelector1.SelectedY != -1);
			if (colorPicker1.Enabled) {
				colorPicker1.FirstColor = gbPaletteSetSelector1.SelectedColor;
			}
		}

		private void initClipboardChangeCheck() {
			NativeMethods.AddClipboardFormatListener(Handle);
		}

		private void OnClipboardUpdate() {
			//TODO
		}

		protected override void WndProc(ref Message m) {
			if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE) {
				OnClipboardUpdate();
			}

			base.WndProc(ref m);
		}

		private void pasteButton_Click(object sender, EventArgs e) {
			/*if (!Clipboard.ContainsText()) { return; }
			if (this.gbPaletteSetSelector1.SelectedY < 0) { return; }

			PaletteSet set = this.PaletteData;
			set.StringToPaletteSet(ref set, Clipboard.GetText(), 0, this.gbPaletteSetSelector1.SelectedY);
			this.PaletteData = set;*/
		}

		private void copyButton_Click(object sender, EventArgs e) {
			/*if (this.gbPaletteSetSelector1.SelectedY < 0) { return; }

			Clipboard.SetText(this.PaletteData[this.gbPaletteSetSelector1.SelectedY].PaletteToString());*/
		}

		private void copyAllButton_Click(object sender, EventArgs e) {
			//Clipboard.SetText(this.PaletteData.PaletteSetToString());
		}
	}
}
