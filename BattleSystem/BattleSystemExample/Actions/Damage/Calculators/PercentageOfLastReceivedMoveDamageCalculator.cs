using System;
using BattleSystem.Actions.Damage;
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
        /// The percentage of the damage to deal.
        /// </summary>
        private readonly int _percentage;

        /// <summary>
        /// The action history.
        /// </summary>
        private readonly ActionHistory _actionHistory;

        /// <summary>
        /// Creates a new <see cref="PercentageOfLastReceivedMoveDamageCalculator"/> instance.
        /// </summary>
        /// <param name="percentage">The percentage of damage to deal.</param>
        /// <param name="actionHistory">The action history.</param>
        public PercentageOfLastReceivedMoveDamageCalculator(
            int percentage,
            ActionHistory actionHistory)
        {
            _percentage = percentage;
            _actionHistory = actionHistory;
        }

        /// <inheritdoc/>
        public (bool success, int amount) Calculate(Character user, DamageAction damage, Character target)
        {
            var result = _actionHistory.LastMoveDamageResultAgainst(user);

            if (result is null)
            {
                return (false, default);
            }

            return (true, Math.Max(1, result.Amount * _percentage / 100));
        }
    }
}
