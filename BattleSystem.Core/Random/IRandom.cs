namespace BattleSystem.Core.Random
{
    /// <summary>
    /// Interface for generating random numbers.
    /// </summary>
    public interface IRandom
    {
        /// <summary>
        /// Returns a non-negative integer less than the given max value.
        /// </summary>
        /// <param name="maxValue">The exclusive max value.</param>
        int Next(int maxValue);

        /// <summary>
        /// Returns a non-negative integer greater than or equal to the given
        /// min value and less than the given max value.
        /// </summary>
        /// <param name="minValue">The inclusive min value.</param>
        /// <param name="maxValue">The exclusive max value.</param>
        int Next(int minValue, int maxValue);
    }
}
