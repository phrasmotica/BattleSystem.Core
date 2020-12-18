using System;
using BattleSystem.Characters;
using BattleSystem.Moves;

namespace BattleSystem.Damage
{
    /// <summary>
    /// Interface for calculating damage dealt by a character using an attack on a target character.
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
