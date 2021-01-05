using BattleSystem.Characters;

namespace BattleSystem.Actions.Damage.Calculators
{
    /// <summary>
    /// Interface for calculating damage dealt by a character using a damage action on a target character.
    /// </summary>
    public interface IDamageCalculator
    {
        /// <summary>
        /// Returns the damage dealt by the user in using the given damage action on the target.
        /// </summary>
        /// <param name="user">The user of the damage action.</param>
        /// <param name="damage">The damage action.</param>
        /// <param name="target">The target of the damage action.</param>
        (bool success, int amount) Calculate(Character user, DamageAction damage, Character target);
    }
}
