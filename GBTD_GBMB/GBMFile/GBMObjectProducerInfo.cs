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
			s.WriteString(Version, 128);
			s.WriteString(Info, 128);
		}

		protected override void LoadFromStream(Stream s) {
			Name = s.ReadString(128);
			Version = s.ReadString(10);
			Info = s.ReadString(128);
		}

		public override string GetTypeName() {
			return "Producer info";
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
