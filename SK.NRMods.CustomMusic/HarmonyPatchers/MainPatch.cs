using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using SK.NRMods.CustomMusic.Service;
using System.Linq;
using UnityEngine;

namespace SK.NRMods.CustomMusic.HarmonyPatchers
{
	internal static class MainPatch
	{
		private static MusicPlayer _player = new();

		[HarmonyPatch(typeof(GodConstant), nameof(GodConstant.startLoad))]
		[HarmonyPrefix()]
		private static void LoadScene(GodConstant __instance)
		{
			_player.Stop();
		}

		[HarmonyPatch(typeof(GodConstant), nameof(GodConstant.MobilePhoneInputs))]
		[HarmonyPrefix()]
		private static bool MobilePhoneInputs()
		{
			var god = GodConstant.Instance;
			if (god.loading_StayInLoadingScreen || god.phoneCall_currentActive)
			{
				return true;
			}
			var inputs = god.player;
			if (inputs.GetButtonDoublePressUp("menu_left"))
			{
				_player.PreviousSong();
				god.Play_UISound_(god.phoneMenu_input, 1f, 1f);
			}
			else if (inputs.GetButtonDoublePressUp("menu_right"))
			{
				_player.NextSong();
				god.Play_UISound_(god.phoneMenu_input, 1f, 1f);
			}
			return false;
		}

		[HarmonyPatch(typeof(GodConstant), nameof(GodConstant.music_findNextSong))]
		[HarmonyPrefix()]
		private static bool music_findNextSong(GodConstant.Music_State __0, bool __1, ref Il2CppSystem.Collections.IEnumerator __result)
		{
#if DEBUG
			Debug.Log(__0.ToString());
#endif
			var god = GodConstant.Instance;

			if (!MusicPlayer.IsSupportedCategory(__0))
			{
				_player.Stop();
			}
			else if (_player.Category != __0 || !_player.IsPlaying || god.music_State == GodConstant.Music_State.off)
			{
				_player.SetMusicCategory(__0);
				_player.RandomSong();
			}

			var useDefault = !_player.IsPlaying;
			if (!useDefault)
			{
				System.Collections.IEnumerator noop()
				{
					yield break;
				}
				__result = noop().WrapToIl2Cpp();
			}
			return useDefault;
		}

		[HarmonyPatch(typeof(RCC_DashboardInputs), nameof(RCC_DashboardInputs.MobilePhone))]
		[HarmonyPrefix()]
		private static void MobilePhone()
		{
			var god = GodConstant.Instance;
			god.nowPlaying ??= new RCC_Settings.SoundtrackSong()
			{
				song_ID = god.lastLoadedSong,
			};
		}

		[HarmonyPatch(typeof(RCC_Settings), nameof(RCC_Settings.getSongName))]
		[HarmonyPrefix()]
		private static bool BeforeGetSongName(RCC_Settings.SongTrack_ID __0, ref string __result)
		{
			if (__0 == RCC_Settings.SongTrack_ID.null_song)
			{
				__result = GetCurrentSongName();
				return false;
			}
			return true;
		}

		private static string GetCurrentSongName()
		{
			return _player?.TrackName ?? string.Empty;
		}
	}
}
