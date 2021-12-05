using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;

namespace BattleSystem.Core.Actions.ProtectLimitChange
{
    /// <summary>
    /// Represents an action that changes the protect limit of the target character.
    /// </summary>
    public class ProtectLimitChangeAction : IAction
    {
        /// <summary>
        /// The targets for the protect limit change.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the protect limit change have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Creates a new <see cref="ProtectLimitChangeAction"/> instance.
        /// </summary>
        public ProtectLimitChangeAction()
        {
            Tags = new HashSet<string>();
        }

        /// <summary>
        /// The action target calculator.
        /// </summary>
        public IActionTargetCalculator ActionTargetCalculator { get; set; }

        /// <summary>
        /// Gets or sets the amount to change the target's protect limit by.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the tags for the protect limit change.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// If the action target calculator is not reactive, set the targets for
        /// the protect limit change's next use.
        /// </summary>
        /// <param name="user">The user of the protect limit change.</param>
        /// <param name="otherCharacters">The other characters.</param>
        public virtual void SetTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            if (!ActionTargetCalculator.IsReactive)
            {
                EstablishTargets(user, otherCharacters);
            }
        }

        /// <inheritdoc />
        public ActionUseResult<TSource> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
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
                var result = target.ChangeProtectLimit<TSource>(Amount, user);
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
        /// Sets the targets for the protect limit change's next use.
        /// </summary>
        /// <param name="user">The user of the protect limit change.</param>
        /// <param name="otherCharacters">The other characters.</param>
        protected void EstablishTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            (_targetsSet, _targets) = ActionTargetCalculator.Calculate(user, otherCharacters);
        }
    }
}
