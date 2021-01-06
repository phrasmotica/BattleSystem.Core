using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calculators
{
    /// <summary>
    /// Interface for calculating damage dealt by a character using a damage action on some targets.
    /// </summary>
    public interface IDamageCalculator
    {
        /// <summary>
        /// Returns the damage dealt by the user in using the given damage action on the given targets.
        /// </summary>
        /// <param name="user">The user of the damage action.</param>
        /// <param name="damage">The damage action.</param>
        /// <param name="targets">The targets of the damage action.</param>
        IEnumerable<DamageCalculation> Calculate(Character user, DamageAction damage, IEnumerable<Character> targets);
    }
}
