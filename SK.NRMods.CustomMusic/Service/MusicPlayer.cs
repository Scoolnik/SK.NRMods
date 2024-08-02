using BepInEx.Unity.IL2CPP.Utils;
using SK.NRMods.CustomMusic.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MusicCategory = GodConstant.Music_State;

namespace SK.NRMods.CustomMusic.Service
{
	public class MusicPlayer : System.IDisposable
	{
		public string TrackName => _currentTrack?.Name;
		public bool IsPlaying => State != MusicPlayerState.Off && State != MusicPlayerState.Pause;

		public MusicPlayerState State { get; private set; }

		public MusicCategory Category;

		private static readonly GodConstant _god = GodConstant.Instance;
		private static readonly RCC_Settings _settings = RCC_Settings.Instance;
		private static readonly MusicCategory[] SupportedCategories = [
			MusicCategory.main_menu, /*MusicCategory.loading,*/ MusicCategory.garage, MusicCategory.cruise,
			MusicCategory.meetspot, MusicCategory.race, //MusicCategory.race_win, MusicCategory.race_loss, 
		];

		private Playlist _playlist = new(MusicCategory.off);

		private MusicLoader _musicLoader;
		private Coroutine _activeCoroutine;
		private MusicAsset _currentTrack;

		private HashSet<MusicAsset> _toDispose = [];
		private DateTime _lastAssetsDisposeTime = DateTime.MinValue;
		
		public MusicPlayer()
		{
			_musicLoader = new MusicLoader(SupportedCategories);
			_musicLoader.LoadPlaylist(MusicCategory.garage);
		}

		public static bool IsSupportedCategory(MusicCategory category)
		{
			return SupportedCategories.Contains(category);
		}

		public void SetMusicCategory(MusicCategory category)
		{
			Category = category;
			_god.music_State = category;

			_playlist = _musicLoader.LoadPlaylist(category);
		}

		public void Play()
		{
			RandomSong();
		}

		public void RandomSong()
		{
			PlayTrack(_playlist.RandomTrack());
		}

		public void NextSong()
		{
			PlayTrack(_playlist.NextTrack());
		}

		public void PreviousSong()
		{
			PlayTrack(_playlist.PreviousTrack());
		}

		public void Stop()
		{
			if (_activeCoroutine != null)
			{
				_god.StopCoroutine(_activeCoroutine);
			}
			_toDispose.Add(_currentTrack);
			_currentTrack = null;
			State = MusicPlayerState.Off;
			CheckAssetsDisposeRequired();
		}

		public void Dispose()
		{
			Stop();
			DisposeLoadedAssets();
			_musicLoader.Dispose();
		}

		private void PlayTrack(MusicAsset track)
		{
			Stop();
			State = MusicPlayerState.Load;

			_god.music_refreshStateSettings();

			_activeCoroutine = _god.StartCoroutine(PlayMusicAsset(track));
		}

		private IEnumerator PlayMusicAsset(MusicAsset audio)
		{
#if DEBUG
			Debug.Log($"Playing: {audio.Name}");
#endif
			_currentTrack = audio;
			yield return audio.Load();
			if (audio.IsCorrupted())
			{
				//TODO
				Debug.Log($"{audio.Name} is corrupted");
			}
			else
			{
				_god.nowPlaying = new()
				{
					song_ID = audio.ID
				};
				_god.UI_Data.start_fade_nowPlaying(false); //TODO: move out of here
				State = MusicPlayerState.Play;

				yield return PlayAudioClip(_god.musicSource, audio.AudioClip);
			}
			NextSong();
		}

		private IEnumerator PlayAudioClip(AudioSource audioSource, AudioClip audio)
		{
			audioSource.clip = audio;
			Debug.Log("before call play. duration: " + audio.length);
			audioSource.Play();
			yield return new WaitForSeconds(audio.length);
			Debug.Log($"after WaitForSeconds {audio.name}");
		}

		private void CheckAssetsDisposeRequired()
		{
			if (_toDispose.Count > 5 && (DateTime.Now - _lastAssetsDisposeTime).TotalMinutes > 1)
			{
				DisposeLoadedAssets();
			}
		}

		private void DisposeLoadedAssets()
		{
			foreach (var asset in _toDispose)
			{
				if (asset != _currentTrack)
				{
					asset.Dispose();
				}
			}
			_toDispose.Clear();
			_lastAssetsDisposeTime = DateTime.Now;
		}
	}
}
