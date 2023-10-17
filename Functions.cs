namespace Monsters
{
	/// <summary>
	/// Contains usefull functions.
	/// </summary>
	public static class Functions
	{
		/// <summary>
		/// Limits the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="value">Value.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		public static int LimitValue (int value, int min = 1, int max = 255)
		{
			if (value > max) {

				return max;
			}
			if (value < min) {
				return min;
			}

			return value;
		}
	}
}

