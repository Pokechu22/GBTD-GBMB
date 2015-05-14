using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using GB.Shared.Tiles;
using GB.Shared.Palettes;

namespace GB.Shared.GBRFile
{
	public class GBRObjectTileSettings : ReferentialGBRObject<GBRObjectTileData>
	{
		public GBRObjectTileSettings(UInt16 UniqueID) : base(UniqueID) {
			//TODO: Set defaults
		}

		/// <summary>
		/// The constant value that is used to represent a non-bookmarked-tile.
		/// </summary>
		public const UInt16 NON_BOOKMAKRED_NUMBER = UInt16.MaxValue;

		#region Since initial version
		/// <summary>
		/// Whether or not simple mode is used.
		/// </summary>
		/// <remarks>Since: Included always.</remarks>
		public Boolean SimpleMode { get; set; }
		
		/// <summary>
		/// Whether or not the grid is enabled.
		/// </summary>
		/// <remarks>Since: Included always.</remarks>
		public Boolean ShowGrid { get; set; }
		/// <summary>
		/// Whether or not the NibbleMarkers are enabled.
		/// </summary>
		/// <remarks>Since: Included always.</remarks>
		public Boolean ShowNibbleMarkers { get; set; }

		/// <summary>
		/// The color used for the Left Mouse button.
		/// </summary>
		/// <remarks>Since: Included always.</remarks>
		public GBColor LeftColor { get; set; }
		/// <summary>
		/// The color used for the Right Mouse button.
		/// </summary>
		/// <remarks>Since: Included always.</remarks>
		public GBColor RightColor { get; set; }
		#endregion

		#region Since GBTD 0.9
		/// <summary>
		/// The width for split copy/paste.
		/// </summary>
		/// <remarks>Since: GBTD 0.9</remarks>
		public UInt16 SplitWidth { get; set; }
		/// <summary>
		/// The height for split copy/paste.
		/// </summary>
		/// <remarks>Since: GBTD 0.9</remarks>
		public UInt16 SplitHeight { get; set; }
		/// <summary>
		/// The order for split copy/paste: 
		/// <para>0:  Left to right, top to bottom</para>
		/// <para>1:  Top to bottom, left to right</para>
		/// </summary>
		/// <remarks>Since: GBTD 0.9</remarks>
		public SplitOrder SplitOrder { get; set; }
		#endregion

		#region Since GBTD 1.0
		/// <summary>
		/// The currently selected color set.
		/// <para>0:  Pocket</para>
		/// <para>1:  GameBoy</para>
		/// <para>2:  GBC</para>
		/// <para>3:  SGB</para>
		/// </summary>
		/// <remarks>Since: GBTD 1.0</remarks>
		public ColorSet ColorSet { get; set; }
		#endregion

		#region Since GBTD 1.1
		/// <summary>
		/// The first bookmarked tile.
		/// </summary>
		/// <remarks>Since: GBTD 1.1</remarks>
		public UInt16 Bookmark1 { get; set; }
		/// <summary>
		/// The second bookmarked tile.
		/// </summary>
		/// <remarks>Since: GBTD 1.1</remarks>
		public UInt16 Bookmark2 { get; set; }
		/// <summary>
		/// The third bookmarked tile.
		/// </summary>
		/// <remarks>Since: GBTD 1.1</remarks>
		public UInt16 Bookmark3 { get; set; }
		#endregion

		#region Since GBTD 2.0
		/// <summary>
		/// Whether or not auto update is enabled.
		/// </summary>
		/// <remarks>Since: GBTD 2.0</remarks>
		public bool AutoUpdate { get; set; }
		#endregion


		#region Since this version
		/// <summary>
		/// The color used for the Middle Mouse button.
		/// </summary>
		/// <remarks>Since: this version.</remarks>
		public GBColor MiddleMouseColor { get; set; }
		/// <summary>
		/// The color used for the XButton1 Mouse button.
		/// </summary>
		/// <remarks>Since: this version.</remarks>
		public GBColor X1MouseColor { get; set; }
		/// <summary>
		/// The color used for the XButton2 Mouse button.
		/// </summary>
		/// <remarks>Since: this version.</remarks>
		public GBColor X2MouseColor { get; set; }
		#endregion

