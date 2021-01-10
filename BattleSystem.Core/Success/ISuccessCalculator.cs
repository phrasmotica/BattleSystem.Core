using System.Collections.Generic;
using BattleSystem.Core.Characters;
using BattleSystem.Core.Moves;

namespace BattleSystem.Core.Success
{
    /// <summary>
    /// Interface for calculating whether a move succeeds.
    /// </summary>
    public interface ISuccessCalculator
    {
        /// <summary>
        /// Calculates how the given move succeeds.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="move">The move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        MoveUseResult Calculate(Character user, Move move, IEnumerable<Character> otherCharacters);
    }
}
