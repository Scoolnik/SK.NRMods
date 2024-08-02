using SK.NRMods.CustomMusic.Model;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MusicCategory = GodConstant.Music_State;

namespace SK.NRMods.CustomMusic.Utils
{
	public static class MusicUtils
	{
		public static IEnumerable<AddressablesMusicAsset> GetDefaultTracksAssets(MusicCategory category)
		{
			var settings = RCC_Settings.Instance;
			var tracks = GetDefaultTracks(category, settings);

			foreach (var track in tracks)
			{
				yield return new AddressablesMusicAsset(RCC_Settings.getSongName(track.song_ID), track);
			}
		}

		public static Il2CppSystem.Collections.Generic.List<RCC_Settings.SoundtrackSong> GetDefaultTracks(MusicCategory category, RCC_Settings settings)
		{
			switch (category)
			{
				case MusicCategory.loading: 
					return settings.loadingMusic;
				case MusicCategory.garage: 
					return settings.walkMusic;
				case MusicCategory.meetspot: 
					return settings.meetspotMusic;
				case MusicCategory.cruise: 
					return settings.cruiseMusic;
				case MusicCategory.race: 
					return settings.raceMusic;
				case MusicCategory.race_win:
				case MusicCategory.race_loss:
					{
						var list = new Il2CppSystem.Collections.Generic.List<RCC_Settings.SoundtrackSong>();
						list.Add(settings.raceWinMusic);
						return list;
					}
				case MusicCategory.main_menu: 
					return settings.menuMusic;
				default:
					return new Il2CppSystem.Collections.Generic.List<RCC_Settings.SoundtrackSong>();
			};
		}

		public static bool RequiresCache(string filePath)
		{
			var type = GetAudioType(filePath);
			return type == AudioType.MPEG || type == AudioType.UNKNOWN;
		}

		public static AudioType GetAudioType(string path)
		{
			var extension = Path.GetExtension(path)?.ToLowerInvariant().Substring(1);
			AudioType type = extension switch
			{
				"aif" => AudioType.AIFF,
				"aiff" => AudioType.AIFF,
				"it" => AudioType.IT,
				"mod" => AudioType.MOD,
				"mp2" => AudioType.MPEG,
				"mp3" => AudioType.MPEG,
				"ogg" => AudioType.OGGVORBIS,
				"s3m" => AudioType.S3M,
				"wav" => AudioType.WAV,
				"xm" => AudioType.XM,
				"flac" => AudioType.UNKNOWN,
				_ => AudioType.UNKNOWN
			};
			return type;
		}
	}
}
