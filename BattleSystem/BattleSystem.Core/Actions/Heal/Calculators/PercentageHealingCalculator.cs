using System;
using BattleSystem.Core.Characters;

namespace BattleSystem.Core.Actions.Heal.Calculators
{
    /// <summary>
    /// Calculates healing equal to a percentage of the target's max health.
    /// </summary>
    public class PercentageHealingCalculator : IHealingCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, HealAction heal, Character target)
        {
            return Math.Max(1, target.MaxHealth * heal.Amount / 100);
        }
    }
}
