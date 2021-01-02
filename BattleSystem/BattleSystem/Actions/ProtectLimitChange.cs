using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Actions.Targets;
using System;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents an action that changes the protect limit of the target character.
    /// </summary>
    public class ProtectLimitChange : IAction
    {
        /// <summary>
        /// The action target calculator.
        /// </summary>
        private IActionTargetCalculator _actionTargetCalculator;

        /// <summary>
        /// The targets for the protect limit change.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the protect limit change have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Gets or sets the amount to change the target's protect limit by.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Sets the action target calculator for this protect limit change.
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
        public IEnumerable<IActionResult<TSource>> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            if (!_targetsSet)
            {
                throw new InvalidOperationException("Cannot use protect limit change when no targets have been set!");
            }

            var results = new List<IActionResult<TSource>>();

            foreach (var target in _targets.Where(c => !c.IsDead))
            {
                var result = target.ChangeProtectLimit<TSource>(Amount, user);
                results.Add(result);
            }

            _targetsSet = false;

            return results;
        }
    }
}
