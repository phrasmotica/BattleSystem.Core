using BattleSystem.Characters;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Represents an attacking move.
    /// </summary>
    public class Attack : IMove
    {
        /// <summary>
        /// Gets or sets the attack's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the attack's maximum uses.
        /// </summary>
        public int MaxUses { get; set; }

        /// <summary>
        /// Gets or sets the attack's remaining uses.
        /// </summary>
        public int RemainingUses { get; set; }

        /// <summary>
        /// Gets or sets the attack's power.
        /// </summary>
        public int Power { get; set; }

        /// <summary>
        /// Gets a summary of the move.
        /// </summary>
        public string Summary => $"{Name} ({RemainingUses}/{MaxUses} uses)";

        /// <summary>
        /// Creates a new <see cref="Attack"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="power">The power.</param>
        public Attack(string name, int maxUses, int power)
        {
            Name = name;

            MaxUses = maxUses;
            RemainingUses = maxUses;

            Power = power;
        }

        /// <inheritdoc />
        public bool CanUse()
        {
            return RemainingUses > 0;
        }

        /// <inheritdoc />
        public virtual void Use(Character user, Character target)
        {
            target.ReceiveAttack(this, user.Stats.Attack);

            RemainingUses--;
        }
    }
}
