using System.Collections.Generic;
using BattleSystem.Characters;
using BattleSystem.Stats;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Represents a buffing move.
    /// </summary>
    public class Buff : IMove
    {
        /// <summary>
        /// Gets or sets the buff's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the buff's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the buff's maximum uses.
        /// </summary>
        public int MaxUses { get; set; }

        /// <summary>
        /// Gets or sets the buff's remaining uses.
        /// </summary>
        public int RemainingUses { get; set; }

        /// <summary>
        /// Gets a summary of the move.
        /// </summary>
        public string Summary => $"{Name} ({RemainingUses}/{MaxUses} uses) - {Description}";

        /// <summary>
        /// Gets or sets the buff's stat multipliers for the user.
        /// </summary>
        public IDictionary<StatCategory, double> UserMultipliers { get; set; }

        /// <summary>
        /// Gets or sets the buff's stat multipliers for the target.
        /// </summary>
        public IDictionary<StatCategory, double> TargetMultipliers { get; set; }

        /// <summary>
        /// Creates a new <see cref="Buff"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="userMultipliers">The user stat multipliers.</param>
        /// <param name="targetMultipliers">The target stat multipliers.</param>
        public Buff(string name, int maxUses, IDictionary<StatCategory, double> userMultipliers, IDictionary<StatCategory, double> targetMultipliers)
        {
            Name = name;

            MaxUses = maxUses;
            RemainingUses = maxUses;

            UserMultipliers = userMultipliers ?? new Dictionary<StatCategory, double>();
            TargetMultipliers = targetMultipliers ?? new Dictionary<StatCategory, double>();
        }

        /// <inheritdoc />
        public bool CanUse()
        {
            return RemainingUses > 0;
        }

        /// <inheritdoc />
        public virtual void Use(Character user, Character target)
        {
            user.ReceiveBuff(UserMultipliers);
            target.ReceiveBuff(TargetMultipliers);

            RemainingUses--;
        }

        /// <summary>
        /// Returns a buff that raises the user's attack by 10% of its base value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        public static Buff RaiseUserAttack(string name, int maxUses)
        {
            return new Buff(name, maxUses, new Dictionary<StatCategory, double>
            {
                [StatCategory.Attack] = 0.1
            }, null)
            {
                Description = "Raises the user's attack by 10%."
            };
        }
    }
}
