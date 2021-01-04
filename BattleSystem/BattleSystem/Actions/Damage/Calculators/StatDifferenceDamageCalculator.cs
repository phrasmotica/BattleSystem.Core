using System;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calculators
{
    /// <summary>
    /// Calculates damage based on a base power value and the difference between the user's attack
    /// stat and the target's defence stat.
    /// </summary>
    public class StatDifferenceDamageCalculator : IDamageCalculator
    {
        /// <summary>
        /// Delegate for a function that transforms the given power.
        /// </summary>
        /// <param name="power">The power.</param>
        public delegate int PowerTransform(int power);

        /// <summary>
        /// The base power to use in calculating the damage to deal.
        /// </summary>
        private readonly int _basePower;

        /// <summary>
        /// Creates a new <see cref="StatDifferenceDamageCalculator"/> instance.
        /// </summary>
        /// <param name="basePower">The base power.</param>
        public StatDifferenceDamageCalculator(int basePower)
        {
            _basePower = basePower;
        }

        /// <inheritdoc/>
        public int Calculate(Character user, DamageAction damage, Character target)
        {
            var transformedPower = _basePower;

            if (user.HasItem)
            {
                foreach (var t in user.Item.DamagePowerTransforms)
                {
                    transformedPower = t(transformedPower);
                }
            }

            var userAttack = user.Stats.Attack.CurrentValue;
            var targetDefence = target.Stats.Defence.CurrentValue;

            // damage is offset by defence to a minimum of 1
            return Math.Max(1, transformedPower * (userAttack - targetDefence));
        }
    }
}
