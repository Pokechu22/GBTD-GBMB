using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace GB.Shared.Palette
{
	class PaletteObjectsEditor<TForm, TSelector, TSet, TRow, TEntry> : UITypeEditor
		where TForm : ChoosePalette<TSelector, TSet, TRow, TEntry>, new()
		where TSelector : GBPaletteSetSelector<TSet, TRow, TEntry>, new()
		where TSet : PaletteSetBase<TRow, TEntry>, new()
		where TRow : PaletteBase<TEntry>
		where TEntry : PaletteEntryBase
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

				form.Set = (TSet)value;

				editorService.ShowDialog(form);

				return form.Set.Clone();
			}

			return value;
		}

		public override void PaintValue(PaintValueEventArgs e) {
			TSet set = e.Value as TSet;
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
