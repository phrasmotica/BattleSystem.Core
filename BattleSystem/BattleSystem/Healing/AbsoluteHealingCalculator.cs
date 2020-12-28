using BattleSystem.Characters;
using BattleSystem.Actions;

namespace BattleSystem.Healing
{
    /// <summary>
    /// Calculates healing equal to the heal's amount.
    /// </summary>
    public class AbsoluteHealingCalculator : IHealingCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, Heal heal, Character target)
        {
            // ensure character cannot over-heal
            return heal.Amount;
        }
    }
}
