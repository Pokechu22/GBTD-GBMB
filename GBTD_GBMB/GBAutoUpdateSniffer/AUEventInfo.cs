using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GB.Shared.AutoUpdate;

namespace GBAutoUpdateSniffer
{
	/// <summary>
	/// Contains a bit of info about a
	/// </summary>
	internal class AUEventInfo : IFormattable
	{
		public readonly UInt16? TileNumber;
		public readonly MessageEventArgs Args;
		public readonly AUEventType Type;

		public readonly DateTime time;

		public AUEventInfo(MessageEventArgs args, AUEventType type) {
			this.Args = args;
			this.TileNumber = null;
			this.Type = type;

			this.time = DateTime.Now;
		}

		public AUEventInfo(TileChangedEventArgs args, AUEventType type) {
			this.Args = args;
			this.TileNumber = args.TileID;
			this.Type = type;

			this.time = DateTime.Now;
		}

		public override string ToString() {
			return this.ToString("A", null);
		}

		/// <summary>
		/// Provides simple formating options.
		/// There's only two things for format: "S" for simple and "A" for all (any other format is treated as "A").
		/// </summary>
		public string ToString(string format, IFormatProvider formatProvider) {
			StringBuilder result = new StringBuilder();

			if (format.Contains('S') || format.Contains('s')) {
				result.Append(Type.ToString().Replace('_', ' '));
				if (TileNumber.HasValue) {
					result.AppendFormat(" (Tile: {0})", TileNumber);
				}
			} else {
				result.AppendLine(time.ToString());
				result.AppendLine();
				result.AppendLine("Raw message: " + this.Args.Message);
				result.AppendLine();
				result.AppendLine("Of type " + Type.ToString().Replace('_', ' '));

				if (TileNumber.HasValue) {
					result.AppendLine("Tile number: " + TileNumber);
				}
			}

			return result.ToString();
		}
	}

	/// <summary>
	/// Types of auto update event.
	/// Intended to be human-visible.
	/// </summary>
	internal enum AUEventType
	{
		Single_tile,
		Total_refresh,
		Tile_refresh,
		Tile_size,
		Tile_palette,
		Color_set_change
	}
}
