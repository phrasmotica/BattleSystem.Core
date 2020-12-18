using System;
using BattleSystem.Characters;
using BattleSystem.Moves;

namespace BattleSystem.Healing
{
    /// <summary>
    /// Calculates healing equal to a percentage of the target's max health.
    /// </summary>
    public class PercentageHealingCalculator : IHealingCalculator
    {
        /// <inheritdoc/>
        public int Calculate(Character user, Heal heal, Character target)
        {
            return Math.Max(1, target.MaxHealth * heal.Amount / 100);
        }
    }
}
