using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GB.Shared.Tile
{
	/// <summary>
	/// Creates a tile from a set of bytes.
	/// </summary>
	public interface TileProvider
	{
		List<Tile> getTiles();
		int getNumberOfTiles();
	}
}
