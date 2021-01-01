using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Healing;
using BattleSystem.Actions.Results;
using BattleSystem.Moves.Targets;
using BattleSystem.Items;

namespace BattleSystem.Actions
{
    /// <summary>
    /// Represents a healing action.
    /// </summary>
    public class Heal : IAction
    {
        /// <summary>
        /// The healing calculator.
        /// </summary>
        private IHealingCalculator _healingCalculator;

        /// <summary>
        /// The move target calculator.
        /// </summary>
        private IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the heal's healing amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Creates a new <see cref="Heal"/>.
        /// </summary>
        public Heal() { }

        /// <summary>
        /// Sets the healing calculator for this heal.
        /// </summary>
        /// <param name="healingCalculator">The healing calculator.</param>
        public void SetHealingCalculator(IHealingCalculator healingCalculator)
        {
            _healingCalculator = healingCalculator;
        }

        /// <summary>
        /// Sets the move target calculator for this heal.
        /// </summary>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        public void SetMoveTargetCalculator(IMoveTargetCalculator moveTargetCalculator)
        {
            _moveTargetCalculator = moveTargetCalculator;
        }

        /// <inheritdoc />
        public virtual IEnumerable<IActionResult<TSource>> Use<TSource>(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);

            var results = new List<IActionResult<TSource>>();

            foreach (var target in targets.Where(c => !c.IsDead).ToArray())
            {
                var amount = _healingCalculator.Calculate(user, this, target);
                var result = target.Heal<TSource>(amount, user);
                results.Add(result);
            }

            return results;
        }
    }
}
