using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;

namespace BattleSystem.Core.Actions.Protect
{
    /// <summary>
    /// Represents a protecting action.
    /// </summary>
    public class ProtectAction : IAction
    {
        /// <summary>
        /// The targets for the protect action.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the protect action have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Creates a new <see cref="ProtectAction"/> instance.
        /// </summary>
        public ProtectAction()
        {
            Tags = new HashSet<string>();
        }

        /// <summary>
        /// The action target calculator.
        /// </summary>
        public IActionTargetCalculator ActionTargetCalculator { get; set; }

        /// <summary>
        /// Gets or sets the tags for the protect action.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// If the action target calculator is not reactive, set the targets
        /// for the protect action's next use.
        /// </summary>
        /// <param name="user">The user of the protect action.</param>
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

            foreach (var target in _targets.Where(c => !c.IsDead))
            {
                var result = target.AddProtect<TSource>(user);
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
        /// Sets the targets for the protect action's next use.
        /// </summary>
        /// <param name="user">The user of the protect action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        protected void EstablishTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            (_targetsSet, _targets) = ActionTargetCalculator.Calculate(user, otherCharacters);
        }
    }
}
