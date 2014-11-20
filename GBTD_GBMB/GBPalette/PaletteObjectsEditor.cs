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
		where TSet : IPaletteSet<TRow, TEntry>, new()
		where TRow : IPalette<TEntry>
		where TEntry : IPaletteEntry
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

				value = form.Set;
			}

			return value;
		}
	}
}
