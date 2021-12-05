using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Stats;
using BattleSystem.Core.Characters.Targets;

namespace BattleSystem.Core.Actions.Buff
{
    /// <summary>
    /// Represents a buffing action.
    /// </summary>
    public class BuffAction : IAction
    {
        /// <summary>
        /// The targets for the next use of the buff.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the buff have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Gets or sets the buff's stat multipliers for the target.
        /// </summary>
        public IDictionary<StatCategory, double> TargetMultipliers { get; private set; }

        /// <summary>
        /// Gets or sets the tags for the buff.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// Creates a new <see cref="BuffAction"/>.
        /// </summary>
        public BuffAction()
        {
            TargetMultipliers = new Dictionary<StatCategory, double>();
            Tags = new HashSet<string>();
        }

        /// <summary>
        /// The action target calculator.
        /// </summary>
        public IActionTargetCalculator ActionTargetCalculator { get; set; }

        /// <summary>
        /// If the action target calculator is not reactive, set the targets for
        /// the buff's next use.
        /// </summary>
        /// <param name="user">The user of the buff.</param>
        /// <param name="otherCharacters">The other characters.</param>
        public virtual void SetTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            if (!ActionTargetCalculator.IsReactive)
            {
                EstablishTargets(user, otherCharacters);
            }
        }

        /// <inheritdoc />
        public virtual ActionUseResult<TSource> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            if (ActionTargetCalculator.IsReactive)
            {
                EstablishTargets(user, otherCharacters);
            }

            if (!_targetsSet)
            {
                return new ActionUseResult<TSource>
                {
                    Success = false,
                    Results = Enumerable.Empty<IActionResult<TSource>>(),
                };
            }

            var results = new List<IActionResult<TSource>>();

            foreach (var target in _targets.Where(c => !c.IsDead).ToArray())
            {
                var result = target.ReceiveBuff<TSource>(TargetMultipliers, user);
                result.Action = this;

                foreach (var tag in Tags)
                {
                    result.Tags.Add(tag);
                }

                results.Add(result);
            }

            _targetsSet = false;

            return new ActionUseResult<TSource>
            {
                Success = true,
                Results = results,
            };
        }

        /// <summary>
        /// Sets the targets for the buff's next use.
        /// </summary>
        /// <param name="user">The user of the buff.</param>
        /// <param name="otherCharacters">The other characters.</param>
        protected void EstablishTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            (_targetsSet, _targets) = ActionTargetCalculator.Calculate(user, otherCharacters);
        }
    }
}
