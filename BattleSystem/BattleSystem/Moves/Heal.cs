using BattleSystem.Characters;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Represents a healing move.
    /// </summary>
    public class Heal : IMove
    {
        /// <summary>
        /// Gets or sets the heal's name.
        /// </summary>
        public string Name { get; set; }

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
        public string Summary => $"{Name} ({RemainingUses}/{MaxUses} uses)";

        /// <summary>
        /// Gets or sets the heal's healing amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the heal's healing mode.
        /// </summary>
        public HealingMode HealingMode { get; set; }

        /// <summary>
        /// Creates a new <see cref="Heal"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="amount">The healing amount.</param>
        /// <param name="healingMode">The healing mode.</param>
        public Heal(string name, int maxUses, int amount, HealingMode healingMode)
        {
            Name = name;

            MaxUses = maxUses;
            RemainingUses = maxUses;

            Amount = amount;
            HealingMode = healingMode;
        }

        /// <inheritdoc />
        public bool CanUse()
        {
            return RemainingUses > 0;
        }

        /// <inheritdoc />
        public virtual void Use(Character user, Character target)
        {
            user.ReceiveHeal(this);

            RemainingUses--;
        }

        /// <summary>
        /// Returns a heal that heals the user by the given percentage of its max health.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="percentage">The percentage to heal by.</param>
        public static Heal HealByPercentage(string name, int maxUses, int percentage)
        {
            return new Heal(name, maxUses, percentage, HealingMode.Percentage);
        }
    }
}
