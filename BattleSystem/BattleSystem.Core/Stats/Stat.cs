using System.Collections.Generic;
using BattleSystem.Core.Items;
using static BattleSystem.Core.Items.Item;

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
        public int CurrentValue => GetCurrentValue();

        /// <summary>
        /// Gets or sets the list of base value transforms.
        /// </summary>
        public List<StatBaseValueTransform> BaseValueTransforms { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Stat"/> instance.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <param name="multiplier">The multiplier.</param>
        public Stat(int baseValue, double multiplier = 1)
        {
            BaseValue = baseValue;
            Multiplier = multiplier;

            BaseValueTransforms = new List<StatBaseValueTransform>();
        }

        /// <summary>
        /// Receives the relevant transforms from the given item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="statCategory">The stat category.</param>
        public void ReceiveTransforms(Item item, StatCategory statCategory)
        {
            var transforms = item.StatBaseValueTransforms[statCategory];
            BaseValueTransforms.AddRange(transforms);
        }

        /// <summary>
        /// Clears any transforms this stat may have received.
        /// </summary>
        public void ClearTransforms()
        {
            BaseValueTransforms.Clear();
        }

        /// <summary>
        /// Returns the current value of the stat.
        /// </summary>
        private int GetCurrentValue()
        {
            var transformedBaseValue = BaseValue;

            foreach (var t in BaseValueTransforms)
            {
                transformedBaseValue = t(transformedBaseValue);
            }

            return (int) (transformedBaseValue * Multiplier);
        }
    }
}
