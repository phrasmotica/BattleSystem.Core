using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Healing;
using BattleSystem.Actions.Results;
using BattleSystem.Actions.Targets;
using System;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents a healing action.
    /// </summary>
    public class Heal : IAction
    {
        /// <summary>
        /// The healing calculator.
        /// </summary>
        private IHealingCalculator _healingCalculator;

        /// <summary>
        /// The action target calculator.
        /// </summary>
        private IActionTargetCalculator _actionTargetCalculator;

        /// <summary>
        /// The targets for the next use of the heal.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the heal have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Gets or sets the heal's healing amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Creates a new <see cref="Heal"/>.
        /// </summary>
        public Heal() { }

        /// <summary>
        /// Sets the healing calculator for this heal.
        /// </summary>
        /// <param name="healingCalculator">The healing calculator.</param>
        public void SetHealingCalculator(IHealingCalculator healingCalculator)
        {
            _healingCalculator = healingCalculator;
        }

        /// <summary>
        /// Sets the action target calculator for this heal.
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
        public virtual (bool success, IEnumerable<IActionResult<TSource>> results) Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            if (_actionTargetCalculator.IsReactive)
            {
                EstablishTargets(user, otherCharacters);
            }

            if (!_targetsSet)
            {
                return (false, Enumerable.Empty<IActionResult<TSource>>());
            }

            var results = new List<IActionResult<TSource>>();

            foreach (var target in _targets.Where(c => !c.IsDead).ToArray())
            {
                var amount = _healingCalculator.Calculate(user, this, target);
                var result = target.Heal<TSource>(amount, user);
                results.Add(result);
            }

            _targetsSet = false;

            return (true, results);
        }

        /// <summary>
        /// Sets the targets for the heal's next use.
        /// </summary>
        /// <param name="user">The user of the heal.</param>
        /// <param name="otherCharacters">The other characters.</param>
        protected void EstablishTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            var (success, targets) = _actionTargetCalculator.Calculate(user, otherCharacters);
                _targets = targets;
            _targetsSet = success;
        }
    }
}
