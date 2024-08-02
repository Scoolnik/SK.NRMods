using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SK.NRMods.CustomMusic.HarmonyParchers;

namespace SK.NRMods.CustomMusic
{
	[BepInPlugin("SK.NRMods.CustomMusic", "Custom music aka NR_LibreMusic", "1.0.2")]
	public class Plugin : BasePlugin
	{
		public override void Load()
		{
			Harmony.CreateAndPatchAll(typeof(MainPatch));
		}
	}
}
