using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SK.NRMods.CustomMusic.Model
{
	public class AddressablesMusicAsset(string name, RCC_Settings.SoundtrackSong asset) : MusicAsset(name)
	{
		public override AudioClip AudioClip { get; protected set; }
		public override RCC_Settings.SongTrack_ID ID { get; protected set; } = asset.song_ID;

		private AsyncOperationHandle<AudioClip> _loadHandle;
		private readonly RCC_Settings.SoundtrackSong _asset = asset;

		public override IEnumerator Load()
		{
			if (!IsCorrupted()) 
			{
				yield break;
			}
			_loadHandle = _asset.songClip.LoadAssetAsync();
			yield return _loadHandle;
			AudioClip = _loadHandle.Result;
		}

		public override bool IsCorrupted()
		{
			return _loadHandle == null || !_loadHandle.IsValid();
		}

		protected override void Unload()
		{
			if (!IsCorrupted())
			{
				_loadHandle.Release();
				AudioClip = null;
			}
		}
	}
}
