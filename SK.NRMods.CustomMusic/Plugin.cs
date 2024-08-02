using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SK.NRMods.CustomMusic.HarmonyParchers;

namespace SK.NRMods.CustomMusic
{
	[BepInPlugin("SK.NRMods.CustomMusic", "Custom music", "1.0.1")]
	public class Plugin : BasePlugin
	{
		public override void Load()
		{
			Harmony.CreateAndPatchAll(typeof(MainPatch));
		}
	}
}
