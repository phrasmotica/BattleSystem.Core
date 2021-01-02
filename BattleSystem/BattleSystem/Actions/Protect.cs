using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Actions.Targets;
using System;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents a protecting action.
    /// </summary>
    public class Protect : IAction
    {
        /// <summary>
        /// The action target calculator.
        /// </summary>
        private IActionTargetCalculator _actionTargetCalculator;

        /// <summary>
        /// The targets for the protect action.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the protect action have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Sets the action target calculator for this protect action.
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
                throw new InvalidOperationException("Cannot use protect action when no targets have been set!");
            }

            var results = new List<IActionResult<TSource>>();

            foreach (var target in _targets.Where(c => !c.IsDead))
            {
                var result = target.AddProtect<TSource>(user);
                results.Add(result);
            }

            _targetsSet = false;

            return results;
        }
    }
}
