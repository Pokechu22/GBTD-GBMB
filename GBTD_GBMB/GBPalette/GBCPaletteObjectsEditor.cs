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
	class GBCPaletteObjectsEditor : UITypeEditor
	{
		IWindowsFormsEditorService editorService = null;

		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context) {
			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
			if (provider != null) {
				editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
			}

			if (editorService != null) {
				GBCPaletteSetSelector chooser = new GBCPaletteSetSelector();

				chooser.Set = (GBCPaletteSet)value;

				editorService.DropDownControl(chooser);

				value = chooser.Set;
			}

			return value;
		}
	}
}
