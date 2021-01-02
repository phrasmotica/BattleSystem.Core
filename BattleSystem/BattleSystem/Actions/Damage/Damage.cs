using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Actions.Targets;
using BattleSystem.Items;
using System;
using BattleSystem.Actions.Damage.Calculators;

namespace BattleSystem.Actions.Damage
{
    /// <summary>
    /// Represents an action that deals damage to the target.
    /// </summary>
    public class Damage : IAction, ITransformable
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
        /// The action target calculator.
        /// </summary>
        private IActionTargetCalculator _actionTargetCalculator;

        /// <summary>
        /// The targets for the next use of the damage action.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the damage action have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Gets or sets the damage action's power.
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
        /// Creates a new <see cref="Damage"/>.
        /// </summary>
        public Damage()
        {
            PowerTransforms = new List<PowerTransform>();
        }

        /// <summary>
        /// Sets the damage calculator for this damage action.
        /// </summary>
        /// <param name="damageCalculator">The damage calculator.</param>
        public void SetDamageCalculator(IDamageCalculator damageCalculator)
        {
            _damageCalculator = damageCalculator;
        }

        /// <summary>
        /// Sets the action target calculator for this damage action.
        /// </summary>
        /// <param name="actionTargetCalculator">The action target calculator.</param>
        public void SetActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            _actionTargetCalculator = actionTargetCalculator;
        }

        /// <inheritdoc />
        public virtual void SetTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            var (_, targets) = _actionTargetCalculator.Calculate(user, otherCharacters);
            _targets = targets;
            _targetsSet = true;
        }

        /// <inheritdoc />
        public virtual IEnumerable<IActionResult<TSource>> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            if (!_targetsSet)
            {
                throw new InvalidOperationException("Cannot use damage action when no targets have been set!");
            }

            var results = new List<IActionResult<TSource>>();

            foreach (var target in _targets.Where(c => !c.IsDead).ToArray())
            {
                var damage = _damageCalculator.Calculate(user, this, target);
                var result = target.ReceiveDamage<TSource>(damage, user);
                results.Add(result);
            }

            _targetsSet = false;

            return results;
        }

        /// <inheritdoc />
        public void ReceiveTransforms(Item item)
        {
            PowerTransforms.AddRange(item.DamagePowerTransforms);
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
