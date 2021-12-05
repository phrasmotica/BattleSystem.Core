using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;
using BattleSystem.Core.Success;

namespace BattleSystem.Core.Actions.Flinch
{
    /// <summary>
    /// Represents an action that causes the target to flinch.
    /// </summary>
    public class FlinchAction : IAction
    {
        /// <summary>
        /// Delegate for a function that creates a success calculator.
        /// </summary>
        public delegate ISuccessCalculator<IAction, bool> ActionSuccessCalculatorFactory();

        /// <summary>
        /// The targets for the next use of the flinch action.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the flinch action have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Creates a new <see cref="FlinchAction"/>.
        /// </summary>
        public FlinchAction()
        {
            Tags = new HashSet<string>();
        }

        /// <summary>
        /// The action target calculator.
        /// </summary>
        public IActionTargetCalculator ActionTargetCalculator { get; set; }

        /// <summary>
        /// The success calculator factory.
        /// </summary>
        public ActionSuccessCalculatorFactory SuccessCalculatorFactory { get; set; }

        /// <summary>
        /// Gets or sets the tags for the flinch action.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <inheritdoc />
        public void SetTargets(Character user, IEnumerable<Character> otherCharacters)
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

            foreach (var target in _targets.Where(c => !c.IsDead).ToArray())
            {
                var result = new FlinchActionResult<TSource>
                {
                    Action = this,
                    User = user,
                    Target = target,
                };

                var success = SuccessCalculatorFactory().Calculate(this);
                if (success)
                {
                    result = target.Flinch<TSource>(user);

                    foreach (var tag in Tags)
                    {
                        result.Tags.Add(tag);
                    }
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
        /// Sets the targets for the flinch action's next use.
        /// </summary>
        /// <param name="user">The user of the flinch action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        protected void EstablishTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            (_targetsSet, _targets) = ActionTargetCalculator.Calculate(user, otherCharacters);
        }
    }
}
