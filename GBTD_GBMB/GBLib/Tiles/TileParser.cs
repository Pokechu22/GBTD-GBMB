using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace GB.Shared.Tiles
{
	/// <summary>
	/// Reads out tiles from a stream or array of bytes.
	/// </summary>
	public abstract class TileParser : IList<Tile>
	{
		protected TileParser() {
			tiles = new List<Tile>();
		}

		public abstract void WriteToStream(Stream stream);
		public Byte[] WriteToBytes() {
			using (MemoryStream stream = new MemoryStream()) {
				WriteToStream(stream);
				return stream.ToArray();
			}
		}

		public abstract void ReadFromStream(Stream stream);
		public void ReadFromBytes(Byte[] data) {
			using (MemoryStream stream = new MemoryStream(data)) {
				ReadFromStream(stream);
			}
		}

		protected List<Tile> tiles;

		public List<Tile> Tiles {
			get {
				return tiles;
			}
			set {
				tiles = value;
			}
		}

		#region IList<Tiles> stuff
		public int IndexOf(Tile item) {
			return tiles.IndexOf(item);
		}

		public void Insert(int index, Tile item) {
			tiles.Insert(index, item);
		}

		public void RemoveAt(int index) {
			throw new NotImplementedException();
		}

		public Tile this[int index] {
			get {
				return tiles[index];
			}
			set {
				tiles[index] = value;
			}
		}

		public void Add(Tile item) {
			tiles.Add(item);
		}

		public void Clear() {
			tiles.Clear();
		}

		public bool Contains(Tile item) {
			return tiles.Contains(item);
		}

		public void CopyTo(Tile[] array, int arrayIndex) {
			tiles.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return tiles.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(Tile item) {
			return tiles.Remove(item);
		}

		public IEnumerator<Tile> GetEnumerator() {
			return tiles.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return tiles.GetEnumerator();
		}
		#endregion
	}
}
