using System;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calculators
{
    /// <summary>
    /// Calculates damage equal to a percentage of the amount of the last damage
    /// action from a move that affected the user.
    /// </summary>
    public class PercentageOfLastReceivedMoveDamageCalculator : IDamageCalculator
    {
        /// <summary>
        /// The default amount of damage.
        /// </summary>
        private readonly int _defaultAmount;

        /// <summary>
        /// Creates a new <see cref="PercentageOfLastReceivedMoveDamageCalculator"/> instance.
        /// </summary>
        /// <param name="defaultAmount">The default amount of damage to return.</param>
        public PercentageOfLastReceivedMoveDamageCalculator(int defaultAmount)
        {
            _defaultAmount = defaultAmount;
        }

        /// <inheritdoc/>
        public int Calculate(Character user, Damage damage, Character target)
        {
            var result = user.ActionHistory.LastMoveDamageResult;
            if (result is null)
            {
                return _defaultAmount;
            }

            return Math.Max(1, result.Amount * damage.Power / 100);
        }
    }
}
