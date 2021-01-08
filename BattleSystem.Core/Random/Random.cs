namespace BattleSystem.Core.Random
{
    public class Random : IRandom
    {
        /// <summary>
        /// The underlying random number generator.
        /// </summary>
        private readonly System.Random _random;

        /// <summary>
        /// Creates a new <see cref="Random"/> instance.
        /// </summary>
        public Random()
        {
            _random = new System.Random();
        }

        /// <inheritdoc />
        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        /// <inheritdoc />
        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
