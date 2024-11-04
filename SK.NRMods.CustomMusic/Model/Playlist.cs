using SK.NRMods.CustomMusic.Utils;
using System.Collections.Generic;
using UnityEngine;
using MusicCategory = GodConstant.Music_State;
using Random = UnityEngine.Random;

namespace SK.NRMods.CustomMusic.Model
{
	public class Playlist
	{
		public readonly MusicCategory Category;

		private int _trackIndex = 0;
		private List<MusicAsset> _tracks = [];
		private readonly int _defaultTracksCount;
		private readonly object _sync = new();

		public Playlist(MusicCategory category)
		{
			Category = category;
			_tracks.AddRange(MusicUtils.GetDefaultTracksAssets(Category));
			_defaultTracksCount = _tracks.Count;

#if DEBUG
			Debug.Log($"Playlist {category}. Default tracks: {_defaultTracksCount}");
#endif
		}

		public MusicAsset RandomTrack(bool preferCustomTracks = true)
		{
#if DEBUG
			Debug.Log("RandomSong");
#endif
			lock (_sync)
			{
				var tracksCount = _tracks.Count;
				var minIndex = preferCustomTracks && _defaultTracksCount < tracksCount ? _defaultTracksCount : 0;
				_trackIndex = Random.Range(minIndex, tracksCount);
				Debug.Log($"{_trackIndex}/{_tracks.Count}");
				return _tracks[_trackIndex];
			}
		}

		public MusicAsset NextTrack()
		{
#if DEBUG
			Debug.Log("NextSong");
#endif
			lock (_sync)
			{
				_trackIndex = (_trackIndex + 1) % _tracks.Count;
				Debug.Log($"{_trackIndex}/{_tracks.Count}");
				return _tracks[_trackIndex];
			}
		}

		public MusicAsset PreviousTrack()
		{
#if DEBUG
			Debug.Log("PreviousSong");
#endif
			lock (_sync)
			{
				_trackIndex = (_trackIndex - 1 + _tracks.Count) % _tracks.Count;
				Debug.Log($"{_trackIndex}/{_tracks.Count}");
				return _tracks[_trackIndex];
			}
		}

		public void AddTrack(MusicAsset track)
		{
			lock (_sync)
			{
				_tracks.Add(track);
			}
		}

		public void AddTracks(IEnumerable<MusicAsset> tracks)
		{
			lock (_sync)
			{
				_tracks.AddRange(tracks);
#if DEBUG
				try
				{

					Debug.Log($"Playlist {Category}. Added custom tracks. Total tracks: {_tracks.Count}");
				}
				catch { }
#endif
			}
		}

		public bool Remove(MusicAsset track)
		{
#if DEBUG
			Debug.Log("Remove track: " + track.Name);
#endif
			lock (_sync)
			{
				return _tracks.Remove(track);
			}
		}

		public void Shuffle()
		{
			Utils.ArrayUtils.Shuffle(ref _tracks);
		}
	}
}
