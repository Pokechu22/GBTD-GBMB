/*using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace GB.Shared.Palette
{
	class PaletteObjectsEditor<TForm, GBPaletteSetSelector, PaletteSet, Palette_, PaletteEntry> : UITypeEditor
		where TForm : ChoosePalette<GBPaletteSetSelector, PaletteSet, Palette_, PaletteEntry>, new()
		where GBPaletteSetSelector : GBPaletteSetSelector, new()
		where PaletteSet : PaletteSetBase<Palette_, PaletteEntry>, new()
		where Palette_ : PaletteBase<PaletteEntry>
		where PaletteEntry : PaletteEntryBase
	{
		IWindowsFormsEditorService editorService = null;

		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context) {
			return UITypeEditorEditStyle.Modal;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
			if (provider != null) {
				editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
			}

			if (editorService != null) {
				TForm form = new TForm();

				//Clone the origional one so that modifications aren't made to it.
				form.Set = (PaletteSet)((PaletteSet)value).Clone();

				editorService.ShowDialog(form);

				if (form.DialogResult == System.Windows.Forms.DialogResult.OK) {
					return form.Set;
				}
			}

			return value;
		}

		public override void PaintValue(PaintValueEventArgs e) {
			PaletteSet set = e.Value as PaletteSet;
			if (set != null) {
				//Draws horizontally.
				float width = (float)e.Bounds.Width / 4f;
				float height = (float)e.Bounds.Height / (float)set.NumberOfRows;

				for (int x = 0; x < 4; x++) {
					for (int y = 0; y < set.NumberOfRows; y++) {
						using (Brush b = new SolidBrush(set[y][x])) {
							e.Graphics.FillRectangle(b, e.Bounds.X + (x * width), e.Bounds.Y + (y * height), width, height);
							//e.Graphics.DrawRectangle(Pens.Black, e.Bounds.X + (x * width), e.Bounds.Y + (y * height), width, height);
						}
					}
				}
			}
		}

		public override bool GetPaintValueSupported(ITypeDescriptorContext context) {
			return true;
		}
	}
}
*/