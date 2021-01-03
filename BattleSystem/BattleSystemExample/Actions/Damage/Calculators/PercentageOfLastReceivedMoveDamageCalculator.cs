using System;
using BattleSystem.Actions.Damage.Calculators;
using BattleSystem.Characters;

namespace BattleSystemExample.Actions.Damage.Calculators
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
        /// The action history.
        /// </summary>
        private readonly ActionHistory _actionHistory;

        /// <summary>
        /// Creates a new <see cref="PercentageOfLastReceivedMoveDamageCalculator"/> instance.
        /// </summary>
        /// <param name="defaultAmount">The default amount of damage to return.</param>
        /// <param name="actionHistory">The action history.</param>
        public PercentageOfLastReceivedMoveDamageCalculator(
            int defaultAmount,
            ActionHistory actionHistory)
        {
            _defaultAmount = defaultAmount;
            _actionHistory = actionHistory;
        }

        /// <inheritdoc/>
        public int Calculate(Character user, BattleSystem.Actions.Damage.Damage damage, Character target)
        {
            var result = _actionHistory.LastMoveDamageResultAgainst(user);

            if (result is null)
            {
                return _defaultAmount;
            }

            return Math.Max(1, result.Amount * damage.Power / 100);
        }
    }
}
