using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Interface for calculating the target(s) of a move.
    /// </summary>
    public interface IMoveTargetCalculator
    {
        /// <summary>
        /// Returns the character(s) that should be targeted from the given characters.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        IEnumerable<Character> Calculate(Character user, IEnumerable<Character> otherCharacters);
    }
}
