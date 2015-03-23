using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.GBMB
{
	public partial class ToolList : UserControl
	{
		protected override Size DefaultSize { get { return new Size(26, 174); } }
		protected override Size DefaultMaximumSize { get { return new Size(26, 174); } }
		protected override Size DefaultMinimumSize { get { return new Size(26, 174); } }

		public ToolList() {
			InitializeComponent();

			SetStyle(ControlStyles.FixedHeight | ControlStyles.FixedWidth, true);
		}

		[Description("Fires when the selected tool has been changed.")]
		public event EventHandler SelectedToolChanged;
		[Description("Fires when the add row button is clicked.")]
		public event EventHandler AddRowClicked;
		[Description("Fires when the add column button is clicked.")]
		public event EventHandler AddColumnClicked;
		[Description("Fires when the remove row button is clicked.")]
		public event EventHandler RemoveRowClicked;
		[Description("Fires when the remove column button is clicked.")]
		public event EventHandler RemoveColumnClicked;
		[Description("Fires when auto update is enabled or disabled.")]
		public event EventHandler AutoUpdateChanged;

		protected void OnSelectedToolChanged(object sender = null, EventArgs args = null) {
			if (SelectedToolChanged != null) {
				SelectedToolChanged(this, new EventArgs());
			}
		}

		protected void OnAddRowClicked(object sender = null, EventArgs args = null) {
			if (AddRowClicked != null) {
				AddRowClicked(this, new EventArgs());
			}
		}
		protected void OnAddColumnClicked(object sender = null, EventArgs args = null) {
			if (AddColumnClicked != null) {
				AddColumnClicked(this, new EventArgs());
			}
		}
		protected void OnRemoveRowClicked(object sender = null, EventArgs args = null) {
			if (RemoveRowClicked != null) {
				RemoveRowClicked(this, new EventArgs());
			}
		}
		protected void OnRemoveColumnClicked(object sender = null, EventArgs args = null) {
			if (RemoveColumnClicked != null) {
				RemoveColumnClicked(this, new EventArgs());
			}
		}

		protected void OnAutoUpdateChanged(object sender = null, EventArgs args = null) {
			if (AutoUpdateChanged != null) {
				AutoUpdateChanged(this, new EventArgs());
			}
		}

		/// <summary>
		/// The currently selected tool.
		/// </summary>
		[Description("The currently selected tool.")]
		public Tool SelectedTool {
			get {
				if (penRadioButton.Checked) {
					return Tool.PEN;
				}
				if (floodRadioButton.Checked) {
					return Tool.FLOOD;
				}
				if (dropperRadioButton.Checked) {
					return Tool.DROPPER;
				}
				return Tool.NONE;
			}
			set {
				switch (value) {
				case Tool.PEN: penRadioButton.Checked = true; floodRadioButton.Checked = dropperRadioButton.Checked = false; break;
				case Tool.FLOOD: floodRadioButton.Checked = true; penRadioButton.Checked = dropperRadioButton.Checked = false; break;
				case Tool.DROPPER: dropperRadioButton.Checked = true; penRadioButton.Checked = floodRadioButton.Checked = false; break;
				default: penRadioButton.Checked = floodRadioButton.Checked = dropperRadioButton.Checked = false; break;
				}

				OnSelectedToolChanged();
			}
		}

		/// <summary>
		/// Whether or not AutoUpdate is enabled.
		/// </summary>
		[Description("Whether or not AutoUpdate is enabled.")]
		public bool AutoUpdate {
			get { return autoUpdateCheckBox.Checked; }
			set { autoUpdateCheckBox.Checked = value; }
		}
	}

	/// <summary>
	/// Different types of tools.
	/// TODO move this to a better file.
	/// </summary>
	public enum Tool
	{
		NONE, PEN, FLOOD, DROPPER
	}
}
