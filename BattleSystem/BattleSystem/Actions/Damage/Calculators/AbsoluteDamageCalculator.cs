using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calculators
{
    /// <summary>
    /// Calculates damage equal to the damage action's power.
    /// </summary>
    public class AbsoluteDamageCalculator : IDamageCalculator
    {
        /// <summary>
        /// The amount of damage to deal.
        /// </summary>
        private readonly int _amount;

        /// <summary>
        /// Creates a new <see cref="AbsoluteDamageCalculator"/> instance.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        public AbsoluteDamageCalculator(int amount)
        {
            _amount = amount;
        }

        /// <inheritdoc/>
        public DamageCalculation Calculate(Character user, DamageAction damage, Character target)
        {
            return new DamageCalculation
            {
                Success = true,
                Amount = _amount,
            };
        }
    }
}
