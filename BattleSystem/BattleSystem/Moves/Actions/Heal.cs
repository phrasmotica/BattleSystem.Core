using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters;
using BattleSystem.Healing;
using BattleSystem.Moves.Targets;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Represents a healing move action.
    /// </summary>
    public class Heal : IMoveAction
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
        public virtual bool Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);
            var applied = false;

            foreach (var target in targets.Where(c => !c.IsDead).ToArray())
            {
                applied = true;
                var amount = _healingCalculator.Calculate(user, this, target);
                target.Heal(amount);
            }

            return applied;
        }
    }
}
