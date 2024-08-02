using System.IO;
using UnityEngine;

namespace SK.NRMods.CustomMusic.Service
{
	internal class Settings
	{
		public static string MusicFolder { get; set; } = Path.Combine(Application.dataPath, "..", "Music");
		public static string CachedMusicFileFormat { get; set; } = ".ogg";
	}
}
