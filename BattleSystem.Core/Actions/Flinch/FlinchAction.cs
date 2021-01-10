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
        /// The action target calculator.
        /// </summary>
        private IActionTargetCalculator _actionTargetCalculator;

        /// <summary>
        /// The success calculator factory.
        /// </summary>
        private ActionSuccessCalculatorFactory _successCalculatorFactory;

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
        /// Gets or sets the tags for the flinch action.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// Sets the action target calculator for this flinch action.
        /// </summary>
        /// <param name="actionTargetCalculator">The action target calculator.</param>
        public void SetActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            _actionTargetCalculator = actionTargetCalculator;
        }

        /// <summary>
        /// Sets the action success calculator factory for this flinch action.
        /// </summary>
        /// <param name="actionSuccessCalculatorFactory">The action target calculator.</param>
        public void SetSuccessCalculatorFactory(ActionSuccessCalculatorFactory actionSuccessCalculatorFactory)
        {
            _successCalculatorFactory = actionSuccessCalculatorFactory;
        }

        /// <inheritdoc />
        public void SetTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            if (!_actionTargetCalculator.IsReactive)
            {
                EstablishTargets(user, otherCharacters);
            }
        }

        /// <inheritdoc />
        public ActionUseResult<TSource> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            if (_actionTargetCalculator.IsReactive)
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

                var success = _successCalculatorFactory().Calculate(this);
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
            var (success, targets) = _actionTargetCalculator.Calculate(user, otherCharacters);
            _targets = targets;
            _targetsSet = success;
        }
    }
}
