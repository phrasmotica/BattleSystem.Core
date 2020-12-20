using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Represents an attacking move action.
    /// </summary>
    public class Attack : IMoveAction
    {
        /// <summary>
        /// The damage calculator.
        /// </summary>
        private readonly IDamageCalculator _damageCalculator;

        /// <summary>
        /// The move target calculator.
        /// </summary>
        private readonly IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the attack's power.
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Creates a new <see cref="Attack"/>.
        /// </summary>
        /// <param name="damageCalculator">The damage calculator.</param>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        /// <param name="power">The power.</param>
        public Attack(
            IDamageCalculator damageCalculator,
            IMoveTargetCalculator moveTargetCalculator,
            int power)
        {
            _damageCalculator = damageCalculator;
            _moveTargetCalculator = moveTargetCalculator;

            Power = power;
        }

        /// <inheritdoc />
        public virtual void Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);

            foreach (var target in targets)
            {
                var damage = _damageCalculator.Calculate(user, this, target);
                target.ReceiveDamage(damage);
            }
        }

        /// <summary>
        /// Returns an attack that calculates damage based on the difference between the user's
        /// attack stat and the target's defence stat.
        /// </summary>
        /// <param name="power">The power.</param>
        public static Attack ByStatDifference(int power)
        {
            return new Attack(
                new StatDifferenceDamageCalculator(),
                new FirstMoveTargetCalculator(),
                power);
        }

        /// <summary>
        /// Returns an attack that calculates damage equal to its power.
        /// </summary>
        /// <param name="power">The power.</param>
        public static Attack ByAbsolutePower(int power)
        {
            return new Attack(
                new AbsoluteDamageCalculator(),
                new FirstMoveTargetCalculator(),
                power);
        }

        /// <summary>
        /// Returns an attack that calculates damage equal to a percentage of the target's max health.
        /// </summary>
        /// <param name="percentage">The percentage.</param>
        public static Attack ByPercentage(int percentage)
        {
            return new Attack(
                new PercentageDamageCalculator(),
                new FirstMoveTargetCalculator(),
                percentage);
        }
    }
}
