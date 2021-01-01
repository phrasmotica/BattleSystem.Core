using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Actions.Results;
using BattleSystem.Actions.Targets;
using BattleSystem.Stats;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents a buffing action.
    /// </summary>
    public class Buff : IAction
    {
        /// <summary>
        /// The action target calculator.
        /// </summary>
        private IActionTargetCalculator _actionTargetCalculator;

        /// <summary>
        /// Gets or sets the buff's stat multipliers for the target.
        /// </summary>
        public IDictionary<StatCategory, double> TargetMultipliers { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Buff"/>.
        /// </summary>
        public Buff()
        {
            TargetMultipliers = new Dictionary<StatCategory, double>();
        }

        /// <summary>
        /// Sets the action target calculator for this buff.
        /// </summary>
        /// <param name="actionTargetCalculator">The action target calculator.</param>
        public void SetActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            _actionTargetCalculator = actionTargetCalculator;
        }

        /// <inheritdoc />
        public virtual IEnumerable<IActionResult<TSource>> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _actionTargetCalculator.Calculate(user, otherCharacters);

            var results = new List<IActionResult<TSource>>();

            foreach (var target in targets.Where(c => !c.IsDead).ToArray())
            {
                var result = target.ReceiveBuff<TSource>(TargetMultipliers, user);
                results.Add(result);
            }

            return results;
        }
    }
}
