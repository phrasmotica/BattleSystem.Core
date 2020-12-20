using System.Collections.Generic;
using BattleSystem.Characters;

namespace BattleSystem.Moves.Targets
{
    /// <summary>
    /// Interface for calculating the target of a move.
    /// </summary>
    public interface IMoveTargetCalculator
    {
        /// <summary>
        /// Returns the character that should be targeted from the given characters.
        /// </summary>
        /// <param name="user">The user of the move.</param>
        /// <param name="otherCharacters">The other characters.</param>
        Character Calculate(Character user, IEnumerable<Character> otherCharacters);
    }
}
