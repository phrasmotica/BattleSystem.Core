using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Actions.Targets;

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
            _targets = _actionTargetCalculator.Calculate(user, otherCharacters);
        }

        /// <inheritdoc />
        public virtual IEnumerable<IActionResult<TSource>> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            var results = new List<IActionResult<TSource>>();

            foreach (var target in _targets.Where(c => !c.IsDead))
            {
                var result = target.AddProtect<TSource>(user);
                results.Add(result);
            }

            return results;
        }
    }
}
