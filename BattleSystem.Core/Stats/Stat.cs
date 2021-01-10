namespace BattleSystem.Core.Stats
{
    /// <summary>
    /// Represents a stat.
    /// </summary>
    public class Stat
    {
        /// <summary>
        /// Gets or sets the base value of the stat.
        /// </summary>
        public int BaseValue { get; private set; }

        /// <summary>
        /// Gets or sets the multiplier for the stat.
        /// </summary>
        public double Multiplier { get; set; }

        /// <summary>
        /// Gets the current value of the stat.
        /// </summary>
        public int CurrentValue => (int) (BaseValue * Multiplier);

        /// <summary>
        /// Creates a new <see cref="Stat"/> instance.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="multiplier">The multiplier.</param>
        public Stat(int baseValue, double multiplier = 1)
        {
            BaseValue = baseValue;
            Multiplier = multiplier;
        }
    }
}
