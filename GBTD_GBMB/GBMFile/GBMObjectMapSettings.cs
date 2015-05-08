using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	public class GBMObjectMapSettings : MasteredGBMObject<GBMObjectMap>
	{
		public GBMObjectMapSettings(UInt16 UniqueID, GBMFile File) : base(UniqueID, File) {
			this.FormWidth = 390;
			this.FormHeight = 288;
			this.FormMaximized = false;
			this.ShowInfoPanel = true;
			this.ShowGrid = true;
			this.ShowDoubleMarkers = false;
			this.ShowPropColors = false;
			this.Zoom = 2;
			this.ColorSet = 0;
			this.Bookmark1 = 0xFFFF;
			this.Bookmark2 = 0xFFFF;
			this.Bookmark3 = 0xFFFF;
			this.BlockFillPattern = BlockFillMode.SELECTED_TILE;
			this.BlockFillWidth = 1;
			this.BlockFillHeight = 1;
			this.AutoUpdate = false;
		}

		public GBMObjectMapSettings(GBMObjectMap Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		/// <summary>
		/// The width of the form.
		/// </summary>
		public UInt32 FormWidth { get; set; }
		/// <summary>
		/// The height of the form.
		/// </summary>
		public UInt32 FormHeight { get; set; }
		/// <summary>
		/// Is the form maximized?
		/// </summary>
		public bool FormMaximized { get; set; }
		/// <summary>
		/// Whether the info panel is to be shown.
		/// </summary>
		public bool ShowInfoPanel { get; set; }
		/// <summary>
		/// Whether the grid is to be shown.
		/// </summary>
		public bool ShowGrid { get; set; }
		/// <summary>
		/// Whether the double markers (dots every 2 tiles) are to be shown.
		/// </summary>
		public bool ShowDoubleMarkers { get; set; }
		/// <summary>
		/// Whether property colors are to be shown.
		/// </summary>
		public bool ShowPropColors { get; set; }
		/// <summary>
		/// The current zoom level.
		/// 
		/// TODO: Enum?
		/// </summary>
		public UInt16 Zoom { get; set; }
		/// <summary>
		/// The current colorset.
		/// 
		/// TODO Replace with the actual ColorSet enum.
		/// </summary>
		public UInt16 ColorSet { get; set; }
		/// <summary>
		/// The first bookmarked tile.
		/// </summary>
		public UInt16 Bookmark1 { get; set; }
		/// <summary>
		/// The second bookmarked tile.
		/// </summary>
		public UInt16 Bookmark2 { get; set; }
		/// <summary>
		/// The third bookmarked tile.
		/// </summary>
		public UInt16 Bookmark3 { get; set; }
		/// <summary>
		/// The block fill pattern.
		/// </summary>
		public BlockFillMode BlockFillPattern { get; set; }
		/// <summary>
		/// The width of block fill.
		/// </summary>
		public UInt32 BlockFillWidth { get; set; }
		/// <summary>
		/// The height of block fill.
		/// </summary>
		public UInt32 BlockFillHeight { get; set; }
		/// <summary>
		/// Whether or not autoupdate is enabled.
		/// </summary>
		/// <remarks>This is not in the origional specs but is still created.</remarks>
		public bool AutoUpdate { get; set; }

		protected override void LoadFromStream(Stream s) {
			this.FormWidth = s.ReadInteger();
			this.FormHeight = s.ReadInteger();
			this.FormMaximized = s.ReadBoolean();
			this.ShowInfoPanel = s.ReadBoolean();
			this.ShowGrid = s.ReadBoolean();
			this.ShowDoubleMarkers = s.ReadBoolean();
			this.ShowPropColors = s.ReadBoolean();
			this.Zoom = s.ReadWord();
			this.ColorSet = s.ReadWord();
			this.Bookmark1 = s.ReadWord();
			this.Bookmark2 = s.ReadWord();
			this.Bookmark3 = s.ReadWord();
			this.BlockFillPattern = (BlockFillMode)s.ReadInteger();
			this.BlockFillWidth = s.ReadInteger();
			this.BlockFillHeight = s.ReadInteger();
			this.AutoUpdate = s.ReadBoolean(false);
		}

		protected override void SaveToStream(Stream s) {
			s.WriteInteger(FormWidth);
			s.WriteInteger(FormHeight);
			s.WriteBoolean(FormMaximized);
			s.WriteBoolean(ShowInfoPanel);
			s.WriteBoolean(ShowGrid);
			s.WriteBoolean(ShowDoubleMarkers);
			s.WriteBoolean(ShowPropColors);
			s.WriteWord(Zoom);
			s.WriteWord(ColorSet);
			s.WriteWord(Bookmark1);
			s.WriteWord(Bookmark2);
			s.WriteWord(Bookmark3);
			s.WriteInteger((uint)BlockFillPattern);
			s.WriteInteger(BlockFillWidth);
			s.WriteInteger(BlockFillHeight);
			s.WriteBoolean(AutoUpdate);
		}

		public override string GetTypeName() {
			return "Map settings";
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = CreateRootTreeNode();

			node.Nodes.Add("FormWidth", "FormWidth: " + FormWidth);
			node.Nodes.Add("FormHeight", "FormHeight: " + FormHeight);
			node.Nodes.Add("FormMaximized", "FormMaximized: " + FormMaximized);
			node.Nodes.Add("ShowInfoPanel", "ShowInfoPanel: " + ShowInfoPanel);
			node.Nodes.Add("ShowGrid", "ShowGrid: " + ShowGrid);
			node.Nodes.Add("ShowDoubleMarkers", "ShowDoubleMarkers: " + ShowDoubleMarkers);
			node.Nodes.Add("ShowPropColors", "ShowPropColors: " + ShowPropColors);
			node.Nodes.Add("Zoom", "Zoom: " + Zoom);
			node.Nodes.Add("ColorSet", "ColorSet: " + ColorSet);

			TreeNode bookmarks = new TreeNode("Bookmarks");
			bookmarks.Nodes.Add("1", "1: " + Bookmark1);
			bookmarks.Nodes.Add("2", "2: " + Bookmark2);
			bookmarks.Nodes.Add("3", "3: " + Bookmark3);
			node.Nodes.Add(bookmarks);

			node.Nodes.Add("BlockFillPattern", "BlockFillPattern: " + BlockFillPattern);
			node.Nodes.Add("BlockFillWidth", "BlockFillWidth: " + BlockFillWidth);
			node.Nodes.Add("BlockFillHeight", "BlockFillHeight: " + BlockFillHeight);

			node.Nodes.Add("AutoUpdate", "AutoUpdate: " + AutoUpdate);

			return node;
		}
	}

	public enum BlockFillMode : uint
	{
		SELECTED_TILE = 0,
		LEFT_TO_RIGHT = 1,
		LEFT_TO_RIGHT_TOP_TO_BOTTOM = 2,
		TOP_TO_BOTTOM = 3,
		TOP_TO_BOTTOM_LEFT_TO_RIGHT = 4,
		RIGHT_TO_LEFT = 5,
		RIGHT_TO_LEFT_TOP_TO_BOTTOM = 6,
		BOTTOM_TO_TOP = 7,
		BOTTOM_TO_TOP_RIGHT_TO_LEFT = 8
	}
}
