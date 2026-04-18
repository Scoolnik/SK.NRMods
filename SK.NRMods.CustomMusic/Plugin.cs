using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SK.NRMods.CustomMusic.HarmonyPatchers;

namespace SK.NRMods.CustomMusic
{
	[BepInPlugin("SK.NRMods.CustomMusic", "Custom music aka NR_LibreMusic", "2.0.0")]
	public class Plugin : BasePlugin
	{
		public override void Load()
		{
			Harmony.CreateAndPatchAll(typeof(MainPatch));
		}
	}
}
