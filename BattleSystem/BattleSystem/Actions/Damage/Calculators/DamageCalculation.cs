using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calculators
{
    /// <summary>
    /// Represents the result of a damage calculation.
    /// </summary>
    public class DamageCalculation
    {
        /// <summary>
        /// Gets or sets the target this damage calculation was calculated for.
        /// </summary>
        public Character Target { get; set; }

        /// <summary>
        /// Gets or sets whether the damage calculation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the calculated amount of damage.
        /// </summary>
        public int Amount { get; set; }
    }
}
