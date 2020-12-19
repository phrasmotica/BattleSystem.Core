using System;
using BattleSystem.Characters;
using BattleSystem.Moves;

namespace BattleSystem.Damage
{
    /// <summary>
    /// Calculates damage equal to a percentage of the target's max health.
    /// </summary>
    public class PercentageDamageCalculator : IDamageCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, Attack attack, Character target)
        {
            return Math.Max(1, target.MaxHealth * attack.Power / 100);
        }
    }
}
