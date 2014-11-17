﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;

using GB.Shared.Tile;

namespace GB.Shared.Palette
{
	/// <summary>
	/// An entire group of Palettes.
	/// </summary>
	/// <typeparam name="Row"></typeparam>
	public interface GBPaletteSet<Row> where Row : GBPalette<GBPaletteEntry>
	{
		/// <summary>
		/// The total number for rows.
		/// </summary>
		public abstract int NumberOfRows {
			get;
		}

		/// <summary>
		/// Each individual row.
		/// </summary>
		public abstract Row[] Rows {
			get;
			set;
		}
	}

	/// <summary>
	/// A single row of entries, 4 in length.
	/// </summary>
	/// <typeparam name="Entry"></typeparam>
	public interface GBPalette<Entry> where Entry : GBPaletteEntry
	{
		/// <summary>
		/// The 'White' color used on this.
		/// (White is in quotes because it could be anything)
		/// </summary>
		[Category("Data"), Description("The 'White' color used on this.\n(White is in quotes because it could be anything)")]
		public abstract Entry EntryWhite {
			get;
			set;
		}
		/// <summary>
		/// The 'Light gray' color used on this.
		/// (Light gray is in quotes because it could be anything)
		/// </summary>
		[Category("Data"), Description("The 'Light gray' color used on this.\n(Light gray is in quotes because it could be anything)")]
		public abstract Entry EntryLightGray {
			get;
			set;
		}
		/// <summary>
		/// The 'Dark gray' color used on this.
		/// (Dark gray is in quotes because it could be anything)
		/// </summary>
		[Category("Data"), Description("The 'Dark gray' color used on this.\n(Dark gray is in quotes because it could be anything)")]
		public abstract Entry EntryDarkGray {
			get;
			set;
		}
		/// <summary>
		/// The 'Black' color used on this.
		/// (Black is in quotes because it could be anything)
		/// </summary>
		[Category("Data"), Description("The 'Black' color used on this.\n(Black is in quotes because it could be anything)")]
		public abstract Entry EntryBlack {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the entry with the specified number.
		/// Numbers are: 
		/// <list type="table">
		/// <listheader>
		/// <term>Number</term>
		/// <description>Variable</description>
		/// </listheader>
		/// <item>
		/// <term>0</term>
		/// <description><see cref="EntryWhite"/></description>
		/// </item>
		/// <item>
		/// <term>1</term>
		/// <description><see cref="EntryLightGray"/></description>
		/// </item>
		/// <item>
		/// <term>2</term>
		/// <description><see cref="EntryDarkGray"/></description>
		/// </item>
		/// <item>
		/// <term>3</term>
		/// <description><see cref="EntryBlack"/></description>
		/// </item>
		/// </list>
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public virtual Entry this[int num] {
			get {
				switch (num) {
				case 0: return this.EntryWhite;
				case 1: return this.EntryLightGray;
				case 2: return this.EntryDarkGray;
				case 3: return this.EntryBlack;
				default: throw new ArgumentOutOfRangeException("Out of bounds; needs to be between 0 and 3 includive; got " + num);
				}
			}
			set {
				switch (num) {
				case 0: this.EntryWhite = value; return;
				case 1: this.EntryLightGray = value; return;
				case 2: this.EntryDarkGray = value; return;
				case 3: this.EntryBlack = value; return;
				default: throw new ArgumentOutOfRangeException("Out of bounds; needs to be between 0 and 3 includive; got " + num);
				}
			}
		}

		/// <summary>
		/// Gets or sets the entry for the specified color.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public virtual Entry this[GBColor color] {
			get {
				switch (color) {
				case GBColor.WHITE: return this.EntryWhite;
				case GBColor.LIGHT_GRAY: return this.EntryLightGray;
				case GBColor.DARK_GRAY: return this.EntryDarkGray;
				case GBColor.BLACK: return this.EntryBlack;
				default: throw new ArgumentOutOfRangeException("Unrecognised GBColor: " + color + " (int: " + ((int)color) + ")");
				}
			}
			set {
				switch (color) {
				case GBColor.WHITE: this.EntryWhite = value; return;
				case GBColor.LIGHT_GRAY: this.EntryLightGray = value; return;
				case GBColor.DARK_GRAY: this.EntryDarkGray = value; return;
				case GBColor.BLACK: this.EntryBlack = value; return;
				default: throw new ArgumentOutOfRangeException("Unrecognised GBColor: " + color + " (int: " + ((int)color) + ")");
				}
			}
		}
	}

	/// <summary>
	/// An individual entry in the palette.
	/// </summary>
	public interface GBPaletteEntry
	{
		/// <summary>
		/// Creates a PaletteEntry using the default value for that color.
		/// </summary>
		/// <param name="color"></param>
		protected GBPaletteEntry(GBColor color) {
			switch (color) {
			case GBColor.WHITE: this.CorespondingID = 0; break;
			case GBColor.DARK_GRAY: this.CorespondingID = 1; break;
			case GBColor.LIGHT_GRAY: this.CorespondingID = 2; break;
			case GBColor.BLACK: this.CorespondingID = 3; break;
			default: throw new ArgumentOutOfRangeException("Unrecognised GBColor: " + color + " (int: " + ((int)color) + ")");
			}
			this.CorespondingColor = color;

			this.SetToDefaultColor();
		}

		/// <summary>
		/// Creates a PaletteEntry using the default color value.
		/// </summary>
		/// <param name="id"></param>
		protected GBPaletteEntry(int id) {
			switch (id) {
			case 0: this.CorespondingColor = GBColor.WHITE; break;
			case 1: this.CorespondingColor = GBColor.DARK_GRAY; break;
			case 2: this.CorespondingColor = GBColor.LIGHT_GRAY; break;
			case 3: this.CorespondingColor = GBColor.BLACK; break;
			default: throw new ArgumentOutOfRangeException("Out of bounds; needs to be between 0 and 3 includive; got " + id);
			}
			this.CorespondingID = id;

			this.SetToDefaultColor();
		}

		/// <summary>
		/// The ID used to render this.
		/// </summary>
		[Category("Data"), Description("The ID used in rendering.  Controls position.")]
		[ReadOnly(true)]
		public readonly int CorespondingID;
		/// <summary>
		/// The GBColor that is repersented by this.
		/// </summary>
		[Category("Data"), Description("The GBColor that this repersents.")]
		[ReadOnly(true)]
		public readonly GBColor CorespondingColor;

		/// <summary>
		/// The color used to display the entry.
		/// </summary>
		[Category("Data"), Description("The color that is displayed by this.")]
		[ReadOnly(true)]
		public virtual Color DisplayColor {
			get {
				return Color;
			}
		}

		/// <summary>
		/// The color used by this entry.
		/// </summary>
		[Category("Data"), Description("The color used by this entry.")]
		public abstract Color Color {
			get;
			set;
		}

		/// <summary>
		/// Sets the color of this value to the wanted color.
		/// </summary>
		public abstract void SetToDefaultColor();

		/// <summary>
		/// Converts this to the actual color used.
		/// </summary>
		/// <param name="entry"></param>
		/// <returns></returns>
		public virtual implicit operator Color(GBPaletteEntry entry) {
			return entry.Color;
		}
	}
}
