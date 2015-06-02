using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace GB.Shared
{
	public static class RecentFileUtils
	{
		/// <summary>
		/// Adds the given file to the "Recently Opened Programs" list.
		/// It will only appear if GBTD is set as the registered program for the '.gbr' extension.
		/// 
		/// http://pinvoke.net/default.aspx/shell32/SHAddToRecentDocs.html
		/// </summary>
		/// <param name="type">The type.  0x0002 is the only useful value for us.  (SHARD_PATH)</param>
		/// <param name="path">The full path of the file name.</param>
		[DllImport("shell32.dll", CharSet = CharSet.Ansi)]
		private static extern void SHAddToRecentDocs(UInt16 type, string path);

		/// <summary>
		/// Adds the given file to the recently opened files list.
		/// </summary>
		/// <param name="fileName"></param>
		public static void AddToRecentlyUsedFilesList(String fileName, ApplicationSettingsBase settings) {
			if (fileName == null) { throw new ArgumentNullException("fileName"); }
			if (settings == null) { throw new ArgumentNullException("settings"); }

			try {
				String fullPath = Path.GetFullPath(fileName).ToLowerInvariant();

				StringCollection files = settings["RecentlyUsedFiles"] as StringCollection;

				if (files == null) {
					files = new StringCollection();
				}

				files.Remove(fullPath);
				files.Insert(0, fullPath);

				settings["RecentlyUsedFiles"] = files;
				settings.Save();

				SHAddToRecentDocs(0x0002, fullPath);
			} catch (Exception e) {
				Console.Error.WriteLine("Failed to add " + fileName + " to recent files list: " + e.ToString() + "!");
				Console.Error.WriteLine(e.StackTrace);
			}
		}

		/// <summary>
		/// Gets the recently used files list.
		/// </summary>
		/// <param name="settings">Application settings to load the list from.</param>
		public static StringCollection GetRecentlyUsedFilesList(ApplicationSettingsBase settings) {
			if (settings == null) { throw new ArgumentNullException("settings"); }

			StringCollection files = settings["RecentlyUsedFiles"] as StringCollection;

			if (files == null) {
				files = new StringCollection();
			}

			return files;
		}

		/// <summary>
		/// Checks if the recently used files list is empty..
		/// </summary>
		/// <param name="settings">Application settings to load the list from.</param>
		public static bool IsRecentlyUsedFilesListEmpty(ApplicationSettingsBase settings) {
			if (settings == null) { throw new ArgumentNullException("settings"); }

			StringCollection files = settings["RecentlyUsedFiles"] as StringCollection;

			if (files == null) {
				return true;
			}
			if (files.Count == 0) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// Sets the children of 'baseItem' to be for the RecentlyUsedFilesList. 
		/// </summary>
		/// <param name="settings">Application settings to load the list from.</param>
		/// <param name="baseItem">The menu item to put the list as children of.  (Any existing children will be cleared)</param>
		/// <param name="onClick">Eventhandler for when any of the child itmes is clicked.  (May be null)</param>
		public static void AddRecentlyUsedFilesListItems(ApplicationSettingsBase settings, MenuItem baseItem, EventHandler onClick = null) {
			if (settings == null) { throw new ArgumentNullException("settings"); }
			if (baseItem == null) { throw new ArgumentNullException("baseItem"); }

			StringCollection recentlyUsedFiles = GetRecentlyUsedFilesList(settings);

			baseItem.MenuItems.Clear();
			foreach (String file in recentlyUsedFiles) {
				baseItem.MenuItems.Add(file, onClick);
			}
		}
	}
}
