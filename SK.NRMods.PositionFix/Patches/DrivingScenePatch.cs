using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using Il2CppSystem.Collections;
using UnityEngine;

namespace SK.NRMods.PositionFix.Patches
{
	public static class DrivingScenePatch
	{
		[HarmonyPatch(typeof(drivingScene), nameof(drivingScene.floatingPoint_correction))]
        [HarmonyPrefix]
        public static bool BeforeUpdateWorldCenter(drivingScene __instance, bool __0, ref IEnumerator __result)
        {
	        var isFirstUpdate = __0;
	        if (isFirstUpdate)
	        {
		        return true;
	        }

		    //the delay is required to prevent raceSpot.race_carsIntro_0 from being stuck, cause it waits until waitForFloatingPoint became false
	        System.Collections.IEnumerator fixAfterUpdate()
	        {
		        yield return new WaitForFixedUpdate();
		        __instance.waitForFloatingPoint = true;
	        };
	        
	        __instance.StartCoroutine(fixAfterUpdate().WrapToIl2Cpp());
	        
            static System.Collections.IEnumerator noop()
            {
	            yield break;
            }

            __result = noop().WrapToIl2Cpp();
            return false;
        }
	}
}
