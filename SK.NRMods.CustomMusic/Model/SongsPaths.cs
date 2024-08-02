using System.Collections.Generic;
using MusicArea = GodConstant.Music_State;

namespace SK.NRMods.CustomMusic.Model
{
	internal class SongsPaths
	{
		private Dictionary<MusicArea, string[]> _paths = [];

		public string[] Get(MusicArea area)
		{
			if (_paths.TryGetValue(area, out var result))
			{
				return result;
			}
			return [];
		}

		public void Set(MusicArea musicArea, string[] paths)
		{
			if (_paths.ContainsKey(musicArea))
			{
				_paths[musicArea] = paths;
			}
			else
			{
				_paths.Add(musicArea, paths);
			}
		}

		public void Clear()
		{
			_paths.Clear();
		}
	}
}
