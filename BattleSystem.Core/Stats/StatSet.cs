using System.Collections.Generic;
using BattleSystem.Core.Items;

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
        /// Receives the relevant transforms from the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void ReceiveTransforms(Item item)
        {
            Attack.ReceiveTransforms(item, StatCategory.Attack);
            Defence.ReceiveTransforms(item, StatCategory.Defence);
            Speed.ReceiveTransforms(item, StatCategory.Speed);
        }

        /// <summary>
        /// Clears any transforms this stat set may have received.
        /// </summary>
        public void ClearTransforms()
        {
            Attack.BaseValueTransforms.Clear();
            Defence.BaseValueTransforms.Clear();
            Speed.BaseValueTransforms.Clear();
        }

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
