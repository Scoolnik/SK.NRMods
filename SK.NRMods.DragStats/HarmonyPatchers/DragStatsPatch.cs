using HarmonyLib;
using SK.NRMods.DragStats.Layout;
using SK.NRMods.DragStats.Model;
using System.Collections;
using UnityEngine;

namespace SK.NRMods.DragStats.HarmonyParchers
{
	internal class DragStatsPatch
	{
		private static DragStatsModel _model = new();
		private static SimpleGUILayout _view;
		private static bool _display = false;

		static DragStatsPatch()
		{
			var guiOptions = GUILayoutOptions.Center;
			var displayOptoins = new SimpleGUILayout.GUILayoutDisplayOptions()
			{
				DisplayedOptions =
				[
					new SimpleGUILayout.CarNameOption(),
					new SimpleGUILayout.CarSpeedOption(),
					new SimpleGUILayout.MaxCarSpeedOption(),
					new SimpleGUILayout.SpeedTimeViewOption(0, 60),
					new SimpleGUILayout.SpeedTimeViewOption(0, 100),
					new SimpleGUILayout.SpeedTimeViewOption(100, 200),
					new SimpleGUILayout.SpeedTimeViewOption(200, 300),
				]
			};
			_view = new(guiOptions, _model, displayOptoins);
		}

		//UI_carGauges - think
		[HarmonyPatch(typeof(RCC_CarControllerV3), "Start")]
		[HarmonyPrefix]
		public static void Start(RCC_CarControllerV3 __instance)
		{
			if (!IsDriveScene() || !IsControllableCar(__instance))
			{
				return;
			}
			_display = false;
		}

		[HarmonyPatch(typeof(RCC_CarControllerV3), "Update")]
		[HarmonyPrefix]
		public static void Update(RCC_CarControllerV3 __instance)
		{
			if (!IsControllableCar(__instance))
			{
				return;
			}
			if (!IsDriveScene())
			{
				_display = false;
				return;
			}
			//GodConstant.game_currentMode //check speedometer/gauges display
			if (Input.GetKeyDown(KeyCode.F1))
			{
				_display = !_display;
				_model.SetRccController(__instance);
			}
			_model.Update(Time.time, __instance.speed);
		}

		[HarmonyPatch(typeof(CarLocalCustom), "OnGUI")]
		[HarmonyPrefix]
		public static void OnGUI(CarLocalCustom __instance)
		{
			if (!IsDriveScene() || !IsControllableCar(__instance.rcc) || !_display)
			{
				return;
			}
			_view.UpdateValues();
			_view.Draw();
		}

		private static bool IsControllableCar(RCC_CarControllerV3 car)
		{
			return car.canControl && !car.externalController && !car.isShopCar;
		}

		private static bool IsDriveScene()
		{
			var scene = GodConstant.Instance.scene_currentType;
			var gameMode = GodConstant.Instance.game_currentMode;
			return scene == GodConstant.Scene_currentType.FREEROAM && gameMode != GodConstant.Game_currentMode.main_menu;
		}
	}
}
