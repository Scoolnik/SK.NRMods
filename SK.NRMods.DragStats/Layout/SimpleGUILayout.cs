using SK.NRMods.DragStats.Model;
using System;
using static SK.NRMods.DragStats.Layout.SimpleGUILayout;

namespace SK.NRMods.DragStats.Layout
{
	internal class SimpleGUILayout(GUILayoutOptions options, DragStatsModel model, GUILayoutDisplayOptions displayOptions) : SimpleGUILayoutBase(options)
	{
		private GUILayoutDisplayOptions _displayOptions = displayOptions;
		private DragStatsModel _model = model;

		public override void UpdateValues()
		{
			var lineIndex = 0;
			foreach (var item in _displayOptions.DisplayedOptions)
			{
				_text[lineIndex++] = GetOptionText(item);
			}
		}

		protected override void Initialize()
		{
			var linesCount = _displayOptions.DisplayedOptions.Length;
			_text = new string[linesCount];
		}

		private string GetOptionText(ILayoutOption option)
		{
			return option.GetValue(_model);
		}

		public struct GUILayoutDisplayOptions(ILayoutOption[] displayedOptions)
		{
			public ILayoutOption[] DisplayedOptions = displayedOptions;
		}

		public struct SpeedTimeViewOption(int from, int to) : ILayoutOption
		{
			public int From = from;
			public int To = to;

			public string GetValue(DragStatsModel model)
			{
				return GetSpeedTimeText(model);
			}

			private string GetSpeedTimeText(DragStatsModel model)
			{
				var time = model.GetTimeBetweenSpeed(From, To);
				return $"{From} - {To} km/h: {Math.Round(time, 2)} s";
			}
		}

		public struct CarSpeedOption() : ILayoutOption
		{
			public string GetValue(DragStatsModel model)
			{
				return $"Speed: {Math.Round(model.Speed, 2)} km/h";
			}
		}

		public struct MaxCarSpeedOption() : ILayoutOption
		{
			public string GetValue(DragStatsModel model)
			{
				return $"Top Speed: {Math.Round(model.MaxSpeed, 2)} km/h";
			}
		}

		public struct CarNameOption() : ILayoutOption
		{
			public string GetValue(DragStatsModel model)
			{
				return GodConstant.Instance.carModelName(model.RCC.carLocal.carOrigin.modelType, true);
			}
		}

		public interface ILayoutOption
		{
			public string GetValue(DragStatsModel model);
		}
	}
}
