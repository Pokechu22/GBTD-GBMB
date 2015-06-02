using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBRFile
{
	/// <summary>
	/// Contains information about which app created the file.
	/// </summary>
	public class GBRObjectProducerInfo : GBRObject
	{
		public GBRObjectProducerInfo(UInt16 UniqueID) : base(UniqueID) {
			this.Name = "Gameboy Tile Designer";
			this.Version = "2.2";
			this.Info = "Home: www.casema.net/~hpmulder";
		}

		private string name, version, info;

		/// <summary>
		/// The name of the producing application.
		/// <para>WARNING: This will be clipped on saving if too large!</para>
		/// </summary>
		public string Name {
			get { return name; }
			set { if (value == null) { throw new ArgumentNullException("value"); } name = value; }
		}
		/// <summary>
		/// The version number of the producing application.
		/// <para>WARNING: This will be clipped on saving if too large!</para>
		/// </summary>
		public string Version {
			get { return version; }
			set { if (value == null) { throw new ArgumentNullException("value"); } version = value; }
		}
		/// <summary>
		/// Other miscalaneous info about the producing application.
		/// <para>WARNING: This will be clipped on saving if too large!</para>
		/// </summary>
		public string Info {
			get { return info; }
			set { if (value == null) { throw new ArgumentNullException("value"); } info = value; }
		}

		/// <summary>
		/// Writes the current app information to this object's data.
		/// </summary>
		public void UpdateWithCurrentApp() {
			var splitVersion = Application.ProductVersion.Split('.');

			//TODO: Use actual info.
			this.Name = "Gameboy Tile Designer";
			this.Version = "2.2";
			this.Info = "Home: www.casema.net/~hpmulder";
			//this.Name = "GBTB in C# v" + Application.ProductVersion + " by Pokechu22";
			//this.Version = splitVersion[0] + "." + splitVersion[1];
			//this.Info = "By Pokechu22; a remake of Harry Mulder's GBTB.  See http://github.com/pokechu22/GBTD_GBMB.";
		}

		protected internal override void SaveToStream(GBRFile file, Stream s) {
			s.WriteString(name, 30);
			s.WriteString(version, 10);
			s.WriteString(info, 80);
		}

		protected internal override void LoadFromStream(GBRFile file, Stream s) {
			this.name = s.ReadString(30);
			this.version = s.ReadString(10);
			this.info = s.ReadString(80);
		}

		public override TreeNode ToTreeNode() {
			TreeNode returned = base.ToTreeNode();

			returned.Nodes.Add("name", "Name: " + name);
			returned.Nodes.Add("version", "Version: " + version);
			returned.Nodes.Add("info", "Info: " + info);

			return returned;
		}
	}
}
