using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// This object contains basic information about a map, and is the master for various Objects contains the actual data.
	/// <para>This does not (appear to) store the actual tiles; instead it stores settings.</para>
	/// </summary>
	public class GBMObjectMap : GBMObject
	{
		public GBMObjectMap(UInt16 UniqueID) : this(UniqueID, 10, 10) { }

		public GBMObjectMap(UInt16 UniqueID, String Name) : this(UniqueID, Name, 10, 10) { }

		public GBMObjectMap(UInt16 UniqueID, UInt32 Width, UInt32 Height) : this(UniqueID, "", Width, Height) { }

		public GBMObjectMap(UInt16 UniqueID, String Name, UInt32 Width, UInt32 Height) : this(UniqueID, Name, Width, Height, 0, "", 0, 2) { }

		public GBMObjectMap(UInt16 UniqueID, String Name, UInt32 Width, UInt32 Height, UInt32 PropCount, String TileFileName, UInt32 TileCount, 
				UInt32 PropColorCount) : base(UniqueID) {
			
			this.Name = Name;

			this.width = Width;
			this.height = Height;

			this.propCount = PropCount;

			this.tileFileName = TileFileName;

			this.tileCount = TileCount;

			this.propColorCount = PropColorCount;
		}

		public GBMObjectMap(GBMObject Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }

		private UInt32 width, height;
		private UInt32 propCount, propColorCount;
		private String tileFileName;
		private UInt32 tileCount;

		/// <summary>
		/// The name of the map (currently ignored)
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// The width of the map.
		/// </summary>
		public UInt32 Width {
			get { return width; }
			set { width = value; if (SizeChanged != null) { SizeChanged(this, new EventArgs()); } }
		}
		/// <summary>
		/// The height of the map.
		/// </summary>
		public UInt32 Height {
			get { return height; }
			set { height = value; if (SizeChanged != null) { SizeChanged(this, new EventArgs()); } }
		}
		/// <summary>
		/// The number of properties.
		/// </summary>
		public UInt32 PropCount {
			get { return propCount; }
			set { propCount = value; if (PropCountChanged != null) { PropCountChanged(this, new EventArgs()); } }
		}
		/// <summary>
		/// The name of the GBR file.
		/// </summary>
		public String TileFile {
			get { return tileFileName; }
			set { tileFileName = value; if (TileFileChanged != null) { TileFileChanged(this, new EventArgs()); } }
		}
		/// <summary>
		/// The number of tiles in the GBR file.
		/// <para>
		/// Note from the specs: 
		/// TileCount does not actually refer to the number of tiles in the GBR-file, as this can be easily changed without the 
		/// GBM-file “knowing” about it. It is used to determine the size of tile-related information in the GBM-file, 
		/// mainly the default properties.
		/// </para>
		/// </summary>
		public UInt32 TileCount {
			get { return tileCount; }
			set { tileCount = value; if (TileCountChanged != null) { TileCountChanged(this, new EventArgs()); } }
		}
		/// <summary>
		/// Number of property colors.
		/// </summary>
		public UInt32 PropColorCount {
			get { return propColorCount; }
			set { propColorCount = value; if (PropColorCountChanged != null) { PropColorCountChanged(this, new EventArgs()); } }
		}

		/// <summary>
		/// Fires when the map is resized, either by changing <see cref="Width"/> or changing <see cref="Height"/>.
		/// </summary>
		public event EventHandler SizeChanged;
		/// <summary>
		/// Fires when <see cref="PropCount"/> is changed.
		/// </summary>
		public event EventHandler PropCountChanged;
		/// <summary>
		/// Fires when <see cref="TileFile"/> is changed.
		/// </summary>
		public event EventHandler TileFileChanged;
		/// <summary>
		/// Fires when <see cref="TileCount"/> is changed.
		/// Please read the disclaimer there; this does NOT represent the amount of tiles in the GBR file.
		/// </summary>
		public event EventHandler TileCountChanged;
		/// <summary>
		/// Fires when <see cref="PropColorCount"/> is changed.
		/// </summary>
		public event EventHandler PropColorCountChanged;

		protected override void SaveToStream(Stream s) {
			s.WriteString(Name, 128);
			s.WriteInteger(Width);
			s.WriteInteger(Height);
			s.WriteInteger(PropCount);
			s.WriteString(TileFile, 256);
			s.WriteInteger(TileCount);
			s.WriteInteger(PropColorCount);
		}

		protected override void LoadFromStream(Stream s) {
			Name = s.ReadString(128);
			Width = s.ReadInteger();
			Height = s.ReadInteger();
			PropCount = s.ReadInteger();
			TileFile = s.ReadString(256);
			TileCount = s.ReadInteger();
			PropColorCount = s.ReadInteger();
		}

		public override string GetTypeName() {
			return "Map";
		}

		public override TreeNode ToTreeNode() {
			TreeNode root = CreateRootTreeNode();

			root.Nodes.Add("Name", "Name: " + Name);
			root.Nodes.Add("Width", "Width: " + Width);
			root.Nodes.Add("Height", "Height: " + Height);
			root.Nodes.Add("PropCount", "PropCount: " + PropCount);
			root.Nodes.Add("TileFile", "TileFile: " + TileFile);
			root.Nodes.Add("TileCount", "TileCount: " + TileCount);
			root.Nodes.Add("PropColorCount", "PropColorCount: " + PropColorCount);

			return root;
		}

		/// <summary>
		/// Resizes the map, changing both <see cref="Width"/> and <see cref="Height"/> at the same time.
		/// </summary>
		/// <param name="newWidth">The new <see cref="Width"/>.</param>
		/// <param name="newHeight">The new <see cref="Height"/></param>
		public void Resize(uint newWidth, uint newHeight) {
			this.width = newWidth;
			this.height = newHeight;
			
			if (SizeChanged != null) { SizeChanged(this, new EventArgs()); }
		}
	}
}
