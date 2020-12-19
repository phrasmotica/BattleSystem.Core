using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Actions
{
    /// <summary>
    /// Interface for a move action.
    /// </summary>
    public interface IMoveAction
    {
        /// <summary>
        /// Applies the effects of the move.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        void Use(Character user, IEnumerable<Character> otherCharacters);
    }
}
