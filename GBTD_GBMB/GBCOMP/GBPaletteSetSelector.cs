using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBRenderer
{
	public partial class GBPaletteSetSelector : UserControl
	{
		#region Private inner classes
		/// <summary>
		/// Internal class which represents an individual entry.
		/// </summary>
		private class PalatteEntry : Label
		{
			/// <summary>
			/// Width/height of the individual control.
			/// </summary>
			private const int WIDTH = 19, HEIGHT = 19;

			/// <summary>
			/// Initial offset for each control (at no aditional offset)
			/// </summary>
			private const int X_OFFSET = 19, Y_OFFSET = 19;

			/// <summary>
			/// The spacing between each control.
			/// </summary>
			private const int X_SPACING = WIDTH, Y_SPACING = HEIGHT + 9;

			public readonly int x, y;
			private GBPaletteSetSelector selector;

			public PalatteEntry(GBPaletteSetSelector selector, int x, int y) {
				this.x = x;
				this.y = y;

				this.selector = selector;

				this.Text = x.ToString();
				this.Name = "entry_x" + x + "_y" + y;

				this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

				this.Size = new System.Drawing.Size(WIDTH, HEIGHT);
				this.Location = new System.Drawing.Point(X_OFFSET + (x * X_SPACING), Y_OFFSET + (y * Y_SPACING));

				this.TabIndex = (y * 4) + x;

				this.Paint += new PaintEventHandler(PalatteEntry_Paint);
				this.MouseDown += new MouseEventHandler(PalatteEntry_MouseDown);
			}

			/// <summary>
			/// Paints an individual entry.
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			internal void PalatteEntry_Paint(object sender, PaintEventArgs e) {
				if (sender is PalatteEntry) {
					e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
					e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

					PalatteEntry c = (PalatteEntry)sender;

					//Draw the main border.
					ControlPaint.DrawBorder(e.Graphics, new Rectangle(0, 0, c.Width, c.Height),
						Color.Black, ButtonBorderStyle.Solid);
					//If selected draw the inner border.
					if (selector.selectedX == c.x && selector.selectedY == c.y) {
						ControlPaint.DrawBorder(e.Graphics, new Rectangle(1, 1, c.Width - 2, c.Height - 2),
							SystemColors.Highlight, ButtonBorderStyle.Solid);
					}
				}
			}

			internal void PalatteEntry_MouseDown(object sender, MouseEventArgs e) {
				if (sender is PalatteEntry) {
					PalatteEntry entry = (PalatteEntry)sender;

					selector.selectedX = entry.x;
					selector.selectedY = entry.y;

					selector.Refresh();
				}
			}
		}
		#endregion

		#region Constants
		/// <summary>
		/// Total number of columns.
		/// </summary>
		public const int COLUMNS_MAX = 4;
		/// <summary>
		/// Total number of rows.
		/// </summary>
		public const int ROWS_MAX = 8;
		#endregion

		#region Private fields

		private PalatteEntry[,] entries = new PalatteEntry[ROWS_MAX, COLUMNS_MAX];
		
		#region Property clones
		/// <summary>
		/// The currently selected coordinates.  If -1 nothing is selected.
		/// </summary>
		private int selectedX = -1, selectedY = -1;

		private int rows = 8;
		#endregion
		#endregion

		#region Public properties
		/// <summary>
		/// The x-location currently selected.  -1 if none selected.
		/// </summary>
		[Description("The x-location currently selected.  -1 if none selected."), Category("Data")]
		public int SelectedX {
			get { return selectedX; }
			set {
				if ((value >= COLUMNS_MAX) || (value < -1)) {
					throw new ArgumentOutOfRangeException("SelectedX", "Value must be between 0 and " + (COLUMNS_MAX - 1) +  " inclusive, or -1 to represent no selection.");
				}
				selectedX = value;
			}
		}

		/// <summary>
		/// The x-location currently selected.  -1 if none selected.
		/// </summary>
		[Description("The x-location currently selected.  -1 if none selected."), Category("Data")]
		public int SelectedY {
			get { return selectedY; }
			set {
				if ((value >= ROWS_MAX) || (value < -1)) {
					throw new ArgumentOutOfRangeException("SelectedY", "Value must be between 0 and " + (ROWS_MAX - 1) + " inclusive, or -1 to represent no selection.");
				}
				selectedY = value;
			}
		}

		/// <summary>
		/// The number of rows to include.
		/// </summary>
		[Description("The number of rows to include."), Category("Data")]
		public int Rows {
			get { return rows; }
			set {
				if ((value > ROWS_MAX) || (value < 1)) {
					throw new ArgumentOutOfRangeException("Rows", "Value must be between 1 and " + (ROWS_MAX) + " inclusive.");
				}
				rows = value;
			}
		}
		#endregion

		public GBPaletteSetSelector() {
			InitializeComponent();
			addControls();
		}

		internal void addControls() {
			this.Controls.Clear();
			//Informational label.

			//Entries
			entries = new PalatteEntry[COLUMNS_MAX, rows];
			for (int y = 0; y < rows; y++) {
				for (int x = 0; x < COLUMNS_MAX; x++) {
					entries[x, y] = new PalatteEntry(this, x, y);
					this.Controls.Add(entries[x, y]);
				}
			}
		}
	}
}
