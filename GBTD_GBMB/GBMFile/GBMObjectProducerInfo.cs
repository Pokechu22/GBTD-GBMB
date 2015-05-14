using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace GB.Shared.GBMFile
{
	/// <summary>
	/// Contains information about the producer of the GBMFile
	/// </summary>
	public class GBMObjectProducerInfo : GBMObject
	{
		public GBMObjectProducerInfo(UInt16 UniqueID) : base(UniqueID) {
			this.UpdateWithCurrentApp();
		}

		public GBMObjectProducerInfo(UInt16 UniqueID, string Name, string Version, string Info) : base(UniqueID) {
			this.Name = Name;
			this.Version = Version;
			this.Info = Info;
		}

		public GBMObjectProducerInfo(GBMObject Master, GBMObjectHeader header, Stream stream) : base(Master, header, stream) { }
		
		/// <summary>
		/// The name of the producing app.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// The version of the application.
		/// </summary>
		public string Version { get; set; }
		/// <summary>
		/// Some additional info about the application.
		/// </summary>
		public string Info { get; set; }

		protected override void SaveToStream(Stream s) {
			s.WriteString(Name, 128);
			s.WriteString(Version, 10);
			s.WriteString(Info, 128);
		}

		protected override void LoadFromStream(Stream s) {
			Name = s.ReadString(128);
			Version = s.ReadString(10);
			Info = s.ReadString(128);
		}

		/// <summary>
		/// Writes the current app information to this object's data.
		/// </summary>
		public void UpdateWithCurrentApp() {
			var splitVersion = Application.ProductVersion.Split('.');

			this.Name = "Gameboy Map Builder";
			this.Version = "1.8";
			this.Info = "Home: http://www.casema.net/~hpmulder";
			//this.Name = "GBMB in C# v" + Application.ProductVersion + " by Pokechu22";
			//this.Version = splitVersion[0] + "." + splitVersion[1];
			//this.Info = "By Pokechu22; a remake of Harry Mulder's GBMB.  See http://github.com/pokechu22/GBTD_GBMB.";
		}

		public override TreeNode ToTreeNode() {
			TreeNode node = CreateRootTreeNode();

			node.Nodes.Add("Name: " + Name);
			node.Nodes.Add("Version: " + Version);
			node.Nodes.Add("Info: " + Info);

			return node;
		}
	}
}
