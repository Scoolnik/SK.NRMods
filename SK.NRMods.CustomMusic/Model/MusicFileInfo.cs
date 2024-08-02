using SK.NRMods.CustomMusic.Utils;
using System.IO;
using UnityEngine;

namespace SK.NRMods.CustomMusic.Model
{
	internal class MusicFileInfo(string originalPath, string cachePath, string name)
	{
		public readonly string Name = name;
		public readonly string Path = MusicUtils.RequiresCache(originalPath) ? cachePath : originalPath;

		public readonly string OriginalPath = originalPath;
		public readonly string CachePath = cachePath;

		public bool IsReadyToLoad()
		{
			return File.Exists(Path);
		}
	}
}
