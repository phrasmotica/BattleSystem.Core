using System.Collections.Generic;

namespace BattleSystem.Core.Stats
{
    /// <summary>
    /// Represents a set of stats.
    /// </summary>
    public class StatSet
    {
        /// <summary>
        /// Gets or sets the attack stat.
        /// </summary>
        public Stat Attack { get; set; }

        /// <summary>
        /// Gets or sets the defence stat.
        /// </summary>
        public Stat Defence { get; set; }

        /// <summary>
        /// Gets or sets the speed stat.
        /// </summary>
        public Stat Speed { get; set; }

        /// <summary>
        /// Returns a dictionary of the stat multipliers of this stat set.
        /// </summary>
        public IDictionary<StatCategory, double> MultipliersAsDictionary()
        {
            return new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = Attack.Multiplier,
                [StatCategory.Defence] = Defence.Multiplier,
                [StatCategory.Speed] = Speed.Multiplier,
            };
        }
    }
}
