using BattleSystem.Characters;
using BattleSystem.Damage;

namespace BattleSystem.Moves
{
    /// <summary>
    /// Represents an attacking move.
    /// </summary>
    public class Attack : IMove
    {
        /// <summary>
        /// The damage calculator.
        /// </summary>
        private readonly IDamageCalculator _damageCalculator;

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
        /// <param name="damageCalculator">The damage calculator.</param>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="power">The power.</param>
        public Attack(
            IDamageCalculator damageCalculator,
            string name,
            int maxUses,
            int power)
        {
            _damageCalculator = damageCalculator;

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
            var damage = _damageCalculator.Calculate(user, this, target);
            target.ReceiveDamage(damage);

            RemainingUses--;
        }

        /// <summary>
        /// Returns an attack that calculates damage based on the difference between the user's
        /// attack stat and the target's defence stat.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="maxUses">The power.</param>
        public static Attack ByStatDifference(string name, int maxUses, int power)
        {
            return new Attack(new StatDifferenceDamageCalculator(), name, maxUses, power);
        }

        /// <summary>
        /// Returns an attack that calculates damage equal to its power.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="power">The power.</param>
        public static Attack ByAbsolutePower(string name, int maxUses, int power)
        {
            return new Attack(new AbsoluteDamageCalculator(), name, maxUses, power);
        }

        /// <summary>
        /// Returns an attack that calculates damage equal to a percentage of the target's max health.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maxUses">The max uses.</param>
        /// <param name="percentage">The percentage.</param>
        public static Attack ByPercentage(string name, int maxUses, int percentage)
        {
            return new Attack(new PercentageDamageCalculator(), name, maxUses, percentage);
        }
    }
}
