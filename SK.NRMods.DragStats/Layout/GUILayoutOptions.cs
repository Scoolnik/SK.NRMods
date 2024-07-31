using UnityEngine;

namespace SK.NRMods.DragStats.Layout
{
	internal class GUILayoutOptions
	{
		public int XOffset = 0;
		public int YOffset = 50;
		public int FontSize = 25;
		public int LinesSpace = 6;
		public int ReferenceScreenHeight = 1080;
		public int ReferenceScreenWidth = 1920;
		public Color Color = Color.white;

		public static GUILayoutOptions Center = new()
		{
			XOffset = 800
		};

		public static GUILayoutOptions Left = new()
		{
			XOffset = 0
		};

		public static GUILayoutOptions Right = new()
		{
			XOffset = 1600
		};

		public void Scale(int screenWidth, int screenHeight)
		{
			var yMultiplier = (float)screenHeight / ReferenceScreenHeight;
			var XMultiplier = (float)screenWidth / ReferenceScreenWidth;
			XOffset = (int)(XOffset * XMultiplier);
			YOffset = (int)(YOffset * yMultiplier);
			FontSize = (int)(FontSize * yMultiplier);
			LinesSpace = (int)(LinesSpace * yMultiplier);
			ReferenceScreenHeight = screenHeight;
			ReferenceScreenWidth = screenWidth; 
		}
	}
}
