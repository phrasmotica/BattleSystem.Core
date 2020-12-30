using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Damage;
using BattleSystem.Actions.Results;
using BattleSystem.Moves.Targets;
using BattleSystem.Items;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents an attacking action.
    /// </summary>
    public class Attack : IAction
    {
        /// <summary>
        /// Delegate for a function that transforms the given power.
        /// </summary>
        /// <param name="power">The power.</param>
        public delegate int PowerTransform(int power);

        /// <summary>
        /// The damage calculator.
        /// </summary>
        private IDamageCalculator _damageCalculator;

        /// <summary>
        /// The move target calculator.
        /// </summary>
        private IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the attack's power.
        /// </summary>
        public int Power
        {
            get => TransformPower(power);
            set => power = value;
        }
        private int power;

        /// <summary>
        /// Gets or sets the list of power transforms.
        /// </summary>
        public List<PowerTransform> PowerTransforms { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Attack"/>.
        /// </summary>
        public Attack()
        {
            PowerTransforms = new List<PowerTransform>();
        }

        /// <summary>
        /// Sets the damage calculator for this attack.
        /// </summary>
        /// <param name="damageCalculator">The damage calculator.</param>
        public void SetDamageCalculator(IDamageCalculator damageCalculator)
        {
            _damageCalculator = damageCalculator;
        }

        /// <summary>
        /// Sets the move target calculator for this attack.
        /// </summary>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        public void SetMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            _moveTargetCalculator = moveTargetCalculator;
        }

        /// <inheritdoc />
        public virtual IEnumerable<IActionResult> Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);

            var results = new List<IActionResult>();

            foreach (var target in targets.Where(c => !c.IsDead).ToArray())
            {
                var damage = _damageCalculator.Calculate(user, this, target);
                var result = target.ReceiveDamage(damage, user.Id);
                results.Add(result);
            }

            return results;
        }

        /// <inheritdoc />
        public void ReceiveTransforms(Item item)
        {
            PowerTransforms.AddRange(item.AttackPowerTransforms);
        }

        /// <inheritdoc />
        public void ClearTransforms()
        {
            PowerTransforms.Clear();
        }

        /// <summary>
        /// Transforms the given power based on the list of power transforms.
        /// </summary>
        /// <param name="power">The power.</param>
        protected int TransformPower(int power)
        {
            var transformedPower = power;

            foreach (var t in PowerTransforms)
            {
                transformedPower = t(transformedPower);
            }

            return transformedPower;
        }
    }
}
