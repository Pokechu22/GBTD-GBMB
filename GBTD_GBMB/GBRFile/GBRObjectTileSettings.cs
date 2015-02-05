using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using GB.Shared.Tiles;

namespace GB.Shared.GBRFile
{
	public class GBRObjectTileSettings : GBRObject
	{
		public GBRObjectTileSettings(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRObjectTileSettings(GBRObjectHeader header, Stream stream) : base(header, stream) { }

		#region Since initial version
		/// <summary>
		/// The UUID of the object that is refered to by these settings; usually a <see cref="GBRObjectTileData"/>.
		/// </summary>
		/// <remarks>Since: Included always.</remarks>
		public UInt16 ReferedObjectID { get; set; }
		
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
		/// TODO: Make an enum.
		/// </summary>
		/// <remarks>Since: GBTD 0.9</remarks>
		public UInt32 SplitOrder { get; set; }
		#endregion

		#region Since GBTD 1.0
		/// <summary>
		/// The currently selected color set.
		/// <para>0:  Pocket</para>
		/// <para>1:  GameBoy</para>
		/// <para>2:  GBC</para>
		/// <para>3:  SGB</para>
		/// TODO: Make an enum.
		/// </summary>
		/// <remarks>Since: GBTD 1.0</remarks>
		public byte ColorSet { get; set; }
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


		#region Since this version //TODO: Not loadable yet.
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

		protected override void SaveToStream(Stream s) {
			s.WriteWord(ReferedObjectID);
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
			s.WriteLong(SplitOrder);

			s.WriteByte(ColorSet);

			s.WriteWord(Bookmark1);
			s.WriteWord(Bookmark2);
			s.WriteWord(Bookmark3);

			s.WriteBool(AutoUpdate);

			s.WriteByte((byte)MiddleMouseColor);
			s.WriteByte((byte)X1MouseColor);
			s.WriteByte((byte)X2MouseColor);
		}

		protected override void LoadFromStream(Stream s) {
			this.ReferedObjectID = s.ReadWord();
			this.SimpleMode = s.ReadBool(false);

			//Create the flags. 
			byte flags = s.ReadByte(0x00);
			this.ShowGrid = (flags & 0x01) != 0;
			this.ShowNibbleMarkers = (flags & 0x02) != 0;

			this.LeftColor = (GBColor)s.ReadByte((byte)GBColor.BLACK);
			this.RightColor = (GBColor)s.ReadByte((byte)GBColor.WHITE);

			SplitWidth = s.ReadWord(1);
			SplitHeight = s.ReadWord(1);
			SplitOrder = s.ReadLong(0);

			ColorSet = s.ReadByte(0);

			Bookmark1 = s.ReadWord();
			Bookmark2 = s.ReadWord();
			Bookmark3 = s.ReadWord();

			this.AutoUpdate = s.ReadBool(false);

			this.MiddleMouseColor = (GBColor)s.ReadByte((byte)GBColor.DARK_GRAY);
			this.X1MouseColor = (GBColor)s.ReadByte((byte)GBColor.BLACK);
			this.X2MouseColor = (GBColor)s.ReadByte((byte)GBColor.BLACK);
		}

		public override string GetTypeName() {
			throw new NotImplementedException();
		}

		public override TreeNode ToTreeNode() {
			throw new NotImplementedException();
		}
	}
}
