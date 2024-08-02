using SK.NRMods.CustomMusic.Model;
using System;
using System.Collections.Generic;
using System.IO;
using MusicCategory = GodConstant.Music_State;

namespace SK.NRMods.CustomMusic.Service
{
	public class MusicLoader : IDisposable
	{
		private Playlist _playlist;

		private readonly BackgroundMusicCacheService _cacheService = new();
		private readonly MusicCategory[] _categories;

		public MusicLoader(MusicCategory[] allowedCategories)
		{
			_categories = allowedCategories;
			_cacheService.TrackCached += OnTrackCached;
			InitFolders();
		}

		public Playlist LoadPlaylist(MusicCategory category)
		{
			if (_playlist?.Category == category)
			{
				return _playlist;
			}

			_cacheService.Stop();
			var playlist = new Playlist(category);
			_playlist = playlist;
			var paths = GetPaths(category);
			var files = _cacheService.GetFilesInfo(paths);
			var toCache = new List<MusicFileInfo>();
			var ready = new List<FileMusicAsset>();
			foreach (var file in files)
			{
				if (file.IsReadyToLoad())
				{
					ready.Add(new FileMusicAsset(file));
				}
				else
				{
					toCache.Add(file);
				}
			}
			//UnityEngine.Debug.LogError(ready.Count);
			//UnityEngine.Debug.LogError(toCache.Count);
			_playlist.AddTracks(ready);
			_cacheService.StartCache(toCache);

			return playlist;
		}

		public void Dispose()
		{
			_cacheService.Stop();
		}

		private void OnTrackCached(MusicFileInfo file)
		{
			//TODO: check if file related to current playlist
			_playlist?.AddTrack(new FileMusicAsset(file));
		}

		private void InitFolders()
		{
			var basicFolder = Settings.MusicFolder;

			foreach (var category in _categories)
			{
				var path = Path.Combine(basicFolder, category.ToString());
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
		}

		private string[] GetPaths(MusicCategory category)
		{
			var path = Path.Combine(Settings.MusicFolder, category.ToString());

			if (!Directory.Exists(path))
			{
				return Array.Empty<string>();
			}
			return Directory.GetFiles(path);
		}
	}
}
