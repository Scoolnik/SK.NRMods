using UnityEngine;

namespace SK.NRMods.DragStats.Layout
{
	internal abstract class SimpleGUILayoutBase
	{
		protected string[] _text = [];
		protected GUIStyle _style;
		protected GUILayoutOptions _options;

		public SimpleGUILayoutBase(GUILayoutOptions options)
		{
			_options = options;
			_style = new()
			{
				fontSize = _options.FontSize,
			};
			_style.normal.textColor = _options.Color;
			Scale(Screen.width, Screen.height);
			Initialize();
		}

		public void Scale(int screenWidth, int screenHeight)
		{
			_options.Scale(screenWidth, screenHeight);
			_style.fontSize = _options.FontSize;
		}

		public abstract void UpdateValues();

		public void Draw()
		{
			var x = _options.XOffset;
			var y = _options.YOffset;
			var lineHeight = _options.LinesSpace + _options.FontSize;
			for (var i = 0; i < _text.Length; i++)
			{
				GUI.Label(new Rect(x, y, 100f, lineHeight), _text[i], _style);
				y += lineHeight;
			}
		}

		protected abstract void Initialize();
	}
}
