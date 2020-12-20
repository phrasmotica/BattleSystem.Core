using System.Collections.Generic;
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
        private readonly IHealingCalculator _healingCalculator;

        /// <summary>
        /// The move target calculator.
        /// </summary>
        private readonly IMoveTargetCalculator _moveTargetCalculator;

        /// <summary>
        /// Gets or sets the heal's healing amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Creates a new <see cref="Heal"/>.
        /// </summary>
        /// <param name="healingCalculator">The healing calculator.</param>
        /// <param name="moveTargetCalculator">The move target calculator.</param>
        /// <param name="amount">The healing amount.</param>
        public Heal(
            IHealingCalculator healingCalculator,
            IMoveTargetCalculator moveTargetCalculator,
            int amount)
        {
            _healingCalculator = healingCalculator;
            _moveTargetCalculator = moveTargetCalculator;

            Amount = amount;
        }

        /// <inheritdoc />
        public virtual void Use(Character user, IEnumerable<Character> otherCharacters)
        {
            var targets = _moveTargetCalculator.Calculate(user, otherCharacters);

            foreach (var target in targets)
            {
                var amount = _healingCalculator.Calculate(user, this, target);
                target.Heal(amount);
            }
        }

        /// <summary>
        /// Returns a heal that heals the user by the given percentage of its max health.
        /// </summary>
        /// <param name="percentage">The percentage to heal by.</param>
        public static Heal ByPercentage(int percentage)
        {
            return new Heal(
                new PercentageHealingCalculator(),
                new UserMoveTargetCalculator(),
                percentage);
        }

        /// <summary>
        /// Returns a heal that heals the user by the given amount.
        /// </summary>
        /// <param name="amount">The amount to heal by.</param>
        public static Heal ByAbsoluteAmount(int amount)
        {
            return new Heal(
                new AbsoluteHealingCalculator(),
                new UserMoveTargetCalculator(),
                amount);
        }
    }
}
