using FFMpegCore;
using FFMpegCore.Enums;
using SK.NRMods.CustomMusic.Model;
using SK.NRMods.CustomMusic.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SK.NRMods.CustomMusic.Service
{
	internal class BackgroundMusicCacheService
	{
		private static readonly string _cacheFolder = Path.Combine(Settings.MusicFolder, ".cache");
		private static readonly string _libsPath;
		private CancellationTokenSource _cancellationTokenSource;

		public Action<MusicFileInfo> TrackCached;

		static BackgroundMusicCacheService()
		{
			_libsPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(BackgroundMusicCacheService)).Location);
			GlobalFFOptions.Configure(new FFOptions { BinaryFolder = _libsPath });

			if (!Directory.Exists(_cacheFolder))
			{
				Directory.CreateDirectory(_cacheFolder);
			}
		}

		public IEnumerable<MusicFileInfo> GetFilesInfo(IEnumerable<string> paths)
		{
			foreach (var path in paths)
			{
				var cachePath = GetCachedPath(path);
				yield return new MusicFileInfo(path, cachePath, Path.GetFileName(path));
			}
		}

		public void Stop()
		{
			if (_cancellationTokenSource != null)
			{
				_cancellationTokenSource.Cancel();
				_cancellationTokenSource.Dispose();
			}
		}

		public void StartCache(IEnumerable<MusicFileInfo> files)
		{
			_cancellationTokenSource = new();
			var cancelToken = _cancellationTokenSource.Token;
			Parallel.ForEachAsync(files, cancelToken, CacheTrackAsync);
		}

		private ValueTask CacheTrackAsync(MusicFileInfo file, CancellationToken cancel)
		{
			if (!file.IsReadyToLoad())
			{
				FFMpegArguments
					.FromFileInput(file.OriginalPath, false)
					.OutputToFile(file.CachePath, true, options => options
						.WithCustomArgument("-vn")
						.WithAudioCodec(AudioCodec.LibVorbis)
						.WithFastStart())
					.CancellableThrough(cancel)
					.ProcessSynchronously();

				cancel.Register(() =>
				{
					File.Delete(file.CachePath);
				});
			}
			TrackCached?.Invoke(file);
			return new();
		}

		private string GetCachedPath(string originalPath)
		{
			if (!MusicUtils.RequiresCache(originalPath))
			{
				return originalPath;
			}
			return Path.Combine(_cacheFolder, Path.GetFileNameWithoutExtension(originalPath) + Settings.CachedMusicFileFormat);
		}
	}
}
