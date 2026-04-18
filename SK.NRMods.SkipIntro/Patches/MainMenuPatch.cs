using BepInEx.Unity.IL2CPP.Utils;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using UnityEngine;

namespace SK.NRMods.SkipIntro.Patches
{
	internal class MainMenuPatch
	{
		private static bool _isLoading = true;

		[HarmonyPatch(typeof(mainMenu), "Start")]
		[HarmonyPrefix]
		public static bool BeforeStart()
		{
			var god = GodConstant.Instance;
			if (god.noPlayerData)
			{
				_isLoading = false;
			}
			return true;
		}

		[HarmonyPatch(typeof(GodConstant), "loadCurrentDrivingCar")]
		[HarmonyPrefix]
		public static bool BeforeSpawnCar()
		{
			return !_isLoading;
		}

		[HarmonyPatch(typeof(mainMenu), "Start")]
		[HarmonyPostfix]
		public static void Start(mainMenu __instance)
		{
			_isLoading = false;
			var god = GodConstant.Instance;
			if (god.noPlayerData)
			{
				return;
			}

			Time.timeScale = 100; // Increase time scale to skip useless pauses caused by "WaitForSeconds"
			god.StartCoroutine(__instance.goTo_loadingScreen().WrapToManaged());
		}

		[HarmonyPatch(typeof(GodConstant), "startLoad")]
		[HarmonyPostfix]
		public static void ResetTimeScale(mainMenu __instance)
		{
			Time.timeScale = 1;
		}
	}
}