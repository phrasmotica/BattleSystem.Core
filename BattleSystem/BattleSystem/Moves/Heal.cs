﻿using BattleSystem.Characters;
using BattleSystem.Healing;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Represents a healing move.
    /// </summary>
    public class Heal : IMove
    {
        /// <summary>
        /// The healing calculator.
        /// </summary>
        private readonly IHealingCalculator _healingCalculator;

        /// <summary>
        /// Gets or sets the heal's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the heal's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the heal's maximum uses.
        /// </summary>
        public int MaxUses { get; set; }

        /// <summary>
        /// Gets or sets the heal's remaining uses.
        /// </summary>
        public int RemainingUses { get; set; }

        /// <summary>
        /// Gets a summary of the move.
        /// </summary>
        public string Summary => $"{Name} ({RemainingUses}/{MaxUses} uses) - {Description}";

        /// <summary>
        /// Gets or sets the heal's healing amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Creates a new <see cref="Heal"/>.
        /// </summary>
        /// <param name="healingCalculator">The healing calculator.</param>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="amount">The healing amount.</param>
        public Heal(IHealingCalculator healingCalculator, string name, int maxUses, int amount)
        {
            _healingCalculator = healingCalculator;

            Name = name;

            MaxUses = maxUses;
            RemainingUses = maxUses;

            Amount = amount;
        }

        /// <inheritdoc />
        public bool CanUse()
        {
            return RemainingUses > 0;
        }

        /// <inheritdoc />
        public virtual void Use(Character user, Character target)
        {
            var amount = _healingCalculator.Calculate(user, this, target);
            user.Heal(amount);

            RemainingUses--;
        }

        /// <summary>
        /// Returns a heal that heals the user by the given percentage of its max health.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="percentage">The percentage to heal by.</param>
        public static Heal ByPercentage(string name, int maxUses, int percentage)
        {
            return new Heal(new PercentageHealingCalculator(), name, maxUses, percentage)
            {
                Description = $"Heals the user by {percentage}% of their max health."
            };
        }

        /// <summary>
        /// Returns a heal that heals the user by the given amount.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="amount">The amount to heal by.</param>
        public static Heal ByAbsoluteAmount(string name, int maxUses, int amount)
        {
            return new Heal(new AbsoluteHealingCalculator(), name, maxUses, amount)
            {
                Description = $"Heals the user by {amount} health."
            };
        }
    }
}
