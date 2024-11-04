using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.NRMods.CustomMusic.Utils
{
	public static class ArrayUtils
	{
		public static void Shuffle<T>(ref List<T> values)
		{
			var random = new Random();
			int n = values.Count;

			for (int i = 0; i < n - 1; i++)
			{
				int j = random.Next(i, n);

				if (j != i)
				{
					T temp = values[i];
					values[i] = values[j];
					values[j] = temp;
				}
			}
		}
	}
}
