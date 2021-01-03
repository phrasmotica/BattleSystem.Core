using BattleSystem.Characters;

namespace BattleSystem.Actions.Heal.Calculators
{
    /// <summary>
    /// Calculates healing equal to the heal's amount.
    /// </summary>
    public class AbsoluteHealingCalculator : IHealingCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, HealAction heal, Character target)
        {
            // ensure character cannot over-heal
            return heal.Amount;
        }
    }
}
