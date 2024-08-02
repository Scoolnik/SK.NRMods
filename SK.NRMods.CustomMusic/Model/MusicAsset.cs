using System;
using System.Collections;
using UnityEngine;

namespace SK.NRMods.CustomMusic.Model
{
	public abstract class MusicAsset : IDisposable
	{
		public readonly string Name;
		public abstract AudioClip AudioClip { get; protected set; }
		public abstract RCC_Settings.SongTrack_ID ID { get; protected set; }

		protected MusicAsset(string name)
		{
			Name = name;
		}

		public abstract IEnumerator Load();
		public abstract bool IsCorrupted();

		public void Dispose()
		{
			Unload();
		}

		protected abstract void Unload();
	}
}
