using System;
using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Damage.Calculators
{
    /// <summary>
    /// Calculates damage based on a base power value and the difference between the user's attack
    /// stat and the target's defence stat.
    /// </summary>
    public class BasePowerDamageCalculator : IDamageCalculator
    {
        /// <summary>
        /// The base power to use in calculating the damage to deal.
        /// </summary>
        private readonly int _basePower;

        /// <summary>
        /// The random number generator.
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Creates a new <see cref="BasePowerDamageCalculator"/> instance.
        /// </summary>
        /// <param name="basePower">The base power.</param>
        /// <param name="random">The random number generator.</param>
        public BasePowerDamageCalculator(int basePower, Random random)
        {
            _basePower = basePower;
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        /// <inheritdoc/>
        public IEnumerable<DamageCalculation> Calculate(Character user, DamageAction damage, IEnumerable<Character> targets)
        {
            var transformedBasePower = _basePower;

            foreach (var t in user.DamagePowerTransforms)
            {
                transformedBasePower = t(transformedBasePower);
            }

            var calculations = new List<DamageCalculation>();

            foreach (var target in targets)
            {
                var userAttack = user.CurrentAttack;
                var targetDefence = target.CurrentDefence;

                var normalisedAmount = transformedBasePower * (userAttack - targetDefence);

                // damage range with 80% as lower bound
                var varianceFactor = _random.Next(80, 101) / 100d;
                var amountWithVariance = (int) (normalisedAmount * varianceFactor);

                // damage is restricted to 70% if the action is hitting more than one target
                var spreadAmount = targets.Count() > 1 ? (int) (amountWithVariance * 0.7) : amountWithVariance;

                // damage is offset by defence to a minimum of 1
                var finalAmount = Math.Max(1, spreadAmount);

                calculations.Add(new DamageCalculation
                {
                    Target = target,
                    Success = true,
                    Amount = finalAmount,
                });
            }

            return calculations;
        }
    }
}
