using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SK.NRMods.PositionFix.Patches;

namespace SK.NRMods.PositionFix
{
	[BepInPlugin("SK.NRMods.PositionFix", "Objects position reset fix", "1.0.0")]
	public class Plugin : BasePlugin
	{
		public override void Load()
		{
			Harmony.CreateAndPatchAll(typeof(DrivingScenePatch));
		}
	}
}
