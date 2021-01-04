using System;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calculators
{
    /// <summary>
    /// Calculates damage equal to a percentage of the target's max health.
    /// </summary>
    public class PercentageDamageCalculator : IDamageCalculator
    {
        /// <summary>
        /// The percentage of the target's max health to deal as damage.
        /// </summary>
        private readonly int _percentage;

        /// <summary>
        /// Creates a new <see cref="PercentageDamageCalculator"/> instance.
        /// </summary>
        /// <param name="percentage">The percentage of the target's max health to deal as damage.</param>
        public PercentageDamageCalculator(int percentage)
        {
            _percentage = percentage;
        }

        /// <inheritdoc/>
        public int Calculate(Character user, DamageAction damage, Character target)
        {
            return Math.Max(1, target.MaxHealth * _percentage / 100);
        }
    }
}
