using System.Collections.Generic;
using System.Linq;
using BattleSystem.Core.Actions.Damage.Calculators;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Characters.Targets;

namespace BattleSystem.Core.Actions.Damage
{
    /// <summary>
    /// Represents an action that deals damage to the target.
    /// </summary>
    public class DamageAction : IAction
    {
        /// <summary>
        /// The targets for the next use of the damage action.
        /// </summary>
        private IEnumerable<Character> _targets;

        /// <summary>
        /// Whether the targets for the next use of the damage action have been set.
        /// </summary>
        private bool _targetsSet;

        /// <summary>
        /// Gets or sets the tags for the damage action.
        /// </summary>
        public HashSet<string> Tags { get; set; }

        /// <summary>
        /// Creates a new <see cref="DamageAction"/>.
        /// </summary>
        public DamageAction()
        {
            Tags = new HashSet<string>();
        }

        /// <summary>
        /// The damage calculator.
        /// </summary>
        public IDamageCalculator DamageCalculator { get; set; }

        /// <summary>
        /// The action target calculator.
        /// </summary>
        public IActionTargetCalculator ActionTargetCalculator { get; set; }

        /// <summary>
        /// If the action target calculator is not reactive, set the targets for
        /// the damage action's next use.
        /// </summary>
        /// <param name="user">The user of the damage action.</param>
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
                    Tags = new HashSet<string>
                    {
                        DamageActionResultTags.NoTargetsSet,
                    },
                };
            }

            // action only succeeds if damage is calculated against all targets successfully
            var damageCalculations = DamageCalculator.Calculate(user, this, _targets.Where(c => !c.IsDead));
            if (damageCalculations.Any(d => !d.Success))
            {
                return new ActionUseResult<TSource>
                {
                    Success = false,
                    Results = Enumerable.Empty<IActionResult<TSource>>(),
                    Tags = new HashSet<string>
                    {
                        DamageActionResultTags.DamageCalculationFailed,
                    },
                };
            }

            var results = new List<IActionResult<TSource>>();

            foreach (var calculation in damageCalculations)
            {
                var result = calculation.Target.ReceiveDamage<TSource>(calculation.Amount, user);
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
        /// Sets the targets for the damage action's next use.
        /// </summary>
        /// <param name="user">The user of the damage action.</param>
        /// <param name="otherCharacters">The other characters.</param>
        protected void EstablishTargets(Character user, IEnumerable<Character> otherCharacters)
        {
            (_targetsSet, _targets) = ActionTargetCalculator.Calculate(user, otherCharacters);
        }
    }
}
