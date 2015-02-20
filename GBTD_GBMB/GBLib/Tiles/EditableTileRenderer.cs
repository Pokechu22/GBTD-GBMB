﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GB.Shared.Tiles
{
	public partial class EditableTileRenderer : TileRenderer
	{
		#region Private fields
		private GBColor leftMouseColor = GBColor.BLACK;
		private GBColor rightMouseColor = GBColor.WHITE;
		private GBColor middleMouseColor = GBColor.DARK_GRAY;
		private GBColor xButton1MouseColor = GBColor.WHITE;
		private GBColor xButton2MouseColor = GBColor.WHITE;

		private TileEditor editor = new NoEditTileEditor();
		#endregion

		#region Public Properties

		/// <summary>
		/// The color that is used for the left mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the left mouse button.")]
		public GBColor LeftMouseColor {
			get { return leftMouseColor; }
			set { leftMouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the right mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the right mouse button.")]
		public GBColor RightMouseColor {
			get { return rightMouseColor; }
			set { rightMouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the middle mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the middle mouse button.")]
		public GBColor MiddleMouseColor {
			get { return middleMouseColor; }
			set { middleMouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the xButton1 mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the xButton1 mouse button.")]
		public GBColor XButton1MouseColor {
			get { return xButton1MouseColor; }
			set { xButton1MouseColor = value; }
		}

		/// <summary>
		/// The color that is used for the xButton2 mouse button.
		/// </summary>
		[Category("Misc"), Description("The color that is used for the xButton2 mouse button.")]
		public GBColor XButton2MouseColor {
			get { return xButton2MouseColor; }
			set { xButton2MouseColor = value; }
		}

		/// <summary>
		/// The editor that is used to edit the tileData.\nIf null, uses a NoEditTileEditor.
		/// </summary>
		[Category("Behavior"), Description("The editor that is used to edit the tileData.\nIf null, uses a NoEditTileEditor.")]
		public TileEditor Editor {
			get {
				return editor;
			}
			set {
				if (value == null) {
					value = new NoEditTileEditor();
				}
				editor = value;
			}
		}

		/// <summary>
		/// Sets the type used for the editor, based off of a <see cref="TileEditorID"/>.
		/// </summary>
		[Category("Behavior"), Description("Sets the type used for the editor, based off of a TileEditorID.")]
		public TileEditorID EditorTypeID {
			set {
				editor = TileEditorProvider.GetEditorForID(value);
			}
			get {
				return editor.GetID();
			}
		}
		#endregion


		public EditableTileRenderer() {
			
		}

		protected override void OnPixelClicked(PixelClickEventArgs e) {
			GBColor color;
			//Try to paletteData the color to the dictionary value; if it fails use black.
			if (e.mouseButton.HasFlag(MouseButtons.XButton2)) {
				color = xButton2MouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.XButton1)) {
				color = xButton1MouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.Middle)) {
				color = middleMouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.Right)) {
				color = rightMouseColor;
			} else if (e.mouseButton.HasFlag(MouseButtons.Left)) {
				color = leftMouseColor;
			} else {
				color = GBColor.WHITE;
			}

			Tile = editor.EditTile(Tile, e.x, e.y, color);

			base.OnPixelClicked(e);
		}
	}
}
