using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Actions.Targets;
using BattleSystem.Core.Actions.Heal.Calculators;

namespace BattleSystem.Core.Actions.Heal
{
    /// <summary>
    /// Represents a healing action.
    /// </summary>
    public class HealAction : IAction
    {
        /// <summary>
        /// The healing calculator.
        /// </summary>
        private IHealingCalculator _healingCalculator;

        /// <summary>
        /// The action target calculator.
        /// </summary>
        private IActionTargetCalculator _actionTargetCalculator;

        /// <summary>
        /// The targets for the next use of the heal.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the heal have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Gets or sets the heal's healing amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the tags for the buff.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// Creates a new <see cref="HealAction"/>.
        /// </summary>
        public HealAction()
        {
            Tags = new HashSet<string>();
        }

        /// <summary>
        /// Sets the healing calculator for this heal.
        /// </summary>
        /// <param name="healingCalculator">The healing calculator.</param>
        public void SetHealingCalculator(IHealingCalculator healingCalculator)
        {
            _healingCalculator = healingCalculator;
        }

        /// <summary>
        /// Sets the action target calculator for this heal.
        /// </summary>
        /// <param name="actionTargetCalculator">The action target calculator.</param>
        public void SetActionTargetCalculator(IActionTargetCalculator actionTargetCalculator)
        {
            _actionTargetCalculator = actionTargetCalculator;
        }

        /// <summary>
        /// If the action target calculator is not reactive, set the targets for
        /// the heal's next use.
        /// </summary>
        /// <param name="user">The user of the heal.</param>
        /// <param name="otherCharacters">The other characters.</param>
        public virtual void SetTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            if (!_actionTargetCalculator.IsReactive)
            {
                EstablishTargets(user, otherCharacters);
            }
        }

        /// <inheritdoc />
        public virtual ActionUseResult<TSource> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
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
                var amount = _healingCalculator.Calculate(user, this, target);
                var result = target.Heal<TSource>(amount, user);
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
        /// Sets the targets for the heal's next use.
        /// </summary>
        /// <param name="user">The user of the heal.</param>
        /// <param name="otherCharacters">The other characters.</param>
        protected void EstablishTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            var (success, targets) = _actionTargetCalculator.Calculate(user, otherCharacters);
            _targets = targets;
            _targetsSet = success;
        }
    }
}
