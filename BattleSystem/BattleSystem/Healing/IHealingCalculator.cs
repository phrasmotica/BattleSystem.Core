using BattleSystem.Characters;
using BattleSystem.Moves.Actions;

namespace BattleSystem.Healing
{
    /// <summary>
    /// Interface for calculating the health healed by the target when a healing move is used on it.
    /// </summary>
    public interface IHealingCalculator
    {
        /// <summary>
        /// Returns the health healed by the target when a healing move is used on it.
        /// </summary>
        /// <param name="user">The user of the heal.</param>
        /// <param name="heal">The heal.</param>
        /// <param name="target">The target of the heal.</param>
        int Calculate(Character user, Heal heal, Character target);
    }
}
