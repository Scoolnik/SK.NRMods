using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SK.NRMods.DragStats.HarmonyParchers;

namespace SK.NRMods.DragStats
{
	[BepInPlugin("SK.NRMods.DragStats", "Drag stats (like 0-100 time)", "1.0.1")]
	public class Plugin : BasePlugin
	{
		public override void Load()
		{
			Harmony.CreateAndPatchAll(typeof(DragStatsPatch));
		}
	}
}
