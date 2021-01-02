using System;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calulators
{
    /// <summary>
    /// Calculates damage equal to a percentage of the target's max health.
    /// </summary>
    public class PercentageDamageCalculator : IDamageCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, Damage damage, Character target)
        {
            return Math.Max(1, target.MaxHealth * damage.Power / 100);
        }
    }
}
