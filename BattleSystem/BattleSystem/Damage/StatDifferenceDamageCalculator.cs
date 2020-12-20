using System;
using BattleSystem.Characters;
using BattleSystem.Moves.Actions;

namespace BattleSystem.Damage
{
    /// <summary>
    /// Calculates damage based on the difference between the user's attack stat and the target's
    /// defence stat.
    /// </summary>
    public class StatDifferenceDamageCalculator : IDamageCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, Attack attack, Character target)
        {
            var userAttack = user.Stats.Attack.CurrentValue;
            var targetDefence = target.Stats.Defence.CurrentValue;

            // damage is offset by defence to a minimum of 1
            return Math.Max(1, attack.Power * (userAttack - targetDefence));
        }
    }
}
