﻿using SK.NRMods.CustomMusic.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace SK.NRMods.CustomMusic.Model
{
	internal class FileMusicAsset(MusicFileInfo file) : MusicAsset(file.Name)
	{
		public override AudioClip AudioClip { get; protected set; }
		public override RCC_Settings.SongTrack_ID ID { get; protected set; } = RCC_Settings.SongTrack_ID.null_song;

		public override bool IsCorrupted()
		{
			return AudioClip == null;
		}

		public override IEnumerator Load()
		{
			if (!IsCorrupted())
			{
				yield break;
			}

			var www = UnityWebRequestMultimedia.GetAudioClip("file:///" + file.Path, MusicUtils.GetAudioType(file.Path));
			yield return www.SendWebRequest();
			if (www.isNetworkError || www.isHttpError)
			{
				Debug.LogError(www.error + "\n" + file.Path);
			}
			else
			{
				var downloadHandler = www.downloadHandler.Cast<DownloadHandlerAudioClip>();
				downloadHandler.streamAudio = true;

				AudioClip = downloadHandler.audioClip;
#if DEBUG
				Debug.Log($"Loaded {Name} from {file.Path}");
#endif
			}
			www.Dispose();
		}

		protected override void Unload()
		{
			if (AudioClip != null)
			{
				AudioClip.Destroy(AudioClip);
				AudioClip = null;
			}
		}
	}
}