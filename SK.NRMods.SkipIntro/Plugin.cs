using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SK.NRMods.SkipIntro.Patches;

namespace SK.NRMods.SkipIntro
{
	[BepInPlugin("SK.NRMods.SkipIntro", "Skip that loooooong hello screen", "2.0.1")]
	public class Plugin : BasePlugin
	{
		public override void Load()
		{
			Harmony.CreateAndPatchAll(typeof(MainMenuPatch));
		}
	}
}
