using BattleSystem.Characters;
using BattleSystem.Actions;

namespace BattleSystem.Damage
{
    /// <summary>
    /// Interface for calculating damage dealt by a character using an attack on a target character.
    /// </summary>
    public interface IDamageCalculator
    {
        /// <summary>
        /// Returns the damage dealt by the user in using the given attack on the target.
        /// </summary>
        /// <param name="user">The user of the attack.</param>
        /// <param name="attack">The attack.</param>
        /// <param name="target">The target of the attack.</param>
        int Calculate(Character user, Attack attack, Character target);
    }
}
