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
		public GBRObjectProducerInfo(UInt16 TypeID, UInt16 UniqueID, UInt32 Size, Stream stream) : base(TypeID, UniqueID, Size, stream) { }
		public GBRObjectProducerInfo(GBRObjectHeader header, Stream stream) : base(header, stream) { }

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

		protected override void SaveToStream(Stream s) {
			s.WriteString(name, 30);
			s.WriteString(version, 10);
			s.WriteString(info, 80);
		}

		protected override void LoadFromStream(Stream s) {
			this.name = s.ReadString(30);
			this.version = s.ReadString(10);
			this.info = s.ReadString(80);
		}

		public override string GetTypeName() {
			return "Producer info";
		}

		public override TreeNode ToTreeNode() {
			TreeNode returned = CreateRootTreeNode();

			returned.Nodes.Add("name", "Name: " + name);
			returned.Nodes.Add("version", "Version: " + version);
			returned.Nodes.Add("info", "Info: " + info);

			AddExtraDataToTreeNode(returned);

			return returned;
		}
	}
}
