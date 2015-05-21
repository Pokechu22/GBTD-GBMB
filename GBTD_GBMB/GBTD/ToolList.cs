using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GB.Shared.Tiles;

namespace GB.GBTD
{
	public partial class ToolList : UserControl
	{
		//Sizes.
		protected override Size DefaultSize { get { return new Size(26, 217); } }
		protected override Size DefaultMaximumSize { get { return new Size(26, 217); } }
		protected override Size DefaultMinimumSize { get { return new Size(26, 217); } }
		//Margin.
		protected override Padding DefaultMargin { get { return new Padding(4); } }

		public ToolList() {
			InitializeComponent();
		}

		#region Events
		/// <summary>
		/// Triggers when the selected tool is changed.
		/// </summary>
		[Category("Action"), Description("Triggers when the selected tool is changed.")]
		public event EventHandler SelectedToolChanged;

		/// <summary>
		/// Triggers when the scroll up button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll up button is clicked.")]
		public event EventHandler ScrollUpClicked;
		/// <summary>
		/// Triggers when the scroll down button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll down button is clicked.")]
		public event EventHandler ScrollDownClicked;
		/// <summary>
		/// Triggers when the scroll left button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll left button is clicked.")]
		public event EventHandler ScrollLeftClicked;
		/// <summary>
		/// Triggers when the scroll right button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the scroll right button is clicked.")]
		public event EventHandler ScrollRightClicked;

		/// <summary>
		/// Triggers when the flip vertically button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the flip vertically button is clicked.")]
		public event EventHandler FlipVerticallyClicked;
		/// <summary>
		/// Triggers when the flip horizontally button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the flip horizontally button is clicked.")]
		public event EventHandler FlipHorizontallyClicked;
		/// <summary>
		/// Triggers when the rotate clockwise button is clicked.
		/// </summary>
		[Category("Action"), Description("Triggers when the rotate clockwise button is clicked.")]
		public event EventHandler RotateClockwiseClicked;

		/// <summary>
		/// Triggers when the Auto Update mode is changed.
		/// </summary>
		[Category("Action"), Description("Triggers when the Auto Update mode is changed.")]
		public event EventHandler AutoUpdateChanged;
		#endregion

		//The silly-looking parameters allow it to be called with or without args, and thus used both normally and as event handlers.
		//However, those arguments are never used.  They are merely there for convinience.

		private void OnSelectedToolChanged(object sender = null, EventArgs value = null) {
			if (SelectedToolChanged != null) {
				SelectedToolChanged(this, new EventArgs());
			}
		}

		private void OnScrollUpClicked(object sender = null, EventArgs value = null) {
			if (ScrollUpClicked != null) {
				ScrollUpClicked(this, new EventArgs());
			}
		}

		private void OnScrollLeftClicked(object sender = null, EventArgs value = null) {
			if (ScrollLeftClicked != null) {
				ScrollLeftClicked(this, new EventArgs());
			}
		}

		private void OnScrollRightClicked(object sender = null, EventArgs value = null) {
			if (ScrollRightClicked != null) {
				ScrollRightClicked(this, new EventArgs());
			}
		}

		private void OnScrollDownClicked(object sender = null, EventArgs value = null) {
			if (ScrollDownClicked != null) {
				ScrollDownClicked(this, new EventArgs());
			}
		}

		private void OnFlipVerticallyClicked(object sender = null, EventArgs value = null) {
			if (FlipVerticallyClicked != null) {
				FlipVerticallyClicked(this, new EventArgs());
			}
		}

		private void OnFlipHorizontallyClicked(object sender = null, EventArgs value = null) {
			if (FlipHorizontallyClicked != null) {
				FlipHorizontallyClicked(this, new EventArgs());
			}
		}

		private void OnRotateClockwiseClicked(object sender = null, EventArgs value = null) {
			if (RotateClockwiseClicked != null) {
				RotateClockwiseClicked(this, new EventArgs());
			}
		}

		private void OnAutoUpdateChanged(object sender = null, EventArgs value = null) {
			if (AutoUpdateChanged != null) {
				AutoUpdateChanged(this, new EventArgs());
			}
		}

		/// <summary>
		/// The currently selected tool.
		/// </summary>
		[Category("Data"), Description("The currently-selected tool.")]
		public TileEditorID SelectedTool {
			get {
				if (penButton.Checked) {
					return TileEditorID.PixelEdit;
				}
				if (floodButton.Checked) {
					return TileEditorID.FloodFill;
				}
				penButton.Checked = true;
				return TileEditorID.PixelEdit;
			}
			set {
				switch (value) {
				case TileEditorID.NoEdit:
					penButton.Checked = false;
					floodButton.Checked = false;
					break;
				case TileEditorID.PixelEdit:
					penButton.Checked = true;
					floodButton.Checked = false;
					break;
				case TileEditorID.FloodFill:
					penButton.Checked = false;
					floodButton.Checked = true;
					break;
				default: throw new InvalidEnumArgumentException("value", (int)value, typeof(TileEditorID));
				}
				OnSelectedToolChanged();
			}
		}

		/// <summary>
		/// Should auto update?
		/// </summary>
		[Category("Data"), Description("Is automatic updatingFromTileList enabled?")]
		public bool AutoUpdate {
			get {
				return autoUpdateCheckbox.Checked;
			}
			set {
				if (value != autoUpdateCheckbox.Checked) {
					autoUpdateCheckbox.Checked = value;
					OnAutoUpdateChanged();
				}
			}
		}
	}
}