		protected override void SaveToStream(GBRFile file, Stream s) {
			base.SaveToStream(file, s);

			s.WriteBool(SimpleMode);
			
			//Create the flags. 
			byte flags = 0;
			if (this.ShowGrid) { flags |= 0x01; }
			if (this.ShowNibbleMarkers) { flags |= 0x02; }
			s.WriteByte(flags);

			s.WriteByte((byte)LeftColor);
			s.WriteByte((byte)RightColor);

			s.WriteWord(SplitWidth);
			s.WriteWord(SplitHeight);
			s.WriteByte((byte)SplitOrder);
			
			s.WriteByte((byte)ColorSet);

			s.WriteWord(Bookmark1);
			s.WriteWord(Bookmark2);
			s.WriteWord(Bookmark3);

			s.WriteBool(AutoUpdate);

			s.WriteByte((byte)MiddleMouseColor);
			s.WriteByte((byte)X1MouseColor);
			s.WriteByte((byte)X2MouseColor);
		}

		protected override void LoadFromStream(GBRFile file, Stream s) {
			base.LoadFromStream(file, s);

			this.SimpleMode = s.ReadBool(false);

			//Create the flags. 
			byte flags = s.ReadByte(0x00);
			this.ShowGrid = (flags & 0x01) != 0;
			this.ShowNibbleMarkers = (flags & 0x02) != 0;

			this.LeftColor = (GBColor)s.ReadByte((byte)GBColor.BLACK);
			this.RightColor = (GBColor)s.ReadByte((byte)GBColor.WHITE);

			SplitWidth = s.ReadWord(1);
			SplitHeight = s.ReadWord(1);
			SplitOrder = (SplitOrder)s.ReadByte((byte)SplitOrder.LEFT_TO_RIGHT_FIRST);

			ColorSet = (ColorSet)s.ReadByte((byte)ColorSet.GAMEBOY);

			Bookmark1 = s.ReadWord(NON_BOOKMAKRED_NUMBER);
			Bookmark2 = s.ReadWord(NON_BOOKMAKRED_NUMBER);
			Bookmark3 = s.ReadWord(NON_BOOKMAKRED_NUMBER);

			this.AutoUpdate = s.ReadBool(false);

			this.MiddleMouseColor = (GBColor)s.ReadByte((byte)GBColor.DARK_GRAY);
			this.X1MouseColor = (GBColor)s.ReadByte((byte)GBColor.BLACK);
			this.X2MouseColor = (GBColor)s.ReadByte((byte)GBColor.BLACK);
		}

		public override string GetTypeName() {
			return "Tile settings";
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = base.ToTreeNode();

			node.Nodes.Add("Simple mode: " + SimpleMode);

			TreeNode flags = new TreeNode("Flags"); //TODO include the numbers.
			flags.Nodes.Add("Grid: " + this.ShowGrid);
			flags.Nodes.Add("Nibble Markers: " + this.ShowNibbleMarkers);
			node.Nodes.Add(flags);

			node.Nodes.Add("Left color: " + LeftColor);
			node.Nodes.Add("Right color: " + RightColor);

			TreeNode splitSettings = new TreeNode("Split Copy/Paste settings");
			splitSettings.Nodes.Add("Split Width: " + SplitWidth);
			splitSettings.Nodes.Add("Split Height: " + SplitHeight);
			splitSettings.Nodes.Add("Split Order: " + SplitOrder + " (" + (byte)SplitOrder + ")");
			node.Nodes.Add(splitSettings);

			node.Nodes.Add("Color set: " + ColorSet + " (" + (byte)ColorSet + ")");

			TreeNode bookmarks = new TreeNode("Bookmarks");
			bookmarks.Nodes.Add("1: " + Bookmark1);
			bookmarks.Nodes.Add("2: " + Bookmark2);
			bookmarks.Nodes.Add("3: " + Bookmark3);
			node.Nodes.Add(bookmarks);

			node.Nodes.Add("AutoUpdate: " + AutoUpdate);

			node.Nodes.Add("Middle color: " + MiddleMouseColor);
			node.Nodes.Add("X1 color: " + X1MouseColor);
			node.Nodes.Add("X2 color: " + X2MouseColor);

			return node;
		}
	}

	/// <summary>
	/// Orders that are valid for <see cref="GBRObjectTileSettings.SplitOrder"/>.  
	/// </summary>
	public enum SplitOrder : byte
	{
		/// <summary>
		/// Ordered left to right and then top to bottom.
		/// </summary>
		LEFT_TO_RIGHT_FIRST,
		/// <summary>
		/// Ordered top to bottom and then left to right.
		/// </summary>
		TOP_TO_BOTTOM_FIRST
	}
}
