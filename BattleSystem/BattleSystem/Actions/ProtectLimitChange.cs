using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Actions.Targets;

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
        public IEnumerable<IActionResult<TSource>> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _actionTargetCalculator.Calculate(user, otherCharacters);

            var results = new List<IActionResult<TSource>>();

            foreach (var target in targets.Where(c => !c.IsDead))
            {
                var result = target.ChangeProtectLimit<TSource>(Amount, user);
                results.Add(result);
            }

            return results;
        }
    }
}
