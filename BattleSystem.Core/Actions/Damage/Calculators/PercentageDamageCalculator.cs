using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Damage.Calculators
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
        public IEnumerable<DamageCalculation> Calculate(Character user, DamageAction damage, IEnumerable<Character> targets)
        {
            return targets.Select(target => new DamageCalculation
            {
                Target = target,
                Success = true,
                Amount = Math.Max(1, target.MaxHealth * _percentage / 100),
            });
        }
    }
}
